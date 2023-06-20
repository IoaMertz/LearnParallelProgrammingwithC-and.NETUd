namespace CountDownEventSection
{
    internal class Program
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        static Random random = new Random();
        static void Main(string[] args)
        {
            for(int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal();
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var finalTask = Task.Factory.StartNew(() => {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                //waiting for the countdown to reach 0
                cte.Wait();
                Console.WriteLine("all tasks completed from " + Task.CurrentId);

            });
            finalTask.Wait();

            Console.WriteLine("Hello, World!");
        }
    }
}