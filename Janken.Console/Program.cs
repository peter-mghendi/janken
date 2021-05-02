using System.Reflection.Metadata;
using System;
using System.Linq;
using Janken.Console.Models;


var game = SelectGame(SelectPlayer(1), SelectPlayer(2));

// TODO Abstract this away properly
game.PlayerOne.Choice = game.PlayerOne.Prompt(game.Choices);
game.PlayerTwo.Choice = game.PlayerTwo.Prompt(game.Choices);

System.Console.WriteLine($"{game.PlayerOne.Name} chose {game.PlayerOne.Choice}!");
System.Console.WriteLine($"{game.PlayerTwo.Name} chose {game.PlayerTwo.Choice}!");

string winnerString = game.Evaluate() switch
{
    IPlayer player => $"{player.Name} wins!",
    _ => "It's a tie!"
};

System.Console.WriteLine(winnerString);


static IGame SelectGame(IPlayer playerOne, IPlayer playerTwo)
{
    // TODO Reflection/Source Generators
    var games = new[] { "Classic" };
    var gameList = string.Join('\n', games.Select((item, i) => $"{i}: {item}"));
    System.Console.Write($"Pick a game:\n{gameList}\n\nYour choice: ");
    int choice = int.Parse(System.Console.ReadLine());
    System.Console.WriteLine();

    return choice switch
    {
        0 => new ClassicGame(playerOne, playerTwo),
        _ => throw new Exception("Unrecognized game")
    };
}

static IPlayer SelectPlayer(int playerNumber)
{
    // TODO Reflection/Source Generators
    var players = new[] { "Human", "Computer" };
    var playerList = string.Join('\n', players.Select((item, i) => $"{i}: {item}"));
    System.Console.Write($"Pick a player type for player {playerNumber}:\n{playerList}\n\nYour choice: ");
    int choice = int.Parse(System.Console.ReadLine());
    System.Console.WriteLine();

    System.Console.Write($"Enter a name for {players[choice]} player: ");
    var name = System.Console.ReadLine();
    System.Console.WriteLine();

    return choice switch
    {
        0 => new HumanPlayer(name),
        1 => new ComputerPlayer(name),
        _ => throw new Exception("Unrecognized player")
    };
}
