using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using DTO;

namespace FunctionallyLibrary
{
    public class MailServices
    {
        /// <summary>
        /// genera una contraseña y la envia por email al correo indicado
        /// </summary>
        /// <param name="user_email"></param>
        /// <returns></returns>
        public void SendNewPassword(DTOuser user,string contraseña)
        {
            //html de correo
            #region
            string mensaje =
                    "<div style='max-width: 700px; padding: 50px 0; margin: 0px auto; font-size: 14px>" +
                        "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; margin-bottom: 20px' >" +
                            "<tbody>" +
                                "<tr>" +
                                    "<td style='vertical-align: top; padding-bottom:30px; 'align='center'>" +
                                    "<a href='javascript:void(0)' target='_blank'>" +
                                    "<img style='width: 100px;' src='https://sso-xo.s3-sa-east-1.amazonaws.com/Company-Img/xyoLogo.png' alt='visitar pagina' style='border:none'>" +
                                    "</td>" +
                                "</tr>" +
                            "</tbody>" +
                        "</table>" +
                        "<div style='padding: 40px; background: #fff;'>" +
                            "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; '>" +
                                "<tbody>" +
                                    "<tr>" +
                                        "<td style='border -bottom:1px solid #f6f6f6;'><h1 style='font-size:14px; font-family:arial; margin:0px; font-weight:bold;'> Estimad@ " + user.name + " " + user.lastName + "</h1>" +
                                            "<p style='margin-top:0px; color:#bbbbbb;'> Instrucciones para ingresar en Flowlive.</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='padding:10px 0 30px 0;'><p>Su nueva contraseña de inicio de sesion.</p>" +
                                            "<center>" +
                                                "<h3 style='border-top:1px solid #f6f6f6; padding-top:20px; color:#777'> Contraseña:   " + contraseña + " </h3>" +
                                            "</center>" +
                                            "<b> Atte Equipo Flowlive </b> <br>" +
                                            "<b>Si no hizo esta solicitud, Contactese con su supervisor o directamente a soporte@flowlivesuite.com</b>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody>" +
                            "</table>" +
                        "</div>" +
                        "<div style='text-align: center; font-size: 12px; color: #b2b2b5; margin-top: 20px' >" +
                            "<p> Desarrollado por Xo-company </p>" +
                        "</div>" +
                    "</div>";
            #endregion
            try
            {
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("no-reply@flowlivesuite.com", "Xrt0234@");
                MailAddress from = new MailAddress("no-reply@flowlivesuite.com", String.Empty, System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(user.email);
                MailMessage message = new MailMessage(from, to);
                message.Body = mensaje;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "Contraseña Flowlive de "+user.name + " " +user.lastName + ".";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                
                client.Send(message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// envia un correo con el log de error al correo configurado en web.config
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="text"></param>
        public void SendLogError(string endpoint, Exception text)
        {
            //html de correo
            #region
            string agentName = Convert.ToString(ConfigurationManager.AppSettings["SuportAgentName"]);
            string agentEmail = Convert.ToString(ConfigurationManager.AppSettings["SuportAgentEmail"]);
            string mensaje =
                    "<div style='max-width: 700px; padding: 50px 0; margin: 0px auto; font-size: 14px>" +
                        "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; margin-bottom: 20px' >" +
                            "<tbody>" +
                                "<tr>" +
                                    "<td style='vertical-align: top; padding-bottom:30px; 'align='center'>" +
                                    "<a href='javascript:void(0)' target='_blank'>" +
                                    "<img style='width: 100px;' src='https://sso-xo.s3-sa-east-1.amazonaws.com/Company-Img/xyoLogo.png' alt='visitar pagina' style='border:none'>" +
                                    "</td>" +
                                "</tr>" +
                            "</tbody>" +
                        "</table>" +
                        "<div style='padding: 40px; background: #fff;'>" +
                            "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; '>" +
                                "<tbody>" +
                                    "<tr>" +
                                        "<td style='border -bottom:1px solid #f6f6f6;'><h1 style='font-size:14px; font-family:arial; margin:0px; font-weight:bold;'> Estimad@ " + agentName + "</h1>" +
                                            "<p style='margin-top:0px; color:#bbbbbb;'> Ha habido una caida inesperada en el sistema.</p>" +
                                        "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td style='padding:10px 0 30px 0;'><p>ERROR en :" + endpoint + ".</p>" +
                                            "<center>" +
                                                "<h3 style='border-top:1px solid #f6f6f6; padding-top:20px; color:#777'>Exception: <br> <code> " + text + "</code> </h3>" +
                                            "</center>" +
                                            "<b> Atte Equipo Flowlive </b> <br>" +
                                            "<b></b>" +
                                        "</td>" +
                                    "</tr>" +
                                "</tbody>" +
                            "</table>" +
                        "</div>" +
                        "<div style='text-align: center; font-size: 12px; color: #b2b2b5; margin-top: 20px' >" +
                            "<p> Desarrollado por Xo-company </p>" +
                        "</div>" +
                    "</div>";
            #endregion
            try
            {
                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("no-reply@flowlivesuite.com", "Xrt0234@");
                MailAddress from = new MailAddress("no-reply@flowlivesuite.com", String.Empty, System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(agentEmail);
                MailMessage message = new MailMessage(from, to);
                message.Body = mensaje;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "Alerta de caida en el sistema Livinn.";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
