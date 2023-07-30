using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Net;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Repositories;
using TicketManagementApplicationAPI.Controllers;
using Moq;

namespace TicketManagementApplicationUnitTest
{
    [TestClass]
    internal class EventControllerTest
    {
        Mock<IEventRepository> _eventRepositoryMock;
        Mock<IEventTypeRepository> _eventTypeMoq;
        Mock<IMapper> _mapperMoq;
        List<Event> _moqList;
        List<EventDto> _dtoMoq;
        
        [TestInitialize]
        public void SetupMoqData()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _eventTypeMoq = new Mock<IEventTypeRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Event>
            {
                new Event {EventId = 1,
                    EventName = "Moq name",
                    EventDescription = "Moq description",
                    EventType = new EventType {EventTypeId = 1,Name="test event type"},
                    EventTypeId = 1,
                    Venue = new Venue {VenueId = 1,Capacity = 12, Location = "Mock location",Type = "mock type"},
                    VenueId = 1
                }
            };
            _dtoMoq = new List<EventDto>
            {
                new EventDto
                {
                    EventDescription = "Moq description",
                    EventId = 1,
                    EventName = "Moq name",
                    EventType = new EventType {EventTypeId = 1,Name="test event type"},
                    Venue = new Venue {VenueId = 1,Capacity = 12, Location = "Mock location",Type = "mock type"}
                }
            };
        }
        [TestMethod]
        public async Task GetAllEventsReturnListOfEvents()
        {
            //Arrange

            IReadOnlyList<Event> moqEvents = _moqList;
            Task<IReadOnlyList<Event>> moqTask = Task.Run(() => moqEvents);
            _eventRepositoryMock.Setup(moq => moq.GetAll()).Returns(moqTask);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<EventDto>>(It.IsAny<IReadOnlyList<Event>>())).Returns(_dtoMoq);

            var controller = new EventController(_eventRepositoryMock.Object, _eventTypeMoq.Object, _mapperMoq.Object);

            //Act
            var events = await controller.GetEvents();
            var eventResult = events.Result as OkObjectResult;
            var eventCount = eventResult.Value as IList;

            //Assert

            Assert.AreEqual(_moqList.Count, eventCount.Count);
        }
    }
}
