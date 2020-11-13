using SSOApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SSOApi.Services
{
    /// <summary>
    /// Clase para el manejo de datos del login
    /// </summary>
    public class LoginRepository
    {
        int iDRetorno;
        //User user = new User();

        

        /// <summary>
        /// Método de logeo para un usuario
        /// </summary>
        /// <returns>String con información del usuario</returns>
        //public User LogIn(Login login)
        //{
        //    #region Conexion BD
        //    var appSettings = ConfigurationManager.AppSettings;
        //    string connectionString = appSettings["ConnectionString"];
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    sqlConnection.Open();
        //    SqlCommand sqlCommand = new SqlCommand("exec sp_Adm_UsrLog @rutm, @psw", sqlConnection); //Ejecuto el SP
        //    sqlCommand.Parameters.AddWithValue("@rutm", login.Rutm);    //Rut del usuario sin DV
        //    sqlCommand.Parameters.AddWithValue("@psw", login.Pass);     //Password del usuario
        //    using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //    {
        //        // process the first result
        //        DisplayUser(reader);
        //        // use NextResult to move to the second result and verify it is returned
        //        if (!reader.NextResult())
        //            throw new InvalidOperationException("Esperando módulos, sólo se ha retornado datos del usuario");
        //        // process the second result
        //        DisplayModules(reader);
        //        reader.Close();
        //    }
        //    sqlConnection.Close();
        //    #endregion
        //    return null;
        //}

        private void DisplayUser(SqlDataReader reader)
        {

            
            if (reader.FieldCount < 2)
                throw new InvalidOperationException("Error en la busqueda");

            while (reader.Read())
            {
                //0,OK 1,RutM 2,IdEmp 3,IdPerfil 4,Perfil 5,Usuario
                
                iDRetorno = int.Parse(reader[0].ToString());
                if (iDRetorno >= 0)
                {
                    //user.Rut = reader[1].ToString();
                    //user.CompanyId = reader[2].ToString();
                    //user.ProfileId = reader[3].ToString();
                    //user.ProfileName = reader[4].ToString();
                    //user.FullName = reader[5].ToString();
                }
                else
                {
                    throw new InvalidOperationException(reader[1].ToString());
                }

            }
        }

        private void DisplayModules(SqlDataReader reader)
        {
            if (reader.FieldCount != 2)
                throw new InvalidOperationException("Módulos no asignados correctamente");
            if (reader.HasRows)
            {
                //user.Modules = new Dictionary<string, string>();
                while (reader.Read())
                {
                    //0,Modulo 1,CRUD
                   
                    
                    //user.Modules.Add(reader[0].ToString(),reader[1].ToString());
                    
               
                }
            }
            
            
        }
    }
}