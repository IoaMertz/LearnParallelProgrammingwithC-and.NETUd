using System.Collections.Concurrent;

namespace ConcurrentDictionary
{
    internal class Program
    {

        private static ConcurrentDictionary<string, string> capitals =
           new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            // normal dict throws exception is key arleady exists. this returns bool.
            bool success = capitals.TryAdd("France","Paris");
            string who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main Thread";
            Console.WriteLine($"{who} { (success ? "added" :"did not add") } the element");
        }
        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddParis);
            AddParis();
            Console.WriteLine("Hello, World!");


            capitals["Russia"] = "Leningrand";
            //capitals["Russia"] = "Moscow";

            capitals.AddOrUpdate("Russia","Moscow",(k,old) => old + $"---> Moscow");


            Console.WriteLine($"The capital of Russia is {capitals["Russia"]}");
            Console.WriteLine($"{capitals["Russia"]}");


        }
    }
}