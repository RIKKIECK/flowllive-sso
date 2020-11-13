using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BO
{
    public class BOsubCompany
    {
        private DALsubCompany subcompanyDAL = new DALsubCompany();

        public List<DTOsubCompany> Get(string id)
        {
            return subcompanyDAL.Get(id);
        }

        public string Add(string companyId,DTOsubCompany dto, DTOuser usu)
        {
            return subcompanyDAL.Add(companyId,dto, usu);
        }

        public string Update(DTOsubCompany dto, DTOuser usu)
        {
            return subcompanyDAL.Update(dto,usu);
        }

        public string Delete(string id, DTOuser usu)
        {
            return subcompanyDAL.Delete(id,usu);
        }
    }
}
