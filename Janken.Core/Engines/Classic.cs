using System;
using System.Collections.Generic;
using Janken.Shared.Attributes;
using Janken.Shared.Models;

namespace Janken.Core.Engines
{
    [Engine]
    public class Classic : IEngine
    {
        public IPlayer PlayerOne { get; }
        
        public IPlayer PlayerTwo { get; }

        public Classic(IPlayer playerOne, IPlayer playerTwo) =>
            (PlayerOne, PlayerTwo) = (playerOne, playerTwo);

        public List<string> Choices => new() { "Rock", "Paper", "Scissors"};

        private Dictionary<string, string> Table => new ()
        {
            [Choices[0]] = Choices[2],
            [Choices[1]] = Choices[0],
            [Choices[2]] = Choices[1],
        };

        public IPlayer? Evaluate()
        {
            if (PlayerOne.Choice is null || PlayerTwo.Choice is null)
                throw new Exception("A choice is null");

            if (Table[PlayerOne.Choice] == PlayerTwo.Choice)
                return PlayerOne;
            
            if (PlayerOne.Choice == PlayerTwo.Choice)
                return null;

            return PlayerTwo;
        }
    }
}