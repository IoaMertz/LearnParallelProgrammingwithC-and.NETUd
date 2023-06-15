namespace LockRecursion
{
    internal class Program
    {
        static SpinLock sl = new SpinLock(true);
        public static void LockRecursion(int x)
        {
            bool locktaken = false;

            try
            {
                sl.Enter(ref locktaken);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                if (locktaken)
                {
                    Console.WriteLine($"Took a Lock, x = {x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"faied to take a lock, x={x}");
                }
            }
        }
        static void Main(string[] args)
        {
            LockRecursion(0);
            Console.WriteLine("Hello, World!");
           
        }
    }
}