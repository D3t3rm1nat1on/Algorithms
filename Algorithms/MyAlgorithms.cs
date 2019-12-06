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

        public static string FormatDuration(int seconds)
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

        public static List<string> SinglePermutations(string s)
        {
            void Perm(bool[] check, string word, string newWord, List<string> list)
            {
                var newCheck = (bool[]) check.Clone();
                for (int i = 0; i < s.Length; i++)
                {
                    if (newCheck[i])
                        continue;
                    newCheck[i] = true;
                    Perm(newCheck, word, newWord + word[i], list);
                    newCheck[i] = false;

                }

                if (!newCheck.Contains(false))
                    list.Add(newWord);
            }

            bool[] checking = new bool[s.Length];
            for (int i = 0; i < s.Length; i++)
                checking[i] = false;

            List<string> result = new List<string>();

            Perm(checking, s, null, result);

            return result.Distinct().ToList();
        }

        public static int Score(int[] dice)
        {
            int score = 0;
            int[] triple =  {0, 1000, 200, 300, 400, 500, 600};
            int[] single =  {0, 100, 0, 0, 0, 50, 0};
            
            for (int i = 1; i <= 6; i++)
            {
                int count = dice.Count(j => j == i);
                score += single[i] * (count % 3) + triple[i] * (count / 3);                
            }
            
            return score;
        }
    }
}