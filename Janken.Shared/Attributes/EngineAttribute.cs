using System;
namespace Janken.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EngineAttribute : Attribute
    {
        public string? Name { get; }

        public EngineAttribute(string? name = null) => Name = name;
    }
}