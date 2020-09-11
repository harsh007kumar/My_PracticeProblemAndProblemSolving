using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class PetrolPump
    { 
        public int Petrol, Distance;
        public PetrolPump(int petrol, int distance)     // Constructor
        { Petrol = petrol; Distance = distance; }
    }

    public class DailyProblem
    {
        // Time O(n) || Space O(1)
        // Simpler approach would be to use a Queue to keep track of pump added in order while current fuel level >= 0,
        // else start Dequeuing Pumps from the start untill fuel level is >= 0 again
        public static int PetrolPump(List<PetrolPump> pumps, int len)
        {
            if (len < 1) return -1;
            int startAt = 0, endAt = 1;
            int currFuel = (pumps[startAt].Petrol - pumps[startAt].Distance);

            // breaking condition is start iterated thru each possible pump in array and endAt has't came around to be same as start
            while (startAt < len)
            {
                while (currFuel >= 0)
                {
                    currFuel += (pumps[endAt].Petrol - pumps[endAt].Distance);           // add next pump
                    endAt = (endAt + 1) % len;                  // update end index
                    if (endAt == startAt) return startAt;       // found the 'starting pump index'
                }
                while (currFuel < 0 && startAt < len)
                {
                    currFuel -= (pumps[startAt].Petrol - pumps[startAt].Distance);      // remove start pump
                    startAt++;                                  // update start index
                }
            }
            return -1;      // start reached end of array still did not found appropriate starting index to complete the loop
        }

    }
}
