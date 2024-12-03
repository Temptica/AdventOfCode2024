namespace AdventureOfCode;

public static class InputReader
{
    public static string[] Read(IAdventure day)
    {
        var className = day.GetType().Name;
        return File.ReadAllLines($"{StaticProps.Path}{className}/{className}.txt");
    }
    
}