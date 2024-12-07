using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace AdventureOfCode.Day6;
[SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
public class Day6 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);
        
        var result = Part1(input);
        
        Part2(result);
    }

    private void Part2(List<Tile> tiles)
    {
        var count = 0;
        var attempts = 0;
        var tilesToCheck = tiles.Where(tile => tile.X != _startTile.X && tile.Y != _startTile.Y).ToList();
        //find duplicates
        var maxAttempts = TileMap.Count;
        Console.WriteLine($"Options to check: {maxAttempts}");
        var tasks = new List<Task>();
        var startTime = DateTime.Now;
        foreach (var tile in TileMap)
        {
            attempts++;
            var attempt = attempts;
            var task = new Task(() => { CheckLoop(tile, attempt, maxAttempts, ref count); });
            tasks.Add(task);
            task.Start();
        }
        
        
        Task.WhenAll(tasks).Wait();
        
        Console.WriteLine(count);
        
        Console.WriteLine($"Time to check: {(DateTime.Now - startTime).TotalMilliseconds} ms");
    }

    private void CheckLoop(Tile tile, int attempt, int maxAttempts, ref int count)
    {
        var tileToCheck = MakeTileCopy(tile);
        var currentTile = MakeTileCopy(_startTile);
        var currentDirection = Direction.Up;
        var tileMap = MakeTileMapCopy(TileMap);
        tileMap.Find(t => t.X == tileToCheck.X && t.Y == tileToCheck.Y)!.HasObstacle = true;
        do
        {
            var nextTile = GetTile(tileMap, currentTile, currentDirection);

            if (nextTile is null)
            {
                Console.WriteLine($"Attempt: {attempt}/{maxAttempts}. No loop. Traversed {tileMap.Count(x=>x.HasBeenTraversed>0)}");
                break;
            };

            if (!nextTile.HasObstacle)
            {
                nextTile.HasBeenTraversed++;
                if (nextTile.HasBeenTraversed > 5)
                {
                    Console.WriteLine($"Attempt: {attempt}/{maxAttempts}. loop. Traversed {tileMap.Count(x=>x.HasBeenTraversed>0)}");
                    count++;
                    break;
                }

                currentTile = new Tile(nextTile.X, nextTile.Y);
                continue;
            }

            if (currentDirection == Direction.Left)
            {
                currentDirection = Direction.Up;
            }
            else
            {
                currentDirection++;
            }

        } while (true);
    }

    private static readonly List<Tile> TileMap = [];
    private Tile _startTile = new(0,0);
    private List<Tile> Part1(string[] input)
    {
        var currentTile = new Tile(0,0);
        
        for (var y = 0; y < input.Length; y++)
        {
            var horizontal = input[y];
            for (int x = 0; x < horizontal.Length; x++)
            {
                var vertical = horizontal[x];
                var tile = new Tile(x, y, vertical == '#');
                TileMap.Add(tile);
                if (vertical == '^')
                {
                    currentTile = new Tile(tile.X, tile.Y);
                    _startTile = new Tile(tile.X, tile.Y);
                }
            }
        }
        
        var currentDirection = Direction.Up;
        var tileMap = MakeTileMapCopy(TileMap);
        GetTile(tileMap, _startTile.X,_startTile.Y)!.HasBeenTraversed++;
        do
        {
            var nextTile = GetTile(tileMap,currentTile, currentDirection);

            if (nextTile is null) break;
            
            if (!nextTile.HasObstacle)
            {
                nextTile.HasBeenTraversed++;
                currentTile = new Tile(nextTile.X, nextTile.Y);
                continue;
            }

            if (currentDirection == Direction.Left)
            {
                currentDirection = Direction.Up;
            }
            else
            {
                currentDirection++;
            }

        } while (true);

        var traversed = tileMap.Where(t => t.HasBeenTraversed > 0).ToList();
        var count = traversed.Count;
        Console.WriteLine(count);
        return traversed;
    }
    
    private enum Direction
    {
        Up, Right, Down, Left
    }
    private static Tile? GetTile(List<Tile> tileMap, Tile currentTile, Direction direction)
    {
        return direction switch
        {
            Direction.Up => GetTile(tileMap, currentTile.X, currentTile.Y - 1),
            Direction.Down => GetTile(tileMap, currentTile.X, currentTile.Y + 1),
            Direction.Left => GetTile(tileMap, currentTile.X - 1, currentTile.Y),
            Direction.Right => GetTile(tileMap, currentTile.X + 1, currentTile.Y),
            _ => throw null!
        };
    }

    private static Tile? GetTile(List<Tile> tileMap, int x, int y)
    {
        return tileMap.FirstOrDefault(t => t.X == x && t.Y == y)!;
    }


    private class Tile(int x, int y, bool hasObstacle = false)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public bool HasObstacle { get; set; } = hasObstacle;
        public int HasBeenTraversed { get; set; }
    }

    private static List<Tile> MakeTileMapCopy(List<Tile> tileMap)
    {
        return tileMap.Select(MakeTileCopy).ToList();
    }

    private static Tile MakeTileCopy(Tile currentTile)
    {
        return new Tile(currentTile.X, currentTile.Y, currentTile.HasObstacle);
    }
}