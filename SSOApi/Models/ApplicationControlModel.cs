using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SSOApi.Models
{
    /// <summary>
    /// modelo de application control
    /// </summary>
    public class ApplicationControlModel
    {
        
        /// <summary>
        /// id de usuario
        /// </summary>
        [Required]
        public string userId { get; set; }
        /// <summary>
        /// id de aplicacion
        /// </summary>
        [Required]
        public string applicationId { get; set; }
        /// <summary>
        /// id de rol
        /// </summary>
        [Required]
        public string rolId { get; set; }

        /// <summary>
        /// funciona que determina si es valido o no
        /// </summary>
        /// <returns></returns>
        public bool isValid()
        {
            bool result = false;
            if (!String.IsNullOrEmpty(this.userId) && !String.IsNullOrEmpty(this.applicationId) && !String.IsNullOrEmpty(this.rolId))
            {
                result = true;
            }
            return result;
        }
    }
}