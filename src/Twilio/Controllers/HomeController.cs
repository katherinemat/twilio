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
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }
        
        public IActionResult SendMessage()
        {
            ViewBag.To = new SelectList(db.Contacts, "PhoneNumber", "FirstName");
            //      ^^ This determines the model binding for the dropdown list
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message newMessage)
        {
            newMessage.Contact = db.Contacts.FirstOrDefault(contact => contact.PhoneNumber == newMessage.To);
            newMessage.Send();

            return RedirectToAction("Index");
        }
    }
}
