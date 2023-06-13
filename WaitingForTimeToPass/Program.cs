namespace WaitingForTimeToPass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                // it doesnt only stops but tells the thread that it can take up another task
                //Thread.Sleep(1000);

                //Thread.SpinWait(1000);

                // it pauses but does not give up its place. The sceduler wont start another task.
                //SpinWait.SpinUntil();

                Console.WriteLine("Press any key to dismarn you have 5 seconds");
                bool canceled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(canceled ? "Bombed Disarmed" : "BOOM!!");
            },token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}