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
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine($"{Environment.NewLine}Enter coordinates for deployment (eg: 5 4 N): ");
                    var coordinateString = Console.ReadLine();
                    var coordinates = Coordinates.GetCoordinatesFromString(coordinateString, plateau.GetType());

                    if (coordinates != null)
                    {
                        Utilities.PrintLoadingMessage("Deploying rover", 800, 5);

                        if (!plateau.IsCoordinatesWithinBounds(coordinates.X, coordinates.Y))
                        {
                            Console.WriteLine($"{Environment.NewLine}Mayday! Rover has missed the plateau and crashed!");
                            Console.ReadKey();
                            break;
                        }

                        var rover = DeployRover(coordinates);
                        if (rover != null)
                        {
                            Console.WriteLine($"{rover.Name} deployed at {rover.GetCurrentPosition()}");
                        }
                        
                    }
                    Console.ReadKey();
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
                    var vehicleChoice = Console.ReadLine();
                    if (Utilities.TryConvertUserInputNumber(vehicleChoice, out int userChoice, 1, rovers.Count()))
                    {
                        var selectedRover = rovers.ElementAtOrDefault(userChoice - 1);
                        Console.WriteLine($"{Environment.NewLine}Enter instructions to move the vehicle: ");
                        var instructions = Console.ReadLine();
                        var distanceMoved = selectedRover.MoveRover(instructions, rovers);
                        if (selectedRover.Staus == VehicleStatus.Active)
                        {
                            Console.WriteLine($"{Environment.NewLine}{selectedRover.Name} is now at {selectedRover.GetCurrentPosition()}");
                        }
                    }
                    Console.ReadKey();
                    break;

                default:
                    if(option.ToLower()!="q")
                    {
                        Console.WriteLine("Invalid option. PLease choose a valid menu item.");
                        Console.ReadKey();
                    }
                    break;

            }
        }

        public void ChoosePlateauMenuItem(string plateauChoice)
        {
            if (Utilities.TryConvertUserInputNumber(plateauChoice, out int convertedNumber, 1, 2))
            {
                switch (convertedNumber)
                {
                    case 1:
                        Console.Write("Enter length of the plateau: ");
                        var userLength = Console.ReadLine();

                        Console.Write("Enter width of the plateau: ");
                        var userWidth = Console.ReadLine();

                        if(Utilities.TryConvertUserInputNumber(userLength, out int length) 
                            && Utilities.TryConvertUserInputNumber(userWidth, out int width))
                        {
                            plateau = new RectangularPlateau(length, width);
                            Utilities.PrintLoadingMessage("Mapping the surface", 800, 3);
                            Console.Write("Plateau mapped.");
                        }
                        

                        break;

                    case 2:
                        Console.WriteLine("Enter radius of the plateau: ");
                        var userRadius = Console.ReadLine();
                        if (Utilities.TryConvertUserInputNumber(userRadius, out int radius))
                        {
                            plateau = new CircularPlateau(radius);
                            Utilities.PrintLoadingMessage("Mapping the surface", 800, 3);
                            Console.Write("Plateau mapped.");
                        }
                        
                        break;
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Creates an instance of the rover at the specified coodinates and 
        /// adds it to the collection of rovers
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="orientation">cardinal direction</param>
        public Rover DeployRover(Coordinates coordinates)
        {
            var lastID = rovers.OrderByDescending(r => r.ID).FirstOrDefault()?.ID ?? 0;
            var rover = new Rover(lastID + 1, coordinates, plateau);
            rovers.Add(rover);
            return rover.CheckRoverStatus(rovers) ? rover : null;
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
