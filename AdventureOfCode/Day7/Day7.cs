using System.Numerics;

namespace AdventureOfCode.Day7;

public class Day7 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);
        
        Part1(input);
    }

    private void Part1(string[] input)
    {
        BigInteger sum = 0;
        var count = 0;
        foreach (var line in input)
        {
            var inputs = line.Split(" ");
            var goal = long.Parse(inputs[0].Replace(":", ""));
            var numbers = new List<int>();
            for (var i = 1; i < inputs.Length; i++)
            {
                numbers.Add(int.Parse(inputs[i]));
            }
            var firstNumber = numbers.First();
            var nextNumbers = numbers.ToList();
            nextNumbers.RemoveAt(0);
            
            if (CheckMath(nextNumbers.ToList(), goal,Operator.Plus,firstNumber)
                || 
                CheckMath(nextNumbers.ToList(), goal, Operator.Multiply,firstNumber)
                ||
                CheckMath(nextNumbers.ToList(), goal, Operator.Concatenation,firstNumber)
                )
            {
                count++;
                sum += goal;
            }
        }
        
        Console.WriteLine(sum);
        Console.WriteLine(count);
    }

    private bool CheckMath(List<int> numbersToCheck, BigInteger goal, Operator oper, BigInteger currentSum)
    {
        var currentNumber = numbersToCheck[0];
        switch (oper)
        {
            case Operator.Plus:
                currentSum += currentNumber;
                break;
            case Operator.Multiply:
                currentSum *= currentNumber;
                break;
            case Operator.Concatenation:
                string add = currentSum.ToString() + currentNumber;
                currentSum = BigInteger.Parse(add);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(oper), oper, null);
        }

        if (goal < currentSum) return false;

        if (numbersToCheck.Count <= 1)
        {
            return goal == currentSum;
        }

        var nextNumbers = numbersToCheck.ToList();
        nextNumbers.RemoveAt(0);
        
        return CheckMath(nextNumbers.ToList(), goal,Operator.Plus,currentSum) || CheckMath(nextNumbers.ToList(), goal,Operator.Multiply,currentSum) || CheckMath(nextNumbers.ToList(), goal,Operator.Concatenation,currentSum);
    }

    private enum Operator
    {
        Plus,
        Multiply,
        Concatenation
    }
}