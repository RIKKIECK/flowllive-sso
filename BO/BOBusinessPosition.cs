using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BO
{
    public class BOBusinessPosition
    {
        private DALBusinessPosition bpDAL = new DALBusinessPosition();

        /// <summary>
        /// retorna todos los BP registrados en la DB
        /// </summary>
        /// <returns></returns>
        public List<DTOBusinessPosition> Get()
        {
            return bpDAL.Get();
        }

        /// <summary>
        /// retorna todos los BP de un usuario
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<DTOBusinessPosition> Get(string user_id)
        {
            return bpDAL.Get(user_id);
        }

        /// <summary>
        /// agrega un BP a un usuario
        /// </summary>
        /// <param name="bp_id"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public string Add(string bp_id, string user_id)
        {
            return bpDAL.Add(bp_id,user_id);
        }

        /// <summary>
        /// desactiva un BP de un usuario
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="bp_id"></param>
        /// <returns></returns>
        public bool Delete(string user_id, string bp_id)
        {
            return bpDAL.Delete(user_id, bp_id);
        }
    }
}
