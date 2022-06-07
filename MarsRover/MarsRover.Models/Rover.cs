using MarsRover.Models.enums;
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
        private Coordinates Position;
        public int ID { get; set; }
        public string Name { get; private set; }

        public VehicleStatus Staus { get; set; }

        public IPlateau Plateau { get; private set; }

        public Rover(int id, Coordinates position, IPlateau plateau)
        {
            ID = id;
            Name = "Rover" + ID.ToString();
            Position = position;
            Staus = VehicleStatus.Active;
            Plateau = plateau;
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
        public double MoveRover(string instructions, List<Rover> rovers)
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
                        if(!CheckRoverStatus(rovers))
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

        /// <summary>
        /// Checks if the rover has crashed with other rovers
        /// </summary>
        /// <param name="rovers"></param>
        /// <returns>true, if active; false, otherwise</returns>
        public bool CheckRoverStatus(List<Rover> rovers)
        {
            var stationaryRover = rovers.Where(r => r.ID != this.ID && r.Position
                                                .IsSamePosition(this.Position)).SingleOrDefault();
            if (stationaryRover != null)
            {
                this.Staus = VehicleStatus.Crashed;
                rovers.RemoveAll(r => r.ID == this.ID || r.ID == stationaryRover.ID);
                Console.WriteLine($"{this.Name} collided with {stationaryRover.Name} at {stationaryRover.Position.X} {stationaryRover.Position.Y}. {this.Name} and {stationaryRover.Name} lost.");
                return false;
            }
            if (!Plateau.IsCoordinatesWithinBounds(this.Position.X, this.Position.Y))
            {
                this.Staus = VehicleStatus.OffCourse;
                Console.WriteLine($"{Environment.NewLine}Mayday!");
                Console.WriteLine($"{Environment.NewLine}Rover{this.ID} has gone off the surface. Rover lost.");
                rovers.RemoveAll(r => r.ID == this.ID);
                return false;
            }

            return true;
        }
    }
}
