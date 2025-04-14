using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using CarFactoryAPI.Tests.Stups;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CarFactoryAPI.Tests.ServicesTests
{
    public class OwnerServiceTests : IDisposable
    {
        private readonly ITestOutputHelper testOutputHelper;

        // Create Mocking of Dependecies
        Mock<ICarsRepository> carRepoMock;
        Mock<IOwnersRepository> ownerRepoMock;
        Mock<ICashService> cashRepoMock;

        // use mocking as fake dependencies
        OwnersService ownersService;

        public OwnerServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            testOutputHelper.WriteLine("test setup");

            // Create Mocking of Dependecies
            carRepoMock = new();
            ownerRepoMock = new();
            cashRepoMock = new();

            // use mocking as fake dependencies
            ownersService = new(carRepoMock.Object, ownerRepoMock.Object, cashRepoMock.Object);

        }
        [Fact]
        [Trait("Author", "Omar")]

        public void BuyCar_AskForNotExisitingCar_NotExist()
        {
            // Arrange
            FactoryContext factoryContext = new();

            CarRepository carRepo = new(factoryContext);
            OwnerRepository ownerRepo = new(factoryContext);
            CashService cashService = new();

            OwnersService ownersService = new(carRepo,ownerRepo,cashService);
            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId = 10,
                OwnerId = 10,
                Amount = 1000
            };

            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("doesn't exist", actualResult);
        }

        [Fact]
        [Trait("Author", "Ahmed")]

        public void BuyCar_AskForSoldCar_AlreadySold()
        {
            // Arrange
            OwnersService ownersService = new(
                new CarRepoStup(),
                new OwnerRepoStup(),
                new CashServiceStup());
            BuyCarInput buyCarInput = new()
            {
                CarId = 5,
                OwnerId = 6,
                Amount = 1000
            };

            // Act 
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("sold", actualResult);
        }

        [Fact]
        [Trait("Author","Ali")]
        public void BuyCar_AskWithPreviousOwner_HaveCar()
        {
            testOutputHelper.WriteLine("BuyCar_AskWithPreviousOwner_HaveCar");
            // Arrange

            // prepare Mocking data
            Car car = new()
            {
                Id = 5,
                VIN = "q3123",
                Price = 1200,
                Type = CarType.BMW,
                Velocity = 50
            };
            Owner owner = new()
            {
                Id = 2,
                Name = "Ali",
                Car = new Car()
            };

            // setup mocking methods
            carRepoMock.Setup(o=>o.GetCarById(It.IsAny<int>())).Returns(car);
            ownerRepoMock.Setup(o => o.GetOwnerById(It.IsAny<int>())).Returns(owner);

            
            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId = 5,
                OwnerId = 2,
                Amount = 1000
            };

            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("have car", actualResult);
        }

        [Fact]
        [Trait("Author", "Ali")]

        public void BuyCar_AskForCarWithInsufficientFund_InsufficientFund()
        {
            testOutputHelper.WriteLine("BuyCar_AskForCarWithInsufficientFund_InsufficientFund");
            // Arrange
            

            // Prepare Mocking Data
            Car car = new Car()
            {
                Id = 5,
                VIN = "fjkeh7998",
                Price = 5000,
                Type = CarType.BMW,
                Velocity = 500
            };
            Owner owner = new Owner()
            {
                Id = 4,
                Name = "Omar"
            };

            // Setup Mocking Methods
            carRepoMock.Setup(o=>o.GetCarById(It.IsAny<int>())).Returns(car);
            ownerRepoMock.Setup(o=>o.GetOwnerById(It.IsAny<int>())).Returns(owner);

            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId = 5,
                OwnerId = 4,
                Amount = 4000
            };

            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Insufficient", actualResult);
        }

        public void Dispose()
        {
            testOutputHelper.WriteLine("test clean up");
        }
    }
}
