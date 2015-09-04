using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_test
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
            

            Parallel.For(0, b.Length-1, (i) =>
            {
                int sum = 0;
                for (int j = 0; j < b[i].Length; j++)
                {
                    sum += b[i][j];
                }
                Console.WriteLine( "Sum: {0}", sum );
            });

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
