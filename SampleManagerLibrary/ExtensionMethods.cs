using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SampleManagerLibrary
{
    public static class ExtensionMethods
    {
        public static string SplitCamelCase(this string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    if ((input[i] >= '0' && input[i] <= '9')
                        || (input[i] >= 'A' && input[i] <= 'z'
                            || (input[i] == '.' || input[i] == '_')))
                    {
                        sb.Append(input[i]);
                    }
                }

                return sb.ToString();
            }
        }
    }
}
