using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using Julian.Me.Web.Models;

namespace Julian.Me.Web.Controllers
{
  [JulianMeAttribute]
  public class ContactController : Controller
  {
    //
    // GET: /Contact/

    public ActionResult Index()
    {
      return View(TempData["Model"] ?? new MessageModel());
    }

    public ActionResult Send(MessageModel model)
    {
      //this.ValidateModel(model);

      if (!ModelState.IsValid)
      {
        return View("Index", model);
      }

      var mail = new MailMessage
      {
        Subject = model.Name,
        Body = model.Message,
        IsBodyHtml = false
      };

      mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["ContactFormEmailAddress"]));
      mail.From = new MailAddress(model.Email);

      var smtp = new SmtpClient(ConfigurationManager.AppSettings["MailServer"]);
      smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailServerUsername"], ConfigurationManager.AppSettings["MailServerPassword"]);

      try
      {
        smtp.Send(mail);
      }
      catch
      {
        TempData["Error"] = "Something went wrong while trying to send the message. You could try again later or just email me directly.";

        return View("Index", model);
      }

      return View("Index");
    }

  }
}
