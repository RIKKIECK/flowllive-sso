using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BOuser
    {
        private DALuser userDAL = new DALuser();
        private DALBusinessPosition bpDAL = new DALBusinessPosition();
        private BORol rolBO = new BORol();
        /// <summary>
        /// retorna un usuario con todos sus datos 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public DTOuser Login(DTOuser dto)
        {
            DTOuser user = userDAL.GetByEmail(dto.email);

            return user;
        }

        /// <summary>
        /// retorna todos los usuarios registrados
        /// </summary>
        /// <returns></returns>
        public List<DTOuser> Get()
        {
            
            return userDAL.Get();
        }

        /// <summary>
        /// retorna un usuario por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTOuser GetUser(string id)
        {
            DTOuser user = userDAL.GetUser(id);
            user.rol = rolBO.Get_By_User(id);
            if (user != null)
            {
                user.listBusinessPosition = bpDAL.Get(id);
            }
            
            return user;
        }

        /// <summary>
        /// retorna una lista con todos los usuarios de una empresa 
        /// </summary>
        /// <returns></returns>
        public List<DTOuser> GetUserByCompany(string id)
        {
            List<DTOuser> list = userDAL.GetUserByCompany(id);
            
            return list;
        }

        /// <summary>
        /// agrega un registro de usuario a la DB, retorna el id del nuevo registro
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public String Add(DTOuser dto,DTOuser user)
        {
            return userDAL.Add(dto,user);
        }

        /// <summary>
        /// desactiva un registro de usuario en la DB, retorna true si se desactivo de forma correcta
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id,DTOuser user)
        {
            return userDAL.Delete(id,user);
        }

        /// <summary>
        /// actualiza un registro de usuario, retorna true si se actualizo de forma correcta
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Update(DTOuser dto,bool cambiopass,DTOuser user)
        {
            List<DTOBusinessPosition> list = bpDAL.Get(dto.id);
            for (int i = 0; i < list.Count; i++)
            {
                bpDAL.Delete(dto.id,list[i].id);
            }
            for (int i = 0; i < dto.listBusinessPosition.Count; i++)
            {
                bpDAL.Add(dto.listBusinessPosition[i].id,dto.id);
            }
            if (cambiopass)
            {
                userDAL.ChangePassWord(dto,user);
            }
            return userDAL.Update(dto,user);
        }

        /// <summary>
        /// retorna un usuario segun su email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DTOuser GetByEmail(string email)
        {
            return userDAL.GetByEmail(email);
        }

        /// <summary>
        /// retorna una lista de usuario que contiene un application control relacionado con la application indicada por su id
        /// </summary>
        /// <param name="app_id"></param>
        /// <returns></returns>
        public List<DTOuser> Get_By_Application(string app_id)
        {
            return userDAL.Get_By_Application(app_id);
        }
        
    }
}
