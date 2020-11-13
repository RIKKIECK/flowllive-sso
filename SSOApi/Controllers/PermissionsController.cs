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
    /// EndPoint 
    /// para obtener Los Permisos
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PermissionsController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(PermissionsController));
        private BOPermissions permissionsBO = new BOPermissions();
        private BOuser userBO = new BOuser();
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// retorna una lista con todos los permisos registrados
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("PERMISSIONS-READ-ALL"))
                {
                    return Ok(permissionsBO.Get());
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar cargar la lista los Permisos.";
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
        /// retorna una lista de permisos de un rol por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("PERMISSIONS-READ-TO-ROL"))
                {

                    if (!String.IsNullOrEmpty(id) && id.Length > 34)
                    {
                        DTORol dto = new DTORol();
                        dto.id = id;
                        return Ok(permissionsBO.Get(id));
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Permissions Get(string id)");
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
                string mensaje = "Ha ocurrido un error al intentar cargar la lista los permisos por rol.";
                emailBO.SendLogError(mensaje, ex, "Permissions");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// agrega permisos a un rol
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(RolPermissionsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);
                    if (usu.permitted("PERMISSIONS-ADD"))
                    {
                        permissionsBO.Delete_All_By_Rol(model.rolId);
                        if (model.permissions.Count > 0)
                        {
                            for (int i = 0; i < model.permissions.Count; i++)
                            {
                                permissionsBO.Add(model.rolId, model.permissions[i], usu);
                            }
                        }
                        return Ok("Datos actualizados correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "Permissions  Post(RolPermissionsModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar agregar una lista de Permisos a un Rol.";
                emailBO.SendLogError(mensaje, ex, "Permissions");
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
