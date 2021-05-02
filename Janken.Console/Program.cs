#nullable enable

using System;
using System.Linq;
using Janken.Core;
using Janken.Core.Models;

System.Console.WriteLine("Janken! v1.0.0");

var engine = SelectEngine(SelectPlayer(1), SelectPlayer(2));
var game = new Game(engine);
var result = game.Start();

System.Console.WriteLine($"{engine.PlayerOne.Name} chose {engine.PlayerOne.Choice}!");
System.Console.WriteLine($"{engine.PlayerTwo.Name} chose {engine.PlayerTwo.Choice}!");
System.Console.WriteLine(result);

static IEngine SelectEngine(IPlayer playerOne, IPlayer playerTwo)
{
    var engineList = string.Join('\n', Game.Engines.Select((type, i) => $"{i}: {type.Name}"));
    System.Console.Write($"Pick an engine:\n{engineList}\n\nYour choice: ");
    int choice = Convert.ToInt32(System.Console.ReadLine());
    Console.WriteLine($"Starting game with {Game.Engines[choice].Name.ToLower()} engine.\n");

    return Game.SelectEngine(choice, playerOne, playerTwo);
}

static IPlayer SelectPlayer(int playerNumber)
{
    var playerList = string.Join('\n', Game.Players.Select((type, i) => $"{i}: {type.Name}"));
    System.Console.Write($"Pick a player type for player {playerNumber}:\n{playerList}\n\nYour choice: ");

    int choice = Convert.ToInt32(System.Console.ReadLine());
    System.Console.Write($"Enter a name for {Game.Players[choice].Name.ToLower()} player: ");

    var name = System.Console.ReadLine() ?? string.Empty;
    System.Console.WriteLine($"\n{name} has joined the game!\n");

    return Game.SelectPlayer(choice, name);
}
