using System.Threading;
using System.Web.Http;
using BO;
using DTO;
using SSOApi.Security;
using Newtonsoft.Json;
using System;
using log4net;
using System.Web.Http.Cors;
using System.Configuration;
using System.Net;

namespace SSOApi.Controllers
{
    /// <summary>
    /// controlador para validar tokens
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthenticateController : ApiController
    {
        private BOuser userBO = new BOuser();
        private ILog log = LogManager.GetLogger(typeof(AuthenticateController));
        private BOemail emailBO = new BOemail();
        /// <summary>
        /// Valida el token y retorna el usuario al cual se le asigno el token
        /// </summary>
        /// <returns>DTOuser</returns>
        [OAuth]
        public IHttpActionResult Get()
        {
            try
            {
                DTOuser usu = JsonConvert.DeserializeObject<DTOuser>(Thread.CurrentPrincipal.Identity.Name);
                
                return Ok(usu);
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar Validar su usuario.";
                emailBO.SendLogError(mensaje, ex, "Authenticate");
                if (!Convert.ToString(ConfigurationManager.AppSettings["ambiente"]).Equals("produccion"))
                {
                    log.Error("\r\n===============================================================\r\n" + mensaje + "\r\n===============================================================\r\n", ex);
                }
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
