using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateInAnArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 2, 3, 4, 6, 7, 9 };
            //int arr[] = { 1, 2, 3, 1, 3, 6, 6 };
            int arr_size = arr.Length / arr[0];
            printRepeating(arr, arr_size);
        }
        
        public static void printRepeating(int[] arr2, int size)
        {
            int i;
            Console.WriteLine("The repeating elements are: \n");
            for (i = 0; i < size; i++)
            {
                if (arr2[Math.Abs(arr2[i])] >= 0)
                    arr2[Math.Abs(arr2[i])] = -arr2[Math.Abs(arr2[i])];
                else
                    Console.WriteLine(" {0} ", Math.Abs(arr2[i]));
            }
        }

        private static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}", arr[i]);
        }
    }
}
