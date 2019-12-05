using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> list = new List<string>();
            Console.WriteLine(MyAlgorithms.FormatDuration(3662));
            foreach (var l in MyAlgorithms.SinglePermutations("aabb"))
            {
                Console.WriteLine(l);
            }
            
        }
    }
}