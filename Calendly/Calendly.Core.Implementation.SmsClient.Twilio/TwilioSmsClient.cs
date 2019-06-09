using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.Extensions.Logging;
using Twilio;

namespace Calendly.Core.Implementation.SmsClient
{
    public class TwilioSmsClient : ISmsClient
    {
        private readonly IConfiguration _config;
        private readonly ILogger<TwilioSmsClient> _logger;

        public TwilioSmsClient(IConfiguration config, ILogger<TwilioSmsClient> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task Send(string to, string message)
        {
            var accountSid = _config["Twilio:AccountSid"];
            var authToken = _config["Twilio:AuthToken"];
            TwilioClient.Init(accountSid, authToken);

            //Send message
            var from = _config["Twilio:FromPhoneNo"];
            var result = await MessageResource.CreateAsync(
                    to: new PhoneNumber(to),
                    from: new PhoneNumber(from),
                    body: message
                );
            
            _logger.LogDebug($"To: {to}, From: {from}, Message: {message}, Status: {result.Status}");
        }
    }
}
