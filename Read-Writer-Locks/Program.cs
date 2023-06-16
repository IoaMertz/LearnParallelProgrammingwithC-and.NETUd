namespace Read_Writer_Locks
{
    internal class Program
    {
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        static Random random = new Random();
        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();
            for(int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    padlock.EnterReadLock();
                    padlock.EnterReadLock();
                    Console.WriteLine($"Entered read lock, x = {0}");
                    Thread.Sleep(5000);

                    padlock.ExitReadLock();
                    padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x= {x}.");
                }));
            }
            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }
            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine("Write Lock Aquired");
                int newValue = random.Next(10);
                x= newValue;
                Console.WriteLine($"set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine("Write lock released");
            }

            Console.WriteLine("Hello, World!");
        }
    }
}