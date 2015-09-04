using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace _2_Thead
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[][] b = new byte[10000][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[10000];
                for (int j = 0; j < b[i].Length; j++)
                {
                    b[i][j] = 1;
                }
            }

            ThreadPool.QueueUserWorkItem( ShowSumCol, b );

            Console.ReadKey();
        }


        static public void ShowSumCol(object obj)
        {
            byte[][] b = (byte[][])obj;
            
            for (int i = 0; i < b.Length; i++)
            {
                ThreadPool.QueueUserWorkItem( SumCol, b[i] );
                //Thread.Sleep(100);
            }
        }


        static public void SumCol(object obj)
        {
            byte[] b = (byte[])obj;

            int sum = 0;

            for (int i = 0; i < b.Length; i++)
            {
                sum += b[i];
            }
            Console.WriteLine("Sum: {0}", sum);
        }
    }
}
