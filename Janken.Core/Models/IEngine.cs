using System.Collections.Generic;

namespace Janken.Core.Models
{
    public interface IEngine
    {
        public IPlayer PlayerOne { get; init; }

        public IPlayer PlayerTwo { get; init; }

        public List<string> Choices { get; }

        public IPlayer? Evaluate();
    }
}