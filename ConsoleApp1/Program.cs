using FYProject1Classes.UserMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomNoGenerator ra = new RandomNoGenerator();
            string pass = ra.RandomPassword();
            Console.WriteLine($"Random String of 6 chars is {pass}");
            Console.ReadKey();
        }
    }
}
