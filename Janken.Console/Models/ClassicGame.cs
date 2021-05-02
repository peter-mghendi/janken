#nullable enable

using System;
using System.Collections.Generic;

namespace Janken.Console.Models
{
    public class ClassicGame : IGame
    {
        public IPlayer PlayerOne { get; init; }
        
        public IPlayer PlayerTwo { get; init; }

        public ClassicGame(IPlayer playerOne, IPlayer playerTwo) =>
            (PlayerOne, PlayerTwo) = (playerOne, playerTwo);

        public List<string> Choices => new() { "rock", "paper", "scissors"};

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