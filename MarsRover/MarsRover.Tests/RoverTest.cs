using NUnit.Framework;
using FluentAssertions;
using MarsRover.Models;
using System;
using MarsRover.Models.Interfaces;
using MarsRover.Models.Plateaus;

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
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N), plateau);
        }

        [Test]
        public void GetCurrentPosition_ShouldReturnCorrectPosition()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N), plateau);
            rover.GetCurrentPosition().Should().Be("1 2 N");
        }

        [Test]
        public void MoveRoverWithValidInstructions_ShouldUpdatePositionAndReturnDistance()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N), plateau);
            rover.MoveRover("LMLMLMLMM").Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinate(3, 3, CardinalPoint.E), plateau);
            rover.MoveRover("MMRMMRMRRM").Should().Be(Math.Round(Math.Sqrt(8),2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverWithInvalidInstructions_ShouldThrowInvalidInputException()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N), plateau);
            rover.MoveRover("LMLMLMLMM").Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinate(3, 3, CardinalPoint.E), plateau);
            rover.MoveRover("MMRMMRMRRM").Should().Be(Math.Round(Math.Sqrt(8), 2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverBeyondPlateauBoundary_ShouldReturnNegativeOne()
        {
            rover = new Rover(1, new Coordinate(1, 3, CardinalPoint.N), plateau);
            rover.MoveRover("RMLMMMM").Should().Be(-1);

            rover = new Rover(1, new Coordinate(4, 5, CardinalPoint.W), plateau);
            rover.MoveRover("RRMRMMMMM").Should().NotBe(-1);

            rover = new Rover(1, new Coordinate(2, 1, CardinalPoint.S), plateau);
            rover.MoveRover("MM").Should().Be(-1);
        }
    }
}