namespace AdventureOfCode.Day8;

public class Day8 : IAdventure
{
    private static List<Frequency> _frequencies = new();
    private static int _maxX;
    private static int _maxY;
    private static bool part2 = false;
    public void Run()
    {
        var input = InputReader.Read(this);
        
        ConvertInput(input);
        
        Part1();
        part2 = true;
        Part1();
        
    }

    private void Part1()
    {
        var nodesToCheck = _frequencies
            .Where(f => f.FrequencyId != null)
            .GroupBy(f => f.FrequencyId)
            .ToList();

        foreach (var frequencyGroup in nodesToCheck)
        {
            var checkedFrequencies = new List<Frequency>();
            foreach (var frequency in frequencyGroup)
            {
                checkedFrequencies.Add(frequency);
                foreach (var frequency2 in frequencyGroup.Where(f=>!checkedFrequencies.Contains(f)))
                {
                    SetAntiNodeLocation(frequency, frequency2);
                }
            }
        }

        var count = _frequencies.Count(f => f.ContainsAntiNode);
        Console.WriteLine(count);
        var file = _frequencies
            .GroupBy(f => f.Y)
            .OrderBy(g => g.Key)
            .Select(frequency => string
                                                    .Join("", frequency
                                                    .OrderBy(f => f.X)
                                                    .Select(GetChar)))
            .ToList();

        File.WriteAllLines("output.txt", file);
    }

    private char GetChar(Frequency frequency)
    {
        if(frequency.FrequencyId.HasValue) return frequency.FrequencyId.Value;
        return frequency.ContainsAntiNode ? '#' : '.';
    }
    private static void SetAntiNodeLocation(Frequency frequency1, Frequency frequency2)
    {
        if(frequency1.FrequencyId != frequency2.FrequencyId) return;
        frequency1.ContainsAntiNode = true;
        frequency2.ContainsAntiNode = true;
        
        
        //get distance from frequency 1 compared to frequency 2.
        var xDistance = Math.Abs(frequency1.X - frequency2.X);
        var yDistance = Math.Abs(frequency1.Y - frequency2.Y);

        var direction1XToMove = 0;
        var direction1YToMove = 0;
        var direction2XToMove = 0;
        var direction2YToMove = 0;

        if (frequency1.X < frequency2.X)
        {
            direction1XToMove = - xDistance;
            direction2XToMove = xDistance;
        }
        else if(frequency1.X > frequency2.X)
        {
            direction1XToMove = xDistance;
            direction2XToMove = - xDistance;
        }

        if (frequency1.Y < frequency2.Y)
        {
            direction1YToMove = - yDistance;
            direction2YToMove = yDistance;
        }
        else if (frequency1.Y > frequency2.Y)
        {
            direction1YToMove = yDistance;
            direction2YToMove = - yDistance;
        }
        DuplicateNodeLocation(frequency1, direction1XToMove, direction1YToMove);
        DuplicateNodeLocation(frequency2, direction2XToMove, direction2YToMove);
    }

    private static void DuplicateNodeLocation(Frequency lastFrequency, int xToAdd, int yToAdd)
    {
        var x = lastFrequency.X + xToAdd;
        var y = lastFrequency.Y + yToAdd;

        var frequency = GetFrequency(x, y);
        if (frequency == null) return;
        frequency.ContainsAntiNode = true;

        #region PART 2
        if(part2) DuplicateNodeLocation(frequency, xToAdd, yToAdd); //could be reppalced with a do-while but would make it less visibly what part 1 and part 2 is
        #endregion

    }
    
    private static Frequency? GetFrequency(int x, int y) => _frequencies.FirstOrDefault(f => f.X == x && f.Y == y);

    private class Frequency
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char? FrequencyId { get; set; }
        public bool ContainsAntiNode { get; set; }
    }
    
    private void ConvertInput(string[] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (int x = 0; x < line.Length; x++)
            {
                var frequency = new Frequency()
                {
                    X = x,
                    Y = y
                };
                
                if (line[x] != '.')
                {
                    frequency.FrequencyId = line[x];
                }
                
                _frequencies.Add(frequency);
            }
            
        }
    }
}