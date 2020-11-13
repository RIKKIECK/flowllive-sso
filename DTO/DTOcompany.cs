using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTOcompany
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public DateTime creationDate { get; set; }
        public List<DTOsubscription> subscriptions { get; set; }
        public List<DTOsubCompany> subCompany { get; set; }
        public DTOcompany()
        {
            this.subscriptions = new List<DTOsubscription>();
            this.subCompany = new List<DTOsubCompany>();
        }
    }
}
