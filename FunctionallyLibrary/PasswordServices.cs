using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace FunctionallyLibrary
{
    public class PasswordServices
    {
        /// <summary>
        /// generar un salt aleatorio
        /// </summary>
        /// <returns></returns>
        public byte[] GetSalt()
        {
            var p = new RNGCryptoServiceProvider();
            var salt = new byte[16];
            p.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// retorna un hash para la contraseña y salt especificados
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public byte[] GetSecureHash(string password, byte[] salt)
        {
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(password, salt);
            return PBKDF2.GetBytes(64);
        }

        /// <summary>
        /// genera una contraseña aleatoria
        /// </summary>
        /// <returns></returns>
        public string GenerateNewPassword()
        {
            Random obj = new Random();
            string posibles = "1234567890";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 6;
            string contraseña = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                contraseña += letra.ToString();
            }
            return contraseña;
        }

        /// <summary>
        /// verifica una contraseña con hash y salt
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="salt"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool ValidatePassword(string pass,byte[] salt, byte[] hash)
        {
            return GetSecureHash(pass, salt).SequenceEqual(hash);
        }

    }
}
