using MarsRover.Models;
using MarsRover.Models.Plateaus;
using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class MissionManager
    {
        private List<Rover> rovers;

        private IPlateau plateau;

        public MissionManager()
        {
            rovers = new List<Rover>();
            plateau = new RectangularPlateau(0,0);
        }

        /// <summary>
        /// Displays the main menu
        /// </summary>
        public void PrintMainMenu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1. Select plateau");
            Console.WriteLine("2. Deploy vehicle");
            Console.WriteLine("3. Move vehicle");
            Console.WriteLine("Esc. Exit");
            Console.Write("\nSelect Option: ");

        }

        /// <summary>
        /// Executes the operation requestion by the user
        /// </summary>
        /// <param name="option">the chosen menu item</param>
        public void ChooseMainMenuItem(char option)
        {
            Console.Clear();
            switch (option)
            {
                case '2':
                    Console.WriteLine("\nEnter coordinates for deployment: ");
                    var coordinateString = Console.ReadLine();
                    var splitCoordinates = coordinateString?.Split(' ');
                    DeployRover(Convert.ToInt32(splitCoordinates[0]), 
                        Convert.ToInt32(splitCoordinates[1]), splitCoordinates[2].FirstOrDefault());
                    break;

                case '3':
                    Console.WriteLine($"Choose vehicle to be moved: {Environment.NewLine}");
                    PrintExplorers();
                    Console.WriteLine($"{Environment.NewLine}Choose vehicle to be moved: ");
                    var vehicleChoice = Console.ReadKey().KeyChar;
                    Utilities.ValidateUserInputNumber(vehicleChoice.ToString());
                    var index = int.Parse(vehicleChoice.ToString()) - 1;
                    var selectedRover = rovers.ElementAtOrDefault(index);
                    Console.WriteLine($"{Environment.NewLine}Enter instructions to move the vehicle: ");
                    var instructions = Console.ReadLine();
                    selectedRover.MoveRover(instructions);
                    Console.WriteLine($"{Environment.NewLine}Rover{selectedRover.ID} is now at {selectedRover.GetCurrentPosition()}");
                    break;

            }
        }

        /// <summary>
        /// Creates an instance of the rover at the specified coodinates and 
        /// adds it to the collection of rovers
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="orientation">cardinal direction</param>
        public void DeployRover(int x, int y, char orientation)
        {
            var cardinalDirection = Utilities.Convert<CardinalPoint>(orientation.ToString());
            var coordinates = new Coordinate(x, y, cardinalDirection);
            var rover = new Rover(rovers.Count()+1, coordinates, plateau);
            rovers.Add(rover);
            
        }

        private void PrintExplorers()
        {
            if (rovers.Count() == 0)
            {
                Console.WriteLine("No active rovers on the surface.");
            }
            int i = 1;
            foreach(var rover in rovers)
            {
                Console.WriteLine($"{i}. Rover{rover.ID} at {rover.GetCurrentPosition()}");
            }
        }
    }
}
