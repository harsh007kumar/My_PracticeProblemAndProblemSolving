using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourGlass
{
    class Program
    {
        public static int SumHourGlass(int row, int col, int[][] array)
        {
            int sum = 0;
            int s1 = array[row][col] + array[row][col + 1] + array[row][col + 2];
            int s2 = array[row + 1][col + 1];
            int s3 = array[row + 2][col] + array[row + 2][col + 1] + array[row + 2][col + 2];
            sum = s1 + s2 + s3;

            return sum;
        }
        static void Main(string[] args)
        {
            int[][] arr = new int[6][];
            int maxHourGlass = int.MinValue;
            for (int arr_i = 0; arr_i < 6; arr_i++)
            {
                string[] arr_temp = Console.ReadLine().Split(' ');
                arr[arr_i] = Array.ConvertAll(arr_temp, Int32.Parse);
            }
            for(int i =0;i<4;i++)
            {
                for(int j=0;j<4;j++)
                {
                    int big = SumHourGlass(i, j, arr);
                    if (big> maxHourGlass)
                        maxHourGlass = big;
                }
            }
            Console.WriteLine(maxHourGlass);
            Console.ReadLine();

        }

    }
}
