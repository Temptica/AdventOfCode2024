namespace AdventureOfCode.Day10;

public class Day10 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);

        var topography = new List<Topography>();
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y];
            for (var x = 0; x < line.Length; x++)
            {
                var top = new Topography(x, y, int.Parse(line[x].ToString()));
                
                if (x != 0)
                {
                    var leftTop = topography.Single(t => t.X == x - 1 && t.Y == y);
                    top.Left = leftTop;
                    leftTop.Right = top;
                }

                if (y != 0)
                {
                    var rightTop = topography.Single(t => t.X == x && t.Y == y - 1);
                    top.Top = rightTop;
                    rightTop.Bottom = top;
                }
                
                topography.Add(top);
            }
        }
        
        Part1(topography);
    }

    private void Part1(List<Topography> topography)
    {
        var tops = topography.Where(t => t.Height == 9 && t.HasValidLowerNeighbour() ).ToList();

        foreach (var top in tops)
        {
            HasTrail(top,top);
        }
        
        var sum = topography.Sum(t => t.TrailStarts.Count);
        Console.WriteLine("Part 1:");
        Console.WriteLine(sum);
        
        var sum2 = topography.Sum(t => t.Trailheads);
        Console.WriteLine("Part 2:");
        Console.WriteLine(sum2);
    }

    private void HasTrail(Topography topography,Topography top)
    {
        if (topography.Height == 0)
        {
            topography.Trailheads++;
            if (!top.TrailStarts.Contains(topography))
            {
                top.TrailStarts.Add(topography);
            }
            return;
        }
        if(!topography.HasValidLowerNeighbour()) return;

        foreach (var validTopography in topography.GetValidLowerNeighbours())
        {
            HasTrail(validTopography,top);
        }
    }

    private class Topography(int x, int y, int height)
    {
        public Topography? Left { get; set; }
        public Topography? Right { get; set; }
        public Topography? Top { get; set; }
        public Topography? Bottom { get; set; }

        public bool HasValidLowerNeighbour()
        {
            if (Left != null && Left.Height == Height -1) return true;
            if (Right != null && Right.Height == Height - 1) return true;
            if (Top != null && Top.Height == Height - 1) return true;
            return Bottom != null && Bottom.Height == Height - 1;
        }

        public List<Topography> GetValidLowerNeighbours()
        {
            var neighbours = new List<Topography>();
            if (Left != null && Left.Height == Height - 1) neighbours.Add(Left);
            if (Right != null && Right.Height == Height - 1) neighbours.Add(Right);
            if (Top != null && Top.Height == Height - 1) neighbours.Add(Top);
            if (Bottom != null && Bottom.Height == Height - 1) neighbours.Add(Bottom);
            return neighbours;
        }
        
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int Height { get; set; } = height;
        public int Trailheads { get; set; } = 0; // PART2
        public List<Topography> TrailStarts { get; set; } = [];
    }
}