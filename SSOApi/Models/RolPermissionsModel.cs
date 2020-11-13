using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DTO;


namespace SSOApi.Models
{
    /// <summary>
    /// representa la relacion entre un rol y los permisos para poder ser agregador o modificados
    /// </summary>
    public class RolPermissionsModel
    {
        /// <summary>
        /// representa al rol que poseera estos permisos
        /// </summary>
        [Required]
        public string rolId { get; set; }

        /// <summary>
        /// lista de permisos para agregar
        /// </summary>
        public List<DTOPermissions> permissions { get; set; }

        public RolPermissionsModel()
        {
            this.permissions = new List<DTOPermissions>();
        }
    }
}