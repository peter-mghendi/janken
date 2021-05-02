using System.Collections.Generic;

namespace Janken.Console.Utils
{
    public static class Input
    {
        private const string errorPrompt = "ERROR! Invalid input. Try again.\n> ";

        public static int PromptList(string prompt, List<string> list) =>
            ReadInt($"{prompt}\n{string.Join('\n', list)}\n> ", 0, list.Count - 1);

        public static string PromptString(string prompt, bool allowEmpty = false) =>
            ReadString(prompt, allowEmpty);

        private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue) 
        {
            System.Console.Write(prompt);
            return (!int.TryParse(System.Console.ReadLine(), out int output) || output < min || output > max) 
                ? ReadInt(errorPrompt, min, max)
                : output;
        }

        private static string ReadString(string prompt, bool allowEmpty) 
        {
            System.Console.Write(prompt);
            var input = System.Console.ReadLine() ?? string.Empty;
            return (!allowEmpty && string.IsNullOrWhiteSpace(input)) 
                ? ReadString(errorPrompt, allowEmpty)
                : input;
        }
    }
}


