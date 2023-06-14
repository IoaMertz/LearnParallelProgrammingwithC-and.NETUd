using System;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var t = Task.Factory.StartNew(() =>
            //{
            //    throw new InvalidOperationException();
            //});

            //var t2 = Task.Factory.StartNew(() =>
            //{
            //    throw new AccessViolationException();
            //});

            // these do not throw erros when they run NOT!!! in debug mode

            try
            {
                Demo();

            }catch (AggregateException e)
            {
                foreach (var t in e.InnerExceptions) { }
                Console.WriteLine($"Handled elsewhere : {e.GetType()} ");
            }

            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }

        private static void Demo()
        {
            var t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);

                throw new InvalidOperationException("Can't do this!");
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                throw new AccessViolationException("Can't access this!");

            });


            // Now we waited for them we see the errors(Even when we runn in Release mode)
            try
            {
                Task.WaitAll(t, t2);

            }
            //Exception specifically made for TPL(Task Parallel Library).
            //You collect all the exceptions from all the Tasks and you put them in a single exception
            catch (AggregateException ae)
            {
                //foreach (var e in ae.InnerExceptions)
                //{
                //    Console.WriteLine($"Exception {e.GetType()} from {e.Source} ");
                //}

                ae.Handle(e =>
                {
                    if(e is InvalidOperationException)
                    {
                        Console.WriteLine("invalid op!");
                        return true;
                    }
                    else return false; // this means the exception wasnt handled and it moves up the chain 
                });

            }
        }
    }
}