using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[][] b = new byte[100][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[100];
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
            byte[][] b = (byte[][])obj;

            for (int i = 0; i < b.Length; i++)
            {
                Thread c = new Thread(SumCol);
                c.Start( b[i] );
                Thread.Sleep(1);
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
            Console.WriteLine( "Sum: {0}", sum );
        }

    }
}
