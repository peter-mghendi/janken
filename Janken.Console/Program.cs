using System;
using System.Linq;
using Janken.Console.Models;

namespace Janken.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var winner = SelectGame(SelectPlayer(1), SelectPlayer(2)).Start();

            string winnerString = winner switch {
                not null => $"{winner.Name} wins!",
                _ => "It's a tie!"
            };

            System.Console.WriteLine(winnerString);
        }

        private static IGame SelectGame(IPlayer playerOne, IPlayer playerTwo)
        {
            // TODO Reflection/Source Generators
            var games = new[] { "Classic" };
            var gameList = string.Join('\n', games.Select((item, i) => $"{i}: {item}"));
            System.Console.Write($"Pick a game:\n{gameList}\n\nYour choice: ");
            int choice = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine();

            return choice switch {
                0 => new ClassicGame(playerOne, playerTwo),
                _ => throw new Exception("Unrecognized game")
            };
        } 

        private static IPlayer SelectPlayer(int playerNumber) {
            // TODO Reflection/Source Generators
            var players = new[] { "Human", "Computer" };
            var playerList = string.Join('\n', players.Select((item, i) => $"{i}: {item}"));
            System.Console.Write($"Pick a player type for player {playerNumber}:\n{playerList}\n\nYour choice: ");
            int choice = int.Parse(System.Console.ReadLine());
            System.Console.WriteLine();

            IPlayer player = choice switch {
                0 => new HumanPlayer(),
                1 => new ComputerPlayer(),
                _ => throw new Exception("Unrecognized player")
            };

            System.Console.Write($"Enter a name for {players[choice]} player: ");
            player.Name = System.Console.ReadLine();
            System.Console.WriteLine();

            return player;
        }
    }
}
