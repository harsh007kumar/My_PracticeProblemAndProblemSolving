using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            string var = null;
            while (var != "0")
            {
                Console.WriteLine("\n\n=== Select which Sorting u wish to perfrom from below\\nB=BubbleSort\nS=SelectionSort\nInsertionSort\n0=EXIT");
                var = Console.ReadLine();
                switch (var.ToUpper())
                {
                    case "B":
                        Console.WriteLine("Bubble Sort");
                        int[] B_arr = takearrayinput();
                        bubblesort(ref B_arr);
                        print(B_arr);
                        break;
                    case "S":
                        Console.WriteLine("Selection Sort");
                        int[] S_arr = selectionsort(takearrayinput());
                        print(S_arr);
                        break;
                    case "I":
                        Console.WriteLine("Insertion Sort");
                        int[] I_arr = Insertionsort(takearrayinput());
                        print(I_arr);
                        break;
                    case "0":
                        Console.WriteLine("=== Exiting the Solution ===");
                        break;
                    default:
                        Console.WriteLine("Enter a Appropriate Keyword");
                        break;
                }
            }
            Console.ReadKey();
        }

        public static void bubblesort(ref int[] arr)
        {
            for (int i = 0; i< arr.Length-1; i++)
            {
                for (int j = 0; j< arr.Length-1-i; j++)
                {
                    if (arr[j] > arr[j + 1])
                        swap(ref arr[j],ref arr[j + 1]);
                }
            }
        }
        public static int[] selectionsort(int[] arr)
        {
            int[] to_sort = arr;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int smallest=arr[i],index=i;
                for (int j = i+1; j < arr.Length; j++)
                {
                    if (arr[j] < smallest)
                    {
                        smallest = arr[j];
                        index = j;
                    }
                }
                if (smallest < arr[i])
                    swap(ref arr[index], ref arr[i]);   
            }
            return to_sort;
        }

        private static int[] Insertionsort(int[] arr)
        {
            int[] to_sort = arr;
            return to_sort;
        }

        /// <summary>
        /// To Swap To elements from any particular array
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        private static void swap(ref int v1,ref int v2)
        {
            v1 = v1 + v2;
            v2 = v1 - v2;
            v1 = v1 - v2;
        }
        private static int[] takearrayinput()
        {
            //int[] arr = new int[] { };
            Console.WriteLine("\nEnter the integer(s) you want in ur array seperated by comma(,) like 2,3,4");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }
        private static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}",arr[i]);
        }
    }
}
