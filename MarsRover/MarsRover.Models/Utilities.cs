using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public static class Utilities
    {
        /// <summary>
        /// Converts the input to type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T Convert<T>(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new ArgumentException("Argument null or empty");
                }
                if (Enum.TryParse(typeof(T), input, out object result))
                {
                    return (T)result;
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Validates the user input is a number and is within the range if provided.
        /// </summary>
        /// <param name="input">user input</param>
        /// <param name="lowerLimit">lower limit of the range expected</param>
        /// <param name="UpperLimit">upper limit of the range expected</param>
        /// <returns>the integer number</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int ConvertUserInputNumber(string input, int? lowerLimit = null, int? UpperLimit = null)
        {
            if(string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(paramName: "input", message: "No input received. Please enter a valid input.");
            }
            if(int.TryParse(input, out int result))
            {
                if((result < (lowerLimit?? int.MinValue)) || (result > (UpperLimit ?? int.MaxValue)))
                {
                    throw new ArgumentOutOfRangeException("Input out of range. Please enter a valid input.");
                }
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid input. Please enter a valid input.");
            }
        }
    }
}
