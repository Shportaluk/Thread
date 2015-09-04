using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Diagnostics;

namespace _Task
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
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<Task<int>> list_task = new List<Task<int>>();
            for (int i = 0; i < b.Length; i++)
            {
                list_task.Add ( new Task<int>(() => Sum( b[i] )) );
                list_task[i].Start();
                Console.WriteLine(list_task[i].Result);
            }


            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);


            //for (int i = 0; i < list_task.Count ; i++)
            //{
            //    Console.WriteLine(list_task[i].Result);
            //}

            Console.Read();
        }

        

        public static int Sum( byte[] b )
        {
            int sum = 0;
            for (int i = 0; i < b.Length; i++)
            {
                sum += b[i];
            }
            return sum;
        }
    }
}
