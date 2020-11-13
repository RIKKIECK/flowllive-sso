using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class DALConexion
    {
        /// <summary>
        /// Conexión a base de datos de single signed on
        /// BUPA-SSO-DB-AWS
        /// BUPA-SSO-DB-XOSERVER
        /// 
        /// </summary>
        public static SqlConnection SQLconnSSO()
        {
            string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
            if (ambiente.Equals("produccion"))
            {
                SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["flowlive-sso"].ConnectionString);
                conexion.Open();
                return conexion;
            }
            else if (ambiente.Equals("QA"))
            {
                SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString);
                conexion.Open();
                return conexion;
            }
            else
            {
                SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[""].ConnectionString);
                conexion.Open();
                return conexion;
            }
        }
    }
}
