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
        private readonly IOptions<SMSSetUp.SMSSetUp> config;

        public Task SendAsync(IdentityMessage message)
        {

            var accountSid = config.Value.SMSAccountIdentification;
            var authToken = config.Value.SMSAccountPassword;
            var fromNumber = config.Value.SMSAccountFrom;

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
