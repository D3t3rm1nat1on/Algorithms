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

        struct MyStruct
        {
            public int A;
        }

        static void Main(string[] args)
        {
            var list = MyAlgorithms.BalancedParens(5);
            foreach (var value in list)
            {
                Console.WriteLine(value);
            }
        }
    }
}