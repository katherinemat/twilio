using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Twilio.Controllers
{
    public class HomeController : Controller
    {
        private TwilioContext db = new TwilioContext();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages()
        {
            ViewBag.Contacts = db.Contacts.ToList();
            ViewBag.To = new SelectList(db.Contacts, "PhoneNumber", "FirstName");
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }
        [HttpPost]
        public IActionResult GetMessages(string newBody, string newTo)
        {
            Message newMessage = new Models.Message();
            newMessage.Body = newBody;
            newMessage.To = newTo;
            newMessage.Send();
            return RedirectToAction("Index");
        }

        public IActionResult SendMessage()
        {
            ViewBag.Contacts = db.Contacts.ToList();
            //ViewBag.To = new SelectList(db.Contacts, "PhoneNumber", "FirstName");
            //      ^^ This determines the model binding for the dropdown list
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(string body, string to)
        {
            Message newMessage = new Message();
            newMessage.To = to;
            newMessage.Body = body;
            newMessage.Send();
            return RedirectToAction("Index");
        }
    }
}
