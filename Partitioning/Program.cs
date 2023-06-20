using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Concurrent;

namespace Partitioning
{
    public class Program
    {
        [Benchmark]
        public void SquareEachValue() 
        {
            const int count = 1000;
            var values = Enumerable.Range(0, count);
            var results = new int[count];
            Parallel.ForEach(values, x => { results[x] = (int)Math.Pow(x, 2); });
        }

        public void SquareEachValueChunked()
        {
            const int count = 100;
            var values = Enumerable.Range(0, count);
            var results = new int[count];

            var part = Partitioner.Create(0,count,100);
            Parallel.ForEach(part, range =>
            {

            });
        }
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            Console.WriteLine(summary);

        }
    }
}