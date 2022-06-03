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
            
        }

        /// <summary>
        /// Displays the main menu
        /// </summary>
        public void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine($"MENU {Environment.NewLine}");
            Console.WriteLine("1. Select plateau");
            Console.WriteLine("2. Deploy vehicle");
            Console.WriteLine("3. Move vehicle");
            Console.WriteLine("Q. Quit");
            Console.Write($"{Environment.NewLine}Select Option: ");

        }

        public void PrintPlateauMenu()
        {
            Console.WriteLine($"PLATEAU TYPES {Environment.NewLine}");
            Console.WriteLine("1. Rectangular plateau");
            Console.WriteLine("2. Circular plateau");
            Console.Write($"{Environment.NewLine}Select plateau: ");

        }

        /// <summary>
        /// Executes the operation requestion by the user
        /// </summary>
        /// <param name="option">the chosen menu item</param>
        public void ChooseMainMenuItem(string option)
        {
            Console.Clear();
            switch (option)
            {
                case "1":
                    PrintPlateauMenu();
                    var plateauChoice = Console.ReadLine();
                    ChoosePlateauMenuItem(plateauChoice);
                    break;

                case "2":
                    Console.WriteLine("\nEnter coordinates for deployment: ");
                    var coordinateString = Console.ReadLine();
                    var splitCoordinates = coordinateString?.Split(' ');
                    DeployRover(Convert.ToInt32(splitCoordinates[0]), 
                        Convert.ToInt32(splitCoordinates[1]), splitCoordinates[2].FirstOrDefault());
                    break;

                case "3":
                    Console.WriteLine($"Choose vehicle to be moved: {Environment.NewLine}");
                    PrintExplorers();
                    Console.WriteLine($"{Environment.NewLine}Choose vehicle to be moved: ");
                    var vehicleChoice = Console.ReadKey().KeyChar;
                    var userChoice = Utilities.ConvertUserInputNumber(vehicleChoice.ToString(), 1, rovers.Count());
                    var selectedRover = rovers.ElementAtOrDefault(userChoice - 1);
                    Console.WriteLine($"{Environment.NewLine}Enter instructions to move the vehicle: ");
                    var instructions = Console.ReadLine();
                    var distanceMoved = selectedRover.MoveRover(instructions);
                    if (distanceMoved == -1)
                    {
                        Console.WriteLine($"{Environment.NewLine}Mayday!");
                        Console.WriteLine($"{Environment.NewLine}Rover{selectedRover.ID} has gone off the surface. Rover lost.");
                        rovers.RemoveAll(r => r.ID == selectedRover.ID); 
                    }
                    else
                    {
                        Console.WriteLine($"{Environment.NewLine}Rover{selectedRover.ID} is now at {selectedRover.GetCurrentPosition()}");
                    }
                    break;

            }
        }

        public void ChoosePlateauMenuItem(string plateauChoice)
        {
            var choice = Utilities.ConvertUserInputNumber(plateauChoice, 1, 2);
            switch(choice)
            {
                case 1:
                    Console.WriteLine("Enter length of plateau: ");
                    var userLength = Console.ReadLine();
                    var length = Utilities.ConvertUserInputNumber(userLength);

                    Console.WriteLine("Enter width of plateau: ");
                    var userWidth = Console.ReadLine();
                    var width = Utilities.ConvertUserInputNumber(userWidth);

                    plateau = new RectangularPlateau(length, width);
                    break;

                case 2:
                    Console.WriteLine("Enter radius of plateau: ");
                    var userRadius = Console.ReadLine();
                    var radius = Utilities.ConvertUserInputNumber(userRadius);

                    plateau = new CircularPlateau(radius);
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
