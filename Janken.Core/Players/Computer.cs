using System;
using System.Collections.Generic;
using Janken.Core.Models;

namespace Janken.Core.Players
{
    public class Computer : IPlayer
    {
        public string Name { get; set; }

        public string? Choice { get; set; }

        public Computer(string name) => Name = name;

        public string Prompt(List<string> choices)
        {
            var random = new Random();
            int choice = random.Next(0, choices.Count - 1);
            return choices[choice];
        }
    }
}