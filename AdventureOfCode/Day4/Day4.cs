namespace AdventureOfCode.Day4;

public class Day4 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);

        Part1(input);
        
        Part2(input);
    }


    private void Part1(string[] input)
    {
        var count = 0;

        for (var lineIndex = 0; lineIndex < input.Length; lineIndex++)
        {
            var line = input[lineIndex];
            for (int charIndex = 0; charIndex < line.Length; charIndex++)
            {
                var character = line[charIndex];
                if (character == 'X')
                {
                    //horizontally to right
                    if (charIndex + 3 < line.Length && line[charIndex + 1] == 'M' && line[charIndex + 2] == 'A' &&
                        line[charIndex + 3] == 'S')
                    {
                        count++;
                    }

                    //horizontally to left
                    if (charIndex - 3 >= 0 && line[charIndex - 1] == 'M' && line[charIndex - 2] == 'A' &&
                        line[charIndex - 3] == 'S')
                    {
                        count++;
                    }

                    //vertically down
                    if (lineIndex + 3 < input.Length && input[lineIndex + 1][charIndex] == 'M' &&
                        input[lineIndex + 2][charIndex] == 'A' &&
                        input[lineIndex + 3][charIndex] == 'S')
                    {
                        count++;
                    }

                    //vertically up
                    if (lineIndex - 3 >= 0 && input[lineIndex - 1][charIndex] == 'M' &&
                        input[lineIndex - 2][charIndex] == 'A' &&
                        input[lineIndex - 3][charIndex] == 'S')
                    {
                        count++;
                    }

                    //diagonally left up
                    if (lineIndex - 3 >= 0 && charIndex - 3 >= 0 && input[lineIndex - 1][charIndex - 1] == 'M' &&
                        input[lineIndex - 2][charIndex - 2] == 'A' &&
                        input[lineIndex - 3][charIndex - 3] == 'S')
                    {
                        count++;
                    }

                    //left down
                    if (lineIndex + 3 < input.Length && charIndex - 3 >= 0 &&
                        input[lineIndex + 1][charIndex - 1] == 'M' &&
                        input[lineIndex + 2][charIndex - 2] == 'A' &&
                        input[lineIndex + 3][charIndex - 3] == 'S')
                    {
                        count++;
                    }

                    //right down
                    if (lineIndex + 3 < input.Length && charIndex + 3 < line.Length &&
                        input[lineIndex + 1][charIndex + 1] == 'M' &&
                        input[lineIndex + 2][charIndex + 2] == 'A' &&
                        input[lineIndex + 3][charIndex + 3] == 'S')
                    {
                        count++;
                    }

                    //right up
                    if (lineIndex - 3 >= 0 && charIndex + 3 < line.Length &&
                        input[lineIndex - 1][charIndex + 1] == 'M' &&
                        input[lineIndex - 2][charIndex + 2] == 'A' &&
                        input[lineIndex - 3][charIndex + 3] == 'S')
                    {
                        count++;
                    }
                }
            }
        }

        Console.WriteLine(count);
    }

    private void Part2(string[] input)
    {
        var count = 0;

        for (var lineIndex = 1; lineIndex < input.Length - 1; lineIndex++)
        {
            var line = input[lineIndex];
            for (int charIndex = 1; charIndex < line.Length - 1; charIndex++)
            {
                var character = line[charIndex];

                if (character == 'A')
                {
                    if (input[lineIndex - 1][charIndex - 1] == 'M'
                        && input[lineIndex - 1][charIndex + 1] == 'M'
                        && input[lineIndex + 1][charIndex - 1] == 'S'
                        && input[lineIndex + 1][charIndex + 1] == 'S')
                    {
                        count++;
                    }

                    if (input[lineIndex - 1][charIndex - 1] == 'S'
                        && input[lineIndex - 1][charIndex + 1] == 'S'
                        && input[lineIndex + 1][charIndex - 1] == 'M'
                        && input[lineIndex + 1][charIndex + 1] == 'M')
                    {
                        count++;
                    }
                    
                    if (input[lineIndex - 1][charIndex - 1] == 'S'
                        && input[lineIndex - 1][charIndex + 1] == 'M'
                        && input[lineIndex + 1][charIndex - 1] == 'S'
                        && input[lineIndex + 1][charIndex + 1] == 'M')
                    {
                        count++;
                    }
                    
                    if (input[lineIndex - 1][charIndex - 1] == 'M'
                        && input[lineIndex - 1][charIndex + 1] == 'S'
                        && input[lineIndex + 1][charIndex - 1] == 'M'
                        && input[lineIndex + 1][charIndex + 1] == 'S')
                    {
                        count++;
                    }
                    
                }
            }
        }
        
        Console.WriteLine(count);
    }
}