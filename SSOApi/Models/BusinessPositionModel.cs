using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSOApi.Models
{
    /// <summary>
    /// modelo de Businessposition
    /// </summary>
    public class BusinessPositionModel
    {
        /// <summary>
        /// id de businessposition
        /// </summary>
        [Required]
        public string bp_id { get; set; }

        /// <summary>
        /// id de usuario
        /// </summary>
        [Required]
        public string user_id { get; set; }
    }
}