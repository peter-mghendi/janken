using System.Collections.Generic;

namespace Janken.Shared.Models
{
    public interface IEngine
    {
        public IPlayer PlayerOne { get; }

        public IPlayer PlayerTwo { get; }

        public List<string> Choices { get; }

        public IPlayer? Evaluate();
    }
}