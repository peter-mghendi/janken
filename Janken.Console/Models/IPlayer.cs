#nullable enable

using System.Collections.Generic;

namespace Janken.Console.Models
{
    public interface IPlayer
    {
        public string? Choice { get; set; } 

        public string Name { get; set; }

        public string Prompt(List<string> choices);
    }
}