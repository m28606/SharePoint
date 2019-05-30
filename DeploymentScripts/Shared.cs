using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentScripts
{
    public static class Shared
    {
        public static string ReplaceLastOccurrenceString(string source, string find, string replace)
        {
            int place = source.LastIndexOf(find, System.StringComparison.Ordinal);
            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }
        
        public enum MessageOptions
        {
            Success,
            Error,
            //Ajinkya Korade: 29-01-2016: Added Title Enum.
            Title,
            Info
        }

        public static void WriteMessage(string message, MessageOptions option)
        {

            switch (option)
            {
                case MessageOptions.Success:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case MessageOptions.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                //Ajinkya Korade: 29-01-2016: Added Title case.
                case MessageOptions.Title:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case MessageOptions.Info:
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
