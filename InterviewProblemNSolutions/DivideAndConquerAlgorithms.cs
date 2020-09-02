using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class Point : IComparable<Point>
    {
        public int X;
        public int Y;
        public bool isVisited;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point() => X = Y = 0;

        // returns distance b/w pair of points, using Pythagorean theorem where H^2 = X^2 + Y^2
        public static double GetDistance(Point p1, Point p2) => Math.Sqrt(Math.Pow(Math.Abs(p1.X - p2.X), 2) + Math.Pow(Math.Abs(p1.Y - p2.Y), 2));

        public int CompareTo(Point p1) => this.X.CompareTo(p1.X);
    }

    /// <summary>
    /// Class which inherits 'IComparer' & implements 'Compare' func now class object can be passed as argument in 'Sort' like func which accepts IComparer argument
    /// Ex : List<Point> p = new List<Point>();
    /// p.Sort(new PointXComparer());       // sort List of points as per X coordinate
    /// Referrence : https://www.techiedelight.com/sort-list-of-objects-csharp/
    /// </summary>
    public class PointXComparer : IComparer<Point>
    {
        public int Compare(Point p1, Point p2) => p1.X.CompareTo(p2.X);
    }
    public class PointYComparer : IComparer<Point>
    {
        public int Compare(Point p1, Point p2) => p1.Y.CompareTo(p2.Y);
    }

    public class Building
    {
        public int left, ht, right;
        public Building(int leftXCoordinate, int height, int rightXCoordinate)
        { left = leftXCoordinate; ht = height; right = rightXCoordinate; }
    }

    public class Strip
    {
        public int X, Ht;
        public Strip(int xCoordinate, int height)
        { X = xCoordinate; Ht = height; }
    }

    public class DivideAndConquerAlgorithms
    {
        // Time O(nlogn) || Space O(1) (does required recursion stack thou)
        public static int MaxValueContinousSubsequence(int[] input, int start, int last)
        {
            if (start == last) return input[start] > 0 ? input[start] : 0;
            var mid = start + (last - start) / 2;
            var leftHalfMax = MaxValueContinousSubsequence(input, start, mid);
            var rightHalfMax = MaxValueContinousSubsequence(input, mid + 1, last);

            int i = 0, leftMax = 0, rightMax = 0, currentSum = 0;
            // for left half find max sum value starting from mid index streching to its left
            for (i = mid; i >= 0; i--)
            {
                currentSum += input[i];
                if (currentSum > leftMax)
                    leftMax = currentSum;
            }
            currentSum = 0; // reset sum

            // for right half find max sum value starting from mid+1 index streching to its right
            for (i = mid + 1; i <= last; i++)
            {
                currentSum += input[i];
                if (currentSum > rightMax)
                    rightMax = currentSum;
            }
            // return max value from left half or right half or combined max value from left+right
            return Math.Max(leftHalfMax, Math.Max(rightHalfMax, leftMax + rightMax));
        }

        // Time O(logn), Ex- 9^24 can we calculated as (9^12)^2 = ((9^6)^2)^2 = (((9^3)^2)^2)^2 = (((9.9^2)^2)^2)^2 total 5 multiplications required
        // as apposed to Linear way where we multiple no by itself O(n-1) times
        public static long FindPower(long num, long power)
        {
            if (power == 0)
                return 1;
            if (power % 2 == 1)
            {
                var oddPower = FindPower(num, power - 1);
                return num * oddPower;
            }
            else
            {
                var evenPower = FindPower(num, power / 2);
                return evenPower * evenPower;
            }
        }

        public static double ClosetPair(Point[] points)
        {
            if (points == null || points.Length == 0) return -1;

            /* 1) sort the points as per X axis using some O(nlogn) sorting technique (Ex-QuickSort)
             * 2) use mid and divide the array into two halfs [0..Mid] & [Mid+1..Last]
             * 3) recursively call ClosetPair on both halfs, which return closed distance 'minDistanceLeft' & 'minDistanceRight' respectively
             * 4) store min of above as 'D' i.e, D = Math.Min(minDistanceLeft,minDistanceRight)
             * 5) create a Strip which include Points around Median of original array within D distance,
             *    i.e, points that are max 'D' distance from Median.X either on left side or right side or median
             * 6) Sort the Strip[] of points as per 'Y' axis using some O(nlogn) sorting technique (Ex-QuickSort)
             * 7) Find and return min distance b/w pair of points present in Strip lets call it D'
             *    [ This step takes constant time O(n) as we are checking only point which are D distance apart from given points,
             *    check Dx2D rectangle in which each DxD cube host only one points hence we need to check only for 7 other cubes to find closet distance ]
             * 8) return Min(D,D')
             */

            // Sort using LINQ, ideally will using QuickSort/MergeSort techique to sort in O(nlogn)
            points = points.OrderBy(p1 => p1.X).ToArray();
            #region order ways to sort instantly
            // points = (from p in points                                                           // Sorting using LINQ Query Expressions
            //          orderby p.X
            //          select p).ToArray();
            // points.OrderBy(delegate (Point p1, Point p2) { return p1.X.CompareTo(p2.X); });      // For sorting List<T> using delegate
            // points.OrderBy((p1, p2) => p1.X.CompareTo(p2.X));                                    // For sorting List<T> using Lambda func
            // points.Sort(new PointXComparer());                                                   // Sort Array of points by passing 'object of IComparer' class which implemented Compare() func
            // point.Sort()     // public int CompareTo(Point p1) => this.X.CompareTo(p1.X);        // Point class implements 'CompareTo' func() of inherited 'IComparable<T>' interface
            #endregion

            return ClosetPairUtil(points, 0, points.Length - 1);
        }

        // Time O(nlogn) horizontal sort + 2T(n/2) Divde & Conquer + O(n) to calculate Strip + O(nlogn) vertical sorting of Strip + O(n) for StripCloset => O(n * (logn)^2)
        public static double ClosetPairUtil(Point[] points, int start, int last)
        {
            var len = last - start + 1;
            if (len <= 3)
                return BruteForcecClosetPair(points, start, last);

            var mid = start + (last - start) / 2;
            var minDistanceLeft = ClosetPairUtil(points, start, mid);       // find min distance in left half
            var minDistanceRight = ClosetPairUtil(points, mid + 1, last);   // find min distance in right half

            var dDistance = Math.Min(minDistanceLeft, minDistanceRight);    // get min of above
            var strip = Strip(points, points[mid], dDistance);              // create strip which include points at max dDistance apart from mid point

            var yAxisSortedStrip = strip.OrderBy(p1 => p1.X).ToArray();     // sort strip using O(nlogn) sorting algo Ex- QuickSort/MergeSort, for simplicity using LINQ
            var dDash = StripCloset(yAxisSortedStrip, dDistance);           // find min distance among points in Strip || Time O(n)

            return Math.Min(dDistance, dDash);
        }

        // Time O(n^2) for finding Shortest Distance b/w pair of Points in 2D space || Space O(1)
        public static double BruteForcecClosetPair(Point[] points, int start, int last)
        {
            double minDistance = int.MaxValue;
            for (int i = start; i <= last; i++)
                for (int j = i + 1; j <= last; j++)
                    if (Point.GetDistance(points[i], points[j]) < minDistance)
                        minDistance = Point.GetDistance(points[i], points[j]);
            return minDistance;
        }

        // returns Array of Points which are 'dDistance' apart on either Left or Right side of midX value from array of Points
        public static Point[] Strip(Point[] points, Point mid, double dDistance)
        {
            List<Point> strip = new List<Point>();
            for (int i = 0; i < points.Length; i++)
                if (Point.GetDistance(points[i], mid) <= dDistance)
                    strip.Add(points[i]);
            return strip.ToArray();
        }

        // find and returns distance b/w pair of Points which are closer than minDistance
        public static double StripCloset(Point[] points, double minDistance)
        {
            var dDash = minDistance;
            for (int i = 0; i < points.Length; i++)
                for (int j = i + 1; j < points.Length && Math.Abs(points[i].Y - points[j].Y) < minDistance; j++)
                    if (Point.GetDistance(points[i], points[j]) < dDash)
                        dDash = Point.GetDistance(points[i], points[j]);
            return dDash;
        }

        // Time O(n^2) || Space O(1000) ~ O(1)
        public static int[] BruteForceSkyLine(Building[] buildings, int start, int last)
        {
            // making an assumption here that right of last most building is less than equal to 1000
            var maxRightX = 1000;
            int[] skyLine = new int[maxRightX];      // array which will hold max height at each point thruout the city

            int i = 0, j = 0, rightMostXInSkyLine = 0;
            for (i = start; i <= last; i++)                             // iterate thru all the buildings in input
            {
                for (j = buildings[i].left; j < buildings[i].right; j++)// iterate thru all the point b/w left and right of the building
                {
                    if (skyLine[j] < buildings[i].ht)                   // update height of Skyline
                        skyLine[j] = buildings[i].ht;
                }
                if (rightMostXInSkyLine < buildings[i].right)
                    rightMostXInSkyLine = buildings[i].right;
            }

            PrintSkyLine(skyLine, rightMostXInSkyLine + 1);
            return skyLine;
        }

        public static void PrintSkyLine(int[] skyline, int length = 0)
        {
            var prvHeight = 0;
            Console.Write($"\n SkyLine for given set of buildings is : ");
            for (int i = 0; i < length; i++)
                if (skyline[i] != prvHeight)
                {
                    Console.Write($" [{i},{skyline[i]}]");
                    prvHeight = skyline[i];
                }
            Console.WriteLine();
        }

        public static void PrintBuildings(Building[] buildings)
        {
            Console.Write($"\n Input Buildings are : ");
            for (int i = 0; i < buildings.Length; i++)
                Console.Write($" [{buildings[i].left},{buildings[i].ht},{buildings[i].right}]");
        }

        // Time O(nlogn) || Space O(n)
        public static List<Strip> GetSkyLine(Building[] buildings, int start, int last)
        {
            /*
             * Divide the input buildings into two halfs leftSkyL and rightSkyL
             * Recursively call the Main method on both the halves to get their left & right SkyLine
             * Now Merge these Skyline (delete any duplicate points with same heights/ or while adding check for last points height)
             */
            if (start == last)                                                  // Time O(1)
            {
                // when we are down to single building Ex: {2,15,5} we return array of keyValue with value {2,15},{5,0}
                var skyLine = new List<Strip>();
                skyLine.Add(new Strip(buildings[start].left, buildings[start].ht));
                skyLine.Add(new Strip(buildings[start].right, 0));
                return skyLine;
            }

            var mid = start + (last - start) / 2;
            var leftHalfSkyLine = GetSkyLine(buildings, start, mid);            // Time O(n/2)
            var rightHalfSkyLine = GetSkyLine(buildings, mid + 1, last);        // Time O(n/2)

            // Merge the Skyline of left and right half to get SkyLine for original question
            return MergeSkyLine(leftHalfSkyLine, rightHalfSkyLine);             // Time O(n)
        }

        // Time O(n+m), n = length of left SkyLine & m = length of right SkyLine || Space O(n+m)
        public static List<Strip> MergeSkyLine(List<Strip> left, List<Strip> right)
        {
            int hLeft = 0, hRight = 0;                                      // to store current height of left & right
            int leftIndex = 0, rightIndex = 0;                              // for iterating thru both the left and right SkyLine
            int leftLen = left.Count, rightLen = right.Count;               // length of left & right Skyline
            int maxHeight = -1, prvHeight = -1;

            List<Strip> mergedSkyLine = new List<Strip>();
            while (leftIndex < leftLen || rightIndex < rightLen)
            {
                if (leftIndex == leftLen)
                    mergedSkyLine.Add(right[rightIndex++]);
                else if (rightIndex == rightLen)
                    mergedSkyLine.Add(left[leftIndex++]);
                else if (left[leftIndex].X < right[rightIndex].X)           // 'X' coordinate from left Skyline is smaller than right
                {
                    hLeft = left[leftIndex].Ht;
                    maxHeight = Math.Max(hLeft, hRight);
                    if (prvHeight != maxHeight)                             // add to Merge only if Height is different than last height
                        mergedSkyLine.Add(new Strip(left[leftIndex].X, maxHeight));
                    leftIndex++;
                }
                else
                {
                    hRight = right[rightIndex].Ht;
                    maxHeight = Math.Max(hLeft, hRight);
                    if (prvHeight != maxHeight)                             // add to Merge only if Height is different than last height
                        mergedSkyLine.Add(new Strip(right[rightIndex].X, maxHeight));
                    rightIndex++;
                }
                prvHeight = maxHeight;
            }
            return mergedSkyLine;
        }

        public static void PrintSkyLine(List<Strip> skyline)
        {
            var prvHeight = 0;
            Console.Write($"\n SkyLine for given set of buildings is : ");
            foreach (var point in skyline)
                if (point.Ht != prvHeight)
                {
                    Console.Write($" [{point.X},{point.Ht}]");
                    prvHeight = point.Ht;
                }
            Console.WriteLine();
        }

    }
}
