// See https://aka.ms/new-console-template for more information
using MarsRover;


string key;
MissionManager mission = new MissionManager();
do
{
    mission.PrintMainMenu();
    key = Console.ReadLine();
    mission.ChooseMainMenuItem(key);

} while (key.ToLower() != "q");
