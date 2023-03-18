using AutoFixture;
using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask;
using GlobalTask.TaskManagement.Application.Models.Notifications;
using Microsoft.Extensions.Logging;
using Moq;

namespace GlobalTask.TaskManagement.UnitTest.Application.Features.Tasks.Commands
{
    public class GetTaskUnitTest
    {
       readonly Mock<ITaskRepository> _taskRepository;
       readonly Mock<ILogger<GetTaskHandler>> _logger;
       readonly Mock<INotificationsHandler> _notificationsHandler;
       readonly Fixture _fixture;
       readonly GetTaskHandler _handler;

        public GetTaskUnitTest()
        {
            _taskRepository = new Mock<ITaskRepository>();
            _logger = new Mock<ILogger<GetTaskHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new GetTaskHandler(_taskRepository.Object, _logger.Object, _notificationsHandler.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Geted_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskQuery = _fixture.Create<GetTaskQuery>();
            var tasks = _fixture.CreateMany<TaskItem>();
            _taskRepository.Setup(x => x.GetAsync()).ReturnsAsync(tasks);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.NotNull(response);
            _taskRepository.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Geted_When_An_Exception_Was_Thrown()
        {
            var taskQuery = _fixture.Create<GetTaskQuery>();
            _taskRepository.Setup(x => x.GetAsync()).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskQuery, CancellationToken.None);

            Assert.Empty(response);
            _taskRepository.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}