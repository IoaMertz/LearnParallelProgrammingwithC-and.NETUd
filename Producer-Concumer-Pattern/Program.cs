using System.Collections.Concurrent;

namespace Producer_Concumer_Pattern
{
    internal class Program
    {
        //wrapper around another collection you specify in the constructor
        //when you enter the 11th element it will block the one trying to add
        //until a place is available
        static BlockingCollection<int> messages =
            new BlockingCollection<int>(new ConcurrentBag<int>(),10);

        static CancellationTokenSource cts = new CancellationTokenSource();
        static Random random = new Random();

        static void ProduceAndConsume()
        {
            var producer = Task.Factory.StartNew(RunProducer);
            var consumer = Task.Factory.StartNew(RunConsumer);

            try
            {
                Task.WaitAll(new[] { producer, consumer }, cts.Token);
            }
            catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
        }
        static void Main(string[] args)
        {   //Producer_Consumer
            // have a collection that you add items throw a thread and
            // you consume or procces items throw a different thread
            Task.Factory.StartNew(ProduceAndConsume,cts.Token);
            Console.ReadKey();
            cts.Cancel();
            
            Console.WriteLine("Hello, World!");
        }

        private static void RunConsumer()
        {
            foreach (var item in messages.GetConsumingEnumerable()) 
            {
                cts.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"-{item}\t");
                Thread.Sleep(random.Next(1000));
            }

        }

        private static void RunProducer()
        {
            while (true)
            {
                cts.Token.ThrowIfCancellationRequested();
                int i = random.Next(100);
                messages.Add(i);
                Console.WriteLine($"+{i}\t");
                Thread.Sleep(random.Next(100));
            }
        }
    }
}