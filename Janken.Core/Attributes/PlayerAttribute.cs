using System;
namespace Janken.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PlayerAttribute : Attribute
    {
        public string? Name { get; init;}

        public PlayerAttribute(string? name = null) => Name = name;
    }
}