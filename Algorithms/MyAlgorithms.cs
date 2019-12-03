using System;
using System.Collections.Generic;
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

        public static string FirstNonRepeatingLetter(string s)
        {
            var str = s.Where(c => s.Count(c1 => char.ToLower(c1) == char.ToLower(c)) == 1).ToList();
            return str.Count > 0 ? str.First().ToString() : "";
        }

        public static string GetReadableTime(int seconds)
        {
            return $"{seconds / 60 / 60:D2}:{seconds / 60 % 60:D2}:{seconds % 60:D2}";
        }

        public static string High(string s)
        {
            return s.Split().OrderByDescending(s1 => s1.Select(Convert.ToInt32).Sum(i => i - 'a' + 1)).First();
        }

        public static string ExpandedForm(long num)
        {
            string res = null;
            string temp = num.ToString();
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != '0')
                {
                    res += $"{temp[i]}{new string('0', temp.Length - i - 1)} ";
                }

            }

            return string.Join(" + ", res.Trim().Split());
        }

        public static string formatDuration(int seconds)
        {
            var result = new List<string>();
            var minutes = TimeSpan.FromSeconds(seconds).Minutes;
            var hours = TimeSpan.FromSeconds(seconds).Hours;
            var days = TimeSpan.FromSeconds(seconds).Days % 365;
            var years = TimeSpan.FromSeconds(seconds).Days / 365;
            seconds %= 60;
            
            if (years > 0) result.Add(years == 1 ? "1 year" : $"{years} years");
            if (days > 0) result.Add(days == 1 ? "1 day" : $"{days} days");
            if (hours > 0) result.Add(hours == 1 ? "1 hour" : $"{hours} hours");
            if (minutes > 0) result.Add(minutes == 1 ? "1 minute" : $"{minutes} minutes");
            if (seconds > 0) result.Add(seconds == 1 ? "1 second" : $"{seconds} seconds");
            
            var count = result.Count;
            if (count == 0) return "now";
            if (count <= 1) return string.Join(", ", result);
            result[count - 2] += $" and {result.Last()}";
            result.RemoveAt(count - 1);
            return string.Join(", ", result);
        }
    }
}