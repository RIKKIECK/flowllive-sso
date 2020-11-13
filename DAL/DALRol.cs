using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DALRol
    {
        public List<DTORol> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTORol> lista = new List<DTORol>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Rol_Get", conn))
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
                                DTORol dto = new DTORol();
                                dto.id = Convert.ToString(fila["id"]);
                                dto.name = Convert.ToString(fila["name"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                lista.Add(dto);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public string Update(DTORol dto)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Rol_Update", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", dto.id));
                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
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

        public string Delete(string id)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Rol_Desactive", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", id));


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

        public DTORol Get(string id)
        {
            DTORol rol = null;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Rol_Get_By_Id", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", id));

                    //Se instancia el adapter, que sirve para ejecutar el comando.
                    using (SqlDataAdapter adap = new SqlDataAdapter(comando))
                    {
                        DataTable tbResultados = new DataTable();

                        //ejecuto el comando utilizando el adapter y lleno la tabla con los datos obtenidos.
                        adap.Fill(tbResultados);

                        //si se lograron extraer datos entonces agregare todos los objetos a la lista.
                        if (tbResultados.Rows.Count > 0 && tbResultados.Rows != null)
                        {
                            rol = new DTORol();
                            DataRow fila = tbResultados.Rows[0];
                            rol.id = Convert.ToString(fila["id"]);
                            rol.creationDate = Convert.ToDateTime(fila["creationDate"]);
                            rol.active = Convert.ToBoolean(fila["active"]);
                            rol.name = Convert.ToString(fila["name"]);

                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {
                                DataRow filaper = tbResultados.Rows[i];
                                DTOPermissions permision = new DTOPermissions();
                                permision.id = Convert.ToString(filaper["permissions_Id"]);
                                permision.creationDate = Convert.ToDateTime(filaper["permissions_creationDate"]);
                                permision.active = Convert.ToBoolean(filaper["permissions_active"]);
                                permision.name = Convert.ToString(filaper["permissions_name"]);

                                rol.permissions.Add(permision);
                            }
                        }
                    }//FIN ADAPTER
                }
            }
            return rol;
        }

        public string Add(DTOuser user, DTORol dto)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Rol_Add", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));
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

        public DTORol Get_By_User(string id)
        {
            DTORol rol = null;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Rol_Get_By_User", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@userId", id));

                    //Se instancia el adapter, que sirve para ejecutar el comando.
                    using (SqlDataAdapter adap = new SqlDataAdapter(comando))
                    {
                        DataTable tbResultados = new DataTable();

                        //ejecuto el comando utilizando el adapter y lleno la tabla con los datos obtenidos.
                        adap.Fill(tbResultados);

                        //si se lograron extraer datos entonces agregare todos los objetos a la lista.
                        if (tbResultados.Rows.Count > 0 && tbResultados.Rows != null)
                        {
                            rol = new DTORol();
                            DataRow fila = tbResultados.Rows[0];
                            rol.id = Convert.ToString(fila["id"]);
                            rol.creationDate = Convert.ToDateTime(fila["creationDate"]);
                            rol.active = Convert.ToBoolean(fila["active"]);
                            rol.name = Convert.ToString(fila["name"]);
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {
                                fila = tbResultados.Rows[i];
                                DTOPermissions permisions = new DTOPermissions();
                                permisions.id = Convert.ToString(fila["Permissions_id"]);
                                permisions.creationDate = Convert.ToDateTime(fila["Permissions_creationDate"]);
                                permisions.active = Convert.ToBoolean(fila["Permissions_active"]);
                                permisions.name = Convert.ToString(fila["Permissions_name"]);
                                rol.permissions.Add(permisions);
                            }
                        }
                    }//FIN ADAPTER
                }
            }
            return rol;
        }
    }
}
