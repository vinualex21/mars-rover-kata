using MarsRover.Models;
using MarsRover.Models.Plateaus;
using MarsRover.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRover.Models.enums;

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
            Console.WriteLine("**********************");
            Console.WriteLine("Melody Mars Mission\n");
            Console.WriteLine("**********************");
            Console.WriteLine($"{Environment.NewLine}MENU {Environment.NewLine}");
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
                    SetPlateau();
                    break;

                case "2":
                    if(plateau == null)
                    {
                        Console.WriteLine($"Please choose the plateau for deployment.{Environment.NewLine}");
                        SetPlateau();
                    }
                    Console.WriteLine("\nEnter coordinates for deployment (eg: 5 4 N): ");
                    var coordinateString = Console.ReadLine();
                    var splitCoordinates = coordinateString?.Split(' ');
                    var x_pos = Utilities.ConvertUserInputNumber(splitCoordinates[0]);
                    var y_pos = Utilities.ConvertUserInputNumber(splitCoordinates[1]);
                    
                    if (!plateau.IsCoordinatesWithinBounds(x_pos, y_pos))
                    {
                        Console.WriteLine($"{Environment.NewLine}Mayday! Rover has missed the plateau and crashed!");
                        Console.ReadKey();
                        break;
                    }
                    
                    DeployRover(x_pos, y_pos, splitCoordinates[2].FirstOrDefault());
                    break;

                case "3":
                    if(rovers.Count()==0)
                    {
                        Console.WriteLine("No active rovers on the surface");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine($"Active rovers on the surface: {Environment.NewLine}");
                    PrintExplorers();
                    Console.WriteLine($"{Environment.NewLine}Choose vehicle to be moved: ");
                    var vehicleChoice = Console.ReadKey().KeyChar;
                    var userChoice = Utilities.ConvertUserInputNumber(vehicleChoice.ToString(), 1, rovers.Count());
                    var selectedRover = rovers.ElementAtOrDefault(userChoice - 1);
                    Console.WriteLine($"{Environment.NewLine}Enter instructions to move the vehicle: ");
                    var instructions = Console.ReadLine();
                    var distanceMoved = selectedRover.MoveRover(instructions, rovers);
                    if (selectedRover.Staus == VehicleStatus.Active)
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
            var lastID = rovers.OrderByDescending(r => r.ID).FirstOrDefault()?.ID ?? 0;
            var rover = new Rover(lastID + 1, coordinates, plateau);
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
                Console.WriteLine($"{i++}. Rover{rover.ID} at {rover.GetCurrentPosition()}");
            }
        }

        private void SetPlateau()
        {
            PrintPlateauMenu();
            var plateauChoice = Console.ReadLine();
            ChoosePlateauMenuItem(plateauChoice);
        }
    }
}
