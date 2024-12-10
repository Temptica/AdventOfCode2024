using System.Numerics;

namespace AdventureOfCode.Day9;

public class Day9 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);
        
        //Part1(input);
        
        Part2(input);
    }

    private void Part2(string[] input)
    {
        var files = new List<int>();
        var spaces = new List<int>();
        for (var i = 0; i < input[0].Length; i++)
        {
            if (i % 2 == 0)
            {
                files.Add(int.Parse(input[0][i].ToString()));
            }
            else
            {
                spaces.Add(int.Parse(input[0][i].ToString()));
            }
        }

        var filesCopy = files.ToList();
        var spacesCopy = spaces.ToList();

        var count = 0;
        var result = new List<int>();
        do
        {
            if (filesCopy.Count>count)
            {
                var fileCount = filesCopy[count];
            
                for (var i = 0; i < fileCount; i++)
                {
                    result.Add(count);
                }
            }

            if (spacesCopy.Count > count)
            {
                var spaceCount = spacesCopy[count];

                for (var i = 0; i < spaceCount; i++)
                {
                    result.Add(-1);
                }
            }
            count++;
        }while (count <= filesCopy.Count && count <= spacesCopy.Count);

        var lastIndex = 1;
        
        do
        {
            if (lastIndex >= result.Count) // if index is too high
            {
                break;
            }
            
            var last = result[^lastIndex];
            
            if (last == -1) //if it is a -1, replace with -2
            {
                result[^lastIndex]= -2;
                last = -2;
            }

            if (last == -2)
            {
                lastIndex++;
                continue;
            }
            
            //if no -1 remaining 
            if (result.All(i => i != -1)) break;
            
            var amount = GetCountOfProceedingNumber(result, last, lastIndex-1); //get amount of proceeding of "last's" value
            var index = 0;
            var antiLoopProtection = result.Count;
            while (true)
            {
                var firstInstance = result.IndexOf(-1,index);
                if (firstInstance == -1)
                {
                    index = 0;
                    break;
                }
                var negativeFound = GetCountOfLeadingNumber(result, firstInstance); //find how many leading -1's are found
                if (negativeFound >= amount) 
                {
                    index = firstInstance;
                    break;
                }

                if (negativeFound == 0)
                {
                    index = 0;
                    break;
                }

                index += negativeFound;
                antiLoopProtection--;
                if (antiLoopProtection == 0)
                {
                    index = 0;
                    break;
                }
            }
            if (antiLoopProtection == 0 ) continue;

            if (index == 0 || index == result.Count - amount- lastIndex+1)
            {
                lastIndex += amount;
                continue;
            }
           
            //move last 'amount' of the back to 'index' and reace index and next 'amount' with 'last'
            for (var i = 0; i < amount; i++)
            {
                result[index+i] = last;
                result[^(lastIndex+i)] = -2;
            }
            
            //result.RemoveRange(result.Count - amount - lastIndex+1, amount);
            //replace old positions with -2
        } while (true);
        
        Console.WriteLine(string.Join("", result));
        
        
        BigInteger sum = 0;
        for (var index = 0; index < result.Count; index++)
        {
            var value = result[index];
            if(value == -2) continue;
            sum += value*index;
        }
        Console.WriteLine("final result:");
        Console.WriteLine(sum);
    }

    private int GetCountOfProceedingNumber(List<int> result, int last, int startIndex)
    {
        var copy = result.ToList();
        copy.Reverse();
        
        var count = 1;
        while (true)
        {
            if (copy.Count <= startIndex)
            {
                break;
            }
            if (copy[count+startIndex] == last)
            {
                count++;
                continue;
            }
            break;
        }
        return count;
    }

    private int GetCountOfLeadingNumber(List<int> result, int startIndex)
    {
        var index = startIndex;
        var count = 0;

        while (true)
        {
            if(index >= result.Count-1 || index <= -1) break;

            if (result[index] == -1)
            {
                count++;
                index++;
                continue;
            }

            break;
        }
        
        return count;
    }

    private void Part1(string[] input)
    {
        var files = new List<int>();
        var spaces = new List<int>();
        for (var i = 0; i < input[0].Length; i++)
        {
            if (i % 2 == 0)
            {
                files.Add(int.Parse(input[0][i].ToString()));
            }
            else
            {
                spaces.Add(int.Parse(input[0][i].ToString()));
            }
        }

        var filesCopy = files.ToList();
        var spacesCopy = spaces.ToList();

        var count = 0;
        var result = new List<int>();
        do
        {
            if (filesCopy.Count>count)
            {
                var fileCount = filesCopy[count];
            
                for (var i = 0; i < fileCount; i++)
                {
                    result.Add(count);
                }
            }

            if (spacesCopy.Count > count)
            {
                var spaceCount = spacesCopy[count];

                for (var i = 0; i < spaceCount; i++)
                {
                    result.Add(-1);
                }
            }
            count++;
        }while (count <= filesCopy.Count && count <= spacesCopy.Count);
        
        do
        {
            var last = result[^1];
            if (last == -1)
            {
                result.RemoveAt(result.Count - 1);
                continue;
            }
            
            var firstDot = result.IndexOf(-1);
            if (firstDot < 0) break;
            //replace at index 
            result[firstDot] = last;
            result.RemoveAt(result.Count - 1);
            count++;
            
        } while (true);
        Console.WriteLine("second result:");
        Console.WriteLine(result);
        
        BigInteger sum = 0;
        for (var index = 0; index < result.Count; index++)
        {
            var file = result[index];
            sum += file*index;
        }
        Console.WriteLine("final result:");
        Console.WriteLine(sum);
    }
    
            
}