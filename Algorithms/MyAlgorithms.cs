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
            list.Add(new List<Value>() {new Value(1, 1)});

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

                var vertical = board.Select((ints) => ints[i]);
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

        public static bool ValidateBattlefield(int[,] field)
        {

            int[] ships = new[] {4, 3, 2, 1};
            bool[,] checkField = new bool[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 1 && !checkField[i, j])
                    {
                        int value = CheckingShip(i, j);
                        if (value >= 0 && value < 4)
                        {
                            if (--ships[value] < 0)
                                return false;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }

            if (ships.Any(i => i > 0))
                return false;
            return true;

            int CheckingShip(int i, int j)
            {
                bool down = false, right = false;
                int counter = 0;


                if (i != 9)
                {
                    if (field[i + 1, j] == 1 && !checkField[i + 1, j])
                    {
                        down = true;
                    }
                }

                if (j != 9)
                {
                    if (field[i, j + 1] == 1 && !checkField[i, j + 1])
                    {
                        right = true;
                    }
                }

                if (right && down)
                    return -1;

                if (i < 9)
                {
                    bool check = false;
                    if (j > 0)
                    {
                        check = field[i + 1, j - 1] == 1;
                    }

                    if (j < 9)
                    {
                        check = check || field[i + 1, j + 1] == 1;
                    }

                    if (check)
                    {
                        return -100;
                    }
                }

                if (down)
                {
                    counter += CheckingShipDown(i, j);
                }

                if (right)
                {
                    counter += CheckingShipRight(i, j);
                }

                return counter;
            }

            int CheckingShipRight(int i, int j)
            {
                if ((i > 0 && field[i - 1, j] == 1) ||
                    (i < 9 && field[i + 1, j] == 1) ||
                    (j < 9 && field[i + 1, j + 1] == 1))
                    return -100;

                checkField[i, j] = true;
                return j < 9 && field[i, j + 1] == 1 ? CheckingShipRight(i, j + 1) + 1 : 0;
            }

            int CheckingShipDown(int i, int j)
            {
                if ((j > 0 && field[i, j - 1] == 1) ||
                    (j < 9 && field[i, j + 1] == 1))
                    return -100;

                if (i < 9)
                {
                    bool check = false;
                    if (j > 0)
                    {
                        check = field[i + 1, j - 1] == 1;
                    }

                    if (j < 9)
                    {
                        check = check || field[i + 1, j + 1] == 1;
                    }

                    if (check)
                    {
                        return -100;
                    }
                }

                checkField[i, j] = true;
                return i < 9 && field[i + 1, j] == 1 ? CheckingShipDown(i + 1, j) + 1 : 0;
            }
        }

        public static int[] Snail(int[][] array)
        {
            var result = new List<int>();
            var size = array[0].Length;
            var check = new bool[size, size];
            var (x, y) = (x: 0, y: 0);

            var a = new[] {Array.Empty<int>()};
            
            if (size == 0)
                return new int[0];

            while (CanGoRight())
            {
                Check();
                x++;
                while (!CanGoRight() && CanGoDown())
                {
                    Check();
                    y++;
                    while (!CanGoDown() && CanGoLeft())
                    {
                        Check();
                        x--;
                        while (!CanGoLeft() && CanGoUp())
                        {
                            Check();
                            y--;
                        }
                    }
                }
            }

            Check();

            bool CanGoRight() => x + 1 < size && !check[y, x + 1];
            bool CanGoDown() => y + 1 < size && !check[y + 1, x];
            bool CanGoLeft() => x > 0 && !check[y, x - 1];
            bool CanGoUp() => y > 0 && !check[y - 1, x];

            void Check()
            {
                check[y, x] = true;
                result.Add(array[y][x]);
            }

            return result.ToArray();
        }
    }
}