using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using rojgar.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace rojgar.Utilities
{
    public class SendMessage
    {
        private readonly EmailSettings emailSettings;
        private readonly SMSSettings smsSettings;
        public SendMessage(IOptions<EmailSettings> _emailSettings, IOptions<SMSSettings> _smsSettings)
        {
            emailSettings = _emailSettings.Value;
            smsSettings = _smsSettings.Value;
        }
        public async Task<bool> SendMail(string SendTo, string Subject, string sbody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailSettings.Mail);
                mail.To.Add(SendTo);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = sbody;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = emailSettings.Host;
                smtp.Port = Convert.ToInt32(emailSettings.Port);
                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                smtp.Timeout = 1000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new System.Net.NetworkCredential(emailSettings.Mail, emailSettings.Password);
                await smtp.SendMailAsync(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SendSMS(string SendTo, string Message, Int64 TemplateId)
        {
            try
            {
                if (SendTo.Length != 10)
                {
                    return false;
                }
                string userdetails = smsSettings.SMSId;
                string apiKey = smsSettings.SecretKey;
                string Password = smsSettings.Password;
                string MobileNumber = SendTo;
                string Msg = Message;
                string strUrl = "http://sms.bulksmsind.in/sendSMS?username=" + userdetails + "&message=" + Msg + "&sendername=" + smsSettings.DisplayName + "&smstype=TRANS&numbers=" + SendTo + "&apikey=" + apiKey + "&templateid=" + TemplateId;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebRequest request = WebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                var json = JsonConvert.DeserializeObject(dataString);
                s.Close();
                readStream.Close();
                response.Close();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
