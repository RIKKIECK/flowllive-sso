using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class DALSubscription
    {
        public DTOsubscription Get(string id)
        {
            DTOsubscription dto = null;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Subscription_By_Id", conn))
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
                            dto = new DTOsubscription();
                            for (int i = 0; i < tbResultados.Rows.Count; i++)
                            {

                                DataRow fila = tbResultados.Rows[i];
                                dto.id = Convert.ToString(fila["id"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                dto.startDate = Convert.ToDateTime(fila["startDate"]);
                                dto.endDate = Convert.ToDateTime(fila["endDate"]);
                                dto.name = Convert.ToString(fila["name"]);
                                dto.description = Convert.ToString(fila["endDate"]);
                                dto.ammount = Convert.ToDouble(fila["ammount"]);
                                dto.usersQuantity = Convert.ToInt32(fila["usersQuantity"]);

                                dto.company.id = Convert.ToString(fila["Company_id"]);
                                dto.company.name = Convert.ToString(fila["Company_name"]);
                                dto.company.creationDate = Convert.ToDateTime(fila["Company_creationDate"]);
                                dto.company.active = Convert.ToBoolean(fila["Company_active"]);

                            }
                        }
                    }//FIN ADAPTER
                }
            }
            return dto;
        }

        public List<DTOsubscription> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOsubscription> lista = new List<DTOsubscription>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Subscription_Get", conn))
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
                                DTOsubscription dto = new DTOsubscription();
                                dto.id = Convert.ToString(fila["id"]);
                                dto.endDate = Convert.ToDateTime(fila["endDate"]);
                                dto.startDate = Convert.ToDateTime(fila["startDate"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                dto.company.id = Convert.ToString(fila["Company_id"]);
                                dto.company.name = Convert.ToString(fila["Company_name"]);
                                dto.company.creationDate = Convert.ToDateTime(fila["Company_creationDate"]);
                                dto.company.active = Convert.ToBoolean(fila["Company_active"]);
                                lista.Add(dto);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public List<DTOsubscription> Get_By_Company(string id){
            //Se crea la lista que se enviara como resultado.
            List<DTOsubscription> lista = new List<DTOsubscription>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Subscription_Get_By_Company", conn))
                {
                    comando.Parameters.AddWithValue("@id", id);
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
                                DTOsubscription dto = new DTOsubscription();
                                dto.id = Convert.ToString(fila["id"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                dto.startDate = Convert.ToDateTime(fila["startDate"]);
                                dto.endDate = Convert.ToDateTime(fila["endDate"]);
                                dto.name = Convert.ToString(fila["name"]);
                                dto.description = Convert.ToString(fila["description"]);
                                dto.ammount = Convert.ToDouble(fila["ammount"]);
                                dto.usersQuantity = Convert.ToInt32(fila["usersQuantity"]);


                                //company
                                dto.company.id = Convert.ToString(fila["Company_id"]);
                                dto.company.name = Convert.ToString(fila["Company_name"]);
                                dto.company.creationDate = Convert.ToDateTime(fila["Company_creationDate"]);
                                dto.company.active = Convert.ToBoolean(fila["Company_active"]);
                                lista.Add(dto);
                            }
                        }
                    }//FIN ADAPTER

                }//FIN COMANDO

            }//FIN CONEXION    

            return lista;
        }

        public string Add(DTOsubscription dto,DTOuser user)
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Subscription_Add", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@companyId", dto.company.id));
                    comando.Parameters.Add(new SqlParameter("@endDate", dto.endDate));
                    comando.Parameters.Add(new SqlParameter("@startDate", dto.startDate));
                    comando.Parameters.Add(new SqlParameter("@nane", dto.name));
                    comando.Parameters.Add(new SqlParameter("@description", dto.description));
                    comando.Parameters.Add(new SqlParameter("@ammount", dto.ammount));
                    comando.Parameters.Add(new SqlParameter("@usersQuantity", dto.usersQuantity));


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

        public bool Update(DTOsubscription dto,DTOuser user)
        {
            var resultado = false;

            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Subscription_Update", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.Add(new SqlParameter("@id", dto.id));
                    comando.Parameters.Add(new SqlParameter("@endDate", dto.endDate));
                    comando.Parameters.Add(new SqlParameter("@startDate", dto.startDate));
                    comando.Parameters.Add(new SqlParameter("@name", dto.name));
                    comando.Parameters.Add(new SqlParameter("@description", dto.description));
                    comando.Parameters.Add(new SqlParameter("@ammount", dto.ammount));
                    comando.Parameters.Add(new SqlParameter("@usersQuantity", dto.usersQuantity));

                    comando.Parameters.Add(new SqlParameter("@userName", user.name));
                    comando.Parameters.Add(new SqlParameter("@userLastName", user.lastName));
                    comando.ExecuteNonQuery();
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool Delete(string id,DTOuser user)
        {
            var resultado = false;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("Subscription_Desactive", conn))
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
