using System;
namespace Janken.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EngineAttribute : Attribute
    {
        public string? Name { get; init;}

        public EngineAttribute(string? name = null) => Name = name;
    }
}