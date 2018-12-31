using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Chat.Controllers
{
    public class ChatController : TwilioController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendSms()
        {
            var accountSid = ConfigurationManager.AppSettings["AccountSid"];
            var authToken = ConfigurationManager.AppSettings["AuthToken"];

            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(ConfigurationManager.AppSettings["MyPhoneNumber"]);
            var from = new PhoneNumber(ConfigurationManager.AppSettings["TwilioNumber"]);

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Pun for the day: I can't believe I got fired from the calendar factory. All I did was take a day off."
                );
            return Content(message.Sid);
        }

        public ActionResult ReceiveSms()
        {
            var response = new MessagingResponse();
            response.Message("The future, the present, and the past walked into a bar. Things got a little tense.");

            return TwiML(response);
        }
    }
}