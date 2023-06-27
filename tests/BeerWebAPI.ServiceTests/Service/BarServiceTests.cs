using BeerWebAPI.DataAccess.Interface;
using Moq;
using Xunit;
using BeerWebAPI.ServiceTests.TestData;
using BeerWebAPI.Shared.Models;
using BeerWebAPI.DataAccess.DbModel;
using BeerWebAPI.Service.Services;

namespace BeerWebAPI.ServiceTests.Service
{
    /// <summary>
    /// Test class that contains test cases for Bar Service
    /// </summary>
    public class BarServiceTests
    {
        readonly Mock<IRepository<BarDBModel>> barRepository;
        public BarServiceTests()
        {
            barRepository = new Mock<IRepository<BarDBModel>>();
        }

        [Fact]
        public void AddBar_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BarModel barModel = new() { Name = "Binge Bar", Id = 1, Address = "New Delhi" };
            barRepository.Setup(x => x.Add(It.IsAny<BarDBModel>())).Returns(true);
            var barService = new BarService(barRepository.Object);

            //Act
            var response = barService.CreateBar(barModel);

            //Assert
            Assert.True(response);
            barRepository.Verify(repo => repo.Add(It.IsAny<BarDBModel>()), Times.Once());
        }

        [Fact]
        public void GetAllBarDetails_ShouldReturnSuccess_ForValidRequest()
        {
            //Arrange
            var bars = BarServiceTestData.GetBarList();
            barRepository.Setup(x => x.GetAll()).Returns(bars);
            var barService = new BarService(barRepository.Object);

            //Act
            List<BarModel>? listBar = barService.GetAllBars();

            //Assert
            Assert.NotNull(listBar);
            Assert.Single(listBar);
            Assert.Equal(bars[0].Name, listBar[0].Name);
            barRepository.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Fact]
        public void GetBarDetails_ShouldReturnSuccess_ForValidBarId()
        {
            //Arrange
            BarDBModel barDBModel = BarServiceTestData.GetBarListById(1);
            barRepository.Setup(x => x.GetById(1)).Returns(barDBModel);
            var barService = new BarService(barRepository.Object);

            //Act
            BarModel? bar = barService.GetBarDetailsById(1);

            //Assert
            Assert.Equal(barDBModel.Name, bar.Name);
            Assert.Equal(barDBModel.Address, bar.Address);
            Assert.Equal(barDBModel.Id, bar.Id);
            barRepository.Verify(repo => repo.GetById(1), Times.Once());
        }

        [Fact]
        public void GetBarDetailsById_ReturnsNull_ForInvalidBarId()
        {
            //Arrange
            barRepository.Setup(x => x.GetById(3)).Returns(BarServiceTestData.GetBarListById(3));
            var barService = new BarService(barRepository.Object);

            //Act
            BarModel bar = barService.GetBarDetailsById(3);

            //Assert
            Assert.Null(bar);
            barRepository.Verify(repo => repo.GetById(3), Times.Once());
        }

        [Fact]
        public void UpdateBar_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            BarModel barModel = new() { Id = 1, Name = "Seven Star Bar", Address = "New Delhi" };
            barRepository.Setup(x => x.GetById(1)).Returns(BarServiceTestData.GetBarListById(1));
            barRepository.Setup(x => x.Update(It.IsAny<BarDBModel>())).Returns(true);
            var barService = new BarService(barRepository.Object);

            //Act
            bool response = barService.ModifyBar(barModel, 1);

            //Assert
            Assert.True(response);
            barRepository.Verify(repo => repo.GetById(1), Times.Once());
            barRepository.Verify(repo => repo.Update(It.IsAny<BarDBModel>()), Times.Once());
        }

        [Fact]
        public void UpdateBar_ReturnNotSuccess_ForInvalidBarId()
        {
            //Arrange
            BarModel barModel = new() { Name = "BlueBar", Address = "New Delhi" };
            var barService = new BarService(barRepository.Object);
            barRepository.Setup(x => x.GetById(3)).Returns(BarServiceTestData.GetBarListById(3));

            //Act
            bool response = barService.ModifyBar(barModel, 3);

            //Assert
            Assert.False(response);
            barRepository.Verify(repo => repo.GetById(3), Times.Once());
        }
    }
}