﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Algorithms
{
    class Program
    {

        struct MyStruct
        {
            public int A;
        }

        static void Main(string[] args)
        {
            var board = new int[][]
            {
                new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
            };
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

                foreach (var VARIABLE in block)
                {
                    Console.Write($"{VARIABLE} ");
                }

                Console.WriteLine();
            }
        }
    }
}