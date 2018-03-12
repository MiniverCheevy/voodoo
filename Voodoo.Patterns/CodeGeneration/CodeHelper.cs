using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.CodeGeneration
{
    public static class CodeHelper
    {
        public static string Format(string code)
        {
            var response = new StringBuilder();
            var lines = code.Split((char) 13);
            var indent = 0;
            var lastWasBlank = false;
            var lastWasOpen = false;
            var lastLine = string.Empty;
            foreach (var line in lines)
            {
                var formatted = line.Trim();
                var thisIsBlank = String.IsNullOrWhiteSpace(formatted);
                var isOpen = formatted == "{";
                if ((thisIsBlank && lastWasBlank) || (lastWasOpen && thisIsBlank))
                    continue;
                if (isOpen)
                {
                    response.AppendLine(addIndent(formatted, indent));
                    indent += 4;
                }
                else if (formatted.StartsWith("}") && indent - 4 >= 0)
                {
                    indent -= 4;
                    response.AppendLine(addIndent(formatted, indent));
                }
                else if (formatted.StartsWith("."))
                {
                    var last = lastLine.IndexOf(".");
                    response.AppendLine(addIndent(formatted, indent + last));
                }
                else
                    response.AppendLine(addIndent(formatted, indent));

                lastWasBlank = thisIsBlank;
                lastWasOpen = isOpen;
                lastLine = formatted;
            }

            return response.ToString();
        }

        private static string addIndent(string formatted, int indent)
        {
            const char singleSpace = ' ';
            var padding = new string(singleSpace, indent);
            return $"{padding}{formatted}";
        }
    }
}