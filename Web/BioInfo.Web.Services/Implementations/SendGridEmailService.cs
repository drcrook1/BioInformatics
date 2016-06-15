using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;

namespace BioInfo.Web.Services.Implementations
{
    public class SendGridEmailService : IIdentityMessageService
    {
        // interface IIdentityMessageService with
        //member this.SendAsync(identityMessage) =
        //     let sendGridMessage = new SendGridMessage()
        //     sendGridMessage.From<- new MailAddress(fromAddress)
        //     let recipients = new List<string>()
        //     recipients.Add(identityMessage.Destination)
        //     sendGridMessage.AddTo(recipients)
        //     sendGridMessage.Subject<- identityMessage.Subject
        //     sendGridMessage.Html<- identityMessage.Body
        //     sendGridMessage.Text<- identityMessage.Body

        //     let credentials = new NetworkCredential(account, password)
        //     let transportWeb = new Web(credentials)
        //     match transportWeb with
        //         | null -> Task.FromResult(0):> Task
        //         | _ -> transportWeb.DeliverAsync(sendGridMessage) 
        //public Task SendAsync(IdentityMessage message)
        //{
        //    new SendGridMessage()
        //}
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
