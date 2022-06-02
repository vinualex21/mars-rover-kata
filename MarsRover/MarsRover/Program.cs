// See https://aka.ms/new-console-template for more information
using MarsRover;

Console.WriteLine("Melody Mars Mission\n");
ConsoleKeyInfo key;
MissionManager mission = new MissionManager();
do
{
    mission.PrintMenu();
    key = Console.ReadKey();
    mission.ChooseMenuItem(key.KeyChar);

} while (key.Key != ConsoleKey.Escape);
