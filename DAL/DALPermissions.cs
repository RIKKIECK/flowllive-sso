using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALPermissions
    {
        public List<DTOPermissions> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOPermissions> lista = new List<DTOPermissions>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Permissions_Get_All", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    //Se instancia el adapter, que sirve para ejecutar el comando.
                    using (SqlDataAdapter adap = new SqlDataAdapter(comando))
                    {
                        DataTable tbResultados = new DataTable();

                        //ejecuto el comando utilizando el adapter y lleno la tabla con los datos obtenidos.
                        adap.Fill(tbResultados);

                        //si se lograron extraer datos entonces agregare todos los objetos a la lista.
                        if (tbResultados.Rows.Count > 0 && tbResultados.Rows != null)
                        {
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {
                                DataRow fila = tbResultados.Rows[i];
                                DTOPermissions nuevo = new DTOPermissions();
                                nuevo.id = Convert.ToString(fila["id"]);
                                nuevo.name = Convert.ToString(fila["name"]);
                                nuevo.active = true;
                                nuevo.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                lista.Add(nuevo);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        

        public string AddToRol(string rolId, string permissionId)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("RolPermission_Add", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@rolId",rolId));
                    comando.Parameters.Add(new SqlParameter("@permissionId", permissionId));
                    using (var read = comando.ExecuteReader())
                    {
                        read.Read();
                        if (read.HasRows)
                        {
                            response = Convert.ToString(read["id"]);
                        }
                    }
                    return response;
                }
            }
        }

        public string DeleteToRol(string rolId, string permissionId)
        {
            string result = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("RolPermission_Delete", conn))
                {

                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@rolId", rolId));
                    comando.Parameters.Add(new SqlParameter("@permissionsId", permissionId));

                    using (var read = comando.ExecuteReader())
                    {
                        read.Read();
                        if (read.HasRows)
                        {
                            result = "ok";
                        }
                    }
                }
            }
            return result;
        }

        public List<DTOPermissions> Get(string id)
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOPermissions> lista = new List<DTOPermissions>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Permissions_Get_By_Rol", conn))
                {
                    comando.Parameters.AddWithValue("@idRol", id);
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    //Se instancia el adapter, que sirve para ejecutar el comando.
                    using (SqlDataAdapter adap = new SqlDataAdapter(comando))
                    {
                        DataTable tbResultados = new DataTable();

                        //ejecuto el comando utilizando el adapter y lleno la tabla con los datos obtenidos.
                        adap.Fill(tbResultados);

                        //si se lograron extraer datos entonces agregare todos los objetos a la lista.
                        if (tbResultados.Rows.Count > 0 && tbResultados.Rows != null)
                        {
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {
                                DataRow fila = tbResultados.Rows[i];
                                DTOPermissions nuevo = new DTOPermissions();
                                nuevo.id = Convert.ToString(fila["id"]);
                                nuevo.name = Convert.ToString(fila["name"]);
                                nuevo.active = true;
                                nuevo.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                lista.Add(nuevo);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public bool Add(string rolId, DTOPermissions dto,DTOuser user)
        {
            bool result = false;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Permission_Add", conn))
                {

                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    comando.Parameters.Add(new SqlParameter("@permissionsId", dto.id));
                    comando.Parameters.Add(new SqlParameter("@rolId", rolId));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));

                    using (var read = comando.ExecuteReader())
                    {
                        read.Read();
                        if (read.HasRows)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool Delete_All_By_Rol(string rolId)
        {
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Permissions_Delete_All_By_Rol", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    comando.Parameters.Add(new SqlParameter("@rolId", rolId));
                    int resp = comando.ExecuteNonQuery();

                    if (resp > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
