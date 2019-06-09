using Calendly.Core.Models;
using MediatR;
using System;

namespace Calendly.Core
{
    /// <summary>
    /// Command fired when an InviteeCreated request is received.
    /// </summary>
    public class InviteeCreatedCommand : IRequest
    {
        public WebhookRequest RequestData { get; }

        public InviteeCreatedCommand(WebhookRequest requestData)
        {
            this.RequestData = requestData;
        }
    }
}
