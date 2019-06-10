using Calendly.Controllers;
using Calendly.Core;
using Calendly.Core.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendly.Tests
{
    /// <summary>
    /// Normally I would cover controllers via Integration tests, but since I'm not doing that
    /// I'll add some unit tests here for some level of validation
    /// </summary>
    [TestClass]
    public class InviteesControllerTests
    {
        private InviteesController _testSubject;
        private IMediator _mediator;

        [TestInitialize]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();
            _testSubject = new InviteesController(_mediator);
        }

        [TestMethod]
        public async Task Created_Returns_BadRequest_For_Null_Parameter()
        {
            //Arrange
            WebhookRequest request = null;

            //Act
            var result = await _testSubject.Created(request);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task Created_Sends_Mediator_Command_When_Request_Is_Valid_And_Returns_200()
        {
            //Arrange
            WebhookRequest request = new WebhookRequest();

            //Act
            var result = await _testSubject.Created(request);

            //Assert
            await _mediator.Received().Send(Arg.Is<InviteeCreatedCommand>(x => x.RequestData == request));
            result.Should().BeOfType<OkResult>();            
        }       
    }
}
