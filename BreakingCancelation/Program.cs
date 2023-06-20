namespace BreakingCancelation
{
    internal class Program
    {

        public static void Demo()
        {
            Parallel.For(0, 20,( int x,ParallelLoopState state) =>
            {
                Console.WriteLine($"{x}, [{Task.CurrentId}]\t");

                if (x == 10)
                {
                    //this lets the task that have arleady started continue
                    //state.Stop(); 

                    state.Break();
                }
            });
        }
        static void Main(string[] args)
        {
            Demo();
            Console.WriteLine("Hello, World!");
        }
    }
}