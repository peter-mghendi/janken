using System;
using System.Collections.Generic;
using System.Linq;
using Janken.Core.Models;

namespace Janken.Core
{
    public class Game
    {
        // TODO Source Generators
        public static readonly List<Type> Engines = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();

        // TODO Source Generators
        public static readonly List<Type> Players = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IPlayer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();

        private readonly IEngine _engine;

        public Game(IEngine engine) => _engine = engine;

        public static IEngine SelectEngine(int i, IPlayer playerOne, IPlayer playerTwo) =>
            (Activator.CreateInstance(Engines[i], args: new[] { playerOne, playerTwo }) as IEngine)!;


        public static IPlayer SelectPlayer(int i, string name) =>
            (Activator.CreateInstance(Players[i], args: name) as IPlayer)!;

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