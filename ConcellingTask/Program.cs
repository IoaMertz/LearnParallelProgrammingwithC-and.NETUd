using System.Linq.Expressions;

namespace ConcellingTask
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            // we can subscribe to an event. When the task is canceled we can call a function
            token.Register(() => {
                Console.WriteLine("Cancelation has been requested");
            });

            // we provide this tokes to the overload of the Task
            var t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    // this is the right way to cancel it
                    //    throw new OperationCanceledException();

                    //    //we can use this to stop or exception
                    //    //Console.WriteLine("skata");
                    //    //break;

                    //}

                    //this is a shortcut of the above code. This is the right way
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            },token);
            //t.Start();

            Task.Factory.StartNew(() =>
            {
                // this waits for a signal to continue in the code bellow.
                // this waithandle in the token waits for the token to be cancelled
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait Handle released, Cancelation was requested");
            });


            //How to make a Task tha answers in many tokes 

            var planned = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();
            
            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(planned.Token,emergency.Token);
            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine(++i);
                    Thread.Sleep(100);
                }
            },paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();

            Console.ReadLine();
            cts.Cancel();

        }
    }
}