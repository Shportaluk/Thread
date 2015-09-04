using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3_Thread
{
    class Program
    {
        public class AsyncSum
        {
            public int TestMethod( byte[] b, out int threadId)
            {
                threadId = Thread.CurrentThread.ManagedThreadId;
                int sum = 0;

                for (int i = 0; i < b.Length; i++)
                {
                    sum += b[i];
                }
                //Thread.Sleep(100);
                return sum;
            }
        }

        public delegate int AsyncMethodCaller(byte[] b, out int threadId);

        static void Main(string[] args)
        {
            // Ініціалізація
            byte[][] b = new byte[10000][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[10000];
                for (int j = 0; j < b[i].Length; j++)
                {
                    b[i][j] = 1;
                }
            }
        
            int[] async_threadId = new int[b.Length];

            for (int i = 0; i < b.Length; i++)
            {
                async_threadId[i] = i;
            }

            List<AsyncMethodCaller> async_caller = new List<AsyncMethodCaller>();
            List<IAsyncResult> async_res = new List<IAsyncResult>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            
            // Проходи масив по рядку
            for (int i = 0; i < b.Length; i++)
            {
                AsyncSum async_sum = new AsyncSum();
                AsyncMethodCaller caller = new AsyncMethodCaller(async_sum.TestMethod);

                async_caller.Add(caller);

                async_res.Add (async_caller[i].BeginInvoke( b[i], out async_threadId[i], null, null) );
                
            }

            int returnValue;

            for (int i = 0; i < b.Length; i++)
            {
                async_res[i].AsyncWaitHandle.WaitOne();
                returnValue = async_caller[i].EndInvoke(out async_threadId[i], async_res[i]);
                async_res[i].AsyncWaitHandle.Close();

                Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".", async_threadId[i], returnValue);
            }

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            Console.ReadLine();
        }
    }
}
