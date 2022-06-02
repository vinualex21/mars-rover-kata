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

        public static void ValidateUserInputNumber(string input, int? lowerLimit = null, int? UpperLimit = null)
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
            }
            else
            {
                throw new ArgumentException("Invalid input. Please enter a valid input.");
            }
        }
    }
}
