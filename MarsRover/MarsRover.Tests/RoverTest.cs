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
    public class RoverTest
    {
        private Rover rover;
        private IPlateau plateau;
        
        [SetUp]
        public void Setup()
        {
            plateau = new RectangularPlateau(5, 5);
            rover = new Rover(1, new Coordinates(1, 2, CardinalPoint.N), plateau);
        }

        #region GetCurrentPosition

        [Test]
        public void GetCurrentPosition_ShouldReturnCorrectPosition()
        {
            rover = new Rover(1, new Coordinates(1, 2, CardinalPoint.N), plateau);
            rover.GetCurrentPosition().Should().Be("1 2 N");
        }

        #endregion

        #region MoveRover

        [Test]
        public void MoveRoverWithValidInstructions_ShouldUpdatePositionAndReturnDistance()
        {
            var rovers = new List<Rover>();
            rover = new Rover(1, new Coordinates(1, 2, CardinalPoint.N), plateau);
            rover.MoveRover("LMLMLMLMM", rovers).Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinates(3, 3, CardinalPoint.E), plateau);
            rover.MoveRover("MMRMMRMRRM", rovers).Should().Be(Math.Round(Math.Sqrt(8), 2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverWithInvalidInstructions_ShouldThrowInvalidInputException()
        {
            var rovers = new List<Rover>();
            rover = new Rover(1, new Coordinates(1, 2, CardinalPoint.N), plateau);
            rover.MoveRover("LMLMLMLMM", rovers).Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinates(3, 3, CardinalPoint.E), plateau);
            rover.MoveRover("MMRMMRMRRM", rovers).Should().Be(Math.Round(Math.Sqrt(8), 2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverBeyondPlateauBoundary_ShouldReturnNegativeOne()
        {
            var rovers = new List<Rover>();
            rover = new Rover(1, new Coordinates(1, 3, CardinalPoint.N), plateau);
            rover.MoveRover("RMLMMMM", rovers).Should().Be(-1);

            rover = new Rover(1, new Coordinates(4, 5, CardinalPoint.W), plateau);
            rover.MoveRover("RRMRMMMMM", rovers).Should().NotBe(-1);

            rover = new Rover(1, new Coordinates(2, 1, CardinalPoint.S), plateau);
            rover.MoveRover("MM", rovers).Should().Be(-1);
        }

        [Test]
        public void MoveRoverToPositionOfAnotherRover_ShouldChangeRoverStatusToCrashed()
        {
            var rover1 = new Rover(1, new Coordinates(4,4, CardinalPoint.W), plateau);
            var rovers = new List<Rover>() { rover1 };
            var rover2 = new Rover(2, new Coordinates(2,4, CardinalPoint.W), plateau);

            rover2.MoveRover("RRMM", rovers);

            rover2.Staus.Should().Be(VehicleStatus.Crashed);
        }

        #endregion

        #region CheckRoverStatus

        [Test]
        public void CheckRoverStatus_GivenRoverInUnoccupiedCoordinates_ShouldReturnTrue()
        {
            //Arrange
            var newRoverCoordinates = new Coordinates(3, 4, CardinalPoint.N);
            var plateau = new RectangularPlateau(5, 5);
            var rover = new Rover(3, newRoverCoordinates, plateau);

            var rover1Coordinates = new Coordinates(1, 1, CardinalPoint.E);
            var existingRover1 = new Rover(1, rover1Coordinates, plateau);

            var rover2Coordinates = new Coordinates(4, 4, CardinalPoint.E);
            var existingRover2 = new Rover(2, rover2Coordinates, plateau);

            var existingRovers = new List<Rover> { existingRover1, existingRover2 };
            
            //Act
            var status = rover.CheckRoverStatus(existingRovers);

            //Assert
            status.Should().BeTrue();
        }

        [Test]
        public void CheckRoverStatus_GivenRoverInOccupiedCoordinates_ShouldReturnFalse()
        {
            //Arrange
            var newRoverCoordinates = new Coordinates(1, 1, CardinalPoint.N);
            var plateau = new RectangularPlateau(5, 5);
            var rover = new Rover(3, newRoverCoordinates, plateau);

            var rover1Coordinates = new Coordinates(1, 1, CardinalPoint.E);
            var existingRover1 = new Rover(1, rover1Coordinates, plateau);

            var rover2Coordinates = new Coordinates(4, 4, CardinalPoint.E);
            var existingRover2 = new Rover(2, rover2Coordinates, plateau);

            var existingRovers = new List<Rover> { existingRover1, existingRover2 };

            //Act
            var status = rover.CheckRoverStatus(existingRovers);

            //Assert
            status.Should().BeFalse();
        }

        #endregion
    }
}