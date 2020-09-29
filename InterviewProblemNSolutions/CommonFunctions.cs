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
        public static void Swap(ref char a, ref char b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// Swap two distinct and different input, doesn't works if inputs are same(Ex- XOR of 5 ^ 5 = 0)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void SwapXOR(ref char a, ref char b)
        {
            a ^= b;
            b ^= a;
            a ^= b;
            /* same as
            a=a^b;
            b=a^b;
            a=a^b;
            */
        }

        public static void Print(this int[] arr, string msg = "")
        {
            if (msg != "") Console.Write($" Printing '{msg}': \t");
            else Console.WriteLine($" ==== Printing given int array with '{arr.Length}' elements ====");

            for (int i=0;i<arr.Length;i++)
                Console.Write($" {arr[i]} >>");
            Console.WriteLine();
        }
        public static void Print(this char[,] arr)
        {
            var row = arr.GetLength(0);
            var col = arr.GetLength(1);
            Console.WriteLine($" ==== Printing given 2-D char array with {row}:Row & {col}:Col ====");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write($" {arr[i, j]} >>");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Print(this int[,] arr, bool silent = false)
        {
            var row = arr.GetLength(0);
            var col = arr.GetLength(1);
            if (!silent) Console.WriteLine($" ==== Printing given 2-D int array with {row}:Row & {col}:Col ====");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write($" {arr[i, j]} >>");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Print(this bool[,] arr)
        {
            var row = arr.GetLength(0);
            var col = arr.GetLength(1);
            Console.WriteLine($" ==== Printing given 2-D bool array with {row}:Row & {col}:Col ====");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    Console.Write($" {arr[i, j]} >>");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        

        public static void Show(this string str) => Console.WriteLine( str);

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

        /// <summary>
        /// Formula is 
        /// a)  num & ~(num-1)     or can also be written as       b)  num & (num* -1)
        /// lets understand with a) and example below
        /// num = 5 in binary(0000 0101)
        /// num-1 = 4 in binary(0000 0100)
        /// ~(num-1) = -5 in binary(1111 1011) // Negation results in all bits changing to opposite values 0 to 1 and 1 to 0
        /// num & ~(num-1)  = (0000 0101) & (1111 1011)  =  (0000 0001) = 1 in Decimal system
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int GetRightMostBit(int num) => num & ~(num - 1);

        // Returns sum of 'square of digits' Ex- 13, 1*1 + 3*3 = 10
        public static int GetSquaredDigitSum(int n) => (n == 0) ? 0 : (n % 10) * (n % 10) + GetSquaredDigitSum(n / 10);

        public static void Print(this HashSet<string> set)
        {
            foreach (var value in set)
                Console.Write($" \'{value}\'");
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
