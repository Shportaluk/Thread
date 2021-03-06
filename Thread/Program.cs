﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_Thead
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[][] b = new byte[10000][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[10000];
                for ( int j = 0; j < b[i].Length; j++ )
                {
                    b[i][j] = 1;
                }
            }
            
            Thread t = new Thread( ShowSumCol );
            t.Start( b );

            Console.ReadKey();
        }


        static public void ShowSumCol(object obj)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            byte[][] b = (byte[][])obj;
            for (int i = 0; i < b.Length; i++)
            {
                Thread c = new Thread(SumCol);
                c.Start( b[i] );
                //Thread.Sleep(1);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }


        static public void SumCol(object obj)
        {
            byte[] b = (byte[])obj;

            int sum = 0;

            for (int i = 0; i < b.Length; i++)
            {
                sum += b[i];
            }
            Console.WriteLine( "Sum: {0}", sum );
        }

    }
}
