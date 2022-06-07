using MarsRover.Models.Plateaus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CardinalPoint Orientation { get; set; }

        private const int COORDINATE_PARTS_COUNT = 3;

        public Coordinates(int x, int y, CardinalPoint orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public bool IsSamePosition(Coordinates c)
        {
            return X == c.X && Y == c.Y;
        }

        public static Coordinates GetCoordinatesFromString(string input, Type typeOfPlateau)
        {
            if (input == null)
                return null;

            int x_coordinate = 0, y_coordinate = 0, convertedNumber;
            var splitCoordinates = input.Split(' ');

            if(splitCoordinates.Count() != COORDINATE_PARTS_COUNT)
            {
                Console.WriteLine("Invalid input format. Please enter coordinates in the specified format.");
                return null;
            }

            int? lowerLimit = typeof(RectangularPlateau).Equals(typeOfPlateau) ? 0 : null;
            if (Utilities.TryConvertUserInputNumber(splitCoordinates[0], out convertedNumber, lowerLimit))
            {
                x_coordinate = convertedNumber;
            }
            else
            {
                return null;
            }
            if (Utilities.TryConvertUserInputNumber(splitCoordinates[1], out convertedNumber, lowerLimit))
            {
                y_coordinate = convertedNumber;
            }
            else
            {
                return null;
            }
            if(Utilities.TryConvert<CardinalPoint>(splitCoordinates[2], out CardinalPoint orientation))
            {
                Coordinates c = new Coordinates(x_coordinate, y_coordinate, orientation);
                return c;
            }
            else
            {
                Console.WriteLine("Invalid Cardinal point. Please enter one of N, E, S or W.");
                return null;
            }
            
        }

    }
}
