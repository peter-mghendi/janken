#nullable enable 

using System;
using System.Collections.Generic;
using System.Linq;
using Janken.Core.Models;

namespace Janken.Console.Players
{
    // TODO: Should each interface implement its own HumanPlayer?
    public partial class Human : IPlayer
    {
        public string Name { get; set; }

        public string? Choice { get; set; }

        public Human(string name) => Name = name;

        public string Prompt(List<string> choices)
        {
            var choiceList = string.Join('\n', choices.Select((item, i) => $"{i}: {item}"));
            System.Console.Write($"{Name}, pick an option:\n{choiceList}\n\nYour choice: ");
            int choice = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine();
            return choices[choice];
        }
    }
}