using System;
using System.Collections.Generic;

namespace ConsoleApplicatons
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = new List<int>()
            {
                0,1,2,3
            };
            Console.WriteLine(nums.Find(x=>x==0));
        }
    }
}
