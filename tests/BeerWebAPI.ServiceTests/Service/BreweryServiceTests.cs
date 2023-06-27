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
    /// Test class that contains test cases for Brewery Service
    /// </summary>
    public class BreweryServiceTests
    {
        readonly Mock<IRepository<BreweryDBModel>> breweryRepository;
        public BreweryServiceTests()
        {
            breweryRepository = new Mock<IRepository<BreweryDBModel>>();
        }

        [Fact]
        public void AddBrewery_ReturSuccess_ForValidReqeust()
        {
            //Arrange
            BreweryModel breweryModel = new() { Name = "United Beer Company", Id = 1 };
            breweryRepository.Setup(x => x.Add(It.IsAny<BreweryDBModel>())).Returns(true);
            var breweryService = new BreweryService(breweryRepository.Object);
            //Act
            var response = breweryService.CreateBrewery(breweryModel);

            //Assert
            Assert.True(response);
            breweryRepository.Verify(repo => repo.Add(It.IsAny<BreweryDBModel>()), Times.Once());
        }

        [Fact]
        public void UpdateBrewery_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            breweryRepository.Setup(x => x.GetById(1)).Returns(BreweryServiceTestData.GetBreweryListById(1));
            breweryRepository.Setup(x => x.Update(It.IsAny<BreweryDBModel>())).Returns(true);
            var breweryService = new BreweryService(breweryRepository.Object);

            //Act
            bool response = breweryService.ModifyBrewery(new BreweryModel { Id = 1, Name = "Test Brewery" }, 1);

            //Assert
            Assert.True(response);
            breweryRepository.Verify(repo => repo.GetById(1), Times.Once());
            breweryRepository.Verify(repo => repo.Update(It.IsAny<BreweryDBModel>()), Times.Once());
        }

        [Fact]
        public void UpdateBrewery_ReturnNotSuccess_ForInvalidBreweryId()
        {
            //Arrange
            breweryRepository.Setup(x => x.GetById(3)).Returns(BreweryServiceTestData.GetBreweryListById(3));
            var breweryService = new BreweryService(breweryRepository.Object);

            //Act
            bool response = breweryService.ModifyBrewery(new BreweryModel { Id = 1, Name = "Test Brewery" }, 3);

            //Assert
            Assert.False(response);
            breweryRepository.Verify(repo => repo.GetById(3), Times.Once());
        }

        [Fact]
        public void GetAllBreweryDetails_ReturnSuccess_ForValidRequest()
        {
            //Arrange
            List<BreweryDBModel> listBreweryModel = BreweryServiceTestData.GetBreweryList();
            breweryRepository.Setup(x => x.GetAll()).Returns(listBreweryModel);
            var breweryService = new BreweryService(breweryRepository.Object);

            //Act
            List<BreweryModel>? listBrewery = breweryService.GetAllBreweries();

            //Assert
            Assert.Equal(listBrewery[0].Name, listBreweryModel[0].Name);
            Assert.NotNull(listBrewery);
            breweryRepository.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Fact]
        public void GetBreweryDetailsById_ReturnSuccess_ForValidBreweryId()
        {
            //Arrange
            var breweryDBModel = BreweryServiceTestData.GetBreweryListById(1);
            breweryRepository.Setup(x => x.GetById(1)).Returns(breweryDBModel);
            var breweryService = new BreweryService(breweryRepository.Object);


            //Act
            BreweryModel? brewery = breweryService.GetBreweryDetailsById(1);

            //Assert
            Assert.Equal(breweryDBModel.Name, brewery.Name);
            Assert.NotNull(brewery);
            breweryRepository.Verify(repo => repo.GetById(1), Times.Once());
        }

        [Fact]
        public void GetBreweryDetailsById_ReturnsNull_ForInvalidBreweryId()
        {
            //Arrange
            breweryRepository.Setup(x => x.GetById(3)).Returns(BreweryServiceTestData.GetBreweryListById(3));
            var breweryService = new BreweryService(breweryRepository.Object);

            //Act
            BreweryModel? brewery = breweryService.GetBreweryDetailsById(3);

            //Assert
            Assert.Null(brewery);
            breweryRepository.Verify(repo => repo.GetById(3), Times.Once());
        }
    }
}