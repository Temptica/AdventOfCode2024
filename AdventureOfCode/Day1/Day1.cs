namespace AdventureOfCode.Day1;

public class Day1 : IAdventure
{
    public void Run()
    {
        var input = File.ReadAllLines(StaticProps.Path+"Day1/Day1.txt");
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in input)
        {
            var split = line.Split("   ");
            leftList.Add(int.Parse(split[0]));
            rightList.Add(int.Parse(split[1]));
        }
        
        Part1(leftList, rightList);
        
        Part2(leftList, rightList);
        
    }

    public void Part1(List<int> leftList, List<int> rightList)
    {
        leftList.Sort();
        rightList.Sort();

        var sum = 0;

        for (var i = 0; i < leftList.Count; i++)
        {
            sum += Math.Abs(leftList[i] - rightList[i]);
        }
        
        Console.WriteLine("Part 1: {0}", sum);
    }

    public void Part2(List<int> leftList, List<int> rightList)
    {
        var sum = 0;
        

        foreach (var left in leftList)
        {
            var amount = rightList.Count(right => right == left);
            sum += left*amount;
        }

        Console.WriteLine("Part 2: {0}", sum);
    }
}