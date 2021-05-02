using System.Collections.Generic;
using System.Linq;
using Janken.Console.Utils;
using Janken.Core.Models;

namespace Janken.Console.Players
{
    public partial class Human : IPlayer
    {
        public string Name { get; set; }

        public string? Choice { get; set; }

        public Human(string name) => Name = name;

        public string Prompt(List<string> choices)
        {
            var choiceList = choices.Select((item, i) => $"{i}: {item}").ToList();
            int choice = Input.PromptList($"{Name}, pick an option:", choiceList);
            System.Console.WriteLine();
            return choices[choice];
        }
    }
}