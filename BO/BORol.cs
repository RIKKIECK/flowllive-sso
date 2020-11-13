using DTO;
using DAL;
using System.Collections.Generic;
using System;

namespace BO
{
    public class BORol
    {
        private DALRol roldal = new DALRol();
        private DALPermissions permiDAL = new DALPermissions();
        private BOPermissions permissionBO = new BOPermissions();

        /// <summary>
        /// retorna un Rol por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private DTORol Get(string id)
        {
            
            return roldal.Get(id);
        }

        /// <summary>
        /// retornauna lista con todos los roles registrados
        /// </summary>
        /// <returns></returns>
        public List<DTORol> Get()
        {
            List<DTORol> roles = roldal.Get();
            
            for (int i = 0; i < roles.Count; i++)
            {
                roles[i].permissions = permissionBO.Get(roles[i].id);
            }

            return roles;
        }

        /// <summary>
        /// retorna un role asignado a un usuario
        /// </summary>
        /// <param name="id">userId</param>
        /// <returns></returns>
        public DTORol Get_By_User(string id)
        {
            return roldal.Get_By_User(id);
        }

        public string Add(DTOuser user, DTORol dto)
        {
            dto.id = roldal.Add(user, dto);
            string result = "";
            if (dto.id.Length > 34)
            {
                for (int i = 0; i < dto.permissions.Count; i++)
                {
                    string resultado = permissionBO.AddToRol(dto.id,dto.permissions[i].id);
                }
                result = "ok";
            }

            return result;
        }

        public string Update(DTOuser usu, DTORol dto)
        {
            string result = "";

            DTORol old = roldal.Get(dto.id);
            old.permissions = permiDAL.Get(dto.id);
            string resp = roldal.Update(dto);

            permissionBO.UpdateToRol(dto.id,old.permissions,dto.permissions);
            result = "ok";
            return result;
        }

        public string Delete(string id)
        {
            return roldal.Delete(id);
        }
        
    }
}
