using Calendly.Core.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using System.Threading;
using System.Threading.Tasks;

namespace Calendly.Core.Tests
{
    [TestClass]
    public class SendSmsInviteeCreatedCommandHandlerTests
    {
        private SendSMSInviteeCreatedCommandHandler _testSubject;
        private IConfiguration _config;
        private ISmsClient _smsClient;
        private ILogger<SendSMSInviteeCreatedCommandHandler> _logger;


        [TestInitialize]
        public void Setup()
        {
            _logger = new NullLogger<SendSMSInviteeCreatedCommandHandler>();
            _config = Substitute.For<IConfiguration>();
            _smsClient = Substitute.For<ISmsClient>();

            _testSubject = new SendSMSInviteeCreatedCommandHandler(_logger, _config, _smsClient);
        }

        [TestMethod]
        public async Task Handle_builds_generic_message_when_firstName_is_null()
        {
            //Arrange
            var request = this.CreateCommandWithInviteeName(firstName: null, lastName: "last");
            var message = "New meeting scheduled!  View your calendar for details.";
            var to = "8675309";
            _config["PhoneNumber"].Returns(to);

            //Act
            await _testSubject.Handle(request, CancellationToken.None);

            //Assert
            await _smsClient.Received().Send(to, message);
        }

        [TestMethod]
        public async Task Handle_builds_generic_message_when_lastName_is_null()
        {
            //Arrange
            var request = this.CreateCommandWithInviteeName(firstName: "first", lastName: null);
            var message = "New meeting scheduled!  View your calendar for details.";
            var to = "8675309";
            _config["PhoneNumber"].Returns(to);

            //Act
            await _testSubject.Handle(request, CancellationToken.None);

            //Assert
            await _smsClient.Received().Send(to, message);
        }

        [TestMethod]
        public async Task Handle_builds_message_with_attendee_name_when_name_is_present()
        {
            //Arrange
            var request = this.CreateCommandWithInviteeName(firstName: "John", lastName: "Smith");
            var message = "New meeting scheduled with John Smith!";
            var to = "8675309";
            _config["PhoneNumber"].Returns(to);

            //Act
            await _testSubject.Handle(request, CancellationToken.None);

            //Assert
            await _smsClient.Received().Send(to, message);
        }

        private InviteeCreatedCommand CreateCommandWithInviteeName(string firstName, string lastName)
        {
            var request = new Models.WebhookRequest()
            {
                payload = new Models.Payload
                {
                    invitee = new Models.Invitee
                    {
                        first_name = firstName,
                        last_name = lastName
                    }
                }
            };

            return new InviteeCreatedCommand(request);
        }
    }
}
