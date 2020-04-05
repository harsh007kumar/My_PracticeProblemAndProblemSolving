using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_GROUND_ZERO
{
    //Custom Delegate
    public delegate void MyFirstDelegate();
    class EventDemo
    {
        //Custom Event
        public event MyFirstDelegate MyFirstEvent;
        public EventDemo()
        {
            //Method which matches Signature of Custom Delegate
            void RegisteredMethod()
            { Console.WriteLine("RegisteredMethod invoked"); }

            //Registering "RegisteredMethod" method with event
            MyFirstEvent += new MyFirstDelegate(RegisteredMethod);

            //Firing custom event
            MyFirstEvent();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            new EventDemo();
            Console.ReadKey();
        }
    }
}
