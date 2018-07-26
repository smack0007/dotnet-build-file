using System;

namespace Foo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Foo!");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }   
}