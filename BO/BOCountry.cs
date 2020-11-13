using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BO
{
    public class BOCountry
    {
        private DALCountry countryDAL = new DALCountry();

        public List<DTOcountry> Get()
        {
            return countryDAL.Get();
        }
    }
}
