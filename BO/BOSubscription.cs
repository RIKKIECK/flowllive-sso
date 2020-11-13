using System.Collections.Generic;
using DAL;
using DTO;

namespace BO
{
    public class BOSubscription
    {
        private DALSubscription subscriptionDAL = new DALSubscription();

        /// <summary>
        /// retorna una subcripcion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOsubscription Get(string id)
        {
            return subscriptionDAL.Get(id);
        }

        /// <summary>
        /// returna una lista con todas las subscripciones
        /// </summary>
        /// <returns></returns>
        public List<DTOsubscription> Get()
        {
            return subscriptionDAL.Get();
        }

        /// <summary>
        /// retorna una lista de subscripciones de una empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DTOsubscription> Get_By_Company(string id)
        {
            return subscriptionDAL.Get_By_Company(id);
        }

        /// <summary>
        /// agrega una subscripcion y retorna su id
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Add(DTOsubscription dto,DTOuser user)
        {
            return subscriptionDAL.Add(dto,user);
        }

        /// <summary>
        /// actualiza una subscripcion
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Update(DTOsubscription dto,DTOuser user)
        {
            return subscriptionDAL.Update(dto,user);
        }

        /// <summary>
        /// desactiva una subscripcion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id,DTOuser user)
        {
            return subscriptionDAL.Delete(id,user);
        }
    }
}
