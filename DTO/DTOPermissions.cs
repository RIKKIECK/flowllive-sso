using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOPermissions
    {
        public string id { get; set; }
        public DateTime creationDate { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
    }
}
