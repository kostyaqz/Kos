using System;
using System.Globalization;
using NUnit.Framework;

namespace ConsoleApp1
{
    class Experiments1
    {
        //[Test]
        class Sample2
        {
            static string who = "class";

            static void F()
            {
                string who = "F";
            }

            static void G()
            {
                F();
                Console.WriteLine(who);
            }

            static void H()
            {
                string who = "H";
                F();
                Console.Write(who);
            }
        }

        static void Main()
        {
    
        }
    }
}
