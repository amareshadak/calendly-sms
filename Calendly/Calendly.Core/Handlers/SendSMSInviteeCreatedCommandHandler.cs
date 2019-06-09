using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Calendly.Core.Handlers
{
    public class SendSMSInviteeCreatedCommandHandler : IRequestHandler<InviteeCreatedCommand>
    {
        private readonly IConfiguration _config;
        private readonly ISmsClient _smsClient;
        private readonly ILogger<SendSMSInviteeCreatedCommandHandler> _logger;

        public SendSMSInviteeCreatedCommandHandler(ILogger<SendSMSInviteeCreatedCommandHandler> logger, IConfiguration config, ISmsClient smsClient)
        {
            _config = config;
            _smsClient = smsClient;
            _logger = logger;
        }

        public async Task<Unit> Handle(InviteeCreatedCommand request, CancellationToken cancellationToken)
        {
            //Build message and destination
            //Note, the "?." syntax protects against NullReferenceExceptions, but 
            //could result in a message with blank names if the request did truly
            //come in with an empty name
            var invitee = request?.RequestData?.payload?.invitee;
            var to = _config["PhoneNumber"];

            //We shouldn't get a message without first_name and last_name, but just in case
            string message;
            if (!String.IsNullOrEmpty(invitee?.first_name) && !String.IsNullOrEmpty(invitee?.last_name))
            {
                message = $"New meeting scheduled with {invitee?.first_name} {invitee?.last_name}!";
            }
            else
            {
                message = $"New meeting scheduled!  View your calendar for details.";
            }

            //Send
            _logger.LogInformation("Sending message");
            await _smsClient.Send(to, message);
            _logger.LogInformation("Message sent");

            return await Unit.Task;
        }
    }
}
