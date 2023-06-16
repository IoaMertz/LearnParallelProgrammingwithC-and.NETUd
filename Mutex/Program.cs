using System.Threading;
namespace MutexSection
{
    internal class Program
    {
        public class BankAccount
        {


            public int Balance { get; private set; }

            public void Deposite(int amount)
            {
                    Balance += amount;
            }

            public void Withdraw(int amount)
            {
                    Balance -= amount;
            }

            public void Transfer(BankAccount where, int amount)
            {
                Balance -= amount;
                where.Balance += amount;
            }
        }
        

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();


            //is a construct that controlles accces to an area of code.
            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();
            



            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        // waiting for the mutex to become available
                        bool havelock = mutex.WaitOne();
                        try
                        {
                            ba.Deposite(1);
                        }
                        finally
                        {
                            if(havelock) mutex.ReleaseMutex();
                        }

                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool havelock = mutex2.WaitOne();
                        try
                        {
                            ba2.Deposite(1);
                        }
                        finally
                        {
                            if (havelock)
                            {
                                mutex2.ReleaseMutex();
                            }

                        }
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        //we want to use both accounts so we need to use both mutex
                        // this is the same as waitHandle.WaitAll()
                        bool haveLock = Mutex.WaitAll(new[] { mutex,mutex2});
                        try
                        {
                            ba.Transfer(ba2,1);
                        }
                        finally
                        {
                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                                mutex2.ReleaseMutex();

                            }
                        }

                    }
                }));


            }



            //the result isnt 0 as we might expect but in varies.
            //+= is not an operation but 2.Is not atomic
            // op1: temp < -get_Balance() + amount
            // op2: set_Balance(temp)
            //so something is happening between the 2 operations
            //and something is happening between these 2 operations


            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Final balance is {ba.Balance}");
            Console.WriteLine($"Final balance is {ba2.Balance}");

        }
    }
}