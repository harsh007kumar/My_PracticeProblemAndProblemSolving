using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreading
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            Console.WriteLine("Hello, world!");

            dynamic a = new object();
            a = "harsh";
            C(a, false);
            a = 123.5D;
            C(a, true);
            a = a + "harsh";
            C(a);
            
            C("");
            //////// Thread ////////////
            Thread t = Thread.CurrentThread;
            t.Name = "PrimaryThread";
            Console.WriteLine(t.Name);
            Console.WriteLine(t.Priority);
            Console.WriteLine(t.IsAlive);
            Console.WriteLine(t.IsBackground);
            Console.WriteLine(t.ExecutionContext);
            //Console.WriteLine(Thread.CurrentContext.ContextID);
            Console.WriteLine(Thread.GetDomain().FriendlyName);


            Thread tt = new Thread(myFun);//Thread class accepts a delegate parameter.
            tt.Name = "Thread1";
            Thread t2 = new Thread(myFun2);
            t2.Name = "Thread2";
            t2.IsBackground = true;
            tt.Start();
            t2.Priority = ThreadPriority.Highest;//Setting Thread Priority
            t2.Start();
            Console.WriteLine("Main thread Running");
            //Console.ReadKey();
        }
        static void myFun()
        {
            Console.WriteLine("Running 1st Thread");
        }
        static void myFun2()
        {
            Console.WriteLine("Running 2nd Thread");
            Console.WriteLine("Thread {0} started", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} completed", Thread.CurrentThread.Name);
        }
        public static void C(dynamic s, bool b = true)
        {
            if (b)
                Console.WriteLine(s);
            else
                Console.Write(s);
        }
    }
}
