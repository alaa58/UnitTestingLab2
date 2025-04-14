using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary.Test
{
    public class CarStoreTests
    {
        [Fact]
        public void AddCar_addToyota_listContainsObject()
        {
            // Arrange
            CarStore carStore = new CarStore();
            Toyota toyota = new Toyota() {velocity = 10, drivingMode = DrivingMode.Forward };
            BMW bmw = new BMW() {velocity = 10, drivingMode = DrivingMode.Forward };

            Assert.Empty(carStore.cars);
            // Act
            carStore.AddCar(toyota);

            // collection Assert
            Assert.NotEmpty(carStore.cars);
            Assert.Equal(1,carStore.cars.Count);

            Assert.Contains<Car>(toyota, carStore.cars); // value Equality
            Assert.Contains<Car>(bmw, carStore.cars); // value Equality

           // search lab: Assert.Collection();
        }
    }
}
