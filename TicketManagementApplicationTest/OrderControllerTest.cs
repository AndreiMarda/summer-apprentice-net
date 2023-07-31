using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicketManagementApplicationAPI.Controllers;
using TicketManagementApplicationAPI.Exceptions;
using TicketManagementApplicationAPI.Model.Dto;
using TicketManagementApplicationAPI.Model;
using TicketManagementApplicationAPI.Repositories;

namespace TicketManagerSystem.UnitTests
{
    [TestClass]
    public class OrderControllerTest
    {
        Mock<IOrderRepository> _orderRepositoryMock;
        Mock<IMapper> _mapperMoq;
        List<Order> _moqList;
        List<OrderDto> _dtoMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Order>
            {
                new Order {
                    OrderId = 1,
                    CustomerId = 1,
                    TicketCategoryId = 1,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 2,
                    TicketCategory = new TicketCategory{TicketCategoryId = 1,Description = "test event type", Price = 100, EventId = 1},
                    Customer = new Customer {CustomerName = "Gicu",CustomerId = 1, Email = "Mock email"},
                    TotalPrice = 1
                }
            };
            _dtoMoq = new List<OrderDto>
            {
                new OrderDto {
                    OrderId = 2,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 3,
                    TotalPrice = 3
                }
            };
        }

        [TestMethod]
        public async Task GetAllOrdersReturnListOfOrders()
        {
            //Arrange

            IReadOnlyList<Order> moqOrders = _moqList;
            Task<IReadOnlyList<Order>> moqTask = Task.Run(() => moqOrders);
            _orderRepositoryMock.Setup(moq => moq.GetAll()).Returns(_moqList);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDto>>(It.IsAny<Order>())).Returns(_dtoMoq);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);

            //Act

            var orders = controller.GetAll();
            var orderResult = orders.Result as OkObjectResult;
            var orderDtoList = orderResult.Value as IEnumerable<OrderDto>;
            //Assert
            Assert.AreEqual(_moqList.Count, orderDtoList.Count());
        }

        //[TestMethod]
        //public async Task GetOrderByIdReturnNotFoundWhenNoRecordFound()
        //{
        //    //Arrange
        //    int orderToFind = 0;
        //    _orderRepositoryMock.Setup(moq => moq.GetById(orderToFind)).ThrowsAsync(new EntityNotFoundException(orderToFind, nameof(Order)));
        //    //_mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDTO>>(It.IsAny<IReadOnlyList<Order>>())).Returns((IEnumerable<OrderDTO>)null);
        //    var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);
        //    //Act
        //    var result = await controller.GetById(orderToFind);
        //    var orderResult = result.Result as NotFoundObjectResult;

        //    //Assert

        //    Assert.IsTrue(orderResult.StatusCode == 404);
        //}

        [TestMethod]
        public async Task GetOrderByIdReturnFirstRecord()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<OrderDto>(It.IsAny<Order>())).Returns(_dtoMoq.First());
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);
            //Act

            var result = await controller.GetById(1);
            var orderResult = result.Result as OkObjectResult;
            var orderCount = orderResult.Value as OrderDto;

            //Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(2, orderCount.OrderId);
        }

        /*[TestMethod]
         public async Task GetEventByIDThrowsAnException()
         {
             //Arrange
             _eventRepositoryMock.Setup(moq => moq.GetById(It.IsAny<int>())).Throws<Exception>();
             _mapperMoq.Setup(moq => moq.Map<EventDTO>(It.IsAny<Event>())).Returns(_dtoMoq.First());
             var controller = new EventController(_eventRepositoryMock.Object, _mapperMoq.Object);
             //Act



             var result = await controller.GetById(1);



             //Assert

             Assert.IsNull(result);
         }*/
    }
}