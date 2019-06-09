using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendly.Core;
using Calendly.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calendly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InviteesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Just a ping to see if the service is reachable
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.UtcNow);
        }

        // POST api/invitee/created
        [HttpPost("created")]
        public async Task<IActionResult> Created([FromBody] WebhookRequest request)
        {
            /*Just validate that the request isn't empty.  Since it's a webhook from a third party,
             * I'm not adding any additional validation on individual properties
            */
            if(request == null)
            {
                return BadRequest();
            }

            /*Just executing the command and returning 200.  Again, because this is a webhook
             * from a third party, I'm assuming they don't care about more granular response codes
            */
            var command = new InviteeCreatedCommand(request);
            await _mediator.Send(command);

            return Ok();
        }
    }
}
