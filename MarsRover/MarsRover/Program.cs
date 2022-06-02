// See https://aka.ms/new-console-template for more information
using MarsRover;

Console.WriteLine("Melody Mars Mission\n");
ConsoleKeyInfo key;
MissionManager mission = new MissionManager();
do
{
    mission.PrintMainMenu();
    key = Console.ReadKey();
    mission.ChooseMainMenuItem(key.KeyChar);

} while (key.Key != ConsoleKey.Escape);
