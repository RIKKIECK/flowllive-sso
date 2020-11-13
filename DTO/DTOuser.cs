using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOuser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public DateTime birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public byte[] hash { get; set; }
        public byte[] salt { get; set; }
        public string imageUrl { get; set; }
        public DateTime lastConn { get; set; }
        public bool active { get; set; }
        public DateTime creationDate { get; set; }
        public DTOcountry country { get; set; }
        public DTOsubCompany subCompany { get; set; }
        public List<DTOBusinessPosition> listBusinessPosition { get; set; }
        public DTORol rol { get; set; }
        public DTOuser()
        {
            this.country = new DTOcountry();
            this.subCompany = new DTOsubCompany();
            this.listBusinessPosition = new List<DTOBusinessPosition>();
            this.rol = new DTORol();
        }

        public bool permitted(string permissionName)
        {
            for (int i = 0; i < this.rol.permissions.Count; i++)
            {
                if (this.rol.permissions[i].name.Equals(permissionName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
