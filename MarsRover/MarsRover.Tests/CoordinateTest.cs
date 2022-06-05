using NUnit.Framework;
using FluentAssertions;
using MarsRover.Models;
using System;
using MarsRover.Models.Interfaces;
using MarsRover.Models.Plateaus;
using System.Collections.Generic;
using MarsRover.Models.enums;

namespace MarsRover.Tests
{
    public class CoordinateTest
    {
        private Coordinates coordinate;

        [SetUp]
        public void Setup()
        {
            
        }

        #region IsSamePosition

        [Test]
        public void IsSamePosition_GivenSameCoordinatesWithSameOrientation_ShouldReturnTrue()
        {
            coordinate = new Coordinates(3, 5, CardinalPoint.S);
            Coordinates c2 = new Coordinates(3, 5, CardinalPoint.S);

            coordinate.IsSamePosition(c2).Should().BeTrue();
        }

        [Test]
        public void IsSamePosition_GivenSameCoordinatesWithDifferentOrientation_ShouldReturnTrue()
        {
            coordinate = new Coordinates(3, 5, CardinalPoint.S);
            Coordinates c2 = new Coordinates(3, 5, CardinalPoint.E);

            coordinate.IsSamePosition(c2).Should().BeTrue();
        }

        [Test]
        public void IsSamePosition_GivenDifferentCoordinatesWithSameOrientation_ShouldReturnFalse()
        {
            coordinate = new Coordinates(5, 5, CardinalPoint.S);
            Coordinates c2 = new Coordinates(3, 5, CardinalPoint.S);

            coordinate.IsSamePosition(c2).Should().BeFalse();
        }

        [Test]
        public void IsSamePosition_GivenDifferentCoordinatesWithDifferentOrientation_ShouldReturnFalse()
        {
            coordinate = new Coordinates(5, 3, CardinalPoint.S);
            Coordinates c2 = new Coordinates(3, 5, CardinalPoint.E);

            coordinate.IsSamePosition(c2).Should().BeFalse();
        }

        #endregion

        #region GetCoordinatesFromString

        [Test]
        public void GivenValidCoordinatesAndOrientation_ShouldReturnCoordinates()
        {
            //Arrange
            string input = "8 2 E";
            var expectedResult = new Coordinates(8,2, CardinalPoint.E);

            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));

            //Assert
            Assert.AreEqual(expectedResult.X, actualResult.X);
            Assert.AreEqual(expectedResult.Y, actualResult.Y);
            Assert.AreEqual(expectedResult.Orientation, actualResult.Orientation);

        }

        [Test]
        public void GivenNullInput_ShouldReturnNull()
        {
            //Arrange
            string input = null;

            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));

            //Assert
            actualResult.Should().BeNull();

        }

        [Test]
        public void GivenNegativeCoordinatesForRectangularPlateau_ShouldReturnNull()
        {
            //Arrange
            string input = "-3 4 N";

            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));

            //Assert
            actualResult.Should().BeNull();

        }

        [Test]
        public void GivenNegativeCoordinatesForCircularPlateau_ShouldReturnCoordinates()
        {
            //Arrange
            string input = "-3 4 N";
            var expectedResult = new Coordinates(-3, 4, CardinalPoint.N);

            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(CircularPlateau));

            //Assert
            Assert.AreEqual(expectedResult.X, actualResult.X);
            Assert.AreEqual(expectedResult.Y, actualResult.Y);
            Assert.AreEqual(expectedResult.Orientation, actualResult.Orientation);

        }

        [Test]
        public void GivenInvalidCardinalPoint_ShouldReturnNull()
        {
            //Arrange
            string input = "6 4 D";

            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));

            //Assert
            actualResult.Should().BeNull();

        }

        [Test]
        public void GivenCoordinatesInInvalidFormat_ShouldReturnNull()
        {
            //Arrange
            string input = "64N";
            //Act
            var actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

            //Arrange
            input = "34 S";
            //Act
            actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

            //Arrange
            input = "(3,4) S";
            //Act
            actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

            //Arrange
            input = "(5,5 S)";
            //Act
            actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

            //Arrange
            input = "(5 8 W)";
            //Act
            actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

            //Arrange
            input = "5 2";
            //Act
            actualResult = Coordinates.GetCoordinatesFromString(input, typeof(RectangularPlateau));
            //Assert
            actualResult.Should().BeNull();

        }

        #endregion
    }
}
