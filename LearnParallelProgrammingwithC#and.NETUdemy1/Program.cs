namespace LearnParallelProgrammingwithC_and.NETUdemy1
{
    internal class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i -- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(Object o)
        {
            int i = 10000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }


        public static int TextLenght(Object o)
        {
            // gives the id of the task we are currently in
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }
        static void Main(string[] args)
        {
            // this makes a Task and also starts its 
            //Task.Factory.StartNew(() => Write('.'));

            //var t = new Task(() => Write('?'));
            //t.Start();

            //Write('-');

            //overload : provide an object which ll be unpacked 

            //Task t = new Task(Write,"hello");
            //t.Start();

            //Task.Factory.StartNew(Write, 123);

            // generic Task with return
            string text1 = "testing", text2 = "this";

            var task1 = new Task<int>(TextLenght, text1);
            task1.Start();

            //as soon as we ask for a task for its result we wait for it to finish . It is a blocking aperation.
            Task<int> task2 = Task.Factory.StartNew(TextLenght, text2);

            Console.WriteLine($"Lenght of '{text1}' is {task1.Result}");





            Console.WriteLine("Main Programm Done");
            Console.ReadLine();
        }
    }
}