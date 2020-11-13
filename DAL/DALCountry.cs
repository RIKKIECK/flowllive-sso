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
    public class DALCountry
    {
        public List<DTOcountry> Get()
        {
            //Se crea la lista que se enviara como resultado.
            List<DTOcountry> lista = new List<DTOcountry>();
            //Se instancia la conexion de datos
            using (SqlConnection conn = DALConexion.SQLconnSSO())
            {
                // se setea el comando que define el procedimiento almacenado y conexion a utilizar para obtener los datos desde la bd.
                using (SqlCommand comando = new SqlCommand("Country_Get_All", conn))
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
                                DTOcountry dto = new DTOcountry();
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
    }
}
