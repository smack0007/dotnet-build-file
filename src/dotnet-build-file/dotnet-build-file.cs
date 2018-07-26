using System;

namespace DotnetBuildFile
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("dotnet-build-file");

            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
        }
    }
}