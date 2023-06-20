namespace MergeOptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1,20).ToArray();

            //Producer
            // WithMergeOptions -> when ll the results be available for consuming
            // in baches or as soon as they are made. default is in baches the system decides
            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(x =>
                {
                    var result = Math.Log10(x);
                    Console.WriteLine($"Produced {result}");
                    return result;
                });

            //consumer
            foreach( var result in results )
            {
                Console.WriteLine($"consumed {result}");
            }
        }
    }
}