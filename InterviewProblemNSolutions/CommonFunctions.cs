using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    class CommonFunctions
    {
    }

    public static class Utility
    {
        public static void Print(string str) => Console.WriteLine($"\n===================== {str} =====================");

        public static void Swap(ref int a,ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static void Print(this int[] arr)
        {
            for(int i=0;i<arr.Length;i++)
                Console.Write($" {arr[i]} >>");
            Console.WriteLine();
        }
    }

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
}
