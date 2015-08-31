using System;
using System.Collections.Generic;
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
            public int TestMethod(int callDuration, byte[] b, out int threadId)
            {
                threadId = Thread.CurrentThread.ManagedThreadId;
                Thread.Sleep(callDuration);
                int sum = 0;

                for (int i = 0; i < b.Length; i++)
                {
                    sum += b[i];
                }
                //Thread.Sleep(100);
                return sum;
            }
        }

        public delegate int AsyncMethodCaller(int callDuration, byte[] b, out int threadId);

        static void Main(string[] args)
        {
            // Ініціалізація
            byte[][] b = new byte[1000][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[100];
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
            
            // Проходи масив по рядку
            for (int i = 0; i < b.Length; i++)
            {
                AsyncSum async_sum = new AsyncSum();
                AsyncMethodCaller caller = new AsyncMethodCaller(async_sum.TestMethod);

                async_caller.Add(caller);

                async_res.Add (async_caller[i].BeginInvoke(100, b[i], out async_threadId[i], null, null) );
                
            }

            int returnValue;

            for (int i = 0; i < b.Length; i++)
            {
                async_res[i].AsyncWaitHandle.WaitOne();
                returnValue = async_caller[i].EndInvoke(out async_threadId[i], async_res[i]);
                async_res[i].AsyncWaitHandle.Close();

                Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".", async_threadId[i], returnValue);
            }

            Console.ReadLine();
        }
    }
}
