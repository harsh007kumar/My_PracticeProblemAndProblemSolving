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
        public static void Print(string str = "") => Console.WriteLine($"\n===================== {str} =====================");

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

        public static int FindPrime(int num)
        {
            if (num <= 1)
                return num;

            int prime = 1;
            // loop to iterate thru all integers smaller than num to find larget prime no
            for (prime = num - 1; prime > 1; prime--)
            {
                bool isPrime = true;
                // loop to check given number is prime or not
                for (int i = prime / 2; i > 1; i--)
                    if (prime % i == 0)
                    { isPrime = false; break; }

                // break outer loop if prime found
                if (isPrime)
                    break;
            }
            return prime;
        }

        /// <summary>
        /// Returns num^power i.e, Math.Pow(num,power)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="power"></param>
        /// <returns></returns>
        public static int Power(int num, int power)
        {
            var ans = 1;
            for (int i = 0; i <= power - 1; i++)
                ans *= num;
            return ans;
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
