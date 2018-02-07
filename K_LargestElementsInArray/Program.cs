using System;

namespace K_LargestElementsInArray
{
    class MainClass
    {
        // Question: Write an efficient program for printing k largest elements in an array. Elements in array can be in any order.
        // For example, if given array is [1, 23, 12, 9, 30, 2, 50] and you are asked for the largest 3 elements i.e., k = 3
        // then your program should print 50, 30 and 23.
                                                                                                                                                                      
        public static void Main(string[] args)
        {
            //int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse); // If wish to take input from user
            int[] arr = { 1, 23, 12, 9, 30, 2, 50 };
            int length = arr.Length;

            int noOfElements = 3;   // Convert.ToInt32(Console.ReadLine());
            int index = noOfElements - 1;
            int[] MaxElemets = new int[noOfElements];

            for (int x = 0; x < length; x++)
            {
                if (MaxElemets[index] < arr[x])
                {
                    MaxElemets[index-2] = MaxElemets[index-1];
                    MaxElemets[index-1] = MaxElemets[index];
                    MaxElemets[index] = arr[x];
                }
            }

            //Print
            foreach (int v in MaxElemets)
                Console.Write($"{v} ");
            
            Console.ReadLine();
        }
    }
}
