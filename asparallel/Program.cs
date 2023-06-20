namespace asparallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
             int count = 50;
            var items = Enumerable.Range(1, count).ToArray();
            var results = new int[count];
            items.AsParallel().ForAll(x => 
            {
                int newValue = x * x * x;
                Console.WriteLine($"{newValue} ({Task.CurrentId})\t");

                results[x - 1] = newValue;

            });
            //items.ToList().ForEach(x => { Console.WriteLine(x); });
            //Console.WriteLine(); 

            var cubes = items.AsParallel().AsOrdered().Select(x => x * x * x);
        }
    }
}