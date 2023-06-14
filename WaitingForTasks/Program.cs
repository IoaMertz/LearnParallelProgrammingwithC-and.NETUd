namespace WaitingForTasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            
            var token = cts.Token;

            var t = new Task(() => {
                Console.WriteLine("I Take 5 secs");
                for(int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000); 
                }
                Console.WriteLine("I am Done t1");
            },token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => { Thread.Sleep(3000); Console.WriteLine("I am done t2"); }, token) ;

            //// waits for all tasks or takes as arguments an array of tasks we want to wait
            //Task.WaitAll();

            //waits for the task to finish. Overload sees the token and stops waiting if it sees that is canceled
            //t.Wait(token);

            //Task.WaitAll(t, t2);

            ////waits only for one . When the first of the 2 is finished it stops waiting
            //Task.WaitAny(t,t2);

            //Console.ReadKey();
            //cts.Cancel();


            // waits for the tasks but only for 4000 secs.
            // If the token is canceled WaitAll() throws an exception and the programm crashes
            // so it must be handled.
            Task.WaitAll(new[] { t,t2},4000,token);

            Console.WriteLine($"Task t status in {t.Status}"); //Running
            Console.WriteLine($"Task t2 status in {t2.Status}");// RanToCompletion




            Console.WriteLine("Hello, World!");
            //Console.ReadLine();
        }
    }
}