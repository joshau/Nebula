using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nebula.Error {

    public class ErrorNotification {

        public static void Send(
            Exception exception, 
            HttpRequest request, 
            string sender, 
            string recipients, 
            string subject, 
            string smtpServer, 
            int smtpPort = 25, 
            string smtpUsername = "", 
            string smtpPassword = "") {

            MailMessage message;
            SmtpClient mailer;
            StringBuilder sb;
            string betterException;

            if (exception.GetType() == typeof(HttpException)) {
                if (((HttpException)exception).GetHttpCode() == 404) return;
            }

            sb = new StringBuilder();
            betterException = exception.ToString();

            betterException = Regex.Replace(betterException, @"(\w:\S+:line \d+)", "<b>$1</b>");
            betterException = Regex.Replace(betterException, @"(\w:\S+\(\d+\):)", "<b>$1</b>");
            betterException = Regex.Replace(betterException, "   at", "<br />&nbsp;&nbsp;&nbsp;at ");
            
            sb.AppendLine(String.Format("<font size=\"5\" color=\"darkred\">{0}</font><br /><br />", exception.Message));
            sb.AppendLine("<font face=\"arial\" size=\"2\">");
            sb.AppendLine(String.Format("Generated: {0}<br /><br />", TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))));
            sb.AppendLine(betterException + "<br /><br /><br />");
            sb.AppendLine("<font size=\"3\"><b>Server Variables</b></font><br /><br />");

            sb.AppendLine("<table border=\"1\" cellspacing=\"0\">");
            sb.AppendLine("<tr><th>Name</th><th>Value</th></tr>");

            for (int i = 0; i < request.ServerVariables.Count; i++) {
                sb.AppendLine("<tr>");
                sb.AppendLine(String.Format("<td>{0}</td>\t<td>{1}</td>", request.ServerVariables.GetKey(i), request.ServerVariables.Get(i)));
                sb.AppendLine("</tr>");
            }
                
            sb.AppendLine("</table>");
            sb.AppendLine("</font>");

            // Send out the notification
            message = new MailMessage(sender, recipients, subject, sb.ToString());
            mailer = new SmtpClient(smtpServer);
            mailer.Port = Convert.ToInt32(smtpPort);
            
            if (!string.IsNullOrEmpty(smtpUsername)) mailer.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);

            message.IsBodyHtml = true;
            mailer.Send(message);
        }
    }
}
