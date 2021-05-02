using System.Collections.Generic;

namespace Janken.Core.Models
{
    public interface IPlayer
    {
        public string? Choice { get; set; } 

        public string Name { get; set; }

        public string Prompt(List<string> choices);
    }
}