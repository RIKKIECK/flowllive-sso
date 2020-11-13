using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SSOApi.Models
{
    /// <summary>
    /// Modelo que valida una subscripcion
    /// </summary>
    public class SubscriptionModel
    {
        /// <summary>
        /// id de la subscription
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// fecha de termino de la subscripcion
        /// </summary>
        [Required]
        public DateTime endDate { get; set; }
        /// <summary>
        /// fecha de inicio de la subscripcion
        /// </summary>
        [Required]
        public DateTime startDate { get; set; }
        /// <summary>
        /// nombre de la suscripcion
        /// </summary>
        [Required]
        public string name { get; set; }
        /// <summary>
        /// descripcion de la suscripcion
        /// </summary>
        [Required]
        public string description { get; set; }
        /// <summary>
        /// valor de la suscripcion 
        /// </summary>
        [Required]
        public Double ammount { get; set; }
        /// <summary>
        /// cantidad de usuarios disponibles para agregar
        /// </summary>
        [Required]
        public int usersQuantity { get; set; }
        /// <summary>
        /// id de la compañia
        /// </summary>
        [Required]
        public string companyId { get; set; }
        /// <summary>
        /// id de la aplicacion
        /// </summary>
        
        
    }
}