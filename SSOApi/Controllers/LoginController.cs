using BO;
using DTO;
using FunctionallyLibrary;
using log4net;
using SSOApi.Models;
using SSOApi.Security;
using SSOApi.Services;
using SSOApi.Validators;
using System;
using System.Configuration;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SSOApi.Controllers
{
    
    /// <summary>
    /// Controlador con todos los métodos para ser consumido como API
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {        
        private LoginRepository loginRepository = new LoginRepository();
        private BOuser userBO = new BOuser();
        private OAuth oAuth = new OAuth();
        private PasswordServices pservice = new PasswordServices();
        private ILog log = LogManager.GetLogger(typeof(LoginController));
        private BOemail emailBO = new BOemail();
        
        /// <summary>
        /// Identifica a un usuario en el sistema
        /// </summary>
        /// <param name="model">string email, string pass</param>
        public IHttpActionResult Post(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DTOuser user = new DTOuser
                    {
                        email = model.email
                    };
                    user = userBO.Login(user);

                    if (user != null)
                    {
                        if (pservice.ValidatePassword(model.pass,user.salt, user.hash))
                        {
                            user = userBO.GetUser(user.id);
                            string token = oAuth.CreateToken(user);
                            return Ok(new { token, user });
                        }
                        else
                        {
                            return BadRequest("Contraseña invalida.");
                        }
                    }
                    else 
                    {
                        return BadRequest("Usuario no registrado, verifique sus credenciales.");
                    }
                }
                else
                {
                    string mensaje = "Los datos suministrados son invalidos.";
                    emailBO.SendDataInvalid(mensaje + "Login Post(LoginModel model)");
                    return BadRequest(mensaje);
                }
            }
            catch (Exception ex)
            {
                string mensaje = "Ha ocurrido un error al intentar hacer Login.";
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
