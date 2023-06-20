namespace Chils_Tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                // if it is set u like this it is called detached.
                //it doesnt matter to the scheduler if it is inside or outside
                var child = new Task(() => {
                    Console.WriteLine("Child task starting.");
                    Thread.Sleep(3000);
                    Console.WriteLine("Child task finishing");
                    throw new Exception();
                    // with this the completion of the parent waits for the child
                },TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s state is {t.Status}");
                    //bitwise operation. ??
                },TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failedHandler = child.ContinueWith(t =>
                {
                    var completionHandler = child.ContinueWith(t =>
                    {
                        Console.WriteLine($"Ops, task {t.Id}'s state is {t.Status}");
                        //bitwise operation. ??
                    }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);
                });
                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }catch (AggregateException ae)
            {
                ae.Handle(e => true);
            }
            Console.WriteLine("Hello, World!");
        }
    }
}