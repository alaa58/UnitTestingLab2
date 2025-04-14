using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactoryLibrary.Test
{
    // Testing Class
    // must be puplic
    // doesn't use any attributes
    public class ToyotaTests
    {

        // Testing Methods
        // must be pulic
        // return void 
        // using naming convential => [MethodName]_[TestCase]_[Expected output]
        // use [Fact] ot [Theory] attributes

        // [Fact] => used for test method with no input parameters
        // [Theory] => parametrized test - test method with input parameters

        [Fact]
        public void IsStopped_CarVelocity20_False()
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = 20, drivingMode = DrivingMode.Forward};

            // Act
            bool actualOutput = toyota.IsStopped();

            // Boolean Assert
            Assert.False(actualOutput);
        }

        [Fact]
        public void GetDirection_DiectionForward_Forward()
        {
            // Arrange
            Toyota toyota = new() { velocity = 10, drivingMode = DrivingMode.Forward };

            // Act
            string actualResult = toyota.GetDirection();

            // string Assert
            // Assert.Equal("Forward", actualResult);
            // Assert.Equal("forward", actualResult, ignoreCase: true);

            Assert.StartsWith("Fo", actualResult);
            Assert.StartsWith("fo", actualResult, StringComparison.OrdinalIgnoreCase);
            Assert.EndsWith("rd", actualResult);
            Assert.Contains("wa", actualResult);

            Assert.Matches("F[a-z]{5}d", actualResult);
            //Assert.DoesNotMatch()
        }

        [Fact]
        public void TimeToCoverDistance_Velocity20Distance40_Time2()
        {
            // Arrange
            Toyota toyota = new Toyota() {velocity= 20, drivingMode = DrivingMode.Backward };

            // Act
            double acutalOutput = toyota.TimeToCoverDistance(40);

            // Numeric Assert
            Assert.Equal(2, acutalOutput);

            Assert.InRange(acutalOutput, 1, 3);

            Assert.True(acutalOutput >= 2);
        }

        [Theory]
        [InlineData(20,20,1)]
        [InlineData(5,20,5)]
        [InlineData(5,10,2)]
        public void TimeToCoverDistance_UseInputs_checkOutput(double velocity, double distance, double time)
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = velocity, drivingMode = DrivingMode.Forward };


            // Act
            double actualTime = toyota.TimeToCoverDistance(distance);

            // Assert
            Assert.Equal(time,actualTime);
        }

        [Fact]
        public void GetMyCar_AskForCopy_SameRefrence()
        {
            // Arrange
            Toyota toyota = new Toyota() { velocity = 50, drivingMode = DrivingMode.Forward};

            // Act
            Car newRef = toyota.GetMyCar();

            // Assert
            Assert.NotNull(newRef);

            Assert.NotSame(toyota, newRef);

            Assert.Equal<Car>(toyota, newRef);  // value Equality
        }
    }
    
}
