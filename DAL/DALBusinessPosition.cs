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
    public class DALBusinessPosition
    {
        public List<DTOBusinessPosition> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOBusinessPosition> lista = new List<DTOBusinessPosition>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("BusinessPosition_Get_All", conn))
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
                                DTOBusinessPosition dto = new DTOBusinessPosition();
                                dto.id = Convert.ToString(fila["id"]);
                                dto.name = Convert.ToString(fila["name"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                lista.Add(dto);
                            }
                        }
                    }//FIN ADAPTER
                }//FIN COMANDO
            }//FIN CONEXION    
            return lista;
        }

        public List<DTOBusinessPosition> Get( string user_id)
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOBusinessPosition> lista = new List<DTOBusinessPosition>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("BusinessPosition_Get_By_User", conn))
                {
                    comando.Parameters.AddWithValue("@id", user_id);
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
                                DTOBusinessPosition dto = new DTOBusinessPosition();
                                dto.id = Convert.ToString(fila["id"]);
                                dto.name = Convert.ToString(fila["name"]);
                                dto.creationDate = Convert.ToDateTime(fila["creationDate"]);
                                dto.active = Convert.ToBoolean(fila["active"]);
                                lista.Add(dto);
                            }
                        }
                    }//FIN ADAPTER
                }//FIN COMANDO
            }//FIN CONEXION    
            return lista;
        }

        public string Add(string bp_id, string user_id )
        {
            string response = "";
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("BusinessPosition_Add", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@user_id", user_id));
                    comando.Parameters.Add(new SqlParameter("@bp_id", bp_id));

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

        public bool Delete(string user_id,string bp_id)
        {
            var resultado = false;
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                using (SqlCommand comando = new SqlCommand("BusinessPosition_Desactive", conn))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    comando.Parameters.Add(new SqlParameter("@user_id", user_id));
                    comando.Parameters.Add(new SqlParameter("@bp_id", bp_id));

                    comando.ExecuteNonQuery();

                    resultado = true;
                }
            }
            return resultado;
        }
    }
}
