using Xunit;
using Moq;
using MicrowaveApp;
using System;
using MicorWave.controllers;
using MicorWave.Interfaces;

namespace MicrowaveApp.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void StartButtonPressed_WhenDoorOpen_DoesNotTurnOnHeater()
        {
            var hwMock = new Mock<IMicrowaveOvenHW>();
            hwMock.Setup(h => h.DoorOpen).Returns(true);

            var controller = new Controller(hwMock.Object);
            controller.Init();

            hwMock.Raise(h => h.StartButtonPressed += null, hwMock.Object, EventArgs.Empty);

            hwMock.Verify(h => h.TurnOnHeater(), Times.Never);
        }

        [Fact]
        public void StartButtonPressed_WhenDoorClosed_TurnsOnHeater()
        {
            // Arrange
            var hwMock = new Mock<IMicrowaveOvenHW>();
            hwMock.Setup(h => h.DoorOpen).Returns(false);

            var controller = new Controller(hwMock.Object);
            controller.Init();

            // Act: simulate start button press
            hwMock.Raise(h => h.StartButtonPressed += null, hwMock.Object, EventArgs.Empty);

            // Assert
            hwMock.Verify(h => h.TurnOnHeater(), Times.Once);
        }

        [Fact]
        public async Task StartButtonPressed_whenMicroWaveRunning_AddMinuteToRunTime()
        {
            // Arrange
            var hwMock = new Mock<IMicrowaveOvenHW>();
            hwMock.Setup(h => h.DoorOpen).Returns(false);

            var controller = new Controller(hwMock.Object);
            controller.Init();

            // Act: simulate start button press
            hwMock.Raise(h => h.StartButtonPressed += null, hwMock.Object, EventArgs.Empty);
            hwMock.Raise(h => h.StartButtonPressed += null, hwMock.Object, EventArgs.Empty);


            // Assert
            hwMock.Verify(h => h.TurnOnHeater(), Times.Once);

            Assert.Equal(120, controller.TimerValue);
            await Task.Delay(120 * 1000);

            hwMock.Verify(h => h.TurnOffHeater(), Times.Once);
        }

        [Fact]
        public void DoorOpenChanged_WhenOpenedAndRunning_TurnsOffHeater()
        {
            var hwMock = new Mock<IMicrowaveOvenHW>();
            var controller = new Controller(hwMock.Object);
            controller.Init();
            hwMock.Raise(h => h.StartButtonPressed += null, hwMock.Object, EventArgs.Empty);

            hwMock.Raise(h => h.DoorOpenChanged += null, true);

            hwMock.Verify(h => h.TurnOffHeater(), Times.Once);
        }

        [Fact]
        public void DoorOpenChanged_WhenOpenedAndNotRunning_DoNothing()
        {
            var hwMock = new Mock<IMicrowaveOvenHW>();
            var controller = new Controller(hwMock.Object);
            controller.Init();

            hwMock.Raise(h => h.DoorOpenChanged += null, true);

            hwMock.Verify(h => h.TurnOffHeater(), Times.Never);
        }
        
        [Fact]
        public void DoorOpenChanged_WhenOpened_LightIsOn()
        {
            var hwMock = new Mock<IMicrowaveOvenHW>();
            var controller = new Controller(hwMock.Object);
            controller.Init();

            hwMock.Raise(h => h.DoorOpenChanged += null, true);

            hwMock.Verify(h => h.TurnOnLicht(), Times.Once);
        }
        [Fact]
        public void DoorOpenChanged_WhenClosed_LightIsOff()
        {
            var hwMock = new Mock<IMicrowaveOvenHW>();
            var controller = new Controller(hwMock.Object);
            controller.Init();

            hwMock.Raise(h => h.DoorOpenChanged += null, false);

            hwMock.Verify(h => h.TurnOffLicht(), Times.Once);
        }
    }
}