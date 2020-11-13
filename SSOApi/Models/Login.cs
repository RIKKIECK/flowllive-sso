using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSOApi.Models
{
    /// <summary>
    /// Clase con lo requerido para el login
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Pass { get; set; }

        /// <summary>
        /// Constructor con parámetros
        /// </summary>
        /// <param name="rutm"></param>
        /// <param name="pass"></param>
        public Login(string rutm, string pass)
        {
            this.email = rutm;
            this.Pass = pass;
        }
    }
}