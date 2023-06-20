namespace BarrierSection
{
    internal class Program
    {
        //multiple threads that need to work in stages p.x
        //all need to finish stage 1
        // we spacifie the number of signals we want to reach before moving on
        // every time the barrier.SignalAndWait() is called 1 is added to the counter.
        static Barrier barrier = new Barrier(2,b =>
        {
            Console.WriteLine($"Phase {b.CurrentPhaseNumber} is finished ");
        });
        public static void Water()
        {
            //phase 0
            Console.WriteLine("Putting the kettle on (takes a bit longer)");
            Thread.Sleep(2000);
            //phase 1
            barrier.SignalAndWait();
            Console.WriteLine("phase number " + barrier.CurrentPhaseNumber); 
            Console.WriteLine("Pouring water into the cup");
            //phase 3
            barrier.SignalAndWait();
            Console.WriteLine("phase number " + barrier.CurrentPhaseNumber); 
            Console.WriteLine("Putting the kettle away");
        }
        public static void Cup()
        {
            Console.WriteLine("Finding the nicest cup (fast)");
            barrier.SignalAndWait();
            Console.WriteLine("phase number " + barrier.CurrentPhaseNumber);
            Console.WriteLine("adding team into the cup");
            barrier.SignalAndWait();
            Console.WriteLine("phase number " + barrier.CurrentPhaseNumber);
            Console.WriteLine("Adding sugar");
        }

        static void Main(string[] args)
        {
            var water = Task.Factory.StartNew(Water);
            var cup = Task.Factory.StartNew(Cup);

            var tea = Task.Factory.ContinueWhenAll(new[] { water,cup}, tasks =>
            {
                Console.WriteLine("Enjoy yor cup of tea");
            } );

            tea.Wait();

            Console.WriteLine("Hello, World!");
        }
    }
}