using System.Linq;
using System.Text.RegularExpressions;

namespace Algorithms
{
    public static class MyAlgorithms
    {
        public static int CountSmileys(string[] smileys) 
        {
            return smileys.Count(s => Regex.IsMatch(s, @"^[:;]{1}[-~]{0,1}[)D]{1}$"));
        }
    }
}