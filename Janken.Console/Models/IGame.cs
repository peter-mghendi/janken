#nullable enable

using System.Collections.Generic;

namespace Janken.Console.Models
{
    public interface IGame
    {
        public IPlayer PlayerOne { get; init; }

        public IPlayer PlayerTwo { get; init; }

        public List<string> Choices { get; }

        public IPlayer? Evaluate();
    }
}