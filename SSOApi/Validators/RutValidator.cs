using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SSOApi.Validators
{
    /// <summary>
    /// Clase para validación del Rut
    /// </summary>
    public class RutValidator
    {      
        /// <summary>
        /// Validaror de RUT
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>bool</returns>
        public bool Validar(string rut)
        {
            string matisaMinima = "1500";

            bool validacion = false;

            if(int.Parse(rut) < int.Parse(matisaMinima))
            {
                return validacion = false;
            }

            try
            {
                rut = rut+Dv(rut);
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
        /// <summary>
        /// Genera el digito verificador
        /// </summary>
        /// <param name="r">Rut</param>
        /// <returns>Dv</returns>
        private static string Dv(string r)
        {
            int suma = 0;
            for (int x = r.Length - 1; x >= 0; x--)
                suma += int.Parse(char.IsDigit(r[x]) ? r[x].ToString() : "0") * (((r.Length - (x + 1)) % 6) + 2);
            int numericDigito = (11 - suma % 11);
            string digito = numericDigito == 11 ? "0" : numericDigito == 10 ? "K" : numericDigito.ToString();
            return digito;
        }


    }
}