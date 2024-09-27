using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class MyCalendarTwo
    {
        List<int[]> nonOverlapping, overlapping;

        public MyCalendarTwo()
        {
            nonOverlapping = new();
            overlapping = new();
        }

        // Time = Space = O(n), n = no of intervals currently in DataStructure
        public bool Book(int start, int end)
        {
            /* ALGO
            1. We keep two list:
                - holds all the non-overlapping intervals
                - holds all the overlapping intervals
            2. Now for each new interval we first check if that overlaps
                with an interval in overlapping list means a part of
                interval is already booked twice hence return false.
            3. if from step #2 no such interval is found, we know for sure we
                can add this new event to DataStructure without Triple booking.
            4. Proper way is to check from non-overlapping list if any part
                of new interval overlaps with existing than just add that
                part to overlapping list.
            5. add the end before returing true, rememeber to add the event
                to non-overlapping list also.
             */
            // check for Triple booking
            foreach (var interval in overlapping)                // O(n)
                // new interval start is smaller than existing interval end
                // & new interval end is greater than existing interval start
                if (start < interval[1] && end > interval[0])
                    return false;
            // Add the overlapped part to 'overlapping' list
            foreach (var interval in nonOverlapping)             // O(n)
                // new interval start is smaller than existing interval end
                // & new interval end is greater than existing interval start
                if (start < interval[1] && end > interval[0])
                    overlapping.Add([Math.Max(start, interval[0]), Math.Min(end, interval[1])]);

            // also remeber to add new event to nonOverlapping list
            nonOverlapping.Add([start, end]);
            return true;
        }
    }
}
