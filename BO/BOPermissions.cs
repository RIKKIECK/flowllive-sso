using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BO
{
    public class BOPermissions
    {
        private DALPermissions permissionsDAL = new DALPermissions();

        /// <summary>
        /// retorna una lista con todos los permisos registrados en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<DTOPermissions> Get()
        {
            return permissionsDAL.Get();
        }

        /// <summary>
        /// retorna una lista de permissions asignados a un Rol
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<DTOPermissions> Get(String id)
        {
            return permissionsDAL.Get(id);
        }

        /// <summary>
        /// agrega 
        /// </summary>
        /// <param name="rolId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Add(string rolId, DTOPermissions dto,DTOuser user)
        {
            
            return permissionsDAL.Add(rolId,dto,user);
        }

        /// <summary>
        /// elimina todos los permisos relacionados al rol indicado por su Id
        /// </summary>
        /// <param name="rolId"></param>
        /// <returns></returns>
        public bool Delete_All_By_Rol(string rolId)
        {
            return permissionsDAL.Delete_All_By_Rol(rolId);
        }

        /// <summary>
        /// asigna un permiso a un rol
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <returns></returns>
        internal string AddToRol(string id1, string id2)
        {
            return permissionsDAL.AddToRol(id1,id2);
        }

        /// <summary>
        /// actualiza la lista de permisos
        /// </summary>
        /// <param name="old"></param>
        /// <param name="newis"></param>

        internal void UpdateToRol(string id, List<DTOPermissions> old, List<DTOPermissions> newis)
        {
            for (int i = 0; i < old.Count; i++)
            {
                for (int j = 0; j < newis.Count; j++)
                {
                    if (old[i].id.Equals(newis[j].id))
                    {
                        old.Remove(old[i]);
                        newis.Remove(newis[j]);
                    }
                }
            }
            for (int i = 0; i < old.Count; i++)
            {
                string result = permissionsDAL.DeleteToRol(id, old[i].id);
            }
            for (int i = 0; i < newis.Count; i++)
            {
                
                permissionsDAL.AddToRol(id, newis[i].id);
            }
             
        }

        //internal void UpdateToRol(string id, List<DTOPermissions> old, List<DTOPermissions> newis)
        //{
        //    if (newis.Count == 0)
        //    {
        //        for (int i = 0; i < old.Count; i++)
        //        {
        //            string result = permissionsDAL.DeleteToRol(id,old[i].id);
        //        }
        //    }
        //    else if (old.Count == 0)
        //    {
        //        for (int i = 0; i < newis.Count; i++)
        //        {
        //            string result = permissionsDAL.AddToRol(id,newis[i].id);
        //        }
        //    }
        //    else
        //    {
        //        List<DTOPermissions> temlist = new List<DTOPermissions>();
        //        List<DTOPermissions> temlist2 = new List<DTOPermissions>();

        //        for (int i = 0; i < old.Count; i++)
        //        {
        //            for (int j = 0; j < newis.Count; j++)
        //            {
        //                string oldid = old[i].id;
        //                string newid = newis[j].id;
        //                if (oldid.Equals(newid))
        //                {
        //                    temlist.Add(old[i]);
        //                    temlist2.Add(newis[j]);
        //                }
        //            }
        //        }
        //        for (int i = 0; i < temlist.Count; i++)
        //        {
        //            for (int j = 0; j < old.Count; j++)
        //            {
        //                if (temlist[i].id.Equals(old[j]))
        //                {
        //                    old.Remove(old[j]);
        //                }
        //            }
        //        }
        //        for (int i = 0; i < temlist2.Count; i++)
        //        {
        //            for (int j = 0; j < newis.Count; j++)
        //            {
        //                if (temlist[i].id.Equals(newis[j].id))
        //                {
        //                    newis.Remove(newis[j]);
        //                }
        //            }
        //        }

        //        for (int i = 0; i < old.Count; i++)
        //        {
        //            string result = permissionsDAL.DeleteToRol(id, old[i].id);
        //        }
        //        for (int i = 0; i < newis.Count; i++)
        //        {
        //            string result = permissionsDAL.AddToRol(id, newis[i].id);
        //        }
        //    }
        //}
    }
}
