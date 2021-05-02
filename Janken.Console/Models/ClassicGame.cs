#nullable enable

using System.Collections.Generic;

namespace Janken.Console.Models
{
    public class ClassicGame : IGame
    {
        public IPlayer PlayerOne { get; init; }
        
        public IPlayer PlayerTwo { get; init; }

        public ClassicGame(IPlayer playerOne, IPlayer playerTwo) =>
            (PlayerOne, PlayerTwo) = (playerOne, playerTwo);

        private List<string> Choices => new() { "rock", "paper", "scissors"};

        private Dictionary<string, string> Table => new ()
        {
            [Choices[1]] = Choices[0],
            [Choices[2]] = Choices[1],
            [Choices[0]] = Choices[2],
        };

        public IPlayer? Start()
        {
            var playerOneChoice = PlayerOne.Prompt(Choices);
            var playerTwoChoice = PlayerTwo.Prompt(Choices);

            System.Console.WriteLine($"{PlayerOne.Name} chose {playerOneChoice}!");
            System.Console.WriteLine($"{PlayerTwo.Name} chose {playerTwoChoice}!");

            if (Table[playerOneChoice] == playerTwoChoice)
                return PlayerOne;
            
            if (playerOneChoice == playerTwoChoice)
                return null;

            return PlayerTwo;
        }
    }
}