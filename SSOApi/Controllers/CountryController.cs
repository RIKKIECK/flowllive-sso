using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTO;
using BO;
using System.Web.Http.Cors;
using SSOApi.Security;
using log4net;
using System.Configuration;

namespace SSOApi.Controllers
{
    /// <summary>
    /// Endpoint para Country
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CountryController : ApiController
    {
        private BOCountry countryBO = new BOCountry();
        private ILog log = LogManager.GetLogger(typeof(CountryController));
        private BOemail emailBO = new BOemail();

        /// <summary>
        /// Retorna una lista con todos los paises que estan registrados en la DB
        /// </summary>
        /// <returns></returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                List<DTOcountry> list = new List<DTOcountry>();
                list = countryBO.Get();
                return Ok(list);
            }
            catch (Exception ex)
            {
                string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
                string mensaje = "Ha ocurrido un error al intentar cargar la lista de Country.";
                emailBO.SendLogError(mensaje, ex, "Country");
                if (!ambiente.Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
        
    }
}
