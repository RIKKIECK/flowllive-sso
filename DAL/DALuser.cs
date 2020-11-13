using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALuser
    {
        public DTOuser GetByEmail(string email)
        {
            DTOuser user = null;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_By_Email", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@email", email));

                    //Se instancia el adapter, que sirve para ejecutar el comando.
                    using (SqlDataAdapter adap = new SqlDataAdapter(comando))
                    {
                        DataTable tbResultados = new DataTable();

                        //ejecuto el comando utilizando el adapter y lleno la tabla con los datos obtenidos.
                        adap.Fill(tbResultados);

                        //si se lograron extraer datos entonces agregare todos los objetos a la lista.
                        if (tbResultados.Rows.Count > 0 && tbResultados.Rows != null)
                        {
                            user = new DTOuser();
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {

                                DataRow fila = tbResultados.Rows[i];
                                user.id = Convert.ToString(fila["id"]);
                                user.name = Convert.ToString(fila["name"]);
                                user.lastName = Convert.ToString(fila["lastName"]);
                                user.email = email;
                                user.salt = (byte[])fila["salt"];
                                user.hash = (byte[])fila["hash"];
                            }
                        }
                    }//FIN ADAPTER
                }
            }
            return user;
        }

        public DTOuser GetUser(string id)
        {
            DTOuser user = null;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_Get_By_Id", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", id));

                    using (var read = comando.ExecuteReader())
                    {
                        read.Read();
                        if (read.HasRows)
                        {
                            user = new DTOuser();
                            user.id = id;
                            user.name = Convert.ToString(read["name"]);
                            user.lastName = Convert.ToString(read["lastName"]);
                            user.birthdate = Convert.ToDateTime(read["birthdate"]);
                            user.imageUrl = Convert.ToString(read["imageUrl"]);
                            user.phone = Convert.ToString(read["phone"]);
                            user.email = Convert.ToString(read["email"]);
                            user.lastConn = Convert.ToDateTime(read["lastConn"]);
                            user.creationDate = Convert.ToDateTime(read["creationDate"]);
                            user.active = Convert.ToBoolean(read["active"]);
                            //country
                            user.country.id = Convert.ToString(read["country_id"]);
                            user.country.name = Convert.ToString(read["country_name"]);
                            user.country.creationDate = Convert.ToDateTime(read["country_creationDate"]);
                            user.country.active = Convert.ToBoolean(read["country_active"]);
                            //company
                            user.subCompany.id = Convert.ToString(read["SubCompany_id"]);
                            user.subCompany.name = Convert.ToString(read["SubCompany_name"]);
                            user.subCompany.active = Convert.ToBoolean(read["SubCompany_active"]);
                            user.subCompany.creationDate = Convert.ToDateTime(read["SubCompany_creationDate"]);
                        }
                    }
                }
            }

            return user;
        }

        public List<DTOuser> GetUserByCompany(string company_id)
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOuser> lista = new List<DTOuser>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("User_Get_By_Company", conn))
                {
                    comando.Parameters.AddWithValue("@id", company_id);
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
                                DTOuser user = new DTOuser
                                {
                                    id = Convert.ToString(fila["id"]),
                                    name = Convert.ToString(fila["name"]),
                                    lastName = Convert.ToString(fila["lastName"]),
                                    birthdate = Convert.ToDateTime(fila["birthdate"]),
                                    phone = Convert.ToString(fila["phone"]),
                                    email = Convert.ToString(fila["email"]),
                                    lastConn = Convert.ToDateTime(fila["lastConn"]),
                                    active = Convert.ToBoolean(fila["active"]),
                                    creationDate = Convert.ToDateTime(fila["creationDate"]),
                                    imageUrl = Convert.ToString(fila["imageUrl"])
                                };
                                user.country.id = Convert.ToString(fila["Country_id"]);
                                user.country.name = Convert.ToString(fila["Country_name"]);
                                user.subCompany.id = Convert.ToString(fila["Company_id"]);
                                user.subCompany.name = Convert.ToString(fila["Company_name"]);
                                lista.Add(user);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public List<DTOuser> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOuser> lista = new List<DTOuser>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("User_Get_All", conn))
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
                                DTOuser user = new DTOuser
                                {
                                    id = Convert.ToString(fila["id"]),
                                    name = Convert.ToString(fila["name"]),
                                    lastName = Convert.ToString(fila["lastName"]),
                                    birthdate = Convert.ToDateTime(fila["birthdate"]),
                                    phone = Convert.ToString(fila["phone"]),
                                    email = Convert.ToString(fila["email"]),
                                    lastConn = Convert.ToDateTime(fila["lastConn"]),
                                    creationDate = Convert.ToDateTime(fila["creationDate"]),
                                    active = Convert.ToBoolean(fila["active"]),
                                    imageUrl = Convert.ToString(fila["imageUrl"])
                                };
                                user.country.id = Convert.ToString(fila["Country_id"]);
                                user.country.name = Convert.ToString(fila["Country_name"]);
                                user.subCompany.id = Convert.ToString(fila["Company_id"]);
                                user.subCompany.name = Convert.ToString(fila["Company_name"]);
                                lista.Add(user);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public string Add(DTOuser dto,DTOuser user)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_Add", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
                    comando.Parameters.Add(new SqlParameter("@lastName", dto.lastName));
                    comando.Parameters.Add(new SqlParameter("@birthdate", dto.birthdate));
                    comando.Parameters.Add(new SqlParameter("@phone", dto.phone));
                    comando.Parameters.Add(new SqlParameter("@email", dto.email));
                    comando.Parameters.Add(new SqlParameter("@salt", dto.salt));
                    comando.Parameters.Add(new SqlParameter("@hash", dto.hash));
                    comando.Parameters.Add(new SqlParameter("@imageUrl", dto.imageUrl));
                    comando.Parameters.Add(new SqlParameter("@countryId", dto.country.id));
                    comando.Parameters.Add(new SqlParameter("@subcompanyId", dto.subCompany.id));
                    comando.Parameters.Add(new SqlParameter("@rolId", dto.rol.id));
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

        public bool Delete(string id,DTOuser user)
        {
            var resultado = false;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_Delete", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", id));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));

                    comando.ExecuteNonQuery();

                    resultado = true;
                }
            }
            return resultado;
        }

        public bool Update(DTOuser dto,DTOuser user)
        {
            var resultado = false;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_Update", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", dto.id));
                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
                    comando.Parameters.Add(new SqlParameter("@lastName", dto.lastName));
                    comando.Parameters.Add(new SqlParameter("@birthdate", dto.birthdate));
                    comando.Parameters.Add(new SqlParameter("@phone", dto.phone));
                    comando.Parameters.Add(new SqlParameter("@email", dto.email));
                    comando.Parameters.Add(new SqlParameter("@imageUrl", dto.imageUrl));
                    comando.Parameters.Add(new SqlParameter("@subCompanyId", dto.subCompany.id));
                    comando.Parameters.Add(new SqlParameter("@countryId", dto.country.id));
                    comando.Parameters.Add(new SqlParameter("@rolId",dto.rol.id));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            return resultado;
        }

        public List<DTOuser> Get_By_Application(string app_id)
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOuser> lista = new List<DTOuser>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("User_Get_By_Application", conn))
                {
                    comando.Parameters.AddWithValue("@app_id", app_id);
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
			                    
			                    //appc.active as appc_active,
			                    //appc.creationDate as appc_creationDate,
			                    
                                DataRow fila = tbResultados.Rows[i];
                                DTOuser user = new DTOuser
                                {
                                    id = Convert.ToString(fila["id"]),
                                    name = Convert.ToString(fila["name"]),
                                    lastName = Convert.ToString(fila["lastName"]),
                                    birthdate = Convert.ToDateTime(fila["birthdate"]),
                                    phone = Convert.ToString(fila["phone"]),
                                    email = Convert.ToString(fila["email"]),
                                    lastConn = Convert.ToDateTime(fila["lastConn"]),
                                    active = Convert.ToBoolean(fila["active"]),
                                    creationDate = Convert.ToDateTime(fila["creationDate"])
                                };
                                user.country.id = Convert.ToString(fila["countryId"]);
                                user.subCompany.id = Convert.ToString(fila["companyId"]);
                                lista.Add(user);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }
        
        public bool ChangePassWord(DTOuser dto,DTOuser user)
        {
            var resultado = false;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("User_Update_Data_Pass", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", dto.id));

                    comando.Parameters.Add(new SqlParameter("@hash", dto.hash));
                    comando.Parameters.Add(new SqlParameter("@salt", dto.salt));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));

                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            return resultado;
        }
    }
}
