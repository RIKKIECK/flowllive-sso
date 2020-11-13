using SSOApi.Models;
using SSOApi.Services;
using SSOApi.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSOApi.Security
{
    /// <summary>
    /// Clase que valida al usuario 
    /// </summary>
    public class SecureLogin
    {
       
        private static RutValidator rutValidator = new RutValidator();
        private static LoginRepository loginRepository = new LoginRepository();
        /// <summary>
        /// Valida al usuario
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static bool ValidateUser(Login login)
        {
            //Si el rut es incorrecto.
            if (!rutValidator.Validar(login.email))
                return false;

            //User user = loginRepository.LogIn(login);
            //Si el usuario es real devolverá modulos a los cuales esta asignado.
            //
            return false;
            
        }
    }
}