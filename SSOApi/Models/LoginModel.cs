using System.ComponentModel.DataAnnotations;

namespace SSOApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string pass { get; set; }
    }
}