using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CourtCase.Models
{
    public class EmailService
    {  
        public string ComplainantEmailBody(string ComplainantName, string RefNo, string qrCodeUrl)
        {
            return $@"
            <p>Dear {ComplainantName},</p>
            
            <p>Welcome to the Departmental Enquiry System.</p>
            <p>A Departmental Enquiry has been initiated against you with reference number <b>{RefNo}</b>.</p>
             
            <hr/>

            <p>विभागीय जांच प्रणाली में आपका स्वागत है।</p>
            <p> आपके विरुद्ध विभागीय जांच प्रारंभ की गई है, जिसका संदर्भ संख्या <b>{RefNo}</b> है।</p>

            <p>For more details, please scan the QR Code:</p>
            <img src='{qrCodeUrl}' style='height:100px; width:100px; max-width:100px;' alt='QR Code' />
            ";
        }
        public string ComplainantSMSbody(string RefNo)
        {
            return $@"विभागीय जांच प्रणाली में आपका स्वागत है, आपके विरुद्ध विभागीय जांच प्रारंभ की गई है, जिसका संदर्भ संख्या {RefNo} है। उत्तर प्रदेश जल निगम";
        }
        public string InspectionEmailBody(string ComplainantName, string RefNo)
        {
            return $@"
            <p>प्रिय महोदय/महोदया,</p>
            <p>विभागीय जांच प्रणाली में आपका स्वागत है।</p>
            <p><b>{ComplainantName}</b> के विरुद्ध विभागीय जांच प्रारंभ की गई है, जिसमें आपको जांच अधिकारी नामित किया गया है। जिसका संदर्भ संख्या <b>{RefNo}</b> है।</p>
            <hr/>
            
            <p>Welcome to the Departmental Enquiry System.</p>
            <p>A Departmental Enquiry with reference number <b>{RefNo}</b> has been initiated against <b> {ComplainantName}</b>.You have been assigned as the enquiry officer.</p> 
            ";
        }


        public string HigherEmailBody(string forwardedDesignation, string RefNo)
        {
            return $@"
            <p>प्रिय महोदय/महोदया,</p>
            <p>विभागीय जाँच प्रणाली में आपका स्वागत है।</p>
            <p>विभागीय जाँच सन्दर्भ संख्या {RefNo} के निर्णय हेतु अग्रेषित कर दी गयी है|</p>
            <hr/>
            <p>Welcome to Departmental Enquiry System.</p> 
            <p>Departmental Enquiry with Reference number {RefNo} has been forwarded  for decision.</p> 
            ";
        }
        public static void SendEmail(string emailId, string subject, string body)
        {
            try
            {
                string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
                string fromEmailPassword = ConfigurationManager.AppSettings["EmailPassword"];

                var fromEmail = new MailAddress("noreply@upjnr.in", "Departmental Enquiry System");
                var toEmail = new MailAddress(emailId);
                string logoUrl = $"{baseUrl}/Jal_Nigam_Home_Page/img/Division_Checking_System_Logo.png";

                // Build HTML Email Body
                string htmlBody = $@"
        <html>
            <head>
                <style>
                    .container {{
                        background-color: #f0f0f0;
                        padding: 20px;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        font-family: Arial, sans-serif;
                    }}
                    .header {{
                        text-align: center;
                        margin-bottom: 20px;
                    }}
                    .footer {{
                        margin-top: 20px;
                        font-size: 10px;
                        color: #888;
                        text-align: center;
                    }}
                </style>
            </head>
            <body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px;"">
                <div class=""container"" style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
                    
                    <!-- Header Section -->
                    <div class=""header"" style=""text-align: center; padding-bottom: 20px;"">
                        <img src=""{logoUrl}"" style=""height: auto; width: auto; max-width: 200px;"" alt=""logo""><br>
                    </div>

                    <!-- Email Body Content -->
                    <div style=""color: #333; font-size: 16px; line-height: 1.6;"">
                        {body}
                    </div> 
                    
                    <!-- Closing Message -->
                    <p style=""margin-top: 20px; font-weight: bold;"">Best Regards,</p>
                    <p>Departmental Enquiry System</p>
                </div>

                <!-- Footer Section -->
                <div class=""footer"" style=""text-align: center; font-size: 14px; color: #666; margin-top: 20px;"">
                    This email was sent from the <a href=""{baseUrl}"" style=""color: #0073e6; text-decoration: none;"">Departmental Enquiry System</a>. <br>
                    &copy; {DateTime.Now.Year} All rights reserved.
                </div>
            </body>
        </html>";

                using (var smtp = new SmtpClient("mail.upjnr.in", 25))
                {
                    smtp.EnableSsl = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword);

                    using (var message = new MailMessage(fromEmail, toEmail)
                    {
                        Subject = subject,
                        Body = htmlBody,
                        IsBodyHtml = true
                    })
                    {
                        smtp.Send(message);
                    }
                }

                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }

        public static string SendHindiSMS(string mobileNo, string smsBody)
        {
            try
            {
                string senderId = "UPJNRD";
                string templateId = "1007666655915036505";
                string baseUrl = "http://164.52.195.161/API/SendMsg.aspx"; 
                string requestUrl = $"{baseUrl}?uname=20250018&pass=jSTuV99F&send={HttpUtility.UrlEncode(senderId)}" +
                                    $"&dest={HttpUtility.UrlEncode(mobileNo)}&msg={HttpUtility.UrlEncode(smsBody)}" +
                                    $"&template_id={HttpUtility.UrlEncode(templateId)}&unicode=1";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error sending SMS: {ex.Message}");
                return "Error";
            }
        }
    }
}
