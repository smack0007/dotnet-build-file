using System;

namespace Foo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Bar!");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }   
}