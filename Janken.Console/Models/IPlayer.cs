using System.Collections.Generic;

namespace Janken.Console.Models
{
    public interface IPlayer
    {
        public string Name { get; set; }

        public string Prompt(List<string> choices);
    }
}