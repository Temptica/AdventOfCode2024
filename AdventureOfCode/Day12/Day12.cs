using System.Numerics;

namespace AdventureOfCode.Day12;

public class Day12 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);
        
        var plants = InitialiseInput(input);
        
        FindRegions(plants);
        
        //Part1(plants.ToList());
        Part2(plants.ToList());
    }

    

    private static void Part1(List<Plant> plants)
    {
        var plantGroups = plants.GroupBy(p => p.RegionId);
        BigInteger sum = 0;
        
        foreach (var region in plantGroups)
        {
            var count = region.Count();
            var borderCount = region.Sum(plant => Math.Abs(plant.Neighbours.Count - 4));
            Console.WriteLine($"{count}: {borderCount} {region.First().Character}");
            sum += count*borderCount;
        }
        
        Console.WriteLine(sum);
    }

    private static void Part2(List<Plant> plants)
    {
        var plantGroups = plants.GroupBy(p => p.RegionId);
        BigInteger sum = 0;
        
        foreach (var region in plantGroups)
        {
            var count = region.Count();
            var borderCount = 0;
            foreach (var plant in region)
            {
                //Top
                if (!plant.TopChecked && !plant.Neighbours.Any(p => p.Y < plant.Y))
                {
                    plant.TopChecked = true;
                    borderCount++;
                    
                    var rightTop = RightNeighboursTopCheck(plant);
                    foreach (var neighbour in rightTop)
                    {
                        neighbour.TopChecked = true;
                    }
                    
                    var leftTop = LeftNeighboursTopCheck(plant);
                    foreach (var neighbour in leftTop)
                    {
                        neighbour.TopChecked = true;
                    }
                }
                
                //Bottom
                if (!plant.BottomChecked && !plant.Neighbours.Any(p => p.Y > plant.Y))
                {
                    plant.BottomChecked = true;
                    borderCount++;
                    
                    var rightBottom = RightNeighboursBottomCheck(plant);
                    foreach (var neighbour in rightBottom)
                    {
                        neighbour.BottomChecked = true;
                    }
                    
                    var leftBottom = LeftNeighboursBottomCheck(plant);
                    foreach (var neighbour in leftBottom)
                    {
                        neighbour.BottomChecked = true;
                    }
                }
                
                //Left
                if (!plant.LeftChecked && !plant.Neighbours.Any(p => p.X < plant.X))
                {
                    plant.LeftChecked = true;
                    borderCount++;
                    
                    var topLeft = TopNeighboursLeftCheck(plant);
                    foreach (var neighbour in topLeft)
                    {
                        neighbour.LeftChecked = true;
                    }
                    
                    var bottomLeft = BottomNeighboursLeftCheck(plant);
                    foreach (var neighbour in bottomLeft)
                    {
                        neighbour.LeftChecked = true;
                    }
                }
                
                //Right
                if (!plant.RightChecked && !plant.Neighbours.Any(p => p.X > plant.X))
                {
                    plant.RightChecked = true;
                    borderCount++;
                    
                    var topRight = TopNeighboursRightCheck(plant);
                    foreach (var neighbour in topRight)
                    {
                        neighbour.RightChecked = true;
                    }
                    
                    var bottomRight = BottomNeighboursRightCheck(plant);
                    foreach (var neighbour in bottomRight)
                    {
                        neighbour.RightChecked = true;
                    }
                }
            }
            Console.WriteLine($"{region.First().Character}: {count} * {borderCount} = {count*borderCount}");
            
            sum += count*borderCount;
        }
        
        Console.WriteLine(sum);
    }

    private static List<Plant> RightNeighboursTopCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.X > plants.X && !p.Neighbours.Any(p2 => p2.Y < p.Y));
        if (neighbour is null) return [];
        
        var neighbours = RightNeighboursTopCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static List<Plant> RightNeighboursBottomCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.X > plants.X && !p.Neighbours.Any(p2 => p2.Y > p.Y));
        if (neighbour is null) return [];
        
        var neighbours = RightNeighboursBottomCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static List<Plant> LeftNeighboursTopCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.X < plants.X && !p.Neighbours.Any(p2 => p2.Y < p.Y));
        if (neighbour is null) return [];
        
        var neighbours = LeftNeighboursTopCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static List<Plant> LeftNeighboursBottomCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.X < plants.X && !p.Neighbours.Any(p2 => p2.Y > p.Y));
        if (neighbour is null) return [];
        
        var neighbours = LeftNeighboursBottomCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static List<Plant> TopNeighboursRightCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.Y < plants.Y && !p.Neighbours.Any(p2 => p2.X > p.X));
        
        if (neighbour is null) return [];
        
        var neighbours = RightNeighboursTopCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static List<Plant> TopNeighboursLeftCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.Y < plants.Y && !p.Neighbours.Any(p2 => p2.X < p.X));
        
        if (neighbour is null) return [];
        
        var neighbours = TopNeighboursLeftCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    private static List<Plant> BottomNeighboursLeftCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.Y > plants.Y && !p.Neighbours.Any(p2 => p2.X < p.X));
        
        if (neighbour is null) return [];
        
        var neighbours = BottomNeighboursLeftCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    private static List<Plant> BottomNeighboursRightCheck(Plant plants)
    {
        var neighbour = plants.Neighbours.FirstOrDefault(p => p.Y > plants.Y && !p.Neighbours.Any(p2 => p2.X > p.X));
        
        if (neighbour is null) return [];
        
        var neighbours = BottomNeighboursRightCheck(neighbour);
        neighbours.Add(neighbour);
        return neighbours;
    }
    
    private static void FindRegions(List<Plant> plants)
    {
        var index = 1;
        foreach (var plant in plants)
        {
            var left = plants.FirstOrDefault(p=>p.X == plant.X-1 && p.Y == plant.Y && plant.Character == p.Character);
            if (left != null && left.RegionId != 0)
            {
                plant.RegionId = left.RegionId;
                plant.Neighbours.Add(left);
                left.Neighbours.Add(plant);
            }
            
            var top = plants.FirstOrDefault(p=>p.X == plant.X && p.Y == plant.Y-1 && plant.Character == p.Character);
            if (top != null && top.RegionId != 0)
            {
                if (plant.RegionId != 0)
                {
                    top.RegionId = plant.RegionId;
                    UpdateNeighbour(plant.RegionId, top);
                }
                else
                {
                    plant.RegionId = top.RegionId;
                }
                
                plant.Neighbours.Add(top);
                top.Neighbours.Add(plant);
            }
            
            if(plant.RegionId != 0) continue;
            
            plant.RegionId = index++;
        }
    }

    private static void UpdateNeighbour(int regionId, Plant top)
    {
        foreach (var plant in top.Neighbours.Where(p=>p.RegionId != regionId))
        {
            plant.RegionId = regionId;
            UpdateNeighbour(regionId,plant);
        }
    }

    private static List<Plant> InitialiseInput(string[] input)
    {
        var plants = new List<Plant>();
        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y];
            plants.AddRange(row.Select((t, x) => new Plant(x, y, t)));
        }

        return plants;
    }

    private class Plant(int x, int y, char type)
    {
        public List<Plant> Neighbours = [];
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public char Character { get; set; } = type;
        public int RegionId { get; set; }
        public bool TopChecked  { get; set; } = false;
        public bool RightChecked  { get; set; } = false;
        public bool LeftChecked  { get; set; } = false;
        public bool BottomChecked  { get; set; } = false;
        
    }
}