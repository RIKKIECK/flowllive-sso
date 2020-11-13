using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using BO;
using SSOApi.Models;
using System.Web.Http.Cors;
using SSOApi.Security;
using log4net;
using Newtonsoft.Json;
using System.Threading;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// CRUD para subscripciones
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SubscriptionController : ApiController
    {
        private BOSubscription subsBO = new BOSubscription();
        private BOuser userBO = new BOuser();
        private ILog log = LogManager.GetLogger(typeof(SubscriptionController));
        private BOemail emailBO = new BOemail();
        /// <summary>
        /// retorna una lista de subscripciones
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                
                List<DTOsubscription> list = new List<DTOsubscription>();
                list = subsBO.Get_By_Company(usu.subCompany.id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al intentar obtener las suscripciones.";
                emailBO.SendLogError(mensaje, ex, "Subscription");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Retorna una subscripcion segun su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                DTOsubscription dto = new DTOsubscription();
                if (!String.IsNullOrEmpty(id) && id.Length>34)
                {
                    dto = subsBO.Get(id);
                    return Ok(dto);
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "Subscription Get(string id)");
                    return BadRequest(mensaje);
                }

            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar cargar un Subscription.";
                emailBO.SendLogError(mensaje, ex, "Subscription");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Agrega una subscripcion
        /// </summary>
        /// <param name="model"></param>
        [OAuth]
        public IHttpActionResult Post(SubscriptionModel model)
        {
            try
            {
                if (ModelState.IsValid && (model.startDate < model.endDate) )
                {
                    DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);
                    DTOsubscription dto = new DTOsubscription();
                    dto.startDate = model.startDate;
                    dto.endDate = model.endDate;
                    dto.company.id = model.companyId;
                    dto.name = model.name;
                    dto.description = model.description;
                    dto.ammount = model.ammount;
                    dto.usersQuantity = model.usersQuantity;
                    string result = subsBO.Add(dto, usu);
                    if (result.Equals("EXIST"))
                    {
                        return BadRequest("La Empresa ya posee una subscripción activa.");
                    }
                    else
                    {
                        return Ok("Subscripción agregada correctamente");
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "Subscription Post(SubscriptionModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al agregar una subscripción.";
                emailBO.SendLogError(mensaje, ex, "Subscription");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// actualiza una subscripcion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Put(SubscriptionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);
                    DTOsubscription dto = new DTOsubscription();
                    dto.id = model.id;
                    dto.startDate = model.startDate;
                    dto.endDate = model.endDate;
                    dto.name = model.name;
                    dto.description = model.description;
                    dto.ammount = model.ammount;
                    dto.usersQuantity = model.usersQuantity;
                    subsBO.Update(dto, usu);
                    return Ok("Subscripción actualizada correctamente.");
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "Subscription Put(SubscriptionModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al actualiza una subscripción.";
                emailBO.SendLogError(mensaje, ex, "Subscription");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// desactiva un registro de la DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                DTOuser usu = userBO.GetUser(JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name).id);

                subsBO.Delete(id, usu);
                return Ok("Eliminado correctamente");
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar desactivar una Subscription.";
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
