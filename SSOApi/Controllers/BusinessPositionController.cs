using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using DTO;
using BO;
using SSOApi.Security;
using SSOApi.Models;
using log4net;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// Endpoint de BusinessPosition
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BusinessPositionController : ApiController
    {
        private BOBusinessPosition bpBO = new BOBusinessPosition();
        private ILog log = LogManager.GetLogger(typeof(BusinessPositionController));
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// 
        /// Retorna todos los BP registrados dependiendo del parametro.
        /// 
        /// PERMISOS : BUSINESSPOSITION-READ-ALL
        /// </summary>
        /// <param name="id">0 para traer todos, el id del usuario para traer uno en específio.</param>
        /// <returns> Lista de Cargos</returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("BUSINESSPOSITION-READ-ALL"))
                {
                    List<DTOBusinessPosition> list = new List<DTOBusinessPosition>();
                    list = bpBO.Get();
                    return Ok(list);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error al intentar cargar la lista de BusinessPosition.";
                emailBO.SendLogError(mensaje, ex, "BusinessPosition");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        
        /// <summary>
        /// Agrega un BP a un usuario
        /// 
        /// PERMISOS : BUSINESSPOSITION-CREATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(BusinessPositionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    if (usu.permitted("BUSINESSPOSITION-CREATE"))
                    {
                        bpBO.Add(model.bp_id, model.user_id);
                        return Ok("datos guardados correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "BussinessPsition Post(BusinessPositionModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar guardar un BusinessPosition.";
                emailBO.SendLogError(mensaje, ex, "BussinessPsition");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Quita un BP a un usuario
        /// 
        /// PERMISOS : BUSINESSPOSITION-DELETE
        /// </summary>
        /// <param name="user_id">usuario </param>
        /// <param name="bp_id">bussines position</param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Delete(string user_id,string bp_id)
        {
            try
            {

                if (!String.IsNullOrEmpty(user_id) && !String.IsNullOrEmpty(bp_id))
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    if (usu.permitted("BUSINESSPOSITION-DELETE"))
                    {
                        bpBO.Delete(user_id, bp_id);
                        return Ok("Registro desactivado correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "BusinessPosition Delete(string user_id,string bp_id)");
                    return BadRequest(mensaje);
                }
                
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar Desactivar un businessposition";
                emailBO.SendLogError(mensaje, ex, "BussinessPsition");
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
