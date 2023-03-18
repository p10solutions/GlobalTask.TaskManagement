using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Contracts.Repositories;
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Application.Models.Notifications;

namespace GlobalTask.TaskManagement.UnitTest.Application.Features.Tasks.Commands
{
    public class CreateTaskUnitTest
    {
       readonly Mock<ITaskRepository> _taskRepository;
       readonly Mock<ILogger<CreateTaskHandler>> _logger;
       readonly Mock<INotificationsHandler> _notificationsHandler;
       readonly Fixture _fixture;
       readonly CreateTaskHandler _handler;

        public CreateTaskUnitTest()
        {
            _taskRepository = new Mock<ITaskRepository>();
            _logger = new Mock<ILogger<CreateTaskHandler>>();
            _notificationsHandler = new Mock<INotificationsHandler>();
            _fixture = new Fixture();
            _handler = new CreateTaskHandler(_taskRepository.Object, _logger.Object, _notificationsHandler.Object);
        }

        [Fact]
        public async Task Task_Should_Be_Created_Successfully_When_All_Information_Has_Been_Submitted()
        {
            var taskCommand = _fixture.Create<CreateTaskCommand>();

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.False(Guid.Empty == response);
            _taskRepository.Verify(x => x.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task Task_Should_Not_Be_Created_When_An_Exception_Was_Thrown()
        {
            var taskCommand = new CreateTaskCommand(string.Empty, DateTime.Now, EStatus.InProgress);
            _taskRepository.Setup(x => x.AddAsync(It.IsAny<TaskItem>())).Throws(new Exception());
            _notificationsHandler
                .Setup(x => x.AddNotification(It.IsAny<string>(), It.IsAny<ENotificationType>(), It.IsAny<object>()))
                    .Returns(_notificationsHandler.Object);
            _notificationsHandler.Setup(x => x.ReturnDefault<Guid>()).Returns(Guid.Empty);

            var response = await _handler.Handle(taskCommand, CancellationToken.None);

            Assert.True(Guid.Empty == response);
            _taskRepository.Verify(x => x.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }
    }
}