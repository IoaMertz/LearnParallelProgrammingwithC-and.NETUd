namespace ManualResetEventSlimSection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // when the evt.wait is called it is set to true
            var evt = new ManualResetEventSlim();
             // this is automatically reset to false after .wait
            var evt2 = new AutoResetEvent(false);



            Task.Factory.StartNew(() => {
                Console.WriteLine("boiling water ");
                evt.Set(); 
            });

            var makeTea = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Waiting for water ...");
                evt.Wait();
                Console.WriteLine("here is the tea");
            });
            makeTea.Wait();
            Console.WriteLine("Hello, World!");
        }
    }
}