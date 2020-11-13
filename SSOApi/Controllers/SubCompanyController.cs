using DTO;
using SSOApi.Security;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BO;
using SSOApi.Models;
using log4net;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// EndPoint para Subempresa
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SubCompanyController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(SubCompanyController));
        private BOsubCompany subcompBO = new BOsubCompany();
        private BOemail emailBO = new BOemail();
        /// <summary>
        /// retorna una lista de Subcompany segun el id de 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                bool permitted = false;

                for (int i = 0; i < usu.rol.permissions.Count; i++)
                {
                    if (usu.rol.permissions[i].name.Equals("SSO-SUBCOMPANY-READ-ALL"))
                    {
                        permitted = true;
                    }
                }
                if (permitted)
                {
                    List<DTOsubCompany> lista = new List<DTOsubCompany>();
                    lista = subcompBO.Get(id);
                    return Ok(lista);
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al obtener las SubEmpresas.";
                emailBO.SendLogError(mensaje, ex, "SubCompany");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// agrega una nueva subempresa
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(SubCompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    bool permitted = false;
                    for (int i = 0; i < usu.rol.permissions.Count; i++)
                    {
                        if (usu.rol.permissions[i].name.Equals("SSO-SubCompany-Create"))
                        {
                            permitted = true;
                        }
                    }
                    if (permitted)
                    {
                        DTOsubCompany dto = new DTOsubCompany();
                        dto.name = model.name;
                        string result = subcompBO.Add(model.companyId, dto,usu);

                        return Ok("SubCompany Guardada correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "SubCompany Post(SubCompanyModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar Guardar una SubCompany.";
                emailBO.SendLogError(mensaje, ex, "SubCompany");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// actualiza una subempresa
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Put(SubCompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                    bool permitted = false;
                    for (int i = 0; i < usu.rol.permissions.Count; i++)
                    {
                        if (usu.rol.permissions[i].name.Equals("SSO-SubCompany-Update"))
                        {
                            permitted = true;
                        }
                    }
                    if (permitted)
                    {
                        DTOsubCompany dto = new DTOsubCompany();
                        dto.id = model.id;
                        dto.name = model.name;
                        string result = subcompBO.Update(dto, usu);

                        return Ok("SubCompany Actualizado correctamente.");
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "SubCompany Put(SubCompanyModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar Actualizar una SubCompany.";
                emailBO.SendLogError(mensaje, ex, "SubCompany");
                //si no esta en produccion se guardara un log.
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// desactiva una subempresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                bool permitted = false;

                for (int i = 0; i < usu.rol.permissions.Count; i++)
                {
                    if (usu.rol.permissions[i].name.Equals("SSO-SUBCOMPANY-DESACTIVE"))
                    {
                        permitted = true;
                    }
                }
                if (permitted)
                {
                    string result = subcompBO.Delete(id,usu);
                    return Ok("Desactivado correctamente.");
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error inesperado al desactivar una SubEmpresas.";
                emailBO.SendLogError(mensaje, ex, "SubCompany");
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
