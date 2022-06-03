using NUnit.Framework;
using FluentAssertions;
using MarsRover.Models;
using System;

namespace MarsRover.Tests
{
    public class UtilitiesTest
    {
        [Test]
        public void ValidateUserInputNumber_GivenNull_ShouldThrowException()
        {
            Action invokingWithNullInput = () => Utilities.ConvertUserInputNumber(null);

            invokingWithNullInput.Should()
                .Throw<ArgumentNullException>("No input received. Please enter a valid input.");
        }

        [Test]
        public void ValidateUserInputNumber_GivenEmptyInput_ShouldThrowException()
        {
            Action invokingWithEmptyInput = () => Utilities.ConvertUserInputNumber("");

            invokingWithEmptyInput.Should()
                .Throw<ArgumentNullException>("No input received. Please enter a valid input.");
        }

        [Test]
        public void ValidateUserInputNumber_GivenAnAlphabet_ShouldThrowException()
        {
            Action invokingWithAlphabetInput = () => Utilities.ConvertUserInputNumber("D");

            invokingWithAlphabetInput.Should()
                .Throw<ArgumentException>("Invalid input. Please enter a valid input.");
        }

        [Test]
        public void ValidateUserInputNumber_GivenASpecialCharacter_ShouldThrowException()
        {
            Action invokingWithSpecialCharacterInput = () => Utilities.ConvertUserInputNumber("%");

            invokingWithSpecialCharacterInput.Should()
                .Throw<ArgumentException>("Invalid input. Please enter a valid input.");
        }

        [Test]
        public void ValidateUserInputNumber_GivenValidNumber_ShouldNotThrowException()
        {
            Action invokingWithValidNumberInput = () => Utilities.ConvertUserInputNumber("5");
            invokingWithValidNumberInput.Should()
                .NotThrow<Exception>();


            invokingWithValidNumberInput = () => Utilities.ConvertUserInputNumber("-10");
            invokingWithValidNumberInput.Should()
                .NotThrow<Exception>();
        }

        [Test]
        public void ValidateUserInputNumber_GivenNumberOutOfGivenRange_ShouldThrowException()
        {
            Action invokingWithOutOfRangeInput = () => Utilities.ConvertUserInputNumber("-3",0);
            invokingWithOutOfRangeInput.Should()
                .Throw<ArgumentOutOfRangeException>("Input out of range. Please enter a valid input.");


            invokingWithOutOfRangeInput = () => Utilities.ConvertUserInputNumber("10", 0, 8);
            invokingWithOutOfRangeInput.Should()
                .Throw<ArgumentOutOfRangeException>("Input out of range. Please enter a valid input.");
        }

        [Test]
        public void ValidateUserInputNumber_GivenNumberInRange_ShouldNotThrowException()
        {
            Action invokingWithInputInRange = () => Utilities.ConvertUserInputNumber("4", 0);
            invokingWithInputInRange.Should()
                .NotThrow<ArgumentOutOfRangeException>();


            invokingWithInputInRange = () => Utilities.ConvertUserInputNumber("3500", 0, 3500);
            invokingWithInputInRange.Should()
                .NotThrow<ArgumentOutOfRangeException>();
        }
    }
}
