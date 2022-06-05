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
        public static bool TryConvert<T>(string input, out T result)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Argument null or empty");
            }
            if (Enum.TryParse(typeof(T), input, out object convertedValue))
            {
                result = (T)convertedValue;
                return true;
            }
            result = default(T);
            return false;
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
        public static bool TryConvertUserInputNumber(string input, out int result, int? lowerLimit = null, int? UpperLimit = null)
        {
            if(string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input received. Please enter a valid input.");
                Console.ReadKey();
                result = 0;
                return false;
            }
            else if(int.TryParse(input, out int output))
            {
                if((output < (lowerLimit?? int.MinValue)) || (output > (UpperLimit ?? int.MaxValue)))
                {
                    Console.WriteLine("Input out of range. Please enter a valid input.");
                    Console.ReadKey();
                    result = 0;
                    return false;
                }
                result = output;
                return true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid input.");
                Console.ReadKey();
                result = 0;
                return false;
            }
        }

        public static void PrintLoadingMessage(string message, int interval, int count)
        {
            Console.Write(message);
            for(int i = 0; i < count; i++)
            {
                Thread.Sleep(interval);
                Console.Write(".");
            }
            Console.WriteLine();
        }
    }
}
