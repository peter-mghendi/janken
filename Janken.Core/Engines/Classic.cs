using System;
using System.Collections.Generic;
using Janken.Core.Models;

namespace Janken.Core.Engines
{
    public class Classic : IEngine
    {
        public IPlayer PlayerOne { get; init; }
        
        public IPlayer PlayerTwo { get; init; }

        public Classic(IPlayer playerOne, IPlayer playerTwo) =>
            (PlayerOne, PlayerTwo) = (playerOne, playerTwo);

        public List<string> Choices => new() { "Rock", "Paper", "Scissors"};

        private Dictionary<string, string> Table => new ()
        {
            [Choices[1]] = Choices[0],
            [Choices[2]] = Choices[1],
            [Choices[0]] = Choices[2],
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