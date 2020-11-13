using BO;
using DTO;
using log4net;
using SSOApi.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using SSOApi.Models;
using FunctionallyLibrary;
using System.Net;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// controlador de usuario, todos los metodos contendran un fultro interno que retorna la info correspondiente a la empresa de casa usuario
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(UserController));
        private AmazonServices awsService = new AmazonServices();
        private MailServices mailService = new MailServices();
        private PasswordServices pservice = new PasswordServices();
        private BOuser userBO = new BOuser();
        private BOBusinessPosition bpBO = new BOBusinessPosition();
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// retorna una lista de usuarios
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                usu = userBO.GetUser(usu.id);
                bool permitted = false;
                bool permettedAll = false;
                for (int i = 0; i < usu.rol.permissions.Count; i++)
                {
                    if (usu.rol.permissions[i].name.Equals("SSO-USER-READ"))
                    {
                        permitted = true;
                    }
                    if (usu.rol.permissions[i].name.Equals("SSO-USER-READ-ALL"))
                    {
                        permettedAll = true;
                    }
                }
                if (permettedAll)
                {
                    List<DTOuser> listUser = null;
                    listUser = userBO.Get();
                    return Ok(listUser);
                }
                else if (permitted)
                {
                    List<DTOuser> listUser = null;
                    listUser = userBO.GetUserByCompany(usu.subCompany.id);
                    return Ok(listUser);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar obtener usuarios.";
                emailBO.SendLogError(mensaje, ex, "User");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// retorna un usuario segun su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);
                return Ok(userBO.GetUser(id));

            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar Obtener un usuario.";
                emailBO.SendLogError(mensaje, ex, "User");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Recibe un usuario, lo agrega 
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(UserModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    
                    if (usu.permitted("SSO-USER-CREATE"))
                    {
                        DTOuser user = new DTOuser();
                        user.name = modelo.name;
                        user.lastName = modelo.lastName;
                        user.birthdate = modelo.birthdate;
                        user.phone = modelo.phone;
                        user.email = modelo.email;
                        user.country.id = modelo.countryId;
                        user.subCompany.id = modelo.subCompanyId;
                        user.imageUrl = awsService.UploadImage(modelo.imageUrl, modelo.email);
                        user.rol.id = modelo.rolId;
                        string pass = pservice.GenerateNewPassword();

                        user.salt = pservice.GetSalt();
                        user.hash = pservice.GetSecureHash(pass, user.salt);
                        user.id = userBO.Add(user, usu);

                        if (user.id.Equals("EXIST"))
                        {
                            return BadRequest("El email especificado ya esta registrado.");
                        }
                        else
                        {
                            mailService.SendNewPassword(user, pass);
                            if (modelo.listBusinessPosition.Count > 0)
                            {
                                for (int i = 0; i < modelo.listBusinessPosition.Count; i++)
                                {
                                    bpBO.Add(modelo.listBusinessPosition[i].id, user.id);
                                }
                            }
                            return Ok("Usuario agregado correctamente.");
                        }
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "User Post(UserModel modelo)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar agregar un User.";
                emailBO.SendLogError(mensaje, ex, "User");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// actualiza un usuario
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Put(UserModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    
                    if (usu.permitted("SSO-USER-UPDATE"))
                    {
                        bool cambiopass = false;
                        DTOuser user = new DTOuser();
                        DTOuser userold = userBO.GetUser(modelo.id);
                        user.id = modelo.id;
                        user.name = modelo.name;
                        user.lastName = modelo.lastName;
                        user.birthdate = modelo.birthdate;
                        user.phone = modelo.phone;
                        user.email = modelo.email;
                        user.country.id = modelo.countryId;
                        user.subCompany.id = usu.subCompany.id;
                        if (!String.IsNullOrEmpty(modelo.rolId) && modelo.rolId.Length > 34)
                        {
                            user.rol.id = modelo.rolId;
                        }
                        if (modelo.imageUrl != userold.imageUrl)
                        {
                            string algo = awsService.DeleteImgInBucket(userold.imageUrl);
                            user.imageUrl = modelo.imageUrl.Length > 120 ? awsService.UploadImage(modelo.imageUrl, modelo.email) : modelo.imageUrl; // Validación penca, momentania XD
                        }
                        else
                        {
                            user.imageUrl = modelo.imageUrl;
                        }
                        if (modelo.resetPassword)
                        {
                            string newpass = pservice.GenerateNewPassword();

                            mailService.SendNewPassword(user, newpass);
                            user.salt = pservice.GetSalt();
                            user.hash = pservice.GetSecureHash(newpass, user.salt);
                            cambiopass = true;
                        }
                        else
                        {
                            if (modelo.password != null && !modelo.password.Equals(""))
                            {
                                mailService.SendNewPassword(user, modelo.password);
                                user.salt = pservice.GetSalt();
                                user.hash = pservice.GetSecureHash(modelo.password, user.salt);
                                cambiopass = true;
                            }
                        }
                        //user.listBusinessPosition = modelo.listBusinessPosition;
                        userBO.Update(user, cambiopass, usu);

                        return Ok("Usuario actualizado correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "User Put(UserModel modelo)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al intentar actualizar un usuario.";
                emailBO.SendLogError(mensaje, ex, "User");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Oculta el registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);
                    userBO.Delete(id, usu);
                    return Ok("Usuario eliminado correctamente.");
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "User Delete(string id)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar eliminar un User.";
                emailBO.SendLogError(mensaje, ex, "User");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
