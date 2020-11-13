using System;
using System.Web.Http;
using DTO;
using BO;
using SSOApi.Security;
using log4net;
using Newtonsoft.Json;
using System.Threading;
using System.Web.Http.Cors;
using System.Net;
using SSOApi.Models;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// endpoint para obtener roles
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RolController : ApiController
    {
        private BORol rolBO = new BORol();
        private BOuser userBO = new BOuser();
        private ILog log = LogManager.GetLogger(typeof(RolController));
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// endpoint para obtener todos los roles
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("ROL-READ-ALL"))
                {
                    return Ok(rolBO.Get());
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar cargar la lista los roles.";
                emailBO.SendLogError(mensaje, ex, "Archive");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// retorna una lista de roles disponibles para asignar una appcontrol de una aplicacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("ROL-READ"))
                {
                    if (!String.IsNullOrEmpty(id) && id.Length > 34)
                    {
                        return Ok(rolBO.Get_By_User(id));
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Rol Get(string id)");
                        return BadRequest(mensaje);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar cargar la lista de roles.";
                emailBO.SendLogError(mensaje, ex, "Archive");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(RolModel model)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("ROL-CREATE"))
                {
                    if (ModelState.IsValid)
                    {
                        DTORol dto = new DTORol();
                        dto.name = model.name;
                        dto.permissions = model.permissions;
                        string result = rolBO.Add(usu, dto);
                        return Ok("Datos guardados correctamente.");

                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Rol Post(RolModel model)");
                        return BadRequest(mensaje);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar agregar un rol.";
                emailBO.SendLogError(mensaje, ex, "Archive");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Put(RolModel model)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("ROL-UPDATE"))
                {
                    if (ModelState.IsValid && model.id != null && model.id.Length > 34)
                    {
                        DTORol dto = new DTORol();
                        dto.id = model.id;
                        dto.name = model.name;
                        dto.permissions = model.permissions;
                        string result = rolBO.Update(usu, dto);
                        return Ok("Datos actualizados correctamente.");
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Rol Put(RolModel model)");
                        return BadRequest(mensaje);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }

            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar actualizar un rol.";
                emailBO.SendLogError(mensaje, ex, "Rol");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("ROL-DELETE"))
                {
                    if (!String.IsNullOrEmpty(id) && id.Length > 34)
                    {
                        string result = rolBO.Delete(id);
                        return Ok("Rol eliminado correctamente.");
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Rol Delete(string id)");
                        return BadRequest(mensaje);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar eliminar un rol.";
                emailBO.SendLogError(mensaje, ex, "Archive");
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
