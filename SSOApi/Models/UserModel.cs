using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSOApi.Models
{
    /// <summary>
    /// modelo de usuario
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// representa el id del usuario
        /// no requerido al agregar
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// representa el primer nombre del usuario
        /// requerido
        /// </summary>
        [Required]
        public string name { get; set; }
        /// <summary>
        /// representa el apellido del usuario
        /// </summary>
        public string lastName { get; set; }
        /// <summary>
        /// representa la fecha de nacimiento del usuario
        /// </summary>
        public DateTime birthdate { get; set; }
        /// <summary>
        /// representa el numero de contacto del usuaario
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// representa el correo electronico del usuario
        /// </summary>
        [Required]
        public string email { get; set; }
        /// <summary>
        /// representa el link de la imagen de perfil de usuario
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// representa la contraseña del usuario
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// representa si el usuario resetea o no la password
        /// </summary>
        public bool resetPassword { get; set; }
        /// <summary>
        /// representa el pais al que pertenece el usuario
        /// </summary>
        [Required]
        public string countryId { get; set; }
        /// <summary>
        /// representa la compañia a la que pertenece el usuario
        /// </summary>
        [Required]
        public string subCompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(40, MinimumLength = 34)]
        public string rolId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DTOBusinessPosition> listBusinessPosition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserModel()
        {
            this.listBusinessPosition = new List<DTOBusinessPosition>();
        }
    }
}