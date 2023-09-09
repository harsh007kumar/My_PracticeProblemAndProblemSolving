using System.Collections.Generic;
using System.Linq;

namespace InterviewProblemNSolutions
{
    public class TimeMap
    {
        readonly Dictionary<string, List<(string val, int time)>> dict;
        public TimeMap() => dict = new Dictionary<string, List<(string, int)>>();

        // Time O(1)
        public void Set(string key, string value, int timestamp)
        {
            if (!dict.ContainsKey(key))
                dict[key] = new List<(string, int)>() { (value, timestamp) };
            else
                dict[key].Add((value, timestamp));
        }

        // Time Best case O(1) Worst Case O(logk), k being total unique values for a given 'key'
        public string Get(string key, int timestamp)
        {
            if (!dict.ContainsKey(key)) return "";
            /* No need to sort as question mentions clearly => All the timestamp of set are strictly in increasing order
            // i.e. when we are adding its already getting sorted in asending order
            // sort associated values in choronological order i.e. timestamp
            //dict[key] = (from valueTime in dict[key]        // O(klogK), k being total unique values for a given 'key'
            //             orderby valueTime.time
            //             select valueTime).ToList();
            */

            var choronogicalValues = dict[key];
            int lt = 0, rt = choronogicalValues.Count - 1;
            string ans = "";
            while (lt <= rt)   // O(logk)
            {
                var mid = lt + (rt - lt) / 2;
                if (choronogicalValues[mid].time == timestamp)  // got actual time match
                    return choronogicalValues[mid].val;
                else if(choronogicalValues[mid].time < timestamp)
                {
                    ans = choronogicalValues[mid].val;
                    lt = mid + 1;
                }
                else rt = mid - 1;
            }
            return ans;
        }
    }
}
