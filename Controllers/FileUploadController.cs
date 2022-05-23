using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using FileUploadWebApi2;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Net.Mail;
using System.Net.Mime;

namespace FileUploadWebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private string myXmlString;
        private object xmlDocument;
        private string EmaiBcc;
        private object fileUploader;
        private object bodyBuilder;

        public object MessageBox { get; private set; }

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        [Route("ReadXmlFile")]

        public string ReadXml()
        {
            string EmailFrom = string.Empty; ;
            string EmailTo = string.Empty;
            string EmailBody = string.Empty;
            string EmailCC = string.Empty;
            string EmailSubject = string.Empty;
            string returnVal = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Users\\Rajkishor\\Desktop\\emaildetails.xml");
            string filename = @"C:\Users\Rajkishor\Desktop\Resume.pdf";


            //XmlNodeList xmlNodeList = doc.SelectNodes("/info/collage");
            var de = doc.ChildNodes[0];
            var v = de.ChildNodes;
            foreach (var element in v)
            {
                XmlElement nodeelement = (XmlElement)element;
                switch (nodeelement.Name)
                {
                    case "EmailFrom":
                        EmailFrom = nodeelement.InnerText;
                        break;
                    case "EmailTo":
                        EmailTo = nodeelement.InnerText;
                        break;
                    case "EmailBody":
                        EmailBody = nodeelement.InnerText;
                        break;

                    case "EmailCC":
                        EmailCC = nodeelement.InnerText;
                        break;
                    case "EmailSubject":
                        EmailSubject = nodeelement.InnerText;
                        break;

                }
            }

            sendEmail(EmailFrom, EmailTo, EmailBody, EmailCC, EmailSubject, "9608501260raj", filename);
            return string.Empty;
        }

        private void sendEmail(string EmailFrom,
        string EmailTo,
        string EmailBody, string EmailCC, string EmailSubject, string password, string filename)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(EmailFrom);
            message.To.Add(new MailAddress(EmailTo));
            message.CC.Add(new MailAddress(EmailCC));
            message.Subject = "Dotnet core";
            MailMessage mail = new MailMessage();
            mail.Subject = EmailSubject;
          
            if (message.Attachments != null && message.Attachments.Any())
            {
                
            }

            message.IsBodyHtml = true; //to make message body as html  
            message.Body = EmailBody;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(EmailFrom, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

    }
}
