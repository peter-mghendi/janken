using System;
using System.Linq;
using Figgle;
using Janken.Console.Utils;
using Janken.Core;
using Janken.Core.Models;

System.Console.WriteLine(FiggleFonts.Standard.Render("Janken!") + "Janken! v1.0.0\n---");

var engine = SelectEngine(SelectPlayer(1), SelectPlayer(2));
var game = new Game(engine);
var result = game.Start();

System.Console.WriteLine($"{engine.PlayerOne.Name} chose {engine.PlayerOne.Choice}!");
System.Console.WriteLine($"{engine.PlayerTwo.Name} chose {engine.PlayerTwo.Choice}!");
System.Console.WriteLine(result);

static IEngine SelectEngine(IPlayer playerOne, IPlayer playerTwo)
{
    var engineList = Game.Engines.Select((type, i) => $"{i}: {type.Name}").ToList();
    int choice = Input.PromptList($"Pick an engine:", engineList);
    Console.WriteLine($"\nStarting game with {Game.Engines[choice].Name.ToLower()} engine.\n");
    return Game.SelectEngine(choice, playerOne, playerTwo);
}

static IPlayer SelectPlayer(int playerNumber)
{
    var playerList = Game.Players.Select((type, i) => $"{i}: {type.Name}").ToList();
    int choice = Input.PromptList($"Pick a player type for player {playerNumber}:", playerList);
    var name = Input.PromptString($"Enter a name for {Game.Players[choice].Name.ToLower()} player: ");
    System.Console.WriteLine($"\n{name} has joined the game!\n");
    return Game.SelectPlayer(choice, name);
}
