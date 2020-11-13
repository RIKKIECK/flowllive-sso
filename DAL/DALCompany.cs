using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAL
{
    public class DALCompany
    {
        public DTOcompany Get(string id)
        {
            DTOcompany company = null;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Company_Get_By_Id", conn))
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
                            company = new DTOcompany();
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {

                                DataRow fila = tbResultados.Rows[i];
                                company.id = Convert.ToString(fila["id"]);
                                company.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                company.active = Convert.ToBoolean(fila["active"]);
                                company.name = Convert.ToString(fila["name"]);
                            }
                        }
                    }//FIN ADAPTER
                }
            }
            return company;
        }

        public List< DTOcompany >Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOcompany> lista = new List<DTOcompany>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Company_Get", conn))
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
                                DTOcompany company = new DTOcompany
                                {
                                    id = Convert.ToString(fila["id"]),
                                    name = Convert.ToString(fila["name"]),
                                    creationDate = Convert.ToDateTime(fila["creationDate"]),
                                    active = Convert.ToBoolean(fila["active"])
                                };
                                lista.Add(company);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public string Add(DTOcompany dto,DTOuser user)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Company_Add", conn))
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

        public bool Update(DTOcompany dto,DTOuser user)
        {
            var resultado = false;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Company_Update", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@id", dto.id));
                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            return resultado;
        }

        public Boolean Delete(string id,DTOuser user)
        {
            var resultado = false;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Company_Delete", conn))
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
    }
}
