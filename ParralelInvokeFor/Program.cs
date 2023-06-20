using System.Threading.Channels;

namespace ParralelInvokeFor
{
    internal class Program
    {
        public static IEnumerable<int> Range(int start, int end, int step)
        {
            for(int i =start; i<=end; i += step)
            {
                yield return i;
            }
        }
        static void Main(string[] args)
        {
            var a = new Action(() => {Console.WriteLine($"First {Task.CurrentId}");});
            var b = new Action(() => {Console.WriteLine($"Second {Task.CurrentId}");});
            var c = new Action(() => {Console.WriteLine($"Thrird {Task.CurrentId}");});

            // it does await in all thing it invokes
            Parallel.Invoke( a, b, c );

            Parallel.For(1, 11, i =>
            {
            //Console.WriteLine($"{i * i}\t");
            });

            string[] words = { "oh", "what", "a", "night" };
            Parallel.ForEach(words, word =>
            {
                //Console.WriteLine(word+" has lenght "+word.Length+" "+Task.CurrentId);
            });

            Parallel.ForEach(Range(1,20,3), (num) =>
            {
                Console.WriteLine(num);
            });


            Console.WriteLine("Hello, World!");
        }
    }
}