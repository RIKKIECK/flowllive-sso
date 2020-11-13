using DTO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSOApi.Models
{
    public class RolModel
    {
        public string id { get; set; }
        [Required]
        public string name { get; set; }

        public List<DTOPermissions> permissions { get; set; }
        public RolModel()
        {
            this.permissions = new List<DTOPermissions>();
        }
    }
}