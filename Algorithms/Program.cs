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
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{i} ==> {MyAlgorithms.TotalIncDec(i)}");
            }
        }
    }
}