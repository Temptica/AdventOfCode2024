using System.Text.RegularExpressions;

namespace AdventureOfCode.Day3;

public partial class Day3 : IAdventure
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don't\(\)")]
    private static partial Regex DontRegex();

    public void Run()
    {
        var input = InputReader.Read(this);
        var joined = string.Join(' ', input);
        Part1(joined);
        Part2(joined);
    }

    private static void Part1(string input)
    {
        var matches = MulRegex().Matches(input);
        var count = 0;
        var foundCount = 0;
        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var mul = int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            count += mul;
            foundCount++;
        }

        Console.WriteLine(count);
    }

    private void Part2(string input)
    {
        var doMatches = DoRegex().Matches(input);
        var dontMatches = DontRegex().Matches(input);
        var charCount = input.Length;
        var mulZones = new List<MulZone>();

        while (true)
        {
            if (mulZones.Count == 0)
            {
                mulZones.Add(new MulZone(0, dontMatches[0].Index, true));
                continue;
            }

            var lastMul = mulZones[^1];

            if (lastMul.MulEnabled)
            {
                var enableMatches = doMatches.Where(m => m.Index > lastMul.EndIndex).ToList();
                if (enableMatches.Count == 0) break;
                mulZones.Add(new MulZone(lastMul.EndIndex, enableMatches[0].Index, false));
                continue;
            }

            var disableMatches = dontMatches.Where(m => m.Index > lastMul.EndIndex).ToList();
            if (disableMatches.Count == 0) break;
            mulZones.Add(new MulZone(lastMul.EndIndex, disableMatches[0].Index, true));
        }

        mulZones.Add(new MulZone(mulZones[^1].EndIndex, input.Length - 1, !mulZones[^1].MulEnabled));

        Console.WriteLine($"Found {mulZones.Count} mul zones");

        var matches = MulRegex().Matches(input);
        var count = 0;
        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var currentMulZone = mulZones.Single(mz => mz.StartIndex <= match.Index && mz.EndIndex >= match.Index);
            if (!currentMulZone.MulEnabled) continue;
            var mul = int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            count += mul;
        }

        Console.WriteLine(count);
    }

    private record MulZone(int StartIndex, int EndIndex, bool MulEnabled);
}