using System;
namespace Janken.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PlayerAttribute : Attribute
    {
        public string? Name { get; }

        public PlayerAttribute(string? name = null) => Name = name;
    }
}