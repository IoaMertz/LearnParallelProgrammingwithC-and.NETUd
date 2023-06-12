using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LearnParallelProgrammingwithC_and.NETUdemy1
{
    internal static class Demo
    {
        public static void write(char c)
        {
            int i = 1000;
            while (i-- > 0) 
            {
                Console.Write(i);
            }

        }

        public static void read(char c) { }

    }
}
