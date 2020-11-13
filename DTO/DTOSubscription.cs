using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTOsubscription
    {
        public string id { get; set; }
        public DateTime creationDate { get; set; }
        public bool active { get; set; }

        public DTOsubCompany company { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Double ammount { get; set; }
        public int usersQuantity { get; set; }

        public DTOsubscription()
        {
            this.company = new DTOsubCompany();
        }
    }
}
