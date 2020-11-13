using System.ComponentModel.DataAnnotations;

namespace SSOApi.Models
{
    /// <summary>
    /// modelo de Company
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        /// representa el id que posee el registro en la DB
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// nombre de Company
        /// </summary>
        [Required]
        public string name { get; set; }
    }
}