using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.IO;
using Newtonsoft.Json;

namespace ConversionApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //send email
        [HttpPost]
        public ActionResult SendEmail()
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("nik@fishbowlllc.com", "Nickolas Fisher");
            mail.To.Add(new MailAddress("nik@fishbowlllc.com", "Nickolas Fisher"));

            mail.Subject = "Hello From the Conversion App";

            mail.Body = "this is my test email";

            smtpClient.Send(mail);

            return new JsonResult(){ Data = true };
        }
        

        public ActionResult GetUsers()
        {
            WebRequest request = WebRequest.Create("https://jsonplaceholder.typicode.com/users");
            request.Method = "GET";

            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var text = reader.ReadToEnd();
                var userList = JsonConvert.DeserializeObject<List<Models.UserModel>>(text);
                return new JsonResult()
                {
                    Data = userList,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            throw new Exception("There was a problem fetching the users");
        }
    }
}