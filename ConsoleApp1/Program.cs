using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Can't do this!");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                var e = new AccessViolationException("Can't access this!");

            });

            Console.WriteLine("Hello, World!");
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
