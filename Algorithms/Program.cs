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
            var temp = MyAlgorithms.GetPINs("369");
            Console.WriteLine(string.Join(' ', temp));
        }
    }
}