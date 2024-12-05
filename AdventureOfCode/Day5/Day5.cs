using System.Text.RegularExpressions;

namespace AdventureOfCode.Day5;

public partial class Day5 : IAdventure
{
    public void Run()
    {
        var input = InputReader.Read(this);
        
        Part1(input);
    }

    private void Part1(string[] input)
    {
        var currentLine = 0;

        var pageList = new List<Page>();
        do
        {
            var line = input[currentLine].Trim();
            var match = InputRegex().Match(line);

            if (!match.Success)
            {
                currentLine++;
                continue;
            };
            
            var firstNumber = int.Parse(match.Groups[1].Value);
            var secondNumber = int.Parse(match.Groups[2].Value);
            
            var page = pageList.FirstOrDefault(p => p.Number == secondNumber);
            if (page == null)
            {
                page = new Page(secondNumber);
                pageList.Add(page);
            }
            
            if(page.DependencyContains(firstNumber))
            {
                currentLine++;
                continue;
            }
            
            page.DependencyAdd(firstNumber);
            
            currentLine++;
        } while (input[currentLine].Trim().Length != 0);
        currentLine++;
        var sum = 0;

        var incorrectLines = new List<List<int>>();
        
        do
        {
            var line = input[currentLine].Trim();
            var numbers = line.Split(',').Select(int.Parse).ToList();
            var checkedNumbers = new List<int>();
            var isNotCorrect = false;
            foreach (var number in numbers)
            {
                var page = pageList.FirstOrDefault(p => p.Number == number);
                if (page == null)
                {
                }
                else if (page.AllDependenciesContains(checkedNumbers))
                {
                    checkedNumbers.Add(number);
                }
                else
                {
                    isNotCorrect = true;
                    break;
                }
            }

            if (!isNotCorrect)
            {
                //take the middle value from this numbers list
                sum += numbers[numbers.Count / 2];
            }
            else
            {
                incorrectLines.Add(numbers);
            }
            
            currentLine++;
        } while (currentLine < input.Length);
        
        Console.WriteLine(sum);

        Part2(pageList,incorrectLines);
    }

    private void Part2(List<Page> pageList, List<List<int>> incorrectLines)
    {
        var sum = 0;
        foreach (var line in incorrectLines)
        {
            var newOrder = new List<Page>();
            var pages = line.Select(x => pageList.Find(y => y.Number == x)).ToList();
            foreach (var page in pages)
            {
                if(newOrder.Contains(page!)) continue;
                //recursive as exercise 
                FindProceedingDependencies(page!,line,pageList,newOrder);
                
            }
            
            sum += newOrder[newOrder.Count / 2].Number;
        }
        
        Console.WriteLine(sum);
    }

    public void FindProceedingDependencies(Page page, List<int> line, List<Page> pageList,List<Page> pageOrder)
    {
        var dependencies = page.GetDependencies(line).Select(x=>pageList.Find(y => y.Number == x)!).ToList();
        if (dependencies.Count != 0)
        {
            foreach (var dependency in dependencies)
            {
                //check if the depenncy is already on the PageOrder
                if (!pageOrder.Contains(dependency))
                {
                    FindProceedingDependencies(dependency,line, pageList, pageOrder);
                }
            }
        }
        pageOrder.Add(page);
    }


    public class Page
    {
        public Page(int number)
        {
            Number = number;
        }
        public int Number { get; set; }
        private List<int> Dependency { get; set; } = new();
        public bool DependencyContains(int number) => Dependency.Contains(number);
        public void DependencyAdd(int number) => Dependency.Add(number);
        
        public bool AllDependenciesContains(List<int> numbers) => numbers.All(DependencyContains);
        
        public List<int> GetDependencies(List<int> numbers)
        {
            return numbers.Where(DependencyContains).ToList();
        }
    }
    
    [GeneratedRegex(@"(\d{2})\|(\d{2})")]
    private static partial Regex InputRegex();
}