using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSOApi.Models
{
    /// <summary>
    /// model                         
    /// </summary>
    public class SubCompanyModel
    {

        /// <summary>
        /// representa id de subcompany
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// representa el nombre de la subempresa
        /// </summary>
        [Required]
        public string name { get; set; }

        /// <summary>
        /// representa el identificador de la company
        /// </summary>
        public string companyId { get; set; }
    }
}