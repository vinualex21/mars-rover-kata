using NUnit.Framework;
using FluentAssertions;
using MarsRover.Models;
using System;

namespace MarsRover.Tests
{
    public class RoverTest
    {
        private Rover rover;
        [SetUp]
        public void Setup()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N));
        }

        [Test]
        public void GetCurrentPosition_ShouldReturnCorrectPosition()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N));
            rover.GetCurrentPosition().Should().Be("1 2 N");
        }

        [Test]
        public void MoveRoverWithValidInstructions_ShouldUpdatePositionAndReturnDistance()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N));
            rover.MoveRover("LMLMLMLMM").Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinate(3, 3, CardinalPoint.E));
            rover.MoveRover("MMRMMRMRRM").Should().Be(Math.Round(Math.Sqrt(8),2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverWithInvalidInstructions_ShouldThrowInvalidInputException()
        {
            rover = new Rover(1, new Coordinate(1, 2, CardinalPoint.N));
            rover.MoveRover("LMLMLMLMM").Should().Be(1);
            rover.GetCurrentPosition().Should().Be("1 3 N");

            rover = new Rover(1, new Coordinate(3, 3, CardinalPoint.E));
            rover.MoveRover("MMRMMRMRRM").Should().Be(Math.Round(Math.Sqrt(8), 2));
            rover.GetCurrentPosition().Should().Be("5 1 E");
        }

        [Test]
        public void MoveRoverBeyondPlateauBoundary_ShouldReturnNegativeOne()
        {
            rover = new Rover(1, new Coordinate(1, 3, CardinalPoint.N));
            rover.MoveRover("RMLMMMM").Should().Be(-1);

            rover = new Rover(1, new Coordinate(4, 5, CardinalPoint.W));
            rover.MoveRover("RRMRMMMMM").Should().NotBe(-1);

            rover = new Rover(1, new Coordinate(2, 1, CardinalPoint.S));
            rover.MoveRover("MM").Should().Be(-1);
        }
    }
}