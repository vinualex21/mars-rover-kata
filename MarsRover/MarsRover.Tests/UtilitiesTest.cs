using NUnit.Framework;
using FluentAssertions;
using MarsRover.Models;
using System;

namespace MarsRover.Tests
{
    public class UtilitiesTest
    {
        private int convertedNumber;

        [Test]
        public void ValidateUserInputNumber_GivenNull_ShouldReturnFalse()
        {
            Utilities.TryConvertUserInputNumber(null, out convertedNumber).Should().BeFalse();
        }

        [Test]
        public void ValidateUserInputNumber_GivenEmptyInput_ShouldReturnFalse()
        {
            Utilities.TryConvertUserInputNumber("", out convertedNumber).Should().BeFalse();
        }

        [Test]
        public void ValidateUserInputNumber_GivenAnAlphabet_ShouldReturnFalse()
        {
            Utilities.TryConvertUserInputNumber("D", out convertedNumber).Should().BeFalse();
        }

        [Test]
        public void ValidateUserInputNumber_GivenASpecialCharacter_ShouldReturnFalse()
        {
            Utilities.TryConvertUserInputNumber("%", out convertedNumber).Should().BeFalse();
        }

        [Test]
        public void ValidateUserInputNumber_GivenValidNumber_ShouldReturnTrueAndOutputValue()
        {
            Utilities.TryConvertUserInputNumber("5", out convertedNumber).Should().BeTrue();
            convertedNumber.Should().Be(5);


            Utilities.TryConvertUserInputNumber("-11", out convertedNumber).Should().BeTrue();
            convertedNumber.Should().Be(-11);
        }

        [Test]
        public void ValidateUserInputNumber_GivenNumberOutOfGivenRange_ShouldReturnFalse()
        {
            Utilities.TryConvertUserInputNumber("-3", out convertedNumber, 0).Should().BeFalse();


            Utilities.TryConvertUserInputNumber("10", out convertedNumber, null, 8).Should().BeFalse();
        }

        [Test]
        public void ValidateUserInputNumber_GivenNumberInRange_ShouldReturnTrueAndOutputValue()
        {
            Utilities.TryConvertUserInputNumber("4", out convertedNumber, 0).Should().BeTrue();
            convertedNumber.Should().Be(4);


            Utilities.TryConvertUserInputNumber("3500", out convertedNumber, 0, 3500).Should().BeTrue();
            convertedNumber.Should().Be(3500);
        }
    }
}
