using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using Moq;
using Xunit.Abstractions;

namespace CarFactoryAPI.Tests.ServicesTests
{
    public class OwnerServiceTest : IDisposable
    {

        private readonly ITestOutputHelper testOutputHelper;

        OwnersService ownersService;

        Mock<ICarsRepository> carsRepo;
        Mock<ICashService> cashSer;
        Mock<IOwnersRepository> ownRepo;
        public OwnerServiceTest(ITestOutputHelper testOutputHelper)

        {
            this.testOutputHelper = testOutputHelper;
            testOutputHelper.WriteLine("test setup");
            carsRepo = new();
            cashSer = new();
            ownRepo = new();

            ownersService = new(carsRepo.Object, ownRepo.Object, cashSer.Object);
        }




        #region car null
        [Fact]

        public void BuyCar_CheckCarNotExist_not_exist()
        {


            BuyCarInput buyCarInput = new BuyCarInput()

            {
                CarId = 2
            };

            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("doesn't exist", actualResult);


        }
        #endregion

        #region Car Sold
        [Fact]
        public void BuyCar_CarSold_Sold()
        {
            Car car = new()
            {
                Id = 5,
                Owner = new()

            };

            // setup mocking methods
            carsRepo.Setup(o => o.GetCarById(It.IsAny<int>())).Returns(car);


            BuyCarInput buyCarInput = new BuyCarInput()
            {
                CarId = 5,
                OwnerId = 2,
                Amount = 1000
            };

            // Act 
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("sold", actualResult);

        }
        #endregion

        #region Owner Null
        [Fact]
        public void BuyCar_OwnerNull_NotExist()
        {
            Car car = new()
            {
                Id = 1,
            };

            carsRepo.Setup(e => e.GetCarById(It.IsAny<int>())).Returns(car);

            BuyCarInput buyCarInput = new()
            {
                CarId = 1,
                OwnerId = 2,
            };
            string Actual = ownersService.BuyCar(buyCarInput);

            Assert.Contains("doesn't exist", Actual);


        }
        #endregion

        #region Owner Have Car

        [Fact]
        public void BuyCar_OwnerHaveCar_AlreadyHave()

        {

            Car car = new()
            {
                Id = 1
            };
            Owner owner = new()
            {
                Id = 1,
                Car = new()

            };
            carsRepo.Setup(o => o.GetCarById(It.IsAny<int>())).Returns(car);
            ownRepo.Setup(o => o.GetOwnerById(It.IsAny<int>())).Returns(owner);

            BuyCarInput buyCarInput = new()
            {
                CarId = 1,
                OwnerId = 1,
            };


            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Already", actualResult);
        }
        #endregion

        #region Price More Than Input
        [Fact]
        public void BuyCar_ComparePriceWithInput_Insufficient()

        {

            Car car = new()
            {
                Id = 2,
                Price = 1000

            };
            Owner owner = new()
            {
                Id = 2,

            };
            carsRepo.Setup(o => o.GetCarById(It.IsAny<int>())).Returns(car);
            ownRepo.Setup(o => o.GetOwnerById(It.IsAny<int>())).Returns(owner);

            BuyCarInput buyCarInput = new()
            {
                OwnerId = 2,
                CarId = 2,
                Amount = 500,
            };


            // Act
            string actualResult = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Insufficient", actualResult);
        }
        #endregion




        #region IsSuccess
        [Fact]
        public void BuyCar_AssignToOwnerFails_something()
        {
            // Arrange
            var car = new Car { Id = 1 };
            var owner = new Owner { Id = 1 };

            carsRepo.Setup(x => x.GetCarById(It.IsAny<int>())).Returns(car);
            ownRepo.Setup(x => x.GetOwnerById(It.IsAny<int>())).Returns(owner);

            carsRepo.Setup(x => x.AssignToOwner(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(false);

            var input = new BuyCarInput
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 1000
            };

            // Act
            var result = ownersService.BuyCar(input);

            // Assert
            Assert.Contains("Something went wrong", result);
        }

        #endregion


        #region Payment 
        [Fact]
        public void BuyCar_Success_PrintMessage()
        {
            // Arrange
            Car car = new()
            {
                Id = 1,
                Price = 1000,
                Velocity = 120
            };

            Owner owner = new()
            {
                Id = 1,
                Name = "Ahmed",
                Car = null
            };

            carsRepo.Setup(o => o.GetCarById(It.IsAny<int>())).Returns(car);
            ownRepo.Setup(o => o.GetOwnerById(It.IsAny<int>())).Returns(owner);
            carsRepo.Setup(x => x.AssignToOwner(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            cashSer.Setup(e => e.Pay(It.IsAny<double>())).Returns($"Amount is {car.Price}");

            BuyCarInput buyCarInput = new()
            {
                CarId = 1,
                OwnerId = 1,
                Amount = 1000,
            };

            // Act
            string actual = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Successfull", actual);
            Assert.Contains("payment result", actual);
        }

        #endregion


        public void Dispose()
        {
            testOutputHelper.WriteLine("test clean up");
        }
    }
}