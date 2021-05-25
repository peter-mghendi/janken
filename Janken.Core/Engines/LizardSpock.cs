using System;
using System.Collections.Generic;
using Janken.Shared.Attributes;
using Janken.Shared.Models;

namespace Janken.Core.Engines
{
    [Engine(name: "Rock, Paper, Scissors, Lizard, Spock")]
    public class LizardSpock : IEngine
    {
        public IPlayer PlayerOne { get; }

        public IPlayer PlayerTwo { get; }

        public LizardSpock(IPlayer playerOne, IPlayer playerTwo) =>
            (PlayerOne, PlayerTwo) = (playerOne, playerTwo);

        public List<string> Choices => new() { "Rock", "Paper", "Scissors", "Lizard", "Spock" };

        private Dictionary<string, List<string>> Table => new()
        {
            [Choices[0]] = new(){ Choices[2], Choices[3] }, // Scissors, Lizard
            [Choices[1]] = new(){ Choices[0], Choices[4] }, // Rock, Spock
            [Choices[2]] = new(){ Choices[1], Choices[3] }, // Paper, Lizard
            [Choices[3]] = new(){ Choices[1], Choices[4] }, // Paper, Spock
            [Choices[4]] = new(){ Choices[0], Choices[2] }, // Rock, Scissors
        };


        public IPlayer? Evaluate()
        {
            if (PlayerOne.Choice is null || PlayerTwo.Choice is null)
                throw new Exception("A choice is null");

            if (Table[PlayerOne.Choice].Contains(PlayerTwo.Choice))
                return PlayerOne;

            if (PlayerOne.Choice == PlayerTwo.Choice)
                return null;

            return PlayerTwo;
        }
    }
}