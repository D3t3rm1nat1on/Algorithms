using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Algorithms
{
    class Program
    {

        static void Main(string[] args)
        {
            int[][] array =
            {
                new []{1, 2, 3},
                new []{4, 5, 6},
                new []{7, 8, 9}
            };

            int[][] empty =
            {
                
            };

            var newArray = MyAlgorithms.Snail(empty);
            
            foreach (var i in newArray)
            {
                Console.Write($"{i} ");
            }

            Console.WriteLine();

        }
        
    }
}