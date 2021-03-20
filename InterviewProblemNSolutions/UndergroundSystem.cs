using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class UndergroundSystem
    {
        /* 
         * Implement the UndergroundSystem class:
         * 
         * void checkIn(int id, string stationName, int t)
         *      A customer with a card id equal to id, gets in the station stationName at time t.
         *      A customer can only be checked into one place at a time.
         * 
         * void checkOut(int id, string stationName, int t)
         *      A customer with a card id equal to id, gets out from the station stationName at time t.
         * 
         * double getAverageTime(string startStation, string endStation)
         *      Returns the average time to travel between the startStation and the endStation.
         *      The average time is computed from all the previous traveling from startStation to endStation that happened directly.
         *      Call to getAverageTime is always valid.
         * 
         * You can assume all calls to checkIn and checkOut methods are consistent.
         * If a customer gets in at time t1 at some station, 
         * they get out at time t2 with t2 > t1. All events happen in chronological order
         */

        Dictionary<int, Pair<string, int>> custDict;
        Dictionary<string, Pair<double, int>> stationAvgDict;
        public UndergroundSystem()
        {
            custDict = new Dictionary<int, Pair<string, int>>();
            stationAvgDict = new Dictionary<string, Pair<double, int>>();
        }

        // Time O(1)
        public void CheckIn(int id, string stationName, int t) => custDict.Add(id, new Pair<string, int>(stationName, t));

        // Time O(1)
        public void CheckOut(int id, string stationName, int t)
        {
            // update avg time b/w station
            UpdateAverageTime(custDict[id], stationName, t);

            // delete customer after he/she exits from the system
            custDict.Remove(id);
        }

        // Time O(1)
        public double GetAverageTime(string startStation, string endStation) => stationAvgDict[GetKey(startStation, endStation)].key;

        
        void UpdateAverageTime(Pair<string, int> entryStationTime, string endStation, int t2)
        {
            string key = GetKey(entryStationTime.key, endStation);
            
            // first entry for this pair of station
            if (!stationAvgDict.ContainsKey(key))
                stationAvgDict.Add(key, new Pair<double, int>(t2 - entryStationTime.val, 1));
            else
            {
                var lastAvg = stationAvgDict[key];
                // new avg  = old avg + ((newvalue - old avg)/new size)
                stationAvgDict[key].key += ((t2 - entryStationTime.val) - lastAvg.key) / ++lastAvg.val;
            }
        }

        string GetKey(string station1, string station2) => station1.CompareTo(station2) < 0 ? station1 + station2 : station2 + station1;
    }
    
    /**
     * Your UndergroundSystem object will be instantiated and called as such:
     * UndergroundSystem obj = new UndergroundSystem();
     * obj.CheckIn(id,stationName,t);
     * obj.CheckOut(id,stationName,t);
     * double param_3 = obj.GetAverageTime(startStation,endStation);
     */
}
