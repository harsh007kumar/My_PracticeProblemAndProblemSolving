using System;

namespace InterviewProblemNSolutions
{
    public class RangeSumQuery2DImmutable
    {
        readonly int[,] mat;
        // Constructor || Time O(r*c)
        public RangeSumQuery2DImmutable(int[][] matrix)
        {
            int r = matrix.Length;
            if (r < 1) return;
            int c = matrix[0].Length;
            mat = new int[r, c + 1];
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    // Calculate Prefix Sum for each row
                    mat[i, j + 1] = mat[i, j] + matrix[i][j];
        }

        // Time O(r'), r' = row2-row1
        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            int sum = 0;
            for (int r = row1; r <= row2; r++)
                sum += mat[r, col2 + 1] - mat[r, col1];
            Console.WriteLine($" Sum of cells b/w top-left corner {row1}:{col1} and bottom-rt corner {row2}:{col2} is: \'{sum}\'");
            return sum;
        }
    }
}
