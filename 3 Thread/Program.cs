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

                return sum;
            }
        }

        public delegate int AsyncMethodCaller(int callDuration, byte[] b, out int threadId);

        static void Main(string[] args)
        {
            // Ініціалізація
            byte[][] b = new byte[100][];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = new byte[100];
                for (int j = 0; j < b[i].Length; j++)
                {
                    b[i][j] = 1;
                }
            }
        
            int threadId;


            // Проходи масив по рядку
            for (int i = 0; i < b.Length; i++)
            {
                AsyncSum async_sum = new AsyncSum();
                AsyncMethodCaller caller = new AsyncMethodCaller(async_sum.TestMethod);

                IAsyncResult result = caller.BeginInvoke(100, b[i], out threadId, null, null);
                result.AsyncWaitHandle.WaitOne();
                int returnValue = caller.EndInvoke(out threadId, result);
                result.AsyncWaitHandle.Close();


                Console.WriteLine("The call executed on thread {0}, with return value \"{1}\".",
                    threadId, returnValue);
            }

                
               

            Console.ReadLine();
        }
    }
}
