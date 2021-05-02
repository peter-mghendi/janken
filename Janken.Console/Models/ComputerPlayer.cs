#nullable enable

using System;
using System.Collections.Generic;

namespace Janken.Console.Models
{
    public class ComputerPlayer : IPlayer
    {
        public string Name { get; set; }

        public string? Choice { get; set; }

        public ComputerPlayer(string name) => Name = name;

        public string Prompt(List<string> choices)
        {
            var random = new Random();
            int choice = random.Next(0, choices.Count - 1);
            return choices[choice];
        }
    }
}