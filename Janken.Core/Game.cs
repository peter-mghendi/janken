using System;
using System.Collections.Generic;
using System.Linq;
using Janken.Core.Models;

using Engine = Janken.Core.Attributes.EngineAttribute;
using Player = Janken.Core.Attributes.PlayerAttribute;

namespace Janken.Core
{
    public class Game
    {
        private readonly IEngine _engine;

        public Game(IEngine engine) => _engine = engine;

        // TODO Source Generators
        private static readonly List<Type> _engineList = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IEngine).IsAssignableFrom(x)
                && x.GetCustomAttributes(typeof(Engine), true).Any()
                && !x.IsInterface
                && !x.IsAbstract)
            .ToList();

        // TODO Source Generators
        public static readonly List<string> Engines = _engineList
           .Select(x => ((Engine)Attribute.GetCustomAttribute(x, typeof(Engine))!)
               .Name ?? x.Name)
           .ToList();

        // TODO Source Generators
        private static readonly List<Type> _playerList = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IPlayer).IsAssignableFrom(x)
                && x.GetCustomAttributes(typeof(Player), true).Any()
                && !x.IsInterface
                && !x.IsAbstract)
            .ToList();

        // TODO Source Generators
        public static readonly List<string> Players = _playerList
           .Select(x => ((Player)Attribute.GetCustomAttribute(x, typeof(Player))!)
               .Name ?? x.Name)
           .ToList();

        // TODO Source Generators
        public static IEngine SelectEngine(int i, IPlayer playerOne, IPlayer playerTwo) =>
            (Activator.CreateInstance(_engineList[i], args: new[] { playerOne, playerTwo }) as IEngine)!;

        // TODO Source Generators
        public static IPlayer SelectPlayer(int i, string name) =>
            (Activator.CreateInstance(_playerList[i], args: name) as IPlayer)!;

        public string Start()
        {
            _engine.PlayerOne.Choice = _engine.PlayerOne.Prompt(_engine.Choices);
            _engine.PlayerTwo.Choice = _engine.PlayerTwo.Prompt(_engine.Choices);
            return _engine.Evaluate() switch
            {
                IPlayer player => $"{player.Name} wins!",
                _ => "It's a tie!"
            };
        }
    }
}