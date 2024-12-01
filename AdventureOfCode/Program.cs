// See https://aka.ms/new-console-template for more information

using System.Reflection;
using AdventureOfCode;

Console.WriteLine("Hello, what would you like to do?");
Console.WriteLine("Enter: Run today's code");
Console.WriteLine("any number: Run given number");

var input = Console.ReadLine();
var day = 0;
if (string.IsNullOrEmpty(input))
{
    day = DateTime.Today.Day;
}
else
{
    int.TryParse(input, out day);
}

//get the Class inhereting from IAdventure with Day and then the number from the input behind it
var adventure = Assembly
    .GetExecutingAssembly()
    .GetTypes()
    .Where(x => x.GetInterfaces().Contains(typeof(IAdventure)))
    .Where(x => x.Name == "Day"+day)
    .Select(x => (IAdventure)Activator.CreateInstance(x))
    .SingleOrDefault();

if (adventure != null)
{
    adventure.Run();
    
}
    