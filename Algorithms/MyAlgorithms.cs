﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            int[] triple = {0, 1000, 200, 300, 400, 500, 600};
            int[] single = {0, 100, 0, 0, 0, 50, 0};

            for (int i = 1; i <= 6; i++)
            {
                int count = dice.Count(j => j == i);
                score += single[i] * (count % 3) + triple[i] * (count / 3);
            }

            return score;
        }

        public static int LongestSlideDown(int[][] pyramid)
        {
            int result = 0;
            int[][] longestPath = new int[pyramid.Length][];

            for (int i = 0; i < pyramid.Length; i++)
            {
                longestPath[i] = (int[]) pyramid[i].Clone();
            }

            void Dijkstra(int y, int x, int path)
            {
//                for (var i = pyramid.Length - 1; i > 0; i--)
//                for (var k = 0; k < pyramid[i].Length - 1; k++)
//                    pyramid[i - 1][k] += System.Math.Max(pyramid[i][k], pyramid[i][k + 1]);
//
//                return pyramid[0][0];
                path += pyramid[y][x];

                if (path < longestPath[y][x])
                    return;

                longestPath[y][x] = path;

                if (path > result)
                    result = path;

                if (y == pyramid.Length - 1)
                    return;

                Dijkstra(y + 1, x, path);
                Dijkstra(y + 1, x + 1, path);
            }

            Dijkstra(0, 0, 0);

            return result;
        }

        struct Value
        {
            public BigInteger Inc;
            public BigInteger Dec;

            public Value(BigInteger Inc, BigInteger Dec)
            {
                this.Inc = Inc;
                this.Dec = Dec;
            }

            public void Increase(BigInteger Inc)
            {
                this.Inc += Inc;
            }

            public void Decrease(BigInteger Dec)
            {
                this.Dec += Dec;
            }
        }

        public static BigInteger TotalIncDec(int x)
        {
            List<List<Value>> list = new List<List<Value>>();
            list.Add(new List<Value> {new Value(1, 1)});

            List<Value> temp1 = new List<Value>();
            temp1.Add(new Value(1, 1));
            for (int i = 1; i < 10; i++)
            {
                temp1.Add(new Value(0, 1));
            }

            list.Add(temp1);

            List<Value> temp2 = new List<Value>();
            for (int i = 0; i < 10; i++)
            {
                temp2.Add(new Value(10 - i, 1 + i));
            }

            list.Add(temp2);

            BigInteger result = 100;

            for (int i = 3; i <= x; i++)
            {
                List<Value> temp = new List<Value>();
                temp.Add(new Value(result, 1));
                for (int j = 1; j < 10; j++)
                {
                    Value value = new Value();
                    for (int k = j; k < 10; k++)
                    {
                        value.Increase(list[i - 1][k].Inc);
                    }

                    for (int k = j; k >= 0; k--)
                    {
                        value.Decrease(list[i - 1][k].Dec);
                    }

                    temp.Add(value);
                }

                list.Add(temp);
                result = 0;
                foreach (var value in temp)
                {
                    result += value.Dec + value.Inc;
                }
            }

            result = 0;
            foreach (var value in list[x])
            {
                result += value.Dec + value.Inc;
            }

            if (x < 2)
                result -= 1;
            else if (x < 4)
                result -= 10;
            else
                result -= 10 * (x - 2);

            return result;
        }


        public static bool ValidateSolution(int[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                List<int> block = new List<int>();
                int column = i % 3;
                int row = i / 3;
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        block.Add(board[column * 3 + j][row * 3 + k]);
                    }
                }

                var vertical = board.Select(ints => ints[i]);
                for (int j = 0; j < 9; j++)
                {
                    if (board[i].Count(i1 => i1 == j + 1) != 1 ||
                        vertical.Count(i1 => i1 == +1) != 1 ||
                        block.Count(i1 => i1 == j + 1) != 1)
                        return false;
                }
            }

            return true;
        }

        public static List<string> BalancedParens(int n)
        {
            if (n == 0)
                return new List<string> {""};

            List<string> result = new List<string>();

            Function("(", n - 1, n, 1);

            return result;

            void Function(string str, int countOpen, int countClose, int needToClose)
            {
                if (countClose + countOpen > 0)
                {
                    if (countClose > 0 && needToClose > 0)
                        Function(str + ")", countOpen, countClose - 1, needToClose - 1);

                    if (countOpen > 0)
                        Function(str + "(", countOpen - 1, countClose, needToClose + 1);
                }
                else
                {
                    result.Add(str);
                }

            }
        }

        public static List<string> GetPINs(string observed)
        {
            var ableNumbers = new Dictionary<char, string[]>
            {
                {'0', new[] {"0", "8"}},
                {'1', new[] {"1", "2", "4"}},
                {'2', new[] {"1", "2", "3", "5"}},
                {'3', new[] {"2", "3", "6"}},
                {'4', new[] {"1", "4", "5", "7"}},
                {'5', new[] {"2", "4", "5", "6", "8"}},
                {'6', new[] {"3", "5", "6", "9"}},
                {'7', new[] {"4", "7", "8"}},
                {'8', new[] {"5", "7", "8", "9", "0"}},
                {'9', new[] {"6", "8", "9"}}
            };

            var result = new List<string> {""};
            foreach (var number in observed)
            {
                var temp = new List<string>();
                var ables = ableNumbers[number];
                foreach (var code in result)
                {
                    foreach (var able in ables)
                    {
                        temp.Add(code + able);
                    }
                }

                result = temp;
            }

            return result;
        }

        public static string sumStrings(string a, string b)
        {
            BigInteger.TryParse(a, out var na);
            BigInteger.TryParse(b, out var nb);
            return (na + nb).ToString();
        }

        public static long NextSmaller(long n)
        {
            string number = n.ToString();
            var ables = new List<char> {number.Last()};
            int replaceLeftIdx = number.Length - 2;
            while (replaceLeftIdx >= 0)
            {
                char replaceLeft = number[replaceLeftIdx];
                var less = ables.Where(c => c < replaceLeft).ToList();
                if (less.Any())
                {
                    var replaceRight = less.Max();
                    ables.Remove(replaceRight);
                    ables.Add(replaceLeft);
                    var ordered = ables.OrderByDescending(c => c);
                    var result = number.Substring(0, replaceLeftIdx) + replaceRight + string.Join("", ordered);
                    if (result[0] == '0')
                        return -1;
                    return long.Parse(result);
                }

                ables.Add(replaceLeft);
                replaceLeftIdx--;
            }

            return -1;
        }

    }
}