using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOActivityLog
    {
        public string id { get; set; }
        public bool active { get; set; }
        public DateTime creationDate { get; set; }
        public string userId { get; set; }
        public string action { get; set; }
        public string description { get; set; }
    }
}
