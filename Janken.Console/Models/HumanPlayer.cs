using System.Collections.Generic;
using System.Linq;

namespace Janken.Console.Models
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }

        public string Prompt(List<string> choices)
        {
            var choiceList = string.Join('\n', choices.Select((item, i) => $"{i}: {item}"));
            System.Console.Write($"{Name}, pick an option:\n{choiceList}\n\nYour choice: ");
            int choice = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine();
            return choices[choice];
        }
    }
}