using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using LastSurvey.Models;
namespace LastSurvey.Controllers
{
    public class SendMailerController : Controller
    {
        private ModelSurvey db = new ModelSurvey();
        //
        // GET: /SendMailer/  
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Index(LastSurvey.Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {

                MailMessage mail = new MailMessage();
                List<string> emailList = new List<string>();
                foreach(User u in db.Users)
                {
                    emailList.Add(u.Email.ToString());
                }
                String emails = "";
                foreach (string s in emailList)
                {
                    if(emails == "")
                    {
                           emails = s + ", ";
                    }
                    else emails = emails + s + ", ";
                }

                emails = emails.Remove(emails.Length - 2);

                mail.To.Add(emails);
                //mail.From = new MailAddress(_objModelMail.From);
                mail.From = new MailAddress("thesurveydau@gmail.com");
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("thesurveydau", "L4rvL4rv!");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("Submitted");
            }
            else
            {
                return View();
            }
        }

    }
}


