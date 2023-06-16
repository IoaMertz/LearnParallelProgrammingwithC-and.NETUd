using System.Runtime.CompilerServices;

namespace Continuations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var task = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("Boiling Water");
            //});

            //var task2 = task.ContinueWith(t =>
            //{
            //    Console.WriteLine($"Completed task {t.Id}, pour water into cup.");
            //});

            //task2.Wait();

            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            var task3 = Task.Factory.ContinueWhenAll(new[] { task,task2},tasks =>
            {
                Console.WriteLine("Tasks completed:");
                foreach (var task in tasks) 
                {
                  Console.WriteLine("-" + task.Result);

                }
                Console.WriteLine("All tasks done");
            });

            task3.Wait();


        }
    }
}