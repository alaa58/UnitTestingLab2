using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary.Test
{
    public class CarFactoryTests
    {
        [Fact]
        public void NewCar_AskForToyota_ObjectOfToyota()
        {
            // Arrange
            BMW bMW = new BMW();

            // Act
            Car? myCar = CarFactory.NewCar(CarTypes.Toyota);

            // Assert
            Assert.NotNull(myCar);
            Assert.IsType<Toyota>(myCar);
            Assert.IsNotType<BMW>(myCar);

            Assert.IsAssignableFrom<Car>(bMW);
        }

        [Fact]
        public void NewCar_AskforHonda_Exception()
        {
            // Arrange



            // Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                // Act
                Car? car = CarFactory.NewCar(CarTypes.Honda);
            });

        }
    }
}
