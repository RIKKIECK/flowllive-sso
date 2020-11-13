
using ApiAlquimia.SlackWebHook;
using FunctionallyLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public class BOemail
    {
        private MailServices emails = new MailServices();
        private String slackUrl = "";
        private String slackAPI = "BUPA SSO API";

        public BOemail()
        {
            slackUrl = ConfigurationManager.AppSettings["Slack_Bot_Develop"];
        }

        /// <summary>
        /// segun el ambiente envia una alerta, ya sea email o slack.
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="ex"></param>
        /// <param name="ambiente"> produccion [email,slackbot] \n QA [slackbot] \n develop [slackbot]</param>
        public void SendLogError(string mensaje, Exception ex=null,string controllerName = null)
        {
            string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
            if (ambiente.Equals("produccion"))
            {
                //  EMAIL
                emails.SendLogError(mensaje, ex);
                //  SLACKBOT
                SlackClient client = new SlackClient(slackUrl);
                client.PostDangerMessage( slackAPI, ex.ToString(), ambiente + " " + controllerName);
            }
            else if (ambiente.Equals("QA"))
            {
                //  SLACKBOT
                SlackClient client = new SlackClient(slackUrl);
                client.PostDangerMessage(slackAPI, ex.ToString(), ambiente + " " + controllerName);
            }
            else
            {
                //  SLACKBOT
                SlackClient client = new SlackClient(slackUrl);
                client.PostDangerMessage( slackAPI, ex.ToString(), ambiente + " " + controllerName);
            }
        }
        
        /// <summary>
        /// envia una alerta indicando que los datos suministrados son invalidos
        /// </summary>
        /// <param name="mensaje"></param>
        public void SendDataInvalid(string mensaje,string controllerName = null)
        {
            SlackClient client = new SlackClient(slackUrl);
            string ambiente = Convert.ToString(ConfigurationManager.AppSettings["ambiente"]);
            if (ambiente.Equals("produccion"))
            {
                client.PostWarningMessage("SSO " + ambiente,  mensaje, "@rrojas " + controllerName);
            }
            else if (ambiente.Equals("QA"))
            {
                client.PostWarningMessage("SSO " + ambiente,  mensaje, "@rrojas " + controllerName);
            }
            else
            {
                client.PostWarningMessage("SSO " + ambiente,  mensaje, "@rrojas " + controllerName);
            }
            
        }
    }
}
