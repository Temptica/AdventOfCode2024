namespace AdventureOfCode.Day2;

public class Day2 : IAdventure
{
    public void Run()
    {
        var input = File.ReadAllLines(StaticProps.Path + "Day2/Day2.txt");

        Part1(input);
    }

    private static void Part1(string[] input)
    {
        var counter = 0;
        foreach (var line in input)
        {
            var intList = line.Split(" ").Select(int.Parse).ToList();
            var isIncreasing = intList[0] < intList[1];
            if (CheckList(intList, -1))
            {
                counter++;   
            }
        }

        Console.WriteLine(counter);
    }

    private static bool CheckList(List<int> intList, int replaceIndex)
    {
        while (true)
        {
            if (intList.Count <= replaceIndex) return false;

            var list = new List<int>(intList);

            if (replaceIndex != -1) list.RemoveAt(replaceIndex);

            var mistake = false;
            
            var isIncreasing = list[0] < list[1];

            for (var i = 1; i < list.Count; i++)
            {
                var lastValue = list[i - 1];
                var currentValue = list[i];

                if (isIncreasing && lastValue > currentValue)
                {
                    mistake = true;
                    break;
                }

                if (!isIncreasing && lastValue < currentValue)
                {
                    mistake = true;
                    break;
                }

                if (Math.Abs(lastValue - currentValue) is < 1 or > 3)
                {
                    mistake = true;
                    break;
                }
            }

            if (!mistake) return true;

            replaceIndex += 1;
        }
    }
}