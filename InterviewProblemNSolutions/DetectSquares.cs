using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class DetectSquares
    {
        /* ALGO
        We need to use find a way to quicky understand if given set of points form a square or not
        Brute force way wud require 3 loops for a given input point to find it it makes a square or not

        Efficient way is we just go thru all the diagnoal points
        A ----- B
        |       |
        |       |
        C ----- D
        Ex if we are given B's corrdinate in to find no of Square
        we just need to check if any point which is daignoal to B exists if yes diagnoal points exists
        we just need to see if there are 2 more points on Cx,By and Bx,Cy present in out data-structure

        if yes we got our square
        */
        Dictionary<string, int> pointsFreq = null;
        List<string> points = null;

        // Default Constructor
        public DetectSquares()
        {
            pointsFreq = new Dictionary<string, int>();
            points = new List<string>();
        }

        // Time O(1)
        public void Add(int[] point)
        {
            var key = point[0] + "," + point[1];
            // add to list
            points.Add(key);
            // add to dictionary (increament counter if already present)
            pointsFreq[key] = 1 + (pointsFreq.TryGetValue(key, out var freq) ? freq : 0);
        }

        // Time O(n)
        public int Count(int[] point)
        {
            int Qx = point[0], Qy = point[1], result = 0;
            foreach (var p in points)                    // O(n)
            {
                var key = p.Split(',');
                int Px = Convert.ToInt32(key[0]);
                int Py = Convert.ToInt32(key[1]);
                // check if any points form a diagnol with input point | also check if current points is not overlapping with input (we need +ve sum area)
                if (Math.Abs(Qx - Px) != Math.Abs(Qy - Py) || Qx == Px) continue;

                // check for 3rd
                string nextPoint = Qx + "," + Py;
                int RCounter = pointsFreq.TryGetValue(nextPoint, out var freq1) ? freq1 : 0;

                // check for 4th only if we found 3rd
                if (RCounter == 0) continue;
                nextPoint = Px + "," + Qy;
                // Use TryGetValue() on dict as it need only 1 look up instead of ContainsKey and then fetching
                // RCounter*=pointsFreq.ContainsKey(nextPoint) ? pointsFreq[nextPoint] : 0;
                RCounter *= pointsFreq.TryGetValue(nextPoint, out var freq2) ? freq2 : 0;

                // update total square conunter
                result += RCounter;
            }
            return result;
        }
    }
}
