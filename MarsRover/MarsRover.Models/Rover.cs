using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class Rover : IExplorer
    {
        private Coordinate Position;
        public int ID { get; set; }

        private IPlateau plateau;

        public Rover(int id, Coordinate position, IPlateau plateau)
        {
            ID = id;
            Position = position;
            this.plateau = plateau;
        }

        /// <summary>
        /// Returns current coordinates of the rover
        /// </summary>
        /// <returns></returns>
        public string GetCurrentPosition()
        {
            return $"{Position.X} {Position.Y} {Position.Orientation}";
        }

        /// <summary>
        /// Moves the rover based on the input instructions
        /// </summary>
        /// <param name="instructions">L: turn left, R: turn right, M: move forward one unit</param>
        /// <returns>The distance travelled</returns>
        public double MoveRover(string instructions)
        {
            //current position
            int x1 = Position.X;
            int y1 = Position.Y;
            var cardinalCount = Enum.GetNames(typeof(CardinalPoint)).Count();
            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case 'L': 
                        Position.Orientation = (CardinalPoint)((int)(Position.Orientation - 1 + cardinalCount) % cardinalCount);
                        break;
                    case 'R':
                        Position.Orientation = (CardinalPoint)((int)(Position.Orientation + 1) % cardinalCount);
                        break;
                    case 'M':
                        switch (Position.Orientation)
                        {
                            case CardinalPoint.N:
                                Position.Y++;
                                break;
                            case CardinalPoint.E:
                                Position.X++;
                                break;
                            case CardinalPoint.S:
                                Position.Y--;
                                break;
                            case CardinalPoint.W:
                                Position.X--;
                                break;
                        }
                        if(!plateau.IsCoordinatesWithinBounds(Position.X, Position.Y))
                        {
                            return -1;
                        }
                        break;

                }
            }
            //distance = sqrt((x-x1)^2+(y-y1)^2) rounded to 2 decimal places
            var distance = Math.Round(Math.Sqrt(Math.Pow(Position.X - x1, 2) + Math.Pow(Position.Y - y1, 2)),2);
            return distance;
        }

    }
}
