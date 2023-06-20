namespace CancelationException
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //it return a parallel query
            var items = ParallelEnumerable.Range(1, 20);

            var cts = new CancellationTokenSource();

            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);

                //if(result>1) throw new InvalidOperationException();
                if (result > 1)
                    cts.Cancel();

                Console.WriteLine($"i = {i}, id = {Task.CurrentId}");
                return result;
            });

            try
            {
                foreach(var c in results)
                {
                    if(c>1)
                        cts.Cancel();
                    Console.WriteLine($"result = {c}");
                }
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name} : {e.Message}");
                    return true;
                });
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Canceled");
            }

        }
    }
}