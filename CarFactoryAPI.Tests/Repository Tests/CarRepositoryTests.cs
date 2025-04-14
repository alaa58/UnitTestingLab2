using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryAPI.Tests.Repository_Tests
{
    public class CarRepositoryTests
    {
        Mock<FactoryContext> factoryContextMock;
        CarRepository carRepository;
        public CarRepositoryTests()
        {
            // setup
            factoryContextMock = new Mock<FactoryContext>();

            carRepository = new(factoryContextMock.Object);
        }

        [Fact]
        public void GetCarById_AskForCarId5_FindCar()
        {
            // Arrange
            // prepare mocking data
            List<Car> cars = new()
            {
                new Car() {Id = 1, VIN= "32332", Type=CarType.Audi},
                new Car() {Id = 2, VIN= "32337", Type=CarType.Audi},
                new Car() {Id = 5, VIN= "32338", Type=CarType.Audi}
            };

            // setup mocking dbset
            factoryContextMock.Setup(o=>o.Cars).ReturnsDbSet(cars);

            // Act
            Car actualResult = carRepository.GetCarById(5);

            // Asssert
            Assert.NotNull(actualResult);
            Assert.Equal("32338", actualResult.VIN);
        }
    }
}
