using System.Numerics;

namespace AdventureOfCode.Day11;

public class Day11 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);

        var stones = InitialiseData(input[0]);
        
        Part1(stones);
    }

    private static List<Stone> InitialiseData(string input)
    {
        var spilt = input.Split(" ");
        var stones = new List<Stone>();
        
        foreach (var line in spilt)
        {
            var stone = new Stone();
            stone.Value = int.Parse(line);
            if (stones.Count != 0)
            {
                var last = stones.Last();
                stone.Left = last;
                last.Right = stone;
            }
            stones.Add(stone);
        }
        return stones;
    }

    private List<Stone> _stones = [];

    private void Part1(List<Stone> stones)
    {
        _stones = stones.ToList();
        for (var i = 0; i < 75; i++)
        {
            var stonesList = _stones.ToList();
            Task.WaitAll(stonesList.Select(stone => Task.Run(() => { CheckStone(stone); })).ToArray());
            Console.WriteLine(i);
        }
        
        Console.WriteLine(_stones.Count);
    }
    
    private int _checkedStones = 0;

    private void CheckStone(Stone stone)
    {
        if (stone.Value == 0)
        {
            stone.Value = 1;
            stone.currentItteration++;
            return;
        }

        if (stone.Value.ToString().Length % 2 == 0)
        {
            //split value in the center (so 11 becomes 1 and 1)
            var stringValue = stone.Value.ToString();
            var rightSplit = int.Parse(stringValue.Remove(0, stringValue.Length / 2));
            var leftSplit = int.Parse(stringValue.Remove(stringValue.Length / 2));
            
            var oldRight = stone.Right;
            stone.Value = leftSplit;
            
            var newStone = new Stone
            {
                Value = rightSplit,
                currentItteration= ++stone.currentItteration,
                Left = stone,
                Right = oldRight
            };
            
            stone.Right = newStone;
            
            if (oldRight != null)
            {
                newStone.Right = oldRight;
            }
            _stones.Add(newStone);
            return;
        }

        stone.Value *= 2024;
        stone.currentItteration++;
    }
    

    private class Stone
    {
        public Stone Left { get; set; }
        public Stone Right { get; set; }
        public BigInteger Value { get; set; }
        public int currentItteration { get; set; } = 0;
    }
}