using Application.SMSSetUp;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Application.Services
{
    public class SMSService : IIdentityMessageService
    {

        public Task SendAsync(IdentityMessage message)
        {

            var accountSid = "ACe78bb6bfa64a631be81c5fb2e98fa549";
            var authToken = "7e9e6915e48f626990ad859a60a32189";
            var fromNumber = "+989926364401";

            TwilioClient.Init(accountSid, authToken);

            MessageResource result = MessageResource.Create(
            new PhoneNumber(message.Destination),
            from: new PhoneNumber(fromNumber),
            body: message.Body
            );

            //Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.TraceInformation(result.Status.ToString());
            //Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
        }

        
    }
}
