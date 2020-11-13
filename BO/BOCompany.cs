using System.Collections.Generic;
using DTO;
using DAL;

namespace BO
{
    public class BOCompany
    {
        private DALCompany companyDAL = new DALCompany();
        private BOsubCompany subcompanyBO = new BOsubCompany();
        /// <summary>
        /// Retorna un registro de company con un id especificado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOcompany Get(string id)
        {
            DTOcompany company = companyDAL.Get(id);
            if (company!= null)
            {
                company.subCompany = subcompanyBO.Get(id);
            }
            return company;
        }

        /// <summary>
        /// retorna una lista de company's
        /// </summary>
        /// <returns></returns>
        public List<DTOcompany> Get()
        {
            return companyDAL.Get();
        }

        /// <summary>
        /// agrega una Empresa
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string Add(DTOcompany dto,DTOuser user)
        {
            return companyDAL.Add(dto,user);
        }

        /// <summary>
        /// actualiza un registro de company
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Update(DTOcompany dto,DTOuser user)
        {
            return companyDAL.Update(dto,user);
        }

        /// <summary>
        /// elimina un registro de empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id,DTOuser user)
        {
            return companyDAL.Delete(id,user);
        }


    }
}
