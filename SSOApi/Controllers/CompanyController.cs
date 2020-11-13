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
    /// Endpoint para Company
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : ApiController
    {
        private BOCompany companyBO = new BOCompany();
        private BOuser userBO = new BOuser();
        private ILog log = LogManager.GetLogger(typeof(CompanyController));
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// Retorna una lista con todas las Empresas
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                if (usu.permitted("SSO-COMPANY-READ-ALL"))
                {
                    List<DTOcompany> lista = new List<DTOcompany>();
                    lista = companyBO.Get();
                    return Ok(lista);
                }
                else
                {
                    return StatusCode( HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error inesperado al eliminar una Empresa.";
                emailBO.SendLogError(mensaje, ex, "Company");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// retorna una empresa segun si ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                
                DTOcompany comp = new DTOcompany();
                if (usu.permitted("SSO-COMPANY-READ"))
                {
                    comp = companyBO.Get(id);
                    return Ok(comp);
                }
                else if (usu.permitted("SSO-COMPANY-READ-ALL"))
                {
                    comp = companyBO.Get(id);
                    List<DTOsubCompany> subcomp = new List<DTOsubCompany>();
                    for (int i = 0; i < comp.subCompany.Count; i++)
                    {
                        if (comp.subCompany[i].id.Equals(usu.subCompany.id))
                        {
                            subcomp.Add(comp.subCompany[i]);
                        }
                    }
                    if (comp.subCompany.Count<1)
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        return Ok(comp);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
                
            }
            catch (Exception ex)
            {
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error inesperado al Obtener una empresa.";
                emailBO.SendLogError(mensaje, ex, "Company");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// agrega una Company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Post(CompanyModel model)
        {
            try
            {
                
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                
                if (usu.permitted("SSO-COMPANY-CREATE"))
                {
                    if (ModelState.IsValid)
                    {
                        DTOcompany company = new DTOcompany
                        {
                            name = model.name
                        };
                        string result = companyBO.Add(company, usu);
                        if (result.Equals("EXIST"))
                        {
                            return BadRequest("Ya existe una empresa que posee el nombre " + model.name + ".");
                        }
                        else
                        {
                            return Ok("Datos guardados correctamente.");
                        }
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Company  Post(CompanyModel model)");
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
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error inesperadoa intentar agregar una empresa.";
                emailBO.SendLogError(mensaje, ex, "Company");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Actualiza una Company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Put(CompanyModel model)
        {
            try
            {
                
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                
                if (usu.permitted("SSO-COMPANY-UPDATE"))
                {
                    if (ModelState.IsValid && model.id != null && model.id != "")
                    {
                        DTOcompany comp = new DTOcompany
                        {
                            id = model.id,
                            name = model.name
                        };
                        companyBO.Update(comp, usu);
                        return Ok("Datos Actualizados correctamente.");
                    }
                    else
                    {
                        string mensaje = "Los datos suministrados son invalidos.";
                        emailBO.SendDataInvalid(mensaje + "Company  Put(CompanyModel model)");
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
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error al intentar actualizar un Empresa.";
                emailBO.SendLogError(mensaje, ex, "Company");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Desactiva una Company
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
                    if (usu.rol.permissions[i].name.Equals("SSO-COMPANY-DESACTIVE"))
                    {
                        permitted = true;
                    }
                }
                if (permitted)
                {
                    companyBO.Delete(id, usu);
                    return Ok("El registro ha sido desactivado correctamente");
                }
                else
                {
                    return StatusCode(HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error al intentar desactivar un Company.";
                emailBO.SendLogError(mensaje, ex, "Company");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
