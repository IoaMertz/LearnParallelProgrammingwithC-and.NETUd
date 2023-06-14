using System.Reflection.PortableExecutable;

namespace CriticalSections
{
    internal class Program
    {
        public class BankAccount
        {
            public object padlock = new object();
            

            public int Balance { get; private set; }

            public void Deposite(int amount)
            {
                lock(padlock) //only 1 thread can enter
                {
                    Balance += amount;
                }
            }

            public void Withdraw(int amount)
            {
                lock (padlock)
                {
                    Balance -= amount;
                }

            }
        }
        
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposite(100);
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
            }



            the result isnt 0 as we might expect but in varies.


            //+= is not an operation but 2.Is not atomic
            // op1: temp < -get_Balance() + amount
            // op2: set_Balance(temp)
            //so something is happening between the 2 operations
            //and something is happening between these 2 operations


            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");

        }
    }
}