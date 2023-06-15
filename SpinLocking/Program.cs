namespace SpinLocking
{
    internal class Program
    {
        public class BankAccount
        {
            public object padlock = new object();

            private int balance;
            public int Balance
            {
                get { return balance; }
                private set { balance = value; }
            }

            public void Deposite(int amount)
            {

                balance += amount;


            }

            public void Withdraw(int amount)
            {
                
                Balance -= amount;
            }
        }

        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            SpinLock sl = new SpinLock();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool locktaken = false;
                        try
                        {
                            sl.Enter(ref locktaken);
                            ba.Deposite(100);
                        }
                        finally 
                        {
                            if (locktaken)
                            {
                                sl.Exit();
                            }
                        }
                    }
                }));
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool locktaken = false;
                        try
                        {
                            sl.Enter(ref locktaken);
                            ba.Withdraw(100);
                        }
                        finally
                        {
                            if (locktaken)
                            {
                                sl.Exit();
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

        }
    }
}