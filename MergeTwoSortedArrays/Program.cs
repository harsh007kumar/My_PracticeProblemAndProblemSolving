using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeTwoSortedArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr1 = { 1, 2, 4, 6, 8 };
            int[] arr2 = { 2, 3, 5, 7, 9 };
            int[] result = MergeArray(arr1, arr2);
            foreach (int var in result)
                Console.WriteLine(" {0}", var);

            Console.ReadLine();
        }
        public static int[] MergeArray(int[] arr1,int[] arr2)
        {
            int lengthOfFirstArray = arr1.Length;
            int lengthOfSecondArray = arr2.Length;
            int resultArrayLength = lengthOfFirstArray + lengthOfSecondArray;
            int[] result = new int[resultArrayLength];
            int i = 0, j = 0;
            for (int k = 0; k < resultArrayLength; k++)
            {
                if(arr1.Length == i)
                { result[resultArrayLength - (i + j + 1)] = arr2[j]; j++; }
                else if(arr2.Length == j)
                { result[resultArrayLength - (i + j + 1)] = arr1[i]; i++; }
                else if (arr1[i] <= arr2[j])
                { result[resultArrayLength - (i + j + 1)] = arr1[i]; i++; }
                else
                { result[resultArrayLength - (i + j + 1)] = arr2[j]; j++; }
            }
            return result;
        }
    }
}
