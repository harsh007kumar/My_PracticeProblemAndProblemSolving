using InterviewProblemNSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            if (len == 1) return pumps[0].Petrol >= pumps[0].Distance ? 0 : -1;

            int startAt = 0, endAt = 1;
            int currFuel = (pumps[startAt].Petrol - pumps[startAt].Distance);

            // breaking condition is start iterated thru each possible pump in array and endAt has't came around to be same as start
            while (startAt < len)
            {
                while (currFuel >= 0)
                {
                    currFuel += (pumps[endAt].Petrol - pumps[endAt].Distance);           // add next pump
                    endAt = (endAt + 1) % len;                  // update end index
                    if (endAt == startAt && currFuel >= 0) return startAt;       // found the 'starting pump index'
                }
                while (currFuel < 0 && startAt < len)
                {
                    currFuel -= (pumps[startAt].Petrol - pumps[startAt].Distance);      // remove start pump
                    startAt++;                                  // update start index
                }
            }
            return -1;      // start reached end of array still did not found appropriate starting index to complete the loop
        }


        /// <summary>
        /// Time O(n*m), where n = length of first number and m = length of second number || Space O(m)
        /// Returns multiplication of two numbers very long numbers(can't fit in memory, numbers can be of 500+ digits long)
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="len1"></param>
        /// <param name="num2"></param>
        /// <param name="len2"></param>
        /// <returns></returns>
        public static string MultiplyLargeNumbersRepresentedAsString(string num1, int len1, string num2, int len2)
        {
            if (len1 < len2)
                MultiplyLargeNumbersRepresentedAsString(num2, len2, num1, len1);
            /* Keep longer no as num1
             * List of string to stored multiplication with each digit of num2 (second num)
             * for each digit in num2
             *      multipy each digit and keep carry which would be added to (mulitplication of next digits)
             *      at end if we have carry add it to the front of last multiplication result.
             * once we have multiplication with each digit, perform sum of all string stored in list, keep carry
             */

            string[] listOfMultiplications = new string[len2];

            // reversing the num2 so its easy to pick the digits one by one from start and multiply by num1
            num2 = ReverseString(num2);

            // fill 'listOfMultiplications' || Time O(len2*len1)
            for (int i = 0; i < len2; i++)
            {
                // append appropriate zeros to end of the string
                AppendZeros(ref listOfMultiplications[i], i);

                // multiply each digit and keep the carry which would be added to mulpilication of next digit
                int carry = 0;
                for (int j = len1 - 1; j >= 0; j--)             // starting from least significant digit
                {
                    int singlesMultiply = (int)Char.GetNumericValue(num2[i]) * (int)Char.GetNumericValue(num1[j]);
                    var carryAdded = singlesMultiply + carry;
                    listOfMultiplications[i] = carryAdded % 10 + listOfMultiplications[i];
                    carry = carryAdded / 10;
                }
                // append any left over carry to the result
                if (carry > 0) listOfMultiplications[i] = carry + listOfMultiplications[i];
            }

            // Now we have the result of multiplications with individual digits at each index,
            // sum all these rows(list of strings to get final result)
            return SumUpNumbersStoredAsListOfStrings(listOfMultiplications, len2);
        }

        public static string SumUpNumbersStoredAsListOfStrings(string[] numList, int len)
        {
            for (int i = 1; i < len; i++)
            {
                // if there is difference in len append extra 0 to start of the numbers to make them of same length
                var diffLen = numList[i].Length - numList[i - 1].Length;
                if (diffLen > 0)
                    AppendZeros(ref numList[i - 1], diffLen);
                else if (diffLen < 0)
                    AppendZeros(ref numList[i], Math.Abs(diffLen));

                int carry = 0;
                string sum = "";
                for (int j = numList[i].Length - 1; j >= 0; j--)
                {
                    int digitsSum = (int)Char.GetNumericValue(numList[i][j]) + (int)Char.GetNumericValue(numList[i - 1][j]);
                    var carryAdded = digitsSum + carry;
                    sum = carryAdded % 10 + sum;
                    carry = carryAdded / 10;
                }
                // append any left over carry to the result
                if (carry > 0) sum = carry + sum;
                numList[i] = sum;
            }
            return numList[len - 1];          // last string now has sum of all the numbers above it in the list
        }

        public static string ReverseString(string str)
        {
            var charArr = str.ToCharArray();
            Array.Reverse(charArr);
            return new string(charArr);
        }

        public static void AppendZeros(ref string s1, int count)
        {
            for (int i = 0; i < count; i++)
                s1 = "0" + s1;
        }

        // Time O(n) || Space O(1) as N = no of unique characters enteries in HashTable = ASCII value = 256
        public static bool IsomorphicStrings(string input, string pattern)
        {
            Dictionary<char, char> myDict = new Dictionary<char, char>();
            HashSet<char> valueExist = new HashSet<char>();
            int len = input.Length;     // pattern length
            if (pattern.Length != len)
                return false;

            for (int i = 0; i < len; i++)
            {
                if (myDict.ContainsKey(input[i]))
                {
                    if (myDict[input[i]] != pattern[i]) return false;       // Key's value doesn'T matches with current character
                }
                else                    // new character encountered
                {
                    if (valueExist.Contains(pattern[i])) return false;      // T key already associated with another char from S
                    else
                    {
                        myDict.Add(input[i], pattern[i]);                   // add key=char from S & its value=char from T
                        valueExist.Add(pattern[i]);                         // also add value to HashSet
                    }
                }
            }
            return true;
        }

        // Time O(n) || Space O(n-k)
        // Returns integer array containing max element for each sliding window for given input
        public static int[] SlidingWindowMaximum(int[] nums, int k)
        {
            var len = nums.Length;
            List<int> q = new List<int>();   // to hold useful elements in current window 'k'
            int[] result;

            //if (k >= len)                       // k == entire len of input, means there would be single element in O/P
            //{
            //    result = new int[1] { Int32.MinValue };
            //    foreach (var element in nums)
            //        result[0] = Math.Max(element, result[0]);
            //    return result;
            //}

            result = new int[len - k + 1];     // k < entire len of input, means there would be multiple element in O/P
            for (int i = 0; i < len; i++)
            {
                //// prepare initial Queue for first K elements
                //if (n < k - 1)
                //{
                //    // first remove all elements smallers than current element from back of queue till Queue is not empty
                //    while (q.Count > 0 && nums[q[q.Count - 1]] < nums[n])
                //        q.RemoveAt(q.Count - 1);
                //    // Insert new element index at end of the Queue
                //    q.Add(n);
                //}
                //else
                //{
                // first remove all elements smallers than current element from back of queue till Queue is not empty, also remove element which is out of the window
                while (q.Count > 0 && (nums[q[q.Count - 1]] < nums[i] || q.Count == k || q[0] == i - k))
                    if (nums[q[q.Count - 1]] < nums[i])
                        q.RemoveAt(q.Count - 1);
                    else //if (q.Count == k || q[0] == n - k)
                        q.RemoveAt(0);
                // Insert new element at end of the Queue
                q.Add(i);

                // Check to not add elements to result till initial Queue is created
                if (i < k - 1) continue;

                // add largest from last window n.e. Front of Queue to result array
                result[i - k + 1] = nums[q[0]];
                //}
            }
            return result;
        }

        // Time O(n) || Space O(1), 2-Pass Soln
        public static void SortArrayByParityII(int[] A)
        {
            int len = A.Length, even = 0, odd = 0;
            // sort array such that all even are on left and all odd r on right
            while (even < len)
            {
                if (A[even] % 2 == 0)
                { Utility.Swap(ref A[even], ref A[odd]); odd++; }
                even++;
            }
            even = 1;
            odd = len - 2;
            // now swap even every 2nd index to make the series even odd
            while (even < odd)
            {
                Utility.Swap(ref A[even], ref A[odd]);
                even += 2;
                odd -= 2;
            }
        }
        // Time O(n) Space O(1) 1-Pass Soln
        public static void SortArrayByParityII_OnePass(int[] nums)
        {
            int i = 0, odd = nums.Length - 1, even = nums.Length - 2;
            // swap no from the end if no is not present at its proper index n.e. Even not at even & odd not present at odd idx
            while (!(i > odd || i > even))   // while(n<nums.Length)
                if (i % 2 == 0)    // even index
                {
                    if (nums[i] % 2 == 1)    // odd no
                    {
                        var temp = nums[odd];
                        nums[odd] = nums[i];
                        nums[i] = temp;
                        odd -= 2;
                    }
                    else i++;
                }
                else        // odd index
                {
                    if (nums[i] % 2 == 0)    // even no
                    {
                        var temp = nums[even];
                        nums[even] = nums[i];
                        nums[i] = temp;
                        even -= 2;
                    }
                    else i++;
                }
        }


        // Time O(M*N), M = no of buildings in Matrix and N = no of empty spots in Matrix(time for one BFS traversal) || Space O(rows*cols)
        public static int ShortestDistanceFromAllBuildings(int[,] input, int rows, int cols)
        {
            if (input == null) return -1;

            int minDistance = Int32.MaxValue;           // To record 'count of no of buildings' present in matrix
            int buildingCount = 0;
            int[,] dist = new int[rows, cols];          // to store 'distance to from a spot/cell' to reach an building
            int[,] pass = new int[rows, cols];          // to store 'no of buildings reachable' from given spots/cell

            // For each building, update all the vacant spots which lead to the building along with Distance to reach using BFS
            // also update 'Pass' for each spot while doing BFS which would prove helpful later to know for how many buildings are reachable for a given spot/cell
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (input[r, c] == 1)               // if starting spot is building start mapping all spots/cell which lead to it
                    {
                        buildingCount++;
                        BFSFromEachBuilding(input, r, c, rows, cols, dist, pass);
                    }

            // Now simple check for spot/cell which has Minimum distance and also covers all buildings present in matrix
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (pass[r, c] == buildingCount)    // paths for no of buildings reachable from given spot match Building count
                        minDistance = Math.Min(minDistance, dist[r, c]);

            return minDistance != Int32.MaxValue ? minDistance : -1;
        }

        // Time O(n), N = no of empty spots in Matrix
        public static void BFSFromEachBuilding(int[,] input, int rowId, int colId, int rows, int cols, int[,] dist, int[,] pass)
        {
            bool[,] visited = new bool[rows, cols];     // will be used to mark spots already visited

            // Queue for BFS traversal which will store rowId,ColId
            Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>();

            // Add Starting Spot/Cell to Queue and mark it visited
            q.Enqueue(new KeyValuePair<int, int>(rowId, colId));
            visited[rowId, colId] = true;
            int distanceFromBuilding = 1;

            // Empty Marker to indicate current level is traversed
            q.Enqueue(new KeyValuePair<int, int>(-1, -1));

            while (q.Count > 0)
            {
                var currSpot = q.Dequeue();
                var row = currSpot.Key;
                var col = currSpot.Value;

                // current level Marker found increament distance from starting building
                if (row == col && col == -1)
                {
                    if (q.Count > 0)                    // still some spots/cell left to process, add another marker
                        q.Enqueue(new KeyValuePair<int, int>(-1, -1));
                    distanceFromBuilding++;
                    continue;
                }
                // top
                if (isValidSpot(row - 1, col, rows, cols, input, dist, pass, visited, distanceFromBuilding)) q.Enqueue(new KeyValuePair<int, int>(row - 1, col));
                // bottom
                if (isValidSpot(row + 1, col, rows, cols, input, dist, pass, visited, distanceFromBuilding)) q.Enqueue(new KeyValuePair<int, int>(row + 1, col));
                // left
                if (isValidSpot(row, col - 1, rows, cols, input, dist, pass, visited, distanceFromBuilding)) q.Enqueue(new KeyValuePair<int, int>(row, col - 1));
                // right
                if (isValidSpot(row, col + 1, rows, cols, input, dist, pass, visited, distanceFromBuilding)) q.Enqueue(new KeyValuePair<int, int>(row, col + 1));
            }
        }

        // Updates dist, pass & visited array // Time O(1)
        public static bool isValidSpot(int r, int c, int maxRow, int maxCol, int[,] input, int[,] dist, int[,] pass, bool[,] visited, int distance)
        {
            // Corner condition plus if already visited spot or came across obsctacle or came across another building
            if (r >= maxRow || r < 0 || c >= maxCol || c < 0 || visited[r, c] || input[r, c] == 1 || input[r, c] == 2) return false;

            dist[r, c] += distance;                     // add new distance to existing 'sum of distances' value at this spot
            pass[r, c]++;                               // increament counter to represent one more building reachable from here
            visited[r, c] = true;                       // mark this spot as visited

            return true;
        }


        /// <summary>
        /// Below function Derives the order of letters in alien language from list of non-empty words from the dictionary
        /// Let N be the total number of strings in the input list.
        /// Let C be the total length of all the words in the input list, added together.
        /// Let U be the total number of unique letters in the alien alphabet
        /// Time O(C) || Space O(U + min(U^2,N))
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string AlienDictionary(string[] input)
        {
            string lastword = "";
            // Stores each character as Graph Vertex(u) and the alphabets which should come after it n.e. its adjacent vertex(v)
            // Edge(u,v) meants 'u' should come before 'v' in the final ordering
            Dictionary<char, List<char>> graph = new Dictionary<char, List<char>>();
            foreach (var word in input)
            {
                // Add new Vertex in Graph
                foreach (char ch in word)
                    if (!graph.ContainsKey(ch)) graph.Add(ch, new List<char>());

                bool isNewWordPrefix = true;
                // Add dependencies to lastword with current word (add edges in graph)
                for (int i = 0; i < lastword.Length && i < word.Length; i++)
                    if (lastword[i] != word[i])
                    {
                        isNewWordPrefix = false;
                        // first check if reverse/cycle dependencies won't be created by adding edge from lastword[n]] -> Add(word[n]
                        if (CycleExists(graph, word[i], lastword[i])) return "";

                        // add new edge/dependencies
                        graph[lastword[i]].Add(word[i]);

                        // BreakOut now since remaining characters won't have impact on the ordering in dictionary
                        // Ex. for "abcd" & "aezf" so after ignoring starting same char 'a', here we catpure b->e
                        // but can't say anything about cd & zf relative order
                        break;
                    }

                if (isNewWordPrefix && word.Length < lastword.Length) return "";
                lastword = word;
            }
            return TopSort(graph);
        }

        // Topological Sorting // Time O(V+E), V = no of vertex, E no of edges
        public static string TopSort(Dictionary<char, List<char>> graph)
        {
            HashSet<char> visited = new HashSet<char>();        // to store visited vertices in graphs
            Stack<char> alienDict = new Stack<char>();
            foreach (var vertexU in graph)
                if (!visited.Contains(vertexU.Key))
                    TopSortUtil(graph, vertexU.Key, visited, alienDict);

            StringBuilder sb = new StringBuilder();
            while (alienDict.Count > 0)
                sb.Append(alienDict.Pop());

            return sb.ToString();
        }

        // Topological Sort using DFS
        public static void TopSortUtil(Dictionary<char, List<char>> graph, char vertexU, HashSet<char> visited, Stack<char> alienDict)
        {
            if (visited.Contains(vertexU)) return;              // if already visited return
            visited.Add(vertexU);

            foreach (var adjacentVertex in graph[vertexU])      // foreach dependencies/adjacentVertexV recursively call TopSortUtil
                if (!visited.Contains(adjacentVertex))
                    TopSortUtil(graph, adjacentVertex, visited, alienDict);

            alienDict.Push(vertexU);                            // after all adjacent vertex are visited append vertexU to top of stack;
        }

        public static bool CycleExists(Dictionary<char, List<char>> graph, char startingNode, char LastNode)
        {
            foreach (var adjacentVertexV in graph[startingNode])
                if (adjacentVertexV == LastNode) return true;   // path found
                else if (CycleExists(graph, adjacentVertexV, LastNode)) return true;    // also check if path exists from adjacent vertices to lastNode

            return false;
        }

        // Time O(n) || Space O(n)
        public static int[] ProductOfArrayExceptSelf(int[] nums, int len)
        {
            /* 2 - Pass Approach
            int l = nums.Length;
            int[] ltProduct = new int[l];
            int[] rtProduct = new int[l];
            int[] ans = new int[l];
            for (int n = l - 1; n >= 0; n--)         // O(n)
            {
                ltProduct[n] = rtProduct[n] = nums[n];
                if (n < l - 1) rtProduct[n] *= rtProduct[n + 1];   // product from of all elements on right
            }
            for (int n = 0; n < l; n++)            // O(n)
            {

                if (n > 0) ltProduct[n] *= ltProduct[n - 1];     // product from of all elements on left
                var ltP = n > 0 ? ltProduct[n - 1] : 1;
                var rtP = n < l - 1 ? rtProduct[n + 1] : 1;
                ans[n] = ltP * rtP;
            }
            return ans;
            */

            int[] leftProduct = new int[len];
            int[] rtProduct = new int[len];

            // calculate Product for all elements on the left of given index for each index
            leftProduct[0] = nums[0];               // copy 1st element directly
            for (int i = 1; i < len; i++)
                leftProduct[i] = nums[i] * leftProduct[i - 1];

            // calculate Product for all elements on the rt of given index for each index
            rtProduct[len - 1] = nums[len - 1];     // copy last element directly
            for (int i = len - 2; i >= 0; i--)
                rtProduct[i] = nums[i] * rtProduct[i + 1];

            int[] result = new int[len];
            for (int i = 0; i < len; i++)
                result[i] = (i - 1 < 0 ? 1 : leftProduct[i - 1]) * (i + 1 == len ? 1 : rtProduct[i + 1]);

            return result;
        }

        // Time O(n) || Space O(min(n,k))
        public static Tuple<bool, int, int> ContinuousSubarraySum(int[] nums, int k)
        {
            int sumUptoI = 0;
            Dictionary<int, int> moduloIndex = new Dictionary<int, int>();

            moduloIndex.Add(0, -1);
            for (int i = 0; i < nums.Length; i++)
            {
                sumUptoI += nums[i];                // update sum of elements till current index
                var mod = k != 0 ? sumUptoI % k : sumUptoI;

                /* if we have found an sum whose mod with 'k' is already present in HashTable,
                 * signifies there exists some index n whose sumI % k=x & index j whose sumJ % k=x
                 * Than there must be numbers in b/w n+1..j which have sum as in multiple of 'k'
                 * a%k = x
                 * b%k = x
                 * (a - b) %k = x -x = 0
                 * here a - b = the sum between n and j.
                 */
                if (moduloIndex.ContainsKey(mod))
                { if (i - moduloIndex[mod] > 1) return new Tuple<bool, int, int>(true, moduloIndex[mod] + 1, i); }
                else moduloIndex.Add(mod, i);
            }
            return new Tuple<bool, int, int>(false, -1, -1);
        }

        // Function adds two binary represented numbers and returns the binary sum as string
        // Time O(n) || Space O(n)
        public static string AddBinary(string a, string b)
        {
            var diffLen = a.Length - b.Length;
            if (diffLen < 0)
                while (diffLen++ != 0)  // append extra zero's to start of a
                    a = "0" + a;
            else if (diffLen > 0)
                while (diffLen-- != 0)  // append extra zero's to start of b
                    b = "0" + b;

            int carry = 0;              // carry
            StringBuilder sb = new StringBuilder();
            for (int i = a.Length - 1; i >= 0; i--)
            {
                var val = Char.GetNumericValue(a[i]) + Char.GetNumericValue(b[i]) + carry;
                if (val <= 1)           // val is 0 or 1
                { sb.Append(val); carry = 0; }
                else if (val == 2)
                { sb.Append("0"); carry = 1; }
                else
                { sb.Append("1"); carry = 1; }
                /* Instead of above 3 if else just use below
                 * sb.Append(val%2);
                 * carry = (val <= 1)? 0 : 1 ;
                 */
            }
            if (carry == 1)
                sb.Append("1");
            return new string(sb.ToString().Reverse().ToArray());
        }

        // Time O(n) || Space O(n)
        public static int[] TwoSum(int[] input, int targetVal)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();                             // Key = Target-currNum,Value = index of currNum
            for (int i = 0; i < input.Length; i++)
                if (dict.ContainsKey(input[i])) return new int[] { dict[input[i]], i };
                else if (!dict.ContainsKey(targetVal - input[i])) dict.Add(targetVal - input[i], i);     // not already present than add
            return new int[0];
        }

        // Function which returns List of Three Unique nums in input array whose SUM evaluate to Zero '0'
        // Time O(n^2) || Space O(1) If just Printing, O(n) if want to return the List of such nums
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var len = nums.Length;
            // Sort Array
            Sorting.Sort.Heapsort(ref nums);

            List<IList<int>> result = new List<IList<int>>();
            for (int i = 0; i < len; i++)
            {
                // current value is greater than zero, break from the loop. Remaining values cannot sum to zero
                if (nums[i] > 0) break;
                // If the current value is the same as the one before, skip it
                if (i > 0 && nums[i - 1] == nums[i]) continue;

                int start = i + 1, last = len - 1;
                while (start < last)
                {
                    if (nums[start] + nums[last] + nums[i] > 0)
                        last--;
                    else if (nums[start] + nums[last] + nums[i] < 0)
                        start++;
                    else // if(nums[start] + nums[last] + nums[n]==0)
                    {
                        result.Add(new List<int>() { nums[i], nums[start++], nums[last--] });
                        // Increment start prv value is same to avoid duplicates in the result.
                        while (start < last && nums[start - 1] == nums[start]) start++;
                    }
                }
            }
            return result;
        }

        // Function which returns List of Four Unique nums in input array whose SUM evaluate to Zero '0'
        // Time O(n^3) || Space O(1) If just Printing, O(n) if want to return the List of such nums
        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            var len = nums.Length;
            // Sort Array
            Sorting.Sort.Heapsort(ref nums);

            List<IList<int>> result = new List<IList<int>>();
            for (int first = 0; first < len; first++)
            {
                // If the current value is the same as the one before, skip it
                if (first > 0 && nums[first - 1] == nums[first]) continue;

                for (int second = first + 1; second < len; second++)
                {
                    // If the current value is the same as the one before, skip it
                    if (second > first + 1 && nums[second - 1] == nums[second]) continue;

                    int third = second + 1, fourth = len - 1;
                    while (third < fourth)
                    {
                        int sum = nums[first] + nums[second] + nums[third] + nums[fourth];
                        if (sum > target)
                            fourth--;
                        else if (sum < target)
                            third++;
                        else // if(sum==target)
                        {
                            result.Add(new List<int>() { nums[first], nums[second], nums[third++], nums[fourth--] });
                            // Increment start prv value is same to avoid duplicates in the result.
                            while (third < fourth && nums[third - 1] == nums[third]) third++;
                        }
                    }
                }
            }
            return result;
        }

        // Given four arrays A, B, C, D of integer values, compute how many tuples (n, j, k, l) there are such that A[n] + B[j] + C[k] + D[l] is zero.
        // Time O(N^2) || Space O(N^2)
        public static int FourSumCount(int[] A, int[] B, int[] C, int[] D)
        {
            int len = A.Length, zeroSumCount = 0;
            Dictionary<int, int> sumCount = new Dictionary<int, int>(len * len);
            // Compute and Store every possible sum from 1st two arrays with their frequency of occurence
            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    if (sumCount.ContainsKey(A[i] + B[j])) sumCount[A[i] + B[j]]++;
                    else sumCount.Add(A[i] + B[j], 1);
            // Now check every possible sum of last 2 arrays it its excat -ve is present in Dictionary
            // than increment 'zeroSumCount' by frequency given for that sum in Dictionary
            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    if (sumCount.ContainsKey(-(C[i] + D[j])))
                        zeroSumCount += sumCount[-(C[i] + D[j])];
            return zeroSumCount;
        }


        // Func() returns the most common repeating word in paragraph which is not in set of banned words
        // Time O(n), n = length of the input paragraph || Space O(n)
        public static string MostCommonWord(string paragraph, string[] banned)
        {
            /* Add all banned words to HashSet to reference later in O(1) time
             * Take each word in paragraph convert to it lower case and add to Dictionary<string,int> to maintain the count how many times it was seen in paragraph
             * while adding word to Dictionary escape last char if its ASCII value is <65 n.e. Not a alphabet
             * Remove all the word in Dictionary which are in Set of Banned words
             * Iterate thru the Dictionary to find the word with Max occurence
             */

            HashSet<string> bannedSet = new HashSet<string>(banned);
            HashSet<char> punctuation = new HashSet<char>(new char[] { '!', '?', '\'', ',', ';', '.' });
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int maxCount = 0;
            string commonWord = "";

            // convert to lower case and split by spaces
            foreach (var lowersCaseSpaceSplittedWord in paragraph.ToLower().Split(' '))
            {
                foreach (var commaSeperatedWord in lowersCaseSpaceSplittedWord.Split(','))
                {
                    if (commaSeperatedWord == "") continue;

                    var word = commaSeperatedWord;

                    // check if last char is punctuation symbols !?',;.
                    while (punctuation.Contains(word[word.Length - 1]))
                        word = word.Substring(0, word.Length - 1);

                    // skip to next word is current word is banned
                    if (bannedSet.Contains(word)) continue;

                    // current word already present in dictionary increament count by 1
                    if (dict.ContainsKey(word)) dict[word]++;
                    else dict.Add(word, 1);

                    if (dict[word] > maxCount)
                    { maxCount = dict[word]; commonWord = word; }
                }
            }
            return commonWord;
        }


        // Func() returns True is given partially filled board of SUDOKO is valid or not
        // Time O(n^2) || Space O(n), where n = no of rows/col of Sudoko n.e. 9 on reducing we can express both time & space as O(1) as it's always going to be fixed val of 9x9=81 
        public static bool ValidSudoku(char[][] board)
        {
            int rows = board.Length;
            int cols = board[0].Length;
            HashSet<char> rowPresence = new HashSet<char>();
            HashSet<char>[] colPresence = new HashSet<char>[cols];
            HashSet<char>[] subGrid = new HashSet<char>[rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                    if (board[r][c] == '.') continue;
                    else
                    {
                        var ch = board[r][c];

                        if (rowPresence.Contains(ch)) return false;      // if an char already present in same row return false
                        else rowPresence.Add(ch);

                        if (colPresence[c] == null) colPresence[c] = new HashSet<char>();
                        if (colPresence[c].Contains(ch)) return false;      // if an char already present in same col return false
                        else colPresence[c].Add(ch);

                        var rIndex = r / 3;
                        var cIndex = c / 3;
                        var gridID = 3 * rIndex + cIndex;

                        if (subGrid[gridID] == null) subGrid[gridID] = new HashSet<char>();
                        if (subGrid[gridID].Contains(ch)) return false;
                        else subGrid[gridID].Add(ch);
                    }
                // reset row HashSet
                rowPresence.Clear();
            }
            return true;
        }

        /// <summary>
        /// Function which solves a Sudoku puzzle by filling the empty cells.
        /// Sudoku solution must satisfy all of the following rules:
        ///     Each of the digits 1-9 must occur exactly once in each row.
        ///     Each of the digits 1-9 must occur exactly once in each column.
        ///     Each of the digits 1-9 must occur exactly once in each of the 9 3x3 sub-boxes of the grid.
        /// Time O(9^(N*N)) as every unassigned index has 9 possible options || Space O(3*9*10) ~O(1)
        /// </summary>
        /// <param name="board"></param>
        public static void SudokuSolver(char[][] board)
        {
            bool[,] rowValidator = new bool[9, 10];
            bool[,] colValidator = new bool[9, 10];
            bool[,] blockValidator = new bool[9, 10];
            int count = 0;
            // Read currently present numbers on board
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    if (board[r][c] != '.')
                    {
                        count++;
                        int index = (board[r][c] - '0');
                        rowValidator[r, index] = true;
                        colValidator[c, index] = true;
                        blockValidator[(r / 3) * 3 + c / 3, index] = true;
                    }
            // Fill Empty Cells
            fillUsingDFS(board, 0, 0, rowValidator, colValidator, blockValidator, ref count);
        }
        public static void fillUsingDFS(char[][] board, int r, int c, bool[,] rowValidator, bool[,] colValidator, bool[,] blockValidator, ref int count)
        {
            // Empty Cell Found
            while (r < 9 && c < 9)
            {
                if (board[r][c] == '.') break;

                // move to next col
                if (c < 8) c++;
                // if above move not possible move to next row 1st col
                else { r++; c = 0; }
            }

            if (r < 9 && c < 9)
            {
                // Try fitting all possible numbers until we fill all empty slots with valid nums
                for (int no = 1; no <= 9; no++)
                    // If Current Number Satisfies all three condition
                    if (rowValidator[r, no] == false && colValidator[c, no] == false && blockValidator[(r / 3) * 3 + c / 3, no] == false)
                    {
                        board[r][c] = (char)(no + '0');
                        // Mark current cell filled
                        rowValidator[r, no] = colValidator[c, no] = blockValidator[(r / 3) * 3 + c / 3, no] = true;
                        ++count;

                        fillUsingDFS(board, r, c, rowValidator, colValidator, blockValidator, ref count);

                        if (count >= 81) return;

                        // Mark current cell as Not Filled
                        rowValidator[r, no] = colValidator[c, no] = blockValidator[(r / 3) * 3 + c / 3, no] = false;

                        --count;
                    }
                // BACK-TRACK, If no sutaible number found revert the cell back to empty
                board[r][c] = '.';
            }
        }

        // Time O(9^(N*N)) as every unassigned index has 9 possible options || Space O(1) || Auxillary Space O(N*N), N = 9
        public static void SudokuSolverEfficient(char[][] b)
        {
            /* ALGO
            We need 3 individual Hashset of size 9 to check if a given digit has been seen before or not in a row, column & grid
            Now we simply go thru original board and mark all the digits in their respective rows, columns and grids

            Now we start recursively filling all cells with '.'
            we try all digits from 1...9
            which ever yields in true we return from that point

            if non of digits yields in success means we made a wrong entry somewhere earlier hence assign back '.' and return false

            in the end if all the cells are filled return true
             */
            int n = 9;
            bool[,] rCheck = new bool[n, n + 1], cCheck = new bool[n, n + 1], gCheck = new bool[n, n + 1];
            // fill the indicator for rows, columns & grid with current digits for each cell in board
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (b[r][c] != '.')
                    {
                        var digit = b[r][c] - '0';
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;
                    }
            // Fill remaining cells using DFS in place
            Fill(0, 0);

            // local helper func
            bool Fill(int r, int c)
            {
                // if reached end of curent row move to 1st column of next row
                if (c == n)
                {
                    r = r + 1;
                    c = 0;
                }
                // if all cells are filled return true
                if (r == n) return true;

                // if found a pre-filled digit move to next cell
                if (b[r][c] != '.')
                    return Fill(r, c + 1);

                for (int digit = 1; digit < 10; digit++)
                    //If Digit not Already Present
                    if (!(rCheck[r, digit] || cCheck[c, digit] || gCheck[GridID(r, c), digit]))
                    {
                        // mark current digit
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = true;

                        // add digit char to board
                        b[r][c] = (char)(digit + '0');

                        // stop as soon as we get a answer
                        if (Fill(r, c + 1)) return true;

                        // un mark current digit
                        rCheck[r, digit] = cCheck[c, digit] = gCheck[GridID(r, c), digit] = false;
                    }
                // if none of the digits work than we revert to original state
                b[r][c] = '.';
                return false;
            }
            // for given row and columns return the approriate GridID
            int GridID(int r, int c) => 3 * (r / 3) + (c / 3);
        }

        // Time O(Log2(N)), log of base 2 N || Space O(1)
        public static int ComplimenetBase10(int num)
        {
            if (num == 0) return 1;
            int ans = 0;
            int multiplyer = 1;     // for calculating power of 2
            while (num != 0)
            {
                var currBit = num & 1;
                num >>= 1;          // right shifting the num can also use num/=2;
                ans += multiplyer * (1 - currBit);  // Number in Base 10 can be represented as sum of powers of 2 (e.g. 5 which in binary is 101 = 2^2 + 2^0 )
                multiplyer <<= 1;   // for calculating power of 2 (multiplyer *=2)
            }
            return ans;
        }

        public static int ComplimenetBase10Faster(int num)
        {
            if (num == 0) return 1;
            int ans = 0;
            int multiplyer = 1;     // for calculating power of 2
            while (multiplyer <= num)
            {
                if ((multiplyer & num) == 0) ans += multiplyer;
                multiplyer <<= 1;   // multiplyer *=2;
            }
            return ans;
        }

        /// <summary>
        /// Time O(LogBase2(N)) as their can be atmost || Space O(1)
        /// Explanation for given num ex- 100 LogBase10(num) gives 2 adding 1 to ans gives no of digits required to represent the number in that base.
        /// Similar for num = 5, LogBase2(num) will give 2 adding 1 to it give 3 which is excatly no of bits required to represent the num in binary
        /// Now right shifting 1 by no of digits required give a numbers which has just 1 bit set a 1 rest all zero
        /// For 5 n.e. 101
        /// 'NoOfBitsToRepresent' = 2 + 1 = 3
        /// Now 'Mask' can be calculated by right shifting 1 NoOfBitsToRepresent times n.e. 1<< 3 = 8 i.e. 1000 in binary
        /// Now subtracting 1 from 'Mask' us all bits reversed except the left most bit
        /// 8 - 1 or 1000 - 1 = 0111
        /// XOR of (Mask-1) & num = 111 ^ 101 = 010 = 2 in decimal
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ComplimenetBase10Fastest(int num) => (num == 0) ? 1 : (num ^ ((1 << (int)(Math.Log(num, 2) + 1)) - 1));

        // Reads the elements from all four border n spiralling inwards till no more element is left to read
        // Time O(N), N = rows*cols n.e, No of elements in matrix || Space O(N)
        public static IList<int> SpiralMatrix(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1) return new List<int>();
            var row = matrix.Length;
            var col = matrix[0].Length;
            List<int> spiralOrder = new List<int>(row * col);
            int top = 0, right = col - 1, bottom = row - 1, left = 0;
            int i = 0, j = 0;
            while (bottom >= top || right >= left)
            {
                // top traversal
                if (j <= right)
                {
                    while (j <= right)
                        spiralOrder.Add(matrix[i][j++]);
                    top++;                  // update boundary
                    i++;
                    j--;
                }
                else break;
                // right traversal
                if (i <= bottom)
                {
                    while (i <= bottom)
                        spiralOrder.Add(matrix[i++][j]);
                    right--;                // update boundary
                    j--;
                    i--;
                }
                else break;
                // bottom traversal
                if (j >= left)
                {
                    while (j >= left)
                        spiralOrder.Add(matrix[i][j--]);
                    bottom--;               // update boundary
                    i--;
                    j++;
                }
                else break;
                // left traversal
                if (i >= top)
                {
                    while (i >= top)
                        spiralOrder.Add(matrix[i--][j]);
                    left++;                 // update boundary
                    j++;
                    i++;
                }
                else break;
            }

            return spiralOrder;
        }
        // Time O(rows*cols) | Space O(1)
        public static List<int> SpiralOrder_Elegant(int[][] mat)
        {
            int topBoundry = 0, rightBoundry = mat[0].Length - 1, bottomBoundry = mat.Length - 1, leftBoundry = 0, r = 0, c = -1, dir = 0, total = mat[0].Length * mat.Length, i = 0;
            List<int> ans = new List<int>();
            while (++i <= total)
                ans.Add(GetNextVal());
            return ans;

            // local helper func
            int GetNextVal()
            {
                switch (dir)
                {
                    case 0: // lt->rt
                        {
                            if (++c > rightBoundry)
                            {
                                r = ++topBoundry;
                                c = rightBoundry;
                                ++dir;
                            }
                            break;
                        }
                    case 1: // top->bottom
                        {
                            if (++r > bottomBoundry)
                            {
                                r = bottomBoundry;
                                c = --rightBoundry;
                                ++dir;
                            }
                            break;
                        }
                    case 2: // rt->lt
                        {

                            if (--c < leftBoundry)
                            {
                                r = --bottomBoundry;
                                c = leftBoundry;
                                ++dir;
                            }
                            break;
                        }
                    case 3: // bottom->top
                        {
                            if (--r < topBoundry)
                            {
                                r = topBoundry;
                                c = ++leftBoundry;
                                dir = (++dir) % 4;
                            }
                            break;
                        }
                }
                return mat[r][c];
            }
        }

        // Time O(N), N = no of moves given in input || Space O(9) ~O(1) 
        public static string WinnerOfTicTacToe(int[][] moves)
        {
            // to count A's & B's presense across 3 rows + 3 cols + 2 diagonal
            Dictionary<string, int>[] direction = new Dictionary<string, int>[8];
            for (int i = 0; i < moves.Length; i++)
            {
                string ch = i % 2 == 0 ? "A" : "B";
                var row = moves[i][0];
                var col = moves[i][1];

                if (direction[row] == null) direction[row] = new Dictionary<string, int>();
                if (direction[col + 3] == null) direction[col + 3] = new Dictionary<string, int>();

                // update rows
                if (direction[row].ContainsKey(ch)) direction[row][ch]++;
                else direction[row].Add(ch, 1);
                // check if any player won
                if (direction[row][ch] == 3) return ch;


                // update columns
                if (direction[col + 3].ContainsKey(ch)) direction[col + 3][ch]++;
                else direction[col + 3].Add(ch, 1);
                // check if any player won
                if (direction[col + 3][ch] == 3) return ch;

                // update top left - bottom rt diagonal
                if (row == col)
                {
                    if (direction[6] == null) direction[6] = new Dictionary<string, int>();
                    if (direction[6].ContainsKey(ch)) direction[6][ch]++;
                    else direction[6].Add(ch, 1);
                    // check if any player won
                    if (direction[6][ch] == 3) return ch;
                }
                // update top rt - bottom left diagonal
                if (row + col == 2)
                {
                    if (direction[7] == null) direction[7] = new Dictionary<string, int>();
                    if (direction[7].ContainsKey(ch)) direction[7][ch]++;
                    else direction[7].Add(ch, 1);
                    // check if any player won
                    if (direction[7][ch] == 3) return ch;
                }
            }

            return moves.Length < 9 ? "Pending" : "Draw";
        }
        // Time = Space = O(r^2) ~O(9) ~O(1), r = no of rows in tic-tak
        public static string WinnerOfTicTacToe_Clean(int[][] moves)
        {
            Dictionary<char, int>[] dict = new Dictionary<char, int>[8];
            for (int i = 0; i < dict.Length; i++)
                dict[i] = new Dictionary<char, int>() { { 'A', 0 }, { 'B', 0 } };

            for (int i = 0; i < moves.Length; i++)
            {
                var r = moves[i][0];
                var c = moves[i][1];
                var player = i % 2 == 0 ? 'A' : 'B';
                // Mark Row                     || Mark Col                     || Mark Lt-Diag                       || Mark Rt-Diag
                if (++dict[r][player] >= 3 || ++dict[c + 3][player] >= 3 || (r == c && ++dict[6][player] >= 3) || (r + c == 2 && ++dict[7][player] >= 3))
                    return player + "";
            }
            return moves.Length == 9 ? "Draw" : "Pending";
        }


        public static string[] ReorderDataInLogFiles(string[] logs)
        {
            // Array.Sort(logs, new LogComparetor());
            var logsList = logs.ToList();
            logsList.Sort(new LogComparetor());
            return logsList.ToArray();
        }

        // Time O(nlogn) || Space O(1)
        public class LogComparetor : IComparer<String>
        {
            public int Compare(String x, String y)
            {
                // cast the object into string and splilt the string into 2 halves from 1st space character
                string[] log1 = x.Split(new char[] { ' ' }, 2);
                string[] log2 = y.Split(new char[] { ' ' }, 2);

                // check the first character of 2nd element is digit or not
                bool is1DigitOrWord = Char.IsDigit(log1[1][0]);
                bool is2DigitOrWord = Char.IsDigit(log2[1][0]);

                if (!is1DigitOrWord && !is2DigitOrWord)     // both are words logs
                {
                    // compare the content
                    int cmp = log1[1].CompareTo(log2[1]);
                    if (cmp != 0) return cmp;

                    // both logs have same word, than compare the identifiers
                    return log1[0].CompareTo(log2[0]);
                }
                else if (!is1DigitOrWord && is2DigitOrWord) // first is word & second is digit
                    return -1;
                else if (is1DigitOrWord && !is2DigitOrWord) // first is digit & second is word
                    return 1;
                else                                        // both are digit logs
                    return 0;
            }
        }

        /// <summary>
        /// Time (nlogn) dependent on sorting || Space O(n) since we are using addtional m/r to convert array to list
        /// 1) Separate strings and digits
        /// 2) Sort strings with comparer
        /// 3) Append digits
        /// 4) Return as array
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static string[] ReorderDataInLogFilesByDividingDigitsAndWordLogs(string[] logs)
        {
            var digitLog = new List<string>();
            var aplhabetLog = new List<string>();

            foreach (var log in logs)
                if (Char.IsDigit(log.Split(' ')[1][0])) digitLog.Add(log);
                else aplhabetLog.Add(log);

            aplhabetLog.Sort(new LogComparetorWord());
            aplhabetLog.AddRange(digitLog);
            return aplhabetLog.ToArray();
        }

        public class LogComparetorWord : IComparer<String>
        {
            public int Compare(String x, String y)
            {
                // cast the object into string and splilt the string into 2 halves from 1st space character
                string[] log1 = x.Split(new char[] { ' ' }, 2);
                string[] log2 = y.Split(new char[] { ' ' }, 2);

                var cmp = log1[1].CompareTo(log2[1]);       // compare the values
                if (cmp != 0) return cmp;
                else
                    return log1[0].CompareTo(log2[0]);      // if values are same compare identifier of log
            }
        }


        /// <summary>
        /// Func returns elements with more than length/2 majority, Using Boyer-Moore Voting
        /// Time O(n) || Space O(1)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MajorityElement(int[] nums)
        {
            int? majority = null;
            int count = 0;
            // Boyer-Moore Voting Algorithm
            foreach (var num in nums)
            {
                if (count == 0)
                {
                    majority = num;
                    count = 1;
                }
                else if (num == majority) count++;
                else count--;
            }
            return (int)majority;
        }

        // Func returns Two elements with more than length/3 majority if they exists, Using Boyer-Moore Voting
        // Time O(n) || Space O(1)
        public static IList<int> MajorityElementII(int[] nums)
        {
            int? m1 = null, m2 = null;
            int c1 = 0, c2 = 0;
            foreach (var num in nums)
            {
                if (c1 == 0)
                    if (m2 == null || m2 != num) { c1 = 1; m1 = num; }
                    else if (num == m1) c1++;
                    else if (c2 == 0)
                        if (m1 == null || m1 != num) { c2 = 1; m2 = num; }
                        else if (num == m2) c2++;
                        // new num doesnt matches either majority number so far
                        else if (num != m1 && num != m2) { c1--; c2--; }
            }

            c1 = c2 = nums.Length / 3;
            // verify m1 & m2 are in majority
            foreach (var num in nums)
            {
                if (num == m1) c1--;
                else if (num == m2) c2--;
            }
            var result = new List<int>(2);
            if (c1 < 0 && m1 != null) result.Add((int)m1);
            if (c2 < 0 && m2 != null) result.Add((int)m2);
            return result;
        }

        // Time O(nlogn) || Spacec O(n)
        public static int MeetingRoomsII(int[][] intervals)
        {
            // Sort by StartTime
            var sortedStartTime = (from meeting in intervals
                                   orderby meeting[0]
                                   select meeting[0]).ToArray();

            // Sort by EndTime
            var sortedEndTime = (from meeting in intervals
                                 orderby meeting[1]
                                 select meeting[1]).ToArray();

            // ALGO: check each if meeting start time is smaller than last meeting end time if so +1 currNoOfRooms else -1, keep count of max
            int minNoOfRooms = 0, currNoOfRooms = 0, i = 0, j = 0;
            while (i < sortedEndTime.Length)
                if (sortedStartTime[i] < sortedEndTime[j])
                { i++; minNoOfRooms = Math.Max(minNoOfRooms, ++currNoOfRooms); }
                else
                { j++; --currNoOfRooms; }

            return minNoOfRooms;
        }


        // From given m x n 2D Grid map of '1's (land) and '0's (water), return the number of islands
        // Time O(n) || Space O(1)
        public static int NumberOfIslands(char[][] grid)
        {
            int NumIslands = 0;
            var rows = grid.Length;
            var cols = grid[0].Length;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (grid[r][c] == '1')
                    {
                        NumIslands++;
                        DFS(grid, rows, cols, r, c);
                        // BFS(grid, rows, cols, r, c);
                    }
            return NumIslands;
        }

        public static void DFS(char[][] grid, int rows, int cols, int curR, int curC)
        {
            // if invalid Row               or invalid column               or already visited node
            if ((curR < 0 || curR >= rows) || (curC < 0 || curC >= cols) || grid[curR][curC] == '0') return;
            // else mark current node visited
            grid[curR][curC] = '0';

            // since vertical & horizontal directions are allowed, do recursive DFS on these 4 directions
            DFS(grid, rows, cols, curR - 1, curC);
            DFS(grid, rows, cols, curR + 1, curC);
            DFS(grid, rows, cols, curR, curC - 1);
            DFS(grid, rows, cols, curR, curC + 1);
        }

        public static void BFS(char[][] grid, int rows, int cols, int curR, int curC)
        {
            // if invalid Row               or invalid column               or already visited node
            if ((curR < 0 || curR >= rows) || (curC < 0 || curC >= cols) || grid[curR][curC] == '0') return;

            Queue<KeyValuePair<int, int>> q = new Queue<KeyValuePair<int, int>>();
            q.Enqueue(new KeyValuePair<int, int>(curR, curC));
            grid[curR][curC] = '0';

            while (q.Count > 0)
            {
                var node = q.Dequeue();
                var row = node.Key;
                var col = node.Value;

                if (!((row - 1 < 0 || row - 1 >= rows) || (col < 0 || col >= cols) || grid[row - 1][col] == '0'))
                { grid[row - 1][col] = '0'; q.Enqueue(new KeyValuePair<int, int>(row - 1, col)); }
                if (!((row + 1 < 0 || row + 1 >= rows) || (col < 0 || col >= cols) || grid[row + 1][col] == '0'))
                { grid[row + 1][col] = '0'; q.Enqueue(new KeyValuePair<int, int>(row + 1, col)); }
                if (!((row < 0 || row >= rows) || (col - 1 < 0 || col - 1 >= cols) || grid[row][col - 1] == '0'))
                { grid[row][col - 1] = '0'; q.Enqueue(new KeyValuePair<int, int>(row, col - 1)); }
                if (!((row < 0 || row >= rows) || (col + 1 < 0 || col + 1 >= cols) || grid[row][col + 1] == '0'))
                { grid[row][col + 1] = '0'; q.Enqueue(new KeyValuePair<int, int>(row, col + 1)); }
            }
        }

        // Func() takes input of non-negative integers representing an elevation map where the width of each bar is 1,
        // and compute how much water it can trap after raining.
        // Time O(n) || Space O(n)
        public static int TrapRainWater_Stack(int[] height)
        {
            /* a) Insert elevation in stack before storing calculate,
             *      water than could be stored b/w current and stack top
             *      Calculate the length of tank
             *      and multiply length with Min of two's height
             *      add it to the totalWater collected so far
             *      now remove the Stack Top if its <= current elevation,
             *      and repeat till we either stack is empty or break out when get elevation > than current
             * b) Keep updating last Height as we dont want to add vol of water which we have alreaedy calculated previous
             * c) Repeat above steps till we have reach end of heights array.
             */
            Stack<int> st = new Stack<int>(height.Length);
            int waterCollected = 0;
            for (int i = 0; i < height.Length; i++)
            {
                int lastHeight = 0;
                // Calculate possible water that got collection by comparing current elevation with ones present in stack
                while (st.Count > 0)
                {
                    var top = st.Peek();
                    var lengthOfBucket = i - top - 1;
                    if (lengthOfBucket > 0)
                        waterCollected += lengthOfBucket * (Math.Min(height[i], height[top]) - lastHeight);
                    lastHeight = height[top];

                    // if last elevation was smaller we have already calculated water b/w it and current hence pop
                    if (lastHeight <= height[i]) st.Pop();
                    // process stop when hit last elevation which is bigger than current
                    else break;
                }
                // Insert new elevation index
                st.Push(i);
            }
            return waterCollected;
        }

        // Time O(n) Space O(1)
        // https://youtu.be/ZI2z5pq0TqA
        public static int TrapRainWater_2Pointer(int[] height)
        {
            int lt = 0, rt = height.Length - 1, trapped = 0, ltMax = 0, rtMax = 0;
            while (lt < rt)
                // we calculate the water stored right above the current index only
                if (height[lt] < height[rt])
                {
                    ltMax = Math.Max(ltMax, height[lt]);
                    trapped += ltMax - height[lt];
                    lt++;
                }
                else
                {
                    rtMax = Math.Max(rtMax, height[rt]);
                    trapped += rtMax - height[rt];
                    rt--;
                }
            return trapped;


            #region Time = Space = O(n)
            //int l = height.Length, trapped = 0;
            //int[] ltMax = new int[l];
            //int[] rtMax = new int[l];
            //ltMax[0] = rtMax[l - 1] = 0;
            //for (int n = 1; n < l; n++)
            //    ltMax[n] = Math.Max(height[n - 1], ltMax[n - 1]);
            //for (int n = l - 2; n >= 0; n--)
            //    rtMax[n] = Math.Max(height[n + 1], rtMax[n + 1]);

            //for (int n = 0; n < l; n++)
            //    // we only want to add +ve water units, and water at current idx which is bascially min of left max, rt max - curr ht
            //    trapped += Math.Max(0, Math.Min(ltMax[n], rtMax[n]) - height[n]);
            //return trapped;
            #endregion
        }


        // Returns True if string is Palindrome
        public static bool IsPalindrome(string str)
        {
            int start = 0, last = str.Length - 1;
            while (start < last)
            {
                if (!Char.IsLetterOrDigit(str[start])) { start++; continue; }
                else if (!Char.IsLetterOrDigit(str[last])) { last--; continue; }
                else if (str[start] != str[last]) return false;
                start++;
                last--;
            }
            return true;
        }



        /// <summary>
        /// Returns true if word exists in given grid bny searching for consecutive word in vertical/ horizontal direction
        /// Time O(N*(4*3^Min(L-1, N-1)) ~O(N*3^L), N = no of cells on board, L = length of word being searched
        ///     Initially we have at most 4 directions to explore, but further the choices are reduced into 3 (since we won't go back to where we come from.
        /// Space O(L) for recursion stack + Auxillary Space O(N) for visited Grid
        ///     Auxillary space can be saved if we can edit original Grid to mark visited cells with '#' while traversing n reverting back when back-tracking
        /// </summary>
        /// <param name="board"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool WordProblem(char[][] board, string word)
        {
            if (board == null || board.Length < 0) return false;
            var rows = board.Length;
            var cols = board[0].Length;

            var visited = new bool[rows, cols];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (DFS(board, word, visited, rows, cols, r, c, 0))
                        return true;
            return false;
        }
        public static bool DFS(char[][] board, string word, bool[,] visited, int rows, int cols, int r, int c, int level = 0)
        {
            // Match found
            if (level >= word.Length) return true;

            // if invalid Row  or invalid column  or already visited node or word doesn't matches
            if ((r < 0 || r >= rows) || (c < 0 || c >= cols) || visited[r, c] || board[r][c] != word[level]) return false;
            // mark current node as visited
            visited[r, c] = true;
            // traverse in all 4 possible directions, return true if any one returns desired result
            if (DFS(board, word, visited, rows, cols, r - 1, c, level + 1)) return true;
            if (DFS(board, word, visited, rows, cols, r + 1, c, level + 1)) return true;
            if (DFS(board, word, visited, rows, cols, r, c - 1, level + 1)) return true;
            if (DFS(board, word, visited, rows, cols, r, c + 1, level + 1)) return true;

            // mark current node as Un-Visited
            visited[r, c] = false;

            return false;
        }

        // Function returns all words present in 2D board matching given list of words
        // Time O(N*(4*3^Min(L-1, N-1)) ~O(N*3^L) for explanation see: WordProblem() above
        public static IList<string> WordProblemII(char[][] board, string[] words)
        {
            /* Generate Trie for list of words
             * Find all possilbe words in board, essential optimization is to stop when curr Word prefix doesn't matches with any word in above trie
             * if a word matched insert it into result
             */
            if (board == null || board.Length < 0 || words.Length <= 0) return new List<string>();
            var rows = board.Length;
            var cols = board[0].Length;

            Trie t = new Trie();
            foreach (var word in words)
                t.Add(word.ToCharArray());

            HashSet<string> result = new HashSet<string>(); // stores output
            List<char> currWord = new List<char>();             // hold current word in Grid while performing DFS

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    DFS(board, currWord, result, t, rows, cols, r, c);

                    // all words found than we can break out, no need to keep searching
                    if (result.Count == words.Length) break;
                }

            return result.ToList<string>();
        }
        public static void DFS(char[][] board, List<char> currWord, HashSet<string> result, Trie t, int rows, int cols, int r, int c)
        {
            // if invalid Row/column or Prefix doesn't exists hence we need not dive deeper to discover new possible words in GRID
            if ((r < 0 || r >= rows) || (c < 0 || c >= cols) || !t.SearchPrefix(currWord)) return;

            char originalChar = board[r][c];
            // add current Node character to currWord
            currWord.Add(originalChar);

            // If current word exists add it to result
            if (t.SearchWord(currWord)) result.Add(new string(currWord.ToArray()));

            // mark current node as Visited
            board[r][c] = '#';

            // traverse in all 4 possible directions, return true if any one returns desired result
            DFS(board, currWord, result, t, rows, cols, r - 1, c);
            DFS(board, currWord, result, t, rows, cols, r + 1, c);
            DFS(board, currWord, result, t, rows, cols, r, c - 1);
            DFS(board, currWord, result, t, rows, cols, r, c + 1);

            // mark current node as Un-Visited
            board[r][c] = originalChar;
            // remove current Node character from currWord
            currWord.RemoveAt(currWord.Count - 1);
        }


        // Time O(N*(4*3^Min(L-1, N-1)) ~O(N*3^L) 
        public static IList<string> WordProblemIIEfficient(char[][] board, string[] words)
        {
            Trie trieObj = new();
            // add all the words in the Trie data-structure
            foreach (var word in words)
                trieObj.Add(word.ToCharArray());

            int rows = board.Length, cols = board[0].Length;
            HashSet<string> result = new();
            List<char> ls = new();
            // start DFS from each cell on the board to see if any valid word can be created from there
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    DFS(r, c, ls, trieObj.root);
                    // all words found than we can break out, no need to keep searching
                    if (result.Count == words.Length) break;
                }

            return result.ToList();

            // local helper func
            void DFS(int r, int c, List<char> curWord, InterviewProblemNSolutions.TrieNode node)
            {
                if (r < 0 || r >= rows || c < 0 || c >= cols || !node.children.TryGetValue(board[r][c], out InterviewProblemNSolutions.TrieNode val)) return;
                var originalChar = board[r][c];

                // mark visited
                board[r][c] = '#';
                curWord.Add(originalChar);

                // if current word exists add to result
                if (val.isWord)
                    result.Add(val.word);

                DFS(r - 1, c, curWord, val);
                DFS(r + 1, c, curWord, val);
                DFS(r, c - 1, curWord, val);
                DFS(r, c + 1, curWord, val);

                // mark un-visited
                board[r][c] = originalChar;
                curWord.RemoveAt(curWord.Count - 1);
            }
        }


        // Time O((N-L)*L), N = len of haystack & L = len of needle being search, Space O(1)
        public static int ImplementIndexOf(string haystack, string needle)
        {
            int nLen = needle.Length, hLen = haystack.Length;
            if (nLen <= 0) return 0;

            for (int i = 0; i < hLen - nLen + 1; i++)
            {
                int index = 0;
                while (index < nLen && index + i < hLen && haystack[index + i] == needle[index])
                    index++;
                if (index == nLen) return i;
            }
            return -1;
        }

        // Time Best case O(N+L) worst O(NxL)|| Space O(1), N = len of haystack & L = len of needle being search, Space O(1)
        public static int ImplementIndexOfRabinKarp(string haystack, string needle)
        {
            int nLen = needle.Length, hLen = haystack.Length;
            if (nLen <= 0) return 0;
            if (hLen < nLen) return -1;
            var mod = 1009;     // PrimeNo can also use something like Math.Powr(2,31);
            int baseVal = 26;   // as its given in problem only small case letters r present, else use 256 for Full ASCII Set
            int H = 1;          // Raise by
            // FORMULA => newRollHash = (base*(prvRollHash-1stCharInWin*H)+newChar)%mod;

            // calculate initial hashValue
            int hHash = 0, nHash = 0, i = 0;
            for (i = 0; i < nLen; i++)
            {
                hHash = ((hHash * baseVal) + haystack[i]) % mod;
                nHash = ((nHash * baseVal) + needle[i]) % mod;
                if (i < nLen - 1) H = (H * baseVal) % mod;     // one less as for needle of length 3 we only want the base^2 thats multiple H with base just twice
            }

            for (i = 0; i < hLen - nLen + 1; i++)
            {
                if (hHash == nHash)
                {
                    int k = 0;
                    while (k < nLen && haystack[k + i] == needle[k])
                        k++;
                    if (k == nLen) return i;
                }
                // calculate next rolling Hash
                if (i < hLen - nLen)
                    hHash = (baseVal * (hHash - (haystack[i] * H)) + haystack[i + nLen]) % mod;

                if (hHash < 0) hHash += mod;
            }
            return -1;
        }

        // DFS algo which returns Total no of unique paths in a Grid [ROW x COL] starting from Top-Lt ending at Bottom-Rt
        public static int UniquePaths_Recursion(int row, int col)
        {
            if (row < 1 || col < 1) return 0;
            if (row <= 1 && col <= 1) return 1;
            return UniquePaths_Recursion(row, col - 1) + UniquePaths_Recursion(row - 1, col);
        }

        // Returns 'Total no of unique paths' in a Grid [ROW x COL] starting from Top-Lt ending at Bottom-Rt
        // Time O(row*col) || Space O(col)
        public static int UniquePaths_DP(int row, int col)
        {
            int[] dp = new int[col];
            dp[0] = 1;      // 1st cell will have just 1 unique path
            for (int r = 0; r < row; r++)
                for (int c = 1; c < col; c++)
                    dp[c] += dp[c - 1];     // visualizing for 2D GRID each cell[row,col] computed as = cell[row-1,col] + cell[row,cell-1],
                                            // to save space consider prv row value n.e cell[row-1,col] as current value of cell[row,col]
            return dp[col - 1];
            /* Space O(row*col) solution below
             * fill 1st row & 1st col with '1' before starting
             *  for(int row = 1; row < n; row++)
             *      for(int col = 1; col < m; col++)
             *          dp[col][row] = dp[row - 1][col] + dp[row][col - 1];
             * return dp[row-1][col-1];
             */
        }


        // Returns 'Total no of unique paths' in a Grid [ROW x COL] with Obsctacle, starting from Top-Lt ending at Bottom-Rt
        // Time O(row*col) || Space O(col)
        public static int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            var row = obstacleGrid.Length;
            var col = obstacleGrid[0].Length;

            int[,] tab = new int[row, col];
            // Starting Cell or Ending Cell is obsctacle return '0' possible paths
            if (obstacleGrid[0][0] == 1 || obstacleGrid[row - 1][col - 1] == 1) return 0;
            // Else mark Starting position
            tab[0, 0] = 1;

            // 1st row
            for (int r = 1; r < row; r++)
                tab[r, 0] = (obstacleGrid[r][0] == 0 && tab[r - 1, 0] == 1) ? 1 : 0;
            // 1st col
            for (int c = 1; c < col; c++)
                tab[0, c] = (obstacleGrid[0][c] == 0 && tab[0, c - 1] == 1) ? 1 : 0;

            for (int r = 1; r < row; r++)
                for (int c = 1; c < col; c++)
                {
                    var fromTop = obstacleGrid[r - 1][c] == 0 ? tab[r - 1, c] : 0;
                    var fromLeft = obstacleGrid[r][c - 1] == 0 ? tab[r, c - 1] : 0;
                    tab[r, c] = fromLeft + fromTop;
                }
            return tab[row - 1, col - 1];
        }


        // Time O(Row*Col) || Space O(1)
        public static void RotateImage(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1) return;

            var rows = matrix.Length;
            var noOFSwapsPerDirection = rows;

            /* stop at row/2
             * start by replacing 1st element in each direction and once all four sides are moved by 90degrees moves on to next element
             * once all items are replaced move inwards for TotalNoOfRows/2 times (works well for odd or even no of rows)
             */
            int times = 0;
            int big = rows - 1, small = 0;
            while (times < rows / 2)
            {
                int elementToRotate = 0;
                while (elementToRotate < noOFSwapsPerDirection - 1)
                {
                    int prvRow = 0 + times, prvCol = 0 + elementToRotate + times;
                    int curRow = 0, curCol = 0;
                    var temp = matrix[prvRow][prvCol];
                    // rotate elements
                    for (int swapNo = 0; swapNo < 4; swapNo++)
                    {
                        switch (swapNo)
                        {
                            case 0:
                                curRow = prvRow + big;
                                curCol = prvCol - small;
                                matrix[prvRow][prvCol] = matrix[curRow][curCol];
                                break;
                            case 1:
                                curRow = prvRow + big;
                                curCol = prvCol + small;
                                matrix[prvRow][prvCol] = matrix[curRow][curCol];
                                break;
                            case 2:
                                curRow = prvRow - big;
                                curCol = prvCol + small;
                                matrix[prvRow][prvCol] = matrix[curRow][curCol];
                                break;
                            case 3:
                                curRow = prvRow - big;
                                curCol = prvCol - small;
                                matrix[prvRow][prvCol] = temp;
                                break;
                        }
                        prvRow = curRow;
                        prvCol = curCol;
                        // Swap Big & Small
                        Utility.Swap(ref big, ref small);
                    }
                    big--;
                    small++;
                    elementToRotate++;
                }
                Utility.Swap(ref big, ref small);
                big -= 2;
                noOFSwapsPerDirection -= 2;
                times++;
            }
        }
        // Best Solution => Transpose the Matrix and Reverse each row
        // Time O(Row^2) || Space O(1)
        public static void RotateImageTransposeAndReverse(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1) return;
            var rows = matrix.Length;
            // Transpose
            for (int r = 0; r < rows; r++)
                for (int c = r; c < rows; c++)
                    Utility.Swap(ref matrix[r][c], ref matrix[c][r]);
            // Reverse each row
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < rows / 2; c++)
                    Utility.Swap(ref matrix[r][c], ref matrix[r][rows - 1 - c]);
        }
        // Efficient Solution => Transpose the Matrix and Reverse each row
        // Time O(Row^2) || Space O(1)
        public static void RotateImageMovingDiagonallyFromLeftTopToMiddle(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1) return;
            int n = matrix.Length;
            for (int i = 0; i < (n + 1) / 2; i++)
                Move(i, i);                     // Start Diagonally from Top-Left towards bottom-right but stop at Center
            // local func
            void Move(int startR, int startC)
            {
                for (int c = startC; c < n - (1 + startC); c++)
                    MoveNumsCycle(startR, c);   // Swap all nums in current row one by one
            }
            void MoveNumsCycle(int r, int c)
            {
                // a * b
                // * * *
                // c * d

                // Temp = A
                int temp = matrix[r][c];
                // A = C
                matrix[r][c] = matrix[-1 + n - c][r];
                // C = D
                matrix[-1 + n - c][r] = matrix[-1 + n - r][-1 + n - c];
                // D = B
                matrix[-1 + n - r][-1 + n - c] = matrix[c][-1 + n - r];
                // B = Temp
                matrix[c][-1 + n - r] = temp;
            }
        }
        // Time O(n^2) | Space O(1), n = no of rows/columns in 'matrix'
        public static void RotateImageFourNumsAtATime(int[][] matrix)
        {
            int topBoundry = 0, rtBoundry = matrix.Length - 1, bottomBoundry = matrix.Length - 1, ltBoundry = 0;
            while (topBoundry < bottomBoundry)
            {
                // we rotate 1 num from each side and once all the 4 boundry 1st num is rotated we move to next
                for (int c = ltBoundry; c < rtBoundry; c++)
                    RotateFour(c - ltBoundry);    // O(1)
                topBoundry++;
                rtBoundry--;
                bottomBoundry--;
                ltBoundry++;
            }

            // local helper func
            void RotateFour(int distance)
            {
                /* SWAP 4 Corner numbers than next num in 1st row 2nd col and so on
                a * b
                * * *
                c * d
                */
                int temp = matrix[topBoundry][ltBoundry + distance];
                matrix[topBoundry][ltBoundry + distance] = matrix[bottomBoundry - distance][ltBoundry];
                matrix[bottomBoundry - distance][ltBoundry] = matrix[bottomBoundry][rtBoundry - distance];
                matrix[bottomBoundry][rtBoundry - distance] = matrix[topBoundry + distance][rtBoundry];
                matrix[topBoundry + distance][rtBoundry] = temp;
            }
        }
        // Don't use below in interview difficult to explain the intution behind the approach
        // Time O(row) || Space O(1)
        public static void RotateFourRectangleApproach(int[][] matrix)
        {
            if (matrix == null || matrix.Length < 1) return;
            var rows = matrix.Length;
            for (int r = 0; r < rows / 2 + rows % 2; r++)       // O(row/2)
                for (int c = 0; c < rows / 2; c++)              // O(row/2)
                {
                    int row = r, col = c;
                    var temp = new int[4];
                    // save 4 corner elemens in array
                    for (int times = 0; times < 4; times++)     // O(1)
                    {
                        temp[times] = matrix[row][col];
                        int swap = row;
                        row = col;
                        col = rows - 1 - swap;
                    }
                    // store these elements back after rotating array 'backwards by 90 degree'/'forwards by by 270 degree'
                    for (int times = 0; times < 4; times++)     // O(1)
                    {
                        matrix[row][col] = temp[(times + 3) % 4];
                        int swap = row;
                        row = col;
                        col = rows - 1 - swap;
                    }
                }
        }


        // Returns 1st missing +ve number (n.e. num which is not part of the input array)
        // Time O(n) || Space O(n)
        public static int FirstMissingPositiveUsingHashSet(int[] nums)
        {
            if (nums.Length == 0) return 1;

            int largest = 0;
            HashSet<int> allPositiveNums = new HashSet<int>() { 0 };
            for (int i = 0; i < nums.Length; i++)           // O(n)
                if (nums[i] > 0)
                {
                    allPositiveNums.Add(nums[i]);
                    largest = Math.Max(largest, nums[i]);
                }

            for (int n = 1; n <= largest; n++)              // O(n) will terminate on finding 1st +ve
                if (!allPositiveNums.Contains(n)) return n;
            return largest + 1;
        }
        // Returns 1st missing +ve number (n.e. num which is not part of the input array)
        // Time O(n) Space O(1) Approach using input array itself as HashTable
        public static int FirstMissingPositive(int[] nums)
        {
            // mark all -vs, 0 and numbers greather than n as '1' also keep track of if 1 was present in the original array or not
            bool onePresent = false;
            int len = nums.Length;
            for (int i = 0; i < len; i++)
                if (nums[i] <= 0 || nums[i] > len)
                {
                    nums[i] = 1;
                }
                else if (nums[i] == 1)
                    onePresent = true;
            // if one not present return it as its the smallest pos integer
            if (!onePresent) return 1;

            // iterate thru all index 1..N and mark them -ve if that num is present in array
            for (int i = 0; i < nums.Length; i++)
                if (nums[Math.Abs(nums[i]) - 1] > 0)
                    nums[Math.Abs(nums[i]) - 1] *= -1;    // marking -ve if not already negative
                                                          // return 1st positive value's (index+1) as ans
            for (int i = 0; i < len; i++)
                if (nums[i] > 0)
                    return 1 + i;
            // if all nums present in array are continuous values from 1..N than return N+1
            return len + 1;
        }


        // Partition lowercase English letters string into as many parts as possible so that each letter appears in at most one part,
        // and returns a list of integers representing the size of these parts.
        // Time O(n) 2 passes || Space O(n), where N length of input string
        public static List<int> PartitionLabels(string S)
        {
            int[] smallCase = new int[26];
            // Take count of all chartacters in input 'S'
            foreach (var ch in S)
                smallCase[ch - 'a']++;

            List<int> result = new List<int>();
            // Try to make smallest string which includes 1st character also add other characters
            // which we found in r way while we make sure we have encoutnered all occurences of 1st
            // and all occurences of other characters in r way so far.
            HashSet<char> toTrack = new HashSet<char>();
            int startIndex = 0;
            for (int i = 0; i < S.Length; i++)
            {
                //if(!toTrack.Contains(S[n]))    toTrack.Add(S[n]);
                toTrack.Add(S[i]);

                // decreament the count
                smallCase[S[i] - 'a']--;

                // if Count for curr Characters reaches '0' means we have encountered all occurences
                // and hence can be removed from toTrack set
                if (smallCase[S[i] - 'a'] == 0)
                    toTrack.Remove(S[i]);

                // Check if set doesn't cotaines any more characters for which we need to find all occurences
                if (toTrack.Count == 0)
                {
                    result.Add(i - startIndex + 1);
                    startIndex = i + 1;
                }
            }
            return result;
        }


        // Time O(1) Space (1) there is a hard upper limit on how many times the loop can iterate.
        // This upper limit is 15 times, and it occurs for the number 3888 representated as MMMDCCCLXXXVIII
        public static string IntegerToRoman(int num)
        {
            var romanSet = new KeyValuePair<string, int>[]{
            new KeyValuePair<string, int>("I", 1),    // 0
            new KeyValuePair<string, int>("IV",4),
            new KeyValuePair<string, int>("V", 5),
            new KeyValuePair<string, int>("IX",9),
            new KeyValuePair<string, int>("X", 10),
            new KeyValuePair<string, int>("XL",40),   // 5
            new KeyValuePair<string, int>("L", 50),
            new KeyValuePair<string, int>("XC",90),
            new KeyValuePair<string, int>("C", 100),
            new KeyValuePair<string, int>("CD",400),
            new KeyValuePair<string, int>("D", 500),  // 10
            new KeyValuePair<string, int>("CM",900),
            new KeyValuePair<string, int>("M", 1000)
            };

            string result = "";
            int currIndex = 12;// romanSet.Length - 1;
            while (num > 0)
            {
                while (num >= romanSet[currIndex].Value)
                {
                    result += romanSet[currIndex].Key;
                    num -= romanSet[currIndex].Value;
                }
                currIndex--;
            }
            return result;
        }
        // Time O(1) Space (1) as there is a finite set of roman numerals, the maximum number possible number can be 3999, which in roman numerals is MMMCMXCIX
        public static int RomanToInteger(string s)
        {
            if (s.Length == 0) return 0;
            var romanToIntMap = new Dictionary<char, int>() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            int result = romanToIntMap[s[0]];
            for (int i = 1; i < s.Length; i++)
            {
                if (romanToIntMap[s[i]] > romanToIntMap[s[i - 1]])
                    result -= 2 * romanToIntMap[s[i - 1]];
                result += romanToIntMap[s[i]];
            }
            return result;
        }
        public static int RomanToInteger_ReadingFromLast(string s)
        {
            if (s.Length == 0) return 0;
            var v = new Dictionary<char, int>() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };
            int ans = v[s[s.Length - 1]];
            for (int i = s.Length - 2; i >= 0; i--)
                if (v[s[i]] < v[s[i + 1]])
                    ans -= v[s[i]];
                else
                    ans += v[s[i]];
            return ans;
        }

        // Functions compares 2 versions and returns -1 (ver1 < ver2) or 1 (ver2 < ver1) else 0 (ver1==ver2)
        // Time O(max(N,M)) || Space O(max(N,M))
        public static int CompareVersionNumbers(string version1, string version2)
        {
            var ver1 = version1.Split('.');
            var ver2 = version2.Split('.');

            int l1 = ver1.Length;
            int l2 = ver2.Length;

            for (int i = 0; i < l1 || i < l2; i++)
            {
                var cur1 = i < l1 ? int.Parse(ver1[i]) : 0;
                var cur2 = i < l2 ? int.Parse(ver2[i]) : 0;
                if (cur1 < cur2)
                    return -1;
                else if (cur1 > cur2)
                    return 1;
            }
            return 0;
        }


        // Time O(Max,(N,(V+E)) || Space O(V+E), N = no of prerequisites
        public static bool CourseSchedule(int numCourses, int[][] prerequisites)
        {
            Dictionary<int, List<int>> DiGraph = new Dictionary<int, List<int>>(numCourses);
            // Create Directed Graph O(n)
            for (int i = 0; i < prerequisites.Length; i++)
            {
                if (!DiGraph.ContainsKey(prerequisites[i][0]))
                    DiGraph.Add(prerequisites[i][0], new List<int>() { prerequisites[i][1] });
                else DiGraph[prerequisites[i][0]].Add(prerequisites[i][1]);

                if (!DiGraph.ContainsKey(prerequisites[i][1]))
                    DiGraph.Add(prerequisites[i][1], new List<int>());
            }

            // 0 not visited || 1 visited & in-Stack || -1 visited and out of Stack
            int[] visited = new int[numCourses];
            foreach (var kvp in DiGraph)
                if (visited[kvp.Key] == 0)
                    if (DetectCycleInDAGUsingDFS(DiGraph, kvp.Key, ref visited))
                        return false;
            return true;
        }
        // Time O(V+E)
        public static bool DetectCycleInDAGUsingDFS(Dictionary<int, List<int>> g, int start, ref int[] visited)
        {
            visited[start] = 1;   // 1 visited & in-Stack
            foreach (var adjacentVertex in g[start])
                // 0 not visited, cheeck for cycle
                if (visited[adjacentVertex] == 0 && DetectCycleInDAGUsingDFS(g, adjacentVertex, ref visited))
                    return true;
                // cycle found return true
                else if (visited[adjacentVertex] == 1)
                    return true;
            visited[start] = -1;//-1 visited and out of Stack
            return false;
        }


        // Time O(Max,(N,(V+E)) || Space O(V+E), N = no of prerequisites
        public static int[] CourseScheduleII(int numCourses, int[][] prerequisites)
        {
            int[] topSort = new int[numCourses];
            int index = 0;

            Dictionary<int, List<int>> DiGraph = new Dictionary<int, List<int>>(numCourses);
            // Create Directed Graph O(n)
            for (int i = 0; i < numCourses; i++) DiGraph.Add(i, new List<int>());

            // Add Adjacent Edges/Dependencies in Graph O(n)
            for (int i = 0; i < prerequisites.Length; i++)
                DiGraph[prerequisites[i][0]].Add(prerequisites[i][1]);

            // 0 not visited || 1 visited & in-Stack || -1 visited and out of Stack
            int[] visited = new int[numCourses];
            foreach (var kvp in DiGraph)
                if (visited[kvp.Key] == 0)
                    if (DetectCycleUsingDFS(DiGraph, kvp.Key, topSort, ref index, visited))
                        return new int[0];
            return topSort;
        }
        // Time O(V+E)
        public static bool DetectCycleUsingDFS(Dictionary<int, List<int>> g, int start, int[] topSort, ref int i, int[] visited)
        {
            visited[start] = 1;     // 1 visited & in-Stack
            foreach (var adjacentVertex in g[start])
                // 0 not visited, cheeck for cycle
                if (visited[adjacentVertex] == 0 && DetectCycleUsingDFS(g, adjacentVertex, topSort, ref i, visited))
                    return true;
                // cycle found return true
                else if (visited[adjacentVertex] == 1)
                    return true;
            visited[start] = -1;    //-1 visited and out of Stack
            topSort[i++] = start;   // Topological order
            return false;
        }


        // Time O(n), n = no of nodes in the binary tree
        // Recursive Solution
        public static bool IsSymmetricRecursive(TreeNode t1, TreeNode t2)
        {
            if (t1 == null && t2 == null) return true;
            if (t1 == null || t2 == null) return false;
            if (t1.val != t2.val) return false;
            return IsSymmetricRecursive(t1.left, t2.right) && IsSymmetricRecursive(t1.right, t2.left);
        }
        // Iterative Solution
        public static bool IsSymmetricIterativeBFSApproach(TreeNode root)
        {
            Queue<TreeNode> q = new Queue<TreeNode>();
            TreeNode nullTreeNode = null;
            q.Enqueue(root);
            q.Enqueue(nullTreeNode);
            List<int> ls = new List<int>();
            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                if (temp == null)
                {
                    if (!CheckPalindrome(ls)) return false;
                    if (q.Count > 0)
                        q.Enqueue(nullTreeNode);
                    ls.Clear();
                    continue;
                }
                if (temp.left != null)
                {
                    q.Enqueue(temp.left);
                    ls.Add(temp.left.val);
                }
                else
                    ls.Add(-1);

                if (temp.right != null)
                {
                    q.Enqueue(temp.right);
                    ls.Add(temp.right.val);
                }
                else
                    ls.Add(-1);

            }
            return true;
        }
        // Helper function used with iterative Solution
        public static bool CheckPalindrome(List<int> ls)
        {
            if (ls.Count < 2) return true;
            int start = 0;
            int last = ls.Count - 1;
            while (start < last)
                if (ls[start++] != ls[last--]) return false;
            return true;
        }


        // Time O(N) || Space O(N)
        public static int BasicCalculator(string s, ref int i, bool evalTillClosingBrackets = false)
        {
            if (evalTillClosingBrackets) i++;
            List<int> ls = new List<int>(100);
            bool isPositive = true;
            while (i < s.Length)
            {
                // if current character is Not empty space
                if (s[i] != ' ')
                {
                    // if current character is an digit 0-9
                    if (Char.IsDigit(s[i]))
                    {
                        int num = 0;
                        while (i < s.Length && Char.IsDigit(s[i]))
                            num = num * 10 + (s[i++] - '0');
                        ls.Add(isPositive ? num : -num);
                        i--;
                    }
                    else if (s[i] == '+') isPositive = true;
                    else if (s[i] == '-') isPositive = false;
                    // Found Opening Bracket, make recursive call
                    else if (s[i] == '(')
                    {
                        int valueInsideParenthesis = BasicCalculator(s, ref i, true);
                        ls.Add(isPositive ? valueInsideParenthesis : -valueInsideParenthesis);
                    }
                    else if (evalTillClosingBrackets && s[i] == ')') break;
                }
                i++;
            }
            int result = 0;
            for (int k = 0; k < ls.Count; k++)
                result += ls[k];
            return result;
        }

        // Time O(n) // Space O(n)
        public static int BasicCalculatorII(string s)
        {
            Stack<int> nums = new Stack<int>();
            Stack<char> ops = new Stack<char>();
            Dictionary<char, int> opDict = new Dictionary<char, int>(4) { { '+', 1 }, { '-', 1 }, { '*', 2 }, { '/', 2 } };

            for (int i = 0; i < s.Length; i++)
            {
                // if space character skip
                if (s[i] == ' ') continue;
                // if operator
                else if (opDict.ContainsKey(s[i]))
                    ops.Push(s[i]);
                // if operand
                else
                {
                    string num = "";
                    // Iterator to find the current number which might be more than 1 characters long in input string
                    do { num += s[i++]; } while (!(i >= s.Length || opDict.ContainsKey(s[i]) || s[i] == ' '));
                    i--;
                    // Highest Priority Operator found on Stack Top
                    if (ops.Count > 0 && opDict[ops.Peek()] >= 2)
                        nums.Push(ApplyOperator(ops.Pop(), int.Parse(num), nums.Pop()));
                    // Stack contains 2 Operators with same Priority, than calculate for operator which came 1st into stack
                    else if (ops.Count >= 2)
                    {
                        var secondOp = ops.Pop();
                        nums.Push(ApplyOperator(ops.Pop(), nums.Pop(), nums.Pop()));
                        ops.Push(secondOp);
                        nums.Push(int.Parse(num));
                    }
                    else nums.Push(int.Parse(num));
                }
            }
            return nums.Count == 1 ? nums.Pop() : ApplyOperator(ops.Pop(), nums.Pop(), nums.Pop());
        }
        // Time O(1)
        public static int ApplyOperator(char op, int n2, int n1)
        {
            switch (op)
            {
                case '+': return n1 + n2;
                case '-': return n1 - n2;
                case '*': return n1 * n2;
                case '/': return n1 / n2;
            }
            return 0;
        }
        /// <summary>
        /// Functions evaluates input expression string containing only non-negative integers
        /// and +, -, *, / operators and empty spaces.
        /// Time O(n) || Space O(n)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int BasicCalculatorIIFaster(string s)
        {
            if (s.Length < 1) return 0;
            char lastOperator = '+';
            int currNum = 0;
            Stack<int> st = new Stack<int>(100);

            for (int i = 0; i < s.Length; i++)
            {
                // if current character is an digit 0-9
                if (Char.IsDigit(s[i])) currNum = currNum * 10 + (s[i] - '0');
                // else if current character is Not empty space
                else if (s[i] != ' ')
                {
                    switch (lastOperator)
                    {
                        case '+':
                            st.Push(currNum);
                            break;
                        case '-':
                            st.Push(-currNum);
                            break;
                        case '*':
                            st.Push(st.Pop() * currNum);
                            break;
                        case '/':
                            st.Push(st.Pop() / currNum);
                            break;
                    }
                    lastOperator = s[i];
                    currNum = 0;
                }
            }
            // as we are calculate for previous operator upon reaching next operator,
            // hence for last operator we need to do explicitly after we have finished pre-processing rest of the data
            // we can move this above by replacing the else if with => if ((s[n]!=' ' && !Char.IsDigit(s[n])) || n==s.Length-1)
            switch (lastOperator)
            {
                case '+':
                    st.Push(currNum);
                    break;
                case '-':
                    st.Push(-currNum);
                    break;
                case '*':
                    st.Push(st.Pop() * currNum);
                    break;
                case '/':
                    st.Push(st.Pop() / currNum);
                    break;
            }
            int result = 0;
            while (st.Count > 0)
                result += st.Pop();
            return result;
        }


        // Time O(n) || Space O(n)
        public static string ReverseWordsInAString(string s)
        {
            string result = "";
            string word = "";
            bool firstWord = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {
                    if (word != "")
                    {
                        result = firstWord ? word : word + ' ' + result;
                        firstWord = false;
                        word = "";
                    }
                }
                else word += s[i];
            }
            return word == "" ? result : (firstWord ? word : word + ' ' + result);
        }

        // Time O(n) || Space O(1)
        public static void ReverseWordsInAStringII(char[] s)
        {
            int start = 0;
            int last = s.Length - 1;
            while (start < last)
            {
                var temp = s[start];
                s[start++] = s[last];
                s[last--] = temp;
            }
            start = 0;
            int currIndex = 0;
            while (currIndex < s.Length)
            {
                if (s[currIndex] == ' ')
                {
                    if (currIndex > start + 1)
                    {
                        // reverse word
                        last = currIndex - 1;
                        while (start < last)
                        {
                            var temp = s[start];
                            s[start++] = s[last];
                            s[last--] = temp;
                        }
                    }
                    start = currIndex + 1;
                }
                currIndex++;
            }
            // reverse last word
            if (currIndex > start + 1)
            {
                // reverse word
                last = currIndex - 1;
                while (start < last)
                {
                    var temp = s[start];
                    s[start++] = s[last];
                    s[last--] = temp;
                }
            }
        }


        // Time O(nLogn) Space O(1)
        public static int MinimumCostToConnectStick(int[] sticks)
        {
            /* ALGO
             * 1) Heapify the 'sticks' array (Convert into MinHeap)
             * 2) Extract Min() twice add it to sum and insert the sum of back into heap
             * 3) Repeat Step2# until only single element is left in Min-Heap
             */
            var len = sticks.Length;
            int ltChild = 0, rtChild = 0;
            // Start Heapification from middle of Array
            for (int i = (len - 1) / 2; i >= 0; i--)
            {
                // Call heapify for each element present at index 'n'
                int j = i;
                while (j < len)
                {
                    var smallest = j;
                    ltChild = j * 2 + 1;
                    rtChild = ltChild + 1;
                    if (ltChild < len && sticks[ltChild] < sticks[j]) smallest = ltChild;
                    if (rtChild < len && sticks[rtChild] < sticks[smallest]) smallest = rtChild;
                    if (smallest != j)
                    {
                        var temp = sticks[smallest];
                        sticks[smallest] = sticks[j];
                        sticks[j] = temp;
                        j = smallest;
                    }
                    else break;
                }
            }
            int sum = 0;
            for (int i = 1; i < sticks.Length; i++)
            {
                var connectCost = ExtractMin() + ExtractMin();
                Insert(connectCost);
                sum += connectCost;
            }
            // Extract Min value from Heap
            int ExtractMin()
            {
                var min = sticks[0];
                // insert last element at root & decrease the length of Heap by '1'
                sticks[0] = sticks[--len];
                // Heapify/Perculate-Down operation
                int j = 0;
                while (j < len)
                {
                    var smallest = j;
                    ltChild = j * 2 + 1;
                    rtChild = ltChild + 1;
                    if (ltChild < len && sticks[ltChild] < sticks[j]) smallest = ltChild;
                    if (rtChild < len && sticks[rtChild] < sticks[smallest]) smallest = rtChild;
                    if (smallest != j)
                    {
                        var temp = sticks[smallest];
                        sticks[smallest] = sticks[j];
                        sticks[j] = temp;
                        j = smallest;
                    }
                    else break;
                }
                return min;
            }
            // Perculate-Up operation
            void Insert(int num)
            {
                int i = len++;
                sticks[i] = num;
                while (i > 0)
                {
                    var parent = (i - 1) / 2;
                    if (sticks[parent] > sticks[i])
                    {
                        var temp = sticks[parent];
                        sticks[parent] = sticks[i];
                        sticks[i] = temp;
                        i = parent;
                    }
                    else break;
                }
            }
            return sum;
        }

        // LC#1000. "Minimum Cost to Merge Stones"
        // There are N piles of stones arranged in a row.  The n-th pile has stones[n] stones.
        // A move consists of merging exactly K consecutive piles into one pile, and the cost of this move is equal to the total number of stones in these K piles.
        // Find the minimum cost to merge all piles of stones into one pile.If it is impossible, return -1
        // Time O((N^2)*(N/K)) || Space: O(N^2)
        public static int MinimumCostToMergeStones(int[] stones, int K)
        {
            var len = stones.Length;

            if (len <= 1) return 0;
            if ((len - 1) % (K - 1) != 0) return -1;


            int[,] minMergeCost = new int[len, len];
            int[] prefixSum = new int[len + 1];
            // pre-caculate the partial sums for O(1) lookup later
            for (int i = 1; i <= len; i++)
                prefixSum[i] = prefixSum[i - 1] + stones[i - 1];

            for (int l = 0; l < len; l++)
                for (int start = 0; start < len - l; start++)
                {
                    var last = start + l;
                    // cost of merging single stone with itself is 0
                    if (start == last) minMergeCost[start, last] = 0;
                    // cost of merging less than K stone is also '0' as we need can merge only K stones at a time
                    else if (last - start + 1 < K) minMergeCost[start, last] = 0;
                    // if length is K, cost is simply the sum of all stone in the given range
                    else if (last - start + 1 == K) minMergeCost[start, last] = prefixSum[last + 1] - prefixSum[start];
                    // if length > K, try finding min TotalCost by finding a Mid-Point such that (start..mid + mid+1..last) is minimum
                    else
                    {
                        minMergeCost[start, last] = int.MaxValue;
                        // mid divides start...last into (1, rest), (K, rest), (2K-1, rest), and so on....
                        for (int mid = start; mid < last; mid += K - 1)
                            minMergeCost[start, last] = Math.Min(minMergeCost[start, last], minMergeCost[start, mid] + minMergeCost[mid + 1, last]);
                        // above only calculate cost of merging K piles, n.e. 1 on left * K-1 piles on rt (cost of creating those 2 piles)
                        // But we still need to add cost of merging left half & right half of the pile to get 1 pile
                        // Here, len of pile - 1 == last - start
                        if ((last - start) % (K - 1) == 0)
                            minMergeCost[start, last] += prefixSum[last + 1] - prefixSum[start];
                    }
                }
            return minMergeCost[0, len - 1];
        }
        // Explanation https://leetcode.com/problems/minimum-cost-to-merge-stones/discuss/675912/DP-code-decoded-for-non-experts-like-me
        // More Explanation https://leetcode.com/problems/minimum-cost-to-merge-stones/discuss/831419/Why-DP-solution-splits-(K-1)-AND-a-bonus-problem-(bottom-up)
        // Time O(N^3/K) || Space: O(N^2)
        public static int MinimumCostToMergeStones100Faster(int[] stones, int K)
        {
            var len = stones.Length;

            if (len <= 1) return 0;
            if ((len - 1) % (K - 1) != 0) return -1;


            int[,] minMergeCost = new int[len, len];
            int[] prefixSum = new int[len + 1];
            for (int i = 1; i <= len; i++)
                prefixSum[i] = prefixSum[i - 1] + stones[i - 1];

            for (int l = K; l <= len; l++)
                for (int start = 0; start <= len - l; start++)
                {
                    var last = start + l - 1;
                    minMergeCost[start, last] = int.MaxValue;
                    for (int mid = start; mid < last; mid += K - 1)
                        minMergeCost[start, last] = Math.Min(minMergeCost[start, last], minMergeCost[start, mid] + minMergeCost[mid + 1, last]);
                    if ((last - start) % (K - 1) == 0)
                        minMergeCost[start, last] += prefixSum[last + 1] - prefixSum[start];
                }
            return minMergeCost[0, len - 1];
        }


        // Time O(n) || Space O(1)
        public static int StringToInteger(string str)
        {
            int num = 0;
            byte first = 0, isNegative = 0;
            foreach (var ch in str)
                if (ch == ' ' && first == 0) continue;
                else
                {
                    // 1st character
                    if (first == 0)
                    {
                        first = 1;
                        // found -ve sign
                        if (ch - '-' == 0) isNegative = 1;
                        // found +ve sign
                        else if (ch - '+' == 0) continue;
                        // 1st character is digit
                        else if (char.IsDigit(ch)) num = ch - '0';
                        // 1st character is NOT an Digit or -ve sign
                        else return 0;
                    }
                    // remaining characters
                    else
                    {
                        if (char.IsDigit(ch))
                        {
                            if (isNegative == 0)
                            { if (num > int.MaxValue / 10 || (num == int.MaxValue / 10 && ch - '0' > 7)) return int.MaxValue; }
                            else
                            { if (num * -1 < int.MinValue / 10 || (num * -1 == int.MinValue / 10 && ch - '0' > 8)) return int.MinValue; }
                            //{ if (num > (int.MaxValue - 1) / 10 || (num == (int.MaxValue - 1) / 10 && ch - '0' > 8)) return int.MinValue; }
                            num = num * 10 + ch - '0';
                        }
                        // 1st character is NOT an Digit or -ve sign
                        else break;
                    }
                }
            return isNegative == 0 ? num : num * -1;
        }


        // Time O(1) || Space O(1), as integer can have max of 10 digits only
        public static int ReverseInt(int num)
        {

            if (num == int.MinValue) return 0;
            Console.WriteLine($"For num {num}");
            bool isPositive = num >= 0;
            num = Math.Abs(num);
            int rev = 0;
            while (num != 0)
            {
                // need to check if multiplying by 10 would lead to num greater than limit n.e. MaxValue
                // & also if rev is enumcatly 1/10 of MaxValue so last possible value that coud b added without overflowing is 7
                // MaxValue value 2147483647, MinValue -2147483648
                if (rev > int.MaxValue / 10 || (rev == int.MaxValue / 10 && num % 10 > (isPositive ? 7 : 8)))
                    return 0;
                rev = rev * 10 + num % 10;
                num /= 10;
                Console.WriteLine($"{rev} {num}");
            }
            return isPositive ? rev : rev * -1;

            /* Slightly confusing
            if (num == int.MinValue) return 0;
            bool isPositive = num >= 0;
            num = Math.Abs(num);
            int reverse = 0;
            while (num != 0)
            {
                if (isPositive)
                { if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 8)) 
                        return 0; }
                else
                { if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 7))
                        return 0; }
                reverse = reverse * 10 + num % 10;
                num /= 10;
            }
            return isPositive ? reverse : reverse * -1;
            */

            /* FOR EVEN FASTER RUNTIME
            //if (x == int.MinValue) return 0;
            bool isPositive = num >= 0;
            int reverse = 0;
            // input is +ve
            if (isPositive)
            {
                while (num != 0)
                {
                    if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 8)) return 0;
                    reverse = reverse * 10 + num % 10;
                    num /= 10;
                }
                return reverse;
            }
            // input is -ve
            if (num == int.MinValue) return 0;
            num = Math.Abs(num);
            while (num != 0)
            {
                if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 7)) return 0;
                reverse = reverse * 10 + num % 10;
                num /= 10;
            }
            return reverse * -1;
            */
        }

        // Time O(NLogN) || Space O(1)
        public static bool SearchA2DMatrixIIBinarySearch(int[,] matrix, int target)
        {
            if (matrix == null || matrix.GetLength(0) < 1) return false;
            var shortLen = Math.Min(matrix.GetLength(0), matrix.GetLength(1));
            for (int i = 0; i < shortLen; i++)
                if (BinarySearchInSorted2D(matrix, target, i, true) || BinarySearchInSorted2D(matrix, target, i, false))
                    return true;
            return false;
        }
        // Time O(logN) || Space O(1)
        public static bool BinarySearchInSorted2D(int[,] matrix, int target, int startIndex, bool verticalSearch)
        {
            int start = startIndex;
            var last = (verticalSearch ? matrix.GetLength(0) : matrix.GetLength(1)) - 1;
            while (start <= last)
            {
                var mid = start + (last - start) / 2;
                if (verticalSearch) // foreach index in Rows-wise Search
                {
                    if (matrix[mid, startIndex] < target) start = mid + 1;
                    else if (matrix[mid, startIndex] > target) last = mid - 1;
                    else return true;
                }
                else // foreach index in Cols-wise Search
                {
                    if (matrix[startIndex, mid] < target) start = mid + 1;
                    else if (matrix[startIndex, mid] > target) last = mid - 1;
                    else return true;
                }
            }
            return false;
        }
        // Search Space Reduction Approach
        // Time O(R+C) || Space O(1), R = no of rows & C = no of columns
        public static bool SearchNumberIn2DMatrix(int[,] matrix, int target)
        {
            if (matrix == null || matrix.GetLength(0) < 1) return false;
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            int r = rows - 1, c = 0;    // Starting from the bottom left corner
            while (r >= 0 && c < cols)
                if (matrix[r, c] == target) return true;
                else if (matrix[r, c] > target) r--;    // move up
                else c++;                               // move right
            return false;
        }


        // Function return a deep copy (clone) of the graph.
        // Time O(V+E), V = no of Vertices & E = no of Edges
        public static ListNode CloneGraph(ListNode node)
        {
            if (node == null) return null;
            // to hold reference of newly created ListListNodes
            Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>(100);
            // to maintain set of already copied ListNodes
            HashSet<int> visited = new HashSet<int>();
            // Queue for BFS traversal
            Queue<ListNode> q = new Queue<ListNode>();
            q.Enqueue(node);
            // mark this ListNode as visited and Done copying
            visited.Add(node.val);

            while (q.Count > 0)
            {
                var cur = q.Dequeue();

                // create parent ListNode with same value as current ListNode
                if (!dict.ContainsKey(cur.val)) dict.Add(cur.val, new ListNode(cur.val));

                foreach (var adjacentListNode in cur.neighbors)
                {
                    // add this adjacent ListNode to the queue if not already copied/visited
                    if (!visited.Contains(adjacentListNode.val))
                    {
                        q.Enqueue(adjacentListNode);
                        // mark this adjacent ListNode as visited and don't add it again to the Queue
                        visited.Add(adjacentListNode.val);
                    }

                    // create new ListNode with adjacent value
                    if (!dict.ContainsKey(adjacentListNode.val)) dict.Add(adjacentListNode.val, new ListNode(adjacentListNode.val));

                    // connect parent -> adjacenet ListNode
                    dict[cur.val].neighbors.Add(dict[adjacentListNode.val]);
                }
            }
            return dict[node.val];
        }


        /// <summary>
        /// Time O(3^N * 4^M) where N is the number of digits in the input that maps to 3 letters (e.g. 2, 3, 4, 5, 6, 8)
        /// and M is the number of digits in the input that maps to 4 letters (e.g. 7, 9)
        /// and N+M is the total number digits in the input.
        /// Space O(3^N * 4^M) ~ O(1)
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static IList<string> LetterCombinationsOfPhoneNo(string digits)
        {
            Dictionary<char, List<char>> digitToChar = new Dictionary<char, List<char>>()
            {
                {'2' , new List<char>(){'a','b','c'}},
                {'3' , new List<char>(){'d','e','f'}},
                {'4' , new List<char>(){'g','h','i'}},
                {'5' , new List<char>(){'j','k','l'}},
                {'6' , new List<char>(){'m','n','o'}},
                {'7' , new List<char>(){'p','q','r','s'}},
                {'8' , new List<char>(){'t','u','v'}},
                {'9' , new List<char>(){'w','x','y','z'}},
            };

            IList<string> ans = new List<string>();
            int l = digits.Length;
            GetCombo(new List<char[]>());
            return ans;

            // local helper func
            void GetCombo(List<char[]> ls, int idx = 0)
            {
                if (idx == l)
                {
                    foreach (var letterCombination in ls)
                        ans.Add(new string(letterCombination));
                }
                else
                {
                    List<char[]> currLs = new List<char[]>();
                    foreach (var letter in digitToChar[digits[idx]])
                        if (ls.Count > 0)
                            foreach (var combo in ls)
                            {
                                combo[idx] = letter;
                                currLs.Add(combo.ToArray());
                            }
                        else
                        {
                            var newList = new char[l];
                            newList[idx] = letter;
                            currLs.Add(newList);
                        }
                    GetCombo(currLs, idx + 1);
                }
            }

            #region Old approach
            //IList<string> result = new List<string>();
            //if (digits.Length == 0) return result;

            //// Create dict from where letters corrosponding to each num would be stored
            //Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>(8);
            //char startingLetter = 'a';
            //for (int inputDigit = 2; inputDigit < 10; inputDigit++)
            //{
            //    dict.Add(inputDigit, new List<char>(3));
            //    for (int n = 0; n < (((inputDigit == 7) || (inputDigit == 9)) ? 4 : 3); n++)
            //        dict[inputDigit].Add(startingLetter++);
            //}

            //LetterCombinations(digits, 0, dict, "", ref result);
            //return result;
            #endregion
        }
        // Helper Recursive Function which goes thru each possible combination and adds them to result list at end
        public static void LetterCombinations(string digits, int currNumIndex, Dictionary<int, List<char>> dict, string letterComboSoFar, ref IList<string> result)
        {
            if (currNumIndex == digits.Length)
                result.Add(letterComboSoFar);
            else
                // For each current num in Dict try combo with all remaining numbers
                foreach (var currChar in dict[digits[currNumIndex] - '0'])
                    LetterCombinations(digits, currNumIndex + 1, dict, letterComboSoFar + currChar, ref result);
        }


        // Time = Space = O(N), N = no of nodes in tree
        public static string Serialize(TreeNode root)
        {
            if (root == null) return "#,";
            return root.val + "," + Serialize(root.left) + Serialize(root.right);
        }
        public static TreeNode DeSerialize(string serialzedData)
        {
            int startIndex = 0;
            return DeserializeHelper(serialzedData.Split(','), ref startIndex);
        }
        // Time = Space = O(N), N = no of nodes in tree
        public static TreeNode DeserializeHelper(string[] split, ref int index)
        {
            if (split[index] == "#") return null;

            var root = new TreeNode(int.Parse(split[index]));
            if (++index < split.Length) root.left = DeserializeHelper(split, ref index);
            if (++index < split.Length) root.right = DeserializeHelper(split, ref index);
            return root;
        }


        // Time = Space = O(V), V = no of Vertex in Graph
        public static IList<int> FindMinHeightTrees(int[][] edges, int n)
        {
            IList<int> result = new List<int>(n);
            if (n < 3)
            {
                for (int i = 0; i < n; i++) result.Add(i);
                return result;
            }

            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>(n);

            // Build the graph
            for (int i = 0; i < n - 1; i++)     // Time O(V-1)
            {
                var vertexU = edges[i][0];
                var vertexV = edges[i][1];
                // Edge U->V
                if (!graph.ContainsKey(vertexU)) graph.Add(vertexU, new List<int>(n - 1) { vertexV });
                else graph[vertexU].Add(vertexV);
                // Edge V->U
                if (!graph.ContainsKey(vertexV)) graph.Add(vertexV, new List<int>(n - 1) { vertexU });
                else graph[vertexV].Add(vertexU);
            }

            int remainingNodes = n;
            HashSet<int> singleEdgeVertices = new HashSet<int>(n);

            // trim out leaf nodes from the Graph until the Graph has more than 2 Vertices
            while (remainingNodes > 2)
            {
                foreach (var vertex in graph.Keys)
                    // Vertex with single Edge, Add it to list of Vertices to be removed
                    if (graph[vertex].Count == 1) singleEdgeVertices.Add(vertex);

                // Remove single edge Vertices from the Graph
                foreach (var singleEdgeVertex in singleEdgeVertices)
                {
                    // remove edge V->U
                    graph[graph[singleEdgeVertex][0]].Remove(singleEdgeVertex);
                    // remove edge U->V
                    graph.Remove(singleEdgeVertex);
                }

                remainingNodes -= singleEdgeVertices.Count();
                // reset removed Vertices for next iteration
                singleEdgeVertices.Clear();
            }

            // Add Remaining Vertices(Max 2 would be left) to result
            foreach (var centroidVertex in graph.Keys)
                result.Add(centroidVertex);
            return result;
        }


        // Time O(Max(N,M}) || Space O(N)
        // N = length on arr & M = Sum of length of all sub-arrays within Pieces Array 
        public static bool ArrayFormationThroughConcatenation(int[] arr, int[][] pieces)
        {
            Dictionary<int, int> aNumIndex = new Dictionary<int, int>(100);
            int i = 0;
            foreach (var integer in arr) aNumIndex.Add(integer, i++);
            foreach (var piece in pieces)
            {
                if (piece.Length == 1 && aNumIndex.ContainsKey(piece[0]))
                    aNumIndex.Remove(piece[0]);
                else
                {
                    int lastIndex = -1;
                    foreach (var num in piece)
                        if (aNumIndex.ContainsKey(num) && (lastIndex == -1 || aNumIndex[num] == 1 + lastIndex))
                        { lastIndex = aNumIndex[num]; aNumIndex.Remove(num); }
                }
                // arr can be formed using pieces array
                if (aNumIndex.Count == 0) return true;
            }
            return false;
        }

        // Time O(N) || Space O(1)
        public static int MinCostToMoveChips(int[] position)
        {
            // cost of merging all chips at different even position to any single even position is Zero
            // cost of merging all chips at different odd position to any single odd position is Zero
            // Now move small pile of chips and merge it with second pile at cost of 1 x no of chips in smaller pile
            int evenPositionChips = 0, oddPositionChips = 0;
            for (int i = 0; i < position.Length; i++)
                if (position[i] % 2 == 0) evenPositionChips++;
                else oddPositionChips++;

            return evenPositionChips < oddPositionChips ? evenPositionChips : oddPositionChips;
        }

        // Time Best Case O(LogN) Worst Case O(N) || Space O(1)
        public static int MinInRotatedSortedArrayWithDuplicates(int[] nums)
        {
            int start = 0;
            int last = nums.Length - 1;
            while (start < last)
            {
                var mid = start + (last - start) / 2;
                /*if(mid<last && nums[mid]>nums[mid+1]) return nums[mid+1];
                else if(start<mid && nums[mid-1]>nums[mid]) return nums[mid];
                else */
                if (nums[mid] > nums[last]) start = mid + 1;
                else if (nums[mid] < nums[last]) last = mid;
                else last--;
            }
            return nums[start];
        }

        // Time O(n), N = length of nums || Space O(1)
        // Time can be reduced to LogN if nums array is sorted as we need not find Max in that case
        public static int SmallestDivisor(int[] nums, int threshold)
        {
            int len = nums.Length;
            int rightBoundry = nums[len - 1];//int.MinValue;
            //for (int n = 0; n < nums.Length; n++) rightBoundry = Math.Max(rightBoundry, nums[n]);   // O(N) time

            int leftBoundry = (rightBoundry % threshold == 0) ? rightBoundry / threshold : (rightBoundry / threshold) + 1;
            int divisor = leftBoundry;

            while (true)    // O(LogN) as we are reducing boundry by half with each pass
            {
                divisor = leftBoundry + ((rightBoundry - leftBoundry) >> 1);    // right shifting by 1 is same as dividing by 2
                int sumResultOfDivision = 0;
                for (int i = 0; i < nums.Length; i++)
                    sumResultOfDivision += (nums[i] / divisor) + ((nums[i] % divisor == 0) ? 0 : 1);
                if (leftBoundry == rightBoundry) break;
                else if (sumResultOfDivision <= threshold) rightBoundry = divisor;
                else leftBoundry = divisor + 1;

            }
            return divisor;
        }


        // Time O(n) || Space O(n)
        public static TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            Dictionary<int, int> valueIndex = new Dictionary<int, int>(100);
            for (int i = 0; i < inorder.Length; i++) valueIndex.Add(inorder[i], i);
            int preorderIndex = 0;
            return BuildTreeRecursiveUtil(valueIndex, inorder, preorder, 0, inorder.Length - 1, ref preorderIndex);
        }
        public static TreeNode BuildTreeRecursiveUtil(Dictionary<int, int> valueIndex, int[] inorder, int[] preorder, int inOrderStart, int inOrderEnd, ref int preorderIndex)
        {
            if (inOrderStart > inOrderEnd) return null;
            var indexInInOrder = valueIndex[preorder[preorderIndex]];
            TreeNode root = new TreeNode(preorder[preorderIndex++]);
            root.left = BuildTreeRecursiveUtil(valueIndex, inorder, preorder, inOrderStart, indexInInOrder - 1, ref preorderIndex);
            root.right = BuildTreeRecursiveUtil(valueIndex, inorder, preorder, indexInInOrder + 1, inOrderEnd, ref preorderIndex);
            return root;
        }


        // Functions take input date and returns Day of the week
        // Time O(1) || Space O(1)
        public static string DayOfTheWeek(int day, int month, int year)
        {
            // day of the week
            string[] dayOfWeek = { "Thursday", "Friday", "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday" };
            // days in month for months
            int[] daysInMonths = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            //// Set of leap years
            //HashSet<int> leapYr = new HashSet<int>(32);
            //int[] leapYears1970To2100 = { 1972, 1976, 1980, 1984, 1988, 1992, 1996, 2000, 2004, 2008, 2012, 2016, 2020, 2024, 2028, 2032, 2036, 2040, 2044, 2048, 2052, 2056, 2060, 2064, 2068, 2072, 2076, 2080, 2084, 2088, 2092, 2096 };
            //for (int n = 0; n < leapYears1970To2100.Length; n++) leapYr.Add(leapYears1970To2100[n]);

            // (noOfYearsSince1971 * 365) + day
            int totalNoOfDays = ((year - 1971) * 365) + day;

            // add no of days passed for the prior months
            for (int currMonth = 1; currMonth < month; currMonth++)
                totalNoOfDays += daysInMonths[currMonth];

            // adjust for leap Years
            for (int yr = 1972; yr < year; yr += 4)
                totalNoOfDays++;

            // add 1 additional day if current year is leap year and input month is > Feb [2100 is not a Leap Year]
            if (month > 2 && year != 2100 && (year - 1972) % 4 == 0)
                totalNoOfDays++;
            // Can use HashSet to check for leap year in O(1) time however to skip creating HashSet altogether use above formula instead
            //if (leapYr.Contains(year) && month > 2)
            //    totalNoOfDays++;

            // and take mod of totalNoOfDays by 7 to find the current day
            return dayOfWeek[totalNoOfDays % 7];
        }
        public static bool IsLeapYear(int year) => ((year % 4) == 0) && ((year % 100) != 0) || ((year % 400) == 0);


        // Time O(n) || Space O(1)
        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            // we start with 1 emptyPlot instead of 0, as if 1st 2 plots are empty we can plot a flower at inedx=0
            int emptyPlotCount = 1;
            for (int i = 0; i < flowerbed.Length; i++)
            {
                if (flowerbed[i] == 1) emptyPlotCount = 0;
                else if (++emptyPlotCount == 3)
                { n--; emptyPlotCount = 1; }
                if (n == 0) return true;
            }
            // check if last 2 plots are empty we can plan 1 more flower as there is infinite space on right of last
            return (n == 1 && emptyPlotCount == 2) ? true : false;
        }


        // Time O(NLogN) || Space O(N)
        public static int TwoSumLessThanK(int[] A, int K)
        {
            int sumLessThanK = -1;
            if (K < 2) return sumLessThanK;
            // Sort the array in ascending order
            Array.Sort(A);          // O(NLogN) || Another way to Sort is A = A.OrderBy(x => x).ToArray<int>();

            int start = 0, last = A.Length - 1;
            while (start < last)    // O(N)
            {
                if (A[start] + A[last] >= K) last--;
                else sumLessThanK = Math.Max(sumLessThanK, A[start++] + A[last]);
            }
            return sumLessThanK;
        }



        // Iterative InOrder traversal + 2 Pointer Approach
        // Time O(N) || Space O(N) || 2 Pass
        public static bool FindTargetInBST(TreeNode root, int k)
        {
            List<int> numbers = new List<int>();
            InOrderIterative(root, numbers);    // O(N)
            int start = 0, last = numbers.Count - 1;
            while (start < last)                // O(N)
                if (numbers[start] + numbers[last] > k) last--;
                else if (numbers[start] + numbers[last] < k) start++;
                else return true;
            return false;
        }
        // Time O(N)
        public static void InOrderIterative(TreeNode current, List<int> numbers)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            while (true)
            {
                while (current != null)
                {
                    st.Push(current);
                    current = current.left;
                }
                if (st.Count == 0) break;
                var top = st.Pop();
                numbers.Add(top.val);
                current = top.right;
            }
        }

        // Recursive Approach using HashSet approach
        // Time O(N) || Space O(N) || 1 Pass
        public static bool FindTargetInBSTFaster(TreeNode root, int k) => FindTargetInBSTFasterUtil(root, k, new HashSet<int>());
        public static bool FindTargetInBSTFasterUtil(TreeNode root, int target, HashSet<int> set)
        {
            if (root == null) return false;
            if (set.Contains(target - root.val)) return true;
            set.Add(root.val);
            if (FindTargetInBSTFasterUtil(root.left, target, set)) return true;
            if (FindTargetInBSTFasterUtil(root.right, target, set)) return true;
            return false;
        }


        // Returns true if there exists 2 nums one in tree1 & second in tree2 such that their sum = target
        // Time O(N+M) || Space O(Max(N,M)), N & M are no of nodes in first & second tree respectively
        public static bool FindTargetInTwoBSTs(TreeNode root1, TreeNode root2, int target)
        {
            HashSet<int> secondTreeValues = new HashSet<int>(5000);
            FillSet(root2, secondTreeValues);
            return CheckTwoSumInBSTsExists(root1, target, secondTreeValues);
        }
        // Time O(N), N = no of Nodes in Tree
        public static void FillSet(TreeNode root, HashSet<int> set)
        {
            if (root == null) return;
            set.Add(root.val);
            FillSet(root.left, set);
            FillSet(root.right, set);
        }
        // Time O(M), M = no of Nodes in Tree
        public static bool CheckTwoSumInBSTsExists(TreeNode root, int target, HashSet<int> set)
        {
            if (root == null) return false;
            if (set.Contains(target - root.val)) return true;
            if (CheckTwoSumInBSTsExists(root.left, target, set)) return true;
            if (CheckTwoSumInBSTsExists(root.right, target, set)) return true;
            return false;
        }


        /// <summary>
        /// Returns the sum of every tree node's tilt.
        /// tilt of a tree node is the absolute difference between the sum of all left subtree node values and all right subtree node values.
        /// If a node does not have a left child, then the sum of the left subtree node values is treated as 0 same for right child.
        /// Time O(n) || Space O(1) || 1 Pass
        /// </summary>
        /// <param name="root"></param>
        /// <param name="oldTreeSum"></param>
        /// <returns></returns>
        public static int FindBinaryTreeTilt(TreeNode root, ref int oldTreeSum)
        {
            if (root == null) return 0;
            int oldLeftTreeSum = 0, oldRightTreeSum = 0;

            int newLeftTreeSum = FindBinaryTreeTilt(root.left, ref oldLeftTreeSum);
            int newRightTreeSum = FindBinaryTreeTilt(root.right, ref oldRightTreeSum);

            oldTreeSum = root.val + oldLeftTreeSum + oldRightTreeSum;
            root.val = Math.Abs(oldLeftTreeSum - oldRightTreeSum);
            return root.val + newLeftTreeSum + newRightTreeSum;
        }


        // Functions  returns the maximum value V for which there exist different nodes A and B where V = |A.val - B.val| and A is an ancestor of B
        // Time O(n) || Space O(1)
        public static int MaxAncestorDiff(TreeNode root, ref int min)
        {
            if (root == null) return 0;
            // update min if root.val is smaller
            min = Math.Min(min, root.val);
            int lMin = min, rMin = min;

            int leftDiff = MaxAncestorDiff(root.left, ref lMin);
            int rightDiff = MaxAncestorDiff(root.right, ref rMin);

            // update min if lMin or rMin is smaller
            min = Math.Min(min, Math.Min(lMin, rMin));

            // find currRoot max difference
            int currDiff = Math.Abs(root.val - min);

            // return max of currDiff or diff from left/right subTree
            return Math.Max(currDiff, Math.Max(leftDiff, rightDiff));
        }
        // Time O(n) | Space O(1) | Auxillary Space O(n), n = no of nodes in the binarytree
        public static int MaxAncestorDiffSimpler(TreeNode root)
        {
            int diff = 0;
            MaxDiff(root.left, root.val, root.val);
            MaxDiff(root.right, root.val, root.val);
            return diff;

            // local helper func
            void MaxDiff(TreeNode r, int largestAncestor, int smallestAncestor)
            {
                if (r == null) return;
                // update the max diff b/w Node and Ancestor
                diff = Math.Max(Math.Max(Math.Abs(smallestAncestor - r.val), Math.Abs(largestAncestor - r.val)), diff);
                // update the max
                largestAncestor = Math.Max(largestAncestor, r.val);
                // update the min
                smallestAncestor = Math.Min(smallestAncestor, r.val);
                // recursively calls the child nodes
                MaxDiff(r.left, largestAncestor, smallestAncestor);
                MaxDiff(r.right, largestAncestor, smallestAncestor);
            }
        }


        /// <summary>
        /// Functions return length of Longest Consecutive Path in Binary Tree
        /// path can be either increasing or decreasing, ex: 1,2,3,4] and [4,3,2,1] are both considered valid, but the path [1,2,4,3] is not valid.
        /// path can be in the child-Parent-child order, where not necessarily be parent-child order.
        /// Using PostOrder traversal Approach
        /// Time O(N) || Space O(1)
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int[] BinaryTreeLongestConsecutiveSequence(TreeNode root, ref int maxSortedLen)
        {
            if (root == null) return new int[2];
            int asc = 1, desc = 1;
            var ltSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.left, ref maxSortedLen);
            var rtSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.right, ref maxSortedLen);

            // if left is not null and absolute diff is 1
            if (root.left != null && Math.Abs(root.left.val - root.val) == 1)
            {
                // if Ascending order
                if (root.left.val - 1 == root.val) asc += ltSubTreeMaxLen[0];
                // if Descending order
                else desc += ltSubTreeMaxLen[1];
            }
            // if right is not null and absolute diff is 1
            if (root.right != null && Math.Abs(root.right.val - root.val) == 1)
            {
                // if Ascending order, and length greater than Ascending order length from left subtree
                if (root.right.val - 1 == root.val) asc = Math.Max(asc, 1 + rtSubTreeMaxLen[0]);
                // if Descending order, and length greater than Descending order length from left subtree
                else desc = Math.Max(desc, 1 + rtSubTreeMaxLen[1]);
            }

            // Update result if longer sequence found in either left-subtree or right-subtree
            // or sequence coming from left-subtree and passing thru root and going into right-subtree
            maxSortedLen = Math.Max(maxSortedLen, asc + desc - 1);

            // return Max Ascending & Descending sequence length possible from current Node
            return new int[] { asc, desc };

            #region PARTIAL SUCCESS ATTEMPTS
            /*
            if (root == null) return 0;
            int asc = 1, desc = 1;
            int ltSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.left, ref maxSortedLen);
            int rtSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.right, ref maxSortedLen);

            if (ltSubTreeMaxLen >= 1 && Math.Abs(root.left.val - root.val) == 1)
            {
                if (root.left.val - 1 == root.val) asc += ltSubTreeMaxLen;
                else desc += ltSubTreeMaxLen;
            }
            if (rtSubTreeMaxLen >= 1 && Math.Abs(root.right.val - root.val) == 1)
            {
                if (root.right.val - 1 == root.val) asc = Math.Max(asc, 1 + rtSubTreeMaxLen);
                else desc = Math.Max(desc, 1 + rtSubTreeMaxLen);
            }
            maxSortedLen = Math.Max(maxSortedLen, asc + desc - 1);
            return Math.Max(asc, desc);

            */

            /*
            if (root == null) return 0;
            int asc = 1, desc = 1;
            if (root.left != null) 
            {
                int ltSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.left, ref maxSortedLen);
                if (Math.Abs(root.left.val - root.val) == 1)
                    if (root.left.val - 1 == root.val) asc += ltSubTreeMaxLen;
                    else desc += ltSubTreeMaxLen;
            }
            if (root.right != null)
            {
                int rtSubTreeMaxLen = BinaryTreeLongestConsecutiveSequence(root.right, ref maxSortedLen);
                if (Math.Abs(root.right.val - root.val) == 1)
                    if (root.right.val - 1 == root.val) asc += rtSubTreeMaxLen;
                    else desc += rtSubTreeMaxLen;
            }
            maxSortedLen = Math.Max(maxSortedLen, asc + desc - 1);
            return Math.Max(asc, desc);
            */


            /*
            if (ascending)
            {
                if (parentValue + 1 == root.val) maxSortedLen++;
                else if (parentValue == 1 + root.val) { maxSortedLen = 2; ascending = !ascending; }
                else maxSortedLen = 1;
            }
            else // Descending
            {
                if (parentValue - 1 == root.val) maxSortedLen++;
                else if (parentValue + 1 == root.val) { maxSortedLen = 2; ascending = !ascending; }
                else maxSortedLen = 1;
            }

            return Math.Max(maxSortedLen, Math.Max(ltSubTreeMaxLen, rtSubTreeMaxLen));
            */


            /*
            //int maxSortedLen = 1, currSortedLen = 1;
            if (root == null) return 0;
            Stack<TreeNode> finishTime = new Stack<TreeNode>(100);
            int maxSortedLen = 1, currSortedLen = 1;
            TreeNode last = null;
            bool isAscending = true;
            while (true)
            {
                while (root != null)
                {
                    finishTime.Push(root);
                    root = root.left;
                }
                if (finishTime.Count == 0) break;
                var temp = finishTime.Pop();
                if (last != null)
                {
                    if (isAscending)
                    {
                        if (last.val < temp.val)
                            maxSortedLen = Math.Max(maxSortedLen, ++currSortedLen);
                        else
                        { maxSortedLen = Math.Max(maxSortedLen, currSortedLen = 2); isAscending = !isAscending; }
                    }
                    else // Descending
                    {
                        if (last.val > temp.val)
                            maxSortedLen = Math.Max(maxSortedLen, ++currSortedLen);
                        else
                        { maxSortedLen = Math.Max(maxSortedLen, currSortedLen = 2); isAscending = !isAscending; }
                    }
                }
                last = temp;
                root = temp.right;
            }
            return maxSortedLen;
            */
            #endregion
        }


        // Time O(r*c) | Space O(r+c)
        public static void SetMatrixZeroesEasy(int[][] matrix)
        {
            HashSet<int> rowsToBeZero = new HashSet<int>();
            HashSet<int> colsToBeZero = new HashSet<int>();
            int rows = matrix.Length, cols = matrix[0].Length;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (matrix[r][c] == 0)
                    {
                        rowsToBeZero.Add(r);
                        colsToBeZero.Add(c);
                    }
            // update all cells of distinct rows
            foreach (var r in rowsToBeZero)
                for (int c = 0; c < cols; c++)
                    matrix[r][c] = 0;
            // update all cells of distinct cols
            foreach (var c in colsToBeZero)
                for (int r = 0; r < rows; r++)
                    matrix[r][c] = 0;
        }

        // Time O(r*c) | Space O(1)
        public static void SetMatrixZeroesWithConstantSpace(int[][] matrix)
        {
            bool setFirstRow = false;
            int row = matrix.Length, col = matrix[0].Length;
            // check if we need to mark 1st row as Zero
            for (int c = 0; c < col; c++)
                if (matrix[0][c] == 0)
                {
                    setFirstRow = true;
                    break;
                }

            // Check the remaining matrix & mark 1st row & 1st Col as 0 if that row/col has zero
            for (int r = 1; r < row; r++)
                for (int c = 0; c < col; c++)
                    if (matrix[r][c] == 0)
                    {
                        matrix[0][c] = 0; // mark row
                        matrix[r][0] = 0; // mark col
                    }

            for (int r = 1; r < row; r++)
                if (matrix[r][0] == 0)
                    MarkZero(r, true);
            for (int c = 0; c < col; c++)
                if (matrix[0][c] == 0)
                    MarkZero(c, false);
            if (setFirstRow)
                MarkZero(0, true);

            // local helper func
            void MarkZero(int idx, bool markRow)
            {
                if (markRow)
                    for (int c = 0; c < col; c++)
                        matrix[idx][c] = 0;
                else
                    for (int r = 0; r < row; r++)
                        matrix[r][idx] = 0;
            }
        }


        // Time O(n) || Space O(1)
        public static void MergeSortedArray(int[] nums1, int m, int[] nums2, int n)
        {
            int lastIndex = --m + --n + 1;
            // while both the arrays still have elements to compare
            while (m >= 0 && n >= 0)
                nums1[lastIndex--] = nums2[n] > nums1[m] ? nums2[n--] : nums1[m--];
            // if second array still has elements to move
            while (n >= 0) nums1[lastIndex--] = nums2[n--];
        }


        // Time O(N^2) || Space O(1)
        public static void FlipAndInvertImage(int[][] A)
        {
            int len = A[0].Length;
            bool isOddLen = len % 2 == 1;
            for (int r = 0; r < A.Length; r++)
            {
                int start = -0, last = len - 1;
                while (start < last)
                {
                    var temp = (A[r][last] + 1) % 2;
                    A[r][last--] = (A[r][start] + 1) % 2;
                    A[r][start++] = temp;
                }
            }
            // Odd Length
            if (isOddLen)
            {
                // center column
                len /= 2;
                for (int r = 0; r < A.Length; r++)
                    A[r][len] = (A[r][len] + 1) % 2;
            }
        }


        // Function returns true if given 4 input coordinates form a Valid-Square
        // Time O(1) || Space O(1)
        public static bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            int[][] coordinate = { p1, p2, p3, p4 };
            // Sort first by X axis and than Y-axis
            var sortedCoordinate = coordinate.OrderBy(x => x[0]).OrderBy(y => y[1]).ToArray();

            // bottom edge
            var width1 = GetDistance(sortedCoordinate[0], sortedCoordinate[1]);

            // if any one side is of length 0, return false
            // [checking only for 1 side as if its not Zero than check at end will ensure no side equals zero]
            if (width1 == 0) return false;

            // left edge
            var height1 = GetDistance(sortedCoordinate[0], sortedCoordinate[2]);
            // top edge
            var width2 = GetDistance(sortedCoordinate[3], sortedCoordinate[2]);     // can also skip calculating this as its redundant
            // right edge
            var height2 = GetDistance(sortedCoordinate[3], sortedCoordinate[1]);

            // right bottom to left top
            var diagonal1 = GetDistance(sortedCoordinate[1], sortedCoordinate[2]);
            // left bottom to right top
            var diagonal2 = GetDistance(sortedCoordinate[0], sortedCoordinate[3]);

            // both diagonal are equal      & height == width   & both heights are same
            return diagonal1 == diagonal2 && height1 == width1 && height1 == height2;

            // both diagonal are equal      &       all sides are equal                                                     & no side is of length 0
            //return diagonal1 == diagonal2 && height1 == width1 && width2 == height2 && height1 == height2 && width1 == width2 && width1 != 0;
        }
        // returns distance b/w 2 coordinates
        public static double GetDistance(int[] p1, int[] p2) => Math.Sqrt(Math.Pow(p1[0] - p2[0], 2) + Math.Pow(p1[1] - p2[1], 2));


        // Linear Search // Time O(N) Space O(1) // Slowest Solution
        public static int MySqrtLinearSearch(int x)
        {
            if (x < 2) return x;
            int last = 1;
            for (int no = 2; no <= x >> 1; no++)
            {
                long square = (long)no * no;
                if (square == x) return no;
                else if (square > x) return last;
                last = no;
            }
            return last;
        }
        // Binary Search // Time O(LogN) Space O(1) Fastest Solution
        public static int MySqrtBinarySearch(int x)
        {
            if (x < 2) return x;
            int start = 2, last = x / 2;
            while (start <= last)
            {
                int pivot = start + (last - start) / 2;
                long square = (long)pivot * pivot;
                if (square > x) last = pivot - 1;
                else if (square < x) start = pivot + 1;
                else return pivot;
            }
            return last;
        }
        // Recursion Approach using formula Sqrt(x)=2*Sqrt(x/4) // Time O(LogN) Space O(LogN)
        public static int MySqrtRecursive(int x)
        {
            if (x < 2) return x;
            /* Sqrt(x)=2*Sqrt(x/4)
             * can be written as below in bit manipulation
             * Sqrt(x)=*Sqrt(x>>2))<<1
             */
            int left = MySqrtRecursive(x >> 2) << 1;
            return (long)(left + 1) * (left + 1) > x ? left : left + 1;
        }


        // Returns all possible "unique permutations" in any order.
        // Time O(N!) || Space O(N), N = length of input array
        public static IList<IList<int>> PermuteUnique(int[] nums)
        {
            List<IList<int>> uniquePermut = new List<IList<int>>();
            HashSet<string> uniqueSet = new HashSet<string>();
            HashSet<int> usedIndex = new HashSet<int>();
            GetUniquePermutations(nums, new int[nums.Length], uniqueSet, uniquePermut, usedIndex);
            return uniquePermut;
        }
        public static void GetUniquePermutations(int[] nums, int[] comboSoFar, HashSet<string> uniqueSet, List<IList<int>> uniquePermutation, HashSet<int> usedIndex, string currCombo = "", int depth = 0)
        {
            if (depth == nums.Length)
            {
                // below check ensure only UNIQUE combos are allowed, remove check to allow duplicates
                if (!uniqueSet.Contains(currCombo))
                {
                    // add to unique set
                    uniqueSet.Add(currCombo);
                    // add this permutation to the result
                    uniquePermutation.Add(comboSoFar.ToList<int>());
                }
            }
            else
            {
                for (int currentIndex = 0; currentIndex < nums.Length; currentIndex++)
                {
                    // If current index is not used
                    if (!usedIndex.Contains(currentIndex))
                    {
                        // update the combination
                        comboSoFar[depth] = nums[currentIndex];

                        /* One possible Optimization may be achieved by Sorting the input nums array
                         * and during backtracking we can skip to integer if its same as last interteger
                         * Means on facing duplicate character we simple add it to 'comboSoFar' and don't make recursive call
                         */

                        // new num which would be added to to 'currCombo' which is used to identify unique permuatations
                        string addToCombo = depth == 0 ? "" + nums[currentIndex] : "," + nums[currentIndex];

                        // mark current index as used
                        usedIndex.Add(currentIndex);

                        // Recursive Call
                        GetUniquePermutations(nums, comboSoFar, uniqueSet, uniquePermutation, usedIndex, currCombo + addToCombo, depth + 1);

                        // mark current index back as used
                        usedIndex.Remove(currentIndex);
                    }
                }
            }
        }



        // Return Max Possible Sum of "nums[n]*(n+1)" for the input array
        // Time O(n) || Space O(1)
        public static int MaxPossibleSumOfProductOfTheIndexesMultipliedByElement(int[] nums)
        {
            int last = nums.Length - 1;
            // skip all -ve values index from the end [these will only decrease Sum]
            while (last >= 0 && nums[last] <= 0) last--;

            int maxIndexProductSum = int.MinValue;
            // if only -ve values are present return max value from nums;
            if (last < 0)
            {
                foreach (var num in nums) maxIndexProductSum = Math.Max(maxIndexProductSum, num);
                return maxIndexProductSum;
            }

            int positiveSum = 0, negativeSum = 0;
            maxIndexProductSum = 0;
            // start from end
            for (int i = last; i >= 0; i--)
            {
                // -ve value
                if (nums[i] < 0)
                {
                    // if -ve hit we take by including this number is great than loss of Sum
                    // we exclude this number and adjust r final Sum
                    if (Math.Abs(nums[i] * (i + 1)) > (positiveSum - negativeSum))  // [-ve impact, which further increases as we iterate left]
                        // Since we skipping this index we will have to subtract all '+ve no sum added so far' to result
                        // and also increase by all '-ve no sum deducted so far'
                        maxIndexProductSum += -positiveSum + negativeSum;
                    else if (Math.Abs(nums[i]) == positiveSum)  // [zero impact, including/excluding this -ve integers makes no diff to final Sum]
                    { }
                    else // (Math.Abs(nums[n]) < positiveSum)   // [NO impact, but need tfurther increases as we iterate left]
                    {
                        negativeSum += Math.Abs(nums[i]);
                        maxIndexProductSum += nums[i] * (i + 1);
                    }
                }
                // +ve value
                else
                {
                    positiveSum += nums[i];
                    maxIndexProductSum += nums[i] * (i + 1);
                }
            }

            return maxIndexProductSum;
        }


        // Time O(n) || Space O(1) // first try
        [Obsolete("FAILS FOR 406th TC: 'abcdede' , 'ab.* de' ")]
        public static bool RegularExpressionMatching(string s, string p)
        {
            int sIndex = 0, pIndex = 0;
            bool wildSearchEnabledWithLastChar = false;
            char lastChar = ' ';
            // Queue keeps tracks of characters which come with wild char '*' and maintins the FIFO order thats used later during character matching
            Queue<char> zeroOrMore = new Queue<char>(p.Length);
            while (sIndex < s.Length && pIndex < p.Length)
            {
                // last character was wild char '*' hence check with last character before '*' till we dont see our current character from pattern
                if (wildSearchEnabledWithLastChar)
                {
                    wildSearchEnabledWithLastChar = false;
                    // if next character is again wild char move to next in pattern as this char would eventually be added to queue for matching later
                    if (pIndex + 1 < p.Length && p[pIndex + 1] == '*') continue;

                    while (zeroOrMore.Count > 0)
                    {
                        lastChar = zeroOrMore.Dequeue();
                        // if char before * and current pIndex char are not same
                        if (p[pIndex] != lastChar)
                            // increament index in string 's' till 'current Pattern Character' is not encountered & last character keeps matching
                            while (sIndex < s.Length && (s.Length - sIndex > p.Length - pIndex || (p[pIndex] != s[sIndex] && (s[sIndex] == lastChar || lastChar == '.')))) sIndex++;
                        else
                            while (sIndex < s.Length - (p.Length - pIndex) && (s[sIndex] == lastChar || lastChar == '.')) sIndex++;
                    }
                    continue;
                    //if (sIndex < s.Length && p[pIndex] != s[sIndex++])
                    //    return false;
                }
                else if (p[pIndex] == '*')
                {
                    wildSearchEnabledWithLastChar = true;
                    lastChar = p[pIndex - 1];
                    if (zeroOrMore.Count == 0 || zeroOrMore.First() != lastChar)
                        zeroOrMore.Enqueue(lastChar);
                }
                // skip check if next character from pattern is wild character '*'
                else if (pIndex + 1 < p.Length && p[pIndex + 1] == '*') { }
                // dot operator matches with any character hence simple move to next character
                else if (p[pIndex] == '.')
                {
                    sIndex++;
                    if (zeroOrMore.Count == 0 || zeroOrMore.First() != '.')
                        zeroOrMore.Enqueue('.');
                }
                // return false if characters don't match
                else if (p[pIndex] != s[sIndex++])
                    return false;

                // move to next char in pattern
                pIndex++;
            }
            // if last character from pattern is '*' wild character
            if (wildSearchEnabledWithLastChar)
                while (sIndex < s.Length && (s[sIndex] == lastChar || lastChar == '.'))
                    sIndex++;
            // Skip all wild characters left after current index in pattern string
            if (sIndex == s.Length)
                while (pIndex + 1 < p.Length && p[pIndex + 1] == '*')
                    pIndex += 2;

            // return true if we have reached end of both strings
            return sIndex == s.Length && pIndex == p.Length;
        }

        public static bool RegularExpressionMatchingRecursive(string s, string p)
        {
            if (p.Length == 0) return s.Length == 0;
            // evaluate 1st character match
            bool firstMatch = s.Length > 0 && (s[0] == p[0] || p[0] == '.');

            // if next character in pattern is wild character
            if (p.Length >= 2 && p[1] == '*')
                // return true if we get regEx match without using wild character (0 times)
                return RegularExpressionMatchingRecursive(s, p.Substring(2)) ||
                    // or we get regEx match by using 1 or more times the character before wild character
                    (firstMatch && RegularExpressionMatchingRecursive(s.Substring(1), p));
            else
                return firstMatch && RegularExpressionMatchingRecursive(s.Substring(1), p.Substring(1));
        }
        // DP Top-Down Approach
        public static bool RegularExpressionMatchingMemo(string s, string p, int[,] memo, int sID = 0, int pID = 0)
        {
            // 0 denotes we have never solved this sub-problem before,
            // 1 we have solved and regEx match was Succesfull
            // -1 we have solved and regEx match was Fail
            if (memo[sID, pID] != 0)
                return memo[sID, pID] == 1;

            bool ans = false;
            // reached end of pattern length
            if (pID == p.Length)
                ans = s.Length == sID;
            else
            {
                // evaluate 1st character match
                bool firstMatch = sID < s.Length && (s[sID] == p[pID] || p[pID] == '.');

                // if next character in pattern is wild character
                if (pID + 1 < p.Length && p[pID + 1] == '*')
                    // return true if we get regEx match without using wild character (0 times)
                    ans = RegularExpressionMatchingMemo(s, p, memo, sID, pID + 2) ||
                        // or we get regEx match by using 1 or more times the character before wild character
                        (firstMatch && RegularExpressionMatchingMemo(s, p, memo, sID + 1, pID));
                else
                    ans = firstMatch && RegularExpressionMatchingMemo(s, p, memo, sID + 1, pID + 1);
            }
            memo[sID, pID] = ans ? 1 : -1;
            return ans;
        }
        // Time = Space = O(n*m), n,m are length of string 's' & 'p' respectively
        public static bool RegularExpressionMatchingMemo_New(string s, string p)
        {
            /* ALGO
            the recursive soln for this problem aka brute force way has way too big of decision tree
            basicially for each star '*' we can either match it or skip using it.
            hence we have 2 choice to make which wud take the Time complexity to O(2^n)

            But if we cache the result for given index of 's' & 'p'
            it gets reduced to O(n*m)

            there are 2 main condition
            a. we get matching characters in s & p or we got '.' dot in p
            now we just need to check if recursive call to n+1,j+1 returns true
            b. we have a char (either actual char or dot) followed by *
            #b1 we dont use it n.e. n,j+2 we are are still to match any character from 's' and we skipped 2 characters in 'p'
            #b2 we use it once to match a char in 's' but keep index of 'p' the same so we can decide in future/recursion if we want to use it again or not n.e. n+1,j
             */
            int sLen = s.Length, pLen = p.Length;
            Dictionary<string, bool> cache = new Dictionary<string, bool>();
            return DFS(0, 0);
            // local helper func
            bool DFS(int i, int j)
            {
                // reached end of Regex and found match
                if (j == pLen) return i == sLen;

                var key = i + "," + j;
                // return if sub-problem is precomputed
                if (cache.ContainsKey(key)) return cache[key];

                // main check
                bool currCharMatch = i < sLen && (s[i] == p[j] || p[j] == '.');
                if (j + 1 < pLen && p[j + 1] == '*')
                    // Don't use * (skipping 2 characters in pattern 'p') || Use * excatly once
                    return cache[key] = DFS(i, j + 2) || (currCharMatch && DFS(i + 1, j));
                // if match not found continue and we don't have * following current character try just current characters
                else
                    return cache[key] = currCharMatch && DFS(i + 1, j + 1);

                // no match found
                return cache[key] = false;
            }
        }
        // DP Bottom-Up Approach
        public static bool RegularExpressionMatchingTab(string s, string p) => true;


        // Time O(1) || Space O(1)
        public static int PoorPigs(int buckets, int minutesToDie, int minutesToTest)
        {
            /* SOLUTION FROM LEETCODE
             * How many states does a pig have
             * If there is no time to test, n.e.minutesToTest / minutesToDie = 0, the pig has only one state -alive.
             * If minutesToTest / minutesToDie = 1 then the pig has a time to die from the poison, that means that now there are two states available for the pig : alive or dead.
             * One more step.If minutesToTest / minutesToDie = 2 then there are three available states for the pig : alive / dead after the first test / dead after the second test.
             * 
             * The number of available states for the pig is states = minutesToTest / minutesToDie + 1.
             * How many buckets could test x pigs with 2 available states
             * 
             * One pig could test 2 buckets - let's make him drink from the bucket number 1 and then wait minutesToDie time.
             * If he is alive - the poison is in the bucket number 2. If he is dead - the poison is in the bucket number 1.
             * 
             * The same way two pigs could test 2 ^ 2 = 4             * 
             * Hence if one pig has two available states, x pigs could test 2^x buckets.
             * 
             * How many buckets could test x pigs with s available states
             * After the discussion above, the answer is quite obvious : s ^ x buckets.
             * 
             * Let's consider as an example one pig with 3 states, n.e. s = minutesToTest / minutesToDie + 1 = 2 + 1 = 3, and show that he could test 3 buckets.
             * 
             * Solution:
             * Hence the problem is to find x such that states ^ x >= bucklets,
             * where x is a number of pigs, states = minutesToTest / minutesToDie + 1 is a number of states available for each pig
             * 
             * The solution is well known : x >= log(buckets) Base states.
             * 
             * To simplify the code let's rewrite the equation with the help of natural logarithms :
             * x >= (log(buckets)/ log(states))
             */

            var states = minutesToTest / minutesToDie + 1;
            return (int)Math.Ceiling((Math.Log(buckets) / Math.Log(states)));
        }


        // Time O(N) || Space O(1)
        public static IList<IList<int>> RemoveInterval(int[][] intervals, int[] toBeRemoved)
        {
            List<IList<int>> sortedIntervals = new List<IList<int>>(intervals.Length);

            for (int i = 0; i < intervals.Length; i++)
            {
                // Add input Interval as it is
                if (intervals[i][1] <= toBeRemoved[0] || intervals[i][0] >= toBeRemoved[1])
                    sortedIntervals.Add(new List<int>() { intervals[i][0], intervals[i][1] });
                // Add 2 Intervals
                else if (intervals[i][0] < toBeRemoved[0] && intervals[i][1] > toBeRemoved[1])
                {
                    sortedIntervals.Add(new List<int>() { intervals[i][0], toBeRemoved[0] });
                    sortedIntervals.Add(new List<int>() { toBeRemoved[1], intervals[i][1] });
                }
                // Add 1 || startTime of interval is < startTime_tobeRemoved
                else if (intervals[i][0] < toBeRemoved[0])
                    sortedIntervals.Add(new List<int>() { intervals[i][0], toBeRemoved[0] });
                // Add 1 || endTime of interval is > endTime _tobeRemoved
                else if (intervals[i][1] > toBeRemoved[1])
                    sortedIntervals.Add(new List<int>() { toBeRemoved[1], intervals[i][1] });
            }

            return sortedIntervals;
        }



        // Given an array of strings strs, group the anagrams together. You can return the answer in any order.
        // Categorize by Count, and stored by matching CODE (char and their count) in Dictionary
        // Time O(N) || Space O(N)
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<int, Dictionary<string, List<string>>> dict = new Dictionary<int, Dictionary<string, List<string>>>(100);
            SortedDictionary<char, int> alphaDict;

            for (int i = 0; i < strs.Length; i++)
            {

                alphaDict = new SortedDictionary<char, int>();
                for (int j = 0; j < strs[i].Length; j++)
                    if (!alphaDict.ContainsKey(strs[i][j])) alphaDict.Add(strs[i][j], 1);
                    else alphaDict[strs[i][j]]++;

                string CODE = "";   // ex: B2F1Z3
                foreach (var character in alphaDict)
                    CODE += "" + character.Key + character.Value;

                // if sub-dictionary of given length does'nt exists create one
                if (!dict.ContainsKey(strs[i].Length)) dict.Add(strs[i].Length, new Dictionary<string, List<string>>(strs.Length));

                // if given anagrams doesn't exists create new entry
                if (!dict[strs[i].Length].ContainsKey(CODE)) dict[strs[i].Length].Add(CODE, new List<string>() { strs[i] });
                // anagram already exists add new word to list of strings
                else dict[strs[i].Length][CODE].Add(strs[i]);
            }

            List<IList<string>> grp = new List<IList<string>>(strs.Length);
            foreach (var AnagramLengthGrp in dict.Values)
                foreach (var anagramsGrp in AnagramLengthGrp.Values)
                    grp.Add(anagramsGrp);

            return grp;
        }
        // Time O(n*l) Space O(n), n = no of words in 'strs' & l = avg length of each word
        public static IList<IList<string>> GroupAnagramsFaster(string[] strs)
        {
            var anagramsDict = new Dictionary<string, List<string>>();
            StringBuilder sb = new StringBuilder();

            foreach (var word in strs)           // O(n)
            {
                var CODE = GetKey(word);
                if (!anagramsDict.ContainsKey(CODE))     // new grp
                    anagramsDict[CODE] = new List<string>() { word };
                else                                    // adding curr word to matching anagram grp
                    anagramsDict[CODE].Add(word);
            }
            // add the grps to the final ans
            var ans = new List<IList<string>>();
            foreach (var grp in anagramsDict.Values)
                ans.Add(grp);
            return ans;
            // Local helper func
            string GetKey(string str)
            {
                var charCount = new int[26];
                foreach (var ch in str)          // O(l)
                    charCount[ch - 'a']++;

                sb.Clear();
                // Form CODE for char which have freq > 0, ex: B2F1Z3
                for (int i = 0; i < 26; i++)           // O(26)
                    if (charCount[i] > 0)
                        sb.Append(charCount[i]).Append((char)(i + 'a'));
                return sb.ToString();
            }
        }


        // Time O(N) || Space O(1)
        public static int LongestMountain(int[] A)
        {
            if (A.Length == 0) return 0;
            /* check for all cases:
             *      1: start of moutain
             *      2: continuing ascend
             *      3: peak found now descending
             *      4: descend ends, start of new mountain if(last and curr integer r not same)
             */
            int max = 0, longestIncreasing = 1;
            bool decreasing = false;
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i - 1] < A[i])
                {
                    // if decreasing is false means we are continuing ascend
                    longestIncreasing = !decreasing ? longestIncreasing + 1 : 2;
                    // else it means NEW moutain has started with len = 2
                    if (longestIncreasing == 2) decreasing = false;
                }
                // for descend to start we must have atleast 2 prior element one for start & 2nd for peak, else not valid moutain
                else if (A[i - 1] > A[i] && longestIncreasing > 1)
                {
                    longestIncreasing++;
                    max = Math.Max(max, longestIncreasing);
                    decreasing = true;
                }
                else // if (A[n-1]==A[n])
                    longestIncreasing = 1;
            }
            return max;
        }


        // Time O(nLogn) || Space O(1)
        public static int[][] MergeIntervals(int[][] intervals)
        {
            intervals = (from pair in intervals
                         orderby pair[1]
                         orderby pair[0]
                         select pair).ToArray();

            List<int[]> merged = new List<int[]>(intervals.Length) { intervals[0] };
            int currIndex = 0;
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] > merged[currIndex][1])
                { merged.Add(intervals[i]); currIndex++; }
                else if (intervals[i][0] <= merged[currIndex][1])
                    merged[currIndex][1] = Math.Max(intervals[i][1], merged[currIndex][1]);
            }
            return merged.ToArray();
        }

        // Time = Space = O(n), n = length of 'intervals' array
        public static int[][] InsertIntervals(int[][] intervals, int[] newInterval)
        {
            List<int[]> merged = [];
            int i = 0, l = intervals.Length, start = newInterval[0], end = newInterval[1];

            // add all intervals whose end time is smaller than new
            while (i < l && intervals[i][1] < start)
                merged.Add(intervals[i++]);

            // update merged interval start, if it overlaps with next
            if (i < l && ((start <= intervals[i][0] && intervals[i][0] <= end) || (start <= intervals[i][1] && intervals[i][1] <= end)))
            {
                start = Math.Min(start, intervals[i][0]);
                end = Math.Max(end, intervals[i++][1]);
            }

            // keep merging next intervals till there start is smaller than merged interval end time
            while (i < l && intervals[i][0] <= end)
            {
                start = Math.Min(start, intervals[i][0]);
                end = Math.Max(end, intervals[i++][1]);
            }

            merged.Add([start, end]);

            // add all remaining intervals
            while (i < l) merged.Add(intervals[i++]);

            return [.. merged];
        }
        // Time = Space = O(n), n = length of 'intervals' array
        public static int[][] InsertIntervalsNew(int[][] intervals, int[] newInterval)
        {
            if (intervals.Length < 1) return new int[][] { newInterval };
            int start = newInterval[0], end = newInterval[1], i = -1, l = intervals.Length;
            List<int[]> ans = new List<int[]>();
            bool insertionDone = false;
            while (++i < l)
                // last interval endTime is smaller than to be inserted interval startTime
                if (!insertionDone && intervals[i][1] >= start)
                {
                    insertionDone = true;
                    // new interval endTime is smaller than current start n.e. new sit between current and last interval
                    if (end < intervals[i][0])
                    {
                        ans.Add(newInterval);
                        ans.Add(intervals[i]);
                    }
                    else // need to join overlapping intervals
                    {
                        start = Math.Min(start, intervals[i][0]);    // take min of both start times
                        end = Math.Max(end, intervals[i][1]);    // take max of both end times
                        while (i + 1 < l)
                        {
                            // next interval startTime is smaller than current endTime than merge next interval into current one
                            if (intervals[i + 1][0] <= end)
                                end = Math.Max(end, intervals[++i][1]);
                            else break;
                        }
                        ans.Add(new int[] { start, end });
                    }
                }
                else    // add before and after intervals which are not affected by insertion
                    ans.Add(intervals[i]);
            // all intervals were smaller than start hence insert at the end
            if (!insertionDone)
                ans.Add(newInterval);
            return ans.ToArray();
        }

        // Time O(n) || Space O(1)
        public static int MaximumProductSubarray(int[] nums)
        {
            if (nums.Length == 0) return 0;
            int maxProduct, currMax, currMin;
            maxProduct = currMax = currMin = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                int tempCurrMax = currMax;
                currMax = Math.Max(Math.Max(currMax * nums[i], currMin * nums[i]), nums[i]);
                currMin = Math.Min(Math.Min(tempCurrMax * nums[i], currMin * nums[i]), nums[i]);
                maxProduct = Math.Max(maxProduct, currMax);
            }
            return maxProduct;
        }
        // Time O(n^3) || Space O(1)
        public static int MaximumProductSubarrayBruteForce(int[] nums)
        {
            if (nums.Length == 0) return 0;

            int len = nums.Length, maxProduct = nums[0];
            // to change len of sub-array
            for (int l = 0; l < len; l++)
                // to change the starting point of sub-array
                for (int startFrom = 0; startFrom < len - l; startFrom++)
                {
                    int subArrayProduct = nums[startFrom];
                    for (int index = startFrom + 1; index <= startFrom + l; index++)
                        subArrayProduct *= nums[index];
                    maxProduct = Math.Max(maxProduct, subArrayProduct);
                }
            return maxProduct;
        }

        // Time O(1) || Space O(1)
        public static int MirroReflection(int p, int q)
        {
            /* My Approach good only for half the test-cases
            if (q == 0) return 0;                   // 0 degree angle
            else if (q == p) return 1;              // 45 dedgree angle
            //else if (q == p / 2) return 2;
            else if ((double)p / 2 % q == 0) return 2;// half the length of side means it will hit the top left cornor
            else if ((double)p / 3 % q == 0) return 1;// 1/4th the length means it will hit the top right corner
            return 0;                               // if not above two will hit bottom right
            */
            /*  2-------1
             *  |       |
             *  |       |
             *  Source  0
             */
            //ulong p = (ulong)p1, q = (ulong)q1;
            if (q == 0) return 0;
            var lcm = (p * q) / Utility.GCD(p, q);

            // even no of bounces and lands on left
            if ((lcm / q) % 2 == 0)
                return 2;
            // odd no of bounces and lands on right & odd no of copies of square
            if ((lcm / p) % 2 == 0)
                return 0;
            // odd no of bounces and lands on right & even no of copies of square
            return 1;
        }


        // Failed Attempt for Generating Parenthesis, not sure why thou
        public static void GenerateParenthesis(int n, HashSet<string> alreadyAdded, List<string> result, string startWith = "", string endWith = "")
        {
            if (n == 0)
            {
                // add current valid is not already added
                if (!alreadyAdded.Contains(startWith + endWith))
                { alreadyAdded.Add(startWith + endWith); result.Add(startWith + endWith); }
            }
            else
            {
                // add opening bracket in startWith and closing bracket in endWith
                GenerateParenthesis(n - 1, alreadyAdded, result, "(" + startWith, endWith + ")");

                // add opening and closing bracket in startWith
                GenerateParenthesis(n - 1, alreadyAdded, result, "()" + startWith, endWith);

                // add opening and closing bracket in endtWith
                GenerateParenthesis(n - 1, alreadyAdded, result, startWith, endWith + "()");
            }
        }
        // Its given that this is nth Catalan Number
        // Time = Space = 4^n/(n(root n))
        public static void GenerateParenthesisBackTrack(int max, string validParan, int open, int close, List<string> result)
        {
            if (validParan.Length == max << 1) result.Add(validParan);
            else
            {
                if (open < max)
                    GenerateParenthesisBackTrack(max, validParan + "(", open + 1, close, result);
                if (close < open)
                    GenerateParenthesisBackTrack(max, validParan + ")", open, close + 1, result);
            }
        }


        // Time O(n) || Space O(1)
        public static int BestTimeToBuyAndSellStockII(int[] prices)
        {
            if (prices.Length < 2) return 0;
            int maxProfit = 0, lastPrice = prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > lastPrice)
                    maxProfit += prices[i] - lastPrice;
                lastPrice = prices[i];
            }
            return maxProfit;
        }

        // first attempt, fails when buying price for 1st tranx is best buying price even for 2nd tranx, n.e. making single buy + sell is better than 2
        // Time O(n) || Space O(1)
        public static int BestTimeToBuyAndSellStockIII(int[] prices)
        {
            if (prices.Length < 2) return 0;
            int maxProfit1 = 0, maxProfit2 = 0, minPrice = prices[0], i = 1;
            for (i = 1; i < prices.Length; i++)
            {
                if (prices[i] < prices[i - 1])
                {
                    if (maxProfit1 == 0) maxProfit1 = prices[i - 1] - minPrice;
                    else if (maxProfit2 == 0) maxProfit2 = prices[i - 1] - minPrice;
                    else if (prices[i - 1] - minPrice > maxProfit1)
                    {
                        maxProfit1 = maxProfit2;
                        maxProfit2 = prices[i - 1] - minPrice;
                    }
                    minPrice = prices[i];
                }
            }
            if (maxProfit1 == 0) maxProfit1 = prices[i - 1] - minPrice;
            else if (maxProfit2 == 0) maxProfit2 = prices[i - 1] - minPrice;
            else if (prices[i - 1] - minPrice > maxProfit1)
            {
                maxProfit1 = maxProfit2;
                maxProfit2 = prices[i - 1] - minPrice;
            }
            return maxProfit1 + maxProfit2;
        }

        // Divide and Conquer Approach || Time O(n) || Space O(n)
        public static int BestTimeToBuyAndSellStockIII_DivideAndConquer(int[] prices)
        {
            int len = prices.Length;
            int ltMin = prices[0], ltMaxProfit = 0, rtProfit = 0, maxProfit = 0, ltProfit = 0;

            int[] rtMax = new int[len];
            rtMax[len - 1] = prices[len - 1];
            // Update MaxValue avaliable on right hand of each index
            for (int i = len - 2; i >= 0; i--)
                rtMax[i] = Math.Max(rtMax[i + 1], prices[i]);

            for (int partitionAt = 0; partitionAt < len; partitionAt++)     // partitionAt is considered part of right array
            {
                // left side profit cal
                if (partitionAt > 0)
                    ltMin = Math.Min(ltMin, prices[partitionAt - 1]);
                if (partitionAt > 0)
                    ltProfit = Math.Max(ltProfit, prices[partitionAt - 1] - ltMin);
                ltMaxProfit = Math.Max(ltMaxProfit, ltProfit);

                // now right side profit cal
                rtProfit = Math.Max(0, rtMax[partitionAt] - prices[partitionAt]);

                // maximum total profit if we partition at current index
                maxProfit = Math.Max(maxProfit, ltMaxProfit + rtProfit);

                // Console.WriteLine($" {partitionAt}, {maxProfit}={ltMaxProfit}+{rtProfit} | {ltProfit}");
            }
            return maxProfit;

            /* Divide the problem into 2 parts
             * 1st tranx in left
             * 2nd tranx in right
             * Return maxProfit = left + right
             * 
             * create Lmin array which hold min value to BUY at so far
             * create Rmax array which hold max value to SELL at so far
             * we try to find maximize profit in left and right half
             */

            //var len = prices.Length;
            //if (len < 2) return 0;
            //int Lmin = prices[0], Rmax = prices[len - 1];

            //int[] left = new int[len];
            //// left to right pass [Sell after considering we have previously bought]
            //for (int n = 1; n < len; n++)
            //{
            //    // max profit possilble at current index is max of either profit at n-1 or curr selling price my Lmin
            //    left[n] = Math.Max(left[n - 1], prices[n] - Lmin);
            //    Lmin = Math.Min(Lmin, prices[n]);
            //}

            //int[] right = new int[len];
            //// right to left pass [Buy after considering we have previously sold]
            //for (int n = len - 2; n >= 0; n--)
            //{
            //    // max profit possilble at current index is max of either profit at n+1 or curr buying price my Rmax
            //    right[n] = Math.Max(right[n + 1], Rmax - prices[n]);
            //    Rmax = Math.Max(Rmax, prices[n]);
            //}

            ///* One optimization is calculating the total profit while calculating max profit for 'right array' above*/
            //int totalProfit = 0;
            //// taking each index as mid point for dividing the entire array to maximize totalProfit
            //for (int n = 0; n < len; n++)
            //    totalProfit = Math.Max(totalProfit, left[n] + right[n]);
            //return totalProfit;
        }


        // Recursive Soln to Decode Strings // Time O(n) || Space O(1)
        public static string DecodeString(string s)
        {
            string result = "";
            Stack<int> repeatBy = new Stack<int>();
            for (int i = 0; i < s.Length; i++)
                if (Char.IsDigit(s[i])) result += GetReaptedString(s, ref i);
                else result += s[i];
            return result;
        }
        public static string GetReaptedString(string s, ref int currIndex)
        {
            string countString = "";
            // keep iterating thru the digit till we dont find Opening Bracket
            while (s[currIndex] != '[') countString += s[currIndex++];
            int count = int.Parse(countString);

            string repeatedString = "";
            string result = "";
            // to skip onto next character after '[' Opening Bracket
            currIndex++;
            while (true)
            {
                // while ']' Closing bracket is not encoutered
                if (s[currIndex] == ']') break;
                // encountered second digit while parsing repeated string, make recursive call
                else if (char.IsDigit(s[currIndex])) repeatedString += GetReaptedString(s, ref currIndex);
                else repeatedString += s[currIndex];

                currIndex++;
            }
            while (count-- > 0)
                result += repeatedString;
            return result;
        }


        // Time O(NLogK) || Space O(1)
        public static int FindKthLargest(int[] nums, int k)
        {
            // heapify 1st k elements for MinHeap
            CreateMinHeap(nums, k);
            // now check for each n-k elements if an num is larger than root,
            // replace it with root and heapify the K-sized Heap
            for (int i = k; i < nums.Length; i++)
                // new no smaller than MinHeap root
                if (nums[i] > nums[0])
                {
                    // make new no the Root & Heapify
                    int temp = nums[0];
                    nums[0] = nums[i];
                    nums[i] = temp;
                    heapify(nums, k);
                }
            // once all elements in nums have been traversed
            // kth highest element would be root of the MinHeap
            return nums[0];
        }
        public static void CreateMinHeap(int[] minHeap, int len)
        {
            for (int i = (len - 1) >> 1; i >= 0; i--)
                heapify(minHeap, len, i);
        }
        // 'Percolate-Down' Operation
        public static void heapify(int[] minHeap, int len, int index = 0)
        {
            while (index < len)
            {
                int leftChild = (index << 1) + 1;
                int rtChild = leftChild + 1;
                int smaller = index;
                if (leftChild < len && minHeap[leftChild] < minHeap[index])
                    smaller = leftChild;
                if (rtChild < len && minHeap[rtChild] < minHeap[smaller])
                    smaller = rtChild;
                if (smaller != index)
                {
                    int temp = minHeap[index];
                    minHeap[index] = minHeap[smaller];
                    minHeap[smaller] = temp;
                    index = smaller;
                }
                else break;
            }
        }


        // Time O(NlogN) Space O(1)
        public static int FindContentChildren(int[] g, int[] s)
        {
            if (s.Length < 1) return 0;
            // sort in order of child with smallest need 1st
            Array.Sort(g);              // O(nlogn)
            // sort in order of cookie with smallest size 1st
            Array.Sort(s);              // O(nlogn)

            int contentChilds = 0, childIndex = g.Length - 1, cookieIndex = s.Length - 1;
            // start iterating from the last
            while (childIndex >= 0 && cookieIndex >= 0)     // O(n)
                // if size of biggest cookie can satisfy current child
                if (g[childIndex--] <= s[cookieIndex])
                {
                    cookieIndex--;
                    contentChilds++;
                }

            return contentChilds;
        }


        // Time O(n) || Space O(1)
        public static bool IsCousins(TreeNode root, int x, int y)
        {
            if (root == null || root.val == x || root.val == y) return false;

            int xDepth = 0, yDepth = 0;
            TreeNode xParent = null, yParent = null;

            GetParent(x, y, root, ref xParent, ref yParent, ref xDepth, ref yDepth, 0, null);
            return xDepth == yDepth && xParent != yParent;
        }
        public static void GetParent(int x, int y, TreeNode root, ref TreeNode xP, ref TreeNode yP, ref int xDepth, ref int yDepth, int currDepth, TreeNode parent)
        {
            if (root == null) return;
            if (root.val == x)
            {
                xP = parent;
                xDepth = currDepth;
            }
            else if (root.val == y)
            {
                yP = parent;
                yDepth = currDepth;
            }
            // already found both Nodes no need to search further
            if (xP != null && yP != null) return;

            GetParent(x, y, root.left, ref xP, ref yP, ref xDepth, ref yDepth, currDepth + 1, root);
            GetParent(x, y, root.right, ref xP, ref yP, ref xDepth, ref yDepth, currDepth + 1, root);
        }


        // Time O(n) || Space O(1)
        public static int MinDepthOfBinaryTree(TreeNode root)
        {
            if (root == null) return 0;
            int depth = int.MaxValue;
            GetMinDepth(root, ref depth, 1);
            return depth;
        }
        public static void GetMinDepth(TreeNode root, ref int depth, int currDepth)
        {
            if (root == null || currDepth >= depth) return;
            if (root.left == null && root.right == null)
                depth = currDepth;
            GetMinDepth(root.left, ref depth, currDepth + 1);
            GetMinDepth(root.right, ref depth, currDepth + 1);
        }


        // Recursive Sol || Throws Time Limit Exceeded for large value of 'n'
        public static int AtMostNGivenDigitSetRecursive(string[] digits, int n)
        {
            int count = 0;
            Array.Sort(digits);
            FindPositiveNumbers(digits, 0, digits.Length - 1, n, 0, ref count);
            return count;
        }
        public static void FindPositiveNumbers(string[] digits, int start, int last, int maxNumLimit, int numberTillNow, ref int count)
        {
            if (numberTillNow > maxNumLimit) return;

            for (int currIndex = start; currIndex <= last; currIndex++)
            {
                // if including current index no yields a <= 'maxNumLimit' no increament the count
                int isValidNumber = (numberTillNow * 10) + digits[currIndex][0] - '0';
                if (isValidNumber <= maxNumLimit)
                {
                    count++;
                    FindPositiveNumbers(digits, start, last, maxNumLimit, isValidNumber, ref count);
                }
                else break;
            }
        }
        // Recursive Sol || Throws Time Limit Exceeded for large value of 'n'
        public static int AtMostNGivenDigitSetDP(string[] digits, int n)
        {
            /* // Dynamic Programming + Counting
             * Intuition [https://leetcode.com/problems/numbers-at-most-n-given-digit-set/solution/]
             * First, call a positive integer X valid if X <= N and X only consists of digits from D. Our goal is to find the number of valid integers.
             * 
             * Say N has K digits. If we write a valid number with k digits (k < K), then there are (D\text{.length})^k(D.length) 
             * k possible numbers we could write, since all of them will definitely be less than N.
             * 
             * Now, say we are to write a valid K digit number from left to right. For example, N = 2345, K = 4, and D = '1', '2', ..., '9'.
             * Let's consider what happens when we write the first digit.
             * 
             *      If the first digit we write is less than the first digit of N,
             *      then we could write any numbers after,
             *      for a total of (D\text{.length})^{K-1}(D.length)^(K−1)
             *      valid numbers from this one-digit prefix. In our example,
             *      if we start with 1, we could write any of the numbers 1111 to 1999 from this prefix.
             *      
             *      If the first digit we write is the same, 
             *      then we require that the next digit we write is equal to or lower than the next digit in N.
             *      In our example (with N = 2345), if we start with 2, 
             *      the next digit we write must be 3 or less.
             *      
             *      We can't write a larger digit, because if we started with eg. 3, 
             *      then even a number of 3000 is definitely larger than N.
             */
            string num = n.ToString();
            int K = num.Length;
            int[] dp = new int[K + 1];
            dp[K] = 1;

            for (int i = K - 1; i >= 0; i--)
            {
                /* Compute dp[n] using below formula
                 * Lets dp[n] be the no of ways to write valid Number if N becomes N[n], N[n+1], N[n+2],...
                 * Ex int n = 2345 = string num = "2345", then 
                 * dp[0] would be maximum 2345
                 * dp[1] would be max 345
                 * dp[2] would be max 45
                 * dp[3] would be max 5
                 * 
                 * By above we can infer that formula :
                 *  dp[n] = (no of single_d in digits < num[n]) * Math.Power(digits.Length,K-n-1)
                 *          +
                 *          if any single_d in digits == num[n] than add dp[n+1] as well.
                 */
                int currNumDigit = num[i] - '0';
                foreach (var no in digits)
                {
                    if (no[0] - '0' < currNumDigit)
                        dp[i] += (int)Math.Pow(digits.Length, K - i - 1);
                    else if (no[0] - '0' == currNumDigit)
                        dp[i] += dp[i + 1];
                }
            }

            for (int i = 1; i < K; i++)
                dp[0] += (int)Math.Pow(digits.Length, i);

            return dp[0];
        }


        // Time O(N*K), N = no of words & K = length of 1st word (it's given all words have same length)
        public static int UniqueMorseRepresentations(string[] words)
        {
            // MorseCode representation of 26 english alphabets
            string[] morseCode = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };

            // Set to store unique transformatation
            HashSet<string> uniqueCode = new HashSet<string>(100);
            for (int i = 0; i < words.Length; i++)
            {
                string transformedWord = "";
                // create the transformation
                for (int j = 0; j < words[i].Length; j++)
                    transformedWord += morseCode[words[i][j] - 'a'];

                // add to to HashSet, which automatically keeps only unique words
                uniqueCode.Add(transformedWord);
            }
            // return unique Counts
            return uniqueCode.Count;
        }


        /// <summary>
        /// Time = Space = O(M^2 * N), where M is the length of each word and N is the total number of words in the input word list.
        /// Given two words(beginWord and endWord), and a dictionary's word list, find the length of shortest transformation sequence from beginWord to endWord, such that:
        ///     Only one letter can be changed at a time.
        ///     Each transformed word must exist in the word list.
        /// Note:
        ///     Return 0 if there is no such transformation sequence.
        ///     All words have the same length.
        ///     All words contain only lowercase alphabetic characters.
        ///     You may assume no duplicates in the word list.
        ///     You may assume beginWord and endWord are non-empty and are not the same.
        /// 
        /// Ex Input:
        /// beginWord = "hit",
        /// endWord = "cog",
        /// wordList = ["hot","dot","dog","lot","log","cog"]
        /// 
        /// Output: 5
        /// 
        /// Explanation: As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
        /// return its length 5.
        /// 
        /// </summary>
        /// <param name="beginWord"></param>
        /// <param name="endWord"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public static int WordLadder(string beginWord, string endWord, IList<string> wordList)
        {
            int listLen = wordList.Count;
            if (listLen < 1) return 0;
            int wordLen = wordList[0].Length;
            bool endWordFound = false;
            Dictionary<string, HashSet<string>> wordDict = new Dictionary<string, HashSet<string>>(100);

            string left, right, preProcessWord;
            // traverse thru all the words in the wordList
            for (int i = 0; i < listLen; i++)
            {
                // traverse thru all the indexes in given word & replace by excatly one index/letter by uniqueChar '*' and create the preProcess Map
                for (int j = 0; j < wordLen; j++)
                {
                    // dog => *og , d*g , do*
                    left = j > 0 ? wordList[i].Substring(0, j - 0) : "";
                    right = j < (wordLen - 1) ? wordList[i].Substring(j + 1) : "";
                    preProcessWord = left + "*" + right;

                    // Ex: *og keys contains => dog & log
                    if (wordDict.ContainsKey(preProcessWord)) wordDict[preProcessWord].Add(wordList[i]);
                    else wordDict.Add(preProcessWord, new HashSet<string>() { wordList[i] });
                }
                if (endWord == wordList[i]) endWordFound = true;
            }

            // if endWord is not present return 0 as transformation won't be possible
            if (!endWordFound) return 0;

            // Add 'beginWord' to wordDict mapping
            for (int j = 0; j < wordLen; j++)
            {
                // dog => *og , d*g , do*
                left = j > 0 ? beginWord.Substring(0, j - 0) : "";
                right = j < (wordLen - 1) ? beginWord.Substring(j + 1) : "";
                preProcessWord = left + "*" + right;

                // Ex: *og keys contains => dog & log
                if (wordDict.ContainsKey(preProcessWord)) wordDict[preProcessWord].Add(beginWord);
                else wordDict.Add(preProcessWord, new HashSet<string>() { beginWord });
            }


            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>(listLen);
            // Create UnDirected Graph for all keys in 'wordDict' with more than 1 word as its value list
            foreach (var hashset in wordDict.Values)
            {
                var connections = hashset.ToList<string>();
                if (connections.Count > 1)
                    for (int i = 0; i < connections.Count; i++)
                        for (int j = i + 1; j < connections.Count; j++)
                        {
                            // Edge U-->V
                            if (graph.ContainsKey(connections[i])) graph[connections[i]].Add(connections[j]);
                            else graph.Add(connections[i], new List<string>() { connections[j] });
                            // Edge V-->U
                            if (graph.ContainsKey(connections[j])) graph[connections[j]].Add(connections[i]);
                            else graph.Add(connections[j], new List<string>() { connections[i] });
                        }
            }
            return FindShortestDistanceInUnDirectedUnWeightedGraph(graph, beginWord, endWord);
        }
        // Time O(V+E) || Space O(V)
        public static int FindShortestDistanceInUnDirectedUnWeightedGraph(Dictionary<string, List<string>> g, string source, string destination)
        {
            if (g.Count == 0 || !g.ContainsKey(source) || !g.ContainsKey(destination)) return 0;

            Dictionary<string, int> distance = new Dictionary<string, int>(g.Count);
            foreach (var vertex in g.Keys)
                distance.Add(vertex, int.MaxValue);

            Queue<string> q = new Queue<string>();
            q.Enqueue(source);
            distance[source] = 1;

            while (q.Count > 0)
            {
                var vertexU = q.Dequeue();
                foreach (var vertexV in g[vertexU])
                    // new Vertex found, calculate the distance from source
                    if (distance[vertexV] == int.MaxValue || distance[vertexU] + 1 < distance[vertexV])
                    {
                        distance[vertexV] = distance[vertexU] + 1;
                        q.Enqueue(vertexV);
                    }
            }

            // if destination distance is not intMax return the value else return 0 indicating 'path not found'
            return distance[destination] != int.MaxValue ? distance[destination] : 0;
        }
        public static int WordLadderFaster(string beginWord, string endWord, IList<string> wordList)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

            for (int i = 0; i < wordList.Count; i++)                // O(n)
                graph.Add(wordList[i], new List<string>());                                 // add other nodes
            if (!graph.ContainsKey(beginWord)) graph.Add(beginWord, new List<string>());    // add source

            // Create graph => make connections (Join Edges among 1 edit distance words)
            for (int i = 0; i < wordList.Count; i++)                // O(n)
            {
                // Connect all nodes which are 1 edit distance away from beginword
                if (IsOneCharDiff(wordList[i], beginWord))          // O(m), m = avg length of the each word
                {
                    graph[wordList[i]].Add(beginWord);
                    graph[beginWord].Add(wordList[i]);
                }
                // we dont add 'endword' as problem says it must be present in List of words

                // Connect all nodes from list which are 1 edit distance away from each other
                for (int j = i + 1; j < wordList.Count; j++)        // O(n) & since its under outer loop of 'n' time total time is O(n^2)
                    if (IsOneCharDiff(wordList[i], wordList[j]))    // O(m), m = avg length of the each word, total time is 
                    {
                        graph[wordList[i]].Add(wordList[j]);
                        graph[wordList[j]].Add(wordList[i]);
                    }
            }
            if (!graph.ContainsKey(endWord)) return 0;  // we can stop check for shortest path if destination is not present in graph

            return ShortestPath(graph, beginWord, endWord);         // O(V+E), V = n+1, E = no of connections

            // returns true is 2 words differ by max 1 character
            bool IsOneCharDiff(string a, string b)
            {
                int diff = 0;
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i] && ++diff > 1)
                        return false;
                return true;
            }
            // BFS to find Shortest path in undirected graph
            int ShortestPath(Dictionary<string, List<string>> _graph, string source, string destination)
            {
                int V = _graph.Count;    // No of Vertices
                Dictionary<string, int> dist = new Dictionary<string, int>(V);
                Queue<string> q = new Queue<string>();
                q.Enqueue(source);
                dist.Add(source, 1);     // distance of source should be 0 but keeping it 1 Coz of stupid LeetCode testcases

                while (q.Count > 0)
                {
                    string parent = q.Dequeue();
                    List<string> adjacentNodes = _graph[parent];
                    for (int i = 0; i < adjacentNodes.Count; i++)
                        if (!dist.ContainsKey(adjacentNodes[i]))
                        {
                            if (adjacentNodes[i] == destination) return dist[parent] + 1;
                            dist.Add(adjacentNodes[i], dist[parent] + 1);
                            q.Enqueue(adjacentNodes[i]);
                        }
                }
                return 0;
            }
        }

        /// <summary>
        /// Given two words, beginWord and endWord, and a dictionary wordList,
        /// return the number of words in the shortest transformation sequence from beginWord to endWord,
        /// or 0 if no such sequence exists.
        /// 
        /// Time = Space = O(n*m), n = length of 'wordList' and m = avg length of each word
        /// </summary>
        /// <param name="beginWord"></param>
        /// <param name="endWord"></param>
        /// <param name="wordList"></param>
        /// <returns></returns>
        public static int WordLadderFastest(string beginWord, string endWord, IList<string> wordList)
        {
            /*  ALGO 
                Create a 1 UnDirected graph for each word in list and beginWord
                which has all possible cases it can be transformed into as values
                ex: 'hot' will have => '*ot' , 'h*t' , 'ho*'

                Now create second graph which holds values of all possible
                transformation and from where they created
                ex: '*ot' will have value => ''hot' , 'got' , 'rot' , etc
                n.e. all possible matching values within 1 edit distance from wordList
    
                Now we just run BFS from source and return depth of transformation on reaching endWord
                else return 0
             */
            Dictionary<string, List<string>> wordDict = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> transformDict = new Dictionary<string, List<string>>();
            List<string> transforms;
            foreach (var word in wordList)                          // O(n)
            {
                // get all possbile transformation for current word
                transforms = Transform(word.ToCharArray());         // O(m)
                // add original word and all its posible transformations
                wordDict[word] = transforms;
                // now for all the transformation add the word as value in transformDict
                foreach (var trans in transforms)                   // O(m)
                    if (!transformDict.ContainsKey(trans))
                        transformDict[trans] = new List<string>() { word };
                    else
                        transformDict[trans].Add(word);
            }
            // base case endWord not reachable
            if (!wordDict.ContainsKey(endWord)) return 0;

            // add begin word to graph
            if (!wordDict.ContainsKey(beginWord))
            {
                wordDict[beginWord] = Transform(beginWord.ToCharArray());
                // now for all the transformation add the word as value in transformDict
                foreach (var trans in wordDict[beginWord])          // O(m)
                {
                    // Console.WriteLine($" trans for original {trans}");
                    if (!transformDict.ContainsKey(trans))
                        transformDict[trans] = new List<string>() { beginWord };
                    else
                        transformDict[trans].Add(beginWord);
                }
            }

            // find shorted path from begin to end using BFS
            return ShortestPath();                                  // O(n*m)

            // local helper func
            int ShortestPath()
            {
                Dictionary<string, int> dist = new Dictionary<string, int>();
                Queue<string> q = new Queue<string>();
                q.Enqueue(beginWord);
                dist[beginWord] = 1;
                while (q.Count > 0)
                {
                    var cur = q.Dequeue();
                    var parentDist = dist[cur];
                    if (cur == endWord) return dist[endWord];
                    foreach (var possibleTransformations in wordDict[cur])
                        if (!dist.ContainsKey(possibleTransformations))
                        {
                            dist[possibleTransformations] = parentDist + 1;
                            foreach (var actualWordsInList in transformDict[possibleTransformations])
                                if (!dist.ContainsKey(actualWordsInList))
                                {
                                    q.Enqueue(actualWordsInList);
                                    dist[actualWordsInList] = parentDist + 1;
                                }
                        }
                }
                return 0;
            }
            List<string> Transform(char[] w)
            {
                List<string> ans = new List<string>();
                char org;
                for (int i = 0; i < w.Length; i++)
                {
                    org = w[i];
                    w[i] = '*';
                    ans.Add(new string(w));
                    w[i] = org;
                }
                return ans;
            }
        }


        // Time = Space = O(N)
        public static int HouseRobberI(int[] nums)
        {
            int len = nums.Length;
            if (len == 0) return 0;
            return MaxNonAdjacentArraySum(nums, 0, len, new Dictionary<int, int>(100));
        }
        // Time = Space = O(N)
        public static int HouseRobberII(int[] nums)
        {
            int len = nums.Length;
            if (len == 0) return 0;
            if (len == 1) return nums[0];
            // MaxAmt by robbering house 0..N-1 OR robbering house 1..N
            return Math.Max(MaxNonAdjacentArraySum(nums, 0, len - 1, new Dictionary<int, int>(100)),
                            MaxNonAdjacentArraySum(nums, 1, len, new Dictionary<int, int>(100)));
        }
        // DP Memoization approach (Top-Down)
        public static int MaxNonAdjacentArraySum(int[] nums, int i, int len, Dictionary<int, int> memo)
        {
            if (i >= len) return 0;
            if (memo.ContainsKey(i)) return memo[i];
            /* We have two choice at each index
             *      1) Either choose current index value and move to current+2 as we cannot select adjacent index
             *      2) Not selecting current index than we can select current+1
             */
            memo.Add(i, Math.Max(nums[i] + MaxNonAdjacentArraySum(nums, i + 2, len, memo), MaxNonAdjacentArraySum(nums, i + 1, len, memo)));
            return memo[i];
        }


        // DP based approach (Top-Down) || Time = Space = O(N) as each Node is visited just once
        public static int HouseRobberIII(TreeNode root) => MaxNonAdjacentTreeSumDP(root, new Dictionary<TreeNode, int>(100));
        public static int MaxNonAdjacentTreeSumDP(TreeNode root, Dictionary<TreeNode, int> memo)
        {
            if (root == null) return 0;
            if (memo.ContainsKey(root)) return memo[root];
            /* Approach is similar to HouseRobberI problem
             * at every Node we have below two choices to make to maximize the TOTAL SUM
             *      1) Select "current Root.val" + "SUM of (left + right child) of immediate left" + "SUM of (left + right child) of immediate right"
             *      2) Exclude current root.val and include "immediate left + right child SUM"
             *  return Max of above two choice.
             *  Do this recursively at every node.
             *  
             *  As one can notice this problem is ideal candidate for solving using DP as sub-problems are repeating 
             *  Hence to speed up the execution, save the result of current Node in a data-structure (n used Dictionary/HashTable)
             *  which can we refer later if we encounter same sub-problem again
             */

            memo.Add(root, Math.Max(
                                    root.val
                                        // SUM of (left + right child) of immediate left
                                        + MaxNonAdjacentTreeSumDP(root.left != null ? root.left.left : null, memo)
                                        + MaxNonAdjacentTreeSumDP(root.left != null ? root.left.right : null, memo)
                                        // SUM of (left + right child) of immediate right
                                        + MaxNonAdjacentTreeSumDP(root.right != null ? root.right.left : null, memo)
                                        + MaxNonAdjacentTreeSumDP(root.right != null ? root.right.right : null, memo)
                                    // immediate left + right child SUM
                                    , MaxNonAdjacentTreeSumDP(root.left, memo) + MaxNonAdjacentTreeSumDP(root.right, memo)
                                   )
                    );
            return memo[root];
        }

        /// <summary>
        /// Given a positive integer K, you need to find the length of the smallest positive integer N such that N is divisible by K,
        /// and N only contains the digit '1', Ex: K = 3 smallest answer is N = 111, which was length 3
        /// Time O(n) || Space O(N)
        /// </summary>
        /// <param name="K"></param>
        /// <returns></returns>
        public static int SmallestRepunitDivByK(int K)
        {
            if (K % 2 == 0) return -1;
            int remainder = 0;
            int count = 0;
            HashSet<int> set = new HashSet<int>(100);
            while (true)
            {
                remainder = (remainder * 10 + 1) % K;
                if (remainder == 0) return count + 1;
                ++count;

                // seen this remainder before than break the loop
                if (set.Contains(remainder)) break;
                // else add this remainder to the set
                else set.Add(remainder);
            }
            return -1;
        }


        /// <summary>
        /// 2 pointers approach
        /// Time O(N*26) = O(N) || Space O(26) = O(1)
        /// There is another intuitive method to solve the problem by using the Sliding Window Approach. The sliding window slides over the string s and validates each character. Based on certain conditions, the sliding window either expands or shrinks.
        /// A substring is valid if each character has at least k frequency.The main idea is to find all the valid substrings with a different number of unique characters and track the maximum length.Let's look at the algorithm in detail.
        /// 
        /// Algorithm
        /// Find the number of unique characters in the string s and store the count in variable maxUnique. For s = aabcbacad, the unique characters are a, b, c, d and maxUnique = 4.
        /// Iterate over the string s with the value of currUnique ranging from 1 to maxUnique. In each iteration, currUnique is the maximum number of unique characters that must be present in the sliding window.
        /// The sliding window starts at index windowStart and ends at index windowEnd and slides over string s until windowEnd reaches the end of string s. At any given point, we shrink or expand the window to ensure that the number of unique characters is not greater than currUnique.
        /// If the number of unique character in the sliding window is less than or equal to currUnique, expand the window from the right by adding a character to the end of the window given by windowEnd
        /// Otherwise, shrink the window from the left by removing a character from the start of the window given by windowStart.
        /// Keep track of the number of unique characters in the current sliding window having at least k frequency given by countAtLeastK. Update the result if all the characters in the window have at least k frequency.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int LongestSubstringWithAtLeastKRepeatingCharacters(string s, int k)
        {
            int result = 0;
            int len = s.Length;
            if (len < k) return result;

            HashSet<char> uniqueChars = new HashSet<char>(26);
            for (int i = 0; i < len; i++) uniqueChars.Add(s[i]);
            int[] charCount = null;

            int charsWithCountAtLeastK = 0, currUnique = 1, maxUnique = uniqueChars.Count;
            while (currUnique <= maxUnique)     // max 26 times
            {
                charCount = new int[26];
                charsWithCountAtLeastK = 0;
                uniqueChars.Clear();
                int windowS = 0, windowE = 0;
                while (windowE < len)           // O(N)
                {
                    uniqueChars.Add(s[windowE]);

                    // unique character with count equal to K
                    if (++charCount[s[windowE] - 'a'] == k) charsWithCountAtLeastK++;

                    while (uniqueChars.Count > currUnique)
                    {
                        if (charCount[s[windowS] - 'a']-- == k) charsWithCountAtLeastK--;
                        if (charCount[s[windowS] - 'a'] == 0) uniqueChars.Remove(s[windowS]);
                        windowS++;
                    }
                    // if all the 'unique characters' in current windows have count more than 'k'
                    if (charsWithCountAtLeastK == uniqueChars.Count)
                        result = Math.Max(result, windowE - windowS + 1);

                    windowE++;
                }
                currUnique++;
            }
            return result;
        }


        // Time O(n^2) || Space O(n)
        public static bool JumpGame(int[] nums, int[] cache, int curr = 0)
        {
            // reached last index
            if (curr == nums.Length - 1) return true;

            // current position has max jump distance Zero
            if (nums[curr] == 0 || curr >= nums.Length || cache[curr] == -1) return false;

            // try jumping from current index with all possible values <= maxJump
            for (int jumpDist = 1; jumpDist <= nums[curr]; jumpDist++)
                if (JumpGame(nums, cache, curr + jumpDist)) return true;

            // no jump permutation leads to last index return false
            cache[curr] = -1;
            return false;
        }

        // Time O(n) || Space O(1)
        public static bool JumpGame_LinearTime(int[] nums)
        {
            /* Iterating right-to-left, for each position we check if there is a potential jump that reaches a GOOD index
             * (currPosition + nums[currPosition] >= leftmostGoodIndex).
             * 
             * If we can reach a GOOD index, then our position is itself GOOD.
             * Also, this new GOOD position will be the new leftmost GOOD index. 
             * Iteration continues until the beginning of the array.
             * If first position is a GOOD index then we can reach the last index from the first position.
             */
            int toReachIndex = nums.Length - 1, i = nums.Length - 2;
            while (i >= 0)
                if (nums[i] + i >= toReachIndex)
                    toReachIndex = i--;
                else
                    i--;
            return toReachIndex == 0;
        }
        // Time O(N^2) || Space O(N) // Optimized DP Approach
        public static int JumpGameII_DP(int[] nums)
        {
            int len = nums.Length;
            int[] dp = new int[len];

            // Start from 2nd Last Index
            for (int i = len - 2; i >= 0; i--)
                // Biggest Jump from current index takes to last index or beyond means we need just 1 jump
                if (i + nums[i] >= len - 1)
                    dp[i] = 1;
                else
                {
                    dp[i] = int.MaxValue / 2;
                    // Find MinJumps starting with Biggest possible jump from current index
                    for (int k = nums[i]; k >= 1; k--)
                        dp[i] = Math.Min(dp[i], 1 + dp[i + k]);
                }

            return dp[0];
        }
        // Time O(N) || Space O(1) // Greedy Approach
        // https://leetcode.com/problems/jump-game-ii/discuss/485780/Python-O(-n-)-sol.-based-on-greedy-of-coverage.-90%2B-With-explanation
        public static int JumpGameII_Greedy(int[] nums)
        {
            if (nums.Length == 1) return 0;
            int currentCoverage = nums[0], lastJumpedIndex = 0, jumpCount = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                currentCoverage = Math.Max(currentCoverage, i + nums[i]);
                if (i == lastJumpedIndex)
                {
                    lastJumpedIndex = currentCoverage;
                    jumpCount++;
                    if (currentCoverage >= nums.Length - 1)
                        return jumpCount;
                }
            }
            return jumpCount;
        }
        // Time = Space = O(N) as every index is visited just once
        public static bool JumpGameIII(int[] nums, int[] cache, int curr)
        {
            // check boundary
            if (curr < 0 || curr >= nums.Length || cache[curr] == -1 || cache[curr] == -2) return false;

            // reached index with value 0
            if (0 == nums[curr] || cache[curr] == 1) return true;

            // special status indicating this index is under process
            cache[curr] = -2;

            // store actual result
            cache[curr] = (JumpGameIII(nums, cache, curr + nums[curr]) || JumpGameIII(nums, cache, curr - nums[curr])) ? 1 : -1;

            return cache[curr] == 1;
        }


        // Time O(n) || Space O(n), n = length of arr
        public static int JumpGameIV(int[] arr)
        {
            int l = arr.Length;
            if (l <= 1) return 0;       // 1st index is last index, No jumps req

            // Create a UnDirected graph, grp of indices with similar values together
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>(l);
            for (int i = 0; i < l; i++)
                if (!graph.ContainsKey(arr[i])) graph.Add(arr[i], new List<int>() { i });
                else graph[arr[i]].Add(i);


            int jumps = 0;

            bool[] visited = new bool[l];
            List<int> curr = new List<int>();
            curr.Add(0);                // Start from 0th index
            visited[0] = true;
            List<int> next = null;      // used to store next layer indices
            // BFS
            while (curr.Count > 0)
            {
                next = new List<int>();
                // iterate thru all index in current list
                foreach (int i in curr)
                {
                    // reached last index than return
                    if (i == l - 1) return jumps;
                    foreach (int sameValueIndex in graph[arr[i]])
                        if (!visited[sameValueIndex])    // Not already Visited
                        {
                            visited[sameValueIndex] = true;
                            next.Add(sameValueIndex);
                        }
                    // remove all same value index from graph as we have added them to the next list (avoid duplicate efforts)
                    graph[arr[i]].Clear();

                    // add index on left if not already visited
                    if (i - 1 >= 0 && !visited[i - 1])
                    {
                        next.Add(i - 1);
                        visited[i - 1] = true;
                    }
                    // add index on right if not already visited
                    if (i + 1 < l && !visited[i + 1])
                    {
                        next.Add(i + 1);
                        visited[i + 1] = true;
                    }
                }
                curr = next;
                jumps++;
            }
            return -1;
        }


        // Iterative InOrder traversal, Time O(N) Space O(H)
        public static TreeNode IncreasingOrderSearchTree(TreeNode root)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode newHead = null, prv = null;
            while (true)
            {
                while (root != null)
                {
                    st.Push(root);
                    root = root.left;
                }
                // breaking condition
                if (st.Count == 0) break;

                root = st.Pop();
                if (newHead == null) newHead = root;

                // make modification as asked in problem
                if (prv != null) prv.right = root;

                // set new node as prv now
                prv = root;

                root.left = null;
                root = root.right;
            }
            return newHead;
        }


        // Time O(n) || Space O(1)
        public static int KthFactorOfN(int n, int k)
        {
            if (n < k) return -1;
            for (int i = 1; i <= n; i++)
                if (n % i == 0 && --k == 0) return i;
            return -1;
        }



        // Time O(N) || Space O(N/3)
        // Issue with HashSet based algo is it fails when we have values close to int.Max which generate garbage when sum*=3;
        public int SingleNumberHashSet(int[] nums)
        {
            HashSet<int> hs = new HashSet<int>(nums.Length / 3 + 2);
            for (int i = 0; i < nums.Length; i++) hs.Add(nums[i]);

            int sum = 0;
            foreach (var num in hs) sum += num;

            // triple sum
            sum *= 3;
            for (int i = 0; i < nums.Length; i++) sum -= nums[i];

            return sum / 2;
        }
        // Dictionary/HashTable based approach
        // Time O(N) || Space O(N/3)
        public static int SingleNumberHashTable(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>(nums.Length / 3 + 2);
            for (int i = 0; i < nums.Length; i++)
                if (dict.ContainsKey(nums[i])) dict[nums[i]]++;
                else dict.Add(nums[i], 1);
            foreach (var kvp in dict)
                if (kvp.Value == 1) return kvp.Key;
            return -1;
        }
        // Bit Manipulation (NOT + AND + XOR) based approach
        // Time O(N) || Space O(1)
        public static int SingleNumber(int[] nums)
        {
            int seen_once = 0, seen_twice = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                seen_once = ~seen_twice & (seen_once ^ nums[i]);
                seen_twice = ~seen_once & (seen_twice ^ nums[i]);
            }
            return seen_once;
        }


        // Time O(NLogN) || Space O(N)
        public static string LargestNumber(int[] nums)
        {
            string[] numsArr = new string[nums.Length];
            // Time O(N)
            for (int i = 0; i < nums.Length; i++) numsArr[i] = nums[i] + "";

            // Time O(NLogN)
            Array.Sort(numsArr, new LargeNo());

            // If first char is 0 no need to check further
            if (numsArr[0] == "0") return "0";

            string result = "";
            for (int i = 0; i < nums.Length; i++) result += numsArr[i];

            return result;
        }
        public class LargeNo : IComparer<string>
        {
            public int Compare(string a, string b)
            {
                string first = a + b;
                string second = b + a;
                return second.CompareTo(first);
            }
        }


        // Time O(NLogN) || Space O(1)
        public static void NextPermutation(int[] nums)
        {
            int len = nums.Length;
            int i = len - 1;
            // from 2nd right index till >= 0
            while (--i >= 0)
                if (nums[i] < nums[i + 1])
                {
                    // search 1st number from last index which is great than nums[n]
                    for (int j = len - 1; j > i; j--)
                        if (nums[j] > nums[i])
                        {
                            int temp = nums[j];
                            nums[j] = nums[i];
                            nums[i] = temp;
                            break;
                        }
                    break;
                }
            // Sort the array in ascending from last => n+1 to End
            Array.Sort(nums, i + 1, len - i - 1);
        }


        // Generate an n x n square matrix and fill nums in range 1...N^2 spiralling inwards
        // Time O(N^2) || Space O(1)
        public static int[][] SpiralMatrixII(int n)
        {
            int[][] result = new int[n][];
            for (int k = 0; k < n; k++) result[k] = new int[n];

            int counter = 1, direction = 0;
            // 0 move right || 1 move down || 2 move left || 3 move up
            int top = 0, right = n - 1, bottom = n - 1, left = -1;
            while (top <= bottom || left <= right)
            {
                switch (direction)
                {
                    case 0:
                        for (int c = ++left; c <= right; c++)
                            result[top][c] = counter++;
                        break;
                    case 1:
                        for (int r = ++top; r <= bottom; r++)
                            result[r][right] = counter++;
                        break;
                    case 2:
                        for (int c = --right; c >= left; c--)
                            result[bottom][c] = counter++;
                        break;
                    case 3:
                        for (int r = --bottom; r >= top; r--)
                            result[r][left] = counter++;
                        break;
                }
                // update direction/ move rt 90 degree
                direction = (direction + 1) % 4;
            }

            return result;
        }


        // Brute Force Soln || Time O(N^2) || Space O(1)
        public int NumPairsDivisibleBy60(int[] time)
        {
            int counter = 0;
            for (int i = 0; i < time.Length; i++)
                for (int j = i + 1; j < time.Length; j++)
                    if ((time[i] + time[j]) % 60 == 0)
                        counter++;
            return counter;
        }
        // Efficient Soln || Time O(n) || Space O(60) ~O(1)
        public static int PairOfSongs(int[] time)
        {
            /* Since we for each song we need another song which on sum gives total in multiple of 60
             * we can re-phrase it as we need to find a song j whose time[j]%60 + current song n time[n]%60 is multiple of 60
             * or in other words (time[n] + time[j]) % 60 == 0
             * 
             *      So for each song we check if there exists any song whose reminder on addition to current song time give total sum 60
             *      if such a song is not present we save current song mod 60 in array, so next this can be used as pair to complete total time in 60s multiple
             *      
             * ex:
             * 1st Song 40 min [update array 40th Index by +1]
             * 2nd song 35 min , does there exists a song who has 25 min extra, ans NO,
             *      hence we also increament arr 35th index by 1
             * 3rd song 20 min, we search is there a song which has 40 min extra, ans yes (1st song)
             *      this forms 1 pair
             * 
             * this way we keep looking entire length of array
             * also remeber to increament the count with each passing song
             * 
             */
            int[] set = new int[60];
            int counter = 0;
            for (int i = 0; i < time.Length; i++)
            {
                counter += set[(60 - (time[i] % 60)) % 60];
                set[time[i] % 60]++;
            }
            return counter;
        }

        // Time O(n) || Space O(1)
        public static string BullsAndCows(string secret, string guess)
        {
            int[] notMatchedSecret = new int[10];
            int[] notMatchedGuess = new int[10];

            int bulls = 0, cows = 0;
            // count bulls => numbers at correct position
            for (int i = 0; i < secret.Length; i++)        // Time O(n)
                if (secret[i] == guess[i]) bulls++;
                else
                {
                    notMatchedSecret[secret[i] - '0']++;
                    notMatchedGuess[guess[i] - '0']++;
                }
            // count cows => numbers which are present but out of position
            for (int i = 0; i < 10; i++)                   // Time O(10)
                cows += Math.Min(notMatchedSecret[i], notMatchedGuess[i]);

            return bulls + "A" + cows + "B";
        }


        // Time O((n) || Space O(1)
        public static int RemoveDuplicates(int[] nums)
        {
            if (nums.Length < 3) return nums.Length;

            int nonDuplicateIndex = 0, counter = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                // new value
                if (nums[i] != nums[nonDuplicateIndex])
                {
                    nums[++nonDuplicateIndex] = nums[i];
                    counter = 1;
                }
                // must be same value
                else if (++counter <= 2) nums[++nonDuplicateIndex] = nums[i];
            }
            return nonDuplicateIndex + 1;
        }


        // Time O(N^3) || Space O(N^2)
        public static int MaxEqualRowsAfterFlips(int[][] matrix)
        {
            string[] orginalMatrix = matrix.Select(eachRow => string.Join("", eachRow.Select(eachNum => eachNum))).ToArray();

            int ans = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                string orginal = orginalMatrix[i];
                string compliment = new string(orginalMatrix[i].Select(eachChar => eachChar == '0' ? '1' : '0').ToArray());
                // max no of rows that matched current row original or compliment
                ans = Math.Max(ans, orginalMatrix.Where(eachRow => eachRow == orginal || eachRow == compliment).Count());
            }
            return ans;
        }


        // Time O(Max(N*K,M)) || Space O(M), N = length of string 's',  M = no of strings in 'words' array & K = M * length of single word in 'words' array.
        public static IList<int> SubstringWithConcatenationOfAllWords(string s, string[] words)
        {
            Dictionary<string, int> uniqCount = new Dictionary<string, int>(5000);
            // populate the hashTable with words and count
            // (if only unique words are present in words we can save space by using HashSet)
            for (int i = 0; i < words.Length; i++)                  // O(M)
                if (uniqCount.ContainsKey(words[i])) uniqCount[words[i]]++;
                else uniqCount.Add(words[i], 1);

            int singleWordLen = words[0].Length, concatLen = words.Length * singleWordLen, sLen = s.Length;

            List<int> result = new List<int>(sLen - concatLen + 1);

            Dictionary<string, int> used = new Dictionary<string, int>(5000);
            for (int i = 0; i < sLen - concatLen + 1; i++)          // O(N)
            {
                used.Clear();

                for (int j = i; j < concatLen - singleWordLen + 1 + i; j += singleWordLen)  // O(K)
                {
                    string part = s.Substring(j, singleWordLen);
                    // curr part not present in Dictionary
                    if (!uniqCount.ContainsKey(part)) break;
                    // curr part present in Dictionary but all instances have already been used previously
                    else if (used.ContainsKey(part) && used[part] >= uniqCount[part]) break;
                    else
                    {
                        if (used.ContainsKey(part)) used[part]++;
                        else used.Add(part, 1);
                        // once last single Word also matches given requirement add it to output
                        if (j == concatLen - singleWordLen + i)
                            result.Add(i);
                    }
                }
            }

            return result;
        }
        // Time O(Max(N*K,M)) || Space O(M), N = length of string 's',  M = no of strings in 'words' array & K = M * length of single word in 'words' array.
        public static IList<int> SubstringWithConcatenationOfAllWordsRecursive(string s, string[] words)
        {
            IList<int> ans = new List<int>();
            Dictionary<string, int> freq = new();
            int sLen = s.Length, wLen = words.Length, oneWordLen = words[0].Length, startingIndex = 0, requiredCharacters = wLen * oneWordLen;
            foreach (var word in words)
                if (!freq.TryGetValue(word, out int value)) freq[word] = 1;
                else freq[word] = ++value;

            Find();
            return ans;

            // local helper func
            void Find(int i = 0)
            {
                if (requiredCharacters == 0)
                    ans.Add(startingIndex);
                else
                    // continue only if we have enouf characters left to for every remaining word
                    while (sLen - i >= requiredCharacters)
                    {
                        // keep updating starting index till we find which result is answer
                        if (requiredCharacters == wLen * oneWordLen)
                            startingIndex = i;

                        var nextWord = s.Substring(i, oneWordLen);
                        if (freq.TryGetValue(nextWord, out int count) && count > 0)
                        {
                            freq[nextWord]--;
                            requiredCharacters -= oneWordLen;
                            Find(i + oneWordLen);
                            freq[nextWord]++;
                            requiredCharacters += oneWordLen;
                        }
                        // we can only move starting index all words need to b right next to each other
                        if (requiredCharacters == wLen * oneWordLen)
                            i++;
                        else break;
                    }
            }
        }


        // Time O(N*K), N = no of Nodes in Tree & K = no of deepest Nodes || Space O(K)
        public static TreeNode LCADeepestLeaves(TreeNode root)
        {
            // Find Max Depth
            int height = Height(root);          // O(N)

            // Fill List of Deepest Nodes
            List<TreeNode> ls = new List<TreeNode>(501);
            FillDeepest(root, ls, height);      // O(N)

            // Find LCA of deepest Nodes
            TreeNode lca = new TreeNode(int.MaxValue);
            for (int i = 0; i < ls.Count; i++)         // K times, K = no of deepest Node
                lca = LCA(root, lca, ls[i]);      // O(N)

            return lca;
        }
        public static int Height(TreeNode node) => node == null ? 0 : 1 + Math.Max(Height(node.left), Height(node.right));
        public static void FillDeepest(TreeNode root, List<TreeNode> ls, int deepest, int depth = 1)
        {
            if (root == null) return;
            if (deepest == depth) ls.Add(root);
            FillDeepest(root.left, ls, deepest, depth + 1);
            FillDeepest(root.right, ls, deepest, depth + 1);
        }
        public static TreeNode LCA(TreeNode root, TreeNode a, TreeNode b)
        {
            if (root == null) return null;
            if (root.val == a.val || root.val == b.val) return root;

            TreeNode left = LCA(root.left, a, b);
            TreeNode right = LCA(root.right, a, b);
            if (left != null && right != null)
                return root;
            else
                return left != null ? left : right;
        }


        // Time O(N*2^N) || Space O(N^2)
        public static void PalindromePartitioning_Recursive(string s, int start, int last, List<IList<string>> finalList, List<string> currlist)
        {
            if (start > last)
                finalList.Add(new List<string>(currlist));
            for (int end = start; end <= last; end++)
            {
                // Checking if sub-string of varying length begining at Index 0 isPalindrome
                if (isPalindrome(s, start, end))
                {
                    // add this sub-string to list
                    currlist.Add(s.Substring(start, (end - start) + 1));
                    // Recursively find Palindrome in remaining Half
                    PalindromePartitioning_Recursive(s, end + 1, last, finalList, currlist);
                    // backtrack and remove the current substring from currentList
                    currlist.RemoveAt(currlist.Count - 1);
                }
            }
        }

        // Time O(N) || Space O(1)
        public static bool isPalindrome(string s, int start, int last)
        {
            while (start < last)
                if (s[start++] != s[last--])
                    return false;
            return true;
        }

        // Time O(N*2^N) || Space O(N^2)
        public static void PalindromePartitioning_DP(string s, int start, int last, List<IList<string>> finalList, List<string> currlist, bool[,] dp)
        {
            if (start > last)
                finalList.Add(new List<string>(currlist));
            for (int end = start; end <= last; end++)
            {
                // Checking if sub-string of varying length begining at Index 0 isPalindrome
                if (s[start] == s[end] && (end - start < 2 || dp[start + 1, end - 1]))
                {
                    dp[start, end] = true;

                    // add this sub-string to list
                    currlist.Add(s.Substring(start, (end - start) + 1));
                    // Recursively find Palindrome in remaining Half
                    PalindromePartitioning_DP(s, end + 1, last, finalList, currlist, dp);
                    // backtrack and remove the current substring from currentList
                    currlist.RemoveAt(currlist.Count - 1);
                }
            }
        }


        // FIRST APPROACH
        // Time O(nLogn) || Space O(1)
        public static int[] SortedSquaresSlow(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
                nums[i] = nums[i] * nums[i];
            Array.Sort(nums);
            return nums;
        }
        // Optimized Approach
        // Time O(N) || Space O(N) for resultant array
        public static int[] SortedSquares(int[] nums)
        {
            int start = 0, last = nums.Length - 1;
            int[] squared = new int[last + 1];
            while (start <= last)
            {
                if (Math.Abs(nums[start]) > Math.Abs(nums[last]))
                    squared[last - start] = nums[start] * nums[start++];
                else
                    squared[last - start] = nums[last] * nums[last--];
            }
            return squared;
        }


        // Function return the values on the rightmost side of Tree, ordered from top to bottom.
        // Time O(N) || Space O(N)
        public static IList<int> BinaryTreeRightSideView(TreeNode root)
        {
            List<int> rtView = new List<int>();
            if (root == null) return rtView;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            q.Enqueue(null);
            TreeNode last = null;
            while (q.Count > 0)
            {
                root = q.Dequeue();
                if (root == null)
                {
                    rtView.Add(last.val);
                    if (q.Count > 0)
                        q.Enqueue(null);
                }
                else
                {
                    last = root;
                    if (root.left != null)
                        q.Enqueue(root.left);
                    if (root.right != null)
                        q.Enqueue(root.right);
                }
            }
            return rtView;
        }


        // Time O(nLogn) || Space O(n), n = no of workers
        public static double MincostToHireWorkers(int[] quality, int[] wage, int k)
        {
            /* ALGO
            1.The Problem is to HIRE 'K' workers in least total amt:
                Following Rules apply:
                    Every worker in paid group be paid in ratio of their quality compared to other workers in the paid group.
                    Every worker in the paid group must be paid at least their minimum wage expectation.
            2. First find and sort workers in increasing order of their ratio of 'wage/quality'
            3. Now add first 'K' workers to MaxHeap (based upon their 'quality'), also keep total sum of their quality
            4. We can calculate Min Cost to HIRE these first K workers by seleting ratio of highest wage/quality worker
                i.e. Kth worker(remeber sorted list) and multiplying by total quality sum.
            5. After Kth index, now we check new worker has quality smaller than worker on Top of MaxHeap, repalce n Heapify
            also update the sum of quality of K workers
            calculate MinCost = Math.Min(minCost,sumOfQuality * ratioOfKthWorker)
             */
            int l = quality.Length;
            WorkerPair[] increasingWageForSameQuality = new WorkerPair[l];
            for (int i = 0; i < l; i++)
                increasingWageForSameQuality[i] = new WorkerPair(quality[i], wage[i]);
            // sort in increasing order of cost per single unit of quality
            increasingWageForSameQuality = (from eachWorker in increasingWageForSameQuality     // O(nlogn)
                                            orderby eachWorker.costPerSingleUnitOfQuality
                                            select eachWorker).ToArray();

            int totalQuality = 0;
            var maxPQ = new PriorityQueue<int, int>(k);
            // first gather first 'k' worker
            for (int i = 0; i < k; i++)                                                                // O(klogk)
            {
                totalQuality += increasingWageForSameQuality[i].quality;
                maxPQ.Enqueue(increasingWageForSameQuality[i].quality, -increasingWageForSameQuality[i].quality);
            }
            // set the initial Total Cost i.e. costPer1Unit-quality aka of kth most efficient worker * totalQualityGathered
            // notice we are not using the 0th worker quality as its most efficient
            // since we need to pay atleast minWage of all worker hence worse rate currently is of 'k' worker is selected which is higher than all worker before it
            double totalCost = increasingWageForSameQuality[k - 1].costPerSingleUnitOfQuality * totalQuality;

            for (int i = k; i < l; i++)                                                                // O((n-k)*logk)
                if (maxPQ.Peek() > increasingWageForSameQuality[i].quality)
                {
                    // update TotalQuality by removing the worker which had the max Quality from KGrp/Heap and replacing with current lower quality worker
                    totalQuality += increasingWageForSameQuality[i].quality - maxPQ.Dequeue();
                    // replace the KGrp/Heap with worker which has lower quality
                    maxPQ.Enqueue(increasingWageForSameQuality[i].quality, -increasingWageForSameQuality[i].quality);
                    // try reducing totalCost
                    totalCost = Math.Min(totalCost, increasingWageForSameQuality[i].costPerSingleUnitOfQuality * totalQuality);
                }

            return totalCost;
        }


        // Time O(n) || Space O(1)
        public static bool IncreasingTripletSubsequence(int[] nums)
        {
            int l1 = int.MaxValue, l2 = int.MaxValue;
            for (int i = 0; i < nums.Length; i++)
                // If no greater than level 1
                if (nums[i] > l1)
                    // & also greater than level 2 than success
                    if (nums[i] > l2)
                        return true;
                    // if not greater than level 2 than make this level 2
                    else
                        l2 = nums[i];
                // else make it level 1
                else
                    l1 = nums[i];
            return false;
        }


        // Time O(K) || Space O(K)
        public static char DecodedStringAtIndex(string S, int K)
        {
            StringBuilder sb = new StringBuilder(K);
            int i = 0;
            while (sb.Length < K)
                if (Char.IsDigit(S[i]))
                {
                    int num = S[i++] - '0';
                    if (sb.Length * num > K)
                    {
                        int mod = K % sb.Length;
                        // if mod is 0 means current decoded string multiple == K, hence return last char
                        // else return Mod position char and strings index start with 0 hence subtract 1
                        return mod == 0 ? sb[sb.Length - 1] : sb[mod - 1];
                    }
                    else
                    {
                        string decoded = sb.ToString();
                        while (num-- > 1)
                            sb.Append(decoded);
                    }
                }
                else
                    sb.Append(S[i++]);

            return sb[sb.Length - 1];
        }
        // Time O(N) || Auxillary Space O(1), N = length of encoded string 'S'|| Algo uses Recursive Space
        public static char DecodedStringAtIndex_O1Space_Recursive(string S, int K)
        {
            if (K == 0) return S[0];

            int i = 0;
            long len = 0;
            while (len < K)
                if (Char.IsDigit(S[i]))
                {
                    int digit = S[i++] - '0';
                    if (len * digit < K) len *= digit;
                    else if (K % (int)len == 0) return DecodedStringAtIndex_O1Space_Recursive(S, (int)len);
                    else return DecodedStringAtIndex_O1Space_Recursive(S, K % (int)len);
                }
                else
                {
                    i++;
                    len++;
                }

            return S[i - 1];
        }
        // Time O(N) || Space O(1), N = length of encoded string 'S'
        public static char DecodedStringAtIndex_O1Space_Iterative(string S, int K)
        {
            int i = 0;
            long len = 0;
            while (len < K)
                if (Char.IsDigit(S[i]))
                {
                    int digit = S[i++] - '0';
                    if (len * digit < K) len *= digit;
                    else
                    {
                        // if mod is Zero means last decoded charater is the required ans
                        K = (K % len == 0) ? (int)len : K % (int)len;
                        i = 0;
                        len = 0;
                    }
                }
                else
                {
                    i++;
                    len++;
                }

            return (K == 0) ? S[0] : S[i - 1];
        }


        // Time O(n) || Space O(1)
        [Obsolete("Failing for some edge cases, Ex LC TestCase 63", true)]
        public static int SmallestRangeII_Faster(int[] A, int K)
        {
            int min = int.MaxValue, max = int.MinValue;
            for (int i = 0; i < A.Length; i++)
            {
                min = Math.Min(min, A[i]);
                max = Math.Max(max, A[i]);
            }

            int bMin = Math.Min(min + K, max - K), bMax = Math.Max(max - K, min + K);

            for (int i = 0; i < A.Length; i++)
                if (A[i] == min || A[i] == max) continue;
                // number can be adjust within new reduced boundry
                else if (A[i] - K >= bMin || A[i] + K <= bMax) continue;
                // number cannot be adjust within new reduced boundry
                // either by performing +k or -k operation
                else
                {
                    if (Math.Abs(bMax - (A[i] + K)) < Math.Abs((A[i] - K) - bMin))
                        bMax = Math.Max(bMax, A[i] + K);
                    else
                        bMin = Math.Min(bMin, A[i] - K);
                }

            return Math.Min(max - min, Math.Abs(bMax - bMin));
        }
        // Time O(nLogn) || Space O(1)
        public static int SmallestRangeII(int[] A, int K)
        {
            Array.Sort(A);
            int last = A.Length - 1;
            int ans = A[last] - A[0];
            for (int i = 0; i < last; i++)
            {
                int n1 = A[i], n2 = A[i + 1];
                int high = Math.Max(A[last] - K, n1 + K);
                int low = Math.Min(A[0] + K, n2 - K);
                ans = Math.Min(ans, high - low);
            }
            return ans;
        }


        // Time O(n) || Space O(1)
        public static ListNode RotateRight(ListNode head, int k)
        {
            if (head == null) return null;
            int len = GetLength(head);
            k %= len;

            if (k == 0) return head;        // if K == 0 no rotation required

            ListNode curr = head;
            while (--len - k > 0)           // get to K-1 th Node
                curr = curr.next;

            ListNode newHead = curr.next;   // now set Kth Node as 'newHead'
            curr.next = null;               // mark the now last node next as null

            curr = newHead;
            while (curr.next != null)       // move to end of the list (this step can be removed if we return last node after calculating length)
                curr = curr.next;

            curr.next = head;               // append original head at end now
            return newHead;

            int GetLength(ListNode headNode)
            {
                int count = 0;
                while (headNode != null)
                {
                    count++;
                    headNode = headNode.next;
                }
                return count;
            }
        }


        // Time = Space = O(2^N)
        public static IList<TreeNode> AllPossibleFBT(int N, Dictionary<int, List<TreeNode>> dict)
        {
            /* Every full binary tree T with 3 or more nodes, has 2 children at its root.
             * Each of those children left and right are themselves full binary trees.
             * 
             * Thus, for N≥3, we can formulate the recursion:
             * FBT(N) = [All trees with left child from FBT(x)and right child from FBT(N−1−x), for all x].
             */
            if (!dict.ContainsKey(N))
            {
                List<TreeNode> ans = new List<TreeNode>();
                if (N == 1) ans.Add(new TreeNode(0));
                else if (N % 2 == 1)
                {
                    for (int x = 0; x < N; x++)
                    {
                        int y = N - 1 - x;
                        foreach (var fbtX in AllPossibleFBT(x, dict))
                            foreach (var fbtY in AllPossibleFBT(y, dict))
                            {
                                TreeNode root = new TreeNode(0)
                                { left = fbtX, right = fbtY };
                                ans.Add(root);
                            }
                    }
                }
                dict.Add(N, ans);
            }
            return dict[N];
        }


        public static string Tree2str(TreeNode t)
        {
            if (t == null) return "";
            else return Tree2strUtil(t);
        }
        // Time O(n) || Space O(1)
        public static string Tree2strUtil(TreeNode t)
        {
            if (t == null) return "()";
            if (t.left == null && t.right == null)
                return t.val + "";
            else if (t.left != null && t.right != null)
                return t.val + "(" + Tree2strUtil(t.left) + ")" + "(" + Tree2strUtil(t.right) + ")";
            else
                if (t.left == null) return t.val + "()" + "(" + Tree2strUtil(t.right) + ")";
            else return t.val + "(" + Tree2strUtil(t.left) + ")";
        }


        // Time O(n^2) || Space O(n)
        public static IList<TreeNode> FindDuplicateSubtrees(TreeNode root, HashSet<string> hset, Dictionary<string, TreeNode> ans)
        {
            if (root == null) return new List<TreeNode>();
            if (root.left != null)
            {
                string leftSubTree = Tree2strUtil(root.left);
                if (hset.Contains(leftSubTree))
                {
                    if (!ans.ContainsKey(leftSubTree))
                        ans.Add(leftSubTree, root.left);
                }
                else
                    hset.Add(leftSubTree);
                FindDuplicateSubtrees(root.left, hset, ans);
            }
            if (root.right != null)
            {
                string rightSubTree = Tree2strUtil(root.right);
                if (hset.Contains(rightSubTree))
                {
                    if (!ans.ContainsKey(rightSubTree))
                        ans.Add(rightSubTree, root.right);
                }
                else
                    hset.Add(rightSubTree);
                FindDuplicateSubtrees(root.right, hset, ans);
            }
            List<TreeNode> result = new List<TreeNode>();
            foreach (var duplicate in ans.Values)
                result.Add(duplicate);
            return result;
        }
        // Time O(n) || Space O(n)
        public static IList<TreeNode> FindDuplicateSubtreesFaster(TreeNode root)
        {
            HashSet<string> uniqueTrees = new HashSet<string>();
            Dictionary<string, TreeNode> duplicates = new Dictionary<string, TreeNode>();

            FindDuplicateSubtreesUtil(root, uniqueTrees, duplicates);

            List<TreeNode> result = new List<TreeNode>(duplicates.Count);

            // remove empty node duplicate if present
            if (duplicates.ContainsKey("()"))
                duplicates.Remove("()");

            foreach (var duplicate in duplicates.Values)
                result.Add(duplicate);

            return result;
        }
        // Time O(n) || Space O(1)
        public static string FindDuplicateSubtreesUtil(TreeNode root, HashSet<string> uniqueTrees, Dictionary<string, TreeNode> duplicates)
        {
            if (root == null) return "()";
            string leftSubTree = FindDuplicateSubtreesUtil(root.left, uniqueTrees, duplicates);
            string rtSubTree = FindDuplicateSubtreesUtil(root.right, uniqueTrees, duplicates);

            if (uniqueTrees.Contains(leftSubTree))
            {
                if (!duplicates.ContainsKey(leftSubTree))
                    duplicates.Add(leftSubTree, root.left);
            }
            else
                uniqueTrees.Add(leftSubTree);

            if (uniqueTrees.Contains(rtSubTree))
            {
                if (!duplicates.ContainsKey(rtSubTree))
                    duplicates.Add(rtSubTree, root.right);
            }
            else
                uniqueTrees.Add(rtSubTree);

            return root.val + "(" + leftSubTree + rtSubTree + ")";
        }


        // Time O(n) || Space O(n), where n = no of elements in nums2
        public static int[] NextGreaterElementI(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> nextHighest = new Dictionary<int, int>(nums2.Length);
            // stores next highest for each element in nums2
            Stack<int> st = new Stack<int>(nums2.Length);

            // start reading from end of nums2
            for (int i = nums2.Length - 1; i >= 0; i--)
            {
                // remove all smaller or equal to numbers from Stack till stack is not empty
                while (st.Count > 0 && st.Peek() <= nums2[i])
                    st.Pop();
                // no add current number to dictionary along with its nextHighest value is Stack is not empty else append -1
                nextHighest.Add(nums2[i], st.Count != 0 ? st.Peek() : -1);
                st.Push(nums2[i]);
            }

            int[] result = new int[nums1.Length];
            // fetch nextHighest for all required numbers from dictionary and store in result array
            for (int i = 0; i < nums1.Length; i++)
                result[i] = nextHighest[nums1[i]];

            return result;
        }



        // Time O(n) || Space O(n), where n = no of elements in nums
        public static int[] NextGreaterElementII(int[] nums)
        {
            int[] result = new int[nums.Length];

            // stores next highest for each element in nums, prepare Stack before start iterating from last element in 2nd loop
            Stack<int> st = new Stack<int>(nums.Length);
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                // remove all smaller or equal to numbers from Stack till stack is not empty
                while (st.Count > 0 && st.Peek() <= nums[i])
                    st.Pop();
                st.Push(nums[i]);
            }

            // start reading from end of nums
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                // remove all smaller or equal to numbers from Stack till stack is not empty
                while (st.Count > 0 && st.Peek() <= nums[i])
                    st.Pop();
                result[i] = st.Count > 0 ? st.Peek() : -1;
                st.Push(nums[i]);
            }
            return result;
        }


        // Time O(nLogn) || Space O(n) , n = no of digits in input 'n'
        public static int NextGreaterElementIII(int n)
        {
            /* Start from right(least significant digits) find a digit which is smaller than one to its right
             *      now place bigger digit at current smaller digit place and sort all the remaing digit in asc order to get next highest
             *  If so such digit exists return -1
             */
            int num = n;
            List<int> ls = new List<int>(10);
            // Store individual digit starting from least significant ones in list
            while (num > 0)
            {
                ls.Add(num % 10);
                num /= 10;
            }

            //// if single digit number or n is intMax value
            //if (ls.Count == 1 || n = int.MaxValue) return -1;

            bool foundSmaller = false;
            for (int i = 1; i < ls.Count; i++)
            {
                if (ls[i] < ls[i - 1])
                {
                    // replace current digit with least significant digit greater than current digit n.e. ls[n]
                    for (int k = 0; k < i; k++)
                        if (ls[k] > ls[i])
                        {
                            int temp = ls[i];
                            ls[i] = ls[k];
                            ls[k] = temp;
                            break;
                        }
                    // now sort all digit (less significant ones to get next smallest number possible)
                    ls.Sort(0, i, new Desc());
                    foundSmaller = true;
                    break;
                }

            }

            if (!foundSmaller) return -1;   // all increasing digits only when starting from least significant to most significant

            long result = 0;
            // build the nextGreaterNumber
            for (int i = ls.Count - 1; i >= 0; i--)
                result = result * 10 + ls[i];

            return result <= int.MaxValue ? (int)result : -1;
        }
        public class Desc : IComparer<int>
        {
            public int Compare(int n1, int n2) => n2.CompareTo(n1);
        }



        // Time O(n) || Auxillary Space O(1) || Recursive Space O(n)
        public static TreeNode InsertIntoBST(TreeNode root, int val)
        {
            if (root == null)
                return root = new TreeNode(val);

            // if value is bigger than root value, insert in right-Subtree
            if (root.val < val)
                root.right = InsertIntoBST(root.right, val);
            // Insert in left-Subtree
            else
                root.left = InsertIntoBST(root.left, val);

            return root;
        }


        // Time O(rows*cols) || Space O(1)
        public static int[] FindDiagonalOrder(int[][] matrix)
        {
            int rows = matrix.Length;
            if (rows == 0) return new int[0];
            int cols = matrix[0].Length;

            int[] ans = new int[rows * cols];
            int i = 0, r = 0, c = 0;
            bool up = true;

            while (i < ans.Length)
                if (up)
                {
                    if (isValidCell(r, c))
                        ans[i++] = matrix[r--][c++];
                    else                    // Change direction
                    {
                        up = !up;
                        if (isValidCell(++r, c)) { }
                        else { ++r; --c; }
                    }
                }
                else
                {
                    if (isValidCell(r, c))
                        ans[i++] = matrix[r++][c--];
                    else                    // Change direction
                    {
                        up = !up;
                        if (isValidCell(r, ++c)) { }
                        else { --r; ++c; }
                    }
                }

            return ans;
            // local func to check validCell
            bool isValidCell(int row, int col) => (row < 0 || col < 0 || row >= rows || col >= cols) ? false : true;
        }


        /// <summary>
        /// Given a binary tree (with root node root), a target node, and an integer value K.
        /// Return a list of the values of all nodes that have a distance K from the target node.
        /// The answer can be returned in any order.
        /// Time O(n) 2-Pass || Space O(n)
        /// </summary>
        /// <param name="root"></param>
        /// <param name="target"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static IList<int> NodesAtKDistanceFromGivenNodeInBinaryTree(TreeNode root, TreeNode target, int k)
        {
            // DFS
            Dictionary<TreeNode, TreeNode> nodeParent = new Dictionary<TreeNode, TreeNode>();
            dfs(root, null);

            // Queue which will eventually hold all kth Distance nodes from given Target Node
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(null);        // in case k = 0
            q.Enqueue(target);      // starting Node for BFS

            // as we don't want to add target node itself or null nodes to ans
            HashSet<TreeNode> seen = new HashSet<TreeNode>();
            seen.Add(target);
            seen.Add(null);

            int distance = 0;
            List<int> ans = new List<int>();
            // BFS
            while (q.Count > 0)
            {
                TreeNode curr = q.Dequeue();
                if (curr == null)
                {
                    if (k == distance)
                        while (q.Count > 0)
                            ans.Add(q.Dequeue().val);
                    else
                    {
                        q.Enqueue(null);
                        distance++;
                    }
                }
                else
                {
                    if (!seen.Contains(curr.left))          // add left child if not already seen
                    {
                        seen.Add(curr.left);
                        q.Enqueue(curr.left);
                    }
                    if (!seen.Contains(curr.right))         // add right child if not already seen
                    {
                        seen.Add(curr.right);
                        q.Enqueue(curr.right);
                    }
                    TreeNode parentNode = nodeParent[curr];
                    if (!seen.Contains(parentNode))         // add node parent if not already seen
                    {
                        seen.Add(parentNode);
                        q.Enqueue(parentNode);
                    }
                }
            }

            return ans;

            // local func to create node-parent mapping
            void dfs(TreeNode node, TreeNode parent)
            {
                if (node != null)
                {
                    nodeParent.Add(node, parent);
                    dfs(node.left, node);
                    dfs(node.right, node);
                }
            }
        }


        // Time O(n) 1-Pass thru each Tree || Space O(n)
        public static bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            List<int> ls1 = new List<int>();
            GetLeafNodes(root1);
            int matchWith = 0;
            // match all leaf nodes from both trees and also check if if we have matched all leafs
            return MatchSecondLeafWithFirst(root2, ref matchWith) && matchWith == ls1.Count;

            // add all leaf nodes from first Tree to a List
            void GetLeafNodes(TreeNode root)
            {
                if (root == null) return;
                if (root.left == null && root.right == null)
                    ls1.Add(root.val);
                GetLeafNodes(root.left);
                GetLeafNodes(root.right);
            }
            // Now check foreach leaf nodes from second Tree with corrospoding leaf node from first
            bool MatchSecondLeafWithFirst(TreeNode root, ref int i)
            {
                if (root == null) return true;
                if (root.left == null && root.right == null)
                    // if 2nd tree has more leafs than 1st or leafs value don't match return false
                    if (i >= ls1.Count || ls1[i++] != root.val) return false;
                return MatchSecondLeafWithFirst(root.left, ref i) && MatchSecondLeafWithFirst(root.right, ref i);
            }
        }


        // Time O(N) || Space O(N), N = no of edges
        public static int[] FindRedundantConnectionFasterButFaulty(int[][] edges)
        {
            int V = edges.Length;
            // Since Nodes are 1 indexed
            List<int>[] graph = new List<int>[V + 1];

            for (int i = 0; i < V; i++)
            {
                int u = edges[i][0];
                int v = edges[i][1];
                if (graph[u] == null) graph[u] = new List<int>();
                if (graph[v] == null) graph[v] = new List<int>();

                graph[u].Add(v);
                graph[v].Add(u);
            }
            // Find the Cycle and add Nodes in cycle to HashSet
            HashSet<int> cycle = new HashSet<int>();

            //for (int n = 1; n <= V; n++)
            //    if (graph[n].Count > 1)  // In Order for Cycle to Pass thru Node it must have atleast 2 edges
            //        if (DetectCycle(graph, n, cycle, new int[V + 1]))
            //            break;
            DetectCycle(graph, 1, cycle, new int[V + 1]);

            for (int i = V - 1; i >= 0; i--)
            {
                int u = edges[i][0];
                int v = edges[i][1];
                // starting from the end we are looking for the edge whoes both nodes are part of cycle in graph
                if (cycle.Contains(u) && cycle.Contains(v))
                    return new int[] { u, v };
            }
            return new int[0];
        }
        // Time O(V+E) || Space O(V)
        public static bool DetectCycle(List<int>[] graph, int source, HashSet<int> cycle, int[] visited)
        {
            Queue<int> q = new Queue<int>(visited.Length);
            int[] parent = new int[visited.Length + 1];

            q.Enqueue(source);
            visited[source] = 1;        // Marked Visited & in Queue
            parent[source] = -1;
            while (q.Count > 0)
            {
                int u = q.Dequeue();
                visited[u] = -1;   // Mark Visited and Not-in Queue  
                foreach (int v in graph[u])
                    if (visited[v] == 0)   // not visited yet
                    {
                        parent[v] = u;
                        visited[v] = 1;
                        q.Enqueue(v);
                    }
                    else if (visited[v] == 1)// already visited, cycle found
                    {
                        //// Add all node which led to nodes U &V to find the nodes included in cycle
                        //int node = v;
                        //while (node != -1)
                        //{
                        //    cycle.Add(node);
                        //    node = parent[node];
                        //}
                        //node = u;
                        //while (node != -1)
                        //{
                        //    cycle.Add(node);
                        //    node = parent[node];
                        //}
                        /* Find out the common parent of U & V
                         * than fill in all parents of U & V in HasSet cycle till current Node is not equal to common parent
                         * at last add common parent to list of nodes which form up the cycle
                         */
                        List<int> nodeToReachU = new List<int>();
                        HashSet<int> nodeToReachV = new HashSet<int>();
                        int node = u;
                        while (node != -1)
                        {
                            nodeToReachU.Add(node);
                            node = parent[node];
                        }
                        node = v;
                        while (node != -1)
                        {
                            nodeToReachV.Add(node);
                            node = parent[node];
                        }
                        foreach (int parentOfU in nodeToReachU)
                            if (nodeToReachV.Contains(parentOfU))
                            {
                                node = u;
                                while (node != parentOfU)
                                {
                                    cycle.Add(node);
                                    node = parent[node];
                                }
                                node = v;
                                while (node != parentOfU)
                                {
                                    cycle.Add(node);
                                    node = parent[node];
                                }
                                cycle.Add(parentOfU);
                            }
                        return true;
                    }
            }
            return false;
        }

        // Time O(N^2) || Space O(N), N = no of nodes in Graph
        public static int[] FindRedundantConnection(int[][] edges)
        {
            int V = edges.Length;
            // Since Nodes are 1 indexed
            List<int>[] graph = new List<int>[V + 1];
            HashSet<int> visited = new HashSet<int>();

            for (int i = 0; i < V; i++)
            {
                int u = edges[i][0];
                int v = edges[i][1];

                if (graph[u] == null) graph[u] = new List<int>();
                if (graph[v] == null) graph[v] = new List<int>();

                visited.Clear();
                // Vertex U and Vertex V both have atleast 1 connection,
                // check before adding this edge if there is connection b/w U & V?
                // if so than adding this edge will result in cycle
                if (graph[u].Count > 0 && graph[v].Count > 0 && DFS(u, v))
                    return edges[i];

                graph[u].Add(v);
                graph[v].Add(u);
            }

            throw new ArgumentException("Adding none of the provided Edge resulted in cycle in Graph");

            // local DFS function
            bool DFS(int source, int target)
            {
                if (!visited.Contains(source))
                {
                    if (source == target) return true;

                    visited.Add(source);
                    foreach (int adjacentNode in graph[source])
                        if (DFS(adjacentNode, target))
                            return true;
                }
                return false;
            }
        }

        // Time = Space = O(N), N = no of nodes in Graph
        public static int[] FindRedundantConnection_Efficient(int[][] edges)
        {
            /* ALGO
            a. Build the graph
            b. find the cycle using DFS keep track of parent of each vertex
            c. once we find the cycle add all the edge of each node in the cycle to a HasSet
            d. now start from the end of the edges array and first edge what is present in HashSet would be our ans
            */
            int V = edges.Length;
            List<int>[] g = new List<int>[V + 1];
            foreach (var edge in edges)
            {
                int u = edge[0], v = edge[1];
                if (g[u] == null) g[u] = new List<int>();
                if (g[v] == null) g[v] = new List<int>();

                // add edge u->v
                g[u].Add(v);
                // add edge v->u
                g[v].Add(u);
            }

            int[] parent = new int[V + 1];
            bool[] visited = new bool[V + 1];
            HashSet<string> cyclicEdges = new HashSet<string>();
            FindCycle();

            // find redundant edge
            for (int i = V - 1; i >= 0; i--)
                if (cyclicEdges.Contains(edges[i][0] + "," + edges[i][1]))
                    return edges[i];
            return new int[0];

            // local helper func
            bool FindCycle(int cur = 1, int last = 0)
            {
                // cycle found
                if (visited[cur] == true)
                    // cycle found
                    if (parent[last] != cur)
                    {
                        int par = cur;
                        while (true)
                        {
                            cyclicEdges.Add(cur + "," + last);   // u->v
                            cyclicEdges.Add(last + "," + cur);   // u->v
                            cur = last;
                            last = parent[last];
                            if (par == cur) break;
                        }
                        return true;
                    }
                    else return false;

                // mark visited
                visited[cur] = true;
                // update parent for current node
                parent[cur] = last;

                foreach (var adjacentEdge in g[cur])
                    if (FindCycle(adjacentEdge, cur))
                        return true;

                // mark un-visited
                visited[cur] = false;
                return false;
            }
        }


        // Time O(n) || Space O(1)
        // After each increase or decrease operation CPU doesn't monitors utilization for 10 seconds
        public static int CPU_OptimizationProblem(int instances, List<int> averageUtil)
        {
            int i = 0;
            long upperLimit = long.MaxValue;// (long)(2 * Math.Pow(10, 8));

            while (i < averageUtil.Count)
            {
                if (averageUtil[i] < 25)
                {
                    if (instances > 1)
                    {
                        instances = (int)Math.Ceiling(instances / 2.0);
                        i += 9;
                    }
                }
                else if (averageUtil[i] > 60)
                {
                    if (upperLimit >= 2 * (long)instances)
                    {
                        instances *= 2;
                        i += 9;
                    }
                }
                i++;
            }
            return instances;
        }


        // Time O(Max(E,V^3)) || Space O(V), V = no of productsNodes & E = no of edges n.e. Total no of Connections b/w 2 products
        public static int GetMinTrioSum(int productsNodes, List<int> productsFrom, List<int> productsTo)
        {
            int ans = int.MaxValue;
            HashSet<int>[] graph = new HashSet<int>[productsNodes + 1];
            // Initialize each Vertex
            for (int i = 0; i < productsNodes; i++)
                graph[i + 1] = new HashSet<int>();  // Note: Products are 1 indexed

            // Create the mapping n.e Graph as per given relations
            for (int i = 0; i < productsFrom.Count; i++)
            {
                int u = productsFrom[i];
                int v = productsTo[i];

                graph[u].Add(v);
                graph[v].Add(u);
            }

            for (int i = 1; i <= productsNodes; i++)
                for (int j = 1; j <= productsNodes; j++)
                    for (int k = 1; k <= productsNodes; k++)
                        // given n,j,k form an Trio n.e. Triangle than compute their Outer Product Sum
                        if (graph[i].Contains(j) && graph[i].Contains(k) &&
                            graph[j].Contains(i) && graph[j].Contains(k) &&
                            graph[k].Contains(j) && graph[k].Contains(i))
                            ans = Math.Min(ans, graph[i].Count + graph[j].Count + graph[k].Count - 6);

            return ans == int.MaxValue ? -1 : ans;
        }



        // Time (2^N) reduced to O(n) using 'DP/Cache' || Space O(n) // DP Top-Down Approach
        public static int NumDecodings(string s, Dictionary<int, int> cache, int i = 0)
        {
            // reached end of the entire encoded string 's' return 1
            if (i == s.Length) return 1;
            // already precomputed ans for given index
            if (cache.ContainsKey(i))
                return cache[i];

            int ways = 0;
            if (i < s.Length && s[i] != '0')
            {
                // if we use just single digit now
                ways += NumDecodings(s, cache, i + 1);
                // if we use double digits now
                if (i + 1 < s.Length && ((s[i] - '0') * 10 + s[i + 1] - '0') < 27)
                    ways += NumDecodings(s, cache, i + 2);
            }
            // add current index ans to cache so we can utilize it later
            cache.Add(i, ways);
            return ways;
        }


        // Time O(SqRoot of target) || Space O(1)
        public static int ReachANumber(int target)
        {
            /* The crux of the problem is to put + and - signs on the numbers 1, 2, 3, ..., k so that the sum is target.
             * 
             * When target < 0 and we made a sum of target, we could switch the signs of all the numbers so that it equals Math.abs(target). Thus, the answer for target is the same as Math.abs(target), and so without loss of generality, we can consider only target > 0.
             * Now let's say k is the smallest number with S = 1 + 2 + ... + k >= target. If S == target, the answer is clearly k.
             * 
             * If S > target, we need to change some number signs. If delta = S - target is even, then we can always find a subset of {1, 2, ..., k} equal to delta / 2 and switch the signs, so the answer is k. (This depends on T = delta / 2 being at most S.) [The proof is simple: either T <= k and we choose it, or we choose k in our subset and try to solve the same instance of the problem for T -= k and the set {1, 2, ..., k-1}.]
             * Otherwise, if delta is odd, we can't do it, as every sign change from positive to negative changes the sum by an even number. So let's consider a candidate answer of k+1, which changes delta by k+1. If this is odd, then delta will be even and we can have an answer of k+1. Otherwise, delta will be odd, and we will have an answer of k+2.
             * 
             * For concrete examples of the above four cases, consider the following:
             * 
             * If target = 3, then k = 2, delta = 0 and the answer is k = 2.
             * If target = 4, then k = 3, delta = 2, delta is even and the answer is k = 3.
             * If target = 7, then k = 4, delta = 3, delta is odd and adding k+1 makes delta even. The answer is k+1 = 5.
             * If target = 5, then k = 3, delta = 1, delta is odd and adding k+1 keeps delta odd. The answer is k+2 = 5.
             */
            target = Math.Abs(target);
            int k = 0;
            while (target > 0)
                target -= ++k;
            // if target is Zero or delta/difference is even than K is our ans
            // else we need to add next (k+1)th Num and see if that makes it Zero if not than we also have to add (k+2)th num as well
            return target % 2 == 0 ? k : k + 1 + k % 2;
        }


        // Time O(n) || Auxiliary Space O(9)=O(1) || Recursive Space O(h), n = no of nodes in tree & h = height of tree
        public static void PseudoPalindromicPaths(TreeNode root, HashSet<int> digitEncounteredSoFar, ref int pseudoPalindromicCount)
        {
            // if current root digit is seen before than remove it 1+1 is even hence palindrome is possible
            if (digitEncounteredSoFar.Contains(root.val)) digitEncounteredSoFar.Remove(root.val);
            // if current root digit is never seen before than add it 
            else digitEncounteredSoFar.Add(root.val);

            // (palindrome is possible if all digits in path from root to leaf have even count and at max 1 digit seen having odd count)
            if (root.left == null && root.right == null)
                pseudoPalindromicCount += (digitEncounteredSoFar.Count < 2) ? 1 : 0;
            else
            {
                if (root.left != null) PseudoPalindromicPaths(root.left, digitEncounteredSoFar, ref pseudoPalindromicCount);
                if (root.right != null) PseudoPalindromicPaths(root.right, digitEncounteredSoFar, ref pseudoPalindromicCount);
            }

            // Undo the operation done before above recursive call
            if (digitEncounteredSoFar.Contains(root.val)) digitEncounteredSoFar.Remove(root.val);
            else digitEncounteredSoFar.Add(root.val);
        }
        public static int PseudoPalindromicPaths(TreeNode root, HashSet<int> digitEncounteredSoFar)
        {
            if (root == null) return 0;

            // if current root digit is seen before than remove it 1+1 is even hence palindrome is possible
            if (digitEncounteredSoFar.Contains(root.val)) digitEncounteredSoFar.Remove(root.val);
            // if current root digit is never seen before than add it 
            else digitEncounteredSoFar.Add(root.val);

            int pseudoPalindromicCount = PseudoPalindromicPaths(root.left, digitEncounteredSoFar);
            pseudoPalindromicCount += PseudoPalindromicPaths(root.right, digitEncounteredSoFar);
            // (palindrome is possible if all digits in path from root to leaf have even count and at max 1 digit seen having odd count)
            if (root.left == null && root.right == null)
                pseudoPalindromicCount += (digitEncounteredSoFar.Count < 2) ? 1 : 0;

            // Undo the operation done before above recursive call
            if (digitEncounteredSoFar.Contains(root.val)) digitEncounteredSoFar.Remove(root.val);
            else digitEncounteredSoFar.Add(root.val);

            return pseudoPalindromicCount;
        }



        // Time = Recursive Space = O(MxN) || Auxillary Space O(1)
        public static void GameOfLife(int[][] board)
        {
            int row = board.Length;
            int col = board[0].Length;
            // Call Driver Func
            BackTrack(0, 0);

            // Helper Functions

            void BackTrack(int r, int c)
            {
                int liveNeighbour = CountLive(r, c);
                if (c < col - 1)
                    BackTrack(r, c + 1);
                else if (r < row - 1)
                    BackTrack(r + 1, 0);
                // While BackTracking apply rule based upon live count calculated before making recursive call
                ApplyRule(r, c, liveNeighbour);
            }

            void ApplyRule(int rID, int cID, int liveCellCount)
            {
                if (board[rID][cID] == 1)
                {
                    // Rule 1 & Rule 3 (LIVE CELL DIES IF DOESNT HAVE EXCATLY 3 LIVE NEIGHBOURS)
                    if (liveCellCount < 2 || liveCellCount > 3) board[rID][cID] = 0;
                }
                // Rule 4 (DEAD CELL WITH EXCATLY 3 LIVE NEIGHBOURS BECOMES LIVE)
                else if (liveCellCount == 3) board[rID][cID] = 1;
            }

            int CountLive(int rID, int cID)
            {
                int live = 0;
                for (int r = rID - 1; r <= rID + 1; r++)
                    for (int c = cID - 1; c <= cID + 1; c++)
                        if (IsValid(r, c) && board[r][c] == 1)
                            live++;
                return live - (board[rID][cID] == 1 ? 1 : 0);
            }
            bool IsValid(int rID, int cID) => rID < 0 || rID >= row || cID < 0 || cID >= col ? false : true;
        }


        /// <summary>
        /// Given a singly linked list L: L0→L1→…→Ln-1→Ln,
        /// re-order it to:               L0→Ln→L1→Ln-1→L2→Ln-2→…
        /// Time O(n) || Space O(n)
        /// </summary>
        /// <param name="A"></param>
        public static void ReOrderList_UsingStack(ListNode A)
        {
            if (A == null || A.next == null) return;

            Stack<ListNode> st = new Stack<ListNode>();
            ListNode mid = Mid(A);
            ListNode curr = mid.next;
            mid.next = null;    // Mark the lastNode->next as null

            // Push all nodes after mid point into Stack
            while (curr != null)
            {
                st.Push(curr);
                curr = curr.next;
            }

            ListNode nxt = A;
            // Start Popping Nodes from Stack and appending in b/w nodes from Start
            while (st.Count > 0)
            {
                st.Peek().next = nxt.next;
                nxt.next = st.Pop();
                nxt = nxt.next.next;    // we move twice since last node is appended in b/w now
            }

            // local func which returns Mid of the LinkedList
            ListNode Mid(ListNode root)
            {
                ListNode slow = root, fast = root.next;
                while (fast != null && fast.next != null)
                {
                    slow = slow.next;
                    fast = fast.next.next;
                }
                return slow;
            }
        }
        // Time = Space O(n), n = no of nodes in LinkedList
        public static void ReOrderList_UsingList(ListNode head)
        {
            if (head?.next == null) return;    // base case nothing to reorder

            ListNode cur = head, fast = head, prv = null, curnext;
            List<ListNode> ls = [];
            // add all the nodes from first half to a list
            while (fast?.next != null)
            {
                ls.Add(cur);
                cur = cur.next;
                fast = fast.next.next;
            }

            // reverse the 2nd half
            fast = cur;
            while (fast != null)
            {
                cur = fast;
                fast = fast.next;
                cur.next = prv;
                prv = cur;
            }

            // merge both the list as per given condition
            head = ls[0];
            for (int i = 0; i < ls.Count; i++)
            {
                ls[i].next = cur;   // appending from reverse of 2nd half
                curnext = cur?.next;
                if (cur != null && i + 1 < ls.Count) cur.next = ls[i + 1];
                cur = curnext;
            }
        }
        /// <summary>
        /// Given a singly linked list L: L0→L1→…→Ln-1→Ln,
        /// re-order it to:               L0→Ln→L1→Ln-1→L2→Ln-2→…
        /// Time O(n) Space O(1), n = no of nodes in LinkedList
        /// </summary>
        /// <param name="head"></param>
        public static void ReorderList_Iterative(ListNode head)
        {
            ListNode slow = head, fast = head, prv = null;
            // get Mid Node
            while (fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            // Reverse the 2nd half
            fast = slow;
            while (fast != null)
            {
                slow = fast;
                fast = fast.next;
                slow.next = prv;
                prv = slow;
            }


            ListNode firstHalf = head, secondHalf = slow, temp;
            // Reorder
            while (secondHalf.next != null)
            {
                temp = firstHalf.next;
                firstHalf.next = secondHalf;
                secondHalf = secondHalf.next;
                firstHalf.next.next = temp;
                firstHalf = temp;
            }
        }


        /// <summary>
        /// Given a sorted linked list, delete all duplicates such that each element appear only once.
        /// Time O(n) || Space O(1)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DeleteDuplicatesI(ListNode head)
        {
            ListNode curr = head;
            while (curr != null && curr.next != null)
                if (curr.val == curr.next.val)
                    curr.next = curr.next.next;
                else
                    curr = curr.next;
            return head;
        }

        /// <summary>
        /// Given a sorted linked list, delete all nodes that have duplicate numbers, leaving only distinct numbers from the original list.
        /// Time O(n) || Space O(1)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode DeleteDuplicatesII(ListNode head)
        {
            ListNode dummy = new ListNode(0) { next = head };
            ListNode curr = dummy;
            while (curr.next != null && curr.next.next != null)
                if (curr.next.val == curr.next.next.val)
                {
                    int val = curr.next.val;
                    while (curr.next != null && curr.next.val == val)
                        curr.next = curr.next.next;
                }
                else
                    curr = curr.next;
            return dummy.next;
        }


        // Time O(Min(n^2,m)) || Space O(h), n = no of nodes in List & m = no of nodes in tree & h = hieght of tree
        public static bool IsSubPath(ListNode head, TreeNode root)
        {
            if (Match(head, root)) return true;
            if (root == null) return false;
            return IsSubPath(head, root.left) || IsSubPath(head, root.right);
        }
        // Time O(n), n = no of nodes in List
        public static bool Match(ListNode head, TreeNode root)
        {
            if (head == null) return true;
            if (root == null || root.val != head.val) return false;
            return Match(head.next, root.left) || Match(head.next, root.right);
        }


        // Time O(Min(n,b)+m), b = appendTill & n,m = length of list1 & list2 respectively || Space O(1)
        public static ListNode MergeInBetween(ListNode list1, int a, int b, ListNode list2)
        {
            int counter = 1;
            ListNode curr = list1, appendFrom = null, list2Head = list2;
            while (true)
            {
                if (counter == a)
                    appendFrom = curr;
                if (counter == b)
                {
                    while (list2.next != null)
                        list2 = list2.next;
                    list2.next = curr.next.next;        // apend 'b.next' to list2 lastNode.next
                    appendFrom.next = list2Head;        // apend list2->Head at 'a'
                    break;
                }
                counter++;
                curr = curr.next;
            }
            return list1;
        }


        // Time O(n) || Auxillary Space O(1) Recursive Soln
        public static TreeNode GetTargetCopy(TreeNode original, TreeNode cloned, TreeNode target)
        {
            /* Since original and Clone are same structurally,
             * if we move left in original we move left in Clone tree also, and same if we move right
             * 
             * If target matches any node in original tree return equivalent reference from cloned 
             * If target found in left return it it must be present in right hence return result of right recursive call
             */
            if (original == null) return original;
            if (target == original) return cloned;  // target found in original return equivalent ref from cloned

            // Search for Target in left-subtree
            TreeNode left = GetTargetCopy(original.left, cloned.left, target);
            if (left != null) return left;  // found target return Clone Node Reference

            // else Target must be present in right-subtree of Original
            return GetTargetCopy(original.right, cloned.right, target);
        }


        // Time = Space = O(n)
        public static ListNode PartitionList_UsingStacks(ListNode head, int x)
        {
            Stack<ListNode> smaller = new Stack<ListNode>(), equalGreater = new Stack<ListNode>();
            while (head != null)
            {
                if (head.val < x)
                    smaller.Push(head);
                else
                    equalGreater.Push(head);
                head = head.next;
            }
            ListNode curr;
            while (equalGreater.Count > 0)
            {
                curr = equalGreater.Pop();
                curr.next = head;
                head = curr;
            }
            while (smaller.Count > 0)
            {
                curr = smaller.Pop();
                curr.next = head;
                head = curr;
            }
            return head;
        }
        // Time O(n) || Space O(1), n = no of nodes in List
        public static ListNode PartitionList(ListNode head, int x)
        {
            ListNode dummyNode = new ListNode(0) { next = head };
            ListNode paritionHead = null, paritionCurr = null;
            while (head.next != null)
                if (head.next.val >= x)
                {
                    if (paritionCurr != null)
                    {
                        paritionCurr.next = head.next;      // add greater/equal to 'x' node in partition list
                        paritionCurr = paritionCurr.next;   // move ahead in partition list
                    }
                    else paritionHead = paritionCurr = head.next;
                    head.next = head.next.next;             // update original List
                }
                else
                    head = head.next;   // move to next node in Original List

            head.next = paritionHead;
            if (paritionCurr != null) paritionCurr.next = null;
            return dummyNode.next;
        }
        // Time O(n) || Space O(1), n = no of nodes in List
        public static ListNode PartitionList_Faster(ListNode head, int x)
        {
            ListNode smallerHead = new ListNode(0), largerHead = new ListNode(0);
            ListNode before = smallerHead, after = largerHead;
            while (head != null)
            {
                if (head.val < x)           // add all smaller nodes to smaller list
                {
                    before.next = head;
                    before = before.next;
                }
                else                        // & all larger nodes to larger list
                {
                    after.next = head;
                    after = after.next;
                }
                head = head.next;
            }
            before.next = largerHead.next;  // join smaller & larger list
            after.next = null;              // mark last node as null
            return smallerHead.next;
        }


        // Time O(k) || Space O(n), K = no of valid permutation
        public static int BeautifulArrangement(int n) => Try(new bool[n + 1], n);
        public static int Try(bool[] noUsed, int total, int curr = 1, int count = 0)
        {
            if (count == total)
                return 1;
            int permutation = 0;
            for (int i = 1; i <= total; i++)
                if (!noUsed[i] && IsDivisible(i, curr))
                {
                    noUsed[i] = true;
                    permutation += Try(noUsed, total, curr + 1, count + 1);
                    noUsed[i] = false;
                }
            return permutation;
        }
        public static bool IsDivisible(int a, int b) => a % b == 0 || b % a == 0;


        // Time O(n) || Space O(n)
        public static string MakeTheStringGreat(string s)
        {
            Stack<char> lastChar = new Stack<char>();
            int i = 0;
            for (i = 0; i < s.Length; i++)
                if (lastChar.Count > 0 && (lastChar.Peek() == (s[i] - 'a') + 'A' || lastChar.Peek() == (s[i] - 'A') + 'a'))
                    lastChar.Pop();
                else
                    lastChar.Push(s[i]);

            i = lastChar.Count;
            char[] ch = new char[i--];
            while (i >= 0)
                ch[i--] = lastChar.Pop();

            return new string(ch);
        }


        // Time O(n) || Space O(n) as we need to create ans string
        public static string RemoveOutermostParentheses(string S)
        {
            int start = 0, curr = 0, openingCount = 0;
            StringBuilder ans = new StringBuilder();
            while (curr < S.Length)
            {
                openingCount = S[curr] == '(' ? openingCount + 1 : openingCount - 1;

                if (openingCount == 0)
                {
                    if (start + 1 != curr)
                        ans.Append(S.Substring(start + 1, (curr - start) - 1));
                    start = curr + 1;
                }
                curr++;
            }
            return ans.ToString();
        }


        /// <summary>
        /// Returns a array such that, for each day in the input, tells you how many days you would have to wait until a warmer temperature.
        /// If there is no future day for which this is possible, put 0 instead
        /// Time = Space = O(n), n = length of array T
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        public static int[] DailyTemperatures(int[] temperatures)
        {
            int l = temperatures.Length;
            int[] warmer = new int[l];
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < l; i++)        // O(n)
            {
                // if stack is empty means or no lower value exists than skip
                while (st.Count > 0 && temperatures[st.Peek()] < temperatures[i])
                    warmer[st.Peek()] = i - st.Pop();   // update index diff in ans array
                st.Push(i); // push current idx
            }
            return warmer;

            #region OLD implementation same approach
            //int[] ans = new int[T.Length];
            //Stack<int> nextHigher = new Stack<int>(T.Length);
            //for (int n = T.Length - 1; n >= 0; n--)
            //{
            //    while (nextHigher.Count > 0 && T[nextHigher.Peek()] <= T[n])
            //        nextHigher.Pop();
            //    // if stack is empty means no higher value exists else take the Stack Top
            //    ans[n] = nextHigher.Count > 0 ? nextHigher.Peek() - n : 0;
            //    nextHigher.Push(n);      // add current val to Stack
            //}
            //return ans;
            #endregion
        }


        /// <summary>
        /// Given a string, sorts it in decreasing order based on the frequency of characters.
        /// Time O(Max(n,klogk)), Space O(k), n = length of the input string & k = Distinct Characters in I/P
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FrequencySort(string s)
        {
            // Map All Characters and their total count in Dictionary
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)                      // Time O(n)
                if (charCount.ContainsKey(s[i])) charCount[s[i]]++;
                else charCount.Add(s[i], 1);

            // Now create a MaxHeap which will store all Characters by their frequency
            CharFreqHeap h = new CharFreqHeap(charCount.Count);
            foreach (var kvp in charCount)                         // O(k), k = Distinct Characters in I/P
                h.Insert(kvp.Value, kvp.Key);                      // O(logk)

            // Now Create answer by appending each char its frequency times, starting with char with max frequency in input string
            StringBuilder sb = new StringBuilder(s.Length);
            while (h.size > 0)                                      // O(k)
            {
                CharPair max = h.ExtractMin();                      // O(logk)
                for (int times = max.count; times > 0; times--)
                    sb.Append(max.ch);
            }
            return sb.ToString();
        }
        class CharFreqHeap
        {
            CharPair[] arr;
            public int i, size;
            public CharFreqHeap(int size)
            {
                i = 0;
                this.size = size;
                arr = new CharPair[size];
            }
            public void Insert(int key, char val)
            {
                int curr = i++;
                arr[curr] = new CharPair(key, val);
                while (curr > 0)
                {
                    int p = Parent(curr);
                    if (arr[p].count < arr[curr].count)
                    {
                        var temp = arr[p];
                        arr[p] = arr[curr];
                        arr[curr] = temp;
                        curr = p;
                    }
                    else break;
                }
            }
            public CharPair ExtractMin()
            {
                CharPair ans = arr[0];
                arr[0] = arr[--size];
                Heapify();
                return ans;
            }
            public void Heapify(int i = 0)
            {
                while (i < size)
                {
                    int max = i, left = Left(i), right = Right(i);
                    if (left < size && arr[left].count > arr[i].count)
                        max = left;
                    if (right < size && arr[right].count > arr[max].count)
                        max = right;
                    if (max != i)
                    {
                        var temp = arr[max];
                        arr[max] = arr[i];
                        arr[i] = temp;
                        i = max;
                    }
                    else break;
                }
            }

            int Left(int index) => index * 2 + 1;
            int Right(int index) => Left(index) + 1;
            int Parent(int index) => (index - 1) / 2;
        }
        class CharPair
        {
            public char ch;
            public int count;
            public CharPair(int count, char ch)
            {
                this.ch = ch;
                this.count = count;
            }
        }


        /// <summary>
        /// Given an array of integers nums, sort the array in increasing order based on the frequency of the values.
        /// If multiple values have the same frequency, sort them in decreasing order.
        /// Time O(Max(n,kLogk)) || Space O(k), n = length of nums & k = no of distinct intergers
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int[] SortArrayByIncreasingFrequency(int[] nums)
        {
            int i = 0;
            // Count the frequency of each distinct num in array nums
            Dictionary<int, int> valFreq = new Dictionary<int, int>();
            for (i = 0; i < nums.Length; i++)                   // Time O(n)
                if (valFreq.ContainsKey(nums[i])) valFreq[nums[i]]++;
                else valFreq.Add(nums[i], 1);

            i = 0;
            // Store number and its frequency in array
            ArrayPair[] intFreq = new ArrayPair[valFreq.Count];
            foreach (var kvp in valFreq)                        // Time O(k)
                intFreq[i++] = new ArrayPair(kvp.Key, kvp.Value);

            // Sort in increasing order based on the frequency of the values. If multiple values have the same frequency, sort them in decreasing order.
            Array.Sort(intFreq, new FrequencyValue());          // Time O(kLogK)

            // update the result array with integer sorted as per probl desc
            int[] ans = new int[nums.Length];
            int currI = 0;
            for (i = 0; i < intFreq.Length; i++)                // Time O(n)
                for (int j = intFreq[i].count; j > 0; j--)
                    ans[currI++] = intFreq[i].val;
            return ans;
        }
        class ArrayPair
        {
            public int val;
            public int count;
            public ArrayPair(int val, int count)
            {
                this.val = val;
                this.count = count;
            }
        }
        class FrequencyValue : IComparer<ArrayPair>
        {
            public int Compare(ArrayPair a, ArrayPair b)
            {
                if (a.count != b.count)
                    return a.count.CompareTo(b.count);  // increasing count/frequency
                else
                    return b.val.CompareTo(a.val);      // if frequency r same than decreasing value
            }
        }


        /// <summary>
        /// Given a 2D array of characters grid of size m x n, return true if there exists any cycle consisting of the same value in grid
        /// Directions (up, down, left, or right)
        /// Time = Space = O(row*col), as each cells is traversed once
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool DetectCyclesIn2DGrid(char[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            bool[,] visited = new bool[rows, cols];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (!visited[r, c] && DFS(r, c, grid[r][c]))
                        return true;
            return false;
            // direction 0 = coming from left, 1 = coming from right, 2 = coming from top & 3 = coming from bottom
            bool DFS(int row, int col, char lastChar, int direction = 0)
            {
                if (row < 0 || row >= rows || col < 0 || col >= cols || grid[row][col] != lastChar) return false;
                if (grid[row][col] == lastChar && visited[row, col]) return true;       // cycle found

                visited[row, col] = true; // mark visited to avoid traversing same cell during different DFS traversal
                                          // check left
                if (direction != 0 && DFS(row, col - 1, lastChar, 1)) return true;
                // check right
                if (direction != 1 && DFS(row, col + 1, lastChar, 0)) return true;
                // check top
                if (direction != 2 && DFS(row - 1, col, lastChar, 3)) return true;
                // check bottom
                if (direction != 3 && DFS(row + 1, col, lastChar, 2)) return true;

                return false;
            }
        }


        /// <summary>
        /// Time O(n) || Auxiliary Space O(1) || Recusive Space O(h), n = no of nodes in tree, h = height of tree
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string SmallestStringStartingFromLeaf(TreeNode root)
        {
            string ans = "";
            GetStrings(root, "", ref ans);       // Time O(n)
            return ans;
        }
        static void GetStrings(TreeNode root, string str, ref string ans)
        {
            if (root == null) { return; }
            string pathTillHere = (char)(root.val + 'a') + str;

            if (root.left == null && root.right == null && (pathTillHere.CompareTo(ans) < 0 || ans == ""))
                ans = pathTillHere;
            GetStrings(root.left, pathTillHere, ref ans);
            GetStrings(root.right, pathTillHere, ref ans);
        }
        // Time O(n + (l*h)), Space O(h), n = no of nodes in the BinaryTree, l = no of leaf nodes, h = height of the tree
        public static string SmallestStringStartingFromLeafUsingStack(TreeNode root)
        {
            /*
            Time => O(n + (l*h)),
            n = no of nodes in the BinaryTree needed for complete traversal
            +
            (l = no of leaf nodes where the comparison actually happens
            *
            h = height of the tree to convert stack to String before every comparison)

            Note:
            1. Also the worst time for the algo will happen when we encounter a Fully-Balanced Tree,
            2. because in case of skewed tree there would be just 1 comparison hence Time gets Reduced to n + h,
            3. which can be written as n + n as height of skewed tree is equal to no of nodes which ultimately results in => O(n) time.

            Space => O(h), h = height of the tree (worst case == n, skewed tree)
             */
            Stack<char> cur = [];
            string smallest = "";
            FindSmallest(root);
            return smallest;

            // local helper func
            void FindSmallest(TreeNode r)
            {
                // add cur node character
                cur.Push((char)(r.val + 'a'));

                if (r.left == null && r.right == null)
                    smallest = MinString(smallest, new string(cur.ToArray()));
                else
                {
                    if (r.left != null) FindSmallest(r.left);
                    if (r.right != null) FindSmallest(r.right);
                }

                // remove cur character before exiting
                cur.Pop();
            }

            string MinString(string s1, string s2) => s1 == "" ? s2 : s1.CompareTo(s2) < 0 ? s1 : s2;
        }


        /// <summary>
        /// Given a binary tree containing digits from 0-9 only, each root-to-leaf path could represent a number.
        /// An example is the root-to-leaf path 1->2->3 which represents the number 123.
        /// Return the total sum of all root-to-leaf numbers.
        /// Time O(n) || Auxiliary Space O(1) || Recusive Space O(h), n = no of nodes in tree, h = height of tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static int SumRootToLeafNumbers(TreeNode root, int no = 0)
        {
            if (root == null) return 0;
            no = no * 10 + root.val;
            if (root.left == null && root.right == null)
                return no;
            else
                return SumRootToLeafNumbers(root.left, no) + SumRootToLeafNumbers(root.right, no);
        }


        // Time O(Max(n,m)) || Space O(n+m), n = no of characters in word1 & m no of characters in word2
        public static bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var str in word1)
                sb1.Append(str);
            foreach (var str in word2)
                sb2.Append(str);
            return sb1.ToString().CompareTo(sb2.ToString()) == 0;
        }
        // Time O(n) || Space O(n), n = no of characters in word1
        public static bool ArrayStringsAreEqualFaster(string[] word1, string[] word2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < word1.Length; i++)
                sb.Append(word1[i]);

            string finalString = sb.ToString();
            int currI = 0, len = finalString.Length;

            for (int i = 0; i < word2.Length; i++)
                for (int j = 0; j < word2[i].Length; j++)
                    if (currI >= len || finalString[currI++] != word2[i][j])
                        return false;
            return currI == len;
        }


        // Time O(n*k) || Space O(k) , n = no of nodes in 's' tree and k = no of nodes in 's' whose value match with 't'
        public static bool SubtreeOfAnotherTree(TreeNode s, TreeNode t)
        {
            /* Approach was to get all nodes who's root val matches with 't' while traversing
             * Than starting Matching from all eligible nodes
             */
            List<TreeNode> ls = new List<TreeNode>();
            GetTMatchingNode(s, t, ls);
            for (int i = 0; i < ls.Count; i++)      // 'k' times
                if (Match(ls[i], t)) return true;   // O(n)
            return false;
        }
        // Time O(n), n = no of node in Tree 's'
        public static void GetTMatchingNode(TreeNode s, TreeNode t, List<TreeNode> ls)
        {
            if (s != null)
            {
                if (s.val == t.val) ls.Add(s);
                GetTMatchingNode(s.left, t, ls);
                GetTMatchingNode(s.right, t, ls);
            }
        }
        // Time O(n), n = no of node in subTreeTree 'a'
        public static bool Match(TreeNode a, TreeNode b)
        {
            if (a == null && b == null) return true;
            if ((a != null && b == null) || (b != null && a == null)) return false;
            return a.val == b.val && Match(a.left, b.left) && Match(a.right, b.right);
        }

        // Time O(n*k) || Space O(1) , n = no of nodes in 's' tree and k = no of nodes in 's' whose value match with 't'
        public static bool SubtreeOfAnotherTreeEfficient(TreeNode s, TreeNode t)
        {
            /* Efficient Approach is to look for SubTree 't' while traversing original tree 's'
             * if any node matches return true
             */
            if (s == null) return false;
            if (Match(s, t)) return true;
            return SubtreeOfAnotherTreeEfficient(s.left, t) || SubtreeOfAnotherTreeEfficient(s.right, t);
        }


        // Brute Force // Time O(n^2), n = length of nums
        public static int KDiffPairsInAnArray(int[] nums, int k)
        {
            Array.Sort(nums);
            int pair = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i - 1] == nums[i]) continue;  // Skipping same left nums as used before
                for (int j = i + 1; j < nums.Length; j++)
                    if (Math.Abs(nums[i] - nums[j]) == k && (j == i + 1 || nums[j - 1] != nums[j]))     // Skip right nums if same as before to capture unique pairs
                        pair++;
            }
            return pair;
        }
        // 2-Pointer approach // Time O(nlogn), n = length of nums
        public static int KDiffPairsInAnArrayFaster(int[] nums, int k)
        {
            Array.Sort(nums);
            int pair = 0, left = 0, right = left + 1;
            while (left < nums.Length && right < nums.Length)
            {
                int diff = Math.Abs(nums[left] - nums[right]);

                if (diff < k)
                    right++;
                else if (diff > k)
                    left++;
                else // if (diff == k)
                {
                    pair++;
                    while (left + 1 < nums.Length && nums[left] == nums[left + 1])
                        left++;
                    // Skipping same left nums as used before
                    left++;
                    // Skip right nums if same as before to capture unique pairs
                    while (right + 1 < nums.Length && nums[right] == nums[right + 1])
                        right++;
                    right++;
                }
                // in case left & right are pointing at same index
                while (left >= right) right++;
            }
            return pair;
        }


        // Time O(n) || Space O(1)
        public static int StringCompression(char[] chars)
        {
            int index = 0, count = 1;
            char lastChar = chars[0];
            for (int i = 1; i < chars.Length; i++)
            {
                if (chars[i] == lastChar)
                    count++;
                else // if(chars[n]!=lastChar)
                {
                    if (count > 1)
                    {
                        string freq = count.ToString();
                        for (int k = 0; k < freq.Length; k++)
                            chars[++index] = freq[k];
                    }
                    lastChar = chars[i];
                    chars[++index] = lastChar;
                    count = 1;
                }
            }
            if (count > 1)
            {
                string freq = count.ToString();
                for (int k = 0; k < freq.Length; k++)
                    chars[++index] = freq[k];
            }
            return index + 1;
        }


        // Time O(n) || Recursive Space O(h)
        public static IList<IList<int>> PathSumII(TreeNode root, int sum)
        {
            List<IList<int>> ans = new List<IList<int>>();
            List<int> ls = new List<int>();
            GetSum(root, sum);
            return ans;
            void GetSum(TreeNode r, int s)
            {
                if (r == null) return;
                s -= r.val;
                ls.Add(r.val);                  // add current node to list

                if (r.left == null && r.right == null && s == 0)
                {
                    int[] result = new int[ls.Count];
                    ls.CopyTo(result);
                    ans.Add(result);
                }
                GetSum(r.left, s);
                GetSum(r.right, s);

                ls.RemoveAt(ls.Count - 1);    // Remove current node from list
            }
        }


        // Time O(n^2) || Auxillary Space O(1) || Recursive Space O(h), n = no of nodes in tree, h = height of tree
        public static int PathSumIII(TreeNode root, int sum)
        {
            if (root == null) return 0;
            return IsMatchingSum(root, sum) + PathSumIII(root.left, sum) + PathSumIII(root.right, sum);
        }
        public static int IsMatchingSum(TreeNode root, int sum)
        {
            if (root == null) return 0;
            sum -= root.val;
            return ((sum == 0) ? 1 : 0) + IsMatchingSum(root.left, sum) + IsMatchingSum(root.right, sum);
        }


        // Time O(nlogn) as we perform merge sort twice || Space O(n)
        public static int CreateSortedArrayThroughInstructions(int[] instructions)
        {
            long cost = 0;
            int modulo = 1000000007, n = instructions.Length;
            int[] smaller = new int[n];
            int[] larger = new int[n];
            int[][] temp = new int[n][];
            int[][] arrSmaller = new int[n][];
            int[][] arrLarger = new int[n][];

            for (int i = 0; i < n; i++)
            {
                arrSmaller[i] = new int[] { instructions[i], i };
                arrLarger[i] = new int[] { instructions[i], i };
            }

            SortSmaller(arrSmaller, 0, n - 1);
            SortLarger(arrLarger, 0, n - 1);

            // Find Min Cost of adding number
            for (int i = 0; i < n; i++)
                cost += Math.Min(smaller[i], larger[i]);

            return (int)(cost % modulo);


            // LOCAL FUNCTIONS

            // Total UnStable Sort
            void SortSmaller(int[][] a, int start, int last)
            {
                if (start == last) return;
                int mid = start + (last - start) / 2;
                SortSmaller(a, start, mid);
                SortSmaller(a, mid + 1, last);
                MergeSmaller(a, start, last, mid);
            }
            // Stable Sort
            void SortLarger(int[][] a, int start, int last)
            {
                if (start == last) return;
                int mid = start + (last - start) / 2;
                SortLarger(a, start, mid);
                SortLarger(a, mid + 1, last);
                MergeLarger(a, start, last, mid);
            }

            void MergeSmaller(int[][] a, int start, int last, int mid)
            {
                int i = start;
                int j = mid + 1;
                int k = start;
                while (i <= mid && j <= last)
                {
                    if (a[i][0] < a[j][0])
                        temp[k++] = a[i++];
                    else
                    {
                        smaller[a[j][1]] += i - start;
                        temp[k++] = a[j++];
                    }
                }
                while (i <= mid)
                    temp[k++] = a[i++];
                while (j <= last)
                {
                    smaller[a[j][1]] += i - start;
                    temp[k++] = a[j++];
                }
                // Restore array 'a'
                for (i = start; i <= last; i++)
                    a[i] = temp[i];
            }

            void MergeLarger(int[][] a, int start, int last, int mid)
            {
                int i = start;
                int j = mid + 1;
                int k = start;
                while (i <= mid && j <= last)
                {
                    if (a[i][0] <= a[j][0])
                        temp[k++] = a[i++];
                    else
                    {
                        larger[a[j][1]] += mid - i + 1;
                        temp[k++] = a[j++];
                    }
                }
                while (i <= mid)
                    temp[k++] = a[i++];
                while (j <= last)
                {
                    larger[a[j][1]] += mid - i + 1;
                    temp[k++] = a[j++];
                }
                // Restore array 'a'
                for (i = start; i <= last; i++)
                    a[i] = temp[i];
            }
        }


        // Time O(n) || Space O(n), n = 30 as stated in LeetCode Problem
        public static string CountAndSay(int n)
        {
            string startnum = "1";
            int count = 1;
            string[] preSolved = new string[31];
            StringBuilder next = new StringBuilder();
            preSolved[1] = startnum;

            for (int i = 2; i < preSolved.Length; i++)
            {
                next = new StringBuilder();
                count = 1;
                char lastDigit = startnum[0];
                for (int j = 1; j < startnum.Length; j++)
                    if (startnum[j] == lastDigit)
                        count++;
                    else
                    {
                        next.Append(count.ToString()).Append(lastDigit);
                        count = 1;
                        lastDigit = startnum[j];
                    }
                next.Append(count.ToString()).Append(lastDigit);
                preSolved[i] = next.ToString();
                startnum = preSolved[i];
            }
            return preSolved[n];
        }



        // Time O(nlogn) || Space O(1)
        public static int NumRescueBoats(int[] people, int limit)
        {
            Array.Sort(people);
            int boats = 0, start = 0, last = people.Length - 1;
            while (start <= last)
                if (people[start] + people[last] > limit)
                {
                    last--;
                    boats++;
                }
                else if (people[start] + people[last] <= limit)
                {
                    boats++;
                    last--;
                    start++;
                }

            return boats;
        }


        // Time O(n^2) || Space O(n)
        // Returns True If Cycle of more than 1 jump is formed by using both forwards and backwards movements
        public static bool CircularArrayLoop(int[] nums)
        {
            int n = nums.Length, jumps = 0;
            HashSet<int> visited;
            for (int i = 0; i < n; i++)
            {
                int currIndex = i, distCovered = 0;
                visited = new HashSet<int>();
                jumps = 0;
                while (!visited.Contains(currIndex))
                {
                    visited.Add(currIndex);
                    distCovered += nums[currIndex];
                    jumps++;
                    currIndex = (currIndex + nums[currIndex]) % n;
                    while (currIndex < 0) currIndex = n + currIndex;

                    if (distCovered != 0 && Math.Abs(distCovered) % n == 0 && jumps > 1) return true;
                }
            }
            return false;
        }
        // Time O(n^2) || Space O(n)
        // Returns True If Cycle of more than 1 jump is formed by using either forwards and backwards movements only from any index
        public static bool CircularArrayLoopSingleDirection(int[] nums)
        {
            int n = nums.Length;
            HashSet<int> visited;
            for (int i = 0; i < n; i++)
            {
                int currIndex = i, distCovered = 0, jumps = 0;
                bool isDirectionFrwd = nums[i] > 0 ? true : false;
                visited = new HashSet<int>();
                while (!visited.Contains(currIndex))
                {
                    visited.Add(currIndex);

                    if (nums[currIndex] > 0 && !isDirectionFrwd) break;
                    else if (nums[currIndex] < 0 && isDirectionFrwd) break;

                    distCovered += nums[currIndex];
                    jumps++;
                    currIndex = (currIndex + nums[currIndex]) % n;
                    while (currIndex < 0) currIndex = n + currIndex;

                    if (distCovered != 0 && Math.Abs(distCovered) % n == 0 && jumps > 1) return true;
                }
            }
            return false;
        }


        // Time = Space = O(100) ~O(1)
        public static int GetMaximumInGeneratedArray(int n)
        {
            if (n == 0) return 0;
            int max = 1;
            int[] nums = new int[n + 1];
            nums[0] = 0;
            nums[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                if (i % 2 == 0) nums[i] = nums[i / 2];
                else nums[i] = nums[(i + 1) / 2] + nums[i / 2];
                max = Math.Max(max, nums[i]);
            }
            return max;
        }


        // Time = Space = O(n), n = no of nodes in Tree
        public static int WidthOfBinaryTree(TreeNode root)
        {
            /* Think of Binary Tree as Heap,
             * left child is at index = dist * 2 + 1
             * right child is at index = dist * 2 + 2
             * 
             * While doing the LevelOrder Traversal keep adding Nodes to queue along with their distance
             * at end of each level update maxWidth n.e. if distance b/w 1st node in queue n last added node in queue + 1 is greater
             */
            int maxWidth = 1;
            Queue<TreePair> q = new Queue<TreePair>();
            q.Enqueue(new TreePair(root, 0));
            q.Enqueue(null);
            TreePair lastNode = null;
            while (q.Count > 0)
            {
                TreePair curr = q.Dequeue();
                if (curr == null)
                {
                    if (q.Count > 0)
                    {
                        maxWidth = Math.Max(maxWidth, 1 + lastNode.distance - q.Peek().distance);
                        q.Enqueue(null);
                    }
                }
                else
                {
                    if (curr.key.left != null)
                    {
                        lastNode = new TreePair(curr.key.left, curr.distance * 2 + 1);
                        q.Enqueue(lastNode);
                    }
                    if (curr.key.right != null)
                    {
                        lastNode = new TreePair(curr.key.right, curr.distance * 2 + 2);
                        q.Enqueue(lastNode);
                    }
                }
            }
            return maxWidth;
        }
        class TreePair
        {
            public TreeNode key;
            public int distance;
            public TreePair(TreeNode t, int d)
            {
                key = t;
                distance = d;
            }
        }


        // Time O(n) || Auxillary Space O(1) Recursive Soln
        public static int LongestUnivaluePath(TreeNode r)
        {
            int longestPath = 0;
            GetLongestPath(r);
            return longestPath;

            // LOCAL FUNC
            int GetLongestPath(TreeNode root)
            {
                if (root == null) return 0;
                int left = GetLongestPath(root.left);
                int right = GetLongestPath(root.right);

                int curPath = 0, leftMax = 0, rtMax = 0;
                if (root.left != null && root.left.val == root.val)
                {
                    leftMax = ++left;
                    curPath = leftMax;
                }
                if (root.right != null && root.right.val == root.val)
                {
                    rtMax = ++right;
                    curPath += rtMax;
                }
                // Update Global Max
                longestPath = Math.Max(longestPath, curPath);

                return Math.Max(leftMax, rtMax);
            }
        }


        // Time O(n) || Space O(n)
        public static int MaxNumberOfKSumPairs(int[] nums, int k)
        {
            Dictionary<int, int> numCount = new Dictionary<int, int>(nums.Length / 2);
            int operations = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int find1 = k - nums[i];
                if (numCount.ContainsKey(find1)) // Pair Found
                {
                    operations++;
                    numCount[find1]--;
                    if (numCount[find1] == 0) numCount.Remove(find1);
                }
                else                            // Pair Not Found
                {
                    if (numCount.ContainsKey(nums[i]))   // Curr Num already exist increament count
                        numCount[nums[i]]++;
                    else                                // else Add current num with frequency 1
                        numCount.Add(nums[i], 1);
                }
            }
            return operations;
        }


        // Time O(n) || Space O(1) Soln
        public static int FindWinnerOfArrayGame(int[] arr, int k)
        {
            int larger = arr[0], i = 1, l = arr.Length, winCount = 0;
            while (true)
            {
                if (larger > arr[i])
                    winCount++;
                else
                {
                    winCount = 1;
                    larger = arr[i];
                }
                if (winCount >= k || winCount > l) break;
                i = (i + 1) % l;
                if (arr[i] == larger) i = (i + 1) % l;  // if at any point arr[n]==larger then increament n by 1 more
            }
            return larger;
        }


        // Time = Space = O(n*m), n = no of rows & m = no of cols
        public static int[][] Shift2DGrid(int[][] grid, int k)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            int count = rows * cols;
            k %= (count);
            if (k == 0) return grid;

            int[][] shifted = new int[rows][];
            for (int r = 0; r < rows; r++) shifted[r] = new int[cols];

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                {
                    int copyTo = ((r * cols) + c + k) % count;
                    int rID = copyTo / cols;
                    int cID = copyTo % cols;
                    shifted[rID][cID] = grid[r][c];
                }
            return shifted;
        }


        // Time O(n^3) || Space O(1), n = length of arr
        public static int CountTripletsThatCanFormTwoArraysOfEqualXOR(int[] arr)
        {
            int l = arr.Length, triplets = 0, a, b;

            for (int i = 0; i < l; i++)
            {
                a = arr[i];             // Assign ith to a
                for (int j = i + 1; j < l; j++)
                {
                    b = arr[j];         // Assign jth to b

                    if (a == b) triplets++;
                    for (int k = j + 1; k < l; k++)
                    {
                        b ^= arr[k];    // ADD kth to b
                        if (a == b) triplets++;
                    }
                    a ^= arr[j];        // ADD jth to a
                }
            }
            return triplets;
        }
        // Time O(n^2) || Space O(1), n = length of arr
        public static int CountTripletsThatCanFormTwoArraysOfEqualXORFaster(int[] arr)
        {
            /*  Main idea is that if we fix two points n and j can we do something in between ??
                considering the fact that `a` and `b` values asked in question are xor values of
                two consecutive contiguous subarrays(n meant those two subarrays share a border).. if we can somehow find third point which gives us
                two equal xor values as asked..In that case total xor must be zero(for whatever segment considered by n and j values)
                this implies we can consider xor for any point k n.e(k > n and k <= j) forming segments[n..k - 1] and[k..j]
                which are values of `a` and `b` given in question and of course equal as total segment xor is 0   
                there can be points like(n, k, j), (n, k + 1, j)...(n, k, k) which will all have total xor as 0   
                n.e xor[n..k - 1] = xor[k..j] and xor[n..k] = xor[k + 1..j] and so on   
                but they are different triplets.
            */

            int l = arr.Length, triplets = 0, xor;
            for (int i = 0; i < l; i++)
            {
                // calculate cur_segment_xor between n..j and j > n
                xor = arr[i];
                for (int j = i + 1; j < l; j++)
                {
                    xor ^= arr[j];
                    // xor of cur_segment [n..j] is 0
                    if (xor == 0)
                        // total number of possible values of k in between as k goes from n + 1..j
                        // n.e j - (n + 1) + 1 ==> j - n values in total
                        triplets += j - i;
                }
            }
            return triplets;
        }


        // Time O(n) || Space O(1), n = length of instructions
        public static bool IsRobotBounded(string instructions)
        {
            int x = 0, y = 0, direction = 0;
            foreach (var instruction in instructions)
                switch (instruction)
                {
                    case 'L':
                        direction = (direction + 1) % 4;
                        break;
                    case 'R':
                        direction = (direction + 3) % 4;
                        break;
                    case 'G':
                        switch (direction)
                        {
                            case 0: y++; break;
                            case 1: x--; break;
                            case 2: y--; break;
                            case 3: x++; break;
                        }
                        break;
                }
            return direction != 0 || (x == 0 && y == 0);
        }


        // Time O(n) || Space O(k)
        public static int[] MostCompetitiveSubsequence(int[] nums, int k)
        {
            Stack<int> st = new Stack<int>(k);
            for (int i = 0; i < nums.Length; i++)
            {
                // sequence is already formed and new numbers is larger than last number
                if (st.Count == k && st.Peek() <= nums[i])
                    continue;
                // just enouf numbers remaining in array to form the required sequence of length 'k'
                else if (st.Count + nums.Length - i == k)
                    st.Push(nums[i]);
                else
                {
                    while (st.Count > 0 && st.Peek() > nums[i] && (st.Count + nums.Length - i) > k)
                        st.Pop();
                    st.Push(nums[i]);
                }
            }
            return st.Reverse().ToArray();
        }


        // Time O(n) || Space O(1)
        public static IList<bool> PrefixesDivisibleBy5(int[] A)
        {
            bool[] ans = new bool[A.Length];
            int num = 0;
            for (int i = 0; i < A.Length; i++)
            {
                // we Just store the right most 3 digits which determine if the num is divisible by 5 or not
                num = ((num << 1) + A[i]) % 5;
                ans[i] = (num % 5 == 0) ? true : false;
            }
            return ans;
        }


        // Time O(n) || Space O(1)
        public static IList<string> SummaryRanges(int[] nums)
        {
            IList<string> ans = new List<string>();
            if (nums.Length == 0) return ans;

            int lastNum = nums[0];
            string first = "" + nums[0];
            bool multipleNum = false;

            for (int i = 1; i < nums.Length; i++)
                if (nums[i] == lastNum + 1)
                {
                    lastNum = nums[i];
                    multipleNum = true;
                }
                else
                {
                    ans.Add(multipleNum ? first + "->" + lastNum : first);
                    first = "" + nums[i];
                    lastNum = nums[i];
                    multipleNum = false;
                }

            ans.Add(multipleNum ? first + "->" + lastNum : first);
            return ans;
        }


        // Time O(n) || Space O(1) Soln
        public static int ThirdDistinctMax(int[] nums)
        {
            long max = nums[0], max2nd = long.MinValue, max3rd = long.MinValue;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > max)
                {
                    max3rd = max2nd;
                    max2nd = max;
                    max = nums[i];
                }
                else if (nums[i] > max2nd)
                {
                    if (nums[i] != max)
                    {
                        max3rd = max2nd;
                        max2nd = nums[i];
                    }
                }
                else if (nums[i] > max3rd)
                    if (nums[i] != max2nd)
                        max3rd = nums[i];
            }
            return (int)(max3rd != long.MinValue ? max3rd : max);
        }


        // Time O(n) || Space O(26)~O(1), n = length of word1
        public static bool DetermineIfTwoStringsAreClose(string word1, string word2)
        {
            if (word1.Length != word2.Length) return false;    // different length words

            int[] freq1 = new int[26];
            int[] freq2 = new int[26];
            for (int i = 0; i < word1.Length; i++)
            {
                freq1[word1[i] - 'a']++;
                freq2[word2[i] - 'a']++;
            }

            Dictionary<int, int> charFreq1 = new Dictionary<int, int>(26);
            Dictionary<int, int> charFreq2 = new Dictionary<int, int>(26);
            for (int i = 0; i < 26; i++)
            {
                // check if all unique characters are same in both strings
                if ((freq1[i] == 0 && freq2[i] != 0) || (freq2[i] == 0 && freq1[i] != 0)) return false;

                // if new frequency add it to dictionary
                if (charFreq1.ContainsKey(freq1[i])) charFreq1[freq1[i]]++;
                // if this frequency is seen earlier add 1 to no of characters which have that freq
                else charFreq1.Add(freq1[i], 1);

                if (charFreq2.ContainsKey(freq2[i])) charFreq2[freq2[i]]++;
                else charFreq2.Add(freq2[i], 1);
            }
            // check if all unique characters have same frequency n.e. 3 characters from word1
            // will only match if there is any characters in word2 which is also present 3 times
            foreach (var kvp in charFreq1)
                if (!charFreq2.TryGetValue(kvp.Key, out int noOfCharsWithXFreqFromW2) || noOfCharsWithXFreqFromW2 != kvp.Value) return false;

            return true;
        }



        // Time = O(rows*cols*Log(Min(rows,cols))) || Space = O(rows*cols)
        public static int[][] DiagonalSort(int[][] mat)
        {
            int rows = mat.Length;
            int cols = mat[0].Length;
            List<List<int>> mLS = new List<List<int>>(rows + cols - 1);
            List<int> dg;   // to hold diagonal elements
            int i = 0, r, c;

            for (c = cols - 1; c >= 0; c--) // Fetch all diagonals starting from 1st col
            {
                dg = new List<int>();
                FetchAll(dg, 0, c);
                dg.Sort();
                mLS.Add(dg);
            }
            for (r = 1; r < rows; r++)      // Fetch all diagonals starting from 1st row leaving 0th row
            {
                dg = new List<int>();
                FetchAll(dg, r, 0);
                dg.Sort();
                mLS.Add(dg);
            }

            i = 0;
            c = cols - 1;
            while (c >= 0)                  // Dump sorted diagonals starting from 1st col
                DumpAll(mLS[i++], 0, c--);
            r = 1;
            while (r < rows)                // Dump sorted diagonals starting from 1st row
                DumpAll(mLS[i++], r++, 0);

            return mat;

            // LOCAL FUNC
            void FetchAll(List<int> ls, int rID, int cID)
            { while (rID < rows && cID < cols) ls.Add(mat[rID++][cID++]); }
            void DumpAll(List<int> ls, int rID, int cID)
            { for (int k = 0; k < ls.Count; k++) mat[rID++][cID++] = ls[k]; }
        }


        // Time O(n) || Space O(26)~O(1)
        public static string SmallestSubsequenceOfDistinctCharacters(string s)
        {
            int[] map = new int[26];
            // Get count of all characters in 's'
            for (int i = 0; i < s.Length; i++)      // O(n)
                map[s[i] - 'a']++;

            // To known which all characters have been used so far
            bool[] used = new bool[26];

            Stack<char> st = new Stack<char>();
            for (int i = 0; i < s.Length; i++)      // O(n)
            {
                map[s[i] - 'a']--;      // Decreament Count of current char
                if (!used[s[i] - 'a'])  // This Character is Not already used
                {
                    // While Stack Not Empty && last character is >= current char && last character will come again in future
                    while (st.Count > 0 && st.Peek() - 'a' >= s[i] - 'a' && map[st.Peek() - 'a'] > 0)
                    {
                        used[st.Peek() - 'a'] = false;
                        st.Pop();
                    }
                    st.Push(s[i]);
                    used[s[i] - 'a'] = true;
                }
            }
            return new string(st.Reverse().ToArray());
        }


        // Time O(n) || Space O(n)
        public static string IncreasingDecreasingString(string s)
        {
            int[] map = new int[26];
            for (int i = 0; i < s.Length; i++) map[s[i] - 'a']++;

            StringBuilder sb = new StringBuilder();
            int index = 0;
            bool direction = true;
            while (sb.Length < s.Length)
            {
                while (map[index] == 0) index = Next(index);
                sb.Append((char)(index + 'a'));
                map[index]--;
                index = Next(index);
            }
            return sb.ToString();

            // LOCAL FUNC
            int Next(int num)
            {
                if (direction)
                {
                    if (++num == 26)
                    {
                        num = 25;
                        direction = !direction;
                    }
                }
                else
                {
                    if (--num == -1)
                    {
                        num = 0;
                        direction = !direction;
                    }
                }
                return num;
            }
        }


        // Time O(3*4)~O(1) || Auxillary Space O(1) || Recursive Space O(4)~O(1)
        public static IList<string> RestoreIPAddresses(string str)
        {
            IList<string> ans = new List<string>();
            int len = str.Length;
            int[] ip = new int[4];
            FindIPs(str, 0, 0);
            return ans;

            // Local Func
            void FindIPs(string s, int i, int currOctet)
            {
                if (currOctet == 4)             // all 4 octet found
                {
                    if (i == len)               // if valid IP n.e. no more remaining numbers
                        ans.Add(ip[0] + "." + ip[1] + "." + ip[2] + "." + ip[3]);
                }
                else
                {
                    if (i >= len) return;       // reached end of string but still all 4 valid octets not found
                    if (s[i] == '0')
                    {
                        ip[currOctet] = 0;
                        FindIPs(s, i + 1, currOctet + 1);
                    }
                    else
                    {
                        int num = 0;
                        while (i < len)
                        {
                            num = (num * 10) + s[i++] - '0';
                            if (num > 255) break;
                            ip[currOctet] = num;
                            FindIPs(s, i, currOctet + 1);
                        }
                    }
                }
            }
        }


        // Time O(n) || Space O(1)
        public static int MinimumInsertionsToBalanceAParenthesesString(string s)
        {
            int opening = 0, closing = 0, toBalance = 0, i = 0;
            while (i < s.Length)
            {
                while (i < s.Length && s[i] == '(')
                {
                    opening++;
                    i++;
                }
                while (i < s.Length && s[i] == ')')
                {
                    closing++;
                    i++;
                }

                if (opening * 2 == closing)
                    opening = closing = 0;
                else if (opening * 2 > closing)
                {
                    opening -= closing / 2;
                    if (closing % 2 == 1)
                    {
                        toBalance++;
                        opening--;
                    }
                    closing = 0;
                }
                else
                {
                    closing -= opening * 2;
                    toBalance += ((closing % 2 == 0) ? closing / 2 : (closing / 2) + 2);
                    opening = closing = 0;
                }
            }
            return toBalance + opening * 2;
        }


        // Time O(n*m) || Space O(n), n = no of space sepeareted words in input string & m = length of longest word
        public static IList<string> PrintWordsVertically(string s)
        {
            string[] spaceSeperated = s.Split(' ');
            int longestWord = 0;
            for (int i = 0; i < spaceSeperated.Length; i++) longestWord = Math.Max(longestWord, spaceSeperated[i].Length);
            string[] ans = new string[longestWord];

            int readTillIndex = -1, lastWordLen = 0, n = spaceSeperated.Length;
            while (--n >= 0)
                if (readTillIndex < spaceSeperated[n].Length - 1)
                {
                    lastWordLen = spaceSeperated[n].Length;
                    for (int j = readTillIndex + 1; j < lastWordLen; j++)   // fill words in 'ans' index by index
                        for (int i = n; i >= 0; i--)                        // traverse and read all word from current word till 0th index word
                            if (j >= spaceSeperated[i].Length)
                                ans[j] = ' ' + ans[j];
                            else
                                ans[j] = spaceSeperated[i][j] + ans[j];
                    readTillIndex = lastWordLen - 1;
                }
            return ans;
        }


        // Time O(n^2) || Space O(1)
        // BruteForce Soln
        public static int LongestSubstringContainingVowelsInEvenCounts(string s)
        {
            HashSet<char> odd = new HashSet<char>(5), even = new HashSet<char>(5);
            //even.Add('a'); even.Add('e'); even.Add('n'); even.Add('o'); even.Add('u');
            int ans = 0;

            for (int i = 0; i < s.Length; i++)
            {
                odd.Clear();
                even.Clear();
                even.Add('a'); even.Add('e'); even.Add('i'); even.Add('o'); even.Add('u');
                for (int j = i; j < s.Length; j++)
                {
                    char ch = s[j];
                    if (isVowel(ch))
                    {
                        if (even.Contains(ch))
                        {
                            even.Remove(ch);
                            odd.Add(ch);
                        }
                        else
                        {
                            even.Add(ch);
                            odd.Remove(ch);
                        }
                    }
                    if (odd.Count == 0) ans = Math.Max(ans, 1 + j - i);
                }
            }
            return ans;

            // LOCAL FUNC
            bool isVowel(char c) => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
        }
        // Time O(n^2) || Space O(1)
        public static int LongestSubstringContainingVowelsInEvenCountsEfficient(string s)
        {
            /* We start with evenStatus as 0 indicating even no of vowels 'a' 'e' 'n' 'o' 'u'
             * Now we traverse thru all possible substrings of 's'
             * while traversing each substring we do below
             *  if vowel
             *      a => xor 1 to 1st bit
             *      e => xor 1 to 2nd bit
             *      n => xor 1 to 3rd bit
             *      o => xor 1 to 4th bit
             *      u => xor 1 to 5th bit
             *  else do nothing
             * Reason for XOR is it flips 1 to 0 and 0 to 1
             * so only thing we have to check, if evenStatus equals 'zero' means al vowels encountered so far are in even count
             */
            int ans = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int evenStatus = 0;
                for (int j = i; j < s.Length; j++)
                {
                    switch (s[j] - 'a')
                    {
                        case 0:
                            evenStatus ^= 1;
                            break;
                        case 4:
                            evenStatus ^= 1 << 2;
                            break;
                        case 8:
                            evenStatus ^= 1 << 3;
                            break;
                        case 14:
                            evenStatus ^= 1 << 4;
                            break;
                        case 20:
                            evenStatus ^= 1 << 5;
                            break;
                        default:
                            break;
                    }
                    if (evenStatus == 0) ans = Math.Max(ans, 1 + j - i);
                }
            }
            return ans;
        }
        // Time O(n) || Space O(1)
        public static int LongestSubstringContainingVowelsInEvenCountsFastest(string s)
        {
            /* Clearing last algo was taking 'quadratic time' since we were going thru all substrings of input
             * 
             * One way to get same result in linear time is by traversing from 0th to last index,
             * 
             * and if we see an non zero value we save it in HashTable along with 1st index it was seen at
             * next time we encounter same value we know all letters we added b/w that index and current index summed up to zero thats only way same value is possible again
             * hence now by update 'ans' if its greater than current index - last index seen with same value,
             */
            int ans = 0, evenStatus = 0;
            Dictionary<int, int> valSeenAtIdx = new Dictionary<int, int>();
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case 'a':
                        evenStatus ^= 1;
                        break;
                    case 'e':
                        evenStatus ^= 1 << 2;
                        break;
                    case 'i':
                        evenStatus ^= 1 << 3;
                        break;
                    case 'o':
                        evenStatus ^= 1 << 4;
                        break;
                    case 'u':
                        evenStatus ^= 1 << 5;
                        break;
                    default:
                        break;
                }
                if (evenStatus == 0) ans = Math.Max(ans, 1 + i);        // Vowel are even starting from 0th
                else if (valSeenAtIdx.ContainsKey(evenStatus)) ans = Math.Max(ans, i - valSeenAtIdx[evenStatus]);   // current evenStatus was seen before
                else valSeenAtIdx.Add(evenStatus, i);                   // else add this new evenStatus along with current index
            }
            return ans;
        }


        // Brute Force, Find all path approach with Exponential Time Complexicity
        // Time O(n^2) || Space O(n), n = rows*cols if input 2D grid
        public static int MinimumEffortPathBruteForce(int[][] heights)
        {
            int rows = heights.Length;
            int cols = heights[0].Length;
            bool[,] visited = new bool[rows, cols];

            return GetEffort(0, 0, heights[0][0]);

            // LOCAL FUNC
            int GetEffort(int r, int c, int lastHt)
            {
                // not a valid cell or already visited return -1 to indicate destination not reachable
                if (r < 0 || r >= rows || c < 0 || c >= cols || visited[r, c]) return -1;

                // Reached destination
                if (r == rows - 1 && c == cols - 1)
                    return Math.Abs(lastHt - heights[r][c]);

                int effort = Int32.MaxValue;
                visited[r, c] = true;

                int currEffort = 0;
                currEffort = GetEffort(r - 1, c, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);

                currEffort = GetEffort(r + 1, c, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);

                currEffort = GetEffort(r, c - 1, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);

                currEffort = GetEffort(r, c + 1, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);

                visited[r, c] = false;
                return effort == Int32.MaxValue ? -1 : (Math.Max(Math.Abs(lastHt - heights[r][c]), effort));
            }
        }
        // Time O(logk*(r*c)) || Space O(r*c), k = 2x10^6 & r = no of rows & c = no of cols
        public static int MinimumEffortPath(int[][] heights)
        {
            /* Instead of finding all the paths from source to destination and than returing the path with min Absolute difference
             * We restrict our path search with some value 'k' (Max permissible Difference b/w 2 diff cells height)
             * and if we are not able to reach from source to destination with this restricted value 'k'.
             * We increament the 'k' by 1. So this way are finding path with by linearly increasing value of K. [K = 1..2..3...]
             * 
             * Better approach can we be can take one start and last value and mid of these would be 'k',
             * if we are able to reach Source->Destination from current mid than we update last to mid, else update start to mid + 1
             * Return last 'k' which was succes
             */
            int rows = heights.Length;
            int cols = heights[0].Length;
            bool[,] visited = new bool[rows, cols];
            int start = 0, last = 2000001, mid;

            int absMinEffort = Int32.MaxValue;
            while (start <= last)
            {
                mid = start + (last - start) / 2;
                int absDiff = GetEffort(0, 0, heights[0][0]);
                if (absDiff != -1)
                {
                    absMinEffort = absDiff;
                    if (absMinEffort == 0) break;       // Reached Destination with 0 AbsDiff than break loop
                    last = Math.Min(mid - 1, absMinEffort - 1);
                }
                else start = mid + 1;
                visited = new bool[rows, cols];         // reset visited Array for next iteration
            }
            return absMinEffort;

            // LOCAL FUNC for DFS traversal
            int GetEffort(int r, int c, int lastHt)
            {
                // not a valid cell or already visited or absDiff is greater than current limit return -1 to indicate destination not reachable
                if (r < 0 || r >= rows || c < 0 || c >= cols || visited[r, c] || Math.Abs(lastHt - heights[r][c]) > mid) return -1;

                // Reached destination
                if (r == rows - 1 && c == cols - 1)
                    return Math.Abs(lastHt - heights[r][c]);

                int effort = Int32.MaxValue;
                visited[r, c] = true;       // mark current cell as visited

                int currEffort = 0;
                // up
                currEffort = GetEffort(r - 1, c, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);
                // down
                currEffort = GetEffort(r + 1, c, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);
                // left
                currEffort = GetEffort(r, c - 1, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);
                // right
                currEffort = GetEffort(r, c + 1, heights[r][c]);
                if (currEffort != -1) effort = Math.Min(effort, currEffort);

                return effort == Int32.MaxValue ? -1 : (Math.Max(Math.Abs(lastHt - heights[r][c]), effort));
            }
        }


        // Time O(N) || Space O(1)
        public static int RotatedDigits(int N)
        {
            int ans = 0;
            for (int i = 1; i <= N; i++)   // O(n)
                if (IsGoodNum(i))
                    ans++;
            return ans;
            // Local Func to check if no is good after rotation
            bool IsGoodNum(int num) // O(1)
            {
                bool flag = false;
                while (num > 0)
                {
                    int k = num % 10;
                    if (k == 3 || k == 4 || k == 7) return false;
                    if (k == 2 || k == 5 || k == 6 || k == 9) flag = true;
                    num = num / 10;
                }
                return flag;
            }
        }


        // Time O(Min(n,m)) || Space O(1), n = len of word1 & m = len of word2
        public static int MinDistance(string word1, string word2)
        {
            int l1 = word1.Length, l2 = word2.Length;
            if (l1 == 0) return l2;
            if (l2 == 0) return l1;

            // Find longest common subsequence of both words
            int[,] LCS = new int[l1 + 1, l2 + 1];
            for (int i = 0; i < l1; i++)
                for (int j = 0; j < l2; j++)
                    if (word1[i] == word2[j])
                        LCS[i + 1, j + 1] = 1 + LCS[i, j];
                    else
                        LCS[i + 1, j + 1] = Math.Max(LCS[i + 1, j], LCS[i, j + 1]);

            return (l1 - LCS[l1, l2]) + (l2 - LCS[l1, l2]);
        }


        // Time O(n*k) || Space O(1), k = avg length of binary representation of numbers
        public static int ConcatenatConsecutiveBinaryNumbers(int n)
        {
            int decimalValue = 0;
            for (int i = 1; i <= n; i++)
                foreach (var ch in DecToBinary(i))
                    decimalValue = ((decimalValue << 1) + ch - '0') % (1000000007);

            return decimalValue;

            // Local func to convert decimal no to its binary representation
            string DecToBinary(int num)
            {
                string s = "";
                while (num != 0)
                {
                    s = (num & 1) + s;
                    num = num >> 1;
                }
                return s;
            }
        }
        // Time O(n) || Space O(1)
        public static int ConcatenatConsecutiveBinaryNumbersFaster(int n)
        {
            long decimalValue = 0;
            for (int i = 1; i <= n; i++)
                decimalValue = (((decimalValue << Length(i)) % 1000000007) + i);

            return Convert.ToInt32(decimalValue);
            // Local func to get length of integer in binary n.e. no of 1's & 0's by using formula => 1 + Log (base2) of Num
            int Length(int num) => 1 + (int)(Math.Log10(num) / Math.Log10(2));
        }


        // Time O(Max(n,k)), n = length of 's' || Space O(26) ~O(1)
        public static bool CanConvertString(string s, string t, int k)
        {
            int sLen = s.Length, tLen = t.Length, currDiff = 0;
            if (sLen != tLen) return false;    // if strings are not of same length they can't be converted to match

            Dictionary<int, int> shiftDiff = new Dictionary<int, int>(26);
            for (int i = 0; i < sLen; i++)
            {
                currDiff = (t[i] - 'a') - (s[i] - 'a');
                if (currDiff < 0) currDiff += 26;

                // Find the shifts required for each index to match char in both strings
                if (shiftDiff.ContainsKey(currDiff)) shiftDiff[currDiff]++;
                else shiftDiff.Add(currDiff, 1);
            }

            shiftDiff.Remove(0);
            for (int i = 1; i <= k; i++)
                if (shiftDiff.ContainsKey(i % 26) && --shiftDiff[i % 26] == 0)
                {
                    shiftDiff.Remove(i % 26);
                    if (shiftDiff.Count == 0) return true;
                }

            return shiftDiff.Count == 0;
        }
        // Time O(n), n = length of 's' || Space O(26) ~O(1)
        public static bool CanConvertStringFaster(string s, string t, int k)
        {
            int sLen = s.Length, currDiff = 0;
            if (sLen != t.Length) return false;     // if strings are not of same length they can't be converted to match

            int[] rotationsReq = new int[26];
            for (int i = 0; i < sLen; i++)
            {
                currDiff = (t[i] - 'a') - (s[i] - 'a');
                if (currDiff == 0) continue;        // 0 rotation required than move to next index
                if (currDiff < 0) currDiff += 26;

                if (rotationsReq[currDiff]++ * 26 + currDiff > k)
                    return false;
            }
            return true;
        }


        // Time O(n) || Auxillary Space O(1) || Recursive Space O(n)
        public static string SmallestStringWithAGivenNumericValueRecursive(int n, int k)
        {
            char[] ans = new char[n];
            string smallestStr = "";
            GetSmallestString(k, n - 1);
            return smallestStr;

            // Local Func
            bool GetSmallestString(int sumRequired, int currIndex)
            {
                if (currIndex == -1)
                {
                    if (sumRequired == 0)               // smallest string found
                    {
                        smallestStr = new string(ans);
                        return true;
                    }
                }
                else
                    // Try using highest value(26=='z') first, if doesn't results in valid string than try smaller value till (1=='a')
                    for (int val = Math.Min(26, sumRequired - currIndex); val >= 1; val++)
                    {
                        ans[currIndex] = (char)(val - 1 + 'a');       // since chars are 0 indexed
                        if (GetSmallestString(sumRequired - val, currIndex - 1)) return true;
                    }
                return false;
            }
        }
        // Time O(n) || Space O(1)
        public static string SmallestStringWithAGivenNumericValueIterative(int n, int k)
        {
            char[] ans = new char[n];
            return GetSmallestString(k, n - 1);

            // Local Func
            string GetSmallestString(int sumRequired, int currIndex)
            {
                int valUsed = 0;
                while (currIndex != -1)
                {
                    // Use Minimum of 
                    // a) highest possible value either (26=='z')
                    // b) sumRequired - no of indexes left to fill
                    valUsed = Math.Min(26, sumRequired - currIndex);
                    ans[currIndex] = (char)(valUsed - 1 + 'a');
                    sumRequired -= valUsed;
                    currIndex--;
                }
                return new string(ans);
            }
        }


        // Time O(nLogn) || Space O(1), n is no of trips
        public static bool CarPooling(int[][] trips, int capacity)
        {
            ///* First Sort the trips by Drop-Off Time
            // * Than Sort it again by Pick-Up Time
            // * Now Start iterating from 1st to last trip,
            // *      pickup people (decrease capacity)
            // *      dropoff people (increase capacity)
            // * If at any point capacity goes -ve return false
            // * else return true at end
            // */
            //Array.Sort(trips, new CarPoolSortByDropOffTime());
            //Array.Sort(trips, new CarPoolSortByPickUpTime());

            ////int start = trips[0][1], last = trips[trips.Length - 1][1];
            //int picking = 0, dropping = 0;


            //while (picking < trips.Length)
            //{
            //    if (trips[picking][1] < trips[dropping][2])     // if ith trip pickTime is less than jth trip drop time, pick passenger and reduce capacity
            //        capacity -= trips[picking++][0];
            //    else                                            // else drop-off passenger and increase capacity
            //        capacity += trips[dropping++][0];
            //    // At any point capcity goes -ve return false
            //    if (capacity < 0) return false;
            //}
            //return true;

            //-- Above algo yields wrong result when departure time of lets say 1st trip is highest than rest all
            // dropping index will stack stuck at 1st trip and return false even though
            // passengers who boarded after 1st trip have dropped off earlier than 1st trip passengers--//

            /* Another Approach is to add passengers in List by their start timestamp
             * and also add -ve passenger passengers in list by their departure timestamp
             * Sort the list by timestamp and iterate thru it,
             * at any point current capacity exceeds total capacity return false else true
             */

            SortedDictionary<int, int> sortedTimeStamp = new SortedDictionary<int, int>();
            for (int i = 0; i < trips.Length; i++)                  // O(n)
            {
                if (!sortedTimeStamp.ContainsKey(trips[i][1])) sortedTimeStamp.Add(trips[i][1], 0);
                sortedTimeStamp[trips[i][1]] += trips[i][0];
                if (!sortedTimeStamp.ContainsKey(trips[i][2])) sortedTimeStamp.Add(trips[i][2], 0);
                sortedTimeStamp[trips[i][2]] -= trips[i][0];
            }
            foreach (var passengerCount in sortedTimeStamp.Values)  // O(2n)~O(n)
            {
                capacity -= passengerCount;
                if (capacity < 0) return false;
            }
            return true;
        }
        //public class CarPoolSortByPickUpTime : IComparer<int[]>
        //{ public int Compare(int[] a, int[] b) => a[1] - b[1]; }
        //public class CarPoolSortByDropOffTime : IComparer<int[]>
        //{ public int Compare(int[] a, int[] b) => a[2] - b[2]; }

        // Time O(Max(n,1001) || Space O(1001)~O(1), n is no of trips
        public static bool CarPoolingBucketSort(int[][] trips, int capacity)
        {
            /* Since Max pick-up/drop-off time is given as 1000 in problem
             * we create 1001 buckets and add +passengers for given pick-up time
             * we create 1001 buckets and subtract -passengers for given drop-off time
             * 
             * Than we traverse the buckets array if at any moment capacity goes -ve return false else true
             */
            int k = 1001;   // max pick-up/drop-off time
            int[] buckects = new int[k];
            for (int i = 0; i < trips.Length; i++)
            {
                buckects[trips[i][1]] += trips[i][0];
                buckects[trips[i][2]] -= trips[i][0];
            }
            for (int i = 0; i < k; i++)
            {
                capacity -= buckects[i];
                if (capacity < 0)
                    return false;
            }
            return true;
        }


        // Time O(xAxis*hlogh) || Space = O(n), n = no of nodes in Binary Tree, xAxis no or different xAxis & h = avg length of List at given xAxis
        public static IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            Dictionary<int, List<VerticalTreePair>> dict = new Dictionary<int, List<VerticalTreePair>>();
            int min = int.MaxValue, max = int.MinValue;
            VerticalTraversal(root, 0, 0);              // O(n)

            List<IList<int>> ans = new List<IList<int>>();
            for (int i = min; i <= max; i++)            // O(xAxis)
            {
                dict[i].Sort(new VerticalSort());       // Sort Vertically
                ans.Add(new List<int>());
                for (int j = 0; j < dict[i].Count; j++) // O(yAxis)
                    ans[ans.Count - 1].Add(dict[i][j].val);
            }
            return ans;

            // Local Func
            void VerticalTraversal(TreeNode r, int xAxis, int yAxis)        // InOrder
            {
                if (r == null) return;
                VerticalTraversal(r.left, xAxis - 1, yAxis - 1);

                min = Math.Min(min, xAxis);
                max = Math.Max(max, xAxis);

                if (!dict.ContainsKey(xAxis)) dict.Add(xAxis, new List<VerticalTreePair>());
                dict[xAxis].Add(new VerticalTreePair(r.val, yAxis));

                VerticalTraversal(r.right, xAxis + 1, yAxis - 1);
            }
        }
        public class VerticalSort : IComparer<VerticalTreePair>
        {
            public int Compare(VerticalTreePair a, VerticalTreePair b)
            {
                int sort = b.vPos - a.vPos;                 // Sort by y Axis (HighYAxis has higher priority)
                return sort != 0 ? sort : a.val - b.val;    // if both have same YAxis than choose smaller value
            }
        }
        public class VerticalTreePair
        {
            public int val, vPos;
            public VerticalTreePair(int val, int vPos)
            {
                this.val = val;
                this.vPos = vPos;
            }
        }


        /// <summary>
        /// You are given an array nums of n positive integers.
        /// 
        /// You can perform two types of operations on any element of the array any number of times:
        /// If the element is even, divide it by 2.
        ///     For example, if the array is [1,2,3,4], then you can do this operation on the last element, and the array will be[1, 2, 3, 2].
        /// If the element is odd, multiply it by 2.
        ///     For example, if the array is [1,2,3,4], then you can do this operation on the first element, and the array will be[2, 2, 3, 4].
        /// 
        /// The deviation of the array is the maximum difference between any two elements in the array.
        /// 
        /// Return the minimum deviation the array can have after performing some number of operations.
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MinimumDeviation(int[] nums)
        {
            #region FIRST ATTEMPT
            /* FIRST ATTEMPT, FAILED for arr {10,4,3} deviation should be => 2
            if (nums.Length < 2) return 0;
            int max = nums[0], min = nums[0];

            for (int n = 1; n < nums.Length; n++)
            {
                if (nums[n] < min)
                    // if curr num is odd try multiple by 2 to reduce deviation
                    if (nums[n] % 2 == 1 && (nums[n] <= max / 2 || Math.Abs(nums[n] * 2 - max) < Math.Abs(nums[n] - min))) nums[n] *= 2;
                else if (nums[n] > max)
                    // if curr num is even try reducing by half to reduce deviation
                    while (nums[n] % 2 == 0 && (nums[n] / 2 >= min || Math.Abs(nums[n] / 2 - min) < Math.Abs(nums[n] - max))) nums[n] /= 2;
                min = Math.Min(min, nums[n]);
                max = Math.Max(max, nums[n]);
            }
            while (max % 2 == 0 && Math.Abs(max / 2 - min) < Math.Abs(max - min)) max /= 2;
            while (min % 2 == 1 && Math.Abs(max - min * 2) < Math.Abs(max - min)) min *= 2;
            return Math.Abs(max - min);
            */
            #endregion

            /* Since we have two options to transform array by either to dividing even nums by 2
             * or we can multiple odd nums by 2 (max 1 opteration as num becomes even than)
             * 
             * Approach is to stick to any one of the 2 avaliable options,
             * Below approach uses first option (even no can be divided by 2, untill they become odd)
             * 
             * Traverse thru array and add each element to MaxHeap based upon:
             *      if odd => multiple num*2 & add to MaxHeap (as it can we reduced later)
             *      if even simply add to heap
             * also maintain MinValue which is added to Heap
             * 
             * Set deviation to int.MaxValue
             * Now Extract top from MaxHeap untill Heap is Not empty
             *      ans = Math.Min(ans,HeapTop-min)
             *  
             *  if HeapTop is even reinsert it back to heap as HeapTop/2
             *      also update min = Math.Min(min,HeapTop/2);
             *  else if HeapTop is odd, break out of loop as odd number can't be further reduced
             */

            int min = int.MaxValue;
            MaxHeap h = new MaxHeap(nums.Length);
            for (int i = 0; i < nums.Length; i++)                   // O(n)
                if (nums[i] % 2 == 0)
                {
                    h.Insert(nums[i]);                              // O(logn)
                    min = Math.Min(min, nums[i]);
                }
                else
                {
                    h.Insert(nums[i] * 2);                          // O(logn)
                    min = Math.Min(min, nums[i] * 2);
                }

            int deviation = int.MaxValue;
            // Worst case time complexity would be when all nums are maximum possible numbers of powers of 2
            // Lets we consider that large possible number high power of 2 is 'm'
            // we would extract and reinsert 'm' logm times untill its finally reduced '1'
            // and since there are total 'n' nums Heapify operation would take logn time
            // Time Complexity => (n*logm*logn)
            // Space Complexity => O(n)
            while (h.Count > 0)
            {
                int max = h.ExtractMax();
                deviation = Math.Min(deviation, max - min);

                // found odd number break out
                if (max % 2 == 1) break;

                h.Insert(max / 2);
                min = Math.Min(min, max / 2);
            }
            return deviation;
        }


        // Time O(n) || Space O(n), n = length of 'num'
        public static string RemoveKDigits(string num, int k)
        {
            Stack<char> st = [];
            foreach (var ch in num)              // O(n)
            {
                // remove prv/more significant digits if they are larger than curr (while K > 0)
                while (k > 0 && st.Count > 0 && st.Peek() > ch)
                {
                    st.Pop();
                    k--;
                }
                st.Push(ch);
            }
            // remove any excess larger digits from the end/last if there is still need to remove more digits
            while (k-- > 0 && st.Count > 1)
                st.Pop();

            // now create the final answer
            var chArr = st.Reverse().ToArray();
            // skip leading zero's
            int i = 0, l = chArr.Length;
            while (i < l && chArr[i] == '0')
                i++;

            // append all digits after getting 1st non-zero digit
            StringBuilder sb = new();
            while (i < l) sb.Append(chArr[i++]);

            string numberAfterKRemovals = sb.ToString();
            // return empty if not empty, else return single zero if result is empty
            return numberAfterKRemovals != "" ? numberAfterKRemovals : "0";


            #region Prv Approach
            ///* Traversing from the starting index
            // * if stack is empty add current digit to stack if num is not '0'
            // * else if check while Last Inserted digit is > current digit than remove stack top and decreament 'k'
            // * 
            // * Now if k > 0 remove finishTime.Top() & decreament 'k' untill k == 0
            // * 
            // * at end pull out all digits from stack and reverse & return
            // */
            //int l = num.Length;
            //if (k >= l) return "0";
            //Stack<char> st = new Stack<char>();

            //for (int i = 0; i < l; i++)
            //    if (k > 0)
            //    {
            //        // while we have digit greater than current digit as stack top, Pop stack
            //        while (st.Count > 0 && st.Peek() - '0' > num[i] - '0')
            //        {
            //            st.Pop();
            //            if (--k == 0) break;
            //        }

            //        // if stack is empty and next digit to be pushed it 0 than skip adding leading zeros
            //        while (st.Count == 0 && num[i] == '0' && i < l - 1) i++;
            //        st.Push(num[i]);
            //    }
            //    else st.Push(num[i]);

            //while (k-- > 0 && st.Count > 1) st.Pop();

            //return new string(st.Reverse().ToArray());
            #endregion
        }


        // Time = Space = O((n-m)*m), n & m = lengths of target & stamp respectively
        public static int[] MovesToStamp(string stamp, string target)
        {
            /* Keep track of all windows which match stamp by having 2 list for each valid window of stamp.Length
             *      => made list(matching letters from target found in stamp)
             *      => todo list(matching letters from target not found in stamp)
             * we calculate this list for each valid window index in target,
             * if todo list of letters is empty meaning all letters already found that add starting index of this list to an Stack of ans
             * and also add & mark done all(in boolean array) index of the matched window indicies to Queue.
             * 
             * after populating all the 'made' & 'todo' list of letters for each index,
             * starting Enqueuing Queue untill its empty
             * and for each index that comes out from Front, do below
             *      => for each index that is pulled out find all valid windows to which this index is part of
             *      => than remove the index from each of these list if current index is present in todo list
             *      => if todo list of given index gets empty at any point, than add starting index of current window to 'ans'
             *          and all indicies in window which are not already marked done to Queue as well.
             * 
             * now check if all the index are not already marked done than result empty array stating stamping not possible to creating target
             * 
             * else add all the starting index from Stack 'ans' to int array and return the desired result
             */
            int n = target.Length, m = stamp.Length;
            Stack<int> ans = new Stack<int>();
            Queue<int> q = new Queue<int>();
            List<StampWindow> ls = new List<StampWindow>(1 + n - m);
            bool[] stamped = new bool[n];
            int indiciesStamped = 0, i = 0;

            HashSet<int> made, todo;
            for (i = 0; i <= n - m; i++)            // O(n-m)
            {
                made = new HashSet<int>();
                todo = new HashSet<int>();
                // Now check each letter for window starting at index 'n' and ending at 'n+m-1'
                for (int j = 0; j < m; j++)         // O(m)
                    if (target[i + j] == stamp[j])
                        made.Add(i + j);
                    else
                        todo.Add(i + j);

                ls.Add(new StampWindow(made, todo));// update made and todo list for window starting at 'n' index in list

                if (todo.Count == 0)                // all letters matched
                {
                    ans.Push(i);                    // push starting index of window in 'ans' Stack
                    foreach (int index in made)     // also mark all the indicies in Target which have been stamped now
                        if (!stamped[index])
                        {
                            indiciesStamped++;
                            q.Enqueue(index);
                            stamped[index] = true;
                        }
                }
            }

            while (q.Count > 0)
            {
                i = q.Dequeue();
                // For all valid windows this index is part of
                for (int k = Math.Max(0, 1 + i - m); k <= Math.Min(n - m, i); k++)
                {
                    if (ls[k].todo.Contains(i))
                    {
                        ls[k].todo.Remove(i);
                        ls[k].made.Add(i);
                        if (ls[k].todo.Count == 0)              // all letters matched
                        {
                            ans.Push(k);                        // push starting index of window in 'ans' Stack
                            foreach (int index in ls[k].made)   // also mark all the indicies in Target which have been stamped now
                                if (!stamped[index])
                                {
                                    indiciesStamped++;
                                    q.Enqueue(index);
                                    stamped[index] = true;
                                }
                        }
                    }
                }
                if (indiciesStamped == n) break;    // This optimization avoid adding un-necessary starting index and also helps save time if all indicies r stamped
            }

            if (indiciesStamped < n)                // not all indicies could be stamped than return empty array to indicate stamping to create target not possible
                return new int[0];

            int[] result = new int[ans.Count];
            i = 0;
            while (ans.Count > 0) result[i++] = ans.Pop();      // starting indicies are saved into result in reverse order n.e from first stamp to last stamp made

            return result;
        }
        public class StampWindow
        {
            public HashSet<int> made, todo;
            public StampWindow(HashSet<int> m, HashSet<int> t)
            {
                made = m;
                todo = t;
            }
        }

        // Time = Space = O(1), as max it would take 32 left shifts before n becomes zero
        public static int HammingWeight(uint n)
        {
            int oneBits = 0;
            while (n != 0)
            {
                oneBits += (int)n & 1;
                n >>= 1;
            }
            return oneBits;
        }


        // Time O(n) || Recursive Space O(h) || Recursive Soln
        public static TreeNode TrimBST(TreeNode root, int low, int high)
        {
            if (root == null) return null;
            if (root.val < low) return TrimBST(root.right, low, high);
            if (root.val > high) return TrimBST(root.left, low, high);

            root.left = TrimBST(root.left, low, high);
            root.right = TrimBST(root.right, low, high);
            return root;
        }


        // Time O(m*klogk) Space O(n), n = length of nums, m = length of l, k = avg diff r[n]-l[n]
        public static IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
        {
            bool[] ans = new bool[l.Length];
            int[] original = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++) original[i] = nums[i];    // keep copy of original array

            for (int i = 0; i < l.Length; i++)
                if (IsArthematic(l[i], r[i]))
                    ans[i] = true;
            return ans;

            // Local Func
            bool IsArthematic(int start, int last)
            {
                if (last == start) return false;                            // base case
                for (int i = start; i <= last; i++) nums[i] = original[i];  // copy back from the original array

                Array.Sort(nums, start, 1 + last - start);                  // sort in asc order from [start...last]
                int diff = nums[start + 1] - nums[start];
                for (int i = start + 1; i < last; i++)
                    if (nums[i + 1] - nums[i] != diff)
                        return false;
                return true;
            }
        }


        // Time O(nlogn) || Space O(n)
        public static void WiggleSort(int[] nums)
        {
            int l = nums.Length, last = nums.Length - 1;
            int[] copy = new int[l];
            Array.Copy(nums, copy, l);
            Array.Sort(copy);
            // copy odd no's
            for (int i = 1; i < l; i += 2) nums[i] = copy[last--];
            // copy even no's
            for (int i = 0; i < l; i += 2) nums[i] = copy[last--];
        }


        // Time = Space = O(n), 1 Pass Algo
        public static int FindLHS(int[] nums)
        {
            Dictionary<int, int> numCount = new Dictionary<int, int>();
            int maxHarmonious = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (!numCount.ContainsKey(nums[i])) numCount.Add(nums[i], 1);
                else numCount[nums[i]]++;

                if (numCount.ContainsKey(nums[i] - 1))      // if number with 1 lessor value present
                    maxHarmonious = Math.Max(maxHarmonious, numCount[nums[i]] + numCount[nums[i] - 1]);
                if (numCount.ContainsKey(nums[i] + 1))      // if number with 1 bigger value present
                    maxHarmonious = Math.Max(maxHarmonious, numCount[nums[i]] + numCount[nums[i] + 1]);
            }

            return maxHarmonious;
        }
        // Time = Space = O(n), 2 Pass
        public static int FindLHSFaster(int[] nums)
        {
            Dictionary<int, int> numCount = new Dictionary<int, int>();
            int maxHarmonious = 0;
            for (int i = 0; i < nums.Length; i++)
                if (!numCount.ContainsKey(nums[i])) numCount.Add(nums[i], 1);
                else numCount[nums[i]]++;

            foreach (int n in numCount.Keys)
                if (numCount.ContainsKey(n + 1))      // if number with 1 bigger value present
                    maxHarmonious = Math.Max(maxHarmonious, numCount[n] + numCount[n + 1]);

            return maxHarmonious;
        }


        // Time O(n) || Space O(1), n = sum of total no of characters for each word in words
        public static string[] KeyBoardRow(string[] words)
        {
            int[] chSet = new int[26];
            foreach (var ch in "qwertyuiop") chSet[ch - 'a'] = 1;
            foreach (var ch in "asdfghjkl") chSet[ch - 'a'] = 2;
            foreach (var ch in "zxcvbnm") chSet[ch - 'a'] = 3;

            List<string> ans = new List<string>();
            for (int i = 0; i < words.Length; i++)
                if (HasSameRowLetters(words[i].ToLower()))
                    ans.Add(words[i]);
            return ans.ToArray();

            // Local Func
            bool HasSameRowLetters(string word)
            {
                int rowID = chSet[word[0] - 'a'];
                for (int i = 1; i < word.Length; i++)
                    if (chSet[word[i] - 'a'] != rowID)
                        return false;
                return true;
            }
        }


        /// <summary>
        /// Given a string path, which is an absolute path, return the simplified canonical path.
        /// Constraints:
        ///     1 <= path.length <= 3000
        ///     path consists of English letters, digits, period '.', slash '/' or '_'.
        ///     path is a valid absolute Unix path.
        /// Time = Space = O(n)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string SimplifyPath(string path)
        {
            int l = path.Length;
            StringBuilder sb = new StringBuilder();
            Stack<string> st = new Stack<string>();
            for (int i = 0; i < l; i++)
            {
                if (path[i] == '/')
                    continue;
                else if (path[i] == '.')
                {
                    while (i < l && path[i] != '/')
                        sb.Append(path[i++]);

                    if (sb.Length >= 3)
                        st.Push(sb.ToString());             // Push new folder to Stack
                    else if (sb.Length == 2)
                        if (path[i - 1] != '.') st.Push(sb.ToString());// Push new folder to Stack
                        else if (st.Count > 0) st.Pop();    // move 1 directory up

                    sb.Clear();
                }
                else    // folder/file name
                {
                    while (i < l && path[i] != '/')         // get the full name of current folder
                        sb.Append(path[i++]);
                    st.Push(sb.ToString());                 // Push new folder to Stack
                    sb.Clear();
                }

            }
            if (st.Count == 0) return "/";                  // if stack Empty

            sb.Clear();
            foreach (var p in st.Reverse().ToArray()) sb.Append('/').Append(p);
            return sb.ToString();
        }



        // DP Bottom-Up Approach
        // Time = Space = O(n)
        public static int StudentAttendanceRecordII_DP(int n)
        {
            #region FIRST RECURSIVE APPROACH, FAILED
            //public static int StudentAttendanceRecordII(int n, int continuousLate = 0, int oneAbsentUsed = 0)
            //{
            //    if (n == 0) return 1;
            //    int mod = 1000000007;
            //    long ans = 0;

            //    foreach (var ch in new char[] { 'A', 'L', 'P' })
            //        if (oneAbsentUsed == 0 && ch == 'A')                    // 1 absent not used and current day attendance is 'A'
            //            ans += StudentAttendanceRecordII(n - 1, 0, 1) % mod;
            //        else if (ch == 'L' && continuousLate + 1 < 3)           // continous late should not be > 2
            //            ans += StudentAttendanceRecordII(n - 1, continuousLate + 1, oneAbsentUsed) % mod;
            //        else if (ch == 'P')
            //            ans += StudentAttendanceRecordII(n - 1, 0, oneAbsentUsed) % mod;
            //    return (int)ans;
            //}
            #endregion

            /* This question is a very difficult question. 
             * If you think about the number, you will definitely time out by searching for something.
             * You can only try to push to the recursion relationship, and then use the dynamic programming method to solve.
             * Since A has only 0 and 1 possibilities, the results are added by considering A == 0 and A==1, respectively.
             * 
             * When A == 0, there is only a combination of L and P at this time.
             * In order to satisfy no more than two consecutive Ls, L and P have only three cases at the end: LL, PL, XP. X stands for both L and P.
             * Use a, b, and c respectively to represent the above three cases.
             * 
             * Then you can get the following relationship:
             * a[n] = b[n-1]，b[n] = c[n-1], c[n] = a[n-1] + b[n-1] + c[n-1]
             * 
             * Then the total num[n] = a[n] + b[n] + c[n]
             * 
             * When A==1, A can be at any position of [1, n].
             * When A is placed at n position, the possible numbers of the corresponding left and right sides are num[n-1], num[ni], respectively.
             * Then the total number of A in the n position is num[n-1] * num[ni].
             * 
             * The most important thing about this question is to find the final recursive relationship,
             * but when looking for a recursive relationship, you need to take A out of consideration,
             * otherwise the recursive relationship is difficult to write.
             */

            if (n < 3)
            {
                if (n == 0) return 1;
                if (n == 1) return 3;       // P, L, A
                if (n == 2) return 8;       // PP, PL, PA, LP, LL, LA, AP, AL
            }
            int mod = 1000000007;           // mod is 10^9 + 7

            long[] a = new long[n + 1];
            long[] b = new long[n + 1];
            long[] c = new long[n + 1];
            long[] LateAndPresentCombo = new long[n + 1];

            // Possible combination of attendance to be marked
            LateAndPresentCombo[0] = 1;     // for 0 days there is only 1 valid attendance record that is empty
            LateAndPresentCombo[1] = 2;     // for 1 days => student can either be Late or Present hence 2
            LateAndPresentCombo[2] = 4;     // for 2 days => student can either be LL, PL, XP (x can be either L or P) hence total 4
            a[2] = 1;       // LL
            b[2] = 1;       // PL
            c[2] = 2;       // XP n.e. LP & PP

            for (int i = 3; i <= n; i++)
            {
                a[i] = b[i - 1];
                b[i] = c[i - 1];
                c[i] = (a[i - 1] + b[i - 1] + c[i - 1]) % mod;
                LateAndPresentCombo[i] = (a[i] + b[i] + c[i]) % mod;
            }

            // Now since we can have 2 states Absent == 0  or Absent == 1
            long ans = LateAndPresentCombo[n];      // Abscent == 0
            for (int i = 1; i <= n; i++)            // for Abscent == 1, try marking abscent at each possible day from Day-1 to Day-N
                // placing 'A' at n index and finding all valid combo's on left multiplied by valid combo's on right
                ans = ((LateAndPresentCombo[i - 1] * LateAndPresentCombo[n - i]) % mod + ans) % mod;

            return (int)ans;
        }


        // Time O(n) || Space O(1) || 71.61% Faster || 66.44% Efficient
        public static int ClimbStairs(int n)
        {
            int num1 = 1, num2 = 1, temp;
            for (int i = 2; i <= n; i++)
            {
                temp = num1 + num2;
                num1 = num2;
                num2 = temp;
            }
            return num2;
        }
        // Time O(n) || Space O(n) || 71.61% Faster || 11.82% Efficient
        public static int ClimbStairs_ExtraMemory(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = dp[1] = 1;
            for (int i = 2; i <= n; i++)
                dp[i] = dp[i - 1] + dp[i - 2];
            return dp[n];
        }


        // Time O(n) || Space O(n)
        public static IList<int> SplitIntoFibonacci(string s)
        {
            List<int> ans = new List<int>();
            return FindFibonacci(0) ? ans : new List<int>();

            // Local Func
            bool FindFibonacci(int i, int fn0 = -1, int fn1 = -1)
            {
                if (i == s.Length)
                {
                    if (ans.Count >= 3) return true;
                }
                else if (s[i] != '0')                          // if current digit is Zero than return false as we dont want any leading zeros
                {
                    int currNum = 0;
                    for (int k = i; k < s.Length; k++)
                    {
                        currNum = (currNum * 10) + s[k] - '0';
                        if (fn0 == -1 || currNum == fn0 + fn1)
                        {
                            ans.Add(currNum);
                            if (FindFibonacci(k + 1, fn1, currNum))     // if fibonnaci split found than return true, else continue searching
                                return true;
                            ans.RemoveAt(ans.Count - 1);
                        }
                        // current no is greater than sum of last 2 fibonnaci nums than exit out as adding more digits is only going to increase the currNum further
                        if (fn0 != -1 && currNum > fn0 + fn1)
                            break;
                    }
                }
                else if (fn0 == -1 || 0 == fn0 + fn1)           // only leading zero is allowed it the currNumber itself is 0
                {
                    ans.Add(0);
                    if (FindFibonacci(i + 1, fn1, 0)) return true;
                    ans.RemoveAt(ans.Count - 1);
                }
                return false;
            }
        }


        // Time = Space = O(n)
        public static int[] ShortestToChar(string s, char c)
        {
            int l = s.Length, closet = int.MaxValue;
            int[] closetLeft = new int[l], closetRight = new int[l];
            for (int i = 0; i < l; i++) // Find Closet from left
            {
                if (s[i] == c) closet = i;
                closetLeft[i] = Math.Abs(i - closet);
            }
            closet = int.MaxValue;
            for (int i = l - 1; i >= 0; i--) // Find Closet from right
            {
                if (s[i] == c) closet = i;
                closetRight[i] = Math.Abs(i - closet);
            }
            for (int i = 0; i < l; i++) // Ans is Math.Min of left & right
                closetLeft[i] = Math.Min(closetLeft[i], closetRight[i]);
            return closetLeft;
        }


        /// <summary>
        /// Time O(n^3) || Space O(n)
        /// The first two levels have breadth O(n) because all for loop iterations of the single recursive call result in a child recursive call.
        /// From the third level on, there is only one decision per recursive call, but we still have to visit the reminder of the string so the depth is O(n).
        /// In total the first two levels have O(n^2) nodes and each leaf has O(n) descendants so it's O(n^3)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsAdditiveNumber(string s)
        {
            return FindFibonacci(0);

            // Local Func
            bool FindFibonacci(int i, int count = 0, long fn0 = -1, long fn1 = -1)
            {
                if (i == s.Length)
                { if (count >= 3) return true; }
                else if (s[i] != '0')                          // if current digit is Zero than return false as we dont want any leading zeros
                {
                    long currNum = 0;
                    for (int k = i; k < s.Length; k++)
                    {
                        currNum = (currNum * 10) + s[k] - '0';
                        if (fn0 == -1 || currNum == fn0 + fn1)
                        {
                            if (FindFibonacci(k + 1, count + 1, fn1, currNum))     // if fibonnaci split found than return true, else continue searching
                                return true;
                        }
                        // current no is greater than sum of last 2 fibonnaci nums than exit out as adding more digits is only going to increase the currNum further
                        if (fn0 != -1 && currNum > fn0 + fn1)
                            break;
                    }
                }
                else if (fn0 == -1 || 0 == fn0 + fn1)           // only leading zero is allowed it the currNumber itself is 0
                { if (FindFibonacci(i + 1, count + 1, fn1, 0)) return true; }
                return false;
            }
        }


        public static bool CanIWin(int maxChoosableInteger, int desiredTotal)
        {
            if (desiredTotal <= 0) return true;

            int tSum = 0;
            //for (int n = 1; n <= maxChoosableInteger; n++) tSum += n;
            tSum = (1 + maxChoosableInteger) * maxChoosableInteger / 2;     // (n+1)*n/2
            if (tSum < desiredTotal) return false;      // if sum of all numbers still is less than desired than return false

            Dictionary<int, bool> cache = new Dictionary<int, bool>();
            return FirstToMoveWins(0, desiredTotal);

            // Local Func
            bool FirstToMoveWins(int choices, int remainingTotal)
            {
                if (cache.ContainsKey(choices)) return cache[choices];
                int bitMask = 0;
                for (int i = maxChoosableInteger; i >= 1; i--)
                {
                    // Mark ith bit ON to indicate ith number is being used, Ex: 100 in binary means num 3 is used, 10000 in binary means num 5 is used
                    bitMask = 1 << (i - 1);

                    if ((choices & bitMask) == 0)   // If Number 'n+1' is not already used
                    {
                        choices ^= bitMask;         // mark current number as used
                        remainingTotal -= i;        // decrease remaining total by currently choosen num

                        if (remainingTotal <= 0) return true;
                        // if 2nd player doesnt wins, than it means P1 wins using num 'n'
                        if (!FirstToMoveWins(choices, remainingTotal)) return true;

                        remainingTotal += i;        // reset remaining total
                        choices ^= bitMask;         // mark back current number as Not used
                    }
                }
                cache.Add(choices, false);
                return false;
            }

            #region 1st Attempt
            //bool[] choices = new bool[maxChoosableInteger + 1];
            //return FirstToMoveWins(choices, desiredTotal);

            //bool FirstToMoveWins(bool[] chooseFrom, int leftSum)
            //{
            //    if (leftSum <= 0)
            //        return true;
            //    for (int n = chooseFrom.Length - 1; n >= 0; n--)
            //        if (!chooseFrom[n])     // not already Used
            //        {
            //            chooseFrom[n] = true;          // mark as Used
            //            // if 2nd player doesnt wins, than it means P1 wins using num 'n'
            //            if (leftSum - n <= 0)
            //            {
            //                chooseFrom[n] = false;     // mark back as Not Used
            //                return true;
            //            }
            //            if (!FirstToMoveWins(chooseFrom, leftSum - n))
            //            {
            //                chooseFrom[n] = false;     // mark back as Not Used
            //                return true;
            //            }
            //            chooseFrom[n] = false;         // mark back as Not Used
            //        }
            //    return false;
            //}
            #endregion
        }



        // Time O(n) || Recursive Space O(h), n = no of nodes, h = depth of tree worstcase equal to 'n'
        public static TreeNode BstToGst(TreeNode r)
        {
            int greaterSum = 0;
            GetSum(r);
            return r;

            // Right->Root->Left Traversal
            void GetSum(TreeNode root)
            {
                if (root == null) return;
                GetSum(root.right);

                greaterSum += root.val;
                root.val = greaterSum;

                GetSum(root.left);
            }
        }


        // Time Complexity: O(k^{N-k}*k!), where N is the length of nums, and k is as given || Recursive Space O(n)
        public static bool CanPartitionKSubsets(int[] nums, int k)
        {
            int sum = 0; foreach (int n in nums) sum += n;

            if (sum % k > 0) return false;          // Can't divide total sum in equal 'k' parts return false

            int idx = nums.Length - 1, target = sum / k;
            Array.Sort(nums);                       // O(nlogn)
            if (nums[idx] > target) return false;   // biggest val is larger than target sum of each grp

            while (idx >= 0 && nums[idx] == target) // remove digits which == target & reduce no of 'k' grps by same amt
            {
                idx--;
                k--;
            }
            return bruteForceSearch(new int[k], idx);

            // Local Func
            bool bruteForceSearch(int[] grps, int i)
            {
                if (i < 0) return true;             // all values are already places succefully in 'k' grps
                int currNum = nums[i--];
                for (int j = 0; j < grps.Length; j++)
                {
                    if (grps[j] + currNum <= target)
                    {
                        grps[j] += currNum;         // add
                        if (bruteForceSearch(grps, i)) return true;
                        grps[j] -= currNum;         // remove back
                    }
                    // adding curr num to empty grp at index 'k' could not could return true ans
                    // hence adding to another any other other grp at diff index would also not yield in true
                    if (grps[j] == 0) break;        // skipping Zero's in grp
                }
                return false;
            }
        }


        // Time = Space = O(n), n is length of deliciousness array
        public static int CountPairs(int[] d)
        {
            Dictionary<int, long> numCount = new Dictionary<int, long>();
            for (int i = 0; i < d.Length; i++)
                if (!numCount.ContainsKey(d[i])) numCount.Add(d[i], 1);
                else numCount[d[i]]++;

            long goodMeal = 0;
            foreach (var kvp in numCount)
            {
                // maximum sum will be 2^20 + 2^20 = 2^21
                // iterate through all possible powers of two
                int power = 1;
                for (int i = 0; i < 22; i++)
                {
                    int target = power - kvp.Key;
                    if (numCount.ContainsKey(target))
                    {
                        if (kvp.Key != target)
                            goodMeal += kvp.Value * numCount[target];
                        else// (kvp.Key==secondNum)
                            goodMeal += kvp.Value * (kvp.Value - 1);
                    }
                    power = power << 1;     // sane as power*=2
                }
            }
            // since each pair is counted twice hence half the final value and mod by 10^9+7
            return (int)(goodMeal / 2 % 1000000007);
        }


        // Time O(nlogn) || Space O(n)
        public static int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            int l1 = arr1.Length, l2 = arr2.Length;
            if (l1 < 2) return arr1;
            if (l2 == 0) { Array.Sort(arr1); return arr1; }

            Dictionary<int, int> numCount = new Dictionary<int, int>();
            int idx = -1, fillAt = 0;
            while (++idx < l1)
                if (!numCount.ContainsKey(arr1[idx])) numCount.Add(arr1[idx], 1);
                else numCount[arr1[idx]]++;

            idx = -1;
            while (++idx < l2)
                if (numCount.ContainsKey(arr2[idx])) // found one of the distinct number from arr2 in arr1
                {
                    int times = numCount[arr2[idx]];
                    while (--times >= 0)               // add matched number at the next index in sorted array
                        arr1[fillAt++] = arr2[idx];
                    numCount.Remove(arr2[idx]);     // remove the number from the HashTable
                }
            int sortRemaingFrom = fillAt;
            foreach (var unMatchedNum in numCount)
            {
                int times = unMatchedNum.Value;
                while (--times >= 0)                   // add UnMatched number at the next index in sorted array
                    arr1[fillAt++] = unMatchedNum.Key;
            }
            Array.Sort(arr1, sortRemaingFrom, l1 - sortRemaingFrom);
            return arr1;
        }


        // Time O(n) || Space O(n), n = no of cells in grid
        public static int IslandPerimeter(int[][] grid)
        {
            /* Traverse thru each Cell inside GRID
             * if cell is land (value == 1)
             *      than set perimeter as 4 & now check for each valid adjacent cell which is also land decreament 1 from current cell perimeter
             *      after calculating above for all 4 direction add current cell perimieter to total perimeter
             *  if cell is water (value == 0)
             *      do nothing & return
             */
            int row = grid.Length;
            int col = grid[0].Length;
            int peri = 0;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    GetPerimeter(i, j);
            return peri;

            // Local Func
            void GetPerimeter(int r, int c)
            {
                if (grid[r][c] == 0) return;

                int perimeter = 4;
                // Up
                if (r - 1 >= 0 && grid[r - 1][c] == 1) perimeter--;

                // Down
                if (r + 1 < row && grid[r + 1][c] == 1) perimeter--;

                // Left
                if (c - 1 >= 0 && grid[r][c - 1] == 1) perimeter--;

                // Right
                if (c + 1 < col && grid[r][c + 1] == 1) perimeter--;

                peri += perimeter;
            }
        }


        // Time = Recursive Space = O(row*col)
        public static int MaxAreaOfIsland(int[][] grid)
        {
            int row = grid.Length, col = grid[0].Length, maxArea = 0;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    if (grid[i][j] == 1)
                        maxArea = Math.Max(maxArea, DFS(i, j));
            return maxArea;

            // Local Func
            int DFS(int r, int c)
            {
                if (r < 0 || r >= row || c < 0 || c >= col || grid[r][c] == 0) return 0;
                grid[r][c] = 0;   // mark visited
                return 1 + DFS(r - 1, c) + DFS(r + 1, c) + DFS(r, c - 1) + DFS(r, c + 1);
            }
        }


        // Time O(row*col) || Space O(1)
        public static int NumMagicSquaresInside(int[][] grid)
        {
            int row = grid.Length, col = grid[0].Length, magicSq = 0;
            if (row < 3 || col < 3) return magicSq;

            HashSet<int> s = new HashSet<int>(9);
            for (int r = 0; r < row - 2; r++)
                for (int c = 0; c < col - 2; c++)
                    if (IsMagicFaster(r, c))
                        magicSq++;
            return magicSq;

            // Local Func
            bool IsMagic(int i, int j)  // Time O(9) ~O(1)
            {
                s.Clear();
                for (int a = i; a < i + 3; a++)
                    for (int b = j; b < j + 3; b++)
                        if (s.Contains(grid[a][b]) || grid[a][b] > 9 || grid[a][b] < 1) return false;
                        else s.Add(grid[a][b]);
                int r1 = grid[i][j] + grid[i][j + 1] + grid[i][j + 2];
                int r2 = grid[i + 1][j] + grid[i + 1][j + 1] + grid[i + 1][j + 2];
                int r3 = grid[i + 2][j] + grid[i + 2][j + 1] + grid[i + 2][j + 2];
                int c1 = grid[i][j] + grid[i + 1][j] + grid[i + 2][j];
                int c2 = grid[i][j + 1] + grid[i + 1][j + 1] + grid[i + 2][j + 1];
                int c3 = grid[i][j + 2] + grid[i + 1][j + 2] + grid[i + 2][j + 2];
                int d1 = grid[i][j] + grid[i + 1][j + 1] + grid[i + 2][j + 2];
                int d2 = grid[i][j + 2] + grid[i + 1][j + 1] + grid[i + 2][j];
                return r1 == r2 && r2 == r3 && r3 == c1 && c1 == c2 && c2 == c3 && c3 == d1 && d1 == d2;
            }
            bool IsMagicFaster(int i, int j)  // Time O(9) ~O(1)
            {
                s.Clear();
                int r1, r2, r3, c1, c2, c3, d1, d2;
                r1 = r2 = r3 = c1 = c2 = c3 = d1 = d2 = 0;

                for (int a = i; a < i + 3; a++)
                    for (int b = j; b < j + 3; b++)
                        if (s.Contains(grid[a][b]) || grid[a][b] > 9 || grid[a][b] < 1) return false;
                        else
                        {
                            s.Add(grid[a][b]);  // add new value to HashSet

                            switch (a - i)      // for rows
                            {
                                case 0: r1 += grid[a][b]; break;
                                case 1: r2 += grid[a][b]; break;
                                case 2: r3 += grid[a][b]; break;
                            }
                            switch (b - j)      // for cols
                            {
                                case 0: c1 += grid[a][b]; break;
                                case 1: c2 += grid[a][b]; break;
                                case 2: c3 += grid[a][b]; break;
                            }
                            if (a - i == b - j)         // for 1st diagonal 
                                d1 += grid[a][b];
                            if ((b - j) + a - i == 2)   // for 2nd diagonal 
                                d2 += grid[a][b];
                        }
                return r1 == r2 && r2 == r3 && r3 == c1 && c1 == c2 && c2 == c3 && c3 == d1 && d1 == d2;
            }
        }


        // Time O(n) || Space O(1)
        public static int[] PrevPermOpt1(int[] arr)
        {
            int i = arr.Length - 1;
            while (--i >= 0)
                if (arr[i] > arr[i + 1]) // num on left is greater than one on rt
                {
                    int j = i, replaceWith = i + 1;
                    while (++j < arr.Length)
                    {
                        if (arr[i] <= arr[j]) break;
                        else if (arr[replaceWith] < arr[j]) replaceWith = j;
                    }
                    int t = arr[replaceWith];
                    arr[replaceWith] = arr[i];
                    arr[i] = t;
                    return arr;
                }
            return arr;
        }


        // Time = Space = O(N*(2^N)) || Cascading based approach
        public static IList<IList<int>> Subsets(int[] nums)
        {
            List<IList<int>> powerSet = new List<IList<int>>();
            powerSet.Add(new List<int>());          // add empty set
            foreach (int number in nums)            // O(n)
            {
                int start = 0, last = powerSet.Count - 1;
                while (start <= last)               // O(2^N)
                {
                    // create new subset from each existing subSet in powerSet
                    // now add current number and insert this new subSet back in powerSet
                    var newSet = powerSet[start].ToList();
                    newSet.Add(number);
                    powerSet.Add(newSet);
                    start++;
                }
            }
            return powerSet;
        }
        // Time = Space = O(N*(2^N)) || BackTracking based approach
        public static IList<IList<int>> Subsets_BackTrack(int[] nums)
        {
            List<IList<int>> powerSet = new List<IList<int>>();
            for (int l = 0; l <= nums.Length; l++)
                BackTrack(0, new List<int>(), l);
            return powerSet;

            // Local Func
            void BackTrack(int startFrom, List<int> subSet, int len)
            {
                if (subSet.Count == len)
                    powerSet.Add(subSet.ToArray());
                else
                    for (int idx = startFrom; idx < nums.Length; idx++)
                    {
                        subSet.Add(nums[idx]);              // add num
                        BackTrack(idx + 1, subSet, len);    // back recursive call to fill remaining numbers in subSet
                        subSet.RemoveAt(subSet.Count - 1);  // remove num while backtracking
                    }
            }
        }


        // Time = Space = O(N*(2^N)) || Lexicographic based approach
        public static IList<IList<int>> Subsets_LexicographicBinarySorted(int[] nums)
        {
            List<IList<int>> powerSet = new List<IList<int>>();
            int n = nums.Length;
            IList<int> currSet;
            // generate bitmask, from 0..00 to 1..11
            for (int i = (int)Math.Pow(2, n); i < (int)Math.Pow(2, n + 1); i++)
            {
                currSet = new List<int>();
                string bitMask = BinaryString(i);
                // Map a subset to each bitmask: 1 on the ith position in bitmask means the presence of nums[n] in the subset, and 0 means its absence.
                for (int j = 0; j < n; j++)
                    if (bitMask[j] == '1')
                        currSet.Add(nums[j]);
                powerSet.Add(currSet);
            }
            return powerSet;

            // Converts Integer into its equivalant binary representation
            string BinaryString(int number)
            {
                Stack<char> st = new Stack<char>();
                while (number > 0)
                {
                    st.Push((number & 1) == 1 ? '1' : '0');
                    number = number >> 1;
                }
                return new string(st.Reverse().ToArray());
            }
        }


        // Time = Space = O(row*col)
        public static int ShortestPathBinaryMatrix(int[][] grid)
        {
            int row = grid.Length, col = grid[0].Length;
            if (grid[0][0] == 1 || grid[row - 1][col - 1] == 1) return -1;   // if start or end is blocked

            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { 0, 0, 1 });
            grid[0][0] = 1;

            int r, c, dist;
            while (q.Count > 0)    // BFS Traversal
            {
                int[] currPos = q.Dequeue();
                r = currPos[0];
                c = currPos[1];
                dist = currPos[2];
                // reached destination
                if (r == row - 1 && c == col - 1) return dist;

                // spread in all 8 valid direction
                for (int i = r - 1; i <= r + 1; i++)
                    for (int j = c - 1; j <= c + 1; j++)
                        // if valid row & col index and cell is empty
                        if (i >= 0 && i < row && j >= 0 && j < col && grid[i][j] == 0)
                        {
                            grid[i][j] = 1;  // mark visited, so it is not picked up again
                            q.Enqueue(new int[] { i, j, dist + 1 });
                        }
            }
            return -1;
        }


        // Time O(n^2) || Space O(n)
        public static int FindNumberOfLIS(int[] nums)
        {
            int l = nums.Length, timeMaxLenFound = 0, maxLen = 1;
            int[,] dp = new int[l, 2];          // 1st row stores maxLen for each index and 2nd stores times its achievable

            for (int i = 0; i < l; i++)         // index for which we are updating maxLen
            {
                // base value for maxLen for curr index & times this maxLen for current index is achievable
                dp[i, 0] = dp[i, 1] = 1;
                for (int j = 0; j < i; j++)     // iterate thru each index from 0 to n-1 to find max len for n
                    if (nums[j] < nums[i])
                        if (dp[i, 0] < dp[j, 0] + 1)        // bigger maxLen found, update maxLen for index and time achievable
                        {
                            dp[i, 0] = dp[j, 0] + 1;
                            dp[i, 1] = dp[j, 1];
                        }
                        else if (dp[i, 0] == dp[j, 0] + 1)  // same maxLen found again update times achievable
                            dp[i, 1] += dp[j, 1];

                if (maxLen < dp[i, 0])
                {
                    maxLen = dp[i, 0];
                    timeMaxLenFound = dp[i, 1];
                }
                else if (maxLen == dp[i, 0])
                    timeMaxLenFound += dp[i, 1];
            }
            return timeMaxLenFound;
        }


        // Time O(n) || Space O(1)
        public static int LeastInterval(char[] tasks, int coolDownTime)
        {
            #region 1st Attempt
            /* Count & store the different task to be done along with their frequency
             * Now Sort them in Descending order as per their frequency
             * Add All tasks with more than 0 frequency in Queue and "cannot be completed Time value as 0"
             * 
             * Start the timer and increament it every time we complete an task or stay idle
             * 
             * Start picking task from front and if their 'cannot be picked up time' is less than current timer pick them
             * 
             * repeat above step till Queue is empty
             */

            //int l = tasks.Length, timer = 0;
            //if (n == 0) return l;
            //int[] charCount = new int[26];
            //for (int n = 0; n < l; n++) charCount[tasks[n] - 'A']--;
            //Array.Sort(charCount);      // sorted in descending order

            //Queue<int[]> q = new Queue<int[]>();
            //for (int n = 0; n < 26; n++)
            //    if (charCount[n] != 0)
            //        // 1st index indicates frequency of task and 2nd informs about time till it cannot be picked up next
            //        q.Enqueue(new int[] { -charCount[n], 0 });

            //while (q.Count > 0)
            //{
            //    var currTask = q.Dequeue();
            //    if (currTask[1] > timer)    // if current task cannot be picked time is greater than timer
            //        timer += currTask[1] - timer;   // CPU stays idle for the difference amt of time
            //    timer++;
            //    if (--currTask[0] > 0)      // currTask JobCount still greater than 0
            //    {
            //        currTask[1] = timer + n;    // update time till currTask cannot be picked up next
            //        q.Enqueue(currTask);
            //    }
            //}
            //return timer;
            #endregion

            #region 2nd Attempt
            /* The maximum number of tasks is 26. Let's allocate an array frequencies of 26 elements to keep the frequency of each task.
             * 
             * Iterate over the input array and store the frequency of task A at index 0, the frequency of task B at index 1, etc.
             * 
             * Sort the array and retrieve the maximum frequency f_max. This frequency defines the max possible idle time: idle_time = (f_max - 1) * n.
             * 
             * Pick the elements in the descending order one by one. At each step,
             * decrease the idle time by min(f_max - 1, f) where f is a current frequency. Remember, that idle_time is greater or equal to 0.
             * 
             * Return busy slots + idle slots: len(tasks) + idle_time.
             */
            //int l = tasks.Length;
            //if (coolDownTime == 0) return l;

            //// calculate frequencies of the tasks
            //int[] frequency = new int[26];
            //for (int n = 0; n < l; n++) frequency[tasks[n] - 'A']++;// O(n)

            //Array.Sort(frequency);                                  // O(26log26)

            //int maxFreqTask = frequency[25], maxIdleTime = (maxFreqTask - 1) * coolDownTime;

            //// not we try reducing idleTime by iterating over remaining tasks and see how many can be completed within coolDown time for maxFreqTask
            //for (int n = 24; n >= 0 && maxIdleTime > 0; n--)        // O(25)
            //    maxIdleTime -= Math.Min(maxFreqTask - 1, frequency[n]);

            //// idle time can be >= 0
            //return l + Math.Max(0, maxIdleTime);
            #endregion

            // Final Attempt
            int l = tasks.Length;
            if (coolDownTime == 0) return l;

            int maxFreq = 0, maxFreqIdx = -1;
            int[] taskFreq = new int[26];
            // calculate freq of each task and find maxFreq
            foreach (var t in tasks) // O(n)
                if (++taskFreq[t - 'A'] > maxFreq)
                {
                    maxFreq = taskFreq[t - 'A'];
                    maxFreqIdx = t - 'A';
                }

            // calculate total idle time
            int idleTime = (maxFreq - 1) * coolDownTime;

            // try reducing idle time
            for (int i = 0; i < 26; i++)  // O(24)
                if (i != maxFreqIdx)
                    idleTime -= Math.Min(maxFreq - 1, taskFreq[i]);

            return l + Math.Max(0, idleTime);
        }


        // Time O(V+E) || Space O(V), V = no of nodes & E = total no of edges
        public static bool IsBipartite(int[][] graph)
        {
            int v = graph.Length, parent;
            Stack<int> st = new Stack<int>();
            int[] color = new int[v];

            for (int i = 0; i < v; i++)     // traverse every node to cover case of disconnected graphs
                if (color[i] == 0)          // Current Node not Colored yet
                {
                    color[i] = 1;           // color every new un-colored node as 'blue'
                    st.Push(i);             // Push newNode to Stack
                    // Start DFS
                    while (st.Count > 0)
                    {
                        parent = st.Pop();
                        // traverse each adjacent node of current node
                        foreach (var adjacentNode in graph[parent])
                            // if adjacentNode not is not colored, assign opposite color to current node
                            if (color[adjacentNode] == 0)
                            {
                                st.Push(adjacentNode);  // add new node to Stack of nodees
                                color[adjacentNode] = color[parent] == 1 ? 2 : 1;
                            }
                            // else if adjacentNode has same color as parent return false
                            else if (color[adjacentNode] == color[parent])
                                return false;
                    }
                }
            return true;
        }


        // Time O(Max(nlogn,n*m) || Space O(n), n = no of rows & m = no of columns in matrix
        public static int[] KWeakestRows(int[][] mat, int k)
        {
            int l = mat.Length, sCount = 0;
            List<int[]> soldiersCount = new List<int[]>(l);
            for (int i = 0; i < l; i++)                // O(n)
            {
                sCount = 0;
                for (int j = 0; j < mat[i].Length; j++)// O(m)
                    if (mat[i][j] == 0) break;
                    else sCount++;
                // update index & soldiers count in List
                soldiersCount.Add(new int[] { i, sCount });
            }
            soldiersCount.Sort(new SoldierComparator());
            int[] ans = new int[k];
            for (int i = 0; i < k; i++)
                ans[i] = soldiersCount[i][0];
            return ans;
        }
        public class SoldierComparator : IComparer<int[]>
        {
            public int Compare(int[] x, int[] y)
            {
                if (x[1] != y[1])   // case when soldier count is diff, sort by soldier count
                    return x[1].CompareTo(y[1]);
                else                // if count is same order by index
                    return x[0].CompareTo(y[0]);
            }
        }


        // Time O(rows*cols) || Space O(row)
        public static int SmallestCommonElement(int[][] mat)
        {
            Dictionary<int, int> numFreq = new Dictionary<int, int>(mat[0].Length);
            foreach (var num in mat[0])
                numFreq.Add(num, 1);

            // Update the Frequency of common elements from each row
            for (int i = 1; i < mat.Length; i++)
                foreach (var num in mat[i])
                    if (numFreq.ContainsKey(num))
                        numFreq[num]++;

            int result = int.MaxValue;
            // find all nums whose frequency is equal to no of rows and update result with Minimun of all
            foreach (var kvp in numFreq)
                if (kvp.Value == mat.Length)
                    result = Math.Min(result, kvp.Key);
            return result != int.MaxValue ? result : -1;
        }


        // Time = Space = O(n)
        public static int LongestPalindrome(string s)
        {
            /* Get the frequency of all unique letters in string
             *  Now for each even frequency letter add its count to answer
             * 
             *  and for each odd frequency letter add its count-1 to answer,
             *  as keeping 1 aside all other count can be used to make even larger palindrome
             *  also set oddValue to 1 if even a single letter with odd count is encountered as we can atleast use 1 odd value in middle
             *  
             * return ans+oddValue
             */
            Dictionary<char, int> ht = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
                if (!ht.ContainsKey(s[i])) ht.Add(s[i], 1);
                else ht[s[i]]++;

            int longestPalindrome = 0, oddValueLetter = 0;
            foreach (var count in ht.Values)
                if (count % 2 != 0)  // odd count
                {
                    oddValueLetter = 1;
                    longestPalindrome += count - 1;
                }
                else                // even count
                    longestPalindrome += count;
            return longestPalindrome + oddValueLetter;
        }


        // Time O(n) || Space O(n)
        public static int FindMaxLength(int[] nums)
        {
            int balance = 0, maxLen = 0;
            Dictionary<int, int> ht = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                // we add 1 to balance if curr number is 1 else we decrease by 1
                balance += nums[i] == 1 ? 1 : -1;
                if (balance == 0)
                    maxLen = Math.Max(maxLen, i + 1);
                else if (ht.ContainsKey(balance))
                    maxLen = Math.Max(maxLen, i - ht[balance]);
                else
                    ht.Add(balance, i);
            }
            return maxLen;
        }


        // Time = Space = O(n)
        public static int MaxSubArrayLen(int[] nums, int k)
        {
            Dictionary<int, int> sumAtIndex = new Dictionary<int, int>();
            int sum = 0, maxLen = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum == k)
                    maxLen = Math.Max(maxLen, i + 1);
                else if (sumAtIndex.ContainsKey(sum - k))
                    maxLen = Math.Max(maxLen, i - sumAtIndex[sum - k]);

                if (!sumAtIndex.ContainsKey(sum))
                    sumAtIndex.Add(sum, i);
            }
            return maxLen;
        }


        // Time O(n) || Space O(1)
        public static int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            /* Using the example nums = [10,5,2,6]:
             * 
             * If we start at the 0th index, [10,5,2,6], the number of intervals is obviously 1.
             * If we move to the 1st index, the window is now [10,5,2,6]. The new intervals created are [5] and [10,5], so we add 2.
             * Now, expand the window to the 2nd index: [10,5,2,6]. The new intervals are [2], [5,2], and [10,5,2], so we add 3.
             * The pattern should be obvious by now; we add right - left + 1 to the output variable every loop.
             */
            if (k <= 1) return 0;
            int product = 1, right = -1, left = 0, subArrCount = 0;
            while (++right < nums.Length)
            {
                product *= nums[right];
                while (left < nums.Length && product >= k)  // keep dividing the value by nums[left] till product is not less than 'K'
                    product /= nums[left++];
                subArrCount += right - left + 1;
            }
            return subArrCount;
        }


        // Time O(n) || Space O(n)
        public static int SubArraySumEqualsK(int[] nums, int k)
        {
            /* 
             * Algo: If the cumulative sum(represented by sum[n] for sum up to ith index) is same as some sum[j]
             * the sum of the elements lying in between those indices is zero. 
             * 
             * Extending the same thought further, 
             * if the cumulative sum up to two indices, say n & j is at a difference of k n.e. if sum[n]−sum[j]=k,
             * the sum of elements lying between indices n & j is k.
             * 
             * Based on this, we make use of a hashmap mapmap which is used to store the cumulative sum up to all the indices possible
             * along with the number of times the same sum occurs.
             * 
             * We store the data in the form: (sum_i, no. of occurrences of sum_i).
             * 
             * We traverse over the array nums and keep on finding the cumulative sum.
             * 
             * Every time we encounter a new sum, we make a new entry in the hashmap corresponding to that sum.
             * If the same sum occurs again, we increment the count by 1
             */
            Dictionary<int, int> uniqueSumFrequency = new Dictionary<int, int>();
            uniqueSumFrequency.Add(0, 1);       // base condition
            int totalSum = 0, count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                totalSum += nums[i];
                if (uniqueSumFrequency.ContainsKey(totalSum - k))
                    count += uniqueSumFrequency[totalSum - k];

                if (!uniqueSumFrequency.ContainsKey(totalSum)) uniqueSumFrequency.Add(totalSum, 1);
                else uniqueSumFrequency[totalSum]++;
            }
            return count;
        }


        // Time O(n) || Space O(n)
        public static int SubarraysDivByK(int[] nums, int k)
        {
            /* Intuition
             * As is typical with problems involving subarrays, we use prefix sums to add each subarray.
             * Let P[n+1] = A[0] + A[1] + ... + A[n]. Then, each subarray can be written as P[j] - P[n] (for j > n).
             * Thus, we have P[j] - P[n] equal to 0 modulo K, or equivalently P[n] and P[j] are the same value modulo K.
             * 
             * Algorithm
             * Count all the P[n]'s modulo K.
             * For example, take A = [4,5,0,-2,-3,1] and K = 5.
             * Then P = [0,4,9,9,7,4,5], and C_0 = 2, C_2 = 1, C_4 = 4
             * With C_0 = 2 (at P[0], P[6]), we get no of pairs using formula => n*(n-1)/2
             * subarray with sum divisible by K, namely A[0:6] = [4, 5, 0, -2, -3, 1].
             * 
             * With C_4 = 4 (at P[1], P[2], P[3], P[5]) , using formula => n*(n-1)/2 we get 6 subarrays which can be observed as below
             * subarrays with sum divisible by K, namely A[1:2], A[1:3], A[1:5], A[2:3], A[2:5], A[3:5].
             */
            int l = nums.Length;
            // get preFixSum
            int[] p = new int[l + 1];
            for (int i = 0; i < l; i++)
                p[i + 1] = p[i] + nums[i];

            // get mod value for each PrefixSum
            int[] modK = new int[k];
            foreach (var prefixSum in p)
                modK[(prefixSum % k + k) % k]++;    // we add l again after 1st mod to compensate for -ve values

            int ans = 0;
            foreach (var modValue in modK)
                ans += modValue * (modValue - 1) / 2;
            return ans;
        }


        // Time = O((2^N)*N) || Space = O(n), since the decision tree has 2 option at every level/index we encounter an alphabet
        public static IList<string> LetterCasePermutation(string S)
        {
            List<string> ans = new List<string>();
            int l = S.Length;
            S = S.ToLower();
            GetTransformation(0, new char[l]);
            return ans;

            // Local Recursive Func
            void GetTransformation(int idx, char[] arr)
            {
                if (idx == l)  // entire word is formed
                    ans.Add(new string(arr));
                else if (Char.IsDigit(S[idx]))
                {
                    arr[idx] = S[idx];
                    GetTransformation(idx + 1, arr);
                }
                else
                {
                    // once with Smallcase
                    arr[idx] = S[idx];
                    GetTransformation(idx + 1, arr);
                    // once with Uppercase
                    arr[idx] = (char)((S[idx] - 'a') + 'A');
                    GetTransformation(idx + 1, arr);
                }
            }
        }


        // Time = Space = O(n^2), n = length of height
        public static int ContainerWithMostWater_DP(int[] height)
        {
            int l = height.Length, minH = 0;
            int[,] dp = new int[l, l];
            return GetMax(0, l - 1);
            // local func
            int GetMax(int left, int right)
            {
                if (left > right || left == right)
                    return 0;
                if (dp[left, right] != 0)
                    return dp[left, right];

                minH = Math.Min(height[left], height[right]);
                return dp[left, right] = Math.Max(minH * (right - left),
                                                    Math.Max(GetMax(left + 1, right),
                                                                GetMax(left, right - 1)
                                                            )
                                                 );
            }
        }
        // Time = O(n) || Space = O(1), n = length of height
        public static int ContainerWithMostWater_TwoPointer(int[] height)
        {
            int left = 0, right = height.Length - 1, maxCapacity = 0;
            while (left < right)
            {
                maxCapacity = Math.Max(maxCapacity, (right - left) * Math.Min(height[left], height[right]));
                // We move left pointer forwd if its the one with lessor height
                if (height[left] < height[right]) left++;
                // else we move right pointer
                else right--;
            }
            return maxCapacity;
        }


        // Time O(nlogn) || Space O(k)
        public static int[] GetKStrongestValues(int[] arr, int k)
        {
            // sort to find the median
            Array.Sort(arr);

            // sort array again as mentioned in problem
            /* A value arr[n] is said to be stronger than a value arr[j] if |arr[n] - m| > |arr[j] - m| where m is the median of the array.
             * If |arr[n] - m| == |arr[j] - m|, then arr[n] is said to be stronger than arr[j] if arr[n] > arr[j].
             * 
             * Median is the middle value in an ordered integer list. More formally,
             * if the length of the list is n, the median is the element in position ((n - 1) / 2) in the sorted list (0-indexed).
             * 
             * For arr = [6, -3, 7, 2, 11], n = 5 and the median is obtained by sorting the array arr = [-3, 2, 6, 7, 11] &
             * the median is arr[m] where m = ((5 - 1) / 2) = 2. The median is 6.
             * 
             * For arr = [-7, 22, 17, 3], n = 4 and the median is obtained by sorting the array arr = [-7, 3, 17, 22] &
             * the median is arr[m] where m = ((4 - 1) / 2) = 1. The median is 3.
             */
            Array.Sort(arr, new StrongComparator(arr[(arr.Length - 1) / 2]));

            int[] kStrong = new int[k];
            for (int i = 0; i < k; i++) kStrong[i] = arr[i];
            return kStrong;
        }
        public class StrongComparator : IComparer<int>
        {
            int m, a1, b1;
            public StrongComparator(int median) => m = median;
            public int Compare(int a, int b)
            {
                a1 = Math.Abs(a - m);
                b1 = Math.Abs(b - m);
                if (a1 != b1) return a1 > b1 ? -1 : 1;
                else return a > b ? -1 : 1;
            }
        }


        // Time = Space = O(2^n), n = total no of bits
        public static IList<int> GrayCode(int totalBits)
        {
            List<int> grayCode = new List<int> { 0, 1 };
            int ithBit = 1, lastIdx;
            while (ithBit < totalBits)
            {
                lastIdx = grayCode.Count - 1;
                // turn on the leftMost 1 bit for all the pre existing no's to get list of no's with 1 additional bit
                for (int i = lastIdx; i >= 0; i--)
                    grayCode.Add(grayCode[i] + (1 << ithBit));
                ithBit++;
            }
            return grayCode;
        }


        // Time = Space = O(n^2)
        public static int NumberOfArithmeticSlices_DP(int[] A)
        {
            /* Since we need to count Arithmetic Sequence of length = 3 or more
             * Brute Force soln is simply finding all sub-arrays in O(n^2) & checking each if its Arithmetic seq or not in O(n),
             * hence Brute force total Time complexicity is O(n^2) || Space O(1)
             * 
             * Its clear in above algo that we would be verifying multiple subarrays again and again un-neccessarily
             * Hence we can optimize this by saving state of an sub-array in cahe and then build the soln from bottoms-up
             * 
             * n.e. first mark all 1 & 2 length arrays as valid sequence,
             * now for each sequence of 3 of more we only have to check if the diff b/w 1st 2 elements is same as diff b/w 2nd & 3rd element
             * and sub-array from 2nd element to last is valid sequence, if so we increament counter by 1
             */
            int l = A.Length, count = 0, ArithmeticSeq = 0;
            bool[,] dp = new bool[l, l];
            for (int len = 1; len <= l; len++)
                for (int start = 0; start <= l - len; start++)
                {
                    ArithmeticSeq = 0;
                    int last = start + len - 1;
                    if (start == last) dp[start, last] = true;
                    else if (start + 1 == last) dp[start, last] = true;
                    else if (A[start + 1] - A[start] == A[start + 2] - A[start + 1] && dp[start + 1, last])
                    {
                        dp[start, last] = true;
                        count++;
                    }
                }
            return count;
        }
        // Time = O(n) || Space = O(1)
        public static int NumberOfArithmeticSlices_Faster(int[] A)
        {
            /* we can have 1-D dp array which stores and no of valid arthimetic sequence till given index,
             * we start from 3rd element in array (we are only counting sequences of length >=3)
             * now for each new element we check if its different with last is same as the diff b/w last and last to last element, if so
             * we know adding this new element to existing sequences would return all prv valid sequences again + 1 more as last 2 elements n this new num forms additional seq
             * 
             * hence we update count of sequences at this index n as dp[n] = 1+dp[n-1]
             * also we increament counter by value of dp[n]
             * 
             * QuickNote: instead of using O(n) space we can achieve same result by just using an variable to store last dp[n-1] value
             * and when adding new number breaks sequence we update this dp value to '0'
             */
            int noOfArthimeticSequence = 0, count = 0;
            for (int i = 2; i < A.Length; i++)
                if (A[i] - A[i - 1] == A[i - 1] - A[i - 2])
                    count += ++noOfArthimeticSequence;
                else
                    noOfArthimeticSequence = 0;
            return count;
        }


        // Time = Space = O(n)
        public static IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        {
            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>(pid.Count);
            for (int i = 0; i < pid.Count; i++)
            {
                if (!graph.ContainsKey(ppid[i])) graph.Add(ppid[i], new List<int>() { pid[i] });
                else graph[ppid[i]].Add(pid[i]);

                if (!graph.ContainsKey(pid[i])) graph.Add(pid[i], new List<int>());
            }

            IList<int> ans = new List<int>();
            DFS(kill);
            return ans;

            // Local Func
            void DFS(int p)
            {
                ans.Add(p);
                foreach (var childProcess in graph[p])
                    DFS(childProcess);
            }
        }

        // Time = Recursive Space = O(row*col)
        public static int NumDistinctIslands(int[][] grid)
        {
            int rows = grid.Length, cols = grid[0].Length, maxR, maxC, r, c;
            HashSet<string> types = new HashSet<string>();  // to store uniqye island shapes found so far
            StringBuilder sb = new StringBuilder(); // to create the map of the shape of the island
            for (r = 0; r < rows; r++)
                for (c = 0; c < cols; c++)
                    if (grid[r][c] == 1)
                    {
                        maxR = r;
                        maxC = c;
                        sb.Append('S'); // to mark the start of mapping island
                        GetUniqueIsland(r, c);
                        types.Add(sb.ToString());
                        sb.Clear();
                    }
            return types.Count;

            // Local Func
            bool GetUniqueIsland(int i, int j)
            {
                if (i < 0 || i >= rows || j < 0 || j >= cols || grid[i][j] == 0) return false;
                grid[i][j] = 0;   // mark current cell visited

                // We map the shape of the Island by add the direction we got the next piece of land from and its relative distance from the starting cell

                // Direction 'R'
                if (GetUniqueIsland(i, j + 1)) sb.Append("R" + (i - r) + (j - c));
                // Direction 'D'
                if (GetUniqueIsland(i + 1, j)) sb.Append("D" + (i - r) + (j - c));
                // Direction 'L'
                if (GetUniqueIsland(i, j - 1)) sb.Append("L" + (i - r) + (j - c));
                // Direction 'U'
                if (GetUniqueIsland(i - 1, j)) sb.Append("U" + (i - r) + (j - c));

                return true;
            }
        }


        // Time O(l*height*width), l = length of array nuts || Space O(height, width)
        public static int SquirrelSimulation_BFS(int height, int width, int[] tree, int[] squirrel, int[][] nuts)
        {
            /* Here we have just 1 squirrel & 1 Tree and 1 or more nuts and none of the position overlap
             * 1st Goal is to find closet nuts from squirrel since we can only collect one nut at a time.
             * next find the shortest distance for each of the nut to the tree.
             * 
             * Now we simply return distTravelBySquirrelToReachClosetNut + distances from 1st nut to tree + (2 * Sum of all min distances from remaining nuts to tree)
             * Here we double the distance for remaining nuts except 1st as we also need to travel back to the tree to deposit the nut before collecting any other nut
             */
            int closetNutFromSquirrel = 0, distancesOffirstNutTotree = 0, l = nuts.Length, totalDist = 0;
            Dictionary<int, int> nutsDistanceToTree = new Dictionary<int, int>(l);

            bool[,] addedToQueue;
            int[,] grid = new int[height, width];
            Queue<int[]> q = new Queue<int[]>();
            int[] closetTreeDist = new int[l];
            for (int i = 0; i < l; i++)
            {
                totalDist += closetTreeDist[i] = GetMinDistanceToTree(nuts[i]);
                grid[nuts[i][0], nuts[i][1]] = 1;        // mark presense of nut
            }

            int closetSquirreRow, closetSquirreCol;
            closetNutFromSquirrel = FindClosetNutFromSquirrel();
            distancesOffirstNutTotree = GetFromClosetTreeDist();

            return closetNutFromSquirrel + (2 * totalDist - distancesOffirstNutTotree);

            // Local func
            int GetMinDistanceToTree(int[] nutPos)
            {
                addedToQueue = new bool[height, width];      // to keep track of visited cells
                q.Clear();
                q.Enqueue(new int[] { nutPos[0], nutPos[1], 0 });
                addedToQueue[nutPos[0], nutPos[1]] = true;   // mark current cell as visited
                int r, c, dist;
                int[] currPos;
                while (true)
                {
                    currPos = q.Dequeue();
                    r = currPos[0];
                    c = currPos[1];
                    dist = currPos[2];
                    if (r == tree[0] && c == tree[1])       // found tree
                        return dist;

                    // Search for Tree in all Four Direction

                    // up
                    if (r - 1 >= 0 && !addedToQueue[r - 1, c])
                    {
                        q.Enqueue(new int[] { r - 1, c, dist + 1 });
                        addedToQueue[r - 1, c] = true;
                    }
                    // down
                    if (r + 1 < height && !addedToQueue[r + 1, c])
                    {
                        q.Enqueue(new int[] { r + 1, c, dist + 1 });
                        addedToQueue[r + 1, c] = true;
                    }
                    // left
                    if (c - 1 >= 0 && !addedToQueue[r, c - 1])
                    {
                        q.Enqueue(new int[] { r, c - 1, dist + 1 });
                        addedToQueue[r, c - 1] = true;
                    }
                    // right
                    if (c + 1 < width && !addedToQueue[r, c + 1])
                    {
                        q.Enqueue(new int[] { r, c + 1, dist + 1 });
                        addedToQueue[r, c + 1] = true;
                    }
                }

            }

            int FindClosetNutFromSquirrel()
            {
                q.Clear();
                q.Enqueue(new int[] { squirrel[0], squirrel[1], 0 });
                addedToQueue = new bool[height, width];      // to keep track of visited cells
                addedToQueue[squirrel[0], squirrel[1]] = true;// mark current cell as visited
                int r, c, dist;
                int[] currPos;
                while (true)
                {
                    currPos = q.Dequeue();
                    r = currPos[0];
                    c = currPos[1];
                    dist = currPos[2];
                    if (grid[r, c] == 1)                    // found nut
                    {
                        closetSquirreRow = r;
                        closetSquirreCol = c;
                        return dist;
                    }

                    // Search for Nuts in all Four Direction

                    // up
                    if (r - 1 >= 0 && !addedToQueue[r - 1, c])
                    {
                        q.Enqueue(new int[] { r - 1, c, dist + 1 });
                        addedToQueue[r - 1, c] = true;
                    }
                    // down
                    if (r + 1 < height && !addedToQueue[r + 1, c])
                    {
                        q.Enqueue(new int[] { r + 1, c, dist + 1 });
                        addedToQueue[r + 1, c] = true;
                    }
                    // left
                    if (c - 1 >= 0 && !addedToQueue[r, c - 1])
                    {
                        q.Enqueue(new int[] { r, c - 1, dist + 1 });
                        addedToQueue[r, c - 1] = true;
                    }
                    // right
                    if (c + 1 < width && !addedToQueue[r, c + 1])
                    {
                        q.Enqueue(new int[] { r, c + 1, dist + 1 });
                        addedToQueue[r, c + 1] = true;
                    }
                }
            }

            int GetFromClosetTreeDist()
            {
                for (int i = 0; i < l; i++)
                    if (nuts[i][0] == closetSquirreRow && nuts[i][1] == closetSquirreCol)
                        return closetTreeDist[i];
                return 0;
            }
        }
        // Time O(n) || Space O(1), n = length of array nuts
        public static int SquirrelSimulation(int height, int width, int[] tree, int[] squirrel, int[][] nuts)
        {
            /* the distance between any two points(tree, squirrel, nut) is given by the absolute difference b/w
             * the corresponding x-coordinates and the corresponding y-coordinates.
             * 
             * Now, in order to determine the required minimum distance, we need to observe a few points. Firstly,
             * the order in which the nuts are picked doesn't affect the final result, except one of the nuts which needs to be visited first from the squirrel's starting position. For the rest of the nuts, it is mandatory to go from the tree to the nut and then come back as well.
             * 
             * For the first visited nut, the saving obtained, given by dd, is the difference between the distance b/w
             * the tree and the current nut & the distance between the current nut and the squirrel. 
             * This is because for this nut, we need not travel from the tree to the nut,
             * but need to travel an additional distance from the squirrel's original position to the nut.
             * 
             * While traversing over the nutsnuts array and adding the to-and-fro distance,
             * we find out the saving, dd, which can be obtained if the squirrel goes to the current nut first. Out of all the nuts, we find out the nut which maximizes the saving and then deduct this maximum saving from the sum total of the to-and-fro distance of all the nuts.
             * 
             * Note that the first nut to be picked needs not necessarily be the nut closest to the squirrel's start point,
             * but it's the one which maximizes the savings.
             */
            int totalDistance = 0, d = int.MinValue;
            for (int i = 0; i < nuts.Length; i++)
            {
                totalDistance += GetDistance(nuts[i], tree);
                d = Math.Max(d, GetDistance(nuts[i], tree) - GetDistance(nuts[i], squirrel));
            }
            return (totalDistance * 2) - d;
            // Local Func
            int GetDistance(int[] start, int[] destination) => Math.Abs(start[0] - destination[0]) + Math.Abs(start[1] - destination[1]);
        }


        // Time = Space = O(n), 3 pass solution
        public static string MinRemoveToMakeValid_Stack_HashSet(string s)
        {
            /* Keep Inserting indices of the in stack for every open bracket encountered,
             * and remove one from list wheneve closing bracket is encountered,
             * 
             * but if stack is empty then add closing bracking index to DontInclude HashSet
             * at end if any open bracket our remaining add them to DontInclude set as well
             * 
             * Now append all character of input in a StringBuilder, which are not in 'DontInclude' set
             * and return sb.ToString()
             */
            Stack<int> st = new Stack<int>();
            HashSet<int> dontInclude = new HashSet<int>();
            for (int i = 0; i < s.Length; i++)          // O(n)
                if (s[i] == '(') st.Push(i);
                else if (s[i] == ')')
                    if (st.Count > 0) st.Pop();
                    else dontInclude.Add(i);

            while (st.Count > 0) dontInclude.Add(st.Pop());

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)          // O(n)
                if (!dontInclude.Contains(i))
                    sb.Append(s[i]);

            return sb.ToString();                       // O(n)
        }
        // Time = Space = O(n), 3 pass solution
        public static string MinRemoveToMakeValid_List(string s)
        {
            /* Set a balancer counter increament it by 1 when open bracket is encountered,
             * and decreament if by 1 when closing is encountered
             * 
             * Start traversing from Left-to-Right
             * Keep inserting the characters all along except when balance becomes -ve,
             * and reset the balance to 0
             * 
             * Now repeat the above operation when traversing from Right-To-Left
             */
            List<char> ls = new List<char>();
            int balance = 0;

            // Left to Right
            for (int i = 0; i < s.Length; i++)          // O(n)
            {
                if (s[i] == ')' && --balance < 0)
                {
                    balance = 0;
                    continue;
                }
                else if (s[i] == '(') balance++;
                ls.Add(s[i]);
            }

            if (balance == 0)
                return new string(ls.ToArray());

            // Right to Left
            balance = 0;
            Stack<char> ans = new Stack<char>();
            for (int i = ls.Count - 1; i >= 0; i--)     // O(n)
            {
                if (ls[i] == '(' && --balance < 0)
                {
                    balance = 0;
                    continue;
                }
                else if (ls[i] == ')') balance++;
                ans.Push(ls[i]);
            }

            return new string(ans.ToArray());           // O(n)
        }


        // Time = O(K*Min(N,2^K)) || Space = O(2^K), K = number of cells(n.e. 8) & N = number of steps
        public static int[] PrisonAfterNDays(int[] cells, int N)
        {
            /* once we can find out the anser using Brute force,
             * we can think how can we speed this up?
             * Patterns likely repeat, hence cache them.
             * If the patterns repeat and we know at what index it does,
             * we can work out where the answer is by calculating how far we are from N when the repetition occurs.
             */
            Dictionary<int, int> cachedState = new Dictionary<int, int>();
            int l = cells.Length, idx, nextState;
            int currState = GetCurrState(cells);
            while (N > 0)
            {
                if (!cachedState.ContainsKey(currState))        // new state found
                    cachedState.Add(currState, N--);
                else
                {
                    // find out after how many steps current state was repeated
                    int cycleLength = cachedState[currState] - N;
                    N %= cycleLength;
                    while (N-- > 0)
                        currState = GetNextState(currState);
                    break;
                }
                currState = GetNextState(currState);
            }
            return ConvertCurrState(currState);

            // Local Func
            int GetCurrState(int[] c)
            {
                nextState = 0;
                for (idx = 0; idx < l; idx++)
                    nextState |= c[idx] << ((l - idx) - 1);     // applying 'OR' operator to iThBit while reading from first to last
                return nextState;
            }
            int GetNextState(int c)
            {
                nextState = 0;
                int leftCell, rtCell;
                for (idx = 1; idx < l - 1; idx++)
                {
                    rtCell = (c & (1 << (idx - 1)));
                    leftCell = c & (1 << (idx + 1));
                    // Either Both left & Right cells are empty or both are occupied
                    if ((leftCell > 0 && rtCell > 0) || (leftCell == 0 && rtCell == 0))
                        nextState |= 1 << idx;
                }

                return nextState;
            }
            int[] ConvertCurrState(int s)
            {
                for (idx = 0; idx < l; idx++)
                    cells[idx] = (s & 1 << l - (idx + 1)) > 0 ? 1 : 0;
                ////Another Method by applying AND operator with every bit position
                //for (idx = l - 1; idx >= 0; idx--)
                //{
                //    cells[idx] = s & 1;
                //    s >>= 1;        // right shift all bits by one
                //}
                return cells;
            }
        }


        // Time O(n) || Space O(1)
        public static int MinFlipsMonoIncr(string S)
        {
            #region FAILED 1st ATTEMPT 72nd of 81 cases
            //int zerosAfterFirst1 = 0, ones = 0, n = 0;
            //while (n < S.Length && S[n] == '0')   // skip all starting zero's
            //    n++;
            //while (n < S.Length)
            //    if (S[n++] == '1')
            //        ones++;
            //    else
            //        zerosAfterFirst1++;

            //if (zerosAfterFirst1 <= 0)     // string is already monotone increasing.
            //    return zerosAfterFirst1;
            //else                        // we encountered some zero's after 1st one.
            //{
            //    n = S.Length - 1;
            //    while (n >= 0 && S[n--] == '1')
            //        ones--;
            //    return Math.Min(zerosAfterFirst1, ones);
            //}
            #endregion

            /* 
             * Basically we go through string and found out how much 1 before index much be flipped to 0
             * plus how many 0's after index need to be flipped to 1 to make monotone increasing sequence.
             * 
             * Add them up and get min for result
             * 
             * For example, with S = "010110": we have P = [0, 0, 1, 1, 2, 3, 3]. Now say we want to evaluate having x=3 zeros.
             * There are P[3] = 1 ones in the first 3 characters, and P[6] - P[3] = 2 ones in the later N-x = 3 characters.
             * So, there is (N-x) - (P[N] - P[x]) = 1 zero in the later N-x characters.
             * We take the minimum among all candidate answers to arrive at the final answer.
             */
            int totalOnes = 0, l = S.Length, onesOnLeft = 0, onesOnRight, elementsOnRtOfCurrentIdx = 0;
            for (int i = 0; i < l; i++)
                if (S[i] == '1')
                    totalOnes++;

            // inititate 'minFlips' with case when we flip all '1' to '0' on right to make monotone increasing sequence
            int minFlips = l - totalOnes;
            for (int i = 0; i < l; i++)
            {
                if (S[i] == '1') onesOnLeft++;
                minFlips = Math.Min(minFlips, onesOnLeft + (l - i - 1) - (totalOnes - onesOnLeft));

                // Above expression can be expanded as below for better understanding
                //elementsOnRtOfCurrentIdx = (l - n - 1);
                //onesOnRight = totalOnes - onesOnLeft;
                //minFlips = Math.Min(minFlips, onesOnLeft + elementsOnRtOfCurrentIdx - onesOnRight);
            }
            return minFlips;
        }


        // Time O(logY) || Space O(1)
        public static int BrokenCalculator(int X, int Y)
        {
            #region 1st ATTEMPT, fails at x = 1, y = 1000000000, O/P should be 39
            //if (X >= Y) return X - Y;
            //long steps = 0, doublingEffort = 0, reducingEffort;
            //while (X != Y)
            //{
            //    doublingEffort = Math.Abs(Y - X * 2);
            //    reducingEffort = Math.Abs(Y - (X - 1) * 2);
            //    if (doublingEffort <= reducingEffort)
            //        X *= 2;
            //    else
            //        X--;
            //    steps++;
            //}
            //return (int)steps;
            #endregion 

            /* Instead of multiplying by 2 or subtracting 1 from X, we could divide by 2 (when Y is even) or add 1 to Y.
             * 
             * The motivation for this is that it turns out we always greedily divide by 2:
             * If say Y is even,
             *      then if we perform 2 additions and one division,
             *      we could instead perform one division & one addition for less operations [(Y+2) / 2 vs Y/2 + 1].
             * If say Y is odd,
             *      then if we perform 3 additions and one division,
             *      we could instead perform 1 addition, 1 division, and 1 addition for less operations [(Y+3) / 2 vs (Y+1) / 2 + 1].
             * 
             * While Y is larger than X, add 1 if it is odd, else divide by 2.
             * Afterwards, we need to do X - Y additions to reach X.
             */
            int steps = 0;
            while (Y > X)
            {
                if (Y % 2 == 0)     // if 'Y' is even divided in half
                    Y /= 2;
                else                // else add 1 to 'Y'
                    Y++;
                steps++;
            }
            return (X - Y) + steps;
        }


        // Time O(sq root n) || Space O(1)
        public static int TwoStepsKeyboard(int n)
        {
            int minSteps = 0, primeNo = 2;
            while (n > 1)
            {
                while (n % primeNo == 0)
                {
                    n /= primeNo;
                    minSteps += primeNo;
                }
                primeNo++;
            }
            return minSteps;
        }


        // Time O(Max(n*l)) || Space O(l), n is length of dictionary 'd' & l is avg length of words in dictionary
        public static string FindLongestWord(string s, IList<string> d)
        {
            string ans = "";
            foreach (var word in d)
            {
                int i, j = 0;
                // check if current word is subsequence of 's'
                for (i = 0; i < s.Length && j < word.Length; i++)
                    if (s[i] == word[j]) j++;

                if (j == word.Length)
                    if (word.Length > ans.Length)           // new match is longer
                        ans = word;
                    else if (word.Length == ans.Length)     // new match of same length comes lexographically earlier
                        ans = word.CompareTo(ans) < 0 ? word : ans;
            }
            return ans;
        }


        // Time O(n) || Space O(1) || Recursive Soln
        public static int ScoreOfParentheses(string S)
        {
            /* Given a balanced parentheses string S, compute the score of the string based on the following rule:
             *      () has score 1
             *      AB has score A + B, where A and B are balanced parentheses strings.
             *      (A) has score 2 * A, where A is a balanced parentheses string.
             */
            int i = 0, l = S.Length;
            return Score();
            // Local Func
            int Score()
            {
                int curScore = 0;
                if (i >= l) return curScore;
                while (i < l)
                    if (S[i] == '(')
                    {
                        if (S[i + 1] == ')')
                        {
                            i += 2;
                            curScore++;
                        }
                        else
                        {
                            i++;
                            curScore += 2 * Score();
                        }
                    }
                    else
                    {
                        i++;
                        break;
                    }
                return curScore;
            }
        }


        // Time O(n)! || Space O(1) || Recursive Space O(n), n os maximum pattern length
        public static int AndroidUnlockPatterns(int m, int n)
        {
            /* Android devices have a special lock screen with a 3 x 3 grid of dots.
             * Users can set an "unlock pattern" by connecting the dots in a specific sequence, 
             * forming a series of joined line segments where each segment's endpoints are two consecutive dots in the sequence.
             * A sequence of k dots is a valid unlock pattern if both of the following are true:
             *      >> All the dots in the sequence are distinct.
             *      >> If the line segment connecting two consecutive dots in the sequence passes through any other dot, 
             *          the other dot must have previously appeared in the sequence. No jumps through non-selected dots are allowed.
             * 
             * Here are some example valid and invalid unlock patterns:
             * The 1st pattern [4,1,3,6] is invalid because the line connecting dots 1 and 3 pass through dot 2, but dot 2 did not previously appear in the sequence.
             * The 2nd pattern [4,1,9,2] is invalid because the line connecting dots 1 and 9 pass through dot 5, but dot 5 did not previously appear in the sequence.
             * The 3rd pattern [2,4,1,3,6] is valid because it follows the conditions.
             *      The line connecting dots 1 and 3 meets the condition because dot 2 previously appeared in the sequence.
             * The 4th pattern [6,5,4,1,9,2] is valid because it follows the conditions.
             *      The line connecting dots 1 and 9 meets the condition because dot 5 previously appeared in the sequence.
             * 
             * Given two integers m and n, return the number of unique and valid unlock patterns of the Android grid lock screen
             * that consist of at least m keys and at most n keys.
             * 
             * Two unlock patterns are considered unique if there is a dot in one sequence that is not in the other, or the order of the dots is different.
             * 
             * 
             * IMPORTANT NOTE:
             *      The algorithm above could be optimized if we consider the symmetry property of the problem.
             *      We notice that the number of valid patterns with first digit 1, 3, 7, 9 are the same.
             *      A similar observation is true for patterns which starts with digit 2, 4, 6, 8.
             *      Hence we only need to calculate one among each group and multiply by 
             */

            // to mark digits from 1 to 9 which are used or not
            bool[] used = new bool[9];
            int unqiuePatterns = 0, mid;
            for (int patternLen = m; patternLen <= n; patternLen++)
                unqiuePatterns += GetPattern(-1, patternLen);
            return unqiuePatterns;

            // local func
            int GetPattern(int last, int len)
            {
                if (len == 0) return 1;

                int patternsOfGivenLen = 0;
                for (int curr = 0; curr < 9; curr++)
                    if (IsValid(last, curr))
                    {
                        used[curr] = true;
                        patternsOfGivenLen += GetPattern(curr, len - 1);
                        used[curr] = false;
                    }
                return patternsOfGivenLen;
            }
            bool IsValid(int last, int curr)
            {
                // newly selected digit is already used before
                if (used[curr]) return false;

                // if first digit is being selected, can select any digit
                if (last == -1) return true;

                // Knight Move (as in chess) or (adjacent cells in a row) or (adjacent cells in a column)
                if ((last + curr) % 2 == 1)
                    return true;

                // if its a diagonal move from ex: Digit 1 to 9 or moving from 3 to 7 or vice versa
                mid = (last + curr) / 2;
                if (mid == 4)       // middle num will be 5 in such a move
                    return used[mid];

                // all other adjacent cells cases, ex: moving from 1 to 5 or moving from 5 to 7
                if (last % 3 != curr % 3 && last / 3 != curr / 3)
                    return true;

                // remaining moves accross a row or column, ex: moving frrom 3 to 9 or moving from 4 to 6
                return used[mid];
            }
        }


        // Time O(n) || Space O(1)
        public static int FindUnsortedSubarray(int[] nums)
        {
            int sortFrom = -1, sortTill = -1, compareIdx, maxTillNow, smallestNum = int.MaxValue, lastSmallest = int.MaxValue;
            for (int i = 1; i < nums.Length; i++)
                if (nums[i - 1] > nums[i])      // not ascending order
                {
                    compareIdx = i - 1;
                    maxTillNow = nums[i - 1];
                    // find out how many elements on right are strictly smaller than 'maxTillNow'
                    while (i < nums.Length && maxTillNow > nums[i])
                        smallestNum = Math.Min(smallestNum, nums[i++]);     // also keep track of minimum num encountered
                    sortTill = i;

                    if (sortFrom == -1 || smallestNum < lastSmallest)
                    {
                        // if encountered unsorted subarray previously than update the compareIdx to earlier sortFrom index
                        if (sortFrom != -1)
                            compareIdx = sortFrom;
                        smallestNum = Math.Min(smallestNum, lastSmallest);  // also update the if min num to smallest Num from either prv or current unsorted subarray

                        // find out how many elements on left are strictly larger than 'smallestNum'
                        while (compareIdx >= 0 && nums[compareIdx] > smallestNum)
                            compareIdx--;

                        sortFrom = compareIdx + 1;
                        lastSmallest = smallestNum;
                    }
                }
            return sortTill - sortFrom;
        }


        // Time = Space = O(n)
        public static bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            Stack<int> st = new Stack<int>();
            int i = 0, j = 0, l = pushed.Length;
            while (i < l || st.Count > 0)
            {
                while (i < l)
                {
                    st.Push(pushed[i]);             // Keep Pushing elements into Stack till we dont find 1st elements thats matches popped num at 'j' index
                    if (pushed[i++] == popped[j])
                        break;
                }

                while (st.Count > 0)
                    if (popped[j] == st.Peek())     // while stack is not keep popping nums from stack till they matches popped num at 'j' index
                    { j++; st.Pop(); }
                    else if (i < l)                 // if we still have more elements to push to Stack, go back to inserting
                        break;
                    else                            // we dont have more elements to push to stack and current stack top doesnt matches next element that should be popped
                        return false;
            }
            return st.Count == 0;                   // all nums were successfully pushed and popped from Stack
        }


        // Time O(n*m) || Space O(1), n = length of 'A' & m = avg len of strings
        public static IList<string> CommonChars(string[] A)
        {
            int[] ansMap = new int[26], charMap = new int[26];
            // Initialize the Max count from 1st string
            foreach (var ch in A[0])
                ansMap[ch - 'a']++;

            // iterate thru remaining strings
            for (int i = 1; i < A.Length; i++)
            {
                // Create the charCount for each string
                foreach (var ch in A[i])
                    charMap[ch - 'a']++;
                // Now update each index of ansMap with minimum of (ansMap[n],charMap[n])
                for (int j = 0; j < ansMap.Length; j++)
                {
                    ansMap[j] = Math.Min(ansMap[j], charMap[j]);
                    charMap[j] = 0;       // resetting for next iteration
                }
            }

            IList<string> ls = new List<string>();
            for (int i = 0; i < ansMap.Length; i++)
                while (ansMap[i]-- > 0)
                    ls.Add("" + (char)(i + 'a'));
            return ls;
        }


        // Time O(n) || Space O(1), n = dividend (worst case when dividend is int.Max/Min and divisor is 1)
        public static int Divide(int dividend, int divisor)
        {
            /* 
             * The key observation to make is that the problems are occurring because there are more negative signed 32-bit integers
             * than there are positive signed 32-bit integers.
             * Each positive signed 32-bit integer has a corresponding negative signed 32-bit integer.
             * However, the same is not true for negative signed 32-bit integers. The smallest one, -2147483648, is alone.
             * It is this number that causes the problems.
             * 
             * The best solution is to work with negative, instead of positive, numbers.
             * This is allows us to use the largest possible range of numbers, and it covers all the ones we need.
             * 
             * At the start of the algorithm, we'll instead convert both inputs to negative.
             * Then, we'll need to modify the loop so that it subtracts the negative divisor from the negative dividend.
             * At the end, we'll need to convert the result back to a positive if the number of negative signs in the input was not 1.
             * 
             * Remember that we're converting the inputs to negative numbers.
             * This is because we don't want separate code for all the possible combinations of positive/negative divisor and dividend.
             * We converted them to negative instead of positive because the range of valid negative numbers is bigger,
             * and therefore overflows can be cleanly avoided.
             */

            //if (dividend == int.MinValue && divisor == -1)
            //    return -int.MaxValue;

            //int isNegativeCount = 0, substraction = 0;
            //if (dividend < 0)
            //{
            //    isNegativeCount++;
            //    dividend = -dividend;
            //}
            //if (divisor < 0)
            //{
            //    isNegativeCount++;
            //    divisor = -divisor;
            //}
            //// algo
            //while (dividend - divisor >= 0)
            //{
            //    substraction++;
            //    dividend -= divisor;
            //}
            //return isNegativeCount == 1 ? -substraction : substraction;

            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;

            int isNegativeCount = 2, quotient = 0;
            if (dividend > 0)
            {
                isNegativeCount--;
                dividend = -dividend;
            }
            if (divisor > 0)
            {
                isNegativeCount--;
                divisor = -divisor;
            }
            // algo
            while (dividend - divisor <= 0)
            {
                quotient--;
                dividend -= divisor;
            }
            return isNegativeCount != 1 ? -quotient : quotient;
        }
        // Time O(log base 2 (n)) || Space O(1), n = max absoulte value of divident
        public static int DivideFaster(int dividend, int divisor)
        {
            // ALGO
            //while (dividend >= divisor)
            //{
            //    // Now that we're in the loop, we know it'll fit at least once as divivend >= divisor
            //    value = divisor;
            //    powerOfTwo = 1;
            //    // Check if double the current value is too big. If not, continue doubling.
            //    // If it is too big, stop doubling and continue with the next step */
            //    while (dividend >= (value + value))      // value >> 1 or value*2
            //    {
            //        value <<= 1;        // value +=value or value*=2
            //        powerOfTwo <<= 1;   // powerOfTwo +=powerOfTwo or powerOfTwo*=2
            //    }

            //    // We have been able to subtract divisor another powerOfTwo times.
            //    quotient += powerOfTwo;
            //    // Remove value so far so that we can continue the process with remainder.
            //    dividend -= value;
            //}
            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;

            int isNegativeCount = 2, quotient = 0, powerOfTwo, value;
            if (dividend > 0)
            {
                isNegativeCount--;
                dividend = -dividend;
            }
            if (divisor > 0)
            {
                isNegativeCount--;
                divisor = -divisor;
            }
            // algo
            while (dividend <= divisor)
            {
                value = divisor;
                // Note: We use a negative powerOfTwo as it's possible we might have the case divide(INT_MIN, 1)
                powerOfTwo = -1;
                while (value >= (int.MinValue >> 1) && dividend <= (value + value))
                {
                    value <<= 1;        // value +=value or value*=2
                    powerOfTwo <<= 1;   // powerOfTwo +=powerOfTwo or powerOfTwo*=2
                }

                // We have been able to subtract divisor another powerOfTwo times.
                quotient += powerOfTwo;
                // Remove value so far so that we can continue the process with remainder.
                dividend -= value;
            }
            // If there was originally one negative sign, then the quotient remains negative.
            // Otherwise, switch it to positive.
            return isNegativeCount != 1 ? -quotient : quotient;
        }


        // Time O(n*(k+m) + m*l) || Space O(m)
        // n = len of queries & k = avg len of strings in queries
        // m = len of words & l = avg len of strings in words
        public static int[] NumSmallerByFrequency(string[] queries, string[] words)
        {
            int[] ans = new int[queries.Length];
            int smallest, frequencyQ;
            int[] frequencyW = new int[words.Length];
            for (int j = 0; j < words.Length; j++)              // O(m)
            {
                smallest = 'z' - 'a';
                foreach (var ch in words[j])                    // O(l)
                    if (ch - 'a' < smallest)
                    {
                        smallest = ch - 'a';
                        frequencyW[j] = 1;
                    }
                    else if (ch - 'a' == smallest)
                        frequencyW[j]++;
            }

            // Iterate thru each queries
            for (int i = 0; i < queries.Length; i++)            // O(n)
            {
                smallest = 'z' - 'a';
                frequencyQ = 0;
                // fetch the frequency of smallest characters for each query
                foreach (var ch in queries[i])                  // O(k)
                    if (ch - 'a' < smallest)
                    {
                        smallest = ch - 'a';
                        frequencyQ = 1;
                    }
                    else if (ch - 'a' == smallest)
                        frequencyQ++;

                for (int j = 0; j < frequencyW.Length; j++)     // O(m)
                    // f(queries[n]) < f(W)
                    if (frequencyQ < frequencyW[j])
                        ans[i]++;
            }
            return ans;
        }


        // Time = Space = O(n)
        public static int DistributeCandies(int[] candyType)
        {
            /* Count no of different candies avaliable,
             * if the distinct candy count reach length/2 than return length/2 as that is max allowed limit
             * else return the whatever distinct count we get after traversing entire array
             */
            HashSet<int> uniqueCandy = new HashSet<int>();
            for (int i = 0; i < candyType.Length; i++)
            {
                uniqueCandy.Add(candyType[i]);
                if (uniqueCandy.Count == candyType.Length / 2)
                    return uniqueCandy.Count;
            }
            return uniqueCandy.Count;

            // Another O(n) but slightly slower approach is to return Math.Min(l/2,uniqueCandy.Count) after traversing entire array
            //for (int n = 0; n < candyType.Length; n++)
            //    uniqueCandy.Add(candyType[n]);
            //return Math.Min(candyType.Length / 2, uniqueCandy.Count);
        }


        // Time O(n) || Space O(1)
        public static int CalculateTime(string keyboard, string word)
        {
            int[] charMap = new int[26];
            // create mapping of which alphabet is placed at which position/index in keyboard
            for (int i = 0; i < keyboard.Length; i++)
                charMap[keyboard[i] - 'a'] = i;

            // update the starting position of finger on the 1st character of keyboard
            int pos = charMap[keyboard[0] - 'a'], time = 0;

            // Calculate the total travel time b/w each char in words from last position finger was placed
            for (int i = 0; i < word.Length; i++)
            {
                time += Math.Abs(charMap[word[i] - 'a'] - pos);
                pos = charMap[word[i] - 'a'];
            }
            return time;
        }


        // Time O(nlogn) || Space O(n), n = length of input n.e. Total no of ppl attending party
        public static List<string> OrderOfSplitOfLoafOfBread(List<string> input)
        {
            if (input == null || input.Count == 0) return new List<string>();

            Dictionary<string, List<string>> dRMap = new Dictionary<string, List<string>>();

            foreach (var donorReciepentPair in input)
            {
                var map = donorReciepentPair.Split('-');
                // add donor to dictionary
                if (!dRMap.ContainsKey(map[0]))
                    dRMap.Add(map[0], new List<string>());

                // add recipent to dictionary
                if (!dRMap.ContainsKey(map[1]))
                    dRMap.Add(map[1], new List<string>());

                // add donor-recipent mapping
                dRMap[map[0]].Add(map[1]);
            }

            return FindOrderRecursively("host");

            // Local Func
            List<string> FindOrderRecursively(string donor)
            {
                List<List<string>> rMap = new List<List<string>>();

                foreach (var recipent in dRMap[donor])
                {
                    rMap.Add(new List<string>());
                    rMap[rMap.Count - 1] = FindOrderRecursively(recipent);      // add recipent
                }
                rMap.Sort(new SortDonor());

                List<string> sortedList = new List<string>();
                foreach (var list in rMap)
                    foreach (var recipent in list)
                        sortedList.Add(recipent);

                sortedList.Add(donor);                                          // add donor at last
                return sortedList;
            }
        }

        public class SortDonor : IComparer<List<string>>
        {
            public int Compare(List<string> a, List<string> b) => b.Count - a.Count;
        }


        // Time O(min(n1,n2)) || Recursive Space O(min(h1,h2)), n = no of nodes, h = max ht of trees
        public static bool FlipEquiv(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null) return true;
            if (root1 == null || root2 == null) return false;
            return root1.val == root2.val
                && ((FlipEquiv(root1.left, root2.left) && FlipEquiv(root1.right, root2.right))
                || (FlipEquiv(root1.left, root2.right) && FlipEquiv(root1.right, root2.left)));
        }


        // Time O(n) || Recursive Space O(h) Soln
        public static int DiameterOfBinaryTree(TreeNode root)
        {
            int diameter = 0;
            GetMaxDia(root);
            return diameter;
            // Local func
            int GetMaxDia(TreeNode r)
            {
                if (r == null) return 0;
                int leftLen = GetMaxDia(r.left);
                int rightLen = GetMaxDia(r.right);
                diameter = Math.Max(diameter, leftLen + rightLen);
                return Math.Max(leftLen, rightLen) + 1;
            }
        }


        // Time = O(h) || Space O(1)
        public static int ClosestToDoubleValue(TreeNode r, double target)
        {
            int closet = r.val;
            while (r != null)
            {
                closet = Math.Abs(r.val - target) < Math.Abs(closet - target) ? r.val : closet;
                if (r.val > target) r = r.left;
                else r = r.right;
            }
            return closet;
        }


        // Time = O(n) || Space = O(1)
        public static IList<string> FindMissingRanges(int[] nums, int lower, int upper)
        {
            /* FIRST APPROACH => TLE
            IList<string> ans = new List<string>();
            int l = nums.Length, missingTill;
            for (int n = 0; n < l; n++)
                if (nums[n] == lower)
                    lower++;
                else    // found a missing no
                {
                    missingTill = lower;
                    while (missingTill != nums[n])
                        missingTill++;

                    if (lower == missingTill - 1)       // single num was missing
                        ans.Add(lower.ToString());
                    else                                // range of numbers are missing
                        ans.Add(lower.ToString() + "->" + (missingTill - 1).ToString());
                    
                    lower = missingTill + 1;
                }
            
            if (lower > upper)      // entire range of numbers is traversed
                return ans;
            else                    // few numbers at end are still missing
            {
                if (lower == upper) ans.Add(lower.ToString());
                else ans.Add(lower.ToString() + "->" + upper.ToString());
            }
            return ans;
            */
            IList<string> ans = new List<string>();
            int l = nums.Length;
            for (int i = 0; i < l; i++)
                if (nums[i] == lower)
                    lower++;
                else    // found a missing no
                {
                    if (lower + 1 == nums[i])       // single num was missing
                        ans.Add(lower.ToString());
                    else                            // range of numbers are missing
                        ans.Add(lower.ToString() + "->" + (nums[i] - 1).ToString());
                    // update lower bound
                    lower = nums[i] + 1;
                }

            if (lower > upper)      // entire range of numbers is traversed
                return ans;
            // few numbers at end are still missing
            else if (lower == upper)
                ans.Add(lower.ToString());
            else
                ans.Add(lower.ToString() + "->" + upper.ToString());

            return ans;
        }


        // Time O(Max(n,m)) || Space O(m), n = len of 'S' & m = len of 'indexes'
        public static string FindReplaceString(string S, int[] indexes, string[] sources, string[] targets)
        {
            if (S.Length < 1) return S;
            Dictionary<int, int> validReplacements = new Dictionary<int, int>();
            for (int i = 0; i < indexes.Length; i++)
                if (IsValid(indexes[i], sources[i]))
                    // 'key' index in S & Value is position of this replacement in indexes
                    validReplacements.Add(indexes[i], i);

            // check if there are no valid replacements to be made
            if (validReplacements.Count == 0)
                return S;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < S.Length; i++)
                if (validReplacements.ContainsKey(i))
                {
                    sb.Append(targets[validReplacements[i]]);
                    i += sources[validReplacements[i]].Length - 1;// frwd the idx to point where next comparsion r 2 b made
                }
                else
                    sb.Append(S[i]);

            return sb.ToString();

            // Local Func
            bool IsValid(int idx, string s)
            {
                for (int k = 0; k < s.Length; k++)
                    if (S[idx + k] != s[k])  // letters don't match
                        return false;
                return true;
            }

        }


        // Time O(n) || Space O(1)
        // Given two strings A and B of lowercase letters, return true if you can swap two letters in A so the result is equal to B, otherwise, return false
        public static bool BuddyStrings(string A, string B)
        {
            /* if len of A != len of B return false at start
             * 
             * Traverse thru the A & B & match each index A[n] with B[n]
             * also keep count of no of distinct characters along with their frquencies.
             * once any character appears more than once update sameCharSwapPossible to true
             * 
             * if A[n]!=B[n]
             * update a varaible misMatchAt which was initially set to -1 with current 'n'
             * if misMatchAt != -1 mean we can previously encountered one more misMatch
             * 
             * check for A[misMatchAt]!=B[n] || B[misMatchAt]!=A[n] than return false as swap cannot be completed
             * else update oneSwapMade to true
             * 
             * at any point if no off diff characters are > 2 return false
             * 
             * else return diff%2 == 0 && (oneSwapMade || sameCharSwapPossible)
             */
            if (A.Length != B.Length) return false;     // different length match not possible

            int[] charCount = new int[26];              // to count frequencies of each letter in A

            int diff = 0, misMatchAt = -1;
            bool sameLetterSwapPossible = false, oneSwapMade = false;
            for (int i = 0; i < A.Length; i++)
            {
                if (++charCount[A[i] - 'a'] > 1)
                    sameLetterSwapPossible = true;
                if (A[i] != B[i])
                {
                    if (++diff > 2)                     // no of different characters more than 2
                        return false;

                    if (misMatchAt == -1)               // 1st misMatch
                        misMatchAt = i;
                    else                                // 2nd misMatch
                    {
                        // if swap not possible return false
                        if (A[misMatchAt] != B[i] || B[misMatchAt] != A[i])
                            return false;
                        oneSwapMade = true;             // else update one SwapMade
                    }
                }
            }

            return diff % 2 == 0 && (oneSwapMade || sameLetterSwapPossible);
        }


        /// <summary>
        /// Time O(n) || Space O(1)
        /// 
        /// Give a string s, count the number of non-empty (contiguous) substrings that have the same number of 0's and 1's,
        /// and all the 0's and all the 1's in these substrings are grouped consecutively.
        /// Substrings that occur multiple times are counted the number of times they occur.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int CountBinarySubstrings(string s)
        {
            int zero = 0, one = 0, ans = 0, i = 1;

            if (s[0] == '0') zero++;
            else one++;
            bool firstCharIsOne = one > 0 ? true : false;

            while (i < s.Length)
            {
                if (!firstCharIsOne)
                {
                    while (i < s.Length && s[i] == '0')
                    { zero++; i++; }

                    ans += Math.Min(zero, one);
                    one = 0;
                }
                firstCharIsOne = false;

                while (i < s.Length && s[i] == '1') // counting ones
                { one++; i++; }

                ans += Math.Min(zero, one);
                zero = 0;
            }
            //ans += Math.Min(zero, one);
            return ans;
        }
        // Time O(n) || Space O(1)
        public static int CountBinarySubstringsFaster(string s)
        {
            int zero = 0, one = 0, ans = 0, l = s.Length, i = 0;
            while (i < l)
            {
                while (i < l && s[i] == '1')   // count contiguous one's
                { one++; i++; }
                ans += Math.Min(one, zero);
                zero = 0;

                while (i < l && s[i] == '0')   // count contiguous zero's
                { zero++; i++; }
                ans += Math.Min(one, zero);
                one = 0;
            }
            return ans;
        }


        /// <summary>
        /// Time = Space = O(n*m), n = len of words & m = avg length of each word
        /// A valid encoding of an array of words is any reference string s and array of indices indices such that:
        ///         words.length == indices.length
        ///         The reference string s ends with the '#' character.
        ///         For each index indices[n], the substring of s starting from indices[n] and up to(but not including) the next '#' character is equal to words[n].
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int MinimumLengthEncoding(string[] words)
        {
            /* We find whether different words have the same suffix,
             * let's put them backwards into a trie (prefix tree).
             * For example, if we have "time" and "me", we will put "emit" and "em" into our trie.
             * 
             * After, the leaves of this trie (nodes with no children) represent words that have no suffix,
             * and we will count sum(word.length + 1 for word in words)
             */
            Dictionary<TrieNode, int> lastNodeIdxMap = new Dictionary<TrieNode, int>();
            TrieNode trie = new TrieNode();

            for (int i = 0; i < words.Length; i++)          // O(n)
            {
                TrieNode temp = trie;
                // Add words in reverse to the Trie
                for (int j = words[i].Length - 1; j >= 0; j--)//O(m)
                    temp = temp.Get(words[i][j]);

                // make sure same word is not added multiple times
                if (!lastNodeIdxMap.ContainsKey(temp))
                    lastNodeIdxMap.Add(temp, i);
            }
            int ans = 0;
            foreach (var kvp in lastNodeIdxMap)             // O(n)
                if (kvp.Key.child.Count == 0)
                    ans += 1 + words[kvp.Value].Length;

            return ans;
        }
        public class TrieNode
        {
            public Dictionary<char, TrieNode> child;

            public string word = string.Empty;  // adding this helps later on while adding words to the result set, https://leetcode.com/problems/word-search-ii/
            public TrieNode() => child = new Dictionary<char, TrieNode>();
            // to add a node and return ref node of node which contains curr character
            public TrieNode Get(char ch)
            {
                if (!child.ContainsKey(ch))
                    child.Add(ch, new TrieNode());
                return child[ch];
            }
        }


        // Time O(n) || Space O(1)
        public static int LongestSubarrayOfOnes(int[] nums)
        {
            int currOnes = 0, prvOnes = 0, i = 0, ans = 0, l = nums.Length;
            bool oneNumDeleted = false;
            while (i < l)
            {
                while (i < l && nums[i] == 1)
                { i++; currOnes++; }

                // update ans
                ans = Math.Max(ans, currOnes + prvOnes);

                prvOnes = currOnes;
                currOnes = 0;
                i++;
                if (i < l) oneNumDeleted = true;
            }
            return Math.Max(0, ans - (oneNumDeleted ? 0 : 1));
        }


        // Time = Space = O(n) || Greedy Approach
        public static int MaximumSwap(int num)
        {
            /* 
             * At each digit of the input number in order, if there is a larger digit that occurs later,
             * we know that the best swap must occur with the digit we are currently considering.
             * 
             * Algorithm:
             * We will compute \text{last[d] = n}last[d] = n, the index \text{n}n of the last occurrence of digit \text{d}d (if it exists).
             * Afterwards, when scanning the number from left to right, if there is a larger digit in the future,
             * we will swap it with the largest such digit.
             * if there are multiple such digits, we will swap it with the one that occurs the latest/closet idx from end.
             */
            char[] n = num.ToString().ToCharArray();
            int[] dMap = new int[10];
            // get the latest idx of each digit present in number (last/max idx it was last seen at)
            for (int i = 0; i < n.Length; i++)
                dMap[n[i] - '0'] = i;

            for (int i = 0; i < n.Length; i++)
                for (int d = 9; d > n[i] - '0'; d--)
                    // if biggest possible digit than current digit was found at idx greater than curr digit idx, swap digits
                    if (dMap[d] > i)
                    {
                        char temp = n[dMap[d]];
                        n[dMap[d]] = n[i];
                        n[i] = temp;
                        return Convert.ToInt32(new string(n));
                    }
            return num;
        }


        // Time O(n) || Recursive Space O(n) || Auxillary Space O(1)
        // Given the root of a binary tree, then value v and depth d, you need to add a row of nodes with value v at the given depth d. The root node is at depth 1.
        public static TreeNode AddOneRowToTree(TreeNode root, int val, int depth, bool replaceLeft = true)
        {
            /* ALGO
            1. Trverse thru all the nodes in the tree till u reach height = depth provided as input
            2. Set the replaceLeft to true when moving left and false when moving to right child-node
            2. Once u have reached the level where additional nodes are to be added
            3. Add new node and rest of the child below to appropriate left or right side basis the boolean flag and return the new node
            4. at each level once we recursively call the func itself keep assign back the result to left and right child if Root not Null
             */
            // add new node at given level/depth
            if (depth == 1)
                return replaceLeft ? new TreeNode(val, root) : new TreeNode(val, null, root);
            // keep iterating until till depth>1
            if (root != null && depth > 1)
            {
                root.left = AddOneRowToTree(root.left, val, depth - 1, true);
                root.right = AddOneRowToTree(root.right, val, depth - 1, false);
            }
            return root;
        }

        // Time O(5^(n/2)) || Space O(1)
        public static IList<string> FindStrobogrammatic(int n)
        {
            KeyValuePair<char, char>[] sPair = new KeyValuePair<char, char>[]
            {
                    new KeyValuePair<char,char>('0','0'),
                    new KeyValuePair<char,char>('1','1'),
                    new KeyValuePair<char,char>('6','9'),
                    new KeyValuePair<char,char>('8','8'),
                    new KeyValuePair<char,char>('9','6')
            };
            int startFrom;

            IList<string> ans = new List<string>();
            char[] num = new char[n];
            GetStroNums(0, n - 1);
            return ans;

            // Local func
            void GetStroNums(int start, int last)
            {
                if (start == last)
                {
                    num[start] = sPair[0].Key;  // 0
                    GetStroNums(start + 1, last - 1);

                    num[start] = sPair[1].Key;  // 1
                    GetStroNums(start + 1, last - 1);

                    num[start] = sPair[3].Key;  // 8
                    GetStroNums(start + 1, last - 1);
                }
                else if (start < last)
                {
                    startFrom = start == 0 ? 1 : 0;
                    for (int i = startFrom; i < 5; i++)
                    {
                        num[start] = sPair[i].Key;
                        num[last] = sPair[i].Value;
                        GetStroNums(start + 1, last - 1);
                    }
                }
                else
                    ans.Add(new string(num));
            }
        }


        // Time O(n*k) || Space O(2^k), n = len of 's'
        public static bool HasAllCodes(string s, int k)
        {
            HashSet<string> set = new HashSet<string>();
            var distinctSubStrings = Math.Pow(2, k);
            for (int start = 0; start <= s.Length - k; start++)
            {
                set.Add(s.Substring(start, k));
                if (set.Count == distinctSubStrings)
                    return true;
            }
            return false;
        }


        // Time = Space = O((n*m)^2), n = no of rows & m = no of columns
        public static int[][] CandyCrush(int[][] board)
        {
            /* first mark all the cells which fits the given conditions of 3 or more continous cells of same candyType either Horizontally or vertically
             * Than Crush/Mark those cells as 0
             * Than Stabalize the board so all cells with value zero bubble up to top and all cells with some value settle towards bottom of Board
             * 
             * repeat above step till we can find cells/Candies to Crush
             * 
             * Note:
             *      if the numbers are only +ve integers instead of using seperate 2D array to mark cell to be crushed later
             *      we can simply set the value to -value and always read value using Math.Abs() when reading the board
             */

            int row = board.Length, col = board[0].Length;
            bool[,] toCrush = new bool[row, col];
            while (TryCrushingBoard())
                StabalizeBoard();
            return board;

            // Local Func
            bool TryCrushingBoard()
            {
                bool someCandyToBeCrushed = false;
                for (int r = 0; r < row; r++)
                    for (int c = 0; c < col; c++)
                        // not empty
                        if (board[r][c] != 0)
                            if (MarkedForBeingCrushed(r, c))
                                someCandyToBeCrushed = true;

                // Crush all the Candy which are marked to be crushed
                if (someCandyToBeCrushed)
                    Crush();

                return someCandyToBeCrushed;
            }
            bool MarkedForBeingCrushed(int rID, int cID)
            {
                int i = rID, j = cID, candyType = board[i][j];
                // check if can be crushed from current idx towards right
                if (j + 2 < col && candyType == board[rID][j + 1] && candyType == board[rID][j + 2])
                    while (j < col && candyType == board[rID][j])
                        toCrush[rID, j++] = true;

                // check if can be crushed from current idx towards downwards direction
                if (i + 2 < row && candyType == board[i + 1][cID] && candyType == board[i + 2][cID])
                    while (i < row && candyType == board[i][cID])
                        toCrush[i++, cID] = true;

                return i > rID || j > cID;
            }
            void Crush()
            {
                for (int r = 0; r < row; r++)
                    for (int c = 0; c < col; c++)
                        if (toCrush[r, c])
                        {
                            board[r][c] = 0;            // mark cell empty
                            toCrush[r, c] = false;      // reset for next iteration
                        }
            }
            void StabalizeBoard()
            {
                int r;
                Queue<int> q = new Queue<int>();
                for (int c = 0; c < col; c++)   // for each column do below
                {
                    // Gather all the non empty Candies from the bottom towards the top
                    for (r = row - 1; r >= 0; r--)
                        if (board[r][c] != 0)
                        {
                            q.Enqueue(board[r][c]);
                            board[r][c] = 0;
                        }


                    r = row - 1;
                    // If there is atleast 1 cell which was empty than Candy count would be less than column length n.e no of rows
                    //if (q.Count != row)
                    while (q.Count > 0)
                        // updates cells from bottom till Queue in not empty
                        board[r--][c] = q.Dequeue();
                }
            }
        }


        // Time = Space = O(r*c)
        public static int LongestLine(int[][] M)
        {
            if (M.Length < 1) return 0;
            int row = M.Length, col = M[0].Length, ans = 0;
            int maxLenPossible = Math.Max(row, col);
            // check all rows & columns + diagonals running from (left-top to rt-bottom) & (right-top to left-bottom)

            Direction[,] grid = new Direction[row, col];
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    if (M[r][c] == 1)
                    {
                        grid[r, c] = new Direction();
                        // set default value 1 in all 4 direction
                        for (int i = 0; i < grid[r, c].d.Length; i++) grid[r, c].d[i] = 1; // O(1)

                        // add count of adjacent 1's found on left of current pos(r,c))
                        if (c - 1 >= 0 && M[r][c - 1] == 1)
                            grid[r, c].d[0] = Math.Max(grid[r, c].d[0], grid[r, c - 1].d[0] + 1);
                        // add count of adjacent 1's found on top of current pos(r,c))
                        if (r - 1 >= 0 && M[r - 1][c] == 1)
                            grid[r, c].d[1] = Math.Max(grid[r, c].d[1], grid[r - 1, c].d[1] + 1);
                        // add count of adjacent 1's found on diag (left-top to rt-bottom) of current pos(r,c))
                        if (r - 1 >= 0 && c - 1 >= 0 && M[r - 1][c - 1] == 1)
                            grid[r, c].d[2] = Math.Max(grid[r, c].d[2], grid[r - 1, c - 1].d[2] + 1);
                        // add count of adjacent 1's found on diag (right-top to left-bottom) of current pos(r,c))
                        if (r - 1 >= 0 && c + 1 < col && M[r - 1][c + 1] == 1)
                            grid[r, c].d[3] = Math.Max(grid[r, c].d[3], grid[r - 1, c + 1].d[3] + 1);

                        // update ans based upon Max Value found in either direction
                        for (int i = 0; i < grid[r, c].d.Length; i++) ans = Math.Max(ans, grid[r, c].d[i]);  // O(1)

                        if (ans == maxLenPossible) return ans;
                    }
            return ans;
        }
        public class Direction
        {
            public int[] d;
            public Direction() => d = new int[4];
        }


        // Time = O(n) || Space = O(1)
        public static ListNode SwapNodes(ListNode head, int k)
        {
            /* Simple Soln with O(n) Space Convert LinkedList to List and swap nodes at given index and convert back list to LinkedList
             * 
             * 
             * Constant Space soln
             * 
             * First create dummy node and point its next to LinkedList head, to simplyfy edge cases
             * 
             * Now all we need to do is to keep track to nodes whose next node is 'toSwap' and also prv of 'swapWith' node
             * Than Swap those two and return dummy.Next as new List Head
             */
            ListNode dummy = new ListNode(0) { next = head };
            ListNode kthFront = dummy, kthEnd = dummy, curr = dummy.next;
            int count = 1;
            /*
            if (k == 1)
                while (curr.next != null)
                {
                    kthEnd = curr;
                    curr = curr.next;
                }
            else
                while (curr.next != null)
                {
                    // update kthFront prv node
                    if (count == k - 1)
                        kthFront = curr;

                    // update kthEnd prv node
                    if (count >= k)
                        kthEnd = kthEnd.next;

                    curr = curr.next;
                    count++;
                }
            */
            // => Above if else Can be further simplyfied to just below
            while (curr.next != null)
            {
                // update kthFront prv node
                if (count == k - 1)
                    kthFront = curr;

                // update kthEnd prv node
                if (count >= k)
                    kthEnd = kthEnd.next;

                curr = curr.next;
                count++;
            }


            // swap kth nodes from front and end
            if (kthFront != kthEnd)
            {
                // A -> B -> C-> D
                ListNode toSwap = kthFront.next;
                ListNode swapWith = kthEnd.next;
                ListNode toSwapNext = null;

                if (kthEnd.next == kthFront) //if(swapWith.next == toSwap)
                {
                    toSwapNext = toSwap.next;
                    kthEnd.next = toSwap;
                    toSwap.next = swapWith;
                }
                else
                {
                    kthEnd.next = toSwap;
                    toSwapNext = toSwap.next;
                    toSwap.next = swapWith.next;

                    kthFront.next = swapWith;
                }
                swapWith.next = toSwapNext;
            }
            return dummy.next;
        }


        // Time O(n^2) || Recursive Space O(n)
        public static bool FlipGameII(string currentState)
        {
            /* First check if we can make a valid move if not return false as we are not making the last move
             * 
             * next check if after making a valid move and passed the nextstate to our opponent
             * and than our opponent cannot win with that state means we can win the game.
             * 
             * keep trying above step for each consecutive "++" if our opponent loses at any stepafter any of those move
             * means we can win the game.
             * 
             * else return false at we could not win the game
             */
            for (int i = 0; i < currentState.Length - 1; i++)
                if (currentState[i] == '+' && currentState[i + 1] == '+')   // valid move of converting '++' to '--'
                    if (!FlipGameII(currentState.Substring(0, i) + "--" + currentState.Substring(i + 2)))   // if opp cannot win n.e. we won
                        return true;
            return false;
        }


        // Time O(n^2) || Space O(1)
        public static int WaysToMakeFair(int[] nums)
        {
            int count = 0, removed = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                removed = i;
                if (IsBalanced())
                    count++;
            }
            return count;

            // Local Func
            bool IsBalanced()
            {
                int odd = 0, even = 0;
                for (int k = 0; k < nums.Length; k++)
                    if (k < removed)
                    {
                        if (k % 2 == 1) odd += nums[k];
                        else even += nums[k];
                    }
                    else if (k > removed)
                    {
                        if (k % 2 == 0) odd += nums[k];
                        else even += nums[k];
                    }
                return odd == even;
            }
        }

        // Time O(n) || Space O(n)
        public static int WaysToMakeFairFaster(int[] nums)
        {
            int l = nums.Length, count = 0, oddSum = 0, evenSum = 0;
            int[] preFixSum = new int[l];
            // calculate prefix sum seperately for all odd and even numbers
            for (int i = 0; i < l; i++)
                if (i % 2 == 1)                 // odd sum
                {
                    oddSum += nums[i];
                    preFixSum[i] += oddSum;
                }
                else                            // even sum
                {
                    evenSum += nums[i];
                    preFixSum[i] += evenSum;
                }

            int leftOddSum = 0, rtOddSum = oddSum, leftEvenSum = 0, rtEvenSum = evenSum;
            for (int i = 0; i < l; i++)
            {
                if (i % 2 == 1)                 // if index is odd
                    rtOddSum -= nums[i];
                else
                    rtEvenSum -= nums[i];

                // parity of the indices after the removed element changes.
                if (leftOddSum + rtEvenSum == leftEvenSum + rtOddSum)
                    count++;

                if (i % 2 == 1) leftOddSum += nums[i];
                else leftEvenSum += nums[i];
            }
            return count;
        }


        // Time O(n) || Space O(1)
        public static int WiggleMaxLength(int[] nums)
        {
            int ans = 1, i = 1, l = nums.Length;
            while (i < l && nums[i] == nums[i - 1])
                i++;

            if (i == l) return ans;

            bool isDiffPositive = nums[i - 1] > nums[i++];
            ans++;
            while (i < l)
            {
                if (isDiffPositive && nums[i - 1] < nums[i])
                {
                    isDiffPositive = !isDiffPositive;
                    ans++;
                }
                else if (!isDiffPositive && nums[i - 1] > nums[i])
                {
                    isDiffPositive = !isDiffPositive;
                    ans++;
                }
                i++;
            }
            return ans;
        }


        // Time O(n*m) || Space O(n), n = no of rooms & m = toal no of keys
        public static bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            int l = rooms.Count;
            List<int> keys = new List<int>() { 0 };
            bool[] visited = new bool[l];
            int count = l;
            // for all keys we have
            for (int i = 0; i < keys.Count; i++)
                // visited each of those rooms which are not already visited
                if (!visited[keys[i]])
                {
                    // all rooms visited
                    if (--count == 0) return true;

                    // mark current room visited
                    visited[keys[i]] = true;

                    // append newly found keys from each room to list of keys we have
                    foreach (var newKey in rooms[keys[i]])
                        // if that room is not already visited.
                        if (!visited[newKey])
                            keys.Add(newKey);
                }
            return false;
        }


        // Time O(2^n) || Auxiliary Space O(1) || Recursive Space O(n), n = no of digits in number
        public static bool ReorderedPowerOf2(int N)
        {
            if (IsPowerOfTwo(N)) return true;
            // Fetch all the digits and their frequency from 'N'
            int[] digits = new int[10];
            int count = 0;
            while (N > 0)
            {
                digits[N % 10]++;
                N /= 10;
                count++;
            }
            N = 0;
            return FindAllNumsRecursively();

            // local func
            bool IsPowerOfTwo(int num)
            {
                while (num % 2 == 0)    // additionally check for num > 0 not required since problem states, 1 <= N <= 10^9
                    num /= 2;
                return num == 1;
            }
            // Recursive func which generates all possible combination of numbers from avaliable set of digits
            bool FindAllNumsRecursively(int i = 0)
            {
                // once we have addded all avaliable digits check if this num is 'power of 2'
                if (i == count)
                    return IsPowerOfTwo(N);
                else
                {
                    for (int k = 0; k < digits.Length; k++)
                        // if kth digit should have to use count greater than 0 &
                        // (we are not adding first digit to Num or if we are adding 1st digit it should not be '0')
                        if (digits[k] > 0 && (i > 0 || k != 0))
                        {
                            N = N * 10 + k;     // add new digit to number
                            digits[k]--;        // reduce count of digit just used

                            if (FindAllNumsRecursively(i + 1))
                                return true;

                            N /= 10;            // remove last added digit from the number
                            digits[k]++;        // reset back the count of digit
                        }
                }
                return false;
            }
        }


        public static int[] CreateMaximumNumberBruteForce(int[] nums1, int[] nums2, int k)
        {
            int[] maxNum = new int[k], currNum = new int[k];
            int l1 = nums1.Length, l2 = nums2.Length;
            CreateMax();
            //CreateMax2();
            return maxNum;
            // local func
            // Works fine but slow algo
            void CreateMax(int i = 0, int j = 0, int idx = 0)
            {
                if (idx == k)
                {
                    bool newNumIsBigger = false;
                    // find out if there is atleast one leading digit which is not same in both max and curr array
                    // and if that digit is greater in currNum array than update the max array
                    for (int index = 0; index < k; index++)
                        if (maxNum[index] != currNum[index])
                        {
                            if (maxNum[index] < currNum[index])
                                newNumIsBigger = true;
                            break;
                        }

                    if (newNumIsBigger)
                        for (int index = 0; index < k; index++)
                            maxNum[index] = currNum[index];
                }
                else
                {
                    for (int i1 = i; i1 < l1; i1++)
                    {
                        if ((l1 - i1) + (l2 - j) < k - idx) break;
                        currNum[idx] = nums1[i1];
                        CreateMax(i1 + 1, j, idx + 1);
                    }

                    for (int j1 = j; j1 < l2; j1++)
                    {
                        if ((l1 - i) + (l2 - j1) < k - idx) break;
                        currNum[idx] = nums2[j1];
                        CreateMax(i, j1 + 1, idx + 1);
                    }
                }
            }

        }
        // Time O(k^2*(n+m)) || Space O(Max(n,m,k))
        public static int[] CreateMaximumNumberTrimAndMerge(int[] nums1, int[] nums2, int k)
        {
            /* First we trim the original arrays(if possible)
             * than we try every combination of removing n,j elements from A & B
             * ex: if k = 3 we try these >> n=0 j=3 >> n=1 j=2 >> n=2 j =1 >> n=3 j =0
             * after trying removing minimum digits from both array we merge trimmed A & B to check if we have landed upon biggest number possible
             */
            nums1 = Trim(nums1, k);     // O(n)
            nums2 = Trim(nums2, k);     // O(m)

            if (nums1.Length + nums2.Length <= k)
                return Merge(nums1, nums2, k);      // O((n+m)*k)

            int[] max = null, curr;
            for (int i = k, j = 0; i >= 0; i--, j++)// O(k^2*(n+m))
                // Make sure we can remove n & j no of nums from A & B respectively
                if (nums1.Length >= i && nums2.Length >= j)
                {
                    curr = Merge(Trim(nums1, i), Trim(nums2, j), k);

                    if (max == null)
                        max = curr;
                    else
                        for (int idx = 0; idx < k; idx++)
                            // Atleast one non-equal digit is bigger in max
                            if (max[idx] > curr[idx])
                                break;
                            // Atleast one non-equal digit is bigger in curr
                            else if (max[idx] < curr[idx])
                            {
                                max = curr;
                                break;
                            }
                }
            return max;

            // local helper functions

            // Function which trims 'k' elements to maximimze array 'A'
            // Time = Space = O(n), n = len of 'A"
            int[] Trim(int[] A, int toTrim)
            {
                toTrim = A.Length - toTrim;
                // Not enouf elements to trim, return original array
                if (toTrim <= 0) return A;

                Stack<int> st = new Stack<int>();
                for (int i = 0; i < A.Length; i++)
                {
                    // stack top num is smaller than curr than remove stack top
                    while (st.Count > 0 && st.Peek() < A[i] && toTrim > 0)
                    {
                        st.Pop();
                        toTrim--;
                    }
                    st.Push(A[i]);
                }
                // if still nums left to trim
                while (toTrim-- > 0) st.Pop();

                return st.Reverse().ToArray();
            }

            // Merge max possible digits from 'A' & 'B' and return merged max possible array of size 'l'
            // Time = O(n+m)*l, n = len of A, m = len of 'B', l = len of Merged Arr
            int[] Merge(int[] A, int[] B, int l)
            {
                int[] merged = new int[l];
                int i = 0, j = 0, idx = 0, lenA = A.Length, lenB = B.Length, n1, n2;
                while (idx < l)
                {
                    n1 = i < lenA ? A[i] : -1;
                    n2 = j < lenB ? B[j] : -1;
                    if (n1 > n2)
                        merged[idx] = A[i++];
                    else if (n1 < n2)
                        merged[idx] = B[j++];
                    else
                    {
                        // find next num which is not equal
                        int i1 = i + 1, j1 = j + 1;
                        while (i1 < lenA && j1 < lenB && A[i1] == B[j1])
                        { i1++; j1++; }

                        if (i1 == lenA)
                            merged[idx] = B[j++];
                        else if (j1 == lenB)
                            merged[idx] = A[i++];
                        else if (A[i1] < B[j1])
                            merged[idx] = B[j++];
                        else // if (A[i1] >= B[j1])
                            merged[idx] = A[i++];
                    }
                    idx++;
                }
                return merged;
            }
        }


        // Time = O(n+m) || Space = O(n), n = no of words in 'wordlist' & m = no of words in 'queries'
        public static string[] Spellchecker(string[] wordlist, string[] queries)
        {
            /* Given a wordlist, we want to implement a spellchecker that converts a query word into a correct word.
             * 
             * For a given query word, the spell checker handles two categories of spelling mistakes:
             * 
             * Capitalization: If the query matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the case in the wordlist.
             *      Example: wordlist = ["yellow"], query = "YellOw": correct = "yellow"
             *      Example: wordlist = ["Yellow"], query = "yellow": correct = "Yellow"
             *      Example: wordlist = ["yellow"], query = "yellow": correct = "yellow"
             * 
             * Vowel Errors: If after replacing the vowels ('a', 'e', 'n', 'o', 'u') of the query word with any vowel individually,
             * it matches a word in the wordlist (case-insensitive), then the query word is returned with the same case as the match in the wordlist.
             *      Example: wordlist = ["YellOw"], query = "yollow": correct = "YellOw"
             *      Example: wordlist = ["YellOw"], query = "yeellow": correct = "" (no match)
             *      Example: wordlist = ["YellOw"], query = "yllw": correct = "" (no match)
             * 
             * 
             * In addition, the spell checker operates under the following precedence rules:
             *      When the query exactly matches a word in the wordlist (case-sensitive), you should return the same word back.
             *      When the query matches a word up to capitlization, you should return the first such match in the wordlist.
             *      When the query matches a word up to vowel errors, you should return the first such match in the wordlist.
             *      If the query has no matches in the wordlist, you should return the empty string.
             * 
             * 
             * Given some queries, return a list of words answer, where answer[n] is the correct word for query = queries[n].
             */


            /* ALGO => We analyze the 3 cases that the algorithm needs to consider:
             *      when the query is an exact match,
             *      when the query is a match up to capitalization,
             *      and when the query is a match up to vowel errors.
             *      
             * In all 3 cases, we can use a hash table to query the answer.
             *      For the first case (exact match),
             *          we hold a set of words to efficiently test whether our query is in the set.
             *          
             *      For the second case (capitalization),
             *          we hold a hash table that converts the word from its lowercase version to the original word (with correct capitalization).
             *          
             *      For the third case (vowel replacement),
             *          we hold a hash table that converts the word from its lowercase version with the vowels masked out, to the original word.
             */
            HashSet<String> words_perfect;
            Dictionary<String, String> words_cap;
            Dictionary<String, String> words_vow;
            StringBuilder sb = new StringBuilder();

            words_perfect = new HashSet<String>();
            words_cap = new Dictionary<String, String>();
            words_vow = new Dictionary<String, String>();
            string lower, vowelLess;

            // Fill respective Dictionary
            for (int i = 0; i < wordlist.Length; i++)
            {
                words_perfect.Add(wordlist[i]);

                lower = wordlist[i].ToLower();
                if (!words_cap.ContainsKey(lower))
                    words_cap.Add(lower, wordlist[i]);

                vowelLess = DeVowel(lower);
                if (!words_vow.ContainsKey(vowelLess))
                    words_vow.Add(vowelLess, wordlist[i]);
            }

            string[] ans = new string[queries.Length];
            // Find Matching word for each query
            for (int i = 0; i < queries.Length; i++)
                ans[i] = GetMatch(queries[i]);

            return ans;

            // Local Func
            // Time O(l), l = len of 'str'
            string DeVowel(string str)
            {
                sb.Clear();
                foreach (var ch in str)
                    sb.Append(IsVowel(ch) ? '*' : ch);
                return sb.ToString();
            }

            // Time O(1)
            bool IsVowel(char ch) => ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';

            // Time O(1)
            string GetMatch(string toSearch)
            {
                // Case 1: excat match
                if (words_perfect.Contains(toSearch))
                    return toSearch;

                // Case 2: Cap mis-match
                lower = toSearch.ToLower();
                if (words_cap.ContainsKey(lower))
                    return words_cap[lower];

                // Case 3: Vowel mis-match
                vowelLess = DeVowel(lower);
                if (words_vow.ContainsKey(vowelLess))
                    return words_vow[vowelLess];

                // Deafult case: Return empty string
                return "";
            }
        }


        // Time O(n^2) || Space O(n), n = length of array
        public static int ThreeSumMulti(int[] arr, int target)
        {
            long mod = 1000000007, ans = 0;
            int l = arr.Length, currTarget;
            Dictionary<int, int> numFreq = new Dictionary<int, int>();
            for (int i = 0; i < l - 2; i++)
            {
                numFreq.Clear();
                currTarget = target - arr[i];
                if (currTarget >= 0)
                    for (int j = i + 1; j < l; j++)
                    {
                        if (numFreq.ContainsKey(currTarget - arr[j]))
                            ans = (ans + (numFreq[currTarget - arr[j]]) % mod) % mod;

                        if (!numFreq.ContainsKey(arr[j]))
                            numFreq.Add(arr[j], 1);
                        else
                            numFreq[arr[j]]++;
                    }
            }

            return (int)(ans % mod);
        }
        // Time O(n^2) || Space O(1)
        public static int ThreeSumMultiConstantSpacec(int[] arr, int target)
        {
            /* Sort Array
             * Traverse ith index in the outer loop from 0th to len - 2 index,
            *      Traverse the inner loop with start = n+1 & last = len-1
            *      if at any point arr[n] + arr[j] + arr[k] == target
            *      
            *      find no of index after jth index which have value equal to arr[j]
            *      also find no of index before kth index which have value equal to arr[k]
            *      
            *      add ( count of nums with same value arr[j] * count of nums with same value arr[k]) % mod
            * return ans
            * 
            * Note:
            *   Interesting point is that we don't care about the order of the values,
            *   meaning A[n] does not necessarily have to be smaller than A[j], therefore sorting doesn't harm,
            *   because n < j < k just wants us to no have any duplicates, like tuple n,j,k and k,j,n should be counted twice
            */
            Array.Sort(arr);    // nlogn
            long mod = 1000000007, ans = 0;
            int start, last, l = arr.Length, sum, j, k, sameValNums;
            for (int i = 0; i < l; i++)
            {
                start = i + 1;
                last = l - 1;
                while (start < last)
                {
                    sum = arr[i] + arr[start] + arr[last];
                    if (sum == target)
                    {
                        if (arr[start] == arr[last])
                        {
                            sameValNums = 1 + last - start;
                            // ans+ = (n)*(n-1)/2
                            ans = (ans + (sameValNums * (sameValNums - 1) >> 1) % mod) % mod;
                            start = last;
                        }
                        else
                        {
                            // count no of nums equal arr[start]
                            j = start;
                            while (j < last && arr[j] == arr[start])
                                j++;
                            // count no of nums equal arr[last]
                            k = last;
                            while (k > start && arr[k] == arr[last])
                                k--;

                            ans += ((j - start) * (last - k)) % mod;

                            // update 'start' & 'last' to search next tuple
                            start = j;
                            last = k;
                        }
                    }
                    else if (sum < target)
                        start++;
                    else
                        last--;
                }
            }

            return (int)(ans % mod);
        }


        // Time O(nlogn) || Space O(n), n = length of Array 'A'
        public static int[] AdvantageCount(int[] A, int[] B)
        {
            int l = B.Length, i, j = 0;
            int[][] bIndex = new int[l][];
            // Step1:
            // save nums and their original index from array 'B'
            for (i = 0; i < l; i++)
                bIndex[i] = new int[] { B[i], i };

            // Sort in Ascending order based upon value
            int[][] sortedB = (from pair in bIndex
                               orderby pair[0]              // O(nlogn)
                               select pair).ToArray();

            // Step2: Sort 'A'
            Array.Sort(A);                                  // O(nlogn)

            // Step3: Check the smallest num in sorted 'A' if it beats nums at same index in sorted 'B' append it to ans list
            //      as we are trying to maximum future chance to use bigger cards from 'A' to beat possible bigger cards in 'B'
            // else if num in A is <= num in 'B' than move to next index in 'A' to check if new bigger num can beat current num in 'B'
            // once we finish this step we would have 'list of nums' which beats atleast one card in 'B'
            List<int> ans = new List<int>(l);
            j = 0;
            for (i = 0; i < l; i++)                     // O(n)
            {
                while (i + j < l && A[i + j] <= sortedB[i][0])
                    j++;

                if (i + j < l)
                    ans.Add(A[i + j]);
            }

            // append all nums if any from 'A' which didnt beat any card in 'B' to the end of 'ans' list
            if (ans.Count < l)
            {
                i = j = 0;
                for (i = 0; i < ans.Count; i++)         // O(n)
                    while (i + j < l && ans[i] != A[i + j])
                        ans.Add(A[i + j++]);

                while (i + j < l)
                    ans.Add(A[i + j++]);
            }


            // Step4: Reconstruct the answer, by replacing the final list of nums against the numbers the beat
            // all nums which didn't beat any card would be placed at the end
            for (i = 0; i < l; i++)                     // O(n)
                sortedB[i][0] = ans[i];

            // Now Sort 'nums' based upon original index of nums they beat/didn't beat in 'B'
            int[] result = (from pair in sortedB
                            orderby pair[1]                 // O(nlogn)
                            select pair[0]).ToArray();

            return result.ToArray();
        }


        // Time = Space = O(m*n), m = no of rows & n = no of columns in 'matrix' || DFS based approach
        public static IList<IList<int>> PacificAtlantic_DFS(int[][] matrix)
        {
            List<IList<int>> coordinateList = new List<IList<int>>();
            int row = matrix.Length;
            if (row < 1) return coordinateList;         // check for empty input matrix

            int col = matrix[0].Length;
            int[,] pas_Atlan_Flow = new int[row, col];

            // check for Pacific ocean flow
            bool[,] isVisited = new bool[row, col];
            DFS(true);

            // check for Atlantic ocean flow
            isVisited = new bool[row, col];
            DFS(false);

            // update the ans list
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    if (pas_Atlan_Flow[r, c] == 3)
                        coordinateList.Add(new int[] { r, c });

            return coordinateList;

            // local func
            bool IsBoundry(int r, int c, bool forPacificFlow)
            {
                if (forPacificFlow)
                {
                    if (r == 0 || c == 0)
                        return true;
                }
                else // Atlantic Flow
                {
                    if (r == row - 1 || c == col - 1)
                        return true;
                }

                return false;
            }

            // DFS
            void DFS(bool forPacificFlow = true)
            {
                int r, c;

                if (forPacificFlow)
                    r = c = 0;
                else
                {
                    r = row - 1;
                    c = col - 1;
                }

                Queue<int[]> q = new Queue<int[]>();
                q.Enqueue(new int[] { r, c });
                isVisited[r, c] = true;

                int[] curr = null;

                while (q.Count > 0)
                {
                    curr = q.Dequeue();
                    r = curr[0];
                    c = curr[1];
                    pas_Atlan_Flow[r, c] += forPacificFlow ? 1 : 2;

                    // check all 4 sides
                    // top
                    if (r - 1 >= 0 && !isVisited[r - 1, c] && (matrix[r - 1][c] >= matrix[r][c] || IsBoundry(r - 1, c, forPacificFlow)))
                    {
                        q.Enqueue(new int[] { r - 1, c });
                        isVisited[r - 1, c] = true;
                    }
                    // left
                    if (c - 1 >= 0 && !isVisited[r, c - 1] && (matrix[r][c - 1] >= matrix[r][c] || IsBoundry(r, c - 1, forPacificFlow)))
                    {
                        q.Enqueue(new int[] { r, c - 1 });
                        isVisited[r, c - 1] = true;
                    }
                    // bottom
                    if (r + 1 <= row - 1 && !isVisited[r + 1, c] && (matrix[r + 1][c] >= matrix[r][c] || IsBoundry(r + 1, c, forPacificFlow)))
                    {
                        q.Enqueue(new int[] { r + 1, c });
                        isVisited[r + 1, c] = true;
                    }
                    // right
                    if (c + 1 <= col - 1 && !isVisited[r, c + 1] && (matrix[r][c + 1] >= matrix[r][c] || IsBoundry(r, c + 1, forPacificFlow)))
                    {
                        q.Enqueue(new int[] { r, c + 1 });
                        isVisited[r, c + 1] = true;
                    }
                }
            }
        }


        // Time = Space = O(m*n), m = no of rows & n = no of columns in 'heights' || BFS based approach
        public static IList<IList<int>> PacificAtlantic_BFS(int[][] heights)
        {
            int rows = heights.Length, cols = heights[0].Length, r, c;
            int[,] canFlow = new int[rows, cols];
            List<IList<int>> result = new List<IList<int>>();

            Queue<Pair<int, int>> q = new Queue<Pair<int, int>>();
            // BFS from pacific
            c = 0;
            for (r = 0; r < rows; r++)
                q.Enqueue(new Pair<int, int>(r, c));
            r = 0;
            for (c = 0; c < cols; c++)
                q.Enqueue(new Pair<int, int>(r, c));
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                // Console.WriteLine(current);
                r = cur.key;
                c = cur.val;

                if (canFlow[r, c] == 0)
                {
                    // Console.WriteLine($"PAC going in loop for {r},{c} {heights[r][c]}");
                    canFlow[r, c]++;
                    if (r + 1 < rows && heights[r][c] <= heights[r + 1][c])
                        q.Enqueue(new Pair<int, int>(r + 1, c));
                    if (r - 1 >= 0 && heights[r][c] <= heights[r - 1][c])
                        q.Enqueue(new Pair<int, int>(r - 1, c));
                    if (c + 1 < cols && heights[r][c] <= heights[r][c + 1])
                        q.Enqueue(new Pair<int, int>(r, c + 1));
                    if (c - 1 >= 0 && heights[r][c] <= heights[r][c - 1])
                        q.Enqueue(new Pair<int, int>(r, c - 1));
                }
            }

            // BFS from atlantic
            bool[,] visited = new bool[rows, cols];
            c = cols - 1;
            for (r = 0; r < rows; r++)
                q.Enqueue(new Pair<int, int>(r, c));
            r = rows - 1;
            for (c = 0; c < cols; c++)
                q.Enqueue(new Pair<int, int>(r, c));
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                // Console.WriteLine(current);
                r = cur.key;
                c = cur.val;
                if (!visited[r, c])
                {
                    // Console.WriteLine($"ATL going in loop for {r},{c} {heights[r][c]}");
                    visited[r, c] = true;
                    canFlow[r, c]++;
                    if (r + 1 < rows && heights[r][c] <= heights[r + 1][c])
                        q.Enqueue(new Pair<int, int>(r + 1, c));
                    if (r - 1 >= 0 && heights[r][c] <= heights[r - 1][c])
                        q.Enqueue(new Pair<int, int>(r - 1, c));
                    if (c + 1 < cols && heights[r][c] <= heights[r][c + 1])
                        q.Enqueue(new Pair<int, int>(r, c + 1));
                    if (c - 1 >= 0 && heights[r][c] <= heights[r][c - 1])
                        q.Enqueue(new Pair<int, int>(r, c - 1));
                }
            }
            for (r = 0; r < rows; r++)
                for (c = 0; c < cols; c++)
                    if (canFlow[r, c] > 1)
                        result.Add(new List<int>() { r, c });
            return result;
        }


        // Time O(m*n) || Auxiliary Space O(1) || Recursive Space O(m*n)
        public static int NumberOfEnclaves(int[][] grid)
        {
            int ans = 0, land;
            int row = grid.Length, col = grid[0].Length;
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    land = CanFall(i, j);
                    if (land != -1)
                        ans += land;
                }
            return ans;

            // Local Func
            int CanFall(int r, int c)   // DFS
            {
                // if reached boundry or this cell has been marked as 'CanFall' than return -1
                if (r < 0 || r >= row || c < 0 || c >= col || grid[r][c] == -1)
                    return -1;

                if (grid[r][c] == 0)    // Sea encoutered
                    return 0;

                grid[r][c] = 0;         // mark visited
                int lt, rt, top, down;

                // left
                lt = CanFall(r, c - 1);
                if (lt == -1) return grid[r][c] = -1;

                // right
                rt = CanFall(r, c + 1);
                if (rt == -1) return grid[r][c] = -1;

                // top
                top = CanFall(r - 1, c);
                if (top == -1) return grid[r][c] = -1;

                // down
                down = CanFall(r + 1, c);
                if (down == -1) return grid[r][c] = -1;

                return 1 + lt + rt + top + down;
            }
        }


        // Time O(Max(n*l,m*l)) || Space O(1), n = len of B, m = len of A & l = avg length of words in respective arrays
        public static IList<string> WordSubsets(string[] A, string[] B)
        {
            int[] allWordsIn_B_CharMap = new int[26], currWordMap = new int[26];

            foreach (var word in B)                             // O(n)
            {
                // get all characters & frequency for current word in B
                for (int i = 0; i < word.Length; i++)           // O(l)
                    currWordMap[word[i] - 'a']++;

                // update final CharMap which would ultimately be used to check universals words in A
                for (int i = 0; i < currWordMap.Length; i++)    // O(26)
                {
                    allWordsIn_B_CharMap[i] = Math.Max(allWordsIn_B_CharMap[i], currWordMap[i]);
                    currWordMap[i] = 0;    // reset so it can be used for next word in B
                }
            }

            List<string> universalWords = new List<string>();

            for (int i = 0; i < A.Length; i++)
                // if current word has all required character and atleast min required count
                if (IsUniversalWord(A[i]))
                    universalWords.Add(A[i]);

            return universalWords;

            // local func
            bool IsUniversalWord(string word)                       // O(m)
            {
                // get all characters & frequency for current word
                for (int i = 0; i < word.Length; i++)               // O(l)
                    currWordMap[word[i] - 'a']++;

                bool result = true;
                // check if curr word is universals words
                for (int i = 0; i < currWordMap.Length; i++)
                {
                    if (allWordsIn_B_CharMap[i] > currWordMap[i])   // O(26)
                        result = false;
                    currWordMap[i] = 0;    // reset so it can be used for next word in A
                }
                return result;
            }
        }


        // Time O(n) || Space O(h), n = no of nodes & h = ht of binary tree
        public static int MaxProduct(TreeNode root)
        {
            long totalSum = GetSum(root), maxProduct = 0;
            UpdateMax(root);
            return (int)(maxProduct % 1000000007);

            // local func
            long GetSum(TreeNode r)
            {
                if (r == null) return 0;
                return r.val + GetSum(r.left) + GetSum(r.right);
            }
            long UpdateMax(TreeNode r)
            {
                if (r == null) return 0;
                long lSum = UpdateMax(r.left);
                maxProduct = Math.Max(maxProduct, lSum * (totalSum - lSum));
                long rSum = UpdateMax(r.right);
                maxProduct = Math.Max(maxProduct, rSum * (totalSum - rSum));
                return r.val + lSum + rSum;
            }
        }


        // Time O(Max(V*KLogK,V+E)) || Recusrive Space O(V), K = avg no of flights from each airport & V = no of airports, E = no of edges/flights
        public static IList<string> FindItinerary(IList<IList<string>> tickets)
        {
            Dictionary<string, List<string>> g = new Dictionary<string, List<string>>();
            string s, d;
            foreach (var ticket in tickets)      // O(n)
            {
                s = ticket[0];
                d = ticket[1];
                // add source
                if (!g.ContainsKey(s)) g.Add(s, new List<string>());
                // add destination
                if (!g.ContainsKey(d)) g.Add(d, new List<string>());
                // add mapping source -> destination
                g[s].Add(d);
            }

            //Sort 'departing flight' in 'lexical order' at each Airport
            foreach (var listOfAirport in g.Values)  // O(V*KLogK)
                listOfAirport.Sort(new SortDepartingFlights());

            LinkedList<string> itinerary = new LinkedList<string>();
            // start journey from 'JFK' airport
            DFS("JFK");                         // O(V+E)

            return itinerary.ToList();

            // Local Func
            void DFS(string airport)
            {
                string flight;
                while (g[airport].Count > 0)
                {
                    flight = g[airport][0];
                    g[airport].RemoveAt(0);     // took the flight to lexographically smallest destination
                    DFS(flight);
                }
                itinerary.AddFirst(airport);    // after taking all the flight add curr Airport to list
            }
        }
        public class SortDepartingFlights : IComparer<string>
        {
            public int Compare(string a, string b) => a.CompareTo(b);
        }


        public static string OriginalDigitsSlow(string s)
        {
            int[] charSet = new int[26];
            // fetch all the alphabets and their frequencies in 's'
            for (int i = 0; i < s.Length; i++)
                charSet[s[i] - 'a']++;

            int[] digitSet = new int[10];
            GetDigitsCount();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < digitSet.Length; i++)
                while (digitSet[i]-- > 0)
                    sb.Append(i);

            return sb.ToString();

            // Local Func
            bool GetDigitsCount(int charUsed = 0)
            {
                if (charUsed == s.Length)
                    return true;
                else
                {
                    // check for 'zero'
                    if (charSet['z' - 'a'] > 0 && charSet['e' - 'a'] > 0 && charSet['r' - 'a'] > 0 && charSet['o' - 'a'] > 0)
                    {
                        charSet['z' - 'a']--;
                        charSet['e' - 'a']--;
                        charSet['r' - 'a']--;
                        charSet['o' - 'a']--;
                        if (GetDigitsCount(charUsed + 4))
                        {
                            digitSet[0]++;
                            return true;
                        }
                        charSet['z' - 'a']++;
                        charSet['e' - 'a']++;
                        charSet['r' - 'a']++;
                        charSet['o' - 'a']++;
                    }
                    // check for 'one'
                    if (charSet['o' - 'a'] > 0 && charSet['n' - 'a'] > 0 && charSet['e' - 'a'] > 0)
                    {
                        charSet['o' - 'a']--;
                        charSet['n' - 'a']--;
                        charSet['e' - 'a']--;
                        if (GetDigitsCount(charUsed + 3))
                        {
                            digitSet[1]++;
                            return true;
                        }
                        charSet['o' - 'a']++;
                        charSet['n' - 'a']++;
                        charSet['e' - 'a']++;
                    }
                    // check for 'two'
                    if (charSet['t' - 'a'] > 0 && charSet['w' - 'a'] > 0 && charSet['o' - 'a'] > 0)
                    {
                        charSet['t' - 'a']--;
                        charSet['w' - 'a']--;
                        charSet['o' - 'a']--;
                        if (GetDigitsCount(charUsed + 3))
                        {
                            digitSet[2]++;
                            return true;
                        }
                        charSet['t' - 'a']++;
                        charSet['w' - 'a']++;
                        charSet['o' - 'a']++;
                    }
                    // check for 'three'
                    if (charSet['t' - 'a'] > 0 && charSet['h' - 'a'] > 0 && charSet['r' - 'a'] > 0 && charSet['e' - 'a'] > 1)
                    {
                        charSet['t' - 'a']--;
                        charSet['h' - 'a']--;
                        charSet['r' - 'a']--;
                        charSet['e' - 'a'] -= 2;
                        if (GetDigitsCount(charUsed + 5))
                        {
                            digitSet[3]++;
                            return true;
                        }
                        charSet['t' - 'a']++;
                        charSet['h' - 'a']++;
                        charSet['r' - 'a']++;
                        charSet['e' - 'a'] += 2;
                    }
                    // check for 'four'
                    if (charSet['f' - 'a'] > 0 && charSet['o' - 'a'] > 0 && charSet['u' - 'a'] > 0 && charSet['r' - 'a'] > 0)
                    {
                        charSet['f' - 'a']--;
                        charSet['o' - 'a']--;
                        charSet['u' - 'a']--;
                        charSet['r' - 'a']--;
                        if (GetDigitsCount(charUsed + 4))
                        {
                            digitSet[4]++;
                            return true;
                        }
                        charSet['f' - 'a']++;
                        charSet['o' - 'a']++;
                        charSet['u' - 'a']++;
                        charSet['r' - 'a']++;
                    }
                    // check for 'five'
                    if (charSet['f' - 'a'] > 0 && charSet['i' - 'a'] > 0 && charSet['v' - 'a'] > 0 && charSet['e' - 'a'] > 0)
                    {
                        charSet['f' - 'a']--;
                        charSet['i' - 'a']--;
                        charSet['v' - 'a']--;
                        charSet['e' - 'a']--;
                        if (GetDigitsCount(charUsed + 4))
                        {
                            digitSet[5]++;
                            return true;
                        }
                        charSet['f' - 'a']++;
                        charSet['i' - 'a']++;
                        charSet['v' - 'a']++;
                        charSet['e' - 'a']++;
                    }
                    // check for 'six'
                    if (charSet['s' - 'a'] > 0 && charSet['i' - 'a'] > 0 && charSet['x' - 'a'] > 0)
                    {
                        charSet['s' - 'a']--;
                        charSet['i' - 'a']--;
                        charSet['x' - 'a']--;
                        if (GetDigitsCount(charUsed + 3))
                        {
                            digitSet[6]++;
                            return true;
                        }
                        charSet['s' - 'a']++;
                        charSet['i' - 'a']++;
                        charSet['x' - 'a']++;
                    }
                    // check for 'seven'
                    if (charSet['s' - 'a'] > 0 && charSet['e' - 'a'] > 1 && charSet['v' - 'a'] > 0 && charSet['n' - 'a'] > 0)
                    {
                        charSet['s' - 'a']--;
                        charSet['e' - 'a'] -= 2;
                        charSet['v' - 'a']--;
                        charSet['n' - 'a']--;
                        if (GetDigitsCount(charUsed + 5))
                        {
                            digitSet[7]++;
                            return true;
                        }
                        charSet['s' - 'a']++;
                        charSet['e' - 'a'] += 2;
                        charSet['v' - 'a']++;
                        charSet['n' - 'a']++;
                    }
                    // check for 'eight'
                    if (charSet['e' - 'a'] > 0 && charSet['i' - 'a'] > 0 && charSet['g' - 'a'] > 0 && charSet['h' - 'a'] > 0 && charSet['t' - 'a'] > 0)
                    {
                        charSet['e' - 'a']--;
                        charSet['i' - 'a']--;
                        charSet['g' - 'a']--;
                        charSet['h' - 'a']--;
                        charSet['t' - 'a']--;
                        if (GetDigitsCount(charUsed + 5))
                        {
                            digitSet[8]++;
                            return true;
                        }
                        charSet['e' - 'a']++;
                        charSet['i' - 'a']++;
                        charSet['g' - 'a']++;
                        charSet['h' - 'a']++;
                        charSet['t' - 'a']++;
                    }
                    // check for 'nine'
                    if (charSet['n' - 'a'] > 1 && charSet['i' - 'a'] > 0 && charSet['e' - 'a'] > 0)
                    {
                        charSet['n' - 'a']--;
                        charSet['i' - 'a']--;
                        charSet['n' - 'a']--;
                        charSet['e' - 'a']--;
                        if (GetDigitsCount(charUsed + 4))
                        {
                            digitSet[9]++;
                            return true;
                        }
                        charSet['n' - 'a']++;
                        charSet['i' - 'a']++;
                        charSet['n' - 'a']++;
                        charSet['e' - 'a']++;
                    }
                    return false;
                }
            }
        }
        // Time O(n) || Space O(1), n = len of 's'
        public static string OriginalDigits(string s)
        {
            int[] charSet = new int[26];
            // fetch all the alphabets and their frequencies in 's'
            for (int i = 0; i < s.Length; i++)
                charSet[s[i] - 'a']++;

            int[] digit = new int[10];
            digit[0] = charSet['z' - 'a'];     // only zero contains letter 'z'
            digit[2] = charSet['w' - 'a'];     // only two contains letter 'w'
            digit[6] = charSet['x' - 'a'];     // only six contains letter 'x'
            digit[8] = charSet['g' - 'a'];     // only eight contains letter 'g'
            digit[4] = charSet['u' - 'a'];     // only four contains letter 'u'
            digit[5] = charSet['f' - 'a'] - digit[4]; // only five & four contains letter 'f', we already know no of 4s hence subtracting that from no of 'f' gives us 5s
            digit[7] = charSet['v' - 'a'] - digit[5]; // only five & seven contains letter 'v', we already know no of 5s hence subtracting that from no of 'v' gives us 7s
            digit[3] = charSet['r' - 'a'] - digit[4] - digit[0];  // only four, zero & three contains letter 'r', we already know no of 4s & os hence subtracting that from no of 'r' gives us 3s
            digit[9] = charSet['i' - 'a'] - digit[8] - digit[6] - digit[5];
            digit[1] = charSet['o' - 'a'] - digit[4] - digit[2] - digit[0];


            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < digit.Length; i++)
                while (digit[i]-- > 0)  // append count of each individual digit
                    sb.Append(i);

            return sb.ToString();
        }


        // Time = O(n) || Space = O(h), n = length of 'voyage'
        public static IList<int> FlipMatchVoyage(TreeNode root, int[] voyage)
        {
            List<int> flips = new List<int>();
            int idx = 0;
            return DFS(root) ? flips : new List<int>() { -1 };

            // Local Func
            bool DFS(TreeNode r)
            {
                if (r == null) return true;
                if (r.val != voyage[idx++]) return false;
                if (idx < voyage.Length && r.left != null && r.left.val != voyage[idx])
                {
                    flips.Add(r.val);
                    return DFS(r.right) && DFS(r.left);
                }
                else
                    return DFS(r.left) && DFS(r.right);
            }
        }


        // Time O(Max(n,m)) || Space O(1), n = len of 'left' & m = len of 'right'
        public static int GetLastMoment(int n, int[] left, int[] right)
        {
            // KEY POINT : 2 Ants collinding & changing direction has same effect as 2 ants virtually crossing each other without any effect
            int leftMost = 0, rightMost = int.MaxValue;
            for (int i = 0; i < left.Length; i++)
                leftMost = Math.Max(leftMost, left[i]);     // max distance ant travelling left wud need to cover (moving towards 0)
            for (int i = 0; i < right.Length; i++)
                rightMost = Math.Min(rightMost, right[i]);  // max distance ant travelling right wud need to cover (moving towards 'n')
            return Math.Max(leftMost, n - rightMost);
        }


        public static int[] CorpFlightBookingsBruteForce(int[][] bookings, int n)
        {
            int[] reservedSeats = new int[n];
            for (int i = 0; i < bookings.Length; i++)
                for (int j = bookings[i][0]; j <= bookings[i][1]; j++)
                    reservedSeats[j - 1] += bookings[i][2];
            return reservedSeats;
        }
        // Time O(Max(n,m)) || Space O(n), m = length of 'bookings'
        public static int[] CorpFlightBookings(int[][] bookings, int n)
        {
            // using the idea of presum
            int[] res = new int[n];
            int start, last;
            for (int i = 0; i < bookings.Length; i++)
            {
                start = bookings[i][0] - 1;     // all index after 'start' will have extra passengers
                last = bookings[i][1] - 1;      // all index after 'last + 1' will not have extra passengers added above
                res[start] += bookings[i][2];
                if (last + 1 < n) res[last + 1] -= bookings[i][2];
            }

            for (int i = 1; i < n; i++)         // Compute PreFix Sum
                res[i] += res[i - 1];
            return res;
        }


        // Time O(n^3) || Space O(1)
        public static int NumTeamsBruteForce(int[] rating)
        {
            int len = rating.Length;
            return GetTeams();
            // Local Func
            int GetTeams(int i = 0, int firstNum = 0, int secondNum = 0)
            {
                if (i == len) return 0;

                int count = 0;
                if (secondNum != 0)
                {
                    if (firstNum < secondNum)
                    {
                        for (int k = i; k < len; k++)
                            if (secondNum < rating[k])
                                count++;
                        return count;
                    }
                    else // if(firstNum > secondNum)
                    {
                        for (int k = i; k < len; k++)
                            if (secondNum > rating[k])
                                count++;
                        return count;
                    }
                }
                else
                {
                    if (firstNum == 0)
                        for (int k = i; k < len - 2; k++)
                            count += GetTeams(k + 1, rating[k]);
                    else // (secondNum==0)
                        for (int k = i; k < len - 1; k++)
                            count += GetTeams(k + 1, firstNum, rating[k]);
                    return count;
                }
            }
        }
        // Time = O(N^2) || Space O(1), n = len of 'rating' array
        public static int NumTeams(int[] rating)
        {
            int len = rating.Length, count = 0, currNum, leftSmaller, leftBigger, rtSmaller, rtBigger;
            for (int i = 1; i < len - 1; i++)
            {
                currNum = rating[i];
                leftSmaller = leftBigger = rtSmaller = rtBigger = 0;
                for (int j = 0; j < i; j++)
                    if (rating[j] < currNum)
                        leftSmaller++;
                    else
                        leftBigger++;

                for (int j = i + 1; j < len; j++)
                    if (rating[j] < currNum)
                        rtSmaller++;
                    else
                        rtBigger++;

                count += (leftSmaller * rtBigger) + (leftBigger * rtSmaller);
            }
            return count;
        }


        // Time = Space = O(n)
        public static int CountLargestGroup(int n)
        {
            Dictionary<int, List<int>> ht = new Dictionary<int, List<int>>();
            int maxGrpSize = 0, maxGrpCount = 0, digitSum;
            for (int i = 1; i <= n; i++)
            {
                digitSum = GetDigitSum(i);
                if (!ht.ContainsKey(digitSum))
                    ht.Add(digitSum, new List<int>() { i });
                else
                    ht[digitSum].Add(i);

                if (ht[digitSum].Count > maxGrpCount)
                {
                    maxGrpCount = ht[digitSum].Count;
                    maxGrpSize = 1;
                }
                else if (ht[digitSum].Count == maxGrpCount)
                    maxGrpSize++;
            }
            return maxGrpSize;

            // local func
            int GetDigitSum(int num)
            {
                int sum = 0;
                while (num > 0)
                {
                    sum += num % 10;
                    num /= 10;
                }
                return sum;
            }
        }


        // Time = Space = O(n) Soln
        public static bool IsPalindrome_UsingStringBuilder(ListNode h)
        {
            StringBuilder sb = new StringBuilder();
            while (h != null)
            {
                sb.Append(h.val);
                h = h.next;
            }
            string str = sb.ToString();
            int start = 0, last = str.Length - 1;
            while (start < last)
                if (str[start++] != str[last--])
                    return false;
            return true;

        }
        // Time = Space = O(n) Soln
        public static bool IsPalindrome_UsingList(ListNode curr)
        {
            List<int> ls = new List<int>();
            while (curr != null)
            {
                ls.Add(curr.val);
                curr = curr.next;
            }
            int start = 0, last = ls.Count - 1;
            while (start < last)
                if (ls[start++] != ls[last--])
                    return false;
            return true;
        }
        // Time O(n) || Space O(1)
        public static bool IsPalindrome(ListNode h)
        {
            //  Count no of nodes in LinkedList
            //  Idea is to reach half point in List while reversing the 1st half as we traverse to reach middle
            //  if list was of odd length skip middle node
            //  now start comparing values of 2nd half & 1st half which was reversed,
            //  if at any point these 2 points values dont match return false
            // 
            //  return true at end
            ListNode prv = null, curr = null, nxt = h;
            int l = GetLen(h);
            bool moveOne = l % 2 != 0;
            l /= 2;
            // reach mid-point in LinkedList
            while (l-- > 0)
            {
                curr = nxt;
                nxt = nxt.next;
                curr.next = prv;
                prv = curr;
            }

            // if odd count of nodes move one more step frwd as middle node need not be checked
            if (moveOne)
                nxt = nxt.next;

            // Validate if Palindrome or not
            while (nxt != null)
            {
                if (nxt.val != curr.val)
                    return false;
                nxt = nxt.next;
                curr = curr.next;
            }

            return true;

            // local func
            int GetLen(ListNode t)
            {
                int size = 0;
                while (t != null)
                {
                    size++;
                    t = t.next;
                }
                return size;
            }
        }


        // Time O(m*n*k + k*l) || Space O(m*n), m = maxZeros, n = maxOnes & k = length of 'strs' array & l = avg length of string in 'strs'
        public static int FindMaxForm(string[] strs, int maxZeros, int maxOnes)
        {
            int[,] dp = new int[maxZeros + 1, maxOnes + 1];
            int zeros, ones;
            for (int i = 0; i < strs.Length; i++)   // O(k)
            {
                zeros = 0;
                for (int j = 0; j < strs[i].Length; j++)    // O(l)
                    if (strs[i][j] == '0')
                        zeros++;
                ones = strs[i].Length - zeros;

                for (int j = maxZeros; j >= zeros; j--)     // O(m)
                    for (int k = maxOnes; k >= ones; k--)   // O(n)
                        dp[j, k] = Math.Max(dp[j, k], 1 + dp[j - zeros, k - ones]);
            }
            return dp[maxZeros, maxOnes];
        }


        // Time O(nlogn) || Space O(n), n = no of sectors
        public static IList<int> MostVisited(int n, int[] rounds)
        {
            List<int> mostVisitedSectors = new List<int>();
            // most repeated sectors would always be b/w 1st sector and last sector of 'rounds' array
            int first = rounds[0], last = rounds[rounds.Length - 1];
            while (true)  // O(n)
            {
                mostVisitedSectors.Add(first);
                if (first == last) break;
                first %= n;
                first++;
            }

            mostVisitedSectors.Sort();  // O(nlogn)
            return mostVisitedSectors;
        }


        // Time O(n) || Space O(1), n = len of 'values' array
        public static int MaxScoreSightseeingPair(int[] values)
        {
            // We start with the first sight val as max value, as we move to next index we need to deduct distance to calculate maxPairScore
            // to achieve same effect we can just keep on decreasing max score found by 1 as we move to next index
            // now only thing to make sure is we keep are prv max sight score updated in case we found new sight with higher score
            int maxScore = 0, max = values[0];
            for (int i = 1; i < values.Length; i++)    // O(n)
            {
                maxScore = Math.Max(maxScore, --max + values[i]);
                max = Math.Max(max, values[i]);
            }
            return maxScore;
        }


        // Time = Space = O(n), n = len of 's'
        public static int LongestValidParenthesesWithStack(string s)
        {
            int longestParentheses = 0, lt = 0, rt = 0, lastValidStart;
            Stack<int> openingBrackets = new Stack<int>();
            while (rt < s.Length)
                if (s[rt++] == '(')     // opening
                    openingBrackets.Push(rt - 1);
                else                    // closing
                {
                    if (openingBrackets.Count > 0)  // stack not empty, update ans with last opening idx & current idx
                        longestParentheses = Math.Max(longestParentheses, rt - openingBrackets.Pop());
                    else                // if stack was empty means we have reached scenario when we found more closing brackets than opening
                    {
                        // reset Stack & update left
                        lt = rt;
                        openingBrackets.Clear();
                    }

                    // also keep updating global max reached, to cover cases of consecutive "()()()()"
                    lastValidStart = openingBrackets.Count == 0 ? lt : openingBrackets.Peek() + 1;
                    longestParentheses = Math.Max(longestParentheses, rt - lastValidStart);
                }
            return longestParentheses;
        }
        // Time O(n) | Space O(1), n = length of string 's'
        public static int LongestValidParenthesesConstantSpace(string s)
        {
            /* ALGO
            Using sliding window approach, We just need to check if the open and close bracket count is balanced or not
                if yes we take the length of current window and update our maxima

            if open bracket values is > 0 we continue on (hoping we wud soon get closing bracket to get even bigger maxima)
            if ever open brackets values go below 0 than we need to move left pointer to current index
                as even removing 1 open bracking in opening will render curr window in inbalanced state.

            once we have iterated from left=>right, repeated same from right=>left(remember now to consider
                closing as open and vice-versa) to get the correct ans after change in direction
             */
            int sLen = s.Length, longestValidParentheses = 0, left = -1, right = -1, leftBracketCounter = 0;
            while (++right < sLen)             // O(n)
            {
                leftBracketCounter += s[right] == '(' ? 1 : -1;
                // if balanced
                if (leftBracketCounter == 0)
                    longestValidParentheses = Math.Max(longestValidParentheses, right - left);
                else if (leftBracketCounter < 0)
                {
                    left = right;
                    leftBracketCounter = 0;
                }
            }
            // now check right to left
            left = sLen;
            right = sLen;
            leftBracketCounter = 0;
            while (--right >= 0)               // O(n)
            {
                leftBracketCounter += s[right] == ')' ? 1 : -1;
                // if balanced
                if (leftBracketCounter == 0)
                    longestValidParentheses = Math.Max(longestValidParentheses, left - right);
                else if (leftBracketCounter < 0)
                {
                    left = right;
                    leftBracketCounter = 0;
                }
            }
            return longestValidParentheses;
        }


        // Time O(n) || Space O(1), n = len of 'points'
        public static int MinTimeToVisitAllPoints(int[][] points)
        {
            int time = 0;
            for (int i = 1; i < points.Length; i++)
                time += Math.Max(Math.Abs(points[i - 1][0] - points[i][0]), Math.Abs(points[i - 1][1] - points[i][1]));
            return time;
        }


        // Time O(n) || Space O(1)
        public static bool IsIdealPermutation(int[] A)
        {
            /* Because the count of local should <= count of global, all we care is when local < global happens.
             * The difference between local and global is global also include nonadjacent n and j,
             * so simplify the question to for every n, find in range 0 to n-2, see if there is a element larger than A[n],
             * if it exist, we can return false directly.
             */
            int max = -1;
            for (int i = 0; i < A.Length - 2; i++)
            {
                max = Math.Max(max, A[i]);
                if (max > A[i + 2])
                    return false;
            }
            return true;
        }


        // Time O(r^2) || Space O(r), r = rowIndex
        public static IList<int> PascalsTriangleII(int rowIndex)
        {
            int[] row = new int[] { 1 }, nextRow;
            while (row.Length - 1 < rowIndex)
            {
                nextRow = new int[row.Length + 1]; // next row always has 1 more len than last
                nextRow[0] = nextRow[row.Length] = 1; // 1st & last index value is always 1
                for (int i = 1; i < nextRow.Length - 1; i++) // compute intermediate value from row above
                    nextRow[i] = row[i - 1] + row[i];
                row = nextRow;
            }
            return row;
        }


        // Time = Space = O(r*c), r = no of rows in nums, c = max no of columns in any nums[n]
        public static int[] DiagonalTraverseII(IList<IList<int>> nums)
        {
            List<int> ans = new List<int>();
            int r = 0, currRow, currCol, maxCol = 0;
            while (r < nums.Count)             // traverse thru all the rows
            {
                maxCol = Math.Max(maxCol, nums[r].Count);// update max columns we need to cover
                currRow = r++;
                currCol = 0;
                while (currRow >= 0 && currCol < maxCol)     // traverse & add current diagonal elements
                    if (currCol++ < nums[currRow--].Count)
                        ans.Add(nums[currRow + 1][currCol - 1]);
            }
            // now we only need to add elements of last row from 2nd column onwards
            for (int i = 1; i < maxCol; i++)
            {
                currRow = r - 1;
                currCol = i;
                while (currRow >= 0 && currCol < maxCol)
                    if (currCol++ < nums[currRow--].Count)
                        ans.Add(nums[currRow + 1][currCol - 1]);
            }
            return ans.ToArray(); ;
        }
        // Time = Space = O(n), n = total no of elements in 2D zigzag array 'nums'
        public static int[] DiagonalTraverseIIFaster(IList<IList<int>> nums)
        {
            List<Stack<int>> LST = new List<Stack<int>>();
            for (int i = 0; i < nums.Count; i++)        // traverse thru all the rows
            {
                while (LST.Count <= i + nums[i].Count)  // add extra Stack to List if required
                    LST.Add(new Stack<int>());

                for (int j = 0; j < nums[i].Count; j++) // add current col elements to appropriate diagonal/stack
                    LST[i + j].Push(nums[i][j]);
            }
            // collect and flatten our List of Stacks to form final ans
            List<int> ans = new List<int>();
            for (int i = 0; i < LST.Count; i++)
                ans.AddRange(LST[i]);

            return ans.ToArray();
        }


        // Time O(n) || Space O(1)
        public static int MinimumOperationsToMakeArrayEqual(int n)
        {
            /* 1	3	5	7	9	11
             * n = even median = 6
             * 5	3	1
             * we now see 'min no of operations' to convert all numbers to median for even 'n' values
             * is just sum of nums[n] in the range 0<= n< (n+1)/2
             * 
             * 1	3	5	7	9
             * n = odd median = 5
             * 4	2	0
             * we now see 'min no of operations' to convert all numbers to median for odd 'n' values
             * is just sum of nums[n] in the range 0<= n< (n+1)/2   -  (n+1)/2;
             */
            int operation = 0, i = 0, currNum = 1;
            while (i++ < (n + 1) / 2)        // O(n/2)
            {
                operation += currNum;
                currNum += 2;
            }
            return operation - (n % 2 == 0 ? 0 : --i);
        }


        // Time = O(Max(n,ulogu)) || Space = O(n), u = total no of unique numbers in 'arr', n = len of 'arr'
        public static int FindLeastNumOfUniqueInts(int[] arr, int k)
        {
            Dictionary<int, int> numFreq = new();
            for (int i = 0; i < arr.Length; i++)                       // O(n)
                if (numFreq.TryGetValue(arr[i], out int freq))
                    numFreq[arr[i]] = freq + 1;
                else numFreq[arr[i]] = 1;

            // sort the numbers basis on their freq (in increasing order)
            var sortedUnqNums = (from nums in numFreq           // O(nlogn)
                                 orderby nums.Value
                                 select nums).ToList();
            int idx = -1;
            while (++idx < arr.Length)                           // O(n)
            {
                k -= sortedUnqNums[idx].Value;
                // 'k' nums removed now break out of the loop
                if (k < 0) break;
            }
            return sortedUnqNums.Count - idx;
            /* via SortedDictionary
            Dictionary<int, int> numFreq = new Dictionary<int, int>();
            for (int n = 0; n < arr.Length; n++)                // O(n)
                if (!numFreq.ContainsKey(arr[n]))
                    numFreq.Add(arr[n], 1);
                else
                    numFreq[arr[n]]++;

            SortedDictionary<int, List<int>> sameFreqNums = new SortedDictionary<int, List<int>>();
            foreach (var kvp in numFreq)
                if (!sameFreqNums.ContainsKey(kvp.Value))       // O(ulogu)
                    sameFreqNums.Add(kvp.Value, new List<int>() { kvp.Key });
                else
                    sameFreqNums[kvp.Value].Add(kvp.Key);

            int uniqueNumsRemoved = 0;
            foreach (var listOfNums in sameFreqNums)            // O(n)
                foreach (var num in listOfNums.Value)
                {
                    if (listOfNums.Key <= k) uniqueNumsRemoved++;
                    k -= listOfNums.Key;
                    if (k < 1) return numFreq.Count - uniqueNumsRemoved;
                }
            return numFreq.Count - uniqueNumsRemoved;
            */
        }


        // Time O((n-m)^2) || Space O(1)
        public static bool DetectKPatternOfLenM(int[] arr, int m, int k)
        {
            int start, repeatStart, count;
            for (start = 0; start <= arr.Length - m; start++)       // O(n-m)
            {
                count = 1;
                // check for Consecutive repeatation in remaining arr
                repeatStart = start + m;
                while (repeatStart <= arr.Length - m && Match())    // O(n-2m)
                {
                    if (++count == k) return true;
                    repeatStart += m;
                }
            }
            return false;
            // local func
            bool Match()
            {
                int i = start, j = repeatStart;
                while (i < start + m)
                    if (arr[i++] != arr[j++])
                        return false;
                return true;
            }
        }


        // Time O(n) || Space O(1)
        public static int MinSteps(string s, string t)
        {
            int[] charSet = new int[26];
            for (int i = 0; i < s.Length; i++)          // O(n)
            {
                charSet[s[i] - 'a']++;      // increament for 's'
                --charSet[t[i] - 'a'];      // decreament for 't'
            }
            int count = 0;
            for (int i = 0; i < charSet.Length; i++)    // O(26)
                if (charSet[i] > 0)         // count remaing characters from 's' we missed in 't'
                    count += charSet[i];
            return count;
        }


        // Time O(n) || Space O(1)
        public static int BulbSwitcherIV(string target)
        {
            char curStateOfRemaingBulbs = '0';
            int flips = 0;
            for (int i = 0; i < target.Length; i++)
                if (target[i] != curStateOfRemaingBulbs)   // 'n'th Bulb state doesnt matches, we need to flip the switch fr all bulbs after 'n'th index
                {
                    flips++;
                    curStateOfRemaingBulbs = curStateOfRemaingBulbs == '0' ? '1' : '0';
                }
            return flips;
        }


        // Time O(n) || Space O(1)
        public static int MaxVowelsInSubstringOfLengthK(string s, int k)
        {
            int maxVowels = 0, currVowels = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (IsVowel(s[i]))       // Increament the count if new 'char in the sliding window is vowel'
                    currVowels++;
                if (i < k - 1) continue;

                maxVowels = Math.Max(maxVowels, currVowels);
                if (maxVowels == k)
                    return k;

                if (IsVowel(s[1 + i - k]))   // Decreament the count if 'char leaving the sliding window was vowel'
                    currVowels--;
            }
            return maxVowels;
            bool IsVowel(char ch) => ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';
        }


        // Time O(n*l) || Space O(1), n = len of array 'words' & l = avg len of 'word'
        public static bool IsAlienSorted(string[] words, string order)
        {
            int[] alienOrder = new int[26];
            for (int i = 0; i < order.Length; i++)
                alienOrder[order[i] - 'a'] = i;

            for (int i = 1; i < words.Length; i++) // O(n)
                if (!ValidOrder(words[i - 1], words[i]))
                    return false;
            return true;
            // local func
            bool ValidOrder(string last, string curr)   // O(l), l = avg length of 'word'
            {
                if (last.Length <= curr.Length)
                {
                    for (int i = 0; i < last.Length; i++)
                        if (alienOrder[last[i] - 'a'] < alienOrder[curr[i] - 'a'])
                            return true;
                        else if (alienOrder[last[i] - 'a'] > alienOrder[curr[i] - 'a'])
                            return false;
                    return true;
                }
                else
                {
                    for (int i = 0; i < curr.Length; i++)
                        if (alienOrder[last[i] - 'a'] < alienOrder[curr[i] - 'a'])
                            return true;
                        else if (alienOrder[last[i] - 'a'] > alienOrder[curr[i] - 'a'])
                            return false;
                    return false;
                }
            }
        }


        // Time O(n) || Space O(1)
        public static int MinSwapToMakeStringsEqual(string s1, string s2)
        {
            int notMatchingX = 0, notMatchingY = 0;
            for (int i = 0; i < s1.Length; i++)
                if (s1[i] != s2[i])
                    if (s1[i] == 'x')
                        notMatchingX++;
                    else
                        notMatchingY++;
            // If total pairs are not even, then we cannot make strings same
            if ((notMatchingX % 2 + notMatchingY % 2) % 2 != 0)
                return -1;
            return notMatchingX / 2 + notMatchingY / 2 + notMatchingX % 2 + notMatchingY % 2;
        }


        // Time O(nlogn) || Space O(1)
        public static int MinDifference(int[] nums)
        {
            if (nums.Length < 5) return 0;
            Array.Sort(nums);

            int l = nums.Length;
            int diff = nums[l - 1] - nums[0];
            // try all combinations from removing 3 nums from left and 0 from rt
            // upto 0 nums from left and all 3 removed from right
            for (int i = 0; i <= 3; i++)
                diff = Math.Min(diff, nums[-1 + l - i] - nums[3 - i]);
            return diff;
        }


        // Time = Space = O(r*c)
        public static int LongestIncreasingPath(int[][] matrix)
        {
            int[][] directionArr = { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int rows = matrix.Length, cols = matrix[0].Length, longest = -1;
            int[,] dp = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    longest = Math.Max(DFS(i, j, -1), longest);
            return longest;

            // local helper func
            int DFS(int r, int c, int lastNum)
            {
                if (r < 0 || r >= rows || c < 0 || c >= cols || matrix[r][c] <= lastNum) return 0;
                // if sub-problem is already precomputed than return the longest possible path from curr cell
                if (dp[r, c] != 0) return dp[r, c];

                int longestPathFromCurrentCell = 0;
                // iterate in all 4 possible directions
                foreach (var direction in directionArr)
                    longestPathFromCurrentCell = Math.Max(longestPathFromCurrentCell, DFS(r + direction[0], c + direction[1], matrix[r][c]));

                return dp[r, c] = 1 + longestPathFromCurrentCell;
            }

            //int row = matrix.Length, col = matrix[0].Length, longest = 0;
            //int[,] dp = new int[row, col];
            //for (int n = 0; n < row; n++)
            //    for (int j = 0; j < col; j++)
            //        longest = Math.Max(longest, DFS(n, j));
            //return longest;
            //int DFS(int r, int c)
            //{
            //    if (dp[r, c] != 0) return dp[r, c];

            //    int max = 0;
            //    // up
            //    if (r - 1 >= 0 && matrix[r - 1][c] > matrix[r][c])
            //        max = Math.Max(max, DFS(r - 1, c));
            //    // down
            //    if (r + 1 < row && matrix[r + 1][c] > matrix[r][c])
            //        max = Math.Max(max, DFS(r + 1, c));
            //    // lt
            //    if (c - 1 >= 0 && matrix[r][c - 1] > matrix[r][c])
            //        max = Math.Max(max, DFS(r, c - 1));
            //    // rt
            //    if (c + 1 < col && matrix[r][c + 1] > matrix[r][c])
            //        max = Math.Max(max, DFS(r, c + 1));

            //    return dp[r, c] = max + 1;
            //}
        }


        // Time = Space = O(n)
        public static string BreakPalindrome(string palindrome)
        {
            int len = palindrome.Length, i = -1;
            if (len < 2) return "";

            bool replaced = false;
            StringBuilder sb = new StringBuilder();
            // try replacing a word
            while (++i < len - 1)
                if (!replaced && palindrome[i] != 'a')
                {
                    if (i != len / 2 || len % 2 == 0)
                    {
                        sb.Append('a');
                        replaced = true;
                    }
                    else
                        sb.Append(palindrome[i]);
                }
                else
                    sb.Append(palindrome[i]);

            // Append Last Character
            if (!replaced) sb.Append(palindrome[i] != 'a' ? 'a' : 'b');
            else sb.Append(palindrome[i]);

            return sb.ToString();
        }
        // Time = Space = O(n)
        public static string BreakPalindrome_Clean(string palindrome)
        {
            // any string of length <=1 is always palindrome
            if (palindrome.Length < 2) return "";

            char[] nonPali = palindrome.ToCharArray();
            int lt = -1, halfPoint = palindrome.Length / 2;
            // replace the 1st non-'a' to 'a' to make the smallest string which is non-palindrome
            while (++lt < halfPoint)
                if (palindrome[lt] != 'a')
                {
                    nonPali[lt] = 'a';
                    return new string(nonPali);
                }

            // if all the chars in the 1st half have only 'a' then replace the last char to 'b'
            nonPali[palindrome.Length - 1] = 'b';
            return new string(nonPali);
        }


        // Time O(n) || Space O(1), n = length of 'text'
        public static string HTMLEntityParser(string text)
        {
            Dictionary<string, char> d = new Dictionary<string, char>();
            d["&quot;"] = '"';
            d["&apos;"] = '\'';
            d["&amp;"] = '&';
            d["&gt;"] = '>';
            d["&lt;"] = '<';
            d["&frasl;"] = '/';

            int l = text.Length;
            StringBuilder sb = new StringBuilder(), curr = new StringBuilder();
            for (int i = 0; i < l; i++)
                if (text[i] != '&' || (i + 1 < l && text[i + 1] == '&'))
                    sb.Append(text[i]);
                else // check if its a special character by taking into all characters till ';' character
                {
                    while (i < l && text[i] != ';')
                        curr.Append(text[i++]);

                    // we havent read the entire input 'text' 
                    if (i < l) curr.Append(';');

                    if (d.ContainsKey(curr.ToString()))
                        sb.Append(d[curr.ToString()]);  // this was special character
                    else
                        sb.Append(curr.ToString());     // not special add to final ans as it is

                    curr.Clear();                       // clear for next iteration
                }

            return sb.ToString();
        }


        // Time = Space = O(n+m), n = len of sentence1, m = len of sentence2
        public static bool AreSentencesSimilar(string sentence1, string sentence2)
        {
            var s1 = sentence1.Split(' ');
            var s2 = sentence2.Split(' ');

            if (s1.Length < s2.Length)   // we want to keep l1 longer/equal to l2
            {
                var temp = s1;
                s1 = s2;
                s2 = temp;
            }
            int i = 0, j = 0, l1 = s1.Length, l2 = s2.Length;
            while (i < l2 && s1[i] == s2[i])
                i++;
            while (l2 - 1 - j >= 0 && s1[l1 - 1 - j] == s2[l2 - 1 - j])
                j++;

            if (i == 0 && j == 0) return false;  // no match found from either end
            if (i + j < l2) return false;   // not all characters matched from smaller sentence
            return true;
        }


        // Time O(n) || Space O(1)
        public static int MaxDiffFromChangingAnInteger(int num)
        {
            string n = num.ToString();
            int l = n.Length;
            int min = Make(true);
            int max = Make(false);
            return max - min;
            // local func
            int Make(bool makeMin)
            {
                int val = 0, swapWith = 9;
                char toSwap = ' ';
                for (int i = 0; i < l; i++)
                    if (toSwap == ' ') // still looking for the digit to swap
                    {
                        if (makeMin)
                        {
                            // try converting 1st digit to '1' if its already '1'
                            if (i == 0 && n[i] != '1')
                            {
                                toSwap = n[i];
                                swapWith = 1;
                                val = swapWith;
                            }
                            // try converting any next digit to 0 which is not already 0 or 1
                            else if (i > 0 && n[i] != '0' && n[i] != '1')
                            {
                                toSwap = n[i];
                                swapWith = 0;
                                val = val * 10 + swapWith;
                            }
                            else
                                val = val * 10 + (n[i] - '0');
                        }
                        else // making Max
                        {
                            // try converting any digit to 9 which is not already 9
                            if (n[i] != '9')
                            {
                                toSwap = n[i];
                                val = val * 10 + swapWith;
                            }
                            else
                                val = val * 10 + (n[i] - '0');
                        }
                    }
                    else if (n[i] == toSwap)
                        val = val * 10 + swapWith;
                    else
                        val = val * 10 + (n[i] - '0');

                return val;
            }
        }


        // Time O(n) || Space O(1)
        public static int[] BeautifulArrangementII(int n, int k)
        {
            // Intution based upon a decent guys observation https://youtu.be/atTsZ0PhuqE
            int smallest = 1, largest = k + 1, i = 0;
            int[] ans = new int[n];
            while (i <= k)      // fill 1st 'K+1' indicies
                ans[i] = (i++ % 2) == 0 ? smallest++ : largest--;
            while (i < n)       // remaining values
                ans[i] = ++i;
            return ans;
        }


        // Time O(n) || Space O(1)
        public static int CountHomogenous(string s)
        {
            int i = 0, l = s.Length, mod = 1000000007;
            double ans = 0, count = 1;
            while (++i < l)
                if (s[i - 1] == s[i])
                    count++;
                else
                {
                    ans = (ans + ((((count + 1) * count) / 2) % mod)) % mod;
                    count = 1;
                }
            ans = (ans + ((((count + 1) * count) / 2) % mod)) % mod;
            return (int)ans;
        }


        // Time = Space = O(n)
        public static int TotalFruitIntoBaskets(int[] tree)
        {
            Dictionary<int, int> typeCount = new Dictionary<int, int>();
            int i = -1, j = 0, l = tree.Length, max = 0;
            while (++i < l)
            {
                if (typeCount.ContainsKey(tree[i]))
                    typeCount[tree[i]]++;
                else
                    typeCount[tree[i]] = 1;

                // more than 2 variant found 
                if (typeCount.Count > 2)
                {
                    max = Math.Max(max, i - j);
                    while (typeCount.Count > 2)
                        if (--typeCount[tree[j++]] == 0)
                        {
                            typeCount.Remove(tree[j - 1]);
                            break;
                        }
                }
            }
            return Math.Max(max, i - j);
        }


        // Time O(n) || Space O(1)
        public static int MaximizeDistToClosestPerson(int[] seats)
        {
            int i = -1, j = 0, maxD = 1, l = seats.Length;
            while (++i < l)
                if (seats[i] == 0) // empty seat found
                {
                    // count no of continous empty seats;
                    j = i;
                    while (j < l && seats[j] == 0)
                        j++;
                    // update min Distance possible
                    if (j == l || i == 0)    // special case of left or right end, so Alex can be seated at opp end
                        maxD = Math.Max(maxD, j - i);
                    else        // found a seat which is not empty, hence min distance is rt in between jth & ith index
                        maxD = Math.Max(maxD, (1 + j - i) / 2);
                    i = j;
                }
            return maxD;
        }


        // Time O(n) || Space O(k)
        public static bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (k == 0) return false;
            HashSet<int> numFreq = new HashSet<int>();
            int i = -1;
            while (++i < nums.Length)
            {
                if (!numFreq.Contains(nums[i]))
                    numFreq.Add(nums[i]);
                else // found a duplicate num at a distance which is <= 'k'
                    return true;

                if (i >= k)    // remove the num which is not part of sliding window of len 'k' now
                    numFreq.Remove(nums[i - k]);
            }
            return false;
        }


        // Time O(n^3) || Space O(1)
        public static int WaysToSplit_IterativeBruteForce(int[] nums)
        {
            int left = 0, mid, rt, l = nums.Length, count = 0;
            for (int i = 0; i < l - 2; i++)
            {
                left += nums[i];
                mid = 0;
                for (int j = i + 1; j < l - 1; j++)
                {
                    mid += nums[j];
                    rt = 0;
                    if (left > mid) continue;
                    else
                        for (int k = j + 1; k < l; k++)
                        {
                            rt += nums[k];
                            if (mid > rt) continue;
                            else { count++; break; }
                        }
                }
            }
            return count;
        }
        // Time O(nlogn) || Space O(n), n = length of 'nums'
        public static int WaysToSplit(int[] nums)
        {
            /* Since we need to divide the arr into 3 parts so that 
             * sum of left <= sum of middle <= sum of right
             * 
             * first optimization to save a lot of time is calculating the prefix sum in O(n) time
             * which gives us sum of values b/w any 2 index in just O(1) time.
             * 
             * now our problem can be re-written as below
             * find n & j index such that
             * prefixSum[n] <= prefixSum[j]-prefixSum[n] <= prefixSum[n-1]-prefixSum[j]
             * 
             * so bascially we need to iterate n thru [1..n-2] we need atleast 1 values for mid & rt sub-array
             * & for each index n, we need to find l & r such that
             *      min index l such that sum of mid-array is greater or equal to left-sub-array)
             *      max index r such that sum of mid-array is smaller than or equal to sum of rt-sub-array)
             * 
             * since prefix sum is sorted we can use binary search to find our 'l' & 'r' index
             * at the end we return count % mod.
             */
            int n = nums.Length, count = 0, l, r;
            long leftSum, remainingSum;

            long[] prefixSum = new long[n + 1];
            prefixSum[0] = nums[0];
            for (int i = 1; i < n; i++)
                prefixSum[i] = prefixSum[i - 1] + nums[i];

            for (int i = 0; i < n - 1; i++)
            {
                leftSum = prefixSum[i];
                remainingSum = prefixSum[n - 1] - leftSum;
                if (prefixSum[n - 1] < leftSum * 3) break;    // to divide array into 3 equal parts total sum should be >= thrice the sum of left-sub-array

                l = SearchLeft(i + 1, leftSum);             // min index l such that sum of mid-array is greater or equal to left-sub-array)
                r = SearchRight(i + 1, remainingSum / 2);   // max index r such that sum of mid-array is smaller than or equal to sum of rt-sub-array)

                count += 1 + r - l;
                count %= 1000000007;
            }
            return (int)count;


            // local func
            int SearchLeft(int idx, long target)
            {
                int mid, left = idx, rt = n - 2;
                while (left < rt)
                {
                    mid = left + ((rt - left) / 2);
                    if (prefixSum[mid] - leftSum >= target)//prefixSum is '1' index
                        rt = mid;
                    else
                        left = mid + 1;
                }
                return left;
            }
            int SearchRight(int idx, long target)
            {
                int mid, left = idx, rt = n - 2;
                while (left < rt)
                {
                    mid = left + (1 + rt - left) / 2;
                    if (prefixSum[mid] - leftSum <= target) //prefixSum is '1' index
                        left = mid;
                    else
                        rt = mid - 1;
                }
                return rt;
            }
        }


        // Time O(l) || Space O(l) required for resultant string, l = length of input string 'riddle'
        public static string ReplaceMissingCharacters(string riddle)
        {
            HashSet<char> avoid = new HashSet<char>();
            int i = -1, l = riddle.Length;
            char[] ans = new char[l];
            while (++i < l)                                     // O(l)
                if (riddle[i] == '?')  // needs replacemenet
                {
                    avoid.Clear();
                    //don't use char used for last index
                    if (i - 1 >= 0)
                        avoid.Add(ans[i - 1]);
                    //don't use char present at next index
                    if (i + 1 < l && riddle[i + 1] != '?')
                        avoid.Add(riddle[i + 1]);

                    // a Random index b/w 0 & 25 can also be generated untill we find a 'char' which can be used
                    for (int j = 0; j < 26; j++)                // O(26) ~O(1)
                        if (!avoid.Contains((char)(j + 'a')))
                        {
                            ans[i] = (char)(j + 'a');
                            break;
                        }
                }
                else
                    ans[i] = riddle[i];

            return new string(ans);
        }


        // Time O(r^2 * c) | Space O(c), r = no of rows & c = no of cols in matrix 2D array
        public static int NumSubmatrixSumTarget(int[][] matrix, int target)
        {
            int rows = matrix.Length, cols = matrix[0].Length, result = 0, rowSum, runningSum, targetSum;
            // get the prefix sum vertially (top-bottom) for each column
            for (int r = 1; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r][c] += matrix[r - 1][c];

            Dictionary<int, int> sumFreq = new();
            for (int r1 = 0; r1 < rows; r1++)          // O(r)
                for (int r2 = r1; r2 < rows; r2++)     // O(r)
                {
                    // find the Prefix sum for each cth col in this sub-matrix
                    int[] combinedSum = new int[cols];
                    for (int c = 0; c < cols; c++)     // O(c)
                    {
                        rowSum = matrix[r2][c] - (r1 > 0 ? matrix[r1 - 1][c] : 0);
                        combinedSum[c] = rowSum + (c > 0 ? combinedSum[c - 1] : 0);
                    }
                    // count 'Sub-Matrix' whose sum match 'target'
                    // above loop iterating from 0...Cols can be merged with below
                    // but for the sake of simplicity n have kept it seperate
                    sumFreq.Clear();
                    sumFreq[0] = 1;       // base case
                    for (int c = 0; c < cols; c++)     // O(c)
                    {
                        runningSum = combinedSum[c];
                        targetSum = runningSum - target;
                        if (sumFreq.TryGetValue(targetSum, out int freq))
                            result += freq;
                        // update running sum Freq
                        if (sumFreq.TryGetValue(runningSum, out int val))
                            sumFreq[runningSum] = 1 + val;
                        else sumFreq[runningSum] = 1;
                    }
                }
            return result;
            ///* First calculate the Prefix sum for each row in the the entire 2D array so we can find the sum of any sub-matrix in O(1)
            // * 
            // * Now iterate thru all the combinations of cols .n.e, c1..c2 where 0 <= c1 < cols & c1 <= c2 <= cols
            // * for each of the sub-matrix b/w above c1..c2,
            // * we create seperate 1D array where each rth index is holding "total row sum" for that row
            // * and now try finding out how many sub-matrix have sum equal to target
            // * Time O(col*col*row) || Space O(col*row)
            // */
            //int rows = matrix.Length, cols = matrix[0].Length, n, j, result = 0;
            //// calculate prefix Sum for 2D array
            //int[,] prefixSum = new int[rows, cols];
            //for (n = 0; n < rows; n++)
            //    for (j = 0; j < cols; j++)
            //        prefixSum[n, j] = (j > 0 ? prefixSum[n, j - 1] : 0) + matrix[n][j];

            //Dictionary<int, int> sumFrequency = new Dictionary<int, int>();
            //// Iterate thru all columns combination
            //for (int c1 = 0; c1 < cols; c1++)
            //{
            //    int[] combinedRowSum = new int[rows];
            //    for (int c2 = c1; c2 < cols; c2++)
            //    {
            //        // find the total sum for each rth row in this sub-matrix
            //        for (int r = 0; r < rows; r++)
            //            combinedRowSum[r] = prefixSum[r, c2] - (c1 > 0 ? prefixSum[r, c1 - 1] : 0);


            //        // count 'Sub-Matrices' whose sum match 'target'
            //        sumFrequency.Clear();
            //        sumFrequency.Add(0, 1);     // base case
            //        int runningSum = 0, targetSum;

            //        for (int r = 0; r < rows; r++)
            //        {
            //            runningSum += combinedRowSum[r];
            //            targetSum = runningSum - target;

            //            if (sumFrequency.ContainsKey(targetSum))
            //                result += sumFrequency[targetSum];

            //            if (!sumFrequency.ContainsKey(runningSum))
            //                sumFrequency[runningSum] = 1;
            //            else
            //                sumFrequency[runningSum]++;
            //        }
            //    }
            //}
            //return result;
        }


        // Time O(log(MaxDate) * n) || Space O(1), MaxDate = 10^9 & n = length of 'bloomDay'
        public static int MinDaysToMakeFlowerBouquets(int[] bloomDay, int m, int k)
        {
            /* 1st thing to check if there are enouf flowers to make 'm' bouquets of 'k' flower each, if not return -1
             * 
             * Now we know the ans for no of days to create 'm' bouquets can be anywhere in b/w 1 day to largest day present in 'bloomDay' array
             * our objective is to find the min date at which we can create 'm' bouquets.
             * 
             * for this we can do a binary search which will find the middle date and we check for below:
             *      if we can make all required bouquets than our rt boundry can be decreased till 'mid'
             *      else we need to update our left boundry to mid+1
             * we keep repeating above step till we left < rt
             * and return rt at end as answer.
             */
            if (m * k > bloomDay.Length) return -1;
            int left = 1, mid, rt = 1000000000; // rt can be either max possible value of bloomDay[n] or max value in present in array
            while (left < rt)                   // O(log(10^9))
            {
                mid = left + (rt - left) / 2;
                if (AllBouquetsCanBeMade())     // O(n)
                    rt = mid;
                else
                    left = mid + 1;
            }
            return rt;
            // local func
            bool AllBouquetsCanBeMade()
            {
                int bouquetsMadeSoFar = 0, continousFlowersFound = 0;
                for (int i = 0; i < bloomDay.Length; i++)
                    if (bloomDay[i] > mid)
                        continousFlowersFound = 0;
                    else if (++continousFlowersFound == k)
                    {
                        if (++bouquetsMadeSoFar == m) return true;
                        continousFlowersFound = 0;
                    }
                return false;
            }
        }



        // Time O(log10001 * n) || Space O(1), n = length of 'nums'
        public static int MinStartValue(int[] nums)
        {
            // since the we know at max array will have 100 elements each with worst val being -100,
            // we know if we start with 100 * 101 will definatly pass the check,
            // now we can use binary search to reduce our search space in 1 / 2 with each step.
            // if mid val passes Check set maxV to mid
            // else update minV to mid +1

            int minV = 1, maxV = 10001, mid;
            while (minV < maxV)         // O(log(10001))
            {
                mid = (minV + maxV) / 2;
                if (StepSumCheck())     // O(n)
                    maxV = mid;
                else
                    minV = mid + 1;
            }
            return maxV;

            // local func
            bool StepSumCheck()
            {
                int stepSum = mid;
                for (int i = 0; i < nums.Length; i++)
                {
                    stepSum += nums[i];
                    if (stepSum < 1) return false;
                }
                return true;
            }
        }
        // Time O(n) || Space O(1), n = length of 'nums'
        public static int MinStartValueFaster(int[] nums)
        {
            int currSum = 0, minStepSum = 0;
            foreach (var n in nums)
            {
                currSum += n;
                minStepSum = Math.Min(minStepSum, currSum);
            }
            return 1 - minStepSum;
        }


        // Time O(target) || Space O(n)
        public static int CombinationSum4(int[] nums, int target)
        {
            Dictionary<int, int> ways = new Dictionary<int, int>();
            SortedSet<int> set = new SortedSet<int>(nums);
            ways[0] = 1;    // base value
            return GetWays(target);

            // local func
            int GetWays(int t)
            {
                if (ways.ContainsKey(t)) return ways[t];

                int result = 0;
                foreach (var n in set)
                {
                    if (n > t) break;
                    result += GetWays(t - n);
                }
                return ways[t] = result;
            }
        }



        public static IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            CombiSum(0, 0, new List<int>());
            return ans;

            void CombiSum(int idx, int currSum, List<int> st)
            {
                if (currSum == target)
                {
                    ans.Add(st.ToList()); // add current combination to the ans, use ToList() here as directly adding list will modify final ans when list is later modified
                    return;
                }
                if (idx == candidates.Length || currSum > target) return;

                var s = st.ToList();
                while (currSum <= target)
                {
                    CombiSum(idx + 1, currSum, s);    // 1st move frwd without adding anything
                    s.Add(candidates[idx]);
                    currSum += candidates[idx];
                }
            }

            #region 1st Approach
            //List<IList<int>> res = new List<IList<int>>();
            //GetCombo(new List<int>(), target);
            //return res;
            //// local func
            //void GetCombo(List<int> curr, int t, int n = 0)
            //{
            //    if (t < 0) return;
            //    else if (t == 0) res.Add(new List<int>(curr));
            //    else
            //        for (int idx = n; idx < candidates.Length; idx++)
            //        {
            //            curr.Add(candidates[idx]);
            //            GetCombo(curr, t - candidates[idx], idx);
            //            curr.RemoveAt(curr.Count - 1);
            //        }
            //}
            #endregion
        }


        // Time O(n) || Space O(h) || Iterative
        public static int KthSmallestInBST(TreeNode r, int k)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            while (true)
            {
                while (r != null)
                {
                    st.Push(r);
                    r = r.left;
                }
                r = st.Pop();
                if (--k == 0) return r.val;
                r = r.right;
            }
            return -1;
        }


        // Time O(n) || Space O(1)
        public static int FindLengthOfShortestSubarray(int[] arr)
        {
            int l = arr.Length, peak = 0, valley = l - 1;
            // find 1st peak from start
            while (peak < l - 2 && arr[peak] <= arr[peak + 1])
                peak++;

            // find 1st valley from end
            while (valley > Math.Max(0, peak) && arr[valley - 1] <= arr[valley])
                valley--;

            if (peak >= valley) return 0;    // all nums are in non-decreasing order

            // set starting value of ans as min of removing either all nums after peek or all nums before valley
            int i = 0, j = valley, ans = Math.Min(l - peak - 1, j);
            while (i <= peak && j < l)
                if (arr[i] <= arr[j])
                {
                    ans = Math.Min(ans, j - i - 1);
                    i++;
                }
                else
                    j++;
            return ans;
        }


        // Time O(n) || Space O(1)
        public static int MaximumProductOfThreeNums(int[] nums)
        {
            int min1, min2, max1, max2, max3;
            min1 = min2 = int.MaxValue;
            max1 = max2 = max3 = int.MinValue;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < min1)
                {
                    min2 = min1;
                    min1 = nums[i];
                }
                else if (nums[i] < min2)
                    min2 = nums[i];

                if (nums[i] > max1)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = nums[i];
                }
                else if (nums[i] > max2)
                {
                    max3 = max2;
                    max2 = nums[i];
                }
                else if (nums[i] > max3)
                    max3 = nums[i];
            }
            return Math.Max(min1 * min2 * max1, max1 * max2 * max3);
        }


        // Time O(n), n = no of elements in 'triangle'
        public static int TriangleMinPathSum(IList<IList<int>> triangle)
        {
            int smallestSum = Int32.MaxValue;
            var len = triangle.Count;
            for (int i = 1; i < len; i++)    // iterate thru all the Lists in Triangle and calculate min value possible at eaech index in that List
            {
                var listLen = triangle[i].Count;
                for (int j = 0; j < listLen; j++)   // update value for each index of current row based upon min value we can find in row above
                {
                    if (j == 0)
                        triangle[i][j] += triangle[i - 1][0];
                    else if (j == listLen - 1)
                        triangle[i][j] += triangle[i - 1][j - 1];
                    else
                        triangle[i][j] += Math.Min(triangle[i - 1][j - 1], triangle[i - 1][j]);
                }
            }
            for (int i = 0; i < triangle[len - 1].Count; i++)
                smallestSum = Math.Min(smallestSum, triangle[len - 1][i]);
            return smallestSum;

        }
        // Time O(n), n = no of elements in 'triangle'
        public static int TriangleMinPathSum_DP(IList<IList<int>> triangle)
        {
            int n = triangle.Count;
            Dictionary<string, int> dp = new Dictionary<string, int>();
            return GetMin(0, 0);
            // local func
            int GetMin(int r, int i)
            {
                if (r == n) return 0;
                if (i > r) return int.MaxValue;

                string key = r + "," + i;
                if (dp.ContainsKey(key)) return dp[key];

                return dp[key] = triangle[r][i] + Math.Min(GetMin(r + 1, i), GetMin(r + 1, i + 1));
            }
        }


        // Time O(n) || Space O(m), n = no of total bricks in the wall & m = no of unique brick-checkpoints
        public static int LeastCrossedBricks(IList<IList<int>> wall)
        {
            int l = wall.Count, minCrossedBricks = l;
            Dictionary<int, int> preFixSumFreq = new Dictionary<int, int>();

            // compute prefix sum for each row & add final in SortedDictionary
            for (int i = 0; i < l; i++)
                // Skip right most point as it should not be included as stated in probl.
                for (int j = 0; j < wall[i].Count - 1; j++)
                {
                    wall[i][j] += j > 0 ? wall[i][j - 1] : 0;
                    if (!preFixSumFreq.ContainsKey(wall[i][j])) preFixSumFreq.Add(wall[i][j], 1);
                    else preFixSumFreq[wall[i][j]]++;

                    minCrossedBricks = Math.Min(minCrossedBricks, l - preFixSumFreq[wall[i][j]]);
                    if (minCrossedBricks == 0) return 0;
                }

            return minCrossedBricks;
        }


        // Time O(n^2) || Space O(n), n = length of 'nums'
        public static int TupleSameProduct(int[] nums)
        {
            Dictionary<int, int> productFreq = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int product = nums[i] * nums[j];
                    if (!productFreq.ContainsKey(product))
                        productFreq[product] = 1;
                    else
                        productFreq[product]++;
                }
            long ans = 0;
            foreach (var freq in productFreq.Values)
                if (freq > 1)       // 2 or more unique nums-pair have same product
                    ans += ((freq * (freq - 1)) / 2) * 8;       // (n * n-1) / 2 * 8
            /* A unique Set has 8 ways
             * Ex: There are 8 valid tuples for pari of {2,6} & {3,4} which are:
             * (2,6,3,4) , (2,6,4,3) , (6,2,3,4) , (6,2,4,3)
             * (3,4,2,6) , (4,3,2,6) , (3,4,6,2) , (4,3,6,2)
             */
            return (int)(ans % 1000000007);
        }


        // Time O(2^n) || Recursive Space O(n)
        public static int MaxLenConcatenatedStringWithUniqueCharacters(IList<string> arr)
        {
            int ans = 0;
            GetMax("", 0);
            return ans;

            // local func
            void GetMax(string currS, int idx)  // O(n^2)
            {
                ans = Math.Max(ans, currS.Length);
                for (int i = idx; i < arr.Count; i++)
                {
                    if (ans == 26) return;
                    var newS = arr[i] + currS;
                    if (!DuplicateChars(newS))
                        GetMax(newS, i + 1);
                }
            }
            bool DuplicateChars(string s)       // O(1)
            {
                int[] charSet = new int[26];
                for (int i = 0; i < s.Length; i++)
                    if (++charSet[s[i] - 'a'] == 2)
                        return true;
                return false;
            }
        }

        // Time O(n^2) || Recursive Space O(n), n = length of 'arr' | DP Soln
        public static int MaxLenConcatenatedStringWithUniqueCharacters_DP_CharMask(IList<string> arr)
        {
            List<int[]> wordMaskLen = new();
            int mask, len;
            foreach (var word in arr)                    // O(n)
                // add to list if no duplicate characters present
                if ((mask = GetCharMask(word)) != -1)    // O(26)
                    wordMaskLen.Add(new int[] { mask, word.Length });

            len = wordMaskLen.Count;
            Dictionary<int, int>[] cache = new Dictionary<int, int>[len];
            for (int i = 0; i < len; i++) cache[i] = new();    // initialize the Dictionary at each index
            return GetMaxLenUnqString(0, 0);

            // local helper func
            int GetMaxLenUnqString(int i, int curMask)
            {
                if (i >= len) return 0;
                // see if for current index we have prv calculated the maxLen with provided curMask
                if (cache[i].TryGetValue(curMask, out int maxLenFromCurrentIdx)) return maxLenFromCurrentIdx;

                int maxLen = 0;
                // skip current word
                maxLen = Math.Max(maxLen, GetMaxLenUnqString(i + 1, curMask));

                // take current word, if it doesn't have any character which are already part of the final concatenation
                if ((curMask & wordMaskLen[i][0]) == 0)
                    maxLen = Math.Max(maxLen, wordMaskLen[i][1] + GetMaxLenUnqString(i + 1, curMask | wordMaskLen[i][0]));  // merge mask before making recursive call

                // save to cache before returning the maxLength
                return cache[i][curMask] = maxLen;
            }
            // create a mask for all characters present in the input wordand switch the ith bit on from left, ith bit = char distance from 'a'
            int GetCharMask(string s)   // O(26)
            {
                int[] unqChar = new int[26];
                int mask = 0;
                foreach (var ch in s)    // Max(26,len), loop will run max of 26 times before we are sure to find a duplicate
                {
                    // return -1 if any word has even 1 duplicate character
                    if (++unqChar[ch - 'a'] > 1)
                        return -1;
                    mask |= (1 << ch - 'a');
                }
                return mask;
            }
        }


        // Time O(n) Space O(1)
        public static int MaxConsecutiveOnesIII(int[] nums, int k)
        {
            int ans = 0, i = -1, j = 0, l = nums.Length, ones = 0, zeros = 0;
            while (++i < l)
                if (nums[i] == 1)
                    ans = Math.Max(ans, (++ones) + zeros);
                else //if(nums[n]==0)
                    if (++zeros <= k)
                    ans = Math.Max(ans, ones + zeros);
                else
                    while (zeros > k)
                        if (nums[j++] == 0)
                            zeros--;
                        else
                            ones--;
            return ans;
        }


        // Time O(n) || Auxillary Space O(1) || Recursive Space O(h)
        public static int CountGoodNodes(TreeNode root, int max = int.MinValue)
        {
            if (root == null) return 0;
            return (root.val >= max ? 1 : 0)
                + CountGoodNodes(root.left, Math.Max(max, root.val))
                + CountGoodNodes(root.right, Math.Max(max, root.val));
        }


        // Finds Bridges in UnDirected-Graph removing which can divide the Graph in 2 or more connected components
        // Time O(Max(n,V+E)) || Space O(V)
        public static IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            // Create unDirected graph
            Dictionary<int, List<int>> g = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)        // intialize all nodes of graph with empty List
                g[i] = new List<int>();

            foreach (var edge in connections)                // O(n)
            {
                var u = edge[0];
                var v = edge[1];
                // U --> V
                g[u].Add(v);
                // V --> U
                g[v].Add(u);
            }

            List<IList<int>> bridges = new List<IList<int>>();

            int time = 1, source = 0;                       // we can choose any Vertex as source for Tarjan's Algo
            bool[] isVisited = new bool[n];                 // to mark is Vertex is already visited or not
            int[] startingTime = new int[n];                // to store the time at which Vertex was 1st visited
            int[] parent = new int[n];                      // to store the time at which Vertex was 1st visited
            int[] nodeWithEarlistTimeReachable = new int[n];// to store the the min-time node avalible via all any of the connected edges (except Parent)

            for (int i = 0; i < n; i++)
                startingTime[i] = parent[i] = nodeWithEarlistTimeReachable[i] = -1; // set default values

            FindAllBridges(source);                         // we can start from any vertex, taking 0th node as source

            foreach (var bridge in bridges)                 // Printing potentially Risky-Connection
                Console.WriteLine($" Edge/Cable: '{bridge[0]}---{bridge[1]}' is Only connection & its failure/damage to this can divide network into 2 or more parts");

            return bridges;

            // local func
            // DFS - Tarjan's Algo
            void FindAllBridges(int u)          // O(V+E)
            {
                isVisited[u] = true;            // Mark visited

                // update the starting time of newly visited node, also update the min-time-node reachable as self only for now
                startingTime[u] = nodeWithEarlistTimeReachable[u] = time++;

                foreach (var v in g[u])
                    if (!isVisited[v])              // not visited yet
                    {
                        parent[v] = u;          // mark parent
                        FindAllBridges(v);      // recursively check for adjacent Vertex

                        // Update 'starting time' of parent node is found one via Adjacent Vertex
                        nodeWithEarlistTimeReachable[u] = Math.Min(nodeWithEarlistTimeReachable[u], nodeWithEarlistTimeReachable[v]);

                        //-- now coming back n.e. back-tracking after above recursive call --//

                        // if Vertex 'V' can say to Vertex 'U' that n have found a Vertex better than you or n have found another way to reach to you
                        if (nodeWithEarlistTimeReachable[v] <= startingTime[u])
                            continue;
                        else // if (nodeWithEarlistTimeReachable[v] > startingTime[u]), meaning Vertex V only connection with Vertex 'U' is edge U---V
                            bridges.Add(new int[] { u, v });
                    }
                    else if (v != parent[u])        // vertex 'v' we are looking at is not parent of 'u'
                                                    // update if a node with even smaller start-time is reachable for Vertex 'u'
                        nodeWithEarlistTimeReachable[u] = Math.Min(nodeWithEarlistTimeReachable[u], startingTime[v]);
            }
        }


        // Time O(nlogn) || Space O(1)
        public static int FrequencyOfTheMostFrequentElement(int[] nums, int k)
        {
            Array.Sort(nums);
            int l = 0, r = -1, ans = 1, slidingWindowTotal = 0;
            while (++r < nums.Length)
            {
                while ((nums[r] * (r - l)) - slidingWindowTotal > k)
                    slidingWindowTotal -= nums[l++];
                slidingWindowTotal += nums[r];
                ans = Math.Max(ans, 1 + r - l);
            }
            return ans;
        }


        // Time O(n *log(ladders)) || Space O(ladders), n = len of arr 'heights'
        public static int FurthestBuilding(int[] heights, int bricks, int ladders)
        {
            MinHeap ob = new MinHeap(ladders);
            int k = 0, bricksSum = 0, diff;
            while (++k < heights.Length)
            {
                diff = heights[k] - heights[k - 1];

                if (heights[k - 1] >= heights[k])
                    continue;
                else if (ladders-- > 0)
                    ob.Insert(diff);                  // O(log ladders)
                else
                {

                    if (ob.Count > 0 && diff > ob.arr[0])   // replace Heap Min with curr & Heapify
                    {
                        // remove an existing Jump being counted as used by ladder & instead mark as used bricks to jump
                        bricksSum += ob.arr[0];
                        ob.arr[0] = diff;
                        ob.Heapify();                       // O(log ladders)
                    }
                    else bricksSum += diff;                 // add to TotalBricksSum

                    if (bricksSum > bricks) break;
                }
            }
            return k - 1;
        }
        // Time O(n*logl) | Space O(l), n = length of 'heights' & l = ladders
        public static int FurthestBuildingUsingPriorityQueueADT(int[] heights, int bricks, int ladders)
        {
            PriorityQueue<int, int> minJump = new();
            int idx = 0, l = heights.Length, gap, bricksUsedSoFar = 0;
            while (++idx < l)                          // O(n)
            {
                gap = heights[idx] - heights[idx - 1];
                if (gap > 0)   // is last building was smaller only than we need to use bricks or ladders
                {
                    // if we have ladders use them first
                    if (ladders-- > 0)
                        minJump.Enqueue(gap, gap);   // O(logl)
                    else    // try using bricks
                    {
                        // min heap is not empty and minJump using ladders is smaller current gap
                        // its wise to use bricks for prv smaller jump and use ladder for current
                        if (minJump.TryPeek(out int smallestJumpSoFar, out int priority) && smallestJumpSoFar < gap)
                            // use the minHeap top and add new gap for which ladder is being used
                            bricksUsedSoFar += minJump.DequeueEnqueue(gap, gap);   // O(logl)
                        else bricksUsedSoFar += gap;
                    }
                    if (bricksUsedSoFar > bricks) break;
                }
            }
            return idx - 1;
        }


        // Time = Space = O(n), n = length of 'nums'
        public static int CountNicePairs(int[] nums)
        {
            Dictionary<int, int> diffFreq = new Dictionary<int, int>();
            long pair = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                var key = nums[i] - Rev(nums[i]);
                if (diffFreq.ContainsKey(key))
                    diffFreq[key]++;
                else
                    diffFreq[key] = 1;

                // minus one as we don't want to count the cases which we have previously counted & key with appears just once
                pair += diffFreq[key] - 1;
                pair %= 1000000007;
            }
            return (int)pair;

            // Local func to get reverse of the integer
            int Rev(int n)
            {
                int rev = 0;
                while (n > 0)
                {
                    rev = rev * 10 + n % 10;
                    n /= 10;
                }
                return rev;
            }
        }


        // Time O(nlogn) || Space O(n)
        public static int[] ArrayRankTransform(int[] arr)
        {
            SortedDictionary<int, List<int>> numIdx = new SortedDictionary<int, List<int>>();
            for (int i = 0; i < arr.Length; i++)
                if (!numIdx.ContainsKey(arr[i]))
                    numIdx[arr[i]] = new List<int>() { i };
                else
                    numIdx[arr[i]].Add(i);

            int curRank = 1;
            foreach (var idxList in numIdx.Values)
            {
                foreach (var idx in idxList)
                    arr[idx] = curRank;
                curRank++;
            }
            return arr;
        }



        // Time O(Max(n,k*(mlogm))) || Space O(n), n = length of 'words', m = avg no of unique words per frequency
        public static IList<string> TopKFrequent(string[] words, int k)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (var word in words)      // O(n)
                if (!freq.ContainsKey(word)) freq[word] = 1;
                else freq[word]++;

            int maxFreq = 0;
            Dictionary<int, List<string>> grpFreq = new Dictionary<int, List<string>>();
            foreach (var kvp in freq)        // O(n)
            {
                maxFreq = Math.Max(maxFreq, kvp.Value);
                if (!grpFreq.ContainsKey(kvp.Value))
                    grpFreq[kvp.Value] = new List<string>() { kvp.Key };
                else
                    grpFreq[kvp.Value].Add(kvp.Key);
            }

            List<string> ans = new List<string>();
            while (k > 0)                      // O(k)
            {
                while (!grpFreq.ContainsKey(maxFreq)) maxFreq--;

                grpFreq[maxFreq].Sort();    // O(mlogm) m = no of unique words for curr frequency
                foreach (var str in grpFreq[maxFreq])
                {
                    ans.Add(str);
                    if (--k == 0) break;
                }
                grpFreq.Remove(maxFreq);
            }
            return ans;
        }


        // Time O(r*c) || Space O(1)
        public static bool IsToeplitzMatrix(int[][] matrix)
        {
            for (int i = 1; i < matrix.Length; i++)        // skip 1st row
                for (int j = 1; j < matrix[0].Length; j++) // skip 1st element of all rows
                    if (matrix[i - 1][j - 1] != matrix[i][j])
                        return false;
            return true;
        }


        // Time = Space = O(1)
        public static int NumRookCaptures(char[][] b)
        {
            int row = b.Length, col = b[0].Length, r = 0, c = 0;
            // find Position of 'Rook'
            for (r = 0; r < row; r++)
                for (c = 0; c < col; c++)
                    if (b[r][c] == 'R')
                        return CapturePawn(0) + CapturePawn(1) + CapturePawn(2) + CapturePawn(3);
            return 0;
            // local func
            int CapturePawn(int direction)
            {
                int i = r, j = c;
                int op = direction < 2 ? -1 : 1;

                while (i >= 0 && i < 8 && j >= 0 && j < 8)
                {
                    if (b[i][j] == 'B') break;            // found Bishop 
                    else if (b[i][j] == 'p') return 1;    // found Pawn

                    // left or right direction update column
                    if (direction % 2 == 0) j += op;
                    // top or down direction update row
                    else i += op;
                }
                return 0;
            }
        }


        // Time O(r*c) || Space O(r+c)
        public static int CountCommunicatingServers(int[][] g)
        {
            int row = g.Length, col = g[0].Length;
            bool[] isRowCommunicating = new bool[row];
            bool[] isColCommunicating = new bool[col];
            List<int>[] rCount = new List<int>[row];    // to store no of columns at which servers are present for each row
            int[] cCount = new int[col];                // count no of servers per col

            for (int r = 0; r < row; r++)
            {
                rCount[r] = new List<int>();
                for (int c = 0; c < col; c++)
                    if (g[r][c] == 1)
                    {
                        rCount[r].Add(c);
                        if (rCount[r].Count > 1)        // more than 2 servers found at curr row
                            isRowCommunicating[r] = true;
                        if (++cCount[c] > 1)            // more than 2 servers found at curr col
                            isColCommunicating[c] = true;
                    }
            }

            int communicatingServers = 0;
            for (int r = 0; r < row; r++)
                if (isRowCommunicating[r])
                {
                    communicatingServers += rCount[r].Count;
                    foreach (var column in rCount[r])
                        cCount[column]--;       // subtract servers which are just counted from their respective columns
                }
            for (int c = 0; c < col; c++)
                if (isColCommunicating[c])
                    communicatingServers += cCount[c];

            return communicatingServers;
        }


        // Time O(m*n) || Space O(1), m,n = max power 'x' & 'y' respectively so that 'number' is <= bound
        public static IList<int> PowerfulIntegers(int x, int y, int bound)
        {
            if (bound <= 1) return new List<int>();

            HashSet<int> ans = new HashSet<int>();

            int maxPowerX = MaxPower(x), maxPowerY = MaxPower(y), powerOfX = 1, powerOfY = 1, lastPowerY, lastPowerX = -1;
            //// Trick to found power in O(1)
            //maxPowerX = x == 1 ? bound : (int)(Math.Log(bound) / Math.Log(x));
            //maxPowerY = y == 1 ? bound : (int)(Math.Log(bound) / Math.Log(y));
            for (int i = 0; i <= maxPowerX; i++)
            {
                powerOfY = 1;
                lastPowerY = -1;
                for (int j = 0; j <= maxPowerY; j++)
                {
                    if (powerOfX + powerOfY <= bound)
                        ans.Add(powerOfX + powerOfY);
                    else
                        break;
                    powerOfY *= y;
                    if (powerOfY == lastPowerY) break;
                    else lastPowerY = powerOfY;
                }
                powerOfX *= x;
                if (powerOfX == lastPowerX) break;
                else lastPowerX = powerOfX;
            }
            return ans.ToList();

            // Local func
            int MaxPower(int powerOf)
            {
                int p = -1, num = 1, last = -1;
                while (num <= bound)
                {
                    p++;
                    num *= powerOf;
                    if (num == last) break;
                    else last = num;
                }
                return p;
            }
        }


        // Time O(Max(l,m)) || Space O(l), l = length of string 's' & m = length of 'queries'
        public static IList<bool> CanMakePaliQueries(string s, int[][] queries)
        {
            int l = s.Length;
            int[] charFreq = new int[26];
            int[][] freqPreFix = new int[l + 1][];      // S O(l)
            freqPreFix[0] = charFreq.ToArray();
            for (int i = 0; i < l; i++)                 // T O(l)
            {
                charFreq[s[i] - 'a']++;
                freqPreFix[i + 1] = charFreq.ToArray();
            }

            bool[] ans = new bool[queries.Length];
            for (int i = 0; i < queries.Length; i++)    // T O(m)
                if (IsPalindrome(queries[i]))           // T O(26) ~O(1)
                    ans[i] = true;
            return ans;

            // Local func
            bool IsPalindrome(int[] q)
            {
                int left = q[0], right = q[1], k = q[2], oddFreq = 0;
                for (int i = 0; i < 26; i++)
                    // count alphabets with odd frequency
                    if ((freqPreFix[right + 1][i] - freqPreFix[left][i]) % 2 != 0)
                        oddFreq++;
                oddFreq -= 2 * k;
                return oddFreq < 2;
            }
        }


        // Time = Space = O(n), n = length of 'dominoes' array
        public static int NumEquivDominoPairs(int[][] dominoes)
        {
            Dictionary<string, int> hs = new Dictionary<string, int>();
            int ans = 0;
            foreach (var pair in dominoes)
            {
                if (pair[0] != pair[1])
                {
                    var rev = pair[1] + "|" + pair[0];
                    if (hs.ContainsKey(rev))
                        ans += hs[rev];
                }
                var key = pair[0] + "|" + pair[1];
                if (hs.ContainsKey(key))
                    ans += hs[key]++;     // add no of pair found in Dict & than also update the count by 1
                else
                    hs[key] = 1;
            }
            return ans;
        }


        // Time O(n) Space O(1)
        public static int MovesToMakeZigzag(int[] nums)
        {
            int[] originalNums = nums.ToArray();
            int evenMoves = 0, oddMoves = 0, l = nums.Length;
            // even index is greater
            for (int i = 0; i < l; i += 2)
            {
                if (i > 0 && nums[i - 1] >= nums[i])
                {
                    evenMoves += 1 + nums[i - 1] - nums[i];
                    nums[i - 1] = nums[i] - 1;
                }
                if (i + 1 < l && nums[i] <= nums[i + 1])
                {
                    evenMoves += 1 + nums[i + 1] - nums[i];
                    nums[i + 1] = nums[i] - 1;
                }
            }
            // odd index is greater
            for (int i = 1; i < l; i += 2)
            {
                if (originalNums[i - 1] >= originalNums[i])
                {
                    oddMoves += 1 + originalNums[i - 1] - originalNums[i];
                    originalNums[i - 1] = originalNums[i] - 1;
                }
                if (i + 1 < l && originalNums[i] <= originalNums[i + 1])
                {
                    oddMoves += 1 + originalNums[i + 1] - originalNums[i];
                    originalNums[i + 1] = originalNums[i] - 1;
                }
            }
            return Math.Min(evenMoves, oddMoves);
        }


        // Time (n*k)) || Recursive Space O(k) || Auxillary Space O(k)
        public static IList<IList<int>> Combinations(int n, int k)
        {
            List<IList<int>> ans = new List<IList<int>>();
            Stack<int> combi = new Stack<int>();
            Populate(1);
            return ans;
            // local func
            void Populate(int i)
            {
                if (combi.Count == k)
                    ans.Add(combi.Reverse().ToList());
                else
                    // Optimized to check if we have enouf nums to fill 'k' combination
                    for (int num = i; num <= n && 1 + n - num >= k - combi.Count; num++)
                    {
                        combi.Push(num);
                        Populate(num + 1);
                        combi.Pop();
                    }
            }
        }


        // Time O(nlogn) || Space O(n)
        public static int ScheduleCourse(int[][] courses)
        {
            //courses = courses.OrderBy(x => x[1]).ToArray();
            courses = (from course in courses                           // O(nlogn)
                       orderby course[1]        // sort by lastDay
                       select course).ToArray();
            MaxHeap maxH = new MaxHeap(courses.Length);
            int time = 0, duration, lastDay;
            for (int i = 0; i < courses.Length; i++)                    // O(n)
            {
                duration = courses[i][0];
                lastDay = courses[i][1];
                // if course duration + curr time <= course lastDay to complete course, we can take the curr course
                if (duration + time <= lastDay)
                {
                    // take the course & add it to Priority Queue
                    time += duration;
                    maxH.Insert(duration);                              // O(logn)
                }
                // duration of curr course is smaller than the largest course we have taken till now
                else if (maxH.Count > 0 && maxH.arr[0] > duration)
                {
                    // remove the course with biggest duration from MaxHeap & add the curr coarse duration
                    time -= maxH.arr[0] - duration;     // update time to reflect taking shorter duration course
                    maxH.arr[0] = duration;             // replace the HeapTop with new duration
                    maxH.MaxHeapify();                  // re-balance the MaxHeap || O(logn)
                }
            }
            return maxH.Count;
        }


        // Time O(2^n) || Space O(n), since at each index we have 2 choice to include this num or not include & move frwd
        public static IList<IList<int>> CombinationSumII(int[] candidates, int target)
        {
            Array.Sort(candidates);                 // O(nlogn)
            List<IList<int>> ans = new List<IList<int>>();
            Stack<int> curr = new Stack<int>();
            int l = candidates.Length, currSum = 0;
            FindCombi(0);
            return ans;

            // local func
            void FindCombi(int idx)
            {
                if (target == currSum) ans.Add(curr.Reverse().ToList());
                if (currSum >= target) return;

                for (int i = idx; i < l; i++)
                {
                    currSum += candidates[i];
                    curr.Push(candidates[i]);

                    FindCombi(i + 1);

                    currSum -= candidates[i];
                    curr.Pop();

                    // Skip all next nums which are same as curr num to avoid duplicate efforts
                    while (i + 1 < l && candidates[i] == candidates[i + 1]) i++;
                }
            }
        }


        // Time = Space = O(n)
        public static int[] SumZero(int n)
        {
            int currNum = 1, idx = 0;
            int[] ans = new int[n];

            while (idx < n / 2) ans[idx++] = -currNum++;     // add -ve nums
            if (n % 2 == 1) ans[idx++] = 0;            // if 'n' is odd
            while (idx < n) ans[idx++] = --currNum;        // add opp +ve nums

            return ans;
        }


        // Time O(n) || Space O(1)
        public static bool NondecreasingArray(int[] nums)
        {
            int modifyLimit = 1, l = nums.Length;
            for (int i = 1; i < l; i++)
                if (nums[i - 1] > nums[i])
                {
                    if (i == 1)                     // 0th element is larger than 1st idx, decrease 0th to 1st value
                        nums[i - 1] = nums[i];
                    else if (i == l - 1)            // last element is smaller, increase it to same as last-1
                        nums[i] = nums[i - 1];
                    else
                    {
                        if (i + 1 < l && nums[i - 1] <= nums[i + 1])
                            nums[i] = nums[i - 1];  // increase the value of nums[n] to n-1
                        else if (i - 1 > 0 && nums[i - 2] <= nums[i])
                            nums[i - 1] = nums[i - 2];// decrease the value of nums[n-1] to n-2
                        else
                            return false;           // single modification to make non-decreasing not possible
                    }

                    if (--modifyLimit < 0)          // more than '1' modification not allowed
                        return false;
                }
            return true;
        }


        // Inspired from Kadane's Algo || Time O(n^2) Space O(1)
        public static int MaxSubarraySumCircular_BruteForce(int[] A)
        {
            int back = 0, front = 1, currSum = A[0], maxSum = A[0], l = A.Length;
            while (back < l)
            {
                while (currSum >= 0)
                {
                    currSum += A[front];
                    maxSum = Math.Max(maxSum, currSum);
                    front = (front + 1) % l;
                    if (front == back)
                    {
                        front = back + 1;
                        currSum = -1;
                    }
                }
                currSum = 0;                            // Reset current sub-array Sum to 0
                back = Math.Max(back, front);           // update the back pointer to current idx n.e. Front
            }
            return maxSum;
        }
        // Time O(n) Space O(1)
        public static int MaxSubarraySumCircular(int[] A)
        {
            int currSum = int.MinValue, maxSum = int.MinValue, currMinSum = int.MaxValue, minSum = int.MaxValue, totalSum = 0, maxNum = int.MinValue;
            bool foundPositive = false;
            for (int i = 0; i < A.Length; i++)
            {
                currSum = A[i] + Math.Max(currSum, 0);
                maxSum = Math.Max(maxSum, currSum);

                totalSum += A[i];
                maxNum = Math.Max(maxNum, A[i]);

                currMinSum = A[i] + Math.Min(currMinSum, 0);
                minSum = Math.Min(minSum, currMinSum);

                if (A[i] >= 0) foundPositive = true;
            }
            // if all nums in array are -ve, return maxNum from array
            // else in normal case return the maximum of maxSum or totalSum - minSum n.e. maxSum in case of wrap around sub-array
            return foundPositive ? Math.Max(maxSum, totalSum - minSum) : maxNum;
        }


        // Time O(n*log(Min(n,k))) || Space O(Min(n,k))
        public static bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            SortedSet<long> set = new SortedSet<long>();
            for (int i = 0; i < nums.Length; i++)
            {
                // if atleast 1 nums exists, which is at max 'k' value apart current number
                if (set.GetViewBetween((long)nums[i] - t, (long)nums[i] + t).Count > 0)
                    return true;
                // add current num to set
                set.Add(nums[i]);
                // if set size is greater than 'k' remove 'n-k'th num
                if (i >= k) set.Remove(nums[i - k]);
            }
            return false;

            /* JAVA TreeSet (balanced BST) based solution using floor & ceiling APIs
            public boolean containsNearbyAlmostDuplicate(int[] nums, int k, int t) {
                TreeSet<Long> balancedBST = new TreeSet();
                for(int n=0;n<nums.length;n++)
                {
                    Long ceil = balancedBST.ceiling((long)nums[n]);
                    if(ceil!=null && Math.abs(ceil-nums[n])<=t)
                        return true;
            
                    Long floor = balancedBST.floor((long)nums[n]);
                    if(floor!=null && Math.abs(floor-nums[n])<=t)
                        return true;
            
                    // add curr num to tree-set
                    balancedBST.add((long)nums[n]);
                    // remove the 'n-k'th num if set size exceeds k
                    if(balancedBST.size()>k)
                        balancedBST.remove((long)nums[n-k]);
                }
                return false;
            }
             */
        }


        // Time O(n) Space O(h), h = log(n), n = no of nodes in List
        public static TreeNode SortedListToBST(ListNode head)
        {
            if (head == null) return null;
            var mid = GetMid(head);
            var root = new TreeNode(mid.val);
            if (mid == head) return root;

            root.left = SortedListToBST(head);
            root.right = SortedListToBST(mid.next);
            return root;

            // Local func
            ListNode GetMid(ListNode h)
            {
                ListNode slow = h, fast = h, prv = null;
                while (fast?.next != null)
                {
                    prv = slow;
                    slow = slow.next;
                    fast = fast.next.next;
                }
                if (prv != null) prv.next = null;
                return slow;
            }
        }


        /// <summary>
        /// Time O(10^5) || Space O(1)
        /// Time O(W^(1/4)∗logW), where W = 10 ^18 is our upper limit for R.
        /// The logW term comes from checking whether each candidate is the root of a palindrome.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int SuperpalindromesInRange(string left, string right)
        {
            /* Problem is to find number which are palindrome & also square of number which is also palindrome
             * This problem has BruteForce solution but in order to avoid TLE as range 10^18 is really huge
             * 
             * we know we are only intrested in numbers which are square of paldinrome,
             * so we in fact need to find all lower level palindrome nums in the range 1...10^9 which are paldinrome
             * & than we can take square of these num and increament counter when square lies b/w Left...Right
             * 
             * In order to further save time we know any 10^9 digit number which is a paldinrome can be constructed in either of below 2 ways
             *      Odd len     12345 + 4321
             *      Even len    1234 + 4321
             * this further reduces our range to 10^5 from 10^9
             * now we can iterate from 1...10^5 and for each num we reverse the num & concatenate to to original num to get final num,
             * whose squrare we can check if lies between Left...Right than increament counter
             */
            ulong l = Convert.ToUInt64(left), r = Convert.ToUInt64(right), range = 100000, n = 0, superPalindrome = 0;
            string lHalf, rHalf;
            // odd len case
            while (++n <= range)                // O(10^5)
            {
                lHalf = n.ToString();
                rHalf = Reverse(lHalf, 1);

                //ulong num = (ulong)Math.Pow(Convert.ToUInt64(lHalf + rHalf), 2); THIS FUCKING DOESNT WORKS
                ulong num = Convert.ToUInt64(lHalf + rHalf);
                num *= num;

                if (num > r)
                    break;

                if (num >= l && num == ReverseNum(num))
                    superPalindrome++;
            }
            n = 0;
            // even len case
            while (++n <= range)                // O(10^5)
            {
                lHalf = n.ToString();
                rHalf = Reverse(lHalf);

                ulong num = Convert.ToUInt64(lHalf + rHalf);
                num *= num;

                if (num > r)
                    break;

                if (num >= l && num == ReverseNum(num))
                    superPalindrome++;
            }
            return (int)superPalindrome;

            // local Helper Func
            string Reverse(string s, int skipLastDigit = 0)
            {
                Stack<char> st = new Stack<char>();
                for (int i = 0; i < s.Length - skipLastDigit; i++)
                    st.Push(s[i]);
                return new string(st.ToArray());
            }
            ulong ReverseNum(ulong number)
            {
                ulong rev = 0;
                while (number > 0)
                {
                    rev = rev * 10 + number % 10;
                    number /= 10;
                }
                return rev;
            }
        }


        // Time O(n^2) Space O(n), n = length of array 'Transactions'
        public static IList<string> InvalidTransactions(string[] transactions)
        {
            Dictionary<string, List<string[]>> d = new Dictionary<string, List<string[]>>();
            // Grp transaction with same name together in Hashtable
            for (int i = 0; i < transactions.Length; i++)           // O(n)
            {
                var curr = transactions[i].Split(',');
                if (!d.ContainsKey(curr[0]))
                    d[curr[0]] = new List<string[]>() { curr };
                else
                    d[curr[0]].Add(curr);
            }
            IList<string> invalid = new List<string>();
            foreach (var tnx in d.Values)                           // O(n)
                if (tnx.Count == 1) // grp has single entry, just check its 'amt' is not more than threshold
                {
                    if (Convert.ToInt32(tnx[0][2]) > 1000)
                        invalid.Add(tnx[0][0] + "," + tnx[0][1] + "," + tnx[0][2] + "," + tnx[0][3]);
                }
                else
                    for (int i = 0; i < tnx.Count; i++)
                        for (int j = 0; j < tnx.Count; j++)
                            // check if any tnx from same grp n.e. same name doesnt exceed threshold amt & also doesn't occur with 60min of any other tnx in different city
                            if (i != j && (Convert.ToInt32(tnx[i][2]) > 1000 || (Math.Abs(Convert.ToInt32(tnx[j][1]) - Convert.ToInt32(tnx[i][1])) <= 60 && tnx[j][3] != tnx[i][3])))
                            {
                                invalid.Add(tnx[i][0] + "," + tnx[i][1] + "," + tnx[i][2] + "," + tnx[i][3]);
                                break;  // once transaction is added to inValid stop further validation for ith transaction
                            }
            return invalid;
        }


        // Time O(n) Space O(1)
        public static bool CheckStraightLine(int[][] coordinates)
        {
            /* The idea of this solution is we use 3 points to find whether two slopes are equal. We can use the equation:
             * 
             * (x1 - x0) * (y2 - y1) != (y1 - y0) * (x2 - x1)
             * where x0, x1, x2, y0, y1, y2 are all coordinates
             * 
             * we may equation simpler, by dividing both sides by (x1 - x0) and (x2 - x1) and we can get:
             * (y2-y1) / (x2-x1) = (y1-y0) / (x1-x0) == Slope
             * 
             * but we will use original equation as we don't have to worry about dividing by 0
             */
            int x1 = coordinates[1][0], y1 = coordinates[1][1], x0 = coordinates[0][0], y0 = coordinates[0][1], y2, x2;
            for (int i = 2; i < coordinates.Length; i++)
            {
                x2 = coordinates[i][0];
                y2 = coordinates[i][1];
                if ((x1 - x0) * (y2 - y1) != (y1 - y0) * (x2 - x1))
                    return false;
            }
            return true;
        }



        // Time O(n^2) Space O(1)
        public static IList<int> PancakeSort(int[] A)
        {
            // Most imp thing to notice is for any given number, in order to move it to any desired position, it takes at most two pancake flips to do so.
            IList<int> ans = new List<int>();
            int l = A.Length;
            if (l < 2) return ans;

            for (int n = l; n >= 1; n--)
                // nth num not present at its correct location
                if (A[n - 1] != n)
                    // find location of nth num in array
                    for (int i = 0; i < n; i++)
                        if (A[i] == n)
                        {
                            if (i == 0)    // nth element is at 0th idx
                            {
                                Flip(n - 1);
                                ans.Add(n);
                            }
                            else
                            {
                                Flip(i);// take the nth element to 0th idx
                                ans.Add(i + 1);
                                Flip(n - 1);// now take nth element to its correct idx
                                ans.Add(n);
                            }
                            break;
                        }
            return ans;

            // local helper func
            void Flip(int k)
            {
                int start = 0, last = k;
                while (start < last)
                {
                    var temp = A[start];
                    A[start++] = A[last];
                    A[last--] = temp;
                }
            }
        }


        // Time O(nlogn) || Space O(n)
        public static bool ConstructTargetArrayWithMultipleSums(int[] target)
        {
            /* Given that the sum is strictly increasing,
             * the largest element in the target must be formed in the last step by adding the total sum in the previous step.
             * Thus, we can simulate the process in a reversed way.
             * 
             * Subtract the largest with the rest of the array,
             * and put the new element into the array. Repeat until all elements become one
             */
            if (target.Length == 1) return target[0] == 1;
            long total = 0, x, top, remaining;
            MaxHeap heap = new MaxHeap(target.Length);
            for (int i = 0; i < target.Length; i++) // O(n)
            {
                total += target[i];
                heap.Insert(target[i]);             // O(logn)
            }
            while (heap.GetMax() > 1)
            {
                top = heap.GetMax();
                //x = top - (total - top);          // Results in TLE in edge cases when max no is very large as compared to sum of reamaining numbers
                remaining = total - top;
                if (remaining > top) return false;
                x = top % remaining;                // using mod instead of subtraction

                if (remaining == 1) return true;    // happens when we just have 2 nums ex: 1,5678 & we can construct 1's array in that case,
                                                    // in situation like 1,2,3,7 we catch false in above if check

                if (x < 1) return false;            // no nums could be < 1 as starting array we are trying to re-create has all 1's

                total -= (top - x);                 // update total
                heap.UpdateTop((int)x);             // replace the HeapTop with new reduced values & Heapify
            }
            return true;
        }


        // Brute Force Time O(n*n/2) Space O(1)
        public static int CountPrimesBruteForce(int n)
        {
            int prime = 0;
            for (int i = 2; i < n; i++)    // O(n)
                if (isPrime(i))             // O(n/2)
                    prime++;
            return prime;
            bool isPrime(int num)
            {
                for (int i = num / 2; i > 1; i--)
                    if (num % i == 0)
                        return false;
                return true;
            }
        }
        // Sieve of Eratosthenes Algo || Time O(n*loglogn) || Space O(n)
        public static int CountPrimes_SieveAlgo(int range)
        {
            bool[] isPrime = new bool[range + 1];   // O(n)
            for (int i = 2; i < range; i++)
                isPrime[i] = true;

            // check only till SqRoot(range)
            for (int n = 2; n * n < range; n++) // O(loglogn)
                if (isPrime[n])
                    // now mark all multiples of current no's as not prime,
                    // start from n^2 as all num b/w n...-1+n^2 are already marked by integers smaller than n
                    // Ex: for 5, 15 is marked by 3 & 10,20 marked by 2 hence start from 5^2=25
                    for (int num = n * n; num < range; num += n)   // O(n)
                        isPrime[num] = false;

            int count = 0;
            for (int n = 2; n < range; n++)    // O(n)
                if (isPrime[n])
                    count++;
            return count;
        }


        // Time O(k) || Space (1) || Sliding Window based Algo
        public static int MaxScore(int[] cardPoints, int k)
        {
            int left = 0, right = cardPoints.Length - 1, maxPoints = 0, currMax = 0;
            // start initially with all K card picked from left
            while (left < k)                    // O(k)
                currMax += cardPoints[left++];
            maxPoints = currMax;
            // now remove one card from left & pick its replacement from right
            while (--left >= 0)                 // O(k)
            {
                currMax += -cardPoints[left];
                currMax += cardPoints[right--];
                maxPoints = Math.Max(maxPoints, currMax);
            }
            return maxPoints;
        }


        // Time O(n) Space O(1)
        public static int FindPeakElement_Linear(int[] nums)
        {
            for (int i = 0; i < nums.Length - 1; i++)
                if (nums[i] > nums[i + 1])
                    return i;
            return nums.Length - 1;
        }
        // Time O(logN) Space O(1)
        public static int FindPeakElement_BinarySearch(int[] nums)
        {
            int left = 0, right = nums.Length - 1, mid;
            while (left < right)
            {
                mid = left + (right - left) / 2;
                if (nums[mid] > nums[mid + 1])
                    right = mid;
                else
                    left = mid + 1;
            }
            return left;
        }


        // Time = Space = O(n^3)
        public static IList<string> AmbiguousCoordinates(string s)
        {
            List<string> validPair = new List<string>();
            for (int i = 2; i < s.Length - 1; i++)
                foreach (var firstC in Make(1, i))
                    foreach (var secondC in Make(i, s.Length - 1))
                        validPair.Add("(" + firstC + ", " + secondC + ")");
            return validPair;
            // local helper func
            IList<string> Make(int l, int r)
            {
                List<string> coordinates = new List<string>();
                string left, right;
                // Make valid coordinate from s.Substring(l, r)
                for (int d = 1; d <= r - l; d++)
                {
                    left = s.Substring(l, d);
                    right = s.Substring(l + d, r - (l + d));
                    // left part doesnt start with '0' or if it than that the only digit && right doesnt ends with '0'
                    if ((left[0] != '0' || left.Length == 1) && !right.EndsWith("0"))
                        // add dot operator if distance is less than total no of chars we have to play with
                        coordinates.Add(left + (d < r - l ? "." : "") + right);
                }
                return coordinates;
            }
        }

        // Time O(n*k) || Space O(n), n = length of 'A"
        public static long GetMaxBySkipFrwdByMaxK(int[] A, int k)
        {
            int l = A.Length;
            Dictionary<int, long> cache = new Dictionary<int, long>();
            return GetMax(0);
            // local func
            long GetMax(int idx)
            {
                if (idx >= l - 1) return A[l - 1];
                if (cache.ContainsKey(idx))
                    return cache[idx];

                long max = long.MinValue;
                // 2 choice include this idx or skip 'k' index ahead
                for (int jump = 1; jump < idx + k; jump++)
                {
                    if (jump >= l) break;
                    max = Math.Max(max, GetMax(idx + jump));
                }
                return cache[idx] = max + A[idx];
            }
        }


        // Time O(Max(n^2,V+E)) Space O(n), n = no of verticies(Rows) in zombies
        public static int zombiClusters(int[][] zombies)
        {
            int n = zombies.Length;
            List<int>[] graph = new List<int>[n];

            // create Graph
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
                for (int j = 0; j < n; j++)
                    if (zombies[i][j] == 1 && i != j)
                        // u-> v
                        graph[i].Add(j);
            }
            int cluster = 0;
            bool[] visited = new bool[n];
            // DFS traversal
            for (int i = 0; i < n; i++)
                if (!visited[i])    // not already visited
                {
                    DFS(i);
                    cluster++;
                }

            return cluster;

            // local func
            void DFS(int idx)
            {
                if (visited[idx]) return;
                visited[idx] = true;
                foreach (var adjacentVertex in graph[idx])
                    DFS(adjacentVertex);
            }
        }


        // Time O(n) Space O(1)
        public static bool IsNumber(string s)
        {
            bool dotSeen = false, digitSeen = false;
            int l = s.Length, i = -1;

            // if Optional sign found at begining than increment idx by 1
            if (s[0] == '+' || s[0] == '-') i++;

            while (++i < l)
                if (s[i] == '.')
                {
                    // if one dot has already been seen return false on getting 2nd
                    if (dotSeen) return false;
                    dotSeen = true;
                }
                // Digit found
                else if (s[i] - '0' >= 0 && s[i] - '0' <= 9)
                    digitSeen = true;
                // 'e' or 'E' found && atleast 1 digit seen
                else if ((s[i] == 'e' || s[i] == 'E') && digitSeen)
                    return FoundInteger(i + 1);
                // found alphabets b/w A...Z or a...z || '+'  '-' signs
                else
                    return false;
            return digitSeen;

            // local func
            bool FoundInteger(int idx)
            {
                if (idx >= l) return false;

                // if Optional sign found at begining than increment idx by 1
                if (s[idx] == '+' || s[idx] == '-') idx++;

                for (int k = idx; k < l; k++)
                    // Anything else other than a Digit (sign operator || dot operator || 'e' or 'E' || some other alphabets b/w A...Z or a...z) found
                    if (!Char.IsDigit(s[k]))
                        return false;
                return idx < l; // ensuring atleast 1 digit was seen
            }
        }


        /*
         * Complete the 'meanderingArray' function below.
         *
         * The function is expected to return an INTEGER_ARRAY.
         * The function accepts INTEGER_ARRAY unsorted as parameter.
         */
        // Time = O(nlogn) || Space = O(n)
        public static List<int> MeanderingArray(List<int> unsorted)
        {
            unsorted.Sort();    // O(nlogn)
            int left = 0, right = unsorted.Count - 1;
            List<int> ans = new List<int>(right + 1);
            while (left <= right)  // O(n)
            {
                ans.Add(unsorted[right--]);
                if (left < right)
                    ans.Add(unsorted[left++]);
            }
            return ans;
        }

        /*
         * Complete the 'carParkingRoof' function below.
         *
         * The function is expected to return a LONG_INTEGER.
         * The function accepts following parameters:
         *  1. LONG_INTEGER_ARRAY cars
         *  2. INTEGER k
         */
        // Time O(nlogn) || Space O(k)
        public static long CarParkingRoof(List<long> cars, int k)
        {
            if (k == 1) return 1;
            // sort parked cars by their spots
            cars.Sort();        // O(nlogn)

            Queue<long> q = new Queue<long>(k); // Space O(k), to hold last 'k' spots
            long roof = long.MaxValue;
            for (int i = 0; i < cars.Count; i++)   // O(n)
            {
                q.Enqueue(cars[i]);
                if (q.Count < k) continue; // less than 'k' spots are covered currently, add more spots

                // try finding the min length of roof that covers 'k' spots
                roof = Math.Min(roof, 1 + cars[i] - q.Dequeue());
            }
            return roof;
        }

        /*
         * Complete the 'awardTopKHotels' function below.
         *
         * The function is expected to return an INTEGER_ARRAY.
         * The function accepts following parameters:
         *  1. STRING positiveKeywords
         *  2. STRING negativeKeywords
         *  3. INTEGER_ARRAY hotelIds
         *  4. STRING_ARRAY reviews
         *  5. INTEGER k
         */
        public static List<int> AwardTopKHotels(string positiveKeywords, string negativeKeywords, List<int> hotelIds, List<string> reviews, int k)
        {
            HashSet<string> positive = new HashSet<string>(positiveKeywords.ToLower().Split(' ').ToArray());
            HashSet<string> negative = new HashSet<string>(negativeKeywords.ToLower().Split(' ').ToArray());

            // using HashTable to store the review Score of each unique Hotel
            Dictionary<int, long> hotelScore = new Dictionary<int, long>();
            for (int i = 0; i < hotelIds.Count; i++)
                if (!hotelScore.ContainsKey(hotelIds[i]))
                    hotelScore[hotelIds[i]] = GetScore(reviews[i].ToLower(), positive, negative);
                else    // update the score
                    hotelScore[hotelIds[i]] += GetScore(reviews[i].ToLower(), positive, negative);


            //var sortedHotel = hotelScore.ToList();
            //sortedHotel.Sort((x, y) => x.Value != y.Value ? y.Value.CompareTo(x.Value) : x.Key.CompareTo(y.Key));

            List<KeyValuePair<int, long>> sortedHotel = hotelScore.ToList();
            sortedHotel.Sort(delegate (KeyValuePair<int, long> h1, KeyValuePair<int, long> h2)
            // we need to sort in descending order of Score Higher Score Hotel comes 1st, hence if score of 2 hotels is different sort by score else pick one with smaller ID
            { return h1.Value != h2.Value ? h2.Value.CompareTo(h1.Value) : h1.Key.CompareTo(h2.Key); });


            List<int> topK = new List<int>();
            foreach (var topRatedHotel in sortedHotel)
            {
                topK.Add(topRatedHotel.Key);
                if (topK.Count == k)   // got our top 'K' hotels
                    break;
            }
            return topK;
        }
        static long GetScore(string review, HashSet<string> positive, HashSet<string> negative)
        {
            long finalScore = 0;
            foreach (var key in review.Split(' '))
                if (positive.Contains(key))
                    finalScore += 3;
                else if (negative.Contains(key))
                    finalScore -= 1;
            return finalScore;
        }


        // Time O(n) Recursive Space O(h)
        public static int MinCameraCover(TreeNode root, int counter = 0)
        {
            int totalCam = 0;
            if (Cover(root) == 0)
                totalCam++;     // using  Max func to cover edge case of when we just have one node in tree
            return totalCam;

            // local helper func
            // 0: not monitored
            // 1: camera is used
            // 2: no camera, but monitored (use for null nodes)
            // Post Order Traversal
            int Cover(TreeNode r)
            {
                if (r == null) return 2;

                var left = Cover(r.left);
                var right = Cover(r.right);

                if (left == 0 || right == 0)
                {
                    totalCam++;
                    return 1;
                }
                if (left == 2 && right == 2)
                    return 0;
                else
                    return 2;
            }
        }



        public static int LongestStrChain(string[] words)
        {
            /* We iterate over words array and
             * store all the words in dictionary where key is the length of words & value HashSet of words with length = key
             * 
             * next we start from largest length key to smallest key
             * and for each word of current key size we try checking 
             * if there is is any word in key-1 Hashet which matches current word if we delete one of the character from curr word
             * 
             * once we find max chain length equal to size of current key we stop iterating further as this is the max we can get
             * 
             * Added Cache later to fetch precomputed sub-problems in O(1)
             */
            Dictionary<int, HashSet<string>> dict = new Dictionary<int, HashSet<string>>();
            int maxWordLen = 0, longestChain = 0;
            // add words in Dictionary as per their length
            foreach (var word in words)         // O(n)
            {
                if (!dict.ContainsKey(word.Length))
                    dict[word.Length] = new HashSet<string>() { word };
                else
                    dict[word.Length].Add(word);

                maxWordLen = Math.Max(maxWordLen, word.Length);
            }


            Dictionary<string, int> cache = new Dictionary<string, int>();
            while (maxWordLen > 0)              // O(n^2)
            {
                // max possible length already found
                if (longestChain >= maxWordLen) break;

                if (dict.ContainsKey(maxWordLen))
                    longestChain = Math.Max(longestChain, FindChain(maxWordLen));

                maxWordLen--;
            }
            return longestChain;

            // local helper func
            int FindChain(int len)
            {
                int currChain = 0;
                foreach (var word in dict[len])
                {
                    currChain = Math.Max(currChain, 1 + GetPredecessor(word));
                    if (currChain == len)
                        break;
                }

                return currChain;
            }
            int GetPredecessor(string original)
            {
                int len = 0;
                if (cache.ContainsKey(original))
                    return cache[original];

                if (dict.ContainsKey(original.Length - 1))
                    foreach (var predecessor in dict[original.Length - 1])
                        if (IsPredecessor(predecessor, original))
                        {
                            len = Math.Max(len, 1 + (GetPredecessor(predecessor)));
                            if (len == original.Length) break;
                        }

                return cache[original] = len;
            }
            bool IsPredecessorSlower(string pre, string org)
            {
                for (int i = 0; i < org.Length; i++)
                    if (pre == org.Substring(0, i) + (i + 1 < org.Length ? org.Substring(i + 1) : ""))
                        return true;
                return false;
            }
            bool IsPredecessor(string pre, string org)
            {
                int i = 0, j = 0;
                bool misMatch = false;
                while (i < pre.Length && j < org.Length)
                    if (pre[i] == org[j])
                    {
                        i++; j++;   // increament both index
                    }
                    else if (misMatch)
                        return false;// return false on 2nd mis-Match
                    else
                    {
                        misMatch = true;
                        j++;        // increae len of longer string on misMatch
                    }
                return true;
            }
        }


        // Time = Space = O(n*m), n = length of 'paths' array & m is avg no of file in each path
        public static IList<IList<string>> FindDuplicateFileInSystem(string[] paths)
        {
            Dictionary<string, List<string>> contentDict = new Dictionary<string, List<string>>();
            foreach (var path in paths)                     // O(n)
            {
                var files = path.Split(' ');
                // skip 0th idx as that contains 'directory'
                for (int i = 1; i < files.Length; i++)      // O(m)
                {
                    // Ex: "1.txt(abcd)" gets splits into => new string[] { "1.txt" ,"abcd)" }
                    var nameContent = files[i].Split('(');

                    // new content than insert new key in HashTable
                    if (!contentDict.ContainsKey(nameContent[1]))
                        contentDict[nameContent[1]] = new List<string>() { files[0] + "/" + nameContent[0] };
                    else // add one more file path to key whose content matches current file content
                        contentDict[nameContent[1]].Add(files[0] + "/" + nameContent[0]);
                }
            }

            List<IList<string>> duplicateFilesGrp = new List<IList<string>>();
            foreach (var kvp in contentDict)
                if (kvp.Value.Count > 1)        // duplicate files
                    duplicateFilesGrp.Add(kvp.Value);
            return duplicateFilesGrp;
        }


        // Time (a+b+c) || Space O(1)
        public static string LongestDiverseString(int a, int b, int c)
        {
            List<Pair<int, char>> ls = new List<Pair<int, char>>();
            ls.Add(new Pair<int, char>(a, 'a'));
            ls.Add(new Pair<int, char>(b, 'b'));
            ls.Add(new Pair<int, char>(c, 'c'));
            ls.Sort((x, y) => x.key.CompareTo(y.key));
            char lastCh = 'x';
            StringBuilder sb = new StringBuilder();
            while (ls[2].key > 0)  // we have keys left to insert
            {
                if (lastCh != ls[2].val)
                {
                    lastCh = ls[2].val;
                    sb.Append(lastCh);
                    if (--ls[2].key > 0)    // can be inserted 1 more time
                    {
                        sb.Append(lastCh);
                        --ls[2].key;        // decreament largest key
                    }
                }
                else if (ls[1].key > 0)
                {
                    --ls[1].key;            // decreament 2nd largest key
                    lastCh = ls[1].val;
                    sb.Append(lastCh);
                }
                else
                    break;
                ls.Sort((x, y) => x.key.CompareTo(y.key));   // O(1)
            }
            return sb.ToString();
        }


        // Time O(nlogn) || Space O(n)
        public static int MinimumMovesToEqualArrayElements(int[] nums)
        {
            // Sort array
            Array.Sort(nums);       // O(nlogn)

            int l = nums.Length;
            // Take adjacent difference
            int[] absDiff = new int[l - 1];
            for (int i = 1; i < l; i++)
                absDiff[i - 1] = nums[i] - nums[i - 1];

            // Compute presum
            int[] preSum = new int[l - 1];
            for (int i = 0; i < l - 1; i++)
                preSum[i] = absDiff[i] + (i > 0 ? preSum[i - 1] : 0);

            // Take sum of the resultant array
            int moves = 0;
            for (int i = 0; i < l - 1; i++)
                moves += preSum[i];
            return moves;
        }
        // Time O(n) || Space O(1)
        public static int MinimumMovesToEqualArrayElementsFaster(int[] nums)
        {
            /* "Building upon above O(nlogn) solution"
             * 
             * Say you have an array as:
             * [a, b, c, d, e, f]  // assume sorted
             * [b-a, c-b, d-c, e-d, f-e] // adjacent difference
             * [b-a, c-a, d-a, e-a, f-a] // presum
             * It should have become clear by now but let me spell it out.
             * 
             * we are just subtracting the minimum element from all the elements in nums.
             * (b+c+d+e+f) - 5*a
             * (a+b+c+d+e+f) - 6a // +-a
             * 
             * -> sum(nums) - n*min(nums)
             * So we were able to reduce the time complexity from O(n*lon(n)) to O(n).
             */
            long sum = 0, min = long.MaxValue;
            foreach (var n in nums)
            {
                sum += n;
                min = Math.Min(min, n);
            }
            return (int)(sum - (min * nums.Length));
        }


        // Time O(n^2) Space = O(n)
        public static int MinAreaRect(int[][] points)
        {
            Dictionary<int, HashSet<int>> xSet = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < points.Length; i++)        // O(n)
                if (!xSet.ContainsKey(points[i][0]))
                    xSet[points[i][0]] = new HashSet<int>() { points[i][1] };
                else
                    xSet[points[i][0]].Add(points[i][1]);

            int minArea = int.MaxValue, x1, y1, x2, y2;
            for (int i = 0; i < points.Length; i++)        // O(n^2)
                for (int j = i + 1; j < points.Length; j++)
                {
                    x1 = points[i][0];
                    y1 = points[i][1];
                    x2 = points[j][0];
                    y2 = points[j][1];
                    if (x1 != x2 && y1 != y2 && xSet[x1].Contains(y2) && xSet[x2].Contains(y1))
                        minArea = Math.Min(minArea, Math.Abs(x1 - x2) * Math.Abs(y1 - y2));
                }
            return minArea == int.MaxValue ? 0 : minArea;
        }


        // Time O(n*m) Space O(n), n = length of words array & m = avg len of each word
        public static string LongestPrefixWord(string[] words)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (var word in words)   // O(n)
                set.Add(word);

            string ans = "";
            StringBuilder prefix = new StringBuilder();

            foreach (var word in words)  // O(n)
                if (word.Length >= ans.Length)
                {
                    prefix.Clear();
                    bool valid = true;
                    foreach (var ch in word) // O(m)
                    {
                        prefix.Append(ch);
                        // if any one of the prefix of current word is not present than mark curr word as NotValid
                        if (!set.Contains(prefix.ToString()))
                        {
                            valid = false;
                            break;
                        }
                    }
                    // either longer valid word found or lexographically smaller same length word
                    if (valid && (word.Length > ans.Length || word.CompareTo(ans) < 0))
                        ans = word;
                }
            return ans;
        }


        // Time O(n!) Space O(n^2), n = 9 n.e. no of rows
        public static IList<IList<string>> SolveNQueens(int n)
        {
            /* We need to place 'n' quees in a chessboard of 'n' rows & 'n' cols
             * We try placing queens one by one at each pos[r,c] which is not under attack by any queen
             * & record this position to ensure later on we are not duplicating any sequence of positions
             * and than we mark below 4 straight lines as under attack:
             *      curr row
             *      curr column
             *      left diagonal: row,col transformed into:
             *          int lMin = Math.Min(r, c);
             *          => [r- lMin, c- lMin] left-most starting point of lt diagonal
             *      right diagonal: row,col transformed into:
             *          int rMin = Math.Min(r, -1 + n - c);
             *          => [r - rMin,c + rMin] right-most starting point of rt diagonal
             * places where we place Queen is updated with 'Q' & where we can't place marked with dot '.'
             * once count of placed quees equals 'n' we check if unique configuration than save to ans
             */
            List<IList<string>> ans = new List<IList<string>>();
            bool[] isRowAttacked = new bool[n];
            bool[] isColAttacked = new bool[n];
            HashSet<int>[] isLtDiagAttacked = new HashSet<int>[n];
            HashSet<int>[] isRtDiagAttacked = new HashSet<int>[n];
            Stack<char> currPlacement = new Stack<char>();          // to keep track of where queens have been placed currently
            HashSet<string> uniqPlacements = new HashSet<string>(); // to store all unique positions where queens have been placed

            // create empty board with just dots
            char[][] board = new char[n][];
            for (int r = 0; r < n; r++)                     // O(n)
            {
                isLtDiagAttacked[r] = new HashSet<int>();
                isRtDiagAttacked[r] = new HashSet<int>();
                board[r] = new char[n];
                for (int c = 0; c < n; c++)
                    board[r][c] = '.';
            }
            Try(0, 0);
            return ans;

            // Local helper func
            void Try(int r, int queensPlaced)
            {
                if (queensPlaced == n)
                {
                    string pattern = new string(currPlacement.ToArray());
                    if (!uniqPlacements.Contains(pattern))
                    {
                        uniqPlacements.Add(pattern);
                        string[] sequence = new string[n];
                        for (int i = 0; i < n; i++)
                            sequence[i] = new string(board[i]);
                        ans.Add(sequence);
                    }
                }
                else if (r < n)
                    for (int c = 0; c < n; c++)             // O(n)
                        if (!UnderAttack(r, c))
                        {
                            // Add Queen + Mark Straight lines that are underAttack + Add position where queen is added
                            Mark(r, c, true);

                            // Make Recursive Call
                            Try(r + 1, queensPlaced + 1);

                            //  Remove Queen + Un-Mark Straight lines now not underAttack + Clear position where queen is removed from
                            Mark(r, c, false);
                        }
            }
            bool UnderAttack(int r, int c)                  // O(1)
            {
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);
                if (isRowAttacked[r] || isColAttacked[c] || isLtDiagAttacked[r - lMin].Contains(c - lMin) || isRtDiagAttacked[c + rMin].Contains(r - rMin))
                    return true;
                return false;
            }
            void Mark(int r, int c, bool val)               // O(1)
            {
                isRowAttacked[r] = isColAttacked[c] = val;
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);
                if (val)    // mark position
                {
                    board[r][c] = 'Q';  // Add Queen
                    isLtDiagAttacked[r - lMin].Add(c - lMin);
                    isRtDiagAttacked[c + rMin].Add(r - rMin);
                    // Add position where queen is added
                    currPlacement.Push((char)('0' + r));
                    currPlacement.Push((char)('0' + c));
                }
                else        // Un-mark position
                {
                    board[r][c] = '.';  // Remove Queen
                    isLtDiagAttacked[r - lMin].Remove(c - lMin);
                    isRtDiagAttacked[c + rMin].Remove(r - rMin);
                    // Clear position where queen is removed from
                    currPlacement.Pop();
                    currPlacement.Pop();
                }
            }
        }
        // Time O(n!) Space O(n^2), n = 9 n.e. no of rows
        public static IList<IList<string>> SolveNQueensFaster(int n)
        {
            /* We need to place 'n' quees in a chessboard of 'n' rows & 'n' cols
             * We try placing queens one by one at each pos[r,c] which is not under attack by any queen
             * and than we mark below 4 straight lines as under attack:
             *      curr row
             *      curr column
             *      left diagonal: row,col transformed into:
             *          int lMin = Math.Min(r, c);
             *          => [r- lMin, c- lMin] left-most starting point of lt diagonal
             *      right diagonal: row,col transformed into:
             *          int rMin = Math.Min(r, -1 + n - c);
             *          => [r - rMin,c + rMin] right-most starting point of rt diagonal
             * places where we place Queen is updated with 'Q' & where we can't place marked with dot '.'
             * once count of placed quees equals 'n' we check if unique configuration than save to ans
             */
            List<IList<string>> ans = new List<IList<string>>();
            bool[] isRowAttacked = new bool[n];
            bool[] isColAttacked = new bool[n];
            HashSet<int>[] isLtDiagAttacked = new HashSet<int>[n];
            HashSet<int>[] isRtDiagAttacked = new HashSet<int>[n];

            // create empty board with just dots
            char[][] board = new char[n][];
            for (int r = 0; r < n; r++)                     // O(n)
            {
                isLtDiagAttacked[r] = new HashSet<int>();
                isRtDiagAttacked[r] = new HashSet<int>();
                board[r] = new char[n];
                for (int c = 0; c < n; c++)
                    board[r][c] = '.';
            }
            Try(0, 0);
            return ans;

            // Local helper func
            void Try(int r, int queensPlaced)
            {
                if (queensPlaced == n)
                {
                    // create the chessBoard we got after placing 'n' queens
                    string[] sequence = new string[n];
                    for (int i = 0; i < n; i++)
                        sequence[i] = new string(board[i]);
                    ans.Add(sequence);
                }
                else if (r < n)
                    for (int c = 0; c < n; c++)             // O(n)
                        if (!UnderAttack(r, c))
                        {
                            // Add Queen + Mark Straight lines that are underAttack
                            Mark(r, c, true);

                            // Make Recursive Call
                            Try(r + 1, queensPlaced + 1);

                            //  Remove Queen + Un-Mark Straight lines now not underAttack
                            Mark(r, c, false);
                        }
            }
            bool UnderAttack(int r, int c)                  // O(1)
            {
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);
                if (isRowAttacked[r] || isColAttacked[c] || isLtDiagAttacked[r - lMin].Contains(c - lMin) || isRtDiagAttacked[c + rMin].Contains(r - rMin))
                    return true;
                return false;
            }
            void Mark(int r, int c, bool val)               // O(1)
            {
                isRowAttacked[r] = isColAttacked[c] = val;
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);

                if (val)    // mark position
                {
                    board[r][c] = 'Q';  // Add Queen
                    isLtDiagAttacked[r - lMin].Add(c - lMin);
                    isRtDiagAttacked[c + rMin].Add(r - rMin);
                }
                else        // Un-mark position
                {
                    board[r][c] = '.';  // Remove Queen
                    isLtDiagAttacked[r - lMin].Remove(c - lMin);
                    isRtDiagAttacked[c + rMin].Remove(r - rMin);
                }
            }
        }
        // Time O(n!) Space O(n), n = 9 n.e. no of rows
        public static int TotalNQueens(int n)
        {
            // Algo same as 'SolveNQueensFaster' we just dont store dots & Queens position anywhere
            int ans = 0;
            bool[] isRowAttacked = new bool[n];
            bool[] isColAttacked = new bool[n];
            HashSet<int>[] isLtDiagAttacked = new HashSet<int>[n];
            HashSet<int>[] isRtDiagAttacked = new HashSet<int>[n];

            for (int r = 0; r < n; r++)                     // O(n)
            {
                isLtDiagAttacked[r] = new HashSet<int>();
                isRtDiagAttacked[r] = new HashSet<int>();
            }
            Try(0, 0);
            return ans;

            // Local helper func
            void Try(int r, int queensPlaced)
            {
                if (queensPlaced == n)
                    ans++;
                else if (r < n)
                    for (int c = 0; c < n; c++)             // O(n)
                        if (!UnderAttack(r, c))
                        {
                            // Mark Straight lines that are underAttack
                            Mark(r, c, true);

                            // Make Recursive Call
                            Try(r + 1, queensPlaced + 1);

                            // Un-Mark Straight lines now not underAttack
                            Mark(r, c, false);
                        }
            }
            bool UnderAttack(int r, int c)                  // O(1)
            {
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);
                if (isRowAttacked[r] || isColAttacked[c] || isLtDiagAttacked[r - lMin].Contains(c - lMin) || isRtDiagAttacked[c + rMin].Contains(r - rMin))
                    return true;
                return false;
            }
            void Mark(int r, int c, bool val)               // O(1)
            {
                isRowAttacked[r] = isColAttacked[c] = val;
                int lMin = Math.Min(r, c);
                int rMin = Math.Min(r, -1 + n - c);

                if (val)    // mark position
                {
                    isLtDiagAttacked[r - lMin].Add(c - lMin);
                    isRtDiagAttacked[c + rMin].Add(r - rMin);
                }
                else        // Un-mark position
                {
                    isLtDiagAttacked[r - lMin].Remove(c - lMin);
                    isRtDiagAttacked[c + rMin].Remove(r - rMin);
                }
            }
        }


        // Time = O(nlogn) || Space O(n)
        public static int EatenApples(int[] apples, int[] days)
        {
            int count = 0, day;
            // To keep count of apple as value & their expiery date as key in ascending order to expiry we use Min-PriorityQueue
            var pq = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
            for (day = 0; day < apples.Length; day++)               // O(n)
            {
                // Added to Priority Queue, if atleast one apple is added && it can be eaten till atleast a day or more
                if (apples[day] > 0 && days[day] > 0)
                    pq.Enqueue(apples[day], days[day] + day);        // O(logn)
                // Remove all such entries which have either expired before or expirying today or have no apples left
                while (pq.TryPeek(out int appleCount, out int expiry) && (expiry <= day || appleCount <= 0))
                    pq.Dequeue();                                   // O(logn)

                if (pq.TryDequeue(out int apple, out int expiryDate))
                {
                    count++;                                        // have one apple
                    pq.Enqueue(apple - 1, expiryDate);                 // decreament count
                }
            }

            int leftDays, min;
            // after 'n' days check if there are apples which you can still eat
            while (pq.Count > 0)
            {
                while (pq.TryPeek(out int appleCount, out int expiry) && (expiry <= day || appleCount <= 0))
                    pq.Dequeue();                                   // O(logn)

                if (pq.TryDequeue(out int apple, out int expiryDate))
                {
                    leftDays = expiryDate - day;
                    min = Math.Min(apple, leftDays);
                    count += min;
                    apple -= min;
                    day += Math.Min(min, leftDays);
                    pq.Enqueue(apple, expiryDate);
                }
            }
            return count;
        }


        // Time = Space = O(n) 
        public static int EvalRPN(string[] tokens)
        {
            int secondNo;
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < tokens.Length; i++)
                switch (tokens[i])
                {
                    case "+":
                        st.Push(st.Pop() + st.Pop());
                        break;

                    case "-":
                        secondNo = st.Pop();
                        st.Push(st.Pop() - secondNo);
                        break;

                    case "/":
                        secondNo = st.Pop();
                        st.Push(st.Pop() / secondNo);
                        break;

                    case "*":
                        st.Push(st.Pop() * st.Pop());
                        break;

                    default:
                        st.Push(Convert.ToInt32(tokens[i]));
                        break;
                }
            return st.Pop();
        }


        // Time = Space = O(r*c), r = length of 'rowSum' & c = length of 'colSum'
        public static int[][] RestoreMatrix(int[] rowSum, int[] colSum)
        {
            int rows = rowSum.Length, cols = colSum.Length;
            int[][] ans = new int[rows][];
            for (int r = 0; r < rows; r++)          // O(r)
            {
                ans[r] = new int[cols];
                if (rowSum[r] > 0)                  // Optimization to avoid calculation for rows where req sum = 0
                    for (int c = 0; c < cols; c++)  // O(c)
                    {
                        ans[r][c] = Math.Min(rowSum[r], colSum[c]);
                        rowSum[r] -= ans[r][c];
                        colSum[c] -= ans[r][c];
                    }
            }
            return ans;
        }


        // Time O(n^2) || Space O(n)
        public static int MaxProductOfWordsLength(string[] words)
        {
            int ans = 0, l = words.Length;
            Pair<int, int>[] maskLen = new Pair<int, int>[l];
            for (int i = 0; i < l; i++)                                    // O(n)
                maskLen[i] = new Pair<int, int>(GetMask(words[i]), words[i].Length);

            for (int i = 0; i < l; i++)                                    // O(n^2)
                for (int j = i + 1; j < l; j++)
                    if ((maskLen[i].key & maskLen[j].key) == 0)
                        ans = Math.Max(ans, maskLen[i].val * maskLen[j].val);
            return ans;


            // local helper func
            int GetMask(string word)    // O(26) ~O(1)
            {
                bool[] charSet = new bool[26];
                foreach (var ch in word)
                    charSet[ch - 'a'] = true;
                int mask = 0;
                for (int i = 0; i < charSet.Length; i++)
                    mask += (charSet[i] ? 1 : 0) << i;
                return mask;
            }
        }


        // Time = Space = O(n)
        public static int MaximumUniqueSubarray(int[] nums)
        {
            int max = 0, currSum = 0, i = -1, j = 0, l = nums.Length;
            HashSet<int> uniq = new HashSet<int>();
            while (++i < l)
            {
                if (!uniq.Contains(nums[i]))
                {
                    uniq.Add(nums[i]);
                    currSum += nums[i];
                }
                else
                {
                    while (nums[j] != nums[i])
                    {
                        uniq.Remove(nums[j]);
                        currSum -= nums[j++];
                    }
                    j++;
                }
                max = Math.Max(max, currSum);
            }
            return max;
        }


        // Time = O(r*(c + clogc)) ~O(r*clogc) || Space O(1), r = no of rows & c = no of columns
        public static int LargestSubmatrix(int[][] matrix)
        {
            int rows = matrix.Length, cols = matrix[0].Length, maxArea = 0;
            // pre-process the input, store the count no of consecutive 1's below each cell in matrix
            for (int r = rows - 2; r >= 0; r--)          // O(r*c)
                for (int c = 0; c < cols; c++)
                    if (matrix[r][c] > 0)
                        matrix[r][c] += matrix[r + 1][c];
            // calculate largest area submatrix by sorting each row (Desc order) by value of consecutive 1's starting from each index
            // and now for each index/col we know all cols left to it have more or equal no of consecutive 1's
            // area = count of consecutive 1's starting from curr index idx * (1+idx)
            for (int r = 0; r < rows; r++)             // O(r*(c + clogc))
            {
                Array.Sort(matrix[r], (x, y) => y.CompareTo(x));  // O(clogc)
                for (int c = 0; c < cols; c++)                         // O(c)
                    maxArea = Math.Max(maxArea, matrix[r][c] * (c + 1));
            }
            return maxArea;
        }


        // Time = Space = O(n)
        public static int MaximumGap(int[] nums)
        {
            int n = nums.Length, min = int.MaxValue, max = int.MinValue, maxGap = 0;
            if (n < 2) return 0;    // edge case les than 2 elements, Gap cannot be calculated

            // 1# Find Min & Max in input array
            for (int i = 0; i < n; i++)            // O(n)
            {
                min = Math.Min(min, nums[i]);
                max = Math.Max(max, nums[i]);
            }

            // 2# Calculate the gap-size, length of each bucket
            int interval = (int)(Math.Ceiling((double)(max - min) / (n - 1)));

            // 3# create 2 buckets to store the Min & Max value lying in a given bucket
            int[] minBucket = new int[n - 1];
            int[] maxBucket = new int[n - 1];

            // 3a# Set default values buckets, useful to identify cases when no values goes in a bucket
            for (int i = 0; i < minBucket.Length; i++)
            {
                minBucket[i] = int.MaxValue;
                maxBucket[i] = int.MinValue;
            }

            // 4# iterate thru the input array and update max & min values for each num in buckets it belongs
            for (int i = 0; i < n; i++)
            {
                if (nums[i] == max || nums[i] == min) continue; // Skip adding Min & Max values to Buckets
                // Formula => (number-min) / interval
                var bucketIdx = (nums[i] - min) / interval;
                minBucket[bucketIdx] = Math.Min(minBucket[bucketIdx], nums[i]);
                maxBucket[bucketIdx] = Math.Max(maxBucket[bucketIdx], nums[i]);
            }

            // 5# Calculate max gap possible b/w buckets
            int lastMax = min;
            for (int i = 0; i < minBucket.Length; i++)
                if (minBucket[i] != int.MaxValue)
                {
                    // compare last buckets Max values with current buckets Min value, to get max gap
                    maxGap = Math.Max(maxGap, minBucket[i] - lastMax);
                    lastMax = maxBucket[i]; // now update lastMax for next iteration
                }
            // 5a# check if gap b/w lastMax & global max updates 'ans'
            maxGap = Math.Max(maxGap, max - lastMax);

            return maxGap;
        }


        // Time O(n*m*k) || Space O(n*m), n = length of products array & m = avg length of words in products & k = length of 'searchWord'
        public static IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            TrieForSearchSuggestionSystem t = new TrieForSearchSuggestionSystem();
            foreach (var product in products)   // O(n)
                t.Add(product.ToCharArray());   // O(m)

            List<IList<string>> ans = new List<IList<string>>();
            List<string> matches;
            Stack<char> prefix = new Stack<char>();
            InterviewProblemNSolutions.TrieNode curr = t.root;

            foreach (var ch in searchWord)      // O(k)
            {
                if (curr.children.ContainsKey(ch))  // to the trieNode from where we would begin our search
                    curr = curr.children[ch];       // if child node with given char doesn't exists than break-out
                else break;

                prefix.Push(ch);
                matches = new List<string>();
                t.SearchSuggestion(curr, prefix, matches); // O(n*m)
                ans.Add(matches);
            }
            return ans;
        }


        // Time O(3^n) Space O(n), n = length of string 'num'
        public static IList<string> AddOperators(string num, int target)
        {
            IList<string> ans = new List<string>();
            BackTrack(0, 0, 0, 0, "");
            return ans;

            // local helper func
            void BackTrack(int idx, long prv, long curr, long val, string s)
            {
                if (idx >= num.Length)
                {
                    if (val == target && curr == 0)
                        ans.Add(s);
                    return;
                }

                curr = curr * 10 + num[idx] - '0';
                if (curr > 0)
                    BackTrack(idx + 1, prv, curr, val, s);

                string currStr = curr.ToString();
                if (s == "")
                    BackTrack(idx + 1, curr, 0, val + curr, currStr);
                else
                {
                    BackTrack(idx + 1, curr, 0, val + curr, s + "+" + currStr);
                    BackTrack(idx + 1, -curr, 0, val - curr, s + "-" + currStr);
                    BackTrack(idx + 1, prv * curr, 0, val - prv + prv * curr, s + "*" + currStr);
                }
            }
        }
        public static IList<string> AddOperators_UsingStacksAndStringBuilder(string num, int target)
        {
            IList<string> ans = new List<string>();
            Stack<string> st = new Stack<string>();
            StringBuilder sb = new StringBuilder();
            BackTrack(0, 0, 0, 0);
            return ans;

            // local helper func
            void BackTrack(int idx, long prv, long curr, long val)
            {
                if (idx >= num.Length)
                {
                    if (val == target && curr == 0)
                    {
                        sb.Clear();
                        foreach (var s in st.Reverse())
                            sb.Append(s);
                        ans.Add(sb.ToString());
                    }
                    return;
                }

                curr = curr * 10 + num[idx] - '0';
                if (curr > 0)
                    BackTrack(idx + 1, prv, curr, val);

                string currStr = curr.ToString();
                if (st.Count == 0)
                {
                    st.Push(currStr);
                    BackTrack(idx + 1, curr, 0, val + curr);
                    st.Pop();
                }
                else
                {
                    st.Push("+" + currStr);
                    BackTrack(idx + 1, curr, 0, val + curr);
                    st.Pop();

                    st.Push("-" + currStr);
                    BackTrack(idx + 1, -curr, 0, val - curr);
                    st.Pop();

                    st.Push("*" + currStr);
                    BackTrack(idx + 1, prv * curr, 0, val - prv + prv * curr);
                    st.Pop();
                }
            }
        }


        // Time O(n^2) || Space O(1)
        public static int BeautySum(string s)
        {
            int[] charSet;
            int beauty = 0, diffChar;
            for (int startFrom = 0; startFrom < s.Length; startFrom++)  // O(n)
            {
                charSet = new int[26];
                diffChar = 0;
                for (int j = startFrom; j < s.Length; j++)  // O(n)
                {
                    if (++charSet[s[j] - 'a'] == 1)
                        diffChar++;

                    if (diffChar > 1)                       // O(1)
                        beauty += GetBeauty();              // O(26)
                }
            }
            return beauty;

            // local helper func
            int GetBeauty()                                 // O(26) ~O(1)
            {
                int min = int.MaxValue, max = int.MinValue;
                for (int i = 0; i < 26; i++)
                    if (charSet[i] != 0)
                    {
                        min = Math.Min(min, charSet[i]);
                        max = Math.Max(max, charSet[i]);
                    }
                return max - min;
            }
        }


        // Time O(n) Space O(1)
        public static bool CheckIfPangram(string s)
        {
            int[] set = new int[26];
            int diffChars = 0;
            for (int i = 0; i < s.Length; i++)
                if (++set[s[i] - 'a'] == 1 && ++diffChars >= 26)
                    return true;
            return false;
        }


        // Time O(nlog26) ~O(n) || Space O(26) ~O(1), n = len of input 's'
        public static string ReorganizeString(string s)
        {
            int[] charSet = new int[26];
            for (int i = 0; i < s.Length; i++)  // O(n)
                charSet[s[i] - 'a']++;

            MaxHeapReorganize ob = new MaxHeapReorganize();
            for (int i = 0; i < 26; i++)        // O(26)
                // add all characters with count > 0 to MaxHeap
                if (charSet[i] != 0)
                    ob.Add(charSet[i], i);

            StringBuilder sb = new StringBuilder();
            int last = 1111;
            while (ob.Count > 0)                // O(nlog26)
                if (ob.arr[0][1] != last)
                {
                    last = ob.arr[0][1];
                    sb.Append((char)('a' + last));    // fetch the char with highest freq & add to ans
                    ob.arr[0][0]--;      // decreament count
                    ob.Heapify();
                }
                else if (ob.Count > 1)     // have atleast 1 more char
                {
                    last = ob.arr[1][1];
                    sb.Append((char)('a' + last));    // fetch the char with highest freq & add to ans
                    ob.arr[1][0]--;      // decreament count
                    ob.Heapify(1);
                }
                else break;             // 'Reorganize' Not Possible
            return ob.Count == 0 ? sb.ToString() : "";
        }


        // Time O(Max(nlogn,mlogm)) || Space O(1), n,m = length of 'horizontalCuts' & 'verticalCuts' respectively
        public static int MaxAreaOfPieceOfCake(int h, int w, int[] horizontalCuts, int[] verticalCuts)
        {
            Array.Sort(horizontalCuts);             // O(nlogn)
            Array.Sort(verticalCuts);               // O(mlogm)
            long maxL = 0, maxW = 0, lastCutIdx = 0;
            foreach (var idx in horizontalCuts)      // O(n)
            {
                maxL = Math.Max(maxL, idx - lastCutIdx);
                lastCutIdx = idx;
            }
            maxL = Math.Max(maxL, h - lastCutIdx);

            lastCutIdx = 0;
            foreach (var idx in verticalCuts)        // O(m)
            {
                maxW = Math.Max(maxW, idx - lastCutIdx);
                lastCutIdx = idx;
            }
            maxW = Math.Max(maxW, w - lastCutIdx);
            return (int)((maxW * maxL) % 1000000007);
        }


        // Time O(numDig^lockLen + deadends.Length) || Space O(numDig^lockLen)
        // numDig = 10 (0-9) , lockLen = 4
        public static int OpenLock(string[] deadends, string target)
        {
            HashSet<string> blocker = new HashSet<string>();
            foreach (var deadEnd in deadends)
                blocker.Add(deadEnd);

            // starting pattern of 0000 is present in deadend
            if (blocker.Contains("0000")) return -1;

            return BFS();

            int BFS()
            {
                Queue<Pair<string, int>> q = new Queue<Pair<string, int>>();
                q.Enqueue(new Pair<string, int>(target, 0));

                while (q.Count > 0)
                {
                    var curr = q.Dequeue();
                    if (curr.key == "0000") return curr.val;

                    var currState = curr.key.ToArray();
                    for (int i = 0; i < currState.Length; i++)
                    {
                        var wheelDigit = currState[i] - '0';

                        // next (digit+1)%10
                        currState[i] = (char)(((wheelDigit + 1) % 10) + '0');
                        var nextState = new string(currState);
                        if (!blocker.Contains(nextState))
                            // added next state along with no moves to Q
                            q.Enqueue(new Pair<string, int>(nextState, 1 + curr.val));
                        blocker.Add(nextState); // mark visited as we dont want to evalute this again

                        // prv (digit+9)%10
                        currState[i] = (char)(((wheelDigit + 9) % 10) + '0');
                        nextState = new string(currState);
                        if (!blocker.Contains(nextState))
                            // added next state along with no moves to Q
                            q.Enqueue(new Pair<string, int>(nextState, 1 + curr.val));
                        blocker.Add(nextState); // mark visited as we dont want to evalute this again

                        currState[i] = (char)(wheelDigit + '0');    // reset back curr digit before moving onto next digit
                    }
                }
                return -1;
            }
        }


        // Time O(Max(nlogn,nlogk)) || Space O(n+k)
        public static int MaxPerformanceTeam(int n, int[] speed, int[] efficiency, int k)
        {
            int[][] players = new int[n][];
            for (int i = 0; i < n; i++)         // O(n)
                players[i] = new int[] { speed[i], efficiency[i] };

            // sort in decreasing order of efficiency, if two players have same efficiency than choose one with higher speed first
            Array.Sort(players, (x, y) => y[1].CompareTo(x[1]));

            long maxPerf = 0, totalSpeed = 0, mod = 1000000007;
            MinHeap mheap = new MinHeap(k + 1);

            for (int i = 0; i < n; i++)         // O(n)
            {
                mheap.Insert(players[i][0]);    // insert ith player speed to MinHeap
                totalSpeed += players[i][0];    // add ith speed to total speed sum

                if (mheap.Count > k)            // if Heap has more than 'k' than pop the player with min Speed
                    totalSpeed -= mheap.ExtractMin();       // O(logk)

                // update max team performance (curr player has the min efficieny)
                maxPerf = Math.Max(maxPerf, totalSpeed * players[i][1]);
            }
            return (int)(maxPerf % mod);
        }


        // Time = Space = O(n), n = no of digits in 'num'
        public static string NumberToWords(int num)
        {
            /* while num > 0, we get the mod of num with 1000
             *      & convert this val (having max 3 digits) into words
             * 
             *      increament the level starting from => "" << Thousand << Million << Billion << Trillion
             *      and append this keyword from current level to end of string if value was > 0
             *      
             *      divide the num by 1000
             */

            if (num < 1) return "Zero";

            string[] digitName = new string[100];
            #region Base Conversion
            digitName[0] = "";
            digitName[1] = "One";
            digitName[2] = "Two";
            digitName[3] = "Three";
            digitName[4] = "Four";
            digitName[5] = "Five";
            digitName[6] = "Six";
            digitName[7] = "Seven";
            digitName[8] = "Eight";
            digitName[9] = "Nine";
            digitName[10] = "Ten";
            digitName[11] = "Eleven";
            digitName[12] = "Twelve";
            digitName[13] = "Thirteen";
            digitName[14] = "Fourteen";
            digitName[15] = "Fifteen";
            digitName[16] = "Sixteen";
            digitName[17] = "Seventeen";
            digitName[18] = "Eighteen";
            digitName[19] = "Nineteen";
            digitName[20] = "Twenty";
            digitName[30] = "Thirty";
            digitName[40] = "Forty";
            digitName[50] = "Fifty";
            digitName[60] = "Sixty";
            digitName[70] = "Seventy";
            digitName[80] = "Eighty";
            digitName[90] = "Ninety";
            #endregion
            string[] levels = new string[] { "", "Thousand", "Million", "Billion", "Trillion" };

            int level = 0;
            Stack<string> st = new Stack<string>();
            while (num > 0)
            {
                Convert3Digits(num % 1000, level);
                num /= 1000;
                level++;    // increament level from Thousand => Million, Million to Billion, etc
            }

            StringBuilder sb = new StringBuilder();
            // append all values from Stack to Stringbuilder and than append one space also
            while (st.Count > 1)
                sb.Append(st.Pop()).Append(' ');

            // append last values without Space
            sb.Append(st.Pop());

            return sb.ToString();

            // local helper func
            void Convert3Digits(int val, int curLevel)
            {
                if (val < 1) return;

                // var levelName = levels[curLevel];
                if (curLevel > 0)
                    st.Push(levels[curLevel]);

                int lastTwo = val % 100;
                if (lastTwo < 20)
                {
                    if (lastTwo > 0)
                        st.Push(digitName[lastTwo]);
                }
                else
                {
                    // int lastD = lastTwo % 10;
                    if (lastTwo % 10 > 0)
                        st.Push(digitName[lastTwo % 10]);

                    // int secondLast = (lastTwo / 10) * 10;
                    lastTwo /= 10;
                    if (lastTwo > 0)
                        st.Push(digitName[lastTwo * 10]);
                }

                val /= 100;
                if (val > 0)
                {
                    st.Push("Hundred");
                    st.Push(digitName[val]);
                }
            }
        }


        // Time = Space = O(n), n = length of 'nums'
        public static int LongestConsecutive(int[] nums)
        {
            int ans = 0, currLongestSeq, currNum;
            HashSet<int> set = new HashSet<int>(nums.Length);
            for (int i = 0; i < nums.Length; i++)
                set.Add(nums[i]);

            for (int i = 0; i < nums.Length; i++)
                // if set doesnt contains num-1 than check to see longest sequence we can get starting from curr num
                if (!set.Contains(nums[i] - 1))
                {
                    currNum = nums[i];
                    currLongestSeq = 1;
                    // find longest seq starting from nums[n]
                    while (set.Contains(++currNum))
                        currLongestSeq++;
                    ans = Math.Max(ans, currLongestSeq);
                }
            return ans;
        }


        // Time O(n) Space O(1), n = len of 'nums'
        public static int NumberOfSubarraysWithBoundedMaximum(int[] nums, int left, int right)
        {
            int startIdx = 0, count = 0, ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > right)
                {
                    startIdx = i + 1;
                    count = 0;
                }
                else if (left <= nums[i])
                    count = 1 + i - startIdx;

                ans += count;
            }
            return ans;
        }


        // Time O(nlogm) Space O(1), n = len of arr1, m = len of arr2
        public static int FindTheDistanceValueBetweenTwoArrays(int[] arr1, int[] arr2, int d)
        {
            Array.Sort(arr2);
            int ans = 0;
            foreach (var n in arr1)      // O(n)
                if (Math.Abs(n - GetCloset(n)) > d)    // O(logm)
                    ans++;
            return ans;

            // local helper func
            int GetCloset(int num)
            {
                int left = 0, right = arr2.Length - 1, mid;
                while (left <= right)
                {
                    mid = left + (right - left) / 2;
                    if (arr2[mid] < num)
                        left = mid + 1;
                    else if (arr2[mid] > num)
                        right = mid - 1;
                    else
                        return num;
                }
                if (right < 0) return arr2[left];
                if (left == arr2.Length) return arr2[right];
                return Math.Abs(arr2[left] - num) < Math.Abs(arr2[right] - num) ? arr2[left] : arr2[right];
            }
        }


        // Time = Space = O(n*k)
        public static int KInversePairs(int n, int k)
        {
            int[,] dp = new int[n + 1, k + 1];
            // build all ansers for values of 'n' starting from 1
            for (int nth = 1; nth <= n; nth++)
                for (int kth = 0; kth <= k; kth++)
                    if (kth == 0)
                        // for k=0 inversion there is only 1 possibility where all elements r in asc order
                        dp[nth, kth] = 1;
                    else
                        for (int i = 0; i <= Math.Min(kth, nth - 1); i++)
                            dp[nth, kth] = (dp[nth, kth] + dp[nth - 1, kth - i]) % 1000000007;
            return dp[n, k];
        }


        // Time O(r*c*logn) || Space O(r*c) , r,c = rows & cols in 'grid' & n = max cell elevation found in grid
        public static int SwimInRisingWater(int[][] grid)
        {
            int row = grid.Length, col = grid[0].Length;
            // lowest time we need to wait is the level of 'destination' cell elevation
            int low = grid[row - 1][col - 1], high = GetMax(), time, ans = grid[row - 1][col - 1];
            bool[,] visited;
            while (low <= high)             // O(logn)
            {
                time = low + (high - low) / 2;
                visited = new bool[row, col];
                if (CanReach(0, 0))         // O(rows*cols)
                {
                    ans = time;
                    high = time - 1;
                }
                else
                    low = time + 1;
            }
            return ans;

            // local helper func
            // DFS traversal
            bool CanReach(int i, int j)
            {
                // if outside grid boundry or curr cell elevation more than 'time' value or current cell has been traversed before
                if (i < 0 || i >= row || j < 0 || j >= col || grid[i][j] > time || visited[i, j]) return false;

                // reached destination cell
                if (i == row - 1 && j == col - 1) return true;

                visited[i, j] = true;       // mark visited

                // try traversal in all 4 possible directions
                return (CanReach(i, j - 1) || CanReach(i - 1, j) || CanReach(i, j + 1) || CanReach(i + 1, j)) ? true : false;
            }

            int GetMax()
            {
                int val = int.MinValue;
                for (int r = 0; r < row; r++)
                    for (int c = 0; c < col; c++)
                        val = Math.Max(grid[r][c], val);
                return val;
            }
        }

        // Time O(nlogn) | Space O(n), where n = total no of cells in the grid
        public static int SwimInRisingWater_PriorityQueue(int[][] grid)
        {
            /*
            We start from the top and for each cell and all valid 4 direction we add max of (waterLevel required to reach current, curr cell val)
            in a min heap priority queue 

            after adding we pickup the smallest wt n.e. GetMin() from min-heap
            and repeat this step and stop once we reach the destination
            */
            int n = grid.Length, w = 0, r, c;
            bool[,] visited = new bool[n, n];
            PriorityQueueForGrid pq = new PriorityQueueForGrid();
            pq.Add(new PairForGrid(0, 0, 0));
            while (pq.count > 0)
            {
                var cur = pq.GetMin();
                r = cur.x;
                c = cur.y;
                w = Math.Max(cur.wt, grid[r][c]);
                if (r == n - 1 && c == n - 1) break;

                if (visited[r, c]) continue;
                visited[r, c] = true;

                if (c + 1 < n && !visited[r, c + 1])   // right
                    pq.Add(new PairForGrid(Math.Max(w, grid[r][c + 1]), r, c + 1));
                if (c - 1 >= 0 && !visited[r, c - 1])  // left
                    pq.Add(new PairForGrid(Math.Max(w, grid[r][c - 1]), r, c - 1));
                if (r + 1 < n && !visited[r + 1, c])   // down
                    pq.Add(new PairForGrid(Math.Max(w, grid[r + 1][c]), r + 1, c));
                if (r - 1 >= 0 && !visited[r - 1, c])  // up
                    pq.Add(new PairForGrid(Math.Max(w, grid[r - 1][c]), r - 1, c));
            }
            return w;
        }

        // Time O(nlogn) Space O(n), n = length of 'stations'
        public static int MinRefuelStops(int target, int startFuel, int[][] stations)
        {
            /*
            as we traverse the sorted list of stations (asc order of distance from start)
            we add the stations in MaxHeap sorted by fuel at given station

            at any point if we reach >=taget than return curr PitStops 
            if we run out of fuel before reaching target & we have no station in ouyr heap return -1
            but if we have some station in MaxHeap pick the top one & increament the 'PitStops' by 1
            */
            int fuelInTank = startFuel, pitStops = 0, lastPos = 0, j = -1;
            MaxHeapMinRefuelStops mHeap = new MaxHeapMinRefuelStops(stations.Length);
            while (++j < stations.Length) // O(n)
            {
                fuelInTank -= stations[j][0] - lastPos;
                lastPos = stations[j][0];

                if (fuelInTank < 0)
                {
                    while (fuelInTank < 0 && mHeap.Count > 0)
                    {
                        pitStops++;
                        var st = mHeap.ExtractMax();
                        fuelInTank += st[1];
                    }
                    // if still not enouf fuel to make it to next station
                    if (fuelInTank < 0) return -1;
                }

                // add curr fuel station to MaxHeap
                mHeap.Add(stations[j]); // O(logn)
            }

            fuelInTank -= target - lastPos;
            while (fuelInTank < 0 && mHeap.Count > 0)
            {
                pitStops++;
                var st = mHeap.ExtractMax();
                fuelInTank += st[1];
            }
            return fuelInTank >= 0 ? pitStops : -1;
        }


        // Time O(n * m^2) Space O(n), n = length of 'words', m = avg length of each word
        public static IList<IList<int>> PalindromePairs(string[] words)
        {
            IList<IList<int>> ans = new List<IList<int>>();
            int l = words.Length;

            // add all words in array to Dictionary with their index as value
            Dictionary<string, int> wordIdx = new Dictionary<string, int>(l);
            for (int i = 0; i < l; i++)        // O(n)
                wordIdx[words[i]] = i;

            // iterate thru each word present in input
            for (int i = 0; i < l; i++)        // O(n)
            {
                // Case 1 : Reverse of current word in present
                var rev = words[i].Reverse();  // O(m)
                if (wordIdx.ContainsKey(rev))
                {
                    int j = wordIdx[rev];
                    if (i != j)
                        ans.Add(new int[] { i, j });
                }
                // Case 2 : found reverse of prefix of curr word whose last half is palindromic
                foreach (var prefix in GetPreFix(words[i])) // O(m^2)
                {
                    var revPrefix = prefix.Reverse();
                    if (wordIdx.ContainsKey(revPrefix))
                        ans.Add(new int[] { i, wordIdx[revPrefix] });
                }
                // Case 3 : found reverse of suffix of curr word whose first half is palindromic
                foreach (var suffix in GetSufFix(words[i]))  // O(m^2)
                {
                    var revSuffix = suffix.Reverse();
                    if (wordIdx.ContainsKey(revSuffix))
                        ans.Add(new int[] { wordIdx[revSuffix], i });
                }
            }
            // special case of having one empty string in input array
            if (wordIdx.ContainsKey(""))
            {
                int blankIdx = wordIdx[""];
                for (int i = 0; i < l; i++)     // O(n)
                    if (blankIdx != i && IsPalindrome(words[i]))
                    {
                        ans.Add(new int[] { i, blankIdx });
                        ans.Add(new int[] { blankIdx, i });
                    }
            }
            return ans;


            // local helper func
            List<string> GetPreFix(string a)
            {
                List<string> pList = new List<string>();
                for (int i = 0; i < a.Length - 1; i++)
                {
                    string left = a.Substring(0, i + 1);
                    string right = a.Substring(i + 1);
                    if (IsPalindrome(right))
                        pList.Add(left);
                }
                return pList;
            }
            List<string> GetSufFix(string a)
            {
                List<string> sList = new List<string>();
                for (int i = 0; i < a.Length - 1; i++)
                {
                    string left = a.Substring(0, i + 1);
                    string right = a.Substring(i + 1);
                    if (IsPalindrome(left))
                        sList.Add(right);
                }
                return sList;
            }
            bool IsPalindrome(string a)
            {
                int left = 0, right = a.Length - 1;
                while (left < right)
                    if (a[left++] != a[right--])
                        return false;
                return true;
            }
            /*
            // Time O(n^2 * m) Space O(1), n = length of 'words', m = avg length of each word
            public IList<IList<int>> PalindromePairs(string[] words)
            {
                IList<IList<int>> ans = new List<IList<int>>();
                for(int n=0;n<words.Length;n++)
                    for(int j=n+1;j<words.Length;j++)
                    {
                        if(IsPalindrome(words[n],words[j]))
                            ans.Add(new int[] {n,j});
                        if(IsPalindrome(words[j],words[n]))
                            ans.Add(new int[] {j,n});
                    }
                return ans;

                // local helper func
                bool IsPalindrome(string a, string b)
                {
                    string merge = a+b;
                    int left = 0, right = merge.Length-1;
                    while(left<right)
                        if(merge[left++]!=merge[right--])
                            return false;
                    return true;
                }
            }
            */
        }


        // Time O(Max(n,m)*k) || Space O(1), n = length of 's', m = avg length of word in 'words' & k = length of array 'words'
        public static int NumMatchingSubseq_BruteForce(string s, string[] words)
        {
            int ans = 0;
            foreach (var word in words)     // O(k)
                if (Subsequence(word))      // O(Max(n,m))
                    ans++;
            return ans;
            // local helper func
            bool Subsequence(string w)
            {
                int i = 0;
                foreach (var ch in s)
                    if (ch == w[i])    // characters match
                        if (++i == w.Length)   // move to next character of word till we have matched all
                            break;
                return i == w.Length;
            }
        }
        // Time O(Max(n,m*k)) || Space O(n), n = length of 's', m = avg length of word in 'words' & k = length of array 'words'
        public static int NumMatchingSubseq(string s, string[] words)
        {
            // dictionary to store all characters present in string 's' as key with their list of indices as value
            Dictionary<char, List<int>> charIdx = new Dictionary<char, List<int>>();
            for (int i = 0; i < s.Length; i++)      // O(n)
                if (!charIdx.ContainsKey(s[i]))
                    charIdx[s[i]] = new List<int>() { i };
                else
                    charIdx[s[i]].Add(i);

            int ans = 0;
            bool foundNextChar;
            foreach (var word in words)             // O(k)
            {
                int i = 0, lastIdx = -1;
                while (i < word.Length)             // O(m)
                {
                    if (!charIdx.ContainsKey(word[i])) break;
                    foundNextChar = false;
                    foreach (var idx in charIdx[word[i]])
                        if (idx > lastIdx)
                        {
                            lastIdx = idx;
                            foundNextChar = true;
                            i++;
                            break;
                        }
                    if (!foundNextChar) break;  // next char was not found in 's'
                }

                if (i == word.Length)
                    ans++;
            }
            return ans;
        }


        // Time O(4^n) || Recursive Space O(n), n = maxMoves
        public static int OutOfBoundaryPathsBruteForce(int m, int n, int maxMove, int startRow, int startColumn)
        {
            return DFS(0, startRow, startColumn);
            // local helper func
            int DFS(int totalMoves, int curR, int curC)
            {
                // total moves count should be <= max allowed moves
                if (totalMoves > maxMove) return 0;
                // reached out of GRID
                if (curR < 0 || curR >= m || curC < 0 || curC >= n) return 1;

                totalMoves++;

                return (DFS(totalMoves, curR - 1, curC) +
                        DFS(totalMoves, curR + 1, curC) +
                        DFS(totalMoves, curR, curC - 1) +
                        DFS(totalMoves, curR, curC + 1)
                       ) % 1000000007;
            }
        }
        // Time = Space = O(m*n*maxMove), Dp-Top-Down approach
        public static int OutOfBoundaryPaths(int m, int n, int maxMove, int startRow, int startColumn)
        {
            long mod = 1000000007;
            // dp to store for each cell in GRID no of way to reach boundry
            Dictionary<int, long>[,] dp = new Dictionary<int, long>[m, n];
            for (int r = 0; r < m; r++)
                for (int c = 0; c < n; c++)
                    dp[r, c] = new Dictionary<int, long>();

            return (int)(Paths(startRow, startColumn, maxMove) % mod);

            // local helper recursive Dp-Top-Down Soln
            long Paths(int r, int c, int movesLeft)
            {
                if (r < 0 || r >= m || c < 0 || c >= n) return 1;
                // no more moves left to proceed further
                if (movesLeft == 0) return 0;

                if (dp[r, c].ContainsKey(movesLeft))
                    return dp[r, c][movesLeft];

                // decrease moves left by 1
                movesLeft--;

                return dp[r, c][movesLeft] = (Paths(r - 1, c, movesLeft) +
                                                Paths(r + 1, c, movesLeft) +
                                                Paths(r, c - 1, movesLeft) +
                                                Paths(r, c + 1, movesLeft)
                                             ) % mod;
            }
            /* Alternate Implementation
             * 
            // dp to store for each cell in GRID no of way to reach boundry
            int[,,] dp = new int[m, n, maxMove + 1];
            for (int r = 0; r < m; r++)
                for (int c = 0; c < n; c++)
                    for (int moves = 1; moves <= maxMove; moves++)
                        dp[r, c, moves] = -1;

            return (int)(Paths(startRow, startColumn, maxMove));

            // local helper recursive Dp-Top-Down Soln
            int Paths(int r, int c, int movesLeft)
            {
                if (r < 0 || r >= m || c < 0 || c >= n) return 1;
                // no more moves left to proceed further
                if (movesLeft == 0) return 0;

                if (dp[r, c, movesLeft] != -1)
                    return dp[r, c, movesLeft];

                // decrease moves left by 1
                movesLeft--;

                return dp[r, c, movesLeft] = (Paths(r - 1, c, movesLeft) + Paths(r + 1, c, movesLeft) + Paths(r, c - 1, movesLeft) + Paths(r, c + 1, movesLeft)) % 1000000007;
            }
            */
        }


        // Time O(nlogn) Space O(n), BST based approach
        public static IList<int> CountSmaller(int[] nums)
        {
            CountSmallerBST bst = new CountSmallerBST();
            for (int i = nums.Length - 1; i >= 0; i--)
                nums[i] = bst.Add(ref bst.root, nums[i], 0);
            return nums;
        }
        // Time O(nlogn) Space O(n), Segment-Tree based approach
        public static IList<int> CountSmaller_SegmentTree(int[] nums)
        {
            SegmentTree_SmallerCount bst = new SegmentTree_SmallerCount(nums);
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                bst.UpdateTree(bst.root, nums[i], 1);           // update count for 'nums[n]' idx
                nums[i] = bst.SumRange(bst.root, bst.Start, nums[i] - 1);   // calculate no of no in range Start..nums[n]-1
            }
            return nums;
        }


        // Time O(n) Space O(1), n = Min(length of linkedlist,right)
        public static ListNode ReverseBetween(ListNode head, int left, int right)
        {
            ListNode dummyN = new ListNode(0);
            dummyN.next = head;

            ListNode prv = dummyN, curr = null, nextNode = dummyN, reverseAfter = null;
            int pos = 0;

            while (++pos <= right + 1)
                if (pos <= left)
                {
                    prv = nextNode;
                    nextNode = nextNode.next;
                }
                else // if (pos > left)
                {
                    if (reverseAfter == null)
                        reverseAfter = prv;

                    curr = nextNode;
                    nextNode = nextNode.next;
                    curr.next = prv;
                    prv = curr;
                }

            reverseAfter.next.next = nextNode;  // stich the reversed end->next to next in original
            reverseAfter.next = curr;           // stich the node before the reversal started->next to reverseHead
            return dummyN.next;
        }


        // Time = Space= O(n), n = length of 'rating', 3-Pass
        public static int Candy(int[] ratings)
        {
            /* Each child must have 1 candy base case
            * first we scan from left to right
            *       if ith rating > n-1th rating
            *       update ith = (n-1th candies) + 1
            * now second pass we move from right
            *       if n-1th rating > ith rating & candyCount[n-1] <= candyCount[n]
            *       candyCount[n-1] = candyCount[n] + 1
            */
            int l = ratings.Length, totalCandy = 0;
            int[] candyCount = new int[l];

            for (int i = 1; i < l; i++)    // O(n)
                if (ratings[i] > ratings[i - 1])
                    candyCount[i] = candyCount[i - 1] + 1;

            for (int i = l - 2; i >= 0; i--) // O(n)
                if (ratings[i] > ratings[i + 1] && candyCount[i] <= candyCount[i + 1])
                    candyCount[i] = Math.Max(candyCount[i], candyCount[i + 1] + 1);

            for (int i = 0; i < l; i++)    // O(n)
                totalCandy += candyCount[i];

            return totalCandy + l;
        }


        // Time O(logN + k), Space O(k) || Binary Search + Sliding Window approach, n = length of 'arr'
        public static IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            // ALGO
            // Find closed num to 'x'
            // than start traversing in both left & right direction
            // and choose the num which is closed to 'x'
            // if its left than update left-- after adding the num
            // else if its right than update right++ after adding the num
            // if both are equal distance apart than choose left
            // if you reach end of either left or right boundry set that side num as int.MaxValue
            // at end sort the 'ans' List
            // - even faster approach would be to just keep updating left & right till 'k' nums are found & than create a new 'ans' with all values starting from left+1 to right-1

            int closetIdx = GetCloset();                // O(logn)
            int lt = closetIdx - 1, rt = closetIdx + 1;
            long left, right;

            int[] ans = new int[k];
            //List<int> ans = new List<int>() { arr[closetIdx] };
            // find 'k-1' closet from array
            while (--k > 0)                             // O(Min(n,k))
            {
                left = lt >= 0 ? arr[lt] : int.MaxValue;
                right = rt < arr.Length ? arr[rt] : int.MaxValue;
                if (Math.Abs(left - x) > Math.Abs(right - x))
                {
                    //ans.Add((int)right);
                    rt++;
                }
                else //if(Math.Abs(left-x)<=Math.Abs(right-x))
                {
                    //ans.Add((int)left);
                    lt--;
                }
            }
            //ans.Sort();                               // O(klogk)

            int i = 0;
            lt++;
            while (lt < rt)                             // O(k)
                ans[i++] = arr[lt++];

            return ans;

            // local helper func
            int GetCloset()
            {
                int start = 0, last = arr.Length - 1, mid, idx = 0;
                long closetVal = int.MaxValue;
                while (start <= last)
                {
                    mid = start + (last - start) / 2;
                    if (arr[mid] == x) return mid;
                    else if (arr[mid] > x)
                    {
                        if (Math.Abs(arr[mid] - x) < Math.Abs(closetVal - x))
                        {
                            closetVal = arr[mid];
                            idx = mid;
                        }
                        last = mid - 1;
                    }
                    else //if(arr[mid]<x)
                    {
                        if (Math.Abs(arr[mid] - x) < Math.Abs(closetVal - x))
                        {
                            closetVal = arr[mid];
                            idx = mid;
                        }
                        start = mid + 1;
                    }
                }
                return idx;
            }
        }


        // Time O(Min(m,nlogn)) || Space O(1), n = length of 'boxTypes' & m = 'truckSize'
        public static int MaximumUnits(int[][] boxTypes, int truckSize)
        {
            Array.Sort(boxTypes, (x, y) => y[1].CompareTo(x[1])); // O(nlogn), sorting based on no of units in each type of box
            int maxUnits = 0, i = 0, minUnitThatCanBeExtracted;
            while (truckSize > 0 && i < boxTypes.Length)         // O(Min(n,m)
            {
                minUnitThatCanBeExtracted = Math.Min(boxTypes[i][0], truckSize);

                maxUnits += minUnitThatCanBeExtracted * boxTypes[i][1];
                truckSize -= minUnitThatCanBeExtracted;
                i++;
            }
            return maxUnits;
            /*
            Array.Sort(boxTypes, (x,y) => y[1].CompareTo(x[1])); // O(nlogn)
            int maxUnits = 0, n=0;
            while(truckSize > 0 && n < boxTypes.Length)         // O(Min(n,m)
            {
                maxUnits+=boxTypes[n][1];
                // decreament truck capacity by 1
                truckSize--;
                // decreament the no of boxes of ith type
                if(--boxTypes[n][0] == 0)
                    n++;
            }   
            return maxUnits;
             */
        }


        // Time = Space = O(r*c), r = rows in 'mat' & c = cols in 'mat'
        public static int[][] MatrixReshape(int[][] mat, int rNew, int cNew)
        {
            int row = mat.Length, col = mat[0].Length;
            if (row * col != rNew * cNew) return mat;

            int[][] ans = new int[rNew][];

            //for (int r = 0; r < rNew; r++)
            //    ans[r] = new int[cNew];

            int[] newLine = new int[cNew];

            int ir = 0, ic = 0;
            // read all the values from the original matrix
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    //// update values in 'reshaped' matrix
                    //ans[ir][ic++] = mat[r][c];

                    newLine[ic++] = mat[r][c];

                    // if reached end of the column of reshaped matrix
                    if (ic == cNew)
                    {
                        // add current filled row to reshaped matrix & update row idx
                        ans[ir++] = newLine;
                        newLine = new int[cNew];
                        ic = 0; // reset col idx to 0
                    }
                }
            return ans;
        }


        // Time = O(nlogn) Space = O(n)
        public static int ReduceArraySizeToTheHalf(int[] arr)
        {
            Dictionary<int, int> numFreq = new Dictionary<int, int>();
            // fetch all numbers and update their frequency in Dictionary
            for (int i = 0; i < arr.Length; i++)                                // O(n)
                if (!numFreq.ContainsKey(arr[i]))
                    numFreq[arr[i]] = 1;
                else
                    numFreq[arr[i]]++;

            int ans = 0, setSize = 0;
            foreach (var kvp in numFreq.OrderByDescending(x => x.Value))        // O(nlogn) because we are sorting the Dictionary in decreasing order of frequency of numbers
                if (ans < arr.Length / 2)
                {
                    setSize++;
                    ans += kvp.Value;
                }
                else break;

            return setSize;
        }


        // Time O(nlogk) Space O(k), n = no of elements in matrix
        public static int KthSmallestInSortedMatrix(int[][] mat, int k)
        {
            MaxHeap maxH = new MaxHeap(k);
            for (int r = 0; r < mat.Length; r++)
                for (int c = 0; c < mat[r].Length; c++)
                    if (maxH.Count < k)
                        maxH.Insert(mat[r][c]);
                    else if (mat[r][c] < maxH.GetMax())
                    {
                        maxH.ExtractMax();
                        maxH.Insert(mat[r][c]);
                    }
            return maxH.ExtractMax();
        }


        // Time O(n+mlogm) | Space O(n+m), n & m = length of string 'order' and 's' respectively
        public static string CustomSortString_Sorting(string order, string s)
        {
            /* ALGO
            1. Find & save the priority of characters in 'charPriority' by iterating thru 1st string 'order'.
            2. The left most characters has the highest priority assign it 26 and decrease priority value by 1 for next char to come
            3. this way for each character present in 'order' we wud have set its priority ranging from 26..1, any char which is not present will have the priority '0'
            4. Now we simple sort the 2nd string 's' where we compare each character being sorted with their values from 'charPriority' array
            5. Higher the value == higher the priority and will come before next character
            6. All characters which are not present in 'order' have 0 priority hence will automatically we sorted and moved towards the end.
             */
            int priority = 26; // highest priority
            int[] charPriority = new int[26];
            // set the priority of all characters in 1st string 'order'
            foreach (var ch in order)        // O(n)
                charPriority[ch - 'a'] = priority--;
            // sort 2nd string 's' as per above priority
            var sorted = s.ToCharArray();   // O(m)
            Array.Sort(sorted, (x, y) => charPriority[y - 'a'].CompareTo(charPriority[x - 'a'])); // O(mlogm)
            return new string(sorted);
        }
        // Time O(Max(n,m), n,m = length of 'str' & 'order' respectively
        public static string CustomSortString(string order, string str)
        {
            // get the count of different chars in 'str'
            int[] count = new int[26];                  // O(n)
            for (int i = 0; i < str.Length; i++)
                count[str[i] - 'a']++;

            int idx = 0;
            char[] sorted = new char[str.Length];
            // read the chars in 'order' and append current char to 'sorted' 'count' times
            foreach (char ch in order)                  // O(m)
                while (count[ch - 'a']-- > 0)
                    sorted[idx++] = ch;

            // add all remaining characters to 'sorted' who's order was not provided in 'order' string
            for (int i = 0; i < count.Length; i++)
                while (count[i]-- > 0)                  // O(n)
                    sorted[idx++] = (char)(i + 'a');

            return new string(sorted);
        }


        // Time = Space = O(n), n = length of 'number'
        public static string ReformatNumber(string number)
        {
            Queue<char> q = new Queue<char>();
            foreach (var ch in number)
                if (ch - '0' >= 0 && ch - '0' <= 9)
                    q.Enqueue(ch);

            StringBuilder sb = new StringBuilder();
            while (q.Count > 4)
                sb.Append(q.Dequeue()).Append(q.Dequeue()).Append(q.Dequeue()).Append('-');

            if (q.Count == 3)
                sb.Append(q.Dequeue()).Append(q.Dequeue()).Append(q.Dequeue());
            else if (q.Count == 4)
                sb.Append(q.Dequeue()).Append(q.Dequeue()).Append('-').Append(q.Dequeue()).Append(q.Dequeue());
            else //if(q.Count==2)
                sb.Append(q.Dequeue()).Append(q.Dequeue());

            return sb.ToString();
        }


        // Time O(n^2) Space O(1), n = length of 'nums' array
        public static int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);       // O(nlogn)
            int triplets = 0, left, right;
            for (int i = 2; i < nums.Length; i++)
            {
                left = 0; right = i - 1;
                while (left < right)
                    if (nums[left] + nums[right] > nums[i])     // triangle can be formed
                        triplets += right-- - left;             // update count and reduce right by 1 to see again if more triplets can be formed,
                                                                // once we found a pair left,right,n we increament count by diff of right-left
                                                                // Bcoz all remaining nums between left to right can also form triplet
                    else
                        left++;                                 // keep moving left towards right to increase value of left side
            }
            return triplets;
        }


        // Time O(n) Space O(1)
        public static int[] ThreeEqualParts(int[] A)
        {
            int ones = 0, len = A.Length;
            // counts 1's
            for (int i = 0; i < len; i++)                               // O(n)
                if (A[i] == 1)
                    ones++;

            // if total 1's not in multiple of three return
            if (ones % 3 != 0)
                return new int[] { -1, -1 };

            // corner case only zero's r present in input
            if (ones == 0)
                return new int[] { 0, 2 }; // we can divide array from anywhere as each part wud be == 0

            int p1 = 0, p2 = 0, p3 = 0, count = 0, target = ones / 3;
            for (int i = 0; i < len; i++)                               // O(n)
                if (A[i] == 1)
                {
                    if (count == 0)
                        p1 = i;
                    else if (count == target)
                        p2 = i;
                    else if (count == 2 * target)
                        p3 = i;

                    if (count++ == 2 * target) break;
                }

            int orignalP2 = p2, originalP3 = p3;
            while (p3 < len && p1 < orignalP2 && p2 < originalP3)       // O(n)
                // if at any point all 3 values dont match means split is not possible
                if (A[p1] != A[p2] || A[p2] != A[p3])
                    return new int[] { -1, -1 };
                else
                { p1++; p2++; p3++; }

            // we check if p3 reached len of array to confirm all values match
            return p3 == len ? new int[] { p1 - 1, p2 } : new int[] { -1, -1 };
        }


        // Time O(n) Space O(1)
        public static string PushDominoes(string dominoes)
        {
            /*
            we initially set the last state as 'L'
            we inspect each char from 0th index to len-1
            if current state is empty n.e. '.' we increament the counter count which initially is set 0
            if the current state is :

                Case 1  => '.'
                    increament count by 1
                Case 2  => 'R'
                    if last state was also 'R'
                        than we append 'R' to final result count times & reset the count to 0
                    else //if last was 'L'
                        than we append '.' to final result count times
                Case 3 => 'L'
                    if last state was also 'L'
                        than we append 'L' to final result count times & reset the count to 0
                    else //if last was 'R'
                        if check if count is odd or even
                            than we append 'R' to final result count/2 times
                            if count was odd we append one '.'
                            lastly we append 'L' to final result count/2 times
                if current state is not '.' we also append curr state once to result
            */
            StringBuilder sb = new StringBuilder();
            char lastState = 'L', toAdd;
            int count = 0, times;
            foreach (var ch in dominoes)
            {
                if (ch == '.')
                {
                    count++;
                    continue;
                }
                if (ch == 'R')
                {
                    toAdd = lastState == 'R' ? 'R' : '.';
                    while (count-- > 0)
                        sb.Append(toAdd);
                    count = 0;  // reset count for next iteration
                }
                else // if (ch=='L')
                {
                    if (lastState == 'L')
                        while (count-- > 0)
                            sb.Append('L');
                    else // if(lastState=='R')
                    {
                        times = count / 2;
                        // startomg half wud fall to Right
                        while (times-- > 0)
                            sb.Append('R');

                        // in case of odd length gap with R...L, we add 1 '.' at center
                        if (count % 2 != 0)
                            sb.Append('.');

                        times = count / 2;
                        // ending half wud fall to Left
                        while (times-- > 0)
                            sb.Append('L');
                    }

                    count = 0;  // reset count for next iteration
                }
                lastState = ch;
                sb.Append(ch);
            }

            // take care of empty states which are still to be filled at end
            while (count-- > 0)
                sb.Append(lastState == 'L' ? '.' : 'R');

            return sb.ToString();
        }


        // Time O(n) Space O(h), Recursive Soln
        public static TreeNode PruneTree(TreeNode root)
        {
            HasOne(ref root);
            return root;

            // local helper func || POST ORDER TRAVERSAL (LEFT >> RIGHT >> ROOT)
            bool HasOne(ref TreeNode r)
            {
                if (r == null)
                    return false;

                bool oneOnlt = HasOne(ref r.left);
                bool oneOnRt = HasOne(ref r.right);
                if (oneOnlt || oneOnRt || r.val == 1)
                    return true;
                else // since we didnt find one on either left or right subtree neither is root value one hence delete this node
                {
                    r = null;
                    return false;
                }
            }
        }


        // Time = O(Max(N^2*l,n*l*26)), Space = O(n), n = no of words in 'wordList' & l = length of each word
        public static IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> unqAdjacencyList = new Dictionary<string, List<string>>();

            // insert all unique words in Dictionary to later form an UnDirected Graph
            for (int i = 0; i < wordList.Count; i++)            // O(n)
                graph[wordList[i]] = new List<string>();        // add other nodes
            unqAdjacencyList[beginWord] = new List<string>();

            if (!graph.ContainsKey(beginWord))
            {
                graph[beginWord] = new List<string>();          // add source

                for (int i = 0; i < wordList.Count; i++)        // O(n)
                    // Connect all nodes which are 1 edit distance away from beginword
                    if (IsCharOneDiff(wordList[i], beginWord))  // O(l)
                        graph[beginWord].Add(wordList[i]);
            }
            // Connect all nodes which are 1 edit distance away from 'wordList'
            for (int i = 0; i < wordList.Count; i++)            // O(n)
            {
                /* Alternate way to find adjacent nodes can b instead of traversing all the nodes to see which are 1 edit distance apart
                 * we store all the words in HashSet 
                 * and than to create Graph we just check for each word
                 *      and for each char in curr word if we try replace origin character with another character (26 possible char)
                 *          if new choosen char is not same as original we replace the original character in word
                 *          and check if this new word in present in HashSet
                 *          if Yes than we make the connection
                 */
                for (int j = i + 1; j < wordList.Count; j++)    // O(n)
                    if (IsCharOneDiff(wordList[i], wordList[j]))// O(l)
                    {
                        graph[wordList[i]].Add(wordList[j]);
                        graph[wordList[j]].Add(wordList[i]);
                    }

                unqAdjacencyList[wordList[i]] = new List<string>();
            }

            List<IList<string>> result = new List<IList<string>>();
            int shortestPathLen = int.MaxValue;

            // we can stop check for shortest paths if destination is not present in graph
            if (!graph.ContainsKey(endWord)) return result;

            // create unique Adjacency List which will be used in DFS
            CreateLevelWiseAdjacencyList_BFS(beginWord, endWord);// O(n)

            // now using Unique Adjacency list created above do DFS traversal from source (level 0) till u find 'destination'
            Stack<string> path = new Stack<string>();
            GetShortestPaths_DFS(beginWord, endWord);           // O(n*l*26)
            return result;

            // local helper func
            void CreateLevelWiseAdjacencyList_BFS(string source, string destination)
            {
                Dictionary<string, int> level = new Dictionary<string, int>();          // to store hopCount
                Queue<string> q = new Queue<string>();

                level[source] = 0;
                q.Enqueue(source);

                while (q.Count > 0)
                {
                    var parent = q.Dequeue();
                    foreach (var adjacentNode in graph[parent])
                        if (!level.ContainsKey(adjacentNode))
                        {
                            level[adjacentNode] = level[parent] + 1;
                            unqAdjacencyList[parent].Add(adjacentNode);

                            if (adjacentNode == destination)
                                shortestPathLen = level[adjacentNode];
                            else // keeping adding intermediate nodes till we reach destination node
                                q.Enqueue(adjacentNode);
                        }
                        else if (level[adjacentNode] > level[parent])
                            unqAdjacencyList[parent].Add(adjacentNode);
                }
            }
            // Time O(V+E)
            void GetShortestPaths_DFS(string source, string destination, int hopsCount = 0)
            {
                if (hopsCount > shortestPathLen) return;

                path.Push(source);

                if (source == destination)
                    result.Add(path.Reverse().ToList());

                foreach (var nodeInLowerLevel in unqAdjacencyList[source])
                    GetShortestPaths_DFS(nodeInLowerLevel, destination);

                path.Pop();
            }

            // returns 'True' if both input diff by max 1 character
            bool IsCharOneDiff(string a, string b)
            {
                bool oneDiffFound = false;
                for (int i = 0; i < a.Length; i++)
                    if (a[i] != b[i])
                        if (oneDiffFound)
                            return false;
                        else
                            oneDiffFound = true;
                return true;
            }
        }


        // Time O(2^k), Auxillary Space O(1), Recursive Space O(k), k = distance of farthest ON bit in 'n' from right
        public static int FindIntegersWithoutConsecutiveOnes(int n)
        {
            int bLen = 0;
            // find the distance of farthest 1 n.e. On bit from right
            for (int i = 0; i < 32; i++)
                if ((1 << i & n) > 0)
                    bLen = i + 1;

            // we set initial count as '2' because smallest input n.e. n==1 has 0 & 1 as valid ans n.e. 2 nums
            int count = 2;
            // now we start with initial number 1
            // and add Zero & One after left shifting the num by 1 place <<1
            // we keeping repeating above process till we have reached the distance of fartest 1 bit in number 'n'
            // also when no of binary number length matches that of input 'n'
            // we only increament counter if current number is <= input 'n'
            CountNumsSmallerThanN();
            return count;


            // local helper func
            void CountNumsSmallerThanN(int currNum = 1, int len = 1, bool lastBitOn = true)
            {
                if (len >= bLen) return;
                currNum <<= 1;    // left shift 1 place

                // after adding OFF bit on right see if num is still smaller than 'n'
                if (currNum <= n)
                {
                    count++;
                    CountNumsSmallerThanN(currNum, len + 1, false);
                }
                // before adding ON bit on right we check if last added bit was not ON
                // & check currNum doesnt exceed 'n'
                if (!lastBitOn && ++currNum <= n)
                {
                    count++;
                    CountNumsSmallerThanN(currNum, len + 1, true);
                }
            }
        }


        // Time = Space = O(n), n = length of 'nums'
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            return GetBST(0, nums.Length - 1);

            // local helper func
            TreeNode GetBST(int lt, int rt)
            {
                if (lt > rt) return null;
                int mid = lt + (rt - lt) / 2;
                return new TreeNode(nums[mid])
                {
                    left = GetBST(lt, mid - 1),
                    right = GetBST(mid + 1, rt)
                };
            }
        }


        // Time O(n^2) Space O(1), n = length of 'target'
        public static int ThreeSumClosest(int[] A, int target)
        {
            Array.Sort(A);
            int l = A.Length, closet = A[0] + A[1] + A[l - 1], lt, rt, sum;
            for (int i = 0; i < l - 2; i++)
            {
                if (i > 0 && A[i] == A[i - 1]) continue;    // optimization
                lt = i + 1;
                rt = l - 1;
                while (lt < rt)
                {
                    if (lt > i + 1 && A[lt] == A[lt - 1])   // optimization
                    {
                        lt++;
                        continue;
                    }
                    sum = A[i] + A[lt] + A[rt];
                    if (Math.Abs(sum - target) < Math.Abs(closet - target))
                        closet = sum;
                    if (sum > target)
                        rt--;
                    else if (sum < target)
                        lt++;
                    else
                        break;
                }
            }
            return (int)closet;
        }


        // Time = Space = O(nlogn)
        public static int[] BeautifulArray(int n)
        {
            int[] temp, beautifulArr = { 1 };     // default state, n=1
            int size = 1, idx, i, val;
            while (size++ < n)
            {
                temp = new int[size];
                idx = 0;
                // add odd values as per formula of 2*num-1
                for (i = 0; i < beautifulArr.Length; i++)
                {
                    val = 2 * beautifulArr[i] - 1;
                    if (val <= size)
                        temp[idx++] = val;
                }
                // now add even values as per formula of 2*num
                for (i = 0; i < beautifulArr.Length; i++)
                {
                    val = 2 * beautifulArr[i];
                    if (val <= size)
                        temp[idx++] = val;
                }
                // update beautiful array
                beautifulArr = temp;
            }
            return beautifulArr;
        }
        // Time = Space = O(nlogn)
        public static int[] BeautifulArray_Cache(int n, Dictionary<int, int[]> cache, ref int biggestBeautifulSeen)
        {
            if (cache.ContainsKey(n))
                return cache[n];

            int size = biggestBeautifulSeen, idx, i, val;
            int[] temp, beautifulArr = cache[size];     // default state, n=1
            while (size++ < n)
            {
                temp = new int[size];
                idx = 0;
                // add odd values as per formula of 2*num-1
                for (i = 0; i < beautifulArr.Length; i++)
                {
                    val = 2 * beautifulArr[i] - 1;
                    if (val <= size)
                        temp[idx++] = val;
                }
                // now add even values as per formula of 2*num
                for (i = 0; i < beautifulArr.Length; i++)
                {
                    val = 2 * beautifulArr[i];
                    if (val <= size)
                        temp[idx++] = val;
                }
                // update beautiful array
                beautifulArr = temp;
                // add to cache
                cache[size] = beautifulArr;
            }
            biggestBeautifulSeen = Math.Max(biggestBeautifulSeen, n);
            return beautifulArr;
        }


        // Time = Space = O(row*col)
        public static int[][] ZeroOneMatrix(int[][] mat)
        {
            /* Find all the position of ones in matrix and add these pos to Queue with Value 0
             * all non zero are marked as Not visited/Calculatedd
             * now start dequeing queue
             *      pick the current node & add its adjacent 4 nodes (lt,rt,top,bottom) which are not already visited to queue 
             *      and also update value for current cell as parentNode + 1
             *      at end this way we will have nearest Zero value for all nodes
             */
            int row = mat.Length, col = mat[0].Length;
            Queue<int[]> q = new Queue<int[]>();

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    if (mat[r][c] == 0)
                        // add current row,col,nearestZeroDist
                        q.Enqueue(new int[] { r, c });
                    else
                        mat[r][c] = -1;     // Closet Zero not calculated

            int closetZero = 0, size;
            while (q.Count > 0)             // BFS
            {
                size = q.Count;
                closetZero++;   // we increament distance by 1
                while (size-- > 0)
                {
                    var temp = q.Dequeue();
                    int r = temp[0], c = temp[1];
                    if (r > 0 && mat[r - 1][c] == -1)                // top
                    {
                        mat[r - 1][c] = closetZero;
                        q.Enqueue(new int[] { r - 1, c });
                    }
                    if (r + 1 < row && mat[r + 1][c] == -1)          // bottom
                    {
                        mat[r + 1][c] = closetZero;
                        q.Enqueue(new int[] { r + 1, c });
                    }
                    if (c > 0 && mat[r][c - 1] == -1)                // left
                    {
                        mat[r][c - 1] = closetZero;
                        q.Enqueue(new int[] { r, c - 1 });
                    }
                    if (c + 1 < col && mat[r][c + 1] == -1)          // right
                    {
                        mat[r][c + 1] = closetZero;
                        q.Enqueue(new int[] { r, c + 1 });
                    }
                }
            }
            return mat;
        }


        // Time = Space = O(n^2), n = length of square grid
        public static int MakingALargeIsland(int[][] grid)
        {
            /* Get the size of all island (using DFS) and assign them a unique ID
             *      store each individual island size in Dictionary with key islandID & value as its size
             * 
             * now traverse all zero's in grid put all unique (max four) island directly connected to it
             *      and add their sum up + 1 to get the max island possible size
             */
            int n = grid.Length, maxIsland = 0, islandID = 100;
            Dictionary<int, int> unqIslandSize = new Dictionary<int, int>();

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (grid[r][c] == 1)    // new island found
                        maxIsland = Math.Max(maxIsland, unqIslandSize[++islandID] = DFS(r, c, islandID));

            unqIslandSize[0] = 0;   // default value of island with ID 0 is 0
            HashSet<int> uniqueConnectedIslands = new HashSet<int>();
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (grid[r][c] == 0)    // possible connection cell connecting 2 or more(max 4) island found
                    {
                        if (r > 0) uniqueConnectedIslands.Add(grid[r - 1][c]);
                        if (r + 1 < n) uniqueConnectedIslands.Add(grid[r + 1][c]);
                        if (c > 0) uniqueConnectedIslands.Add(grid[r][c - 1]);
                        if (c + 1 < n) uniqueConnectedIslands.Add(grid[r][c + 1]);

                        int connectedSize = 1;  // value is 1 as we are converting cell r,c zero to One
                        foreach (var island in uniqueConnectedIslands)
                            connectedSize += unqIslandSize[island];

                        maxIsland = Math.Max(maxIsland, connectedSize);
                        uniqueConnectedIslands.Clear();     // clean Hashset for next iteration
                    }

            return maxIsland;

            // local helper DFS func to get the largest island size
            int DFS(int i, int j, int ID)
            {
                if (i < 0 || i >= n || j < 0 || j >= n || grid[i][j] != 1) return 0;
                grid[i][j] = ID;        // mark visited with unique island ID
                // 1 + bottom + top + right + left
                return 1 + DFS(i + 1, j, ID) + DFS(i - 1, j, ID) + DFS(i, j + 1, ID) + DFS(i, j - 1, ID);
            }
        }


        // Time O(n!) Space O(n), n = length of 'nums'
        public static IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            Array.Sort(nums);       // O(nlogn)
            List<IList<int>> ans = new List<IList<int>>();
            BackTrack(0, new Stack<int>());
            return ans;
            // local helper func
            void BackTrack(int idx, Stack<int> curSet)
            {
                ans.Add(curSet.Reverse().ToArray());
                for (int i = idx; i < nums.Length; i++)
                    if (i == idx || nums[i - 1] != nums[i])
                    {
                        curSet.Push(nums[i]);
                        BackTrack(i + 1, curSet);
                        curSet.Pop();
                    }
            }
        }


        public static bool CheckMove(char[][] board, int rMove, int cMove, char color)
        {
            int[][] direction = new int[][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };
            return DFS(rMove, cMove);

            // local helper func
            bool DFS(int r, int c)
            {
                return CheckInOneDirection(r - 1, c, 1, 0) || CheckInOneDirection(r + 1, c, 1, 1) || CheckInOneDirection(r, c - 1, 1, 2) || CheckInOneDirection(r, c + 1, 1, 3);
            }

            bool CheckInOneDirection(int r, int c, int len, int dir)
            {
                if (r < 0 || r == 8 || c < 0 || c == 8 || board[r][c] == '.') return false;
                ++len;  // update length

                if (board[r][c] == color)
                {
                    if (len >= 3)
                        return true;
                }
                else // opp color
                {
                    r += direction[dir][0];
                    c += direction[dir][1];
                    return CheckInOneDirection(r, c, len, dir);
                }

                return false;
            }
        }


        // Time = O(r*c*log(r*c)) || Space O(r*c), r = no of rows & c = no of cols in 'Matrix'
        public static int[][] MatrixRankTransform(int[][] matrix)
        {
            /* Deriving from Rank Transformation for 1-D array,
             * we will save all the nums in Dictionary 'numsPos' as key with value as list of positions of each such number in matrix
             * 
             * Set 'rank' variable as 1
             * Also create a array to maxRank to store the max rank for each row & also for cols (default curr max rank is 0)
             * Now we sort 'numsPos' with key & read numbers from the smallest to largest value
             * 
             * for each curr number we check the maxRank for current numbers row & col and +1 to the current max and set that as rank
             * we have to also check maxRank if multiple numbers with same value exists in current numbers row or col
             * 
             * we also keep updating the maxRank in rows & cols after each rank update
             */
            Dictionary<int, List<int>> numsPos = new Dictionary<int, List<int>>();
            int rows = matrix.Length, cols = matrix[0].Length, currMax, r, c;
            for (r = 0; r < rows; r++)
                for (c = 0; c < cols; c++)
                    if (!numsPos.ContainsKey(matrix[r][c]))                 // new number
                        numsPos[matrix[r][c]] = new List<int>() { r * 1000 + c };
                    else                                                    // num already present, add new position
                        numsPos[matrix[r][c]].Add(r * 1000 + c);

            int[] maxRankRow = new int[rows], maxRankCol = new int[cols];   // default rank fr all rows & col is 0

            foreach (var currNumPositions in numsPos.OrderBy(x => x.Key))   // O(rows*cols*log(rows*cols)) if we have all distinct nums in matrix
                foreach (var currGrp in GetSameNumberGrps(currNumPositions.Value))
                {
                    currMax = 0;        // set default rank

                    // find the minimum viable rank for curr group by looking at maxRank in all rows and cols where curr number is present
                    foreach (var pos in currGrp)
                        currMax = Math.Max(currMax, Math.Max(maxRankRow[pos / 1000], maxRankCol[pos % 1000]));

                    currMax++;          // add +1 to get current rank

                    foreach (var pos in currGrp)
                    {
                        r = pos / 1000;
                        c = pos % 1000;
                        // set rank & also update maximum Rank each row & col where curr Number was present
                        maxRankRow[r] = maxRankCol[c] = matrix[r][c] = currMax;
                    }
                }

            return matrix;

            // local helper func
            List<IList<int>> GetSameNumberGrps(List<int> positions)
            {
                List<IList<int>> grps = new List<IList<int>>();
                HashSet<int> visited = new HashSet<int>();
                Queue<int> q = new Queue<int>();
                List<int> grp = new List<int>();
                int curR, curC;
                foreach (var pos in positions)
                    if (!visited.Contains(pos))
                    {
                        q.Enqueue(pos);
                        visited.Add(pos);

                        while (q.Count > 0)
                        {
                            var currPos = q.Dequeue();
                            grp.Add(currPos);
                            curR = currPos / 1000;
                            curC = currPos % 1000;
                            // all nums which are in same row or same col are added to curr group
                            foreach (var uniqPos in positions)
                                if (!visited.Contains(uniqPos))
                                    if (curR == uniqPos / 1000 || curC == uniqPos % 1000)    // same row or col
                                    {
                                        q.Enqueue(uniqPos);             // add to Queue
                                        visited.Add(uniqPos);           // mark visited
                                    }
                        }

                        grps.Add(grp);

                        // reset fr next grp
                        grp = new List<int>();
                        q.Clear();
                    }
                return grps;
            }
        }


        // Time O(nlogn+m) Space O(n), n = no of unique elements in 'arr' & m = total nums in 'arr'
        public static bool CanReorderDoubled(int[] arr)
        {
            Dictionary<int, int> numFreq = new Dictionary<int, int>();
            for (int i = 0; i < arr.Length; i++)
                if (!numFreq.ContainsKey(arr[i]))
                    numFreq[arr[i]] = 1;    // add new num with freq = 1
                else
                    numFreq[arr[i]]++;      // increase the frequency

            var uniqSortNums = from kvp in numFreq
                               orderby kvp.Key
                               select kvp.Key;

            // reading uniq numbers in sorted order (asc)
            foreach (var num in uniqSortNums)
                if (numFreq[num] > 0)       // freq is 1 or more, check if there exists a num which is * 2 in Dict
                    if (num < 0)    // for -ve nums
                    {
                        // if -ve odd num found
                        if (num % 2 != 0) return false;

                        var half = num / 2;
                        if (!numFreq.ContainsKey(half) || numFreq[half] < numFreq[num])
                            return false;
                        else
                            numFreq[half] -= numFreq[num];  // decrease the frequency of * 2 number in Dictionary for current num
                    }
                    else //(num>0) for +ve nums
                    {
                        var twice = num * 2;
                        if (!numFreq.ContainsKey(twice) || numFreq[twice] < numFreq[num])
                            return false;
                        else
                            numFreq[twice] -= numFreq[num];  // decrease the frequency of * 2 number in Dictionary for current num
                    }

            // if all pair found & present
            return true;
        }


        // Time = Space = O(1)
        public static string ComplexNumberMultiply(string num1, string num2)
        {
            var n1 = num1.Split('+');
            var n2 = num2.Split('+');
            int real1 = int.Parse(n1[0]), real2 = int.Parse(n2[0]), imag1 = int.Parse((n1[1].Split('i'))[0]), imag2 = int.Parse((n2[1].Split('i'))[0]);
            Console.WriteLine($" {real1},{imag1},{real2},{imag2}");
            int r1r2 = real1 * real2, r1i2 = real1 * imag2, i1r2 = imag1 * real2, i1i2 = imag1 * imag2;

            return (r1r2 - i1i2) + "+" + (r1i2 + i1r2) + "n";
        }



        // Time = Space = O(n), n = no of nodes in Tree || Recursive Soln
        public static bool IsValidSerialization(string preorder)
        {
            var tree = preorder.Split(',');
            int i = 0, nodesCount = tree.Length;
            return Check() && i == nodesCount;

            // local helper func
            bool Check()
            {
                if (i >= nodesCount) return false;
                // curr node is null, return true
                if (tree[i++] == "#") return true;
                // else check if it has both valid left & right subtree
                return Check() && Check();
            }
        }
        // Time = O(n) ||  Space = O(1), n = length of 'preorder' || Iterative Soln
        public static bool IsValidSerialization_Iterative(string preorder)
        {
            int count = 1;
            for (int i = 0; i < preorder.Length; i++)
                if (preorder[i] != ',')
                    if (preorder[i] == '#')
                    {
                        // if count goes below 1 before we reach the end no need to check any further, return false
                        if (--count <= 0 && i < preorder.Length - 1)
                            return false;
                    }
                    else if (i == 0 || preorder[i - 1] == ',')
                        count++;
            return count == 0;
        }


        // Larry https://youtu.be/HY1uys6hS_Y
        // Time O(Sqrt(c)) Space O(1)
        public static bool JudgeSquareSum(int c)
        {
            long a = 0;
            while (2 * a * a <= c)
            {
                int b = (int)(Math.Sqrt(c - (a * a)));
                if ((b * b) + (a * a) == c) return true;
                a++;
            }
            return false;
        }


        // Time O(n) Space O(1), n = length of 'ops'
        public static int RangeAdditionII(int rBoundry, int cBoundry, int[][] ops)
        {
            foreach (var op in ops)  // O(n)
            {
                rBoundry = Math.Min(rBoundry, op[0]);
                cBoundry = Math.Min(cBoundry, op[1]);
            }
            return rBoundry * cBoundry;
        }


        // Time = O(n) Space O(1), n = len of 'nums'
        public static int ArrayNesting(int[] nums)
        {
            int max = 1, curMax, val, nVal;
            for (int i = 0; i < nums.Length; i++)
            {
                //curMax = -1;
                //val = n;
                //while (val >= 0)
                //{
                //    nVal = nums[val];
                //    nums[val] = -1;
                //    if (val == nVal) break;
                //    val = nVal;
                //    curMax++;
                //}
                //max = Math.Max(max, curMax);
                //if (max > nums.Length / 2) break;       // Optimization
                curMax = 0;
                val = i;
                while (nums[val] >= 0)
                {
                    nVal = nums[val];
                    nums[val] = -1;               // mark Visited
                    val = nVal;
                    curMax++;
                }
                if ((max = Math.Max(max, curMax)) > nums.Length / 2)  // Optimization
                    break;
            }
            return max;
        }


        // Coding Decoded https://youtu.be/mhCJgZWJlSI
        public static IList<TreeNode> GenerateTrees(int n) => GetBST(1, n);
        public static IList<TreeNode> GetBST(int start, int last)
        {
            var combinations = new List<TreeNode>();
            if (start > last)
                combinations.Add(null);
            else if (start == last)
                combinations.Add(new TreeNode(start));
            else// if(start<last)
                for (int r = start; r <= last; r++)
                    foreach (var ltBST in GetBST(start, r - 1))
                        foreach (var rtBST in GetBST(r + 1, last))
                            combinations.Add(new TreeNode(r) { left = ltBST, right = rtBST });
            return combinations;
        }


        // Time = O(m+n) Space = O(n), n = no of Verticies, m = length of 'edges'
        public static int ReachableNodes(int[][] edges, int maxMoves, int n)
        {
            // create the graph
            Dictionary<int, int>[] g = new Dictionary<int, int>[n];
            for (int i = 0; i < n; i++) g[i] = new Dictionary<int, int>();

            int u, v, cnt;
            foreach (var edge in edges)      // O(m)
            {
                u = edge[0];
                v = edge[1];
                cnt = edge[2];
                // u->v
                g[u][v] = cnt;
                // v->u
                g[v][u] = cnt;
            }

            int reachableNodes = 0;
            bool[] visited = new bool[n];
            Get(0, maxMoves);                // O(Min(n,maxMoves))

            return reachableNodes;
            // local helper func, DFS tarversal
            void Get(int currNode, int moveLeft)
            {
                if (moveLeft < 0 || visited[currNode]) return;  // no more moves left or already visited node
                reachableNodes++;           // since current node is reachable
                visited[currNode] = true;   // mark visited

                List<int> connectedNodes = g[currNode].Keys.ToList();   // creating another collection as editing Dictionary while iterating is not allowed
                foreach (var adjNode in connectedNodes)
                {
                    // get count of no of nodes reachable b/w curr and adjacent nodes excluding both
                    reachableNodes += Math.Min(moveLeft, g[currNode][adjNode]);

                    // remove same amt of nodes from adjacent to avoid counting same in b/w nodes twice
                    g[adjNode][currNode] -= Math.Min(moveLeft, g[currNode][adjNode]);

                    // only continue DFS further if adjacent is not visited yet
                    if (visited[adjNode]) continue;

                    // further check nodes reachable from Adjacent Node
                    Get(adjNode, -1 + moveLeft - g[currNode][adjNode]);
                }
            }
        }


        // Time O(n) Space O(1), n = len of 'text'
        public static int MaxNumberOfBalloons(string text)
        {
            int[] charSet = new int[26];
            for (int i = 0; i < text.Length; i++)
                charSet[text[i] - 'a']++;

            // B A L-2 O-2 N, we take the min of all characters required to make 1 'ballon'
            return Math.Min(charSet['b' - 'a'], Math.Min(charSet['a' - 'a'], Math.Min(charSet['l' - 'a'] / 2, Math.Min(charSet['o' - 'a'] / 2, charSet['n' - 'a']))));
        }


        // Time O(n) Space O(1), n = length of 'A'
        public static int MaxTurbulenceSize(int[] A)
        {
            int maxTurbulent = 1, i = -1, currMax = 1;
            bool lookingForGreater = true;
            while (++i < A.Length - 1)
            {
                if (lookingForGreater)
                {
                    if (A[i] > A[i + 1])
                    {
                        currMax++;
                        lookingForGreater = !lookingForGreater;
                    }
                    else
                        currMax = A[i] != A[i + 1] ? 2 : 1;
                }
                else // !lookingForGreater
                {
                    if (A[i] < A[i + 1])
                    {
                        currMax++;
                        lookingForGreater = !lookingForGreater;
                    }
                    else
                        currMax = A[i] != A[i + 1] ? 2 : 1;
                }
                maxTurbulent = Math.Max(maxTurbulent, currMax);
            }
            return maxTurbulent;
        }


        // Time = Space = O(m*n*k)
        public static int ShortestPathWithObstaclesElimination(int[][] grid, int k)
        {
            Queue<int[]> q = new Queue<int[]>();
            int m = grid.Length, n = grid[0].Length;
            bool[,,] seen = new bool[m, n, k + 1];
            int shortestRoute = 0, r, c, kLeft, obstacleCost, currR, currC;
            seen[0, 0, k] = true;
            q.Enqueue(new int[] { 0, 0, k });
            int[][] direction = { new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 } };
            while (q.Count > 0)
            {
                int curQCount = q.Count;
                while (--curQCount >= 0)
                {
                    var cur = q.Dequeue();
                    r = cur[0]; c = cur[1]; kLeft = cur[2];

                    // reached bottom-rt destination
                    if (r == m - 1 && c == n - 1)
                        return shortestRoute;

                    foreach (var d in direction)
                    {
                        currR = r + d[0];
                        currC = c + d[1];
                        // if current cell is valid coordinate
                        if (0 <= currR && currR < m && 0 <= currC && currC < n)
                        {
                            // curr cell is obstacle
                            obstacleCost = grid[currR][currC] == 1 ? 1 : 0;

                            // if curr cell is not seen with curr state
                            if (kLeft - obstacleCost >= 0 && !seen[currR, currC, kLeft - obstacleCost])
                            {
                                q.Enqueue(new int[] { currR, currC, kLeft - obstacleCost });
                                seen[currR, currC, kLeft - obstacleCost] = true;
                            }
                        }
                    }
                }
                shortestRoute++;
            }

            return -1;  // no path found
        }



        // Time O(n) Space O(k) Soln, n = length of LinkedList
        public static ListNode[] SplitListToParts(ListNode head, int k)
        {
            ListNode[] ans = new ListNode[k];

            // split into 'k' parts
            // If there are N nodes in the list, and k parts,
            // then every part has N/k elements, except the first N%k parts have an extra one.
            int len = GetLength(head), eachPartLen = len / k, firstModK = len % k, elementsFilledSoFar = 0, idx = 0;
            while (head != null)
            {
                if (elementsFilledSoFar == 0)   // start filling new part
                    ans[idx++] = head;

                var noOfElementsToBeFilled = eachPartLen + (idx <= firstModK ? 1 : 0);
                if (++elementsFilledSoFar == noOfElementsToBeFilled)
                {
                    elementsFilledSoFar = 0;    // reset counter
                    var next = head.next;
                    head.next = null;           // mark the end of current list
                    head = next;
                }
                else
                    head = head.next;
            }
            return ans;

            // local helper func
            int GetLength(ListNode h)
            {
                int l = 0;
                while (h != null) { l++; h = h.next; }
                return l;
            }
        }

        // Time O(n) Space O(n), n = no of nodes in the linked list
        public static int[] NextLargerNodes(ListNode head)
        {
            int len = 0, idx = 0;
            ListNode h = head;
            while (h != null)
            {
                len++;
                h = h.next;
            }
            // Console.WriteLine($"Length {len}");

            int[] ans = new int[len];
            Stack<MyNode> st = new Stack<MyNode>();
            while (head != null)
            {
                // if no element in stack just push current element and along with its index
                if (st.Count == 0)
                    st.Push(new MyNode(head.val, idx));
                else
                {
                    if (st.Peek().val > head.val)
                        st.Push(new MyNode(head.val, idx));
                    else
                    {
                        // while stack is not empty and stack top is smaller than current node values
                        while (st.Count > 0 && st.Peek().val < head.val)
                        {
                            var n = st.Pop();
                            while (n.idx < idx && ans[n.idx] == 0)
                                ans[n.idx++] = head.val;
                        }
                        // once we have updated max fr all prv values smaller than current node values, push current node value & index
                        st.Push(new MyNode(head.val, idx));
                    }
                }
                // Console.WriteLine($"{idx} value {head.val}");
                head = head.next;
                idx++;
            }
            return ans;
        }

        // Time O(n) Space O(n), n = no of nodes in the linked list
        public static int[] NextLargerNodes_Faster(ListNode head)
        {
            int len = 0, idx = 0;
            ListNode h = head;
            while (h != null)
            {
                len++;
                h = h.next;
            }

            int[] ans = new int[len];
            Stack<MyNode> st = new Stack<MyNode>();
            while (head != null)
            {
                if (st.Count > 0 && st.Peek().val < head.val)
                    // while stack is not empty and stack top is smaller than current node values
                    while (st.Count > 0 && st.Peek().val < head.val)
                    {
                        var n = st.Pop();
                        while (n.idx < idx && ans[n.idx] == 0)
                            ans[n.idx++] = head.val;
                    }

                // push current node value &index
                st.Push(new MyNode(head.val, idx++));
                head = head.next;
            }
            return ans;
        }


        // Time = O(n) Space = O(1)
        public static int FindNonMinOrMax(int[] nums)
        {
            if (nums.Length < 3) return -1;    // we need to have min 3 unique numbers fr valid ans
            int min = nums[0], max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (min != max && nums[i] != min && nums[i] != max)
                {
                    if (min < nums[i] && nums[i] < max) return nums[i];
                    else if (nums[i] < min) return min;
                    else return max;
                    // var unique = new List<int>() {min,max,nums[n]};
                    // unique.Sort(); // sort 3 unique numbers we have
                    // return unique[1]; // return the middle one
                }
                if (nums[i] < min) min = nums[i];
                if (nums[i] > max) max = nums[i];
            }
            return -1;
        }

        // Time = Space = O(1)
        public static int FindNonMinOrMax_Faster(int[] nums)
        {
            if (nums.Length < 3) return -1;    // we need to have min 3 unique numbers fr valid ans
            // since we have onyl distinct integers we can check 1st three numbers to get valid ans
            int min = Math.Min(nums[0], Math.Min(nums[1], nums[2]));
            int max = Math.Max(nums[0], Math.Max(nums[1], nums[2]));
            return (min < nums[0] && nums[0] < max) ? nums[0] : (min < nums[1] && nums[1] < max) ? nums[1] : nums[2];
        }

        // Time O(Max(s,r*n)) Space O(r*n)
        public static IList<string> FindAllRecipes(string[] recipes, List<List<string>> ingredients, string[] supplies)
        {
            /* Algo/Approach
            Create a graph with each element in supplies order as 0
            Now traverse the reciepe add it to graph if not already present set the order as no of ingredients required to make it
                now traverse thru each ingredients:
                    if not already present in graph
                        add new node for the ingridents & the current recipe in the list of linked Nodes to each ingredient
                    if already present in graph
                        just add the current recipe to the list of linked Nodes
            Now create a queue prefilled with the supplies
                and for each node decrease the order of all linked Nodes by -1
                if any linked Node reaches order 0 than add it to the end of the queue
            At the end iterate thru the recipes and check if node with the matching name has order 0 than add recipe to the ans
            */
            Dictionary<string, int> inDegree = new Dictionary<string, int>();
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            foreach (var sup in supplies)                           // O(s), s = length of supplies
            {
                inDegree[sup] = 0;
                graph[sup] = new List<string>();
            }
            for (int i = 0; i < recipes.Length; i++)                // O(r), r = no of recipes
            {
                var recipe = recipes[i];
                var recipe_ingredient = ingredients[i];

                if (!graph.ContainsKey(recipe))
                    graph[recipe] = new List<string>();
                inDegree[recipe] = recipe_ingredient.Count;

                foreach (var ingre in recipe_ingredient)            // O(n), avg no of ingredients/recipe
                {
                    if (!graph.ContainsKey(ingre))
                    {
                        graph[ingre] = new List<string>();
                        graph[ingre].Add(recipe);
                        inDegree[ingre] = int.MaxValue;
                    }
                    else
                    {
                        graph[ingre].Add(recipe);   // add new recipe which depends on current ingredient
                    }
                }
            }

            Queue<string> canBeMade = new Queue<string>(supplies);  // O(s), s = length of supplies
            while (canBeMade.Count > 0)
            {
                var hasBeenMade = canBeMade.Dequeue();
                foreach (var dependent in graph[hasBeenMade])
                {
                    if (--inDegree[dependent] == 0)
                        canBeMade.Enqueue(dependent);
                }
            }

            List<string> ans = new List<string>();
            foreach (var recipe in recipes)                         // O(r), s = no of recipes
                if (inDegree[recipe] == 0)
                    ans.Add(recipe);
            return ans;
        }

        // Ref https://leetcode.com/problems/find-all-people-with-secret/solutions/1600202/java-hashmap-and-queue/
        // Time O(n^2) Space O(Max(m,n)), n = length of meetings
        public static IList<int> FindAllPeopleUsingQueue(int n, int[][] meetings, int firstPerson)
        {
            /*  Create a graph with Key a person and Value as list of meetings they had with others in the format {meetingWith, meetTime}

                Initialize 1st time ppl knew secret for each person as int.MaxValue and update it to 0 for firstPerson which got to know the secret from person at index o (lets all this array 'SecretKnowTime')

                Now Initialize the queue with the people who know the secret at time t0 n.e. 0th and firstPerson

                Now while Dequeuing the person p1 from the Q
                go thru all meetings done by p1 (if any)

                while going thru meetings check if it was held at time n.e. after they knew secret than skip to next meeting
                also skip if the p2 who is meeting p1 knew the secret from schedule meeting time.

                we only add p2 to queue if current meeting is when they got to know the secret for the very first time.
                also reduce/update the time in SecretKnowTime array for p2.
             */

            int[] SecretKnowTime = Enumerable.Repeat(int.MaxValue, n).ToArray();
            SecretKnowTime[0] = SecretKnowTime[firstPerson] = 0;    // set time when first 2 person know the secret
            Dictionary<int, List<int[]>> meetSchedule = new Dictionary<int, List<int[]>>();
            foreach (var meet in meetings)                           // O(m), m = no of meetings
            {
                var p1 = meet[0];
                var p2 = meet[1];
                var time = meet[2];
                // update Graph for P1
                if (!meetSchedule.ContainsKey(p1))
                    meetSchedule[p1] = new List<int[]>() { new int[] { p2, time } };
                else
                    meetSchedule[p1].Add(new int[] { p2, time });
                // update Graph for P2
                if (!meetSchedule.ContainsKey(p2))
                    meetSchedule[p2] = new List<int[]>() { new int[] { p1, time } };
                else
                    meetSchedule[p2].Add(new int[] { p1, time });
            }

            Queue<int> q = new();
            q.Enqueue(0);
            q.Enqueue(firstPerson);
            while (q.TryDequeue(out int person1))                                     // O(n), all person know the secret
                if (meetSchedule.TryGetValue(person1, out List<int[]> meetingsP1Had))
                    foreach (var person2 in meetingsP1Had)      // O(n)
                        // P1 meeting with P2 happened before P1 knew Secret or P2 knew secret before his meeting with P1
                        if (SecretKnowTime[person1] > person2[1] || SecretKnowTime[person2[0]] <= person2[1]) continue;
                        else // secret was passed for 1st time/earliest time to p2 from p1 at time 't'
                        {
                            q.Enqueue(person2[0]);
                            SecretKnowTime[person2[0]] = person2[1];  // update the secret known time for P2 to earlier duration
                        }

            List<int> knowSecret = new();
            for (int i = 0; i < n; i++)                        // O(n)
                if (SecretKnowTime[i] != int.MaxValue)
                    knowSecret.Add(i);
            return knowSecret.ToList();

        }

        // SLOWER (but passes all Tests)
        // Time O(Max(mlogm,m*n)) | Space O(n*m), m = length of 'meetings'
        public static IList<int> FindAllPeopleUsingDFS(int n, int[][] meetings, int firstPerson)
        {
            /* ALGO
            Sort the meetings by time
            Create HashSet 'knows' to store the id of people who know the secret
            Iterate thru first time x given in the meeting Create a undirected graph of all people involved in meeting connected as per their meeting
            perform start DFS from each person who knows the secret and mark all ppl they met as visited and knows secret
            At the end convert the 'knows' set to list, sort and return
            */
            Dictionary<int, List<int>> g = new();
            bool[] knows = new bool[n];
            bool[] visited = new bool[n];
            knows[0] = knows[firstPerson] = true;

            // sort the meeting as per time (asc)
            Array.Sort(meetings, (a, b) => a[2].CompareTo(b[2]));         // O(mlogm)
            int p1, p2, curTime = meetings[0][2];
            for (int i = 0; i < meetings.Length; i++)                          // O(m)
            {
                // this cur meeting is part of new grp/time
                if (curTime != meetings[i][2])
                {
                    // evaluate how many new ppl got to know the secret
                    foreach (var person in g.Keys)
                        if (knows[person])
                            DFS(person);                                // O(n)

                    // create a new graph
                    g = new();
                    // reset visited
                    for (int j = 0; j < n; j++) visited[j] = false;
                    // update the curTime
                    curTime = meetings[i][2];
                }

                // Ooptimization: can be added is everyone knows the secret at time 'X" stop iterating thru rest of the meetings

                // add meeting to the undirect graph
                p1 = meetings[i][0];
                p2 = meetings[i][1];
                // add p1 meeting
                if (g.TryGetValue(p1, out List<int> p1Meetings)) p1Meetings.Add(p2);
                else g[p1] = new List<int>() { p2 };
                // add p2 meeting
                if (g.TryGetValue(p2, out List<int> p2Meetings)) p2Meetings.Add(p1);
                else g[p2] = new List<int>() { p1 };
            }
            // run the DFS for the last meeting
            // evaluate how many new ppl got to know the secret
            foreach (var person in g.Keys)
                if (knows[person])
                    DFS(person);                                     // O(n)

            IList<int> result = new List<int>();
            for (int i = 0; i < n; i++)
                if (knows[i])
                    result.Add(i);
            return result;

            // local helper func
            void DFS(int personWhoKnows)
            {
                // already visited return
                if (visited[personWhoKnows]) return;
                // mark visited
                visited[personWhoKnows] = true;
                // add to knows set
                knows[personWhoKnows] = true;
                // spread secret in all meetings
                if (g.TryGetValue(personWhoKnows, out List<int> meetingWithPpl))
                    foreach (var personIMet in meetingWithPpl)
                        DFS(personIMet);
            }
        }
        // Time O(nlogn) | Space O(n)
        public static void RecoverTree(TreeNode root)
        {
            List<int> ls = new List<int>();
            int idx = 0;

            InOrder(root);      // first take the inorder of the BST
            ls.Sort();          // sort the elements to make the BST valid, O(nlogn)
            FixTree(root);      // now update the BST as per the sorted list without changing the structure

            // inline helper func
            void FixTree(TreeNode r)
            {
                if (r == null) return;
                FixTree(r.left);
                r.val = ls[idx++];
                FixTree(r.right);
            }
            void InOrder(TreeNode r)
            {
                if (r == null) return;
                InOrder(r.left);
                ls.Add(r.val);
                InOrder(r.right);
            }
        }

        // Time = Space O(n)
        public static void RecoverTree_Faster_Recursive(TreeNode root)
        {
            TreeNode first = null, second = null, prv = null;
            // find the 2 out of order Nodes which have been swapped while doing InOrder traversal
            InOrder(root);      // O(n)

            // Swap the 2 out of order nodes with each other
            int t = first.val;
            first.val = second.val;
            second.val = t;

            // Inline helper func
            void InOrder(TreeNode r)
            {
                if (r == null) return;
                InOrder(r.left);
                if (prv != null && prv.val > r.val)
                {
                    if (first == null) first = prv;     // 1st bad node n.e. prv is stored in 'first'
                    second = r;                         // 2nd bad node n.e. root is stored in 'second'
                }
                prv = r;
                InOrder(r.right);
            }
        }

        // Time O(n) | Space O(1)
        public static void RecoverTree_Faster_Iterative(TreeNode root)
        {
            TreeNode first = null, second = null, prv = null, curr;
            // find the 2 out of order Nodes which have been swapped while doing InOrder traversal
            curr = root;
            Stack<TreeNode> st = new Stack<TreeNode>();
            while (true)
            {
                while (curr != null)
                {
                    st.Push(curr);
                    curr = curr.left;
                }
                if (st.Count == 0) break;
                curr = st.Pop();
                if (prv != null && prv.val > curr.val)
                {
                    second = curr;                      // 2nd bad node n.e. root is stored in 'second'
                    if (first == null) first = prv;     // 1st bad node n.e. prv is stored in 'first'
                    else break;                         // 2nd instance nodes r not in order, we got both the nodes no need to iterate further
                }
                prv = curr;
                curr = curr.right;
            }

            // Swap the 2 out of order nodes with each other
            (second.val, first.val) = (first.val, second.val);
        }

        // Time = Space = O(n), n = no of nodes
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            TreeNode merged = null;
            Merge(root1, root2, ref merged);
            return merged;
            // inline helper func
            void Merge(TreeNode r1, TreeNode r2, ref TreeNode m) // Time O(n)
            {
                if (r1 == null) m = r2;
                else if (r2 == null) m = r1;
                else
                {
                    m = new TreeNode(r1.val + r2.val);
                    Merge(r1.left, r2.left, ref m.left);
                    Merge(r1.right, r2.right, ref m.right);
                }
            }
        }

        // Time = Space = O(n), n = no of nodes
        public static TreeNode MergeTrees_Efficient(TreeNode r1, TreeNode r2)
        {
            if (r1 == null) return r2;
            else if (r2 == null) return r1;
            else
            {
                r1.val += r2.val;
                r1.left = MergeTrees_Efficient(r1.left, r2.left);
                r1.right = MergeTrees_Efficient(r1.right, r2.right);
                return r1;
            }
        }

        // Time O(nlogk) Space O(Max(k,d)), n = length of nums, d = distinct nums
        public static int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> numCount = new Dictionary<int, int>();
            foreach (var no in nums)             // O(n)
                if (!numCount.ContainsKey(no))
                    numCount[no] = 1;
                else
                    numCount[no]++;
            var topK = new MinHeapPair(k);
            foreach (var kvp in numCount)        // O(nlogk)
            {
                if (topK.Count < k)
                    topK.Add(kvp.Key, kvp.Value);
                else if (topK.arr[0][1] < kvp.Value)
                {
                    topK.ExtractMin();
                    topK.Add(kvp.Key, kvp.Value);
                }
            }
            return topK.arr.Select(pair => pair[0]).ToArray(); // O(k)

            /* Dictionary+LINQ, Time O(NLogN) Space O(N)
            Dictionary<int, int> numCount = new Dictionary<int, int>();
            foreach (var no in nums) // O(N)
                if (!numCount.ContainsKey(no))
                    numCount[no] = 1;
                else
                    numCount[no]++;
            return numCount.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key).Take(k).ToArray(); // O(NLogN)
            */
        }

        // Time O(nlogn) | Space O(n), n = no of cars in positions/speed array
        // requirs sorting better if target value is very high as compared to no of cars n.e. target >>> nlogn
        public static int CarFleet(int target, int[] position, int[] speed)
        {
            #region ALGO
            /*
             * Create a custom list which has both position and speed and sort it  by position of cars
             * Now we create a stack and start from 0th index of sorted array of position of cars
             * we add total time required to reach 'target' for current car with given 's' speed from 'p' position
             * 
             * we add it to the stack
             * 
             * before adding to stack just check if there is a faster car whose time to reach target is lessort than curr than pop it
             * as that will merge to current grp/fleet of cars
             * 
             * at the end we know how many fleets r there by simply counting the no grp left in stack
             */
            #endregion
            int l = position.Length;
            List<MyNode> posSpeed = new List<MyNode>();
            for (int i = 0; i < l; i++)
                posSpeed.Add(new MyNode(position[i], speed[i]));

            var sorted = (from posSp in posSpeed orderby posSp.val select posSp).ToList();      // O(nlogn)
            Stack<decimal> finishTime = new Stack<decimal>();
            foreach (var car in sorted)             // O(n)
            {
                // if there are faster car than current car will block before reaching target than pop them as it wud merge with curr car fleet
                while (finishTime.Count > 0 && finishTime.Peek() <= (decimal)(target - car.val) / car.idx)
                    finishTime.Pop();
                finishTime.Push((decimal)(target - car.val) / car.idx);  // add current car time to reach target
            }
            return finishTime.Count;
        }

        // Time = O(target) | Space = O(n), n = no of cars in input array's
        public static int CarFleet_Faster(int target, int[] position, int[] speed)
        {
            #region ALGO
            /*
                We just need to calculate the finish time of each car and basis that
                if we start from either start of the very end of track
                we can see how many slowers car will join leader car and form 1 grp.

                any car which is slower than leading car will reach in its own grp
             */
            #endregion
            int[] track = new int[target];
            for (int i = 0; i < speed.Length; i++)      // O(n)
                track[position[i]] = speed[i];  // add speed of each car on the track where they start from
            Stack<decimal> leaders = new Stack<decimal>();
            for (int i = target - 1; i >= 0; i--)       // O(target)
            {
                if (track[i] > 0)
                {
                    var currCarFinishTime = (decimal)(target - i) / track[i];
                    // if there are no leader cars or leader will reach before current car can than add curr car to the stack as it will reach in different grp
                    if (leaders.Count == 0 || leaders.Peek() < currCarFinishTime)
                        leaders.Push(currCarFinishTime);
                }
            }
            return leaders.Count;
        }

        // Time O(logP*n) Space O(1), P = max pile size in array of piles, n = length of piles array
        public static int KokoEatingBananas(int[] piles, int h)
        {
            #region ALGO
            /*
            Since we know max speed required to finish all the pile is just the size of max pile (we can also skip finding it & just start with int.MaxValue)
            minSpeed is 1 banana per hour

            as we can only go to 1 pile per hr and eat at the max speed at which we can either finish all or have to wait if eating speed in more than no of banana in given pile

            so we try all the combination in binary search way, thats we see if mid speed (avg of min+max) is good enouf to eat all the banans in <= 'h' hrs before gaurd arrive or not
            if yes we move the max to mid-1
            if no we move the min to mid+1
            also keep track of last speed at which we were able to eat all piles

            keep repeating above operation till min is <= max

            to understand if current speed 's' is enouf to finish we just need to see how many hours it will take to finish current pile and add that to reqHours
            if before finish all the piles reqHours cross threshold of 'h' we know this speed 's' is not enouf
            */
            #endregion
            int minSpeedK = 1, maxSpeedK = 1, l = piles.Length;
            // get max piles size from
            foreach (var pile in piles)  // O(n)
                if (maxSpeedK < pile)
                    maxSpeedK = pile;

            // now we try to reduce maxSpeed
            int minSpeedToFinishAll = maxSpeedK;
            while (minSpeedK <= maxSpeedK)  // O(logP)
            {
                var mid = minSpeedK + (maxSpeedK - minSpeedK) / 2;
                if (CanEatAllInH(mid))   // O(n)
                {
                    minSpeedToFinishAll = mid;
                    maxSpeedK = mid - 1;
                }
                else
                    minSpeedK = mid + 1;
            }
            return minSpeedToFinishAll;

            // helper func
            bool CanEatAllInH(int speed)
            {
                int reqHours = 0;
                foreach (var pile in piles)
                {
                    reqHours += pile / speed + (pile % speed == 0 ? 0 : 1);
                    if (reqHours > h) return false;
                }
                return true;
            }
        }

        // Time = O(n) Space = O(1), n = no of characters in word
        public static bool EqualFrequency(string word)
        {
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach (var ch in word)                    // O(n)
                if (!freq.ContainsKey(ch))
                    freq[ch] = 1;
                else
                    freq[ch]++;

            // only 1 unique char is present in input
            if (freq.Count == 1)
                return true;

            var fList = freq.Keys.ToList();
            // we try to see if by removing any 1 of the given characters its possible to achieve same freq for all letters
            foreach (var ch in fList)                   // O(26)
            {
                bool charRemoved = false;
                // remove freq of current character by 1
                if (--freq[ch] == 0)
                {
                    freq.Remove(ch);
                    charRemoved = true;
                }
                // check now if all elements have same frequency, by passing in a copy of current dictionary
                if (Check(freq.ToDictionary(entry => entry.Key, entry => entry.Value))) // O(26) n.e. O(1)
                    return true;

                // Restore back original freq of current character
                if (charRemoved) freq[ch] = 1;
                else freq[ch]++;
            }
            return false;

            // local helper func
            bool Check(Dictionary<char, int> f)
            {
                int currFreq = -1;
                // instead of using same dict copy the dictionary
                foreach (var kvp in f)
                    if (currFreq == -1)    // encountered 1st char
                        currFreq = kvp.Value;
                    else if (currFreq != kvp.Value)
                        return false;
                return true;
            }
        }


        // Time O(n^2) | Space O(1)
        public static int PalindromicSubstrings(string s)
        {
            int l = s.Length;
            if (l == 1) return 1;
            int count = 1, lt, rt;
            for (int i = 1; i < l; i++)
            {
                count++;    // each single char is palindrome

                // odd length
                lt = i - 1;
                rt = i + 1;
                while (lt >= 0 && rt < l && s[lt] == s[rt])
                {
                    count++;
                    lt--;
                    rt++;
                }

                // even length
                lt = i - 1;
                rt = i;
                while (lt >= 0 && rt < l && s[lt] == s[rt])
                {
                    count++;
                    lt--;
                    rt++;
                }
            }
            return count;
        }

        // Time O(32) ~O(1) | Space O(1)
        public static uint ReverseBits(uint n)
        {
            uint reverse = 0;
            int loopTime = 32;
            while (loopTime-- > 0)     // O(32)
            {
                reverse <<= 1;    // left shift the current ans by 1
                reverse += n & 1;   // now add whatever bit (On/Off) from the no we are reversing
                n >>= 1;
            }
            return reverse;
        }

        // Time = Space = O(1)
        public static int SumOfTwoIntegersWithoutUsingPlusOperatorBitManipulation(int a, int b)
        {
            while (b != 0)
            {
                var temp = (a & b) << 1;
                a = a ^ b;
                b = temp;
            }
            return a;
        }

        // Time = Space = O(1)
        public static int SumOfTwoIntegersWithoutUsingPlusOperatorAndUsingLogAndPowerFunc(int a, int b) => (int)Math.Log((Math.Pow(2, a) * Math.Pow(2, b)), 2);

        // Time = Space = O(r*c), r = no of rows & c = no of cols in GRID
        public static int OrangesRotting(int[][] grid)
        {
            /* ALGO
            we iterate thru each cell in the grid
            if its rotten we run BFS from current cell in all 4 possible directions and find out max time to rotten all adjacent cells
            we store this elapsed time value
            and we encounter another cell that is rotten but not already visited we run the BFS again from current cell
            we update the max of current elapsed time vs last elapsed time

            at the end we iterate thru grid once to check if we have all empty or rotten oranges if yes we return max elapsed time else -1
            */
            int rows = grid.Length, cols = grid[0].Length, maxElaspedTime = 0, r, c;
            int[][] directions = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            bool[,] visited = new bool[rows, cols];
            Queue<Pair<int, int>> q = new Queue<Pair<int, int>>();
            for (r = 0; r < rows; r++)
                for (c = 0; c < cols; c++)
                    if (grid[r][c] == 2)   // rotten
                        q.Enqueue(new Pair<int, int>(r, c));

            if (q.Count > 0) q.Enqueue(null);
            maxElaspedTime = Math.Max(maxElaspedTime, BFS());

            // check if 1 or more fresh orange is still present
            for (r = 0; r < rows; r++)
                for (c = 0; c < cols; c++)
                    if (grid[r][c] == 1)
                        return -1;
            return maxElaspedTime;

            // local helper func
            int BFS()
            {
                int totalTime = 0, curTime = 0, x, y;
                while (q.Count > 0)
                {
                    var cur = q.Dequeue();
                    if (cur == null)
                    {
                        totalTime = curTime++;
                        if (q.Count > 0) q.Enqueue(null);
                    }
                    else
                    {
                        x = cur.key;
                        y = cur.val;
                        grid[x][y] = 2;
                        foreach (var dir in directions)
                            if (IsValid(x + dir[0], y + dir[1]))
                                q.Enqueue(new Pair<int, int>(x + dir[0], y + dir[1]));
                    }
                }
                return totalTime;
            }
            bool IsValid(int x, int y)
            {
                // not valid cell or not a fresh orange
                if (x < 0 || x >= rows || y < 0 || y >= cols || grid[x][y] != 1 || visited[x, y]) return false;
                visited[x, y] = true;
                return true;
            }
        }


        // Time O(Max(klogk,n*k)) | Space O(n), k = no of unique values in 'hand' and n = length of hand
        public static bool IsNStraightHand(int[] hand, int groupSize)
        {
            if (hand.Length % groupSize != 0) return false;
            /*
            1. Create a dict with hands as key and no of time it appears as valu
            2. Now we sort this dict on keys (asc)
            3. Start from 1st key and move on to next values while deducting the counter -1 from dict
            till we get 'groupSize' consecutive elements
            4. once create once group repeat process from 2#
            5. if at any point consecutive no is no found or we run of values while creating last group return false
            */
            Dictionary<int, int> handCount = new Dictionary<int, int>();
            foreach (var val in hand)                // O(n)
                if (!handCount.ContainsKey(val))
                    handCount[val] = 1;
                else
                    handCount[val]++;
            var sorted = (from kvp in handCount
                          orderby kvp.Key
                          select kvp.Key).ToList();   // O(klogk)
            bool groupFormed = false;
            int idxInSorted = 0, len = hand.Length, totalHandsUsed = 0, curGrpCount = 0, lastVal = -1, curVal;
            while (totalHandsUsed < len)               // O(n)
            {
                if (idxInSorted >= sorted.Count) return false;   // we don't have next consecutive val for current grp
                curVal = sorted[idxInSorted]; // get next value for the group
                                              // update sorted if counter for any value goes to zero
                if (--handCount[sorted[idxInSorted]] == 0)
                {
                    sorted.RemoveAt(idxInSorted);   // O(k)
                    idxInSorted--;  // to compensate for removed idx
                }
                // cur value is greater than 1 + lastVal
                if (lastVal != -1 && lastVal + 1 != curVal)
                    return false;

                // Console.WriteLine($" Val {curVal} | Counter after removal {handCount[sorted[idxInSorted]]}");
                if (++curGrpCount == groupSize)
                {
                    curGrpCount = 0;
                    idxInSorted = 0;
                    lastVal = -1;
                    groupFormed = true;
                }
                else
                {
                    groupFormed = false;
                    idxInSorted++;
                    lastVal = curVal;
                }
                totalHandsUsed++;
            }
            return groupFormed;
        }
        // NeetCode https://youtu.be/amnrMCVd2YI
        // Time O(n*logk) | Space O(n), k = no of unique values in 'nums' and n = length of hand
        public static bool IsPossibleDivide(int[] nums, int k)
        {
            /* ALGO
            1. Create a dict with nums as key and no of time it appears as value
            2. We also add all unique values to MinHeap
            3. While MinHeap Counter is > 0 we start to form a grp and pick starting point as minElement from Heap and use it to form grp
            4. after 1st element in grp we see if dictionary has val+1 if yes we pick that up and continue forming current grp
            5. At any point if next value is not present in dict we can return false
            6. we also keep reducing counter-1 in dictionary whenever we are taking a val to form a grp and if it reaches 0 we also remove this value from MinHeap
            6a. Imp point to note is we are trying to remove a value from Heap which is not the minmum that we can just return false,
                Bcoz, if we have val,counter {1,2},{2,1},{3,1},{4,1},{5,6}
                we start with 1 and reduce counter in dict {1,1},{2,1},{3,1},{4,1},{5,6}
                now we look for 1+1 n.e. 2 and yes its present but on reduce it by 1 in dict we also need to pop it from Heap but Heap min is 1 not 2
                hence we can return true because we know whenever we are going to create next grp we won't find net consecutive val for 1 which is still present once in dict
            7. if at any point consecutive no is no found or we run of values while creating last group return false
            */
            if (nums.Length % k != 0) return false;
            Dictionary<int, int> numFreq = new Dictionary<int, int>();
            foreach (var val in nums)                   // O(n)
                if (!numFreq.ContainsKey(val))
                    numFreq[val] = 1;
                else
                    numFreq[val]++;

            MinHeap heap = new MinHeap(numFreq.Count);
            foreach (var num in numFreq.Keys)           // O(klogk)
                heap.Insert(num);
            int curGrpCount = 0, first;
            while (heap.Count > 0)                      // O(n)
            {
                first = heap.GetMin();
                if (--numFreq[first] == 0)
                    heap.ExtractMin();                  // O(logn)
                while (++curGrpCount < k)
                    if (numFreq.ContainsKey(++first))
                    {
                        if (--numFreq[first] == 0)
                        {
                            if (heap.GetMin() != first)
                                return false;
                            else
                                heap.ExtractMin();      // O(logn)
                        }
                    }
                    else
                        return false;
                curGrpCount = 0;
            }
            return true;
        }


        // Time O(n) | Space O(1), n = length of triplets
        public static bool MergeTriplets(int[][] triplets, int[] target)
        {
            bool gotA = false, gotB = false, gotC = false;
            int tarA = target[0], tarB = target[1], tarC = target[2];
            foreach (var triplet in triplets)
                if (HasAllValuesSmallerOrEqualToTarget(triplet))
                {
                    gotA = gotA | tarA == triplet[0];
                    gotB = gotB | tarB == triplet[1];
                    gotC = gotC | tarC == triplet[2];
                    if (gotA && gotB && gotC) return true;
                }
            return false;
            bool HasAllValuesSmallerOrEqualToTarget(int[] A) => A[0] <= tarA && A[1] <= tarB && A[2] <= tarC;
        }


        // Time O(nlogn) Space O(1), n = length of 'intervals' array
        public static int EraseOverlapIntervals(int[][] intervals)
        {
            intervals = intervals.OrderBy(x => x[0]).ToArray();
            int prvEnd = intervals[0][1], l = intervals.Length, removed = 0;
            for (int i = 1; i < l; i++)
                // no overlapping
                if (intervals[i][0] >= prvEnd)
                    prvEnd = intervals[i][1];
                else
                {
                    removed++;
                    // remove the interval which ends later and keep the one which end earlier
                    prvEnd = Math.Min(prvEnd, intervals[i][1]);
                }
            return removed;
        }

        // Optimized BRUTE FORCE
        // Time O(n* range+m) Space O(m), n = length of intervals and m = length of queries
        public static int[] MinInterval_BruteForce(int[][] intervals, int[] queries)
        {
            Dictionary<int, int> numSmallestInterval = new Dictionary<int, int>();
            int smallest = Int32.MaxValue, largest = Int32.MinValue;
            foreach (var num in queries)         // O(m)
            {
                numSmallestInterval[num] = Int32.MaxValue;
                smallest = Math.Min(smallest, num);
                largest = Math.Max(largest, num);
            }

            foreach (var interval in intervals)  // O(n)
            {
                int size = 1 + interval[1] - interval[0], lt = interval[0], rt = interval[1];
                if (rt < smallest || lt > largest) continue;

                while (lt <= rt)                 // O(range), range=largest-smallest
                {
                    if (numSmallestInterval.ContainsKey(lt))
                        numSmallestInterval[lt] = Math.Min(numSmallestInterval[lt], size);
                    if (++lt > largest)
                        break;
                }
            }

            int[] minInterval = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++)   // O(m)
                minInterval[i] = numSmallestInterval[queries[i]] != Int32.MaxValue ? numSmallestInterval[queries[i]] : -1;
            return minInterval;
        }

        // Time O(Max(nlogn,n*m)) Space O(m), n = length of intervals and m = length of queries
        public static int[] MinInterval_Sorting(int[][] intervals, int[] queries)
        {
            /* ALGO
            Sort the intervals array on the basis of size (1+right-left)
            Create a dictionary which initially has all the numbers in queries as key and idx as value
            Now start iterating on sorted intervals foreach sorted interval
                for all the numbers in dictionary which lie b/w this interval
                set the size (which we know wud be smallest as we have sorted the intervals in asc order)
                also update the minSize array with the size for given num by using idx from dictionary and remvoe it from dictionary

                we break this loop once dictionary has 0 elements or we run out of all the intervals
            return minSize array;
            */
            // intervals = intervals.OrderBy(x => 1+x[1]-x[0]).ToArray();
            int l = queries.Length, smallest = Int32.MaxValue, largest = Int32.MinValue, num;
            Dictionary<int, List<int>> queryNumIdx = new Dictionary<int, List<int>>();
            for (int i = 0; i < l; i++)            // O(m)
            {
                num = queries[i];
                if (!queryNumIdx.ContainsKey(num))
                    queryNumIdx[num] = new List<int>() { i };
                else
                    queryNumIdx[num].Add(i);

                smallest = Math.Min(smallest, num);
                largest = Math.Max(largest, num);
            }
            int[] minInterval = new int[l];
            foreach (var n in intervals.OrderBy(x => 1 + x[1] - x[0]))   // O(nlogn)
            {
                int size = 1 + n[1] - n[0], lt = n[0], rt = n[1];
                if (rt < smallest || lt > largest) continue; // optimization
                var dict = queryNumIdx.ToList();
                // An improvement can be made by sorting the query array and finding the ranges of values which lie b / w lt and rt
                // and only iterating and updating applicable those values instead of blindly search entire query array
                foreach (var kvp in dict)    // O(m)
                    if (lt <= kvp.Key && kvp.Key <= rt)
                    {
                        foreach (var idx in kvp.Value)
                            minInterval[idx] = size;
                        queryNumIdx.Remove(kvp.Key);    // smallest found for current num hence we can remove from dictionary
                    }
            }
            // set default value for num for which no size could be found
            foreach (var indexRange in queryNumIdx.Values)
                foreach (var idx in indexRange)
                    minInterval[idx] = -1;
            return minInterval;
        }
        // Time O(nlogn+mlogm) Space O(n), n = length of intervals and m = length of queries
        public static int[] MinInterval_Efficient_Heap_Sorting(int[][] intervals, int[] queries)
        {
            /* ALGO
            a. sort the intervals basis their start value
            b. sort the queries and keep original indexes in Dictionary
            c. Now iterate thru the sorted intervals whose starting value is smaller than the current queries[n] where we are at (n starts from 0)
            d. keep adding above values in to MinHeap which has size at the main value paired with ending index of a interval
            e. now check and remove HeapMin its its ending value is smaller than queried values n.e. query is out of range
            f. repeated step#e till we found a interval whoese end value is >= query value or Heap is empty
            g. if Heap empty insert -1 in result array for current query value.
            */
            var pq = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
            Dictionary<int, List<int>> queryNumIdx = new Dictionary<int, List<int>>();
            int queryLen = queries.Length, i;
            int[] minInterval = new int[queryLen];                     // result array

            // add all the numbers from the query into dictionary as key and their index in a list as value
            for (i = 0; i < queryLen; i++)                             // O(m)
                if (!queryNumIdx.ContainsKey(queries[i]))
                    queryNumIdx[queries[i]] = new List<int>() { i };
                else
                    queryNumIdx[queries[i]].Add(i);
            // sort the input query array
            var sortedQuery = (from query in queryNumIdx
                               orderby query.Key
                               select query.Key).ToList();      // O(mlogm)
            i = 0;
            queryLen = sortedQuery.Count;


            intervals = intervals.OrderBy(x => x[0]).ToArray(); // O(nlogn)
            int j = 0, intervalLen = intervals.Length;
            while (i < queryLen)                                // O(m)
            {
                var curQueryVal = sortedQuery[i++];
                while (j < intervalLen)                         // O(n)
                {
                    // adding interval till the startValue is <= current queried value
                    if (intervals[j][0] <= curQueryVal)
                    {
                        pq.Enqueue(intervals[j][1], 1 + intervals[j][1] - intervals[j][0]);  // O(logn)
                        j++;
                    }
                    else
                        break;
                }

                // remove all invalid interval from MinHeap n.e. one which have end value smaller than current queried value
                while (pq.Count > 0 && pq.Peek() < curQueryVal)
                    pq.Dequeue();                            // O(logn)

                // update minsize
                var minSize = pq.TryPeek(out int key, out int priority) ? priority : -1;
                foreach (var idx in queryNumIdx[curQueryVal])
                    minInterval[idx] = minSize;

                // now current query value is no longer needed
                queryNumIdx.Remove(curQueryVal);
            }

            // set default value for any remaining index
            foreach (var indexes in queryNumIdx.Values)
                foreach (var idx in indexes)
                    minInterval[idx] = -1;

            return minInterval;
        }


        // Time O(n*m) | Space O(Max(n,m)), n & m = length of string s1 & s2 respectively
        public static string MultiplyStrings(string s1, string s2)
        {
            // base case
            if (s1[0] == '0' || s2[0] == '0') return "0";
            int l1 = s1.Length, l2 = s2.Length;
            // we always want to process with s1 >= s2
            if (l1 < l2) return MultiplyStrings(s2, s1);

            // creat integer array with bigger number as num1 and smaller num padded with 0 on left as num2
            int[] num1 = new int[l1];
            int[] num2 = new int[l1];
            for (int i = l1 - 1; i >= 0; i--) num1[i] = s1[i] - '0';
            int k = l1;
            for (int i = l2 - 1; i >= 0; i--) num2[--k] = s2[i] - '0';

            Stack<int> lastMultiplication = new Stack<int>();
            Stack<int> afterCurrentMultipliation;
            int n1, n2, carry;
            // multiple each digit in 2nd string by each num in 1st string n.e. 'num1' (right->left)
            for (int j = l1 - 1; j >= l1 - l2; j--)
            {
                n2 = num2[j];
                afterCurrentMultipliation = new Stack<int>();
                // move previous values by 1 position to right
                int move = j;
                while (move++ < l1 - 1)
                    afterCurrentMultipliation.Push(lastMultiplication.Count > 0 ? lastMultiplication.Pop() : 0);
                carry = 0;
                // start multiplication
                for (int i = l1 - 1; i >= 0; i--)
                {
                    n1 = num1[i];
                    carry += lastMultiplication.Count > 0 ? lastMultiplication.Pop() : 0;
                    carry += (n1 * n2);
                    afterCurrentMultipliation.Push(carry % 10);
                    carry /= 10;
                }
                // add any remaining carry to computed ans
                if (carry != 0) afterCurrentMultipliation.Push(carry);

                lastMultiplication = new Stack<int>(afterCurrentMultipliation.ToArray());
            }
            // create final ans
            StringBuilder sb = new StringBuilder();
            foreach (var digit in lastMultiplication.Reverse())
                sb.Append(digit);
            return sb.ToString();
        }


        // Time = Space = O(logN)
        public static double MyPower(double x, long n)
        {
            /* ALGO
            Instead of multiple number by itself N times we can just apply 'divide and conquer' technique
            by calculating half the req power and multiplying the ans with itself again to get the actual power
            + mod of 2 n.e. power 1 if any left after dividing the power in half
             */

            // base case
            if (x == 0) return 0;
            if (x == 1) return 1;

            // add base case for power
            Dictionary<long, double> powerCache = new Dictionary<long, double>()
            {
                { 0, 1 },   // anything to power 0 is 1
                { 1, x }    // power 1 means the original number itself
            };

            return n >= 0 ? GetPower(Math.Abs(n)) : 1 / GetPower(Math.Abs(n));

            // local helper func
            double GetPower(long power)
            {
                if (powerCache.ContainsKey(power))
                    return powerCache[power];
                // we divide the work into half + carry if any
                return powerCache[power] = GetPower(power / 2) * powerCache[power / 2] * GetPower(power % 2);
            }
        }


        // Time O(n*logk) | Space O(k), n = length of 'points' array
        public static int[][] KClosestPoints(int[][] points, int k)
        {
            /* ALGO
            we add each points in Maxheap with their distance to origin 0,0 as priority
            for each new points add to MaxHeap
                if Heap size is smaller than 'k'
                if Heap top n.e. largest distance is larger than incoming
                    element than remove top and add new point

            at the end pop all the K points from Heap and add to result and return

            Note: keep the priorty as double as we don't want to round of the distance by using int and skew the result
             */
            // base case
            if (points.Length <= k) return points;

            var maxHeap = new System.Collections.Generic.PriorityQueue<int[], double>(Comparer<double>.Create((x, y) => y.CompareTo(x)));
            foreach (var point in points)                // O(n)
            {
                // Euclidean distance formula n.e., √((x1 - x2)^2 + (y1 - y2)^2).
                var distance = Math.Sqrt(Math.Pow(point[0], 2) + Math.Pow(point[1], 2));
                // Console.WriteLine($" Dist {distance} from origin for {point[0]},{point[1]}");
                if (maxHeap.Count < k)
                    maxHeap.Enqueue(point, distance);    // O(logk)
                else
                {
                    maxHeap.TryPeek(out int[] element, out double priority);
                    if (distance < priority)
                        // remove top and immediately add new element
                        maxHeap.DequeueEnqueue(point, distance);    // O(logk)
                }
            }
            int[][] kCloset = new int[k][];
            for (int i = 0; i < k; i++)                        // O(k)
                kCloset[i] = maxHeap.Dequeue();           // O(logk)
            return kCloset;
        }


        // Time O(m*n) | Space O(1)
        public static int NumberOfBeams(string[] bank)
        {
            int lastRowSecurityDevices = 0, curRowDevices, lasers = 0;
            foreach (var row in bank)           // O(m)
            {
                curRowDevices = 0;
                // count no of security devices on current floor
                foreach (var ch in row)         // O(n)
                    if (ch == '1')
                        curRowDevices++;

                lasers += curRowDevices * lastRowSecurityDevices;
                // update if last row has atleast 1 device
                if (curRowDevices != 0)
                    lastRowSecurityDevices = curRowDevices;
            }
            return lasers;
        }


        // Time O(nlogn) | Space O(1)
        public static int FindMinArrowShots(int[][] points)
        {
            points = (from point in points              // O(nlogn)
                      orderby point[1]    // sort in ascending order by end cordinate
                      select point).ToArray();
            int arrows = 1, xAxisEndOfLastBalloon = points[0][1];
            for (int i = 1; i < points.Length; i++)            // O(n)
            {
                int start = points[i][0], end = points[i][1];
                if (start <= xAxisEndOfLastBalloon)
                    continue;
                else
                {
                    arrows++;
                    xAxisEndOfLastBalloon = end;
                }
            }
            return arrows;
        }


        // Time = Space = O(n), n = length of 'nums' array
        public static int MinOperations(int[] nums, Dictionary<int, int> cache)
        {
            Dictionary<int, int> freq = new();
            foreach (var num in nums)
                if (freq.TryGetValue(num, out int count))
                    freq[num] = ++count;
                else
                    freq[num] = 1;
            int opCount = 0;
            foreach (var numFreq in freq.Values)
                if (numFreq == 1) return -1;
                else opCount += ReduceToZero(numFreq);
            return opCount;

            // local helper func
            int ReduceToZero(int dividend)
            {
                // 1st approach Brute Force // Still Passed all TC's
                // if(reduce.TryGetValue(n, out int redux)) return redux;
                // else return reduce[n] = 1+Math.Min(ReduceToZero(n-3),ReduceToZero(n-2));

                // 2nd alternative & a faster way is just to divide the no by 3 and than take mod 3 it can be three value 0, 1, 2
                // if 0 no need to further reduce if its 2 means we need to reduce its +1 more time & its its 1 still need to reduce
                // 1 more time,
                // now before u wonder how do we reduce leftover 1
                // ex: reduce 10
                // Quotient: 10/3 = 3
                // Remainder: 10%3 = 1
                // total 3+1 n.e. 4 operation to reduce 10 to 0
                // but it wud be like reduce 10 once by 3, that leave 7 again by 3 leave 4 now twice by 2 leave 0
                // 1 + 1 + 2 = 4
                // Ex: for 8
                // Q: 8/3 = 2
                // R: 8%3 = 2
                // Reducing looks like 8 >> 5 >> 2 >> 0
                // Twice by 3 once by 2     1 +  1 +  1
                if (cache.TryGetValue(dividend, out int redux)) return redux;
                int quotient = dividend / 3;
                int remainder = dividend % 3;
                return cache[dividend] = quotient + ((remainder == 0) ? 0 : 1);
            }
        }


        // Time O(n) | Space O(n), n = no of nodes in the binary-tree
        public static int AmountOfTime(TreeNode root, int start)
        {
            // ALGO
            // 1st pass we iterate and once we found start node we return back the distance+1 for root node and update it
            // and also update both the child of start node & descendants

            // 2nd iteration we will have the distance for the root node we keep update the distance for each node if its not updated already
            // lastly we return the maximum distance found

            Dictionary<int, int> nodesDistFromStart = new();
            int maxDistance = 0, dist = GetDistanceOfStartFromRoot(root, start);     // O(n)

            // left child is not updated, update the distance
            if (root.left != null && !nodesDistFromStart.ContainsKey(root.left.val))
                UpdateChild(root.left, ++dist);                                      // O(n)
                                                                                     // right child is not updated, update the distance
            if (root.right != null && !nodesDistFromStart.ContainsKey(root.right.val))
                UpdateChild(root.right, ++dist);                                     // O(n)

            return maxDistance;

            // local helper func
            int GetDistanceOfStartFromRoot(TreeNode r, int s)           // O(n)
            {
                if (r == null) return -1;
                if (r.val == s)    // found start
                {
                    nodesDistFromStart[r.val] = 0;
                    // update both child of start node
                    UpdateChild(r.left, 1);
                    UpdateChild(r.right, 1);
                    // return the distance to parent of start
                    return 1;
                }
                else
                {
                    var ltDist = GetDistanceOfStartFromRoot(r.left, s);
                    if (ltDist != -1)  // start found in the left side
                    {
                        // update distance of cur root
                        maxDistance = Math.Max(maxDistance, nodesDistFromStart[r.val] = ltDist++);
                        // update distance of the other child
                        UpdateChild(r.right, ltDist);
                        // return the distance for the parent
                        return ltDist;
                    }
                    else
                    {
                        var rtDist = GetDistanceOfStartFromRoot(r.right, s);
                        if (rtDist != -1)  // start found in the right side
                        {
                            // update distance of cur root
                            maxDistance = Math.Max(maxDistance, nodesDistFromStart[r.val] = rtDist++);
                            // update distance of the other child
                            UpdateChild(r.left, rtDist);
                            // return the distance for the parent
                            return rtDist;
                        }
                        return -1;
                    }
                }
            }
            void UpdateChild(TreeNode r, int dist)                       // O(n)
            {
                if (r == null) return;
                maxDistance = Math.Max(maxDistance, nodesDistFromStart[r.val] = dist++);
                UpdateChild(r.left, dist);
                UpdateChild(r.right, dist);
            }
        }

        // Time O(n^2) | Space O(n)
        public static string PermutationSequence(int n, int k, Dictionary<int, int> factDict)
        {
            /*
            Idea is to calculate each value of the permutation by dividing the complete range into sub-ranges.
            We will create an array for storing indices of the sub-range and these indices will be used to get the final result.
            */

            // create a list with all numbers from 1..N
            List<int> nums = new();
            for (int i = 1; i <= n; i++) nums.Add(i);

            List<char> ans = new();
            Sequence(1, factDict[n], n);
            return new string(ans.ToArray());

            // local helper func
            void Sequence(int start, int end, int numCount)
            {
                if (numCount == 0) return;
                int range = end - start + 1;
                int curRange = range / numCount;
                for (int i = 1; i <= numCount; i++)             // O(n)
                {
                    int lower = start + (i - 1) * curRange;
                    int upper = lower + curRange - 1;
                    if (lower <= k && k <= upper)
                    {
                        ans.Add((char)(nums[i - 1] + '0'));     // add the number to the final ans
                        nums.RemoveAt(i - 1);                   // remove the current no from the list
                        Sequence(lower, upper, numCount - 1);   // O(n), recusively call the smaller range for n-1 numbers
                        break;
                    }
                }
            }
        }


        // Time O(Max(n,wlogw+dlogd)) | Space O(n), n = length of matches, w = no of winners, d = no of 1 match defeated
        // worst case we have 50% winner and loosers everthing wud be n/2 n.e n hence T (nlogn) | S O(n)
        public static IList<IList<int>> FindWinners(int[][] matches)
        {
            /* ALGO
            We have 2 dictionary:
                winner
                loosers
            we iterate thru all the matches and for each match
                we add the winner to winner list if he is not present in loosers already
                for each looser we add them to loosers hashtable with matches lost count 1
                    we also check if looser is present in winner we also remove them for it
            */
            HashSet<int> winners = new();
            Dictionary<int, int> loosers = new();
            foreach (var match in matches)               // O(n)
            {
                var winner = match[0];
                var looser = match[1];

                // add P1 to winners, if not already lost a match before
                if (!loosers.ContainsKey(winner))
                    winners.Add(winner);

                // add P2 to loosers
                if (loosers.TryGetValue(looser, out int lostMatchesCount))
                    loosers[looser] = 1 + lostMatchesCount; // not the 1st match P2 lost
                else
                {
                    // P2 lost 1st match and if they have won before need to remove from winners section
                    if (winners.Contains(looser))
                        winners.Remove(looser);
                    // add lost matches count as '1' for P2
                    loosers[looser] = 1;
                }
            }

            // sort the winners                             // O(wlogw)
            var sortedWinners = (from whoWon in winners
                                 orderby whoWon
                                 select whoWon).ToList();
            List<IList<int>> ans = new List<IList<int>>();
            // add winners
            ans.Add(sortedWinners);

            // find out 1 match loosers
            List<int> oneMatchLoosers = new();
            foreach (var kvp in loosers)                     // O(d)
                if (kvp.Value == 1)
                    oneMatchLoosers.Add(kvp.Key);
            // sort the 1 match loosers
            oneMatchLoosers.Sort();                         // O(dlogd)
                                                            // add one match loosers
            ans.Add(oneMatchLoosers);

            return ans;
        }


        // Time O(n^2) | Space O(n) , n = length of 'points' array
        public static int MaxPointsOnALine(int[][] points)
        {
            Dictionary<string, HashSet<int>> slopeIntercept_LineCount = new();
            double slope, intercept;

            for (int p1 = 0; p1 < points.Length; p1++)
                for (int p2 = p1 + 1; p2 < points.Length; p2++)
                {
                    (slope, intercept) = CalculateSlopeIntercept(points[p1], points[p2]);
                    string key = slope + "," + intercept;
                    if (slopeIntercept_LineCount.TryGetValue(key, out HashSet<int> unqPoints))
                    {
                        // both the points for the given (slope,intercept) key
                        unqPoints.Add(p1);
                        unqPoints.Add(p2);
                    }
                    else slopeIntercept_LineCount[key] = new HashSet<int>() { p1, p2 };
                }

            int maxPointsOnALine = 1;
            // get the slope,intercept combo which has most no of points
            foreach (var setOfPointsOnSlope in slopeIntercept_LineCount.Values)
                maxPointsOnALine = Math.Max(maxPointsOnALine, setOfPointsOnSlope.Count);
            return maxPointsOnALine;

            // local helper func
            (double, double) CalculateSlopeIntercept(int[] p1, int[] p2)
            {
                int x1 = p1[0], x2 = p2[0], y1 = p1[1], y2 = p2[1];
                if (y1 == y2)
                    return (0, y2);  // line parallel to X-axis, hence slope is zero
                else if (x1 == x2)
                    return (double.MaxValue, x1);  // line parallel to Y-Axis, avoid divide-by-zero-error simply return x cordinate
                else
                {
                    double slope = (double)(y2 - y1) / (double)(x2 - x1);
                    double intercept = y1 - (slope * x1);
                    return (slope, intercept);
                }
            }
        }


        // Time = Space = O(n), n = length of input 'arr'
        public static int SumSubarrayMins(int[] arr)
        {
            /* ALGO
            1# The brute force way to solve is simple find no of sub-array n^2 find min in each sub-array n n.e. n^3 Soln

            2# better is while creating sub-array and adding each num keep track of the min hence Time get reduced to n^2 as loop for finding min is not req

            3# To Solve it in O(n) we need to understand for each num how many sub-arrays it wud be part of (such that its the smallest num in all)
            for this we need to use monotonic sequence
            we calculate no of num on left each num is part of by
                check while stack is not empty we see the top num if larger than pop and take its count of sub-array it was the smallest
                now add current num to stack with smallest+1 as no of array it wud be the smallest on left
            we also keep storing this count of each num in another array

            we calculate in the same now from right to left, count of sub-arrays each num is smallest in
                one added condition to avoid duplicates nums is this time we pop the top from stack even is its equal to current

            Now its simple we get the total min sum by using below for each number in array
            num * smallerOnLeftCount * smallerOnRightCount
             */
            int n = arr.Length, modulo = 1000000007, biggerThan;
            long totalMinSum = 0, minTimes;
            long[] smallerThanNumsOnLeft = new long[n];
            Stack<Pair<int, int>> st = new();
            // Count no of subarray each num is smaller than on the left
            for (int i = 0; i < n; i++)
            {
                biggerThan = 1;
                while (st.Count > 0 && st.Peek().key > arr[i])
                    biggerThan += st.Pop().val;
                st.Push(new Pair<int, int>(arr[i], biggerThan));
                smallerThanNumsOnLeft[i] = biggerThan;
            }
            st.Clear();
            // Count no of subarray each num is smaller than on the right
            for (int i = n - 1; i >= 0; i--)
            {
                biggerThan = 1;
                while (st.Count > 0 && st.Peek().key >= arr[i])  // remove the stack top even if its equal to curr num
                    biggerThan += st.Pop().val;
                st.Push(new Pair<int, int>(arr[i], biggerThan));
                // now calculate the total after taking into account how many sub-arrays each num was part of in total
                minTimes = (arr[i] * (smallerThanNumsOnLeft[i] * biggerThan) % modulo) % modulo;
                totalMinSum = (totalMinSum + minTimes) % modulo;
            }
            return (int)totalMinSum;
            #region TLE O(n^2)
            // // Time = Space = O(n^2), n = length of input 'arr' || TLE
            // public int SumSubarrayMins(int[] arr) {
            //     int totalMinSum = 0, n = arr.Length, curMin,modulo=1000000007,arrMin = int.MaxValue;
            //     foreach(var num in arr) arrMin = Math.Min(arrMin,num);
            //     for(int startingIdx=0;startingIdx<n;startingIdx++)
            //     {
            //         curMin = int.MaxValue;
            //         for(int endIdx=startingIdx;endIdx<n;endIdx++)
            //         {
            //             curMin = Math.Min(curMin,arr[endIdx]);
            //             // optimization added to prevent further looking once we have found the minima of the entire array
            //             if(curMin==arrMin)
            //             {
            //                 totalMinSum = (totalMinSum+((curMin*(n-endIdx))%modulo))%modulo;
            //                 break;
            //             }
            //             Console.Write($" {curMin} ||");
            //             totalMinSum = (totalMinSum+curMin)%modulo;
            //         }
            //         Console.WriteLine();
            //     }
            //     Console.WriteLine($"totalMinSum {totalMinSum}");
            //     return totalMinSum;
            // }
            #endregion
        }


        // Time O(n*(logn)^2) | Space O(n)
        public static long MinimumCost(int[] nums, int k, int dist)
        {
            long minSum = long.MaxValue;
            Window window = new Window(k - 2);
            for (int l = 1 + 1; l < Math.Min(1 + dist, nums.Length - 1); l++)
                window.Insert(nums[l]);

            for (int i = 1; i < nums.Length - (k - 2); ++i)
            {
                if (i + dist < nums.Length)
                    window.Insert(nums[i + dist]);
                long temp = nums[0] + nums[i] + window.MinSum();
                minSum = Math.Min(minSum, temp);
                window.Remove(nums[i + 1]);
            }
            return minSum;
            #region good Attempts but TLE
            /*
            // PriorityQueue based | TLE
            // Time O(n*distlog(dist)) | Space O(k), n = length of 'nums' k , dist are input params given in arguments
            //Create MaxHeap of k values from a given index
            //Now iterate till currIdx+dist add new elements if its smaller than MaxHeap by replacing Heap top with new element
        
            //take the sum of of all elements in the Heap and update sumMinTotal if its smaller
            PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
            long minCostKSubArray = long.MaxValue, curMinCost;

            for (int n = 1; n < nums.Length - dist; n++)         // O(n)
            {
                maxHeap.Clear();
                curMinCost = 0;
                for (int idx = n; idx <= n + dist; idx++)      // O(dist)
                {
                    if (maxHeap.Count < k - 1)
                    {
                        maxHeap.Enqueue(nums[idx], nums[idx]);  // O(log(dist))
                        curMinCost += nums[idx];
                    }
                    else if (maxHeap.Peek() > nums[idx])
                    {
                        curMinCost -= maxHeap.Dequeue();        // O(log(dist))
                        curMinCost += nums[idx];
                        maxHeap.Enqueue(nums[idx], nums[idx]);  // O(log(dist))
                    }
                }
                minCostKSubArray = Math.Min(minCostKSubArray, curMinCost);
            }

            return minCostKSubArray + (long)nums[0];

            // DP based | TLE
            int l = nums.Length;
            Dictionary<int, long>[] idxKMin = new Dictionary<int, long>[l];
            for (int n = 1; n < l; n++) idxKMin[n] = new();
            return nums[0] + Min(1, k - 1);
            // local helper func
            long Min(int n, int minValReq, int firstSplitIdx = -1)
            {
                if (minValReq == 0) return 0;
                if (n >= l) return int.MaxValue;
                if (idxKMin[n].TryGetValue(minValReq, out long val)) return val;

                long minCost = long.MaxValue;
                // skip current index
                if(l-minValReq<n+1 && (firstSplitIdx==-1 || firstSplitIdx+dist- minValReq <= n+1))
                    minCost = Math.Min(minCost, Min(n + 1, minValReq));

                
                // take one of the values from n...n+dist
                for (int idx = n; idx <= Math.Min((firstSplitIdx!=-1 ? (firstSplitIdx + dist - (minValReq-1)) : (n + dist)), l - minValReq); idx++)
                    minCost = Math.Min(minCost, nums[idx] + Min(idx + 1, minValReq - 1, firstSplitIdx==-1 ? idx : firstSplitIdx));
                return idxKMin[n][minValReq] = minCost;
            }
            */
            #endregion
        }


        /// <summary>
        /// Time O(n^2) | Space O(1), n = length of 'nums'
        /// 1. Start from 0th index till n-1,
        /// keep iterating thru indexes till we found a num which is smaller than next
        /// 2. count on bits for cur and next numbers
        /// if same swap the numbers in array
        /// set index to n-1 to check if newly replaced num is smaller than even the last num
        /// else return false
        /// </summary>
        public static bool CanSortArray(int[] nums)
        {
            // skip indexes which are already sorted
            // when we encounter a value smaller than last value check if they can be sorted
            // if yes swap
            // now start from idx -1 again
            int i = 0, l = nums.Length, cur, next, temp;
            if (l < 2) return true;

            while (i < l - 1)
            {
                cur = nums[i];
                next = nums[i + 1];
                if (cur > next)
                {
                    if (CountBits(cur) == CountBits(next))      // can swap
                    {
                        nums[i] = next;
                        nums[i + 1] = cur;
                        i = i > 0 ? i - 1 : i;
                    }
                    else
                        return false;
                }
                else i++;
            }

            return true;

            // local helper func which return no of on bits for a given number
            int CountBits(int n)
            {
                int onBits = 0;
                while (n > 0)
                {
                    onBits += n & 1;
                    n >>= 1;
                }
                return onBits;
            }
        }


        // Time O(n) | Space O(1), n = length of 'nums'
        public static int MinimumArrayLength(int[] nums)
        {
            /* ALGO
            The only thing to keep in mind is
            smallerNum % largerNum = smallerNum
            so we can actully reduce the length of entire array to the no of smallest element frequeny
            and than since any num % num = 0, we wud need up with (smallerNumFreq+1)/2 numbers after all the operations
                even no of nums result in freq/2 zero's
                odd no result in 1 + freq/2 zero's
                hence just (1+freq of smallest)/2 to get final count

            2nd point to keep in mind is is there a way to further reduce the minNum we have in the array (which is not zero)
            this is crucial bcoz if we get a even smaller num we know we can reduce all other numbers by that num & utlimatly be left with array of size 1
            */
            int smallest = int.MaxValue, minValue = 0, minFreq = 0;
            foreach (var n in nums)              // O(n)
                if (n < smallest)                  // found new smallest
                {
                    smallest = minValue = n;
                    minFreq = 1;
                }
                else if (n == smallest)
                    minFreq++;                  //update the freq of smallest

            // try to get a min value smaller than current minValue
            foreach (var n in nums)              // O(n)
                                                 // if we can further reduce the minValue just return 1
                if (n % minValue != 0)   // if mod is not zero it surely will be smaller than current minValue
                    return 1;

            return ++minFreq / 2;
        }


        // Time O(n/10) ~O(1) | Space O(1), n = input number
        public static int CountDigitOne(int n)
        {
            if (n == 0) return 0;
            long baseNum = 1, sum = 0, lt, cur, rt;
            while (baseNum <= n)
            {
                lt = n / baseNum / 10;
                cur = n / baseNum % 10;
                rt = n % baseNum;
                if (cur > 1) sum += (lt + 1) * baseNum;
                else if (cur == 1) sum += (lt * baseNum) + rt + 1;
                else sum += lt * baseNum;
                baseNum *= 10;
            }
            return (int)sum;
        }


        // Time O(nlogn) | Space O(n), n = length of 'nums' array
        public static int[][] DivideArray(int[] nums, int k)
        {
            List<int[]> ans = new List<int[]>();
            Array.Sort(nums);               // O(nlogn)

            // see if grouping from left to right any pair of 3 nums min to max don't fit within K than return empty
            for (int i = 2; i < nums.Length; i += 3)  // O(n/3) ~O(n)
                // check min-max diff is within 'k'
                if (nums[i] - nums[i - 2] > k)
                    return new int[0][];
                else
                    ans.Add(new int[3] { nums[i - 2], nums[i - 1], nums[i] });
            return ans.ToArray();
        }


        // Time O(k) | Space O(n), k = diff in no of digits in (high - low), n = no of valid answers
        public static IList<int> SequentialDigits(int low, int high)
        {
            /* ALGO
                1. convert the low and high into string
                2. now take a empty int array who's length if equal to no of digits in low
                3. append 0th index with first index
                4. check if initial digits allow creating sequence
                    - which is possible is all numbers are excatly 1 plus the last number
                    - or we get a idx which has lower digit than prv means we can add any digit instead and it won't violate low <= curNum
                5. now for length of current array we start with 1st digit and the icreamental digit till entire array is full
                6. after combination are used for a given length we increament the length of curr array
                6. special condition need to be taken care is when cur array length == no of digits in High
                    - we keep checking each digit we add is smaller than the digit as given index in high
                    - once we see we are adding a smaller digit than equaivalent digit in high for given idx we can skip checking rest of the digits
                    - at any point if we are adding digit greather than high digit we break out as no further sequential would be within bounds
                7. create the number with all the digits in array and append to final list
             */
            IList<int> ans = new List<int>();
            string start = low + "", end = high + "";
            int[] cur = new int[start.Length];
            cur[0] = start[0] - '0';

            // checking the low number is sequential or not
            for (int i = 1; i < start.Length; i++)          // O(9)
                // next digit is smaller than prv digit which allows adding any bigger digit without exceeding low
                if (start[i] < start[i - 1]) break;
                // else check if digits are sequential
                else if (start[i] - '0' != 1 + start[i - 1] - '0')
                {
                    // if not increament the starting digit as we cannot start with given 1st digit
                    cur[0] = 1 + start[0] - '0';
                    break;
                }

            while (cur.Length <= end.Length)                // O(k)
            {
                // add all possible combinations
                var digit = cur[0];

                while (digit <= 9)                          // O(9)
                {
                    // no points in starting with a digit so big that we run out of sequential digits by the last index
                    if (digit + cur.Length > 10) break;

                    bool digitCheckRequired = cur.Length == end.Length;

                    // assign digits
                    for (int i = 0; i < cur.Length; i++)    // O(9)
                        // no check required
                        if (!digitCheckRequired)
                            cur[i] = digit + i;
                        else // check required
                        {

                            if (digit + i < end[i] - '0')
                            {   // since a smaller digit found earlier further digit check not needed
                                digitCheckRequired = false;
                                cur[i] = digit + i;
                            }
                            else if (digit + i == end[i] - '0')
                                cur[i] = digit + i;
                            else return ans;
                        }

                    // create the current number
                    var num = 0;
                    foreach (var d in cur)                  // O(9)
                        num = num * 10 + d;

                    // add to final list
                    ans.Add(num);

                    // increment starting digit for next iteration of num with same total no of digits
                    digit++;
                }

                // increament the no of digits in current by 1
                cur = new int[cur.Length + 1];
                // and assigned 1 as starting digit
                cur[0] = 1;
            }
            return ans;
        }

        // Time = Space = O(1)
        public static IList<int> SequentialDigits_Alternate(int low, int high)
        {
            return (new[]
            {
                12,
                23,
                34,
                45,
                56,
                67,
                78,
                89,
                123,
                234,
                345,
                456,
                567,
                678,
                789,
                1234,
                2345,
                3456,
                4567,
                5678,
                6789,
                12345,
                23456,
                34567,
                45678,
                56789,
                123456,
                234567,
                345678,
                456789,
                1234567,
                2345678,
                3456789,
                12345678,
                23456789,
                123456789
            }).Where(x => x >= low && x <= high).ToList();
        }


        // Time O(n^2) | Space O(n) | TLE
        public static long MaximumSubarraySum(int[] nums, int k)
        {
            long maxGood = long.MinValue, curSum;
            long[] prefixSum = new long[nums.Length + 1];
            Pair<int, int>[] numIdx = new Pair<int, int>[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                prefixSum[i + 1] = prefixSum[i] + nums[i];
                numIdx[i] = new(nums[i], i);
            }
            var sorted = (from pair in numIdx
                          orderby pair.key
                          select pair).ToArray();

            for (int start = 0; start < nums.Length; start++)
                for (int last = start + 1; last < nums.Length; last++)
                {
                    long diff = sorted[last].key - sorted[start].key;
                    if (diff < k) continue;
                    else if (diff == k)
                    {
                        int i1 = sorted[last].val, i2 = sorted[start].val;
                        curSum = i1 > i2 ? prefixSum[i1 + 1] - prefixSum[i2] : prefixSum[i2 + 1] - prefixSum[i1];
                        if (curSum > maxGood)
                            maxGood = curSum;
                    }
                    else if (diff > k)
                        break;
                }

            return maxGood != long.MinValue ? maxGood : 0;
        }

        // Time = Space = O(n), n = length of 'nums'
        public static long MaximumSubarraySum_Faster(int[] nums, int k)
        {
            long maxGood = long.MinValue;
            long[] prefixSum = new long[nums.Length + 1];
            Dictionary<int, List<int>> numIdx = new();

            for (int i = 0; i < nums.Length; i++)
            {
                // calculate prefix sum
                prefixSum[i + 1] = prefixSum[i] + nums[i];

                // store num as key and indicies is shows up as Value in Dictionary/HashTable
                if (numIdx.ContainsKey(nums[i]))
                    numIdx[nums[i]].Add(i);
                else
                    numIdx[nums[i]] = new List<int>() { i };

                long plusK = nums[i] + k;
                long minusK = nums[i] - k;
                // if(plusK<=int.MaxValue)
                if (numIdx.ContainsKey((int)plusK))
                    foreach (var idx in numIdx[(int)plusK])
                        maxGood = Math.Max(maxGood, idx > i ? prefixSum[idx + 1] - prefixSum[i] : prefixSum[i + 1] - prefixSum[idx]);
                // if(minusK>=int.MinValue)
                if (numIdx.ContainsKey((int)minusK))
                    foreach (var idx in numIdx[(int)minusK])
                        maxGood = Math.Max(maxGood, idx > i ? prefixSum[idx + 1] - prefixSum[i] : prefixSum[i + 1] - prefixSum[idx]);
            }
            return maxGood != long.MinValue ? maxGood : 0;
        }

        // Time O(n^3) | Space O(1)
        public static int NumberOfPairs(int[][] points)
        {
            // sort points in increasing order of x-axis if 2 points have same x axis one with higher y-axis gets priority and will show up before 1 with lower y axis
            Array.Sort(points, (p1, p2) => p1[0] != p2[0] ? p1[0].CompareTo(p2[0]) : p2[1].CompareTo(p1[1]));
            int waysToPlace = 0;
            for (int i = 0; i < points.Length; i++)
                for (int j = i + 1; j < points.Length; j++)
                    if (points[i][1] >= points[j][1])
                    {
                        bool foundMiddle = false;
                        for (int k = i + 1; k < j; k++)
                            if ((points[i][0] <= points[k][0] && points[k][0] <= points[j][0])
                               && (points[i][1] >= points[k][1] && points[k][1] >= points[j][1]))
                            {
                                foundMiddle = true;
                                break;
                            }

                        if (!foundMiddle) waysToPlace++;
                    }
            return waysToPlace;
        }


        // BRUTE FORCE Time O(n^2) | Space O(1), n =  length of nums | TLE
        public static int CountRangeSumBruteForce(int[] nums, int lower, int upper)
        {
            int count = 0, l = nums.Length;
            long rangeSum;
            for (int start = 0; start < l; start++)
            {
                rangeSum = 0;
                for (int end = start; end < l; end++)
                {
                    rangeSum += nums[end];
                    if (lower <= rangeSum && rangeSum <= upper)
                        count++;
                }
            }
            return count;
        }
        // Time O(nlogn) | Space O(n), n =  length of nums
        public static int CountRangeSum(int[] nums, int lower, int upper)
        {
            int l = nums.Length;
            long[] prefixSum = new long[l + 1];
            for (int i = 0; i < l; i++)
                prefixSum[i + 1] = prefixSum[i] + nums[i];

            return CountMerge(0, l + 1);       // nlogn

            // local herlper func
            int CountMerge(int start, int end)
            {
                if (end - start <= 1) return 0;
                int mid = (start + end) >> 1;
                long[] sorted = new long[end - start];
                int count = CountMerge(start, mid) + CountMerge(mid, end);
                int j = mid, k = mid, t = mid;
                for (int i = start, r = 0; i < mid; i++, r++)
                {
                    while (k < end && prefixSum[k] - prefixSum[i] < lower)
                        k++;
                    while (j < end && prefixSum[j] - prefixSum[i] <= upper)
                        j++;
                    while (t < end && prefixSum[t] < prefixSum[i])
                        sorted[r++] = prefixSum[t++];
                    sorted[r] = prefixSum[i];
                    count += j - k;
                }
                Array.Copy(sorted, 0, prefixSum, start, t - start);
                return count;
            }
        }


        // Time O(n) | Space O(1)
        public static bool IsSelfCrossing(int[] x)
        {
            /* ALGO there are only 3 diff ways a line will cross another line
            Case1
            _________
            |       |
            |       |
            |_______|____  ----> itersection with n-3
                    |
            Case4
            _________
            |       |
            |       |
            |       |  ----> itersection with n-4
            |_______|

            Case3
            _________
            |    ___|___________ ------> intersection with n-5
            |       |          |
            |       |          | 
            |__________________|
            */
            // compare each point at idx n with n-3, n-4 & n-5 points and see if it crosses them than return true
            for (int i = 3; i < x.Length; i++)
            {
                // case 1# n-3
                if (x[i] >= x[i - 2] && x[i - 1] <= x[i - 3])
                    return true;
                // case 2# n-4
                if (i >= 4)
                    if (x[i] + x[i - 4] == x[i - 2] && x[i - 1] == x[i - 3])
                        return true;
                // case 3# n-5
                if (i >= 5)
                    if (x[i] + x[i - 4] >= x[i - 2] && x[i - 1] + x[i - 5] >= x[i - 3] && x[i - 2] > x[i - 4] && x[i - 3] > x[i - 1])
                        return true;
            }
            return false;
        }


        // Time = Space = O(n)
        public static int NumSquares(int n, Dictionary<int, int> minPerfectSq)
        {
            /* ALGO
            1. Have a cache to store the soln of minPrefect sq which sum up to current no
            2. Now for each no which is not present in cache set the max ans to number itself as 1 is perfect * no makes the ans
            3. Now there r 3 cases:
            -  no is divisible by 2, check if half of current return min ans if yes our ans is 2*minPerfectSq to build half
            - similar if no divisible by 3, check if third of current return min ans if yes our ans is 3*minPerfectSq to build third
            - Now check all possible no b/w no-1...2 if any no is perf sq our ans => 1 + minPerfSq to make remaining number
            - at end save the result in cache and return
             */
            // if present in cache return from there
            if (minPerfectSq.TryGetValue(n, out int minSq)) return minSq;

            int ans = n;    // default value

            // check if current no is a square of another no if yes than min sq to make n is 1
            int root = (int)Math.Sqrt(n);
            if (root * root == n)
                ans = 1;
            else
            {
                // divisible bt 2
                if (n % 2 == 0)
                    ans = Math.Min(ans, 2 * NumSquares(n >> 1, minPerfectSq));
                // divisible by 3
                if (n % 3 == 0)
                    ans = Math.Min(ans, 3 * NumSquares(n / 3, minPerfectSq));
                // try all possible combinations
                for (int cur = n - 1; cur > 1; cur--)
                {
                    root = (int)Math.Sqrt(cur);
                    if (root * root == cur)  // we got a break-up which is a perfect square
                        ans = Math.Min(ans, 1 + NumSquares(n - cur, minPerfectSq));
                }
            }
            return minPerfectSq[n] = ans;
        }


        // Time O(n^2) | Space O(n), n = length of 'nums'
        public static List<int> LargestDivisibleSubset(int[] nums)
        {
            Array.Sort(nums);   // O(nlogn)
            int l = nums.Length;
            List<int>[] cache = new List<int>[l];
            return DFS(0, 1);

            // local helper func
            List<int> DFS(int idx, int lastNum)         // O(n^2)
            {
                if (idx >= l) return new List<int>();

                List<int> afterTaking = new();
                // take curr no (if mod with last is 0)
                if (nums[idx] % lastNum == 0)
                    if (cache[idx] != null)
                        afterTaking = new List<int>(cache[idx]);
                    else
                    {
                        afterTaking.Add(nums[idx]);
                        afterTaking.AddRange(DFS(idx + 1, nums[idx]));
                        // update in cache
                        cache[idx] = new List<int>(afterTaking);
                    }
                // skip curr num
                var withoutTaking = DFS(idx + 1, lastNum);
                return afterTaking.Count > withoutTaking.Count ? afterTaking : withoutTaking;
            }
        }


        // Time = Space = O(n), n = length of 'nums'
        public static int[] RearrangeArray(int[] nums)
        {
            /* ALGO
            1. Set pos to 0 and neg to 1 to denote the where each type of num is to be placed in result array
            2. Iterate thru all the indicies in orignal array
            3.  if num is +ve add the num in result array and increament pos idx by 2
            4. else add num in result array and increament neg idx by 2
             */
            int pos = 0, neg = 1, l = nums.Length;
            int[] result = new int[l];
            for (int i = 0; i < l; i++)
                if (nums[i] > 0)   // +ve
                {
                    result[pos] = nums[i];
                    pos += 2;
                }
                else            // -ve
                {
                    result[neg] = nums[i];
                    neg += 2;
                }
            return result;
            /* 2 Pass Soln | Time = Space = O(n), n = length of 'nums'
                int l = nums.Length, n = 0, j = 0;
                int[] result = new int[l];
                // update +ve values
                while(n<l-1)                // O(n) 
                {
                    // get next +ve idx in original array
                    while(nums[j]<0)
                        j++;
                    // assign +ve value in new array
                    result[n] = nums[j++];
                    n+=2;
                }
                n=1;
                j=0;
                // update -ve values
                while(n<l)                  // O(n) 
                {
                    // get next -ve idx in original array
                    while(nums[j]>0)
                        j++;
                    // assign -ve value in new array
                    result[n] = nums[j++];
                    n+=2;
                }
                return result;
            */
        }


        // Time O(nlogn + mlogm)  | Space O(m), m = length of 'meetings'
        public static int MeetingRoomsIII(int n, int[][] meetings)
        {
            /* ALGO
            Add all meeting to Delayed PQ
            add all rooms to free PQ
            while delayed meeting > 0
                a. take the the startTime of top
                b. all meetings which end before current meeting start gets added to free list
                c. if free room > 0 
                    assign smallest roomID to current meeting and all the time when this meeting will get free
                d. no free rooms avaliable
                    extract the roomID of first meeting which will end and assign that room to cur meeting also add the time when this meeting will get free
            */

            // minHeap to store the list of free/unsed rooms (roomID as Key & Priority)
            PriorityQueue<int, int> freeRooms = new();
            for (int i = 0; i < n; i++)                                        // O(nlogn)
                freeRooms.Enqueue(i, i);

            // minHeap to store the list of delayed meetings (totalTime as Key and startTime as Priority)
            PriorityQueue<int, long> delayedMeeting = new();
            for (int i = 0; i < meetings.Length; i++)                          // O(m)
            {
                int startTime = meetings[i][0], totalMeetingTime = meetings[i][1] - meetings[i][0];
                delayedMeeting.Enqueue(totalMeetingTime, startTime);     // O(logm)
            }

            // minHeap to track the time when a roomID gets free (roomID as Key and int[] { endTime, roomID } as Priority)
            // (sorted by meeting end time, if 2 meetings end at same one with smaller roomID gets precedence)
            PriorityQueue<int, long[]> whenRoomGetsFree = new PriorityQueue<int, long[]>(Comparer<long[]>.Create((a, b) => a[0] != b[0] ? a[0].CompareTo(b[0]) : a[1].CompareTo(b[1])));

            // Array to track no of time a room is used
            int[] timesUsed = new int[n];

            // iterate thru all the meetings and assign the rooms
            while (delayedMeeting.TryDequeue(out int totalMeetingTime, out long curMeetingStartTime))     // O(mlogm)
            {
                // check to see which all meetings have finished and if their room can be added back to free list
                while (whenRoomGetsFree.TryPeek(out int freeRoomID, out long[] endIDRoomID))
                {
                    var endTime = endIDRoomID[0];
                    if (endTime <= curMeetingStartTime)    // if room getting free before current meeting, assign to free room list
                        freeRooms.Enqueue(freeRoomID, whenRoomGetsFree.Dequeue());
                    else break;
                }

                // if room avaliable assign the room with smallest ID first
                // public bool TryDequeue (out TElement element, out TPriority priority);
                if (freeRooms.TryDequeue(out int roomId, out int priority))
                {
                    // increament the Counter
                    timesUsed[roomId]++;
                    // update the 'whenRoomGetsFree' PQ
                    whenRoomGetsFree.Enqueue(roomId, new long[] { curMeetingStartTime + totalMeetingTime, roomId });
                }
                // no room is free to be assigned yet
                else
                {
                    // take the time of first meetings which gets free
                    whenRoomGetsFree.TryDequeue(out int nextFreeRoomID, out long[] nextEndIDRoomID);
                    // increament the Counter
                    timesUsed[nextFreeRoomID]++;
                    // update the 'whenRoomGetsFree' PQ with time from when this room gets free
                    var endTime = nextEndIDRoomID[0];
                    whenRoomGetsFree.Enqueue(nextFreeRoomID, new long[] { endTime + totalMeetingTime, nextFreeRoomID });
                }
            }

            // return the room which is used most (if multiple return the idx of room which is smaller)
            int mostUsed = 0, roomIdx = -1;
            for (int i = 0; i < n; i++)
                if (timesUsed[i] > mostUsed)
                {
                    mostUsed = timesUsed[i];
                    roomIdx = i;
                }
            return roomIdx;
        }


        // Time = Space = O(r), r = length of 'rectangles'
        public static bool IsRectangleCover(int[][] rectangles)
        {
            /* ALGO
            1. Create a HashSet to hold all 4 points of each rectangle
            2. Initialize a TotalAreaOfAllRectangle which keeps on adding area for each rectangle we traverse
            3. Now while iterating thru each rect all 4 points, we see if any point is
                already present in the HashSet remove it as we don't want duplicate points
                not present add to the HashSet
                (if you see in the figure of the example we can figure all points except
                the points of the perfect rectangle would occur twice, this way we can filter all points except extreme ones)
            4. At return true if set has excatly 4 poins and area of those points == TotalAreaOfAllRectangle
            */
            HashSet<string> set = new();
            int x1 = int.MaxValue, x2 = int.MinValue, y1 = int.MaxValue, y2 = int.MinValue;
            long area = 0;
            foreach (var r in rectangles)                // O(n)
            {
                // calculate the extreme 4 points of final perfect rectangle
                x1 = Math.Min(x1, r[0]);
                x2 = Math.Max(x2, r[2]);
                y1 = Math.Min(y1, r[1]);
                y2 = Math.Max(y2, r[3]);

                // extract all corner of the rectangle
                string leftBottom = r[0] + "," + r[1],
                leftTop = r[0] + "," + r[3],
                rtBottom = r[2] + "," + r[1],
                rtTop = r[2] + "," + r[3];

                // add the area of cur rectangle to the total
                // Rect Area = (a-x) * (b-y)
                area += (r[2] - r[0]) * (r[3] - r[1]);

                // if points is not present add to the Set if already present remove from the set
                if (set.Contains(leftBottom)) set.Remove(leftBottom);
                else set.Add(leftBottom);
                if (set.Contains(leftTop)) set.Remove(leftTop);
                else set.Add(leftTop);
                if (set.Contains(rtBottom)) set.Remove(rtBottom);
                else set.Add(rtBottom);
                if (set.Contains(rtTop)) set.Remove(rtTop);
                else set.Add(rtTop);
            }
            // set has only 4 unq points left and the area of perfect rectangle matches sum of total area of each rectangle
            // and all 4 final points of perfect rectangle are present in the set
            return set.Count == 4 && area == (x2 - x1) * (y2 - y1)
            && set.Contains(x1 + "," + y1) && set.Contains(x1 + "," + y2) && set.Contains(x2 + "," + y1) && set.Contains(x2 + "," + y2);
        }


        // Time = Space = O(1), since at max we are going to run the loop 32 times
        public static int RangeBitwiseAnd(int left, int right)
        {
            /* ALGO
            1. First bit n.e. right most bit of either num 'a' or 'a+1' one of them atleast have 0 as 1st bit
                hence AND of that bits will result in 0 no matter what numbers come after
            2. Using above approach we can just see if the diff b/w left and right most numbers is atleast 1
                there is bound to be a numbers in the range that will have 1st bit as zero
                hence the final AND for the bit will be always zero
            3. We keep applying above approach and right shift left and right most num by 1 bit till left!=right
            4. As soon as they match it means from this point onwards whatever is to the left
                will remain same in the final ans
            5. Hence just left-shifting the remaing matching number
                by no of iteration or bits we have evaluated to 0 will result in final ans
             */
            int ithBitFromRight = 0;
            while (left != right)
            {
                // since left and right don't match the 1st bit will definatly have 0 once or more in the range left..right
                left >>= 1;
                right >>= 1;
                // since 1st bit is certain to be 0 we right shift both nums by 1 position to match them again
                ithBitFromRight++;
            }
            return left << ithBitFromRight;
        }


        // Time O(nlogMAX) | Space O(n), n = length of 'nums', MAX = 100000
        public static bool CanTraverseAllPairs(int[] nums)
        {
            /* ALGO
            Handle the edge cases, if n = 1, return true, if nums[n] = 1, return false.
            Create an Graph which will store the Node for all unique values & primeFactor (except 1)
            For each element nums[n], iterate over all its prime factors, and for each prime factor add an undirected edge between num and prime
            Also we keep adding all the nodes we create in graph to NotVisited set which is used later to find if its 1 connected graph
            Run DFS from any value ex: 1st value in num to count the number of components.
            Return true if the graph has one component, and false otherwise.
             */
            int l = nums.Length, min = nums.Min();
            if (l == 1) return true;   // only 1 num in input (hence connected graph)
            if (min == 1) return false;// there wud be atleast 1 node which won't be connected to anything since GCD of 1 is only 1 which we are not considering as given in problem

            Dictionary<int, List<int>> g = new();
            HashSet<int> NotVisited = new();
            // add PrimeFactor as dummyNode and connect it to the num is divides
            for (int i = 0; i < l; i++)                            // O(n)
                                                                   // cal PrimeFactor only for unq numbers
                if (!g.ContainsKey(nums[i]))
                {
                    g[nums[i]] = new();
                    NotVisited.Add(nums[i]);
                    int prime = 2, n = nums[i];
                    // prime factor algo
                    while (prime * prime <= n)                   // O(logMAX)
                    {
                        if (n % prime == 0)
                        {
                            // prime to num edge
                            if (g.TryGetValue(prime, out List<int> dividesNums))
                                dividesNums.Add(nums[i]);
                            else g[prime] = new List<int>() { nums[i] };

                            // num to prime edge
                            g[nums[i]].Add(prime);

                            // add primeNode to not visited Set
                            NotVisited.Add(prime);

                            // keep reducing the number till just added prime number divides it perfectly (so its not duplicated)
                            while (n % prime == 0)
                                n /= prime;
                        }
                        prime++;
                    }
                    // add the last largest prime which is not 1 or
                    // the num itself as node for the num is already added
                    if (n > 1 && n != nums[i])
                    {
                        // prime to num edge
                        if (g.TryGetValue(n, out List<int> dividesNums))
                            dividesNums.Add(nums[i]);
                        else g[n] = new List<int>() { nums[i] };

                        // num to prime edge
                        g[nums[i]].Add(n);

                        // add primeNode to not visited Set
                        NotVisited.Add(n);
                    }
                }
            // run DFS from any node ex: 1st numbers and it should be able to visit all nodes if its 1 connected graph
            DFS(nums[0]);                                   // O(n)
            return NotVisited.Count == 0;


            // local helper func
            void DFS(int node)
            {
                // new node Not visited yet
                if (NotVisited.Contains(node))
                {
                    NotVisited.Remove(node); // remove from set now
                    foreach (var adjNode in g[node])
                        DFS(adjNode);
                }
            }
        }


        // Time = Space = O(n), n = no of nodes in tree
        public static int FindBottomLeftValue(TreeNode root)
        {
            /* Algo 
            1. Create Queue which will node elements of type TreeNode
            2. leftMost stores the left most node we have seen till now
            3. isFirstNodeOfNextLevel tells whether the next element we traverse
                is going to be the leftMost or not or next level
            4. Add root and than Null to the queue
            5. a Null marks cur level traversal is complete
            6. Iterate thru the queue and keep on adding left n right child if not null
            7. once we get null if there are still elements left in queue n.e.
                next level than add Null pointer which will mark end of next level
            9. reset 'isFirstNodeOfNextLevel' so leftmost is captured
             */
            int leftMost = root.val;
            bool isFirstNodeOfNextLevel = false;
            Queue<TreeNode> q = new();
            q.Enqueue(root);
            q.Enqueue(null);
            while (q.TryDequeue(out TreeNode cur))
                if (cur == null)
                {
                    if (q.Count > 0)
                        q.Enqueue(null);
                    isFirstNodeOfNextLevel = true;
                }
                else
                {
                    if (isFirstNodeOfNextLevel)
                    {
                        leftMost = cur.val;
                        isFirstNodeOfNextLevel = false;
                    }
                    if (cur.left != null) q.Enqueue(cur.left);
                    if (cur.right != null) q.Enqueue(cur.right);
                }
            return leftMost;
        }


        // Time = Space = O(n), n = no of nodes in 'Binary Tree'
        public static bool IsEvenOddTree(TreeNode root)
        {
            List<TreeNode> curLevel = [root], nextLevel = [];
            bool evenIndexedLevel = true;
            TreeNode lastVal;

            while (curLevel.Count > 0)
            {
                lastVal = curLevel[0];
                // add left child to next level
                if (lastVal.left != null) nextLevel.Add(lastVal.left);
                // add right child to next level
                if (lastVal.right != null) nextLevel.Add(lastVal.right);

                
                if (evenIndexedLevel)
                {
                    if (lastVal.val % 2 == 0) return false;  // 1st value not odd
                    for (int i = 1; i < curLevel.Count; i++)
                    {
                        // all nums should be odd & increasing value
                        if (curLevel[i].val % 2 == 0 || lastVal.val >= curLevel[i].val) return false;
                        else lastVal = curLevel[i];

                        // add left child to next level
                        if (curLevel[i].left != null) nextLevel.Add(curLevel[i].left);
                        // add right child to next level
                        if (curLevel[i].right != null) nextLevel.Add(curLevel[i].right);
                    }
                }
                else
                {
                    if (lastVal.val % 2 == 1) return false;  // 1st value not even
                    for (int i = 1; i < curLevel.Count; i++)
                    {
                        // all nums should be even & decreasing value
                        if (curLevel[i].val % 2 == 1 || lastVal.val <= curLevel[i].val) return false;
                        else lastVal = curLevel[i];

                        // add left child to next level
                        if (curLevel[i].left != null) nextLevel.Add(curLevel[i].left);
                        // add right child to next level
                        if (curLevel[i].right != null) nextLevel.Add(curLevel[i].right);
                    }
                }

                // update next level onto current
                curLevel = nextLevel;
                // reset next level
                nextLevel = new();
                // switch 'evenIndexedLevel' boolean
                evenIndexedLevel = !evenIndexedLevel;
            }
            return true;
        }


        // Time O(nlogn) Space O(1), n = length of 'tokens'
        public static int BagOfTokensScore(int[] tokens, int curPower)
        {
            /* ALGO
            1. Sort the array
            2. now our aim is to increase the score and return the max we can get at any point in time
            3. we set the boundery to lt = 0 and rt = n-1
            4. if we can increase the score n.e. smallest token which is on left is <= curPower, increase the token+1 and reduce power, update max
            5. if above not possible try to max power which is on the right most side by sacrificing 1 token, if we have atleast 1
            6. return the maxToken
             */
            Array.Sort(tokens);                     // O(nlogn)
            int lt = 0, rt = tokens.Length - 1, curToken = 0, maxToken = 0;
            while (lt <= rt)                           // O(n)
                                                       // increase score if we can
                if (tokens[lt] <= curPower)
                {
                    // increament the token and update the max seen so far
                    maxToken = Math.Max(maxToken, ++curToken);
                    // reduce the power and move the lt pointer frwd
                    curPower -= tokens[lt++];
                }
                else if (curToken > 0)
                {
                    // increament the power and move the rt pointer -1
                    curPower += tokens[rt--];
                    // reduce the token count by 1
                    --curToken;
                }
                else break;
            return maxToken;
        }


        // Time O(n) Space O(1), n = length of 's'
        public static int MinimumLengthStr(string s)
        {
            /* ALGO
            1. For each matching left and right most character remove as many characters from both end as possible
            2. At the end return the distance b/w rt and lt pointer
             */
            int lt = 0, rt = s.Length - 1;
            while (lt < rt && s[lt] == s[rt])
            {
                char cur = s[lt];
                while (lt < rt && s[lt] == cur) lt++;
                while (lt <= rt && s[rt] == cur) rt--;
            }
            return 1 + rt - lt;
        }


        // Time O(r*c*uniqHeights) | Space O(r*c), r & c = no of rows and cols respectively, uniqHeights = 20000
        public static int TrapRainWaterII(int[][] heightMap)
        {
            /* ALGO
            1. Identify all the cells which can collected water and those from where waters falls out ulitmately via any of 4 edges
            2. Base case: Neither of the boundry cells cannot collect water
            
            3. Now group cells which can hold water as value in a Dictionary with cell height as the key
            4. Get the list of uniq height sorted in ascending order from above Dictionary Keys
            5. Now starting with the cell which has the minimum ht look in all 4 direction
                a. see a wall or another cell with different ht update => 'smallestBoundryWallHeight'
                b. cell of same ht, run DFS on it too if not already visited and add it to the list of 'recentlyVisitedCells'
            6. using 'recentlyVisitedCells' list we know which cells can be filled to next closed wall/cell
                hence use the diff for each cell with 'smallestBoundryWallHeight' to calculate water it can store currently
                Note: [this cell might collect more water which wud be calculated when we are iterating thru next ht]
            7. Also remember to add cell with increase ht back to the dictionary 'sameHeightCells' to it can be used again
            8. Update update grand total 'totalWaterCollected'

            by following step#5 we keep increasing the height of each cell till the point water collection is no longer possible for it
            */
            int rows = heightMap.Length, cols = heightMap[0].Length, totalWaterCollected = 0;
            int[][] direction = [[-1, 0], [1, 0], [0, -1], [0, 1]];
            bool[,] canCollect = new bool[rows, cols];
            bool[,] visited = new bool[rows, cols];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    canCollect[r, c] = true;

            Queue<int[]> q = new();
            // mark all boundry as Cannot collect water and add these Cells to Queue which cannot hold water
            for (int r = 0; r < rows; r++) // left and right most column
            {
                canCollect[r, 0] = canCollect[r, cols - 1] = false;
                visited[r, 0] = visited[r, cols - 1] = true;
                q.Enqueue([r, 0]);
                q.Enqueue([r, cols - 1]);
            }
            for (int c = 1; c < cols - 1; c++) // 1st and last row
            {
                canCollect[0, c] = canCollect[rows - 1, c] = false;
                visited[0, c] = visited[rows - 1, c] = true;
                q.Enqueue([0, c]);
                q.Enqueue([rows - 1, c]);
            }

            // identify cells which can hold water
            while (q.TryDequeue(out int[] cur))
            {
                int r = cur[0], c = cur[1];
                // if(visited[r,c]) continue;
                // visited[r,c] = true;

                // check if any of 4 cells has higher ht means water will flow for it too via cur cell as its neighbour
                foreach (var dir in direction)
                {
                    int adj_rID = r + dir[0], adj_cID = c + dir[1];
                    if (ValidCell(adj_rID, adj_cID) && heightMap[adj_rID][adj_cID] >= heightMap[r][c] && !visited[adj_rID, adj_cID])
                    {
                        canCollect[adj_rID, adj_cID] = false;
                        visited[adj_rID, adj_cID] = true;
                        q.Enqueue([adj_rID, adj_cID]);
                    }
                }
            }


            // grp all the cells that can hold water by their heights
            Dictionary<int, List<int[]>> sameHeightCells = [];
            for (int r = 1; r < rows - 1; r++)
                for (int c = 1; c < cols - 1; c++)
                    if (canCollect[r, c])
                    {
                        var ht = heightMap[r][c];
                        // current height is already present, add new cell as value to key with given ht
                        if (sameHeightCells.TryGetValue(ht, out List<int[]> val))
                            val.Add([r, c]);
                        else sameHeightCells[ht] = new List<int[]>() { new int[] { r, c } };
                    }
            // get all the unique heights in ascending order
            var unqHeights = (from kvp in sameHeightCells
                                       orderby kvp.Key
                                       select kvp.Key).ToArray();


            int smallestBoundryWallHeight, waterTrappedInCurPond = 0;
            List<int[]> recentlyVisitedCells = null;
            foreach (var ht in unqHeights)
            {
                // reset visited
                visited = new bool[rows, cols];

                foreach (var cell in sameHeightCells[ht].ToList())
                {
                    recentlyVisitedCells = [];
                    smallestBoundryWallHeight = int.MaxValue;
                    int r = cell[0], c = cell[1];
                    // not already visited
                    if (!visited[r, c])
                        // we calculate the smallestWall surrounding current cell and any adjacent cell of same ht only
                        FindSameHtAdjCells(r, c);   // run DFS to find cells in same pond

                    // Skip as we don't want to add -ve water
                    if (smallestBoundryWallHeight <= heightMap[r][c]) continue;

                    waterTrappedInCurPond = 0;
                    // Fill the cell(s) just iterated to the height of next neareset cell or wall
                    foreach(var sameHtCell in recentlyVisitedCells)
                    {

                        int r1 = sameHtCell[0], c1 = sameHtCell[1];
                        //Console.WriteLine($"Water trapped at [{r1},{c1}] which has ht {heightMap[r1][c1]}: {smallestBoundryWallHeight - heightMap[r1][c1]}");

                        // calculate water trapped by current cell
                        waterTrappedInCurPond += smallestBoundryWallHeight - heightMap[r1][c1];
                        // fill to next boundry
                        heightMap[r1][c1] = smallestBoundryWallHeight;

                        // if new height is already present, add cur cell to same list
                        if (sameHeightCells.TryGetValue(smallestBoundryWallHeight, out List<int[]> val))
                            val.Add([r1, c1]);
                    }
                    // update grand total
                    totalWaterCollected += waterTrappedInCurPond;
                }
            }
            return totalWaterCollected;

            // local helper func
            bool ValidCell(int r, int c) => r >= 0 && r < rows && c >= 0 && c < cols;

            void FindSameHtAdjCells(int r, int c)               // DFS
            {
                if (!visited[r, c])
                {
                    // mark cur cell as visited
                    visited[r, c] = true;
                    recentlyVisitedCells.Add([r, c]);

                    // look in all 4 direct and update boundry
                    foreach (var dir in direction)
                    {
                        int r1 = r + dir[0], c1 = c + dir[1];
                        // if another adjacent cell of same height
                        if (canCollect[r1, c1] && heightMap[r1][c1] == heightMap[r][c])
                            FindSameHtAdjCells(r1, c1);
                        // if boundry found update the wall height
                        else smallestBoundryWallHeight = Math.Min(smallestBoundryWallHeight, heightMap[r1][c1]);
                    }
                }
            }
        }


        // Time = Space = O(n), n = no of nodes in LinkedList
        public static ListNode RemoveZeroSumSublists(ListNode cur)
        {
            /* ALGO
            1.Add a DummyNode at start
            2. Create a Dictionary<int,ListNode> to store the preFixSumMap seen so far and the node at which it was seen
            3. We keep adding each node value to int 'sumSoFar' till and check if it's present in Dictionary/Hashtable
                if present // now deletion starts
                a. initialize 'deleteFrom' pointer to next value of node who's preFix matched with 'sumSoFar'
                b. Initialize 'deletePrefixSum' with startingFrom.value as delete will happen after this node
                c. add 'deleteFrom' node value to 'deletePrefixSum'
                d. remove 'deletePrefixSum' from dictionary // marking deletion of that node
                e. repeat above steps till deleteFrom!=cur
                f. at last assigned the startingFrom.next to cur.next as even cur Node wud be deleted
            4. move cur to cur.next
            5. return dummy.next
            */

            ListNode dummy = new ListNode(0), deleteNode;
            dummy.next = cur;
            // remove all 0 value nodes
            cur = dummy;
            while(cur.next!=null)
            {
                if(cur.next.val==0)
                    cur.next = cur.next.next;
                else cur = cur.next;
            }
            Dictionary<int, ListNode> preFixSumMap = new();
            cur = dummy;
            int sumSoFar = 0, deletePrefixSum = 0;
            while (cur != null)                // O(n)
            {
                sumSoFar += cur.val;
                if (preFixSumMap.TryGetValue(sumSoFar, out ListNode startingFrom))
                {
                    deleteNode = startingFrom.next;
                    deletePrefixSum = sumSoFar;
                    while (deleteNode != cur)
                    {
                        deletePrefixSum += deleteNode.val;
                        preFixSumMap.Remove(deletePrefixSum);
                        deleteNode = deleteNode.next;
                    }
                    startingFrom.next = cur.next;
                }
                else preFixSumMap[sumSoFar] = cur;

                cur = cur.next;
            }
            return dummy.next;
        }


        // Time = Space = O(n) | Linear Soln
        public static int PivotInteger_Linear(int n)
        {
            int[] prefixSum = new int[n + 1];
            for (int i = 1; i <= n; i++)            // O(n)
                prefixSum[i] = prefixSum[i - 1] + (i);
            int lt = 1, rt = n, mid, ltSum, rtSum;
            // find the pivot element
            while (lt <= rt)                        // O(logn)
            {
                mid = (lt + rt) / 2;
                ltSum = prefixSum[mid];
                rtSum = prefixSum[n] - prefixSum[mid - 1];

                if (ltSum == rtSum) return mid;
                else if (ltSum < rtSum) lt = mid + 1;
                else rt = mid - 1;
            }
            return -1;
        }
        // Time O(logn) | Space = O(1) | Logarithmic Soln
        public static int PivotInteger_Logarithmic(int n)
        {
            /* ALGO
            1. We need to have a way to quickly check for any no what is the sum of its left half and right half, pivot being inclusive in both sections
            2. You can use prefixSum also for each pivot b/w 1..N
                but using Maths formula => n*(n+1)/2 is bttr/faster
                as it eleminates the need to traverse array
            3. Now use binary search to check all the pivot points b/w 1..N
            4. if ltSum==rtSum return pivot
            5. else if ltSum < rtSum, move lt pointer = mid+1
            6. else move rt pointer to mid-1;
            7. If no ans found return -1 at the end.
             */
            int lt = 1, rt = n, mid, ltSum, rtSum, totalSum = ComputeSum(n);
            // find the pivot element
            while (lt <= rt)                        // O(logn)
            {
                mid = (lt + rt) / 2;
                ltSum = ComputeSum(mid);
                rtSum = totalSum - ComputeSum(mid - 1);

                if (ltSum == rtSum) return mid;
                else if (ltSum < rtSum) lt = mid + 1;
                else rt = mid - 1;
            }
            return -1;

            // local helper func
            static int ComputeSum(int n) => n * (n + 1) / 2;
        }


        // Time = Space = O(n), n = length of nums
        public static int NumSubarraysWithSum(int[] nums, int goal)
        {
            Dictionary<int, int> sumFreq = [];
            int curSum = 0, subArrayCount = 0;
            for (int i = 0; i < nums.Length; i++)      // O(n)
            {
                curSum += nums[i];
                if (curSum == goal) subArrayCount++;
                if (sumFreq.TryGetValue(curSum - goal, out int counter))
                    subArrayCount += counter;

                if (sumFreq.TryGetValue(curSum, out int freqOfCurSum)) sumFreq[curSum] = 1 + freqOfCurSum;
                else sumFreq[curSum] = 1;
            }
            return subArrayCount;
        }

        // Time = Space = O(n), n = length of 'nums'
        public static int MaxSubarrayLength(int[] nums, int k)
        {
            Dictionary<int, int> numFreq = [];
            int lt = 0, rt = -1, maxGoodLen = 1, l = nums.Length;
            while (++rt < l)               // O(n)
            {
                if (numFreq.TryGetValue(nums[rt], out int freq))
                {
                    numFreq[nums[rt]] = ++freq;
                    // need to move the left pointer to decrease the freq of the number which just breached 'k'
                    while (numFreq[nums[rt]] > k)
                        --numFreq[nums[lt++]];
                }
                else numFreq[nums[rt]] = 1;
                // update the good sub-array length
                maxGoodLen = Math.Max(maxGoodLen, 1 + rt - lt);
            }
            return maxGoodLen;
        }

        // Time O(n), Space O(1), n = length of 'nums'
        public static long CountSubarrays(int[] nums, int k)
        {
            int maxNum = nums.Max(), freqMax = 0, lt = 0, rt = -1, l = nums.Length;
            long countSubArrWithMaxNumAppearsAtleastKTime = 0;
            while (++rt < l)
            {
                // increament the global max number if found at cur idx
                if (nums[rt] == maxNum)
                    freqMax++;

                // move the left pointer till freq == k
                while (freqMax == k)
                    // move left pointer and reduce freq if global max is found
                    freqMax -= nums[lt++] == maxNum ? 1 : 0;

                // update the sub-array count, since we are reducing freq till its k-1 hence we don't need to add lt+1 just adding left pointer works
                countSubArrWithMaxNumAppearsAtleastKTime += lt;
            }
            return countSubArrWithMaxNumAppearsAtleastKTime;

            #region Easy to understand
            // // Time O(n), Space O(1), n = length of 'nums'
            // public long CountSubarrays(int[] nums, int k) {
            //     int maxNum = nums.Max(), freqMax = 0, lt = 0, rt = -1 , l = nums.Length;
            //     long countSubArrWithMaxNumAppearsAtleastKTime = 0;
            //     while(++rt<l)
            //     {
            //         if(nums[rt]==maxNum)
            //             freqMax++;

            //         while(freqMax>k || (freqMax==k && nums[lt]!=maxNum))
            //             freqMax-=nums[lt++] == maxNum ? 1 : 0;

            //         countSubArrWithMaxNumAppearsAtleastKTime += freqMax==k ? lt+1 : 0;
            //     }
            //     return countSubArrWithMaxNumAppearsAtleastKTime;
            // }
            #endregion
        }


        // sliding window with 3 pointers
        // Time = Space = O(n), n = length of 'nums'
        public static int SubarraysWithKDistinct(int[] nums, int k)
        {
            Dictionary<int, int> numFreq = [];
            int ltFar = 0, ltNear = 0, rt = -1, countSubArrayWithKDistinctNums = 0, l = nums.Length;
            while (++rt < l)
            {
                if (numFreq.TryGetValue(nums[rt], out int freq))
                    numFreq[nums[rt]] = 1 + freq;
                else numFreq[nums[rt]] = 1;

                if (numFreq.Count > k)             // O(n)
                {
                    // since we have excess of distinct we keep reducing the window till we have just 'k'
                    while (numFreq.Count > k)
                    {
                        // one number is no longer part of subaray, hence reducing total distinct count to 'K'
                        if (--numFreq[nums[ltNear++]] == 0)
                        {
                            numFreq.Remove(nums[ltNear - 1]);
                            break;
                        }
                    }
                    // since we have just 'k' distinct now which is both the longest and shorted valid subarray set both the left pointers to same idx
                    ltFar = ltNear;
                }

                if (numFreq.Count == k)
                {
                    // try reducing if we can reduce the sliding window to min by removing
                    // duplicate of the cur num just traversed in array
                    while (numFreq[nums[ltNear]] > 1)
                        --numFreq[nums[ltNear++]];

                    // update the valid sub-array count
                    countSubArrayWithKDistinctNums += 1 + ltNear - ltFar;
                }
            }
            return countSubArrayWithKDistinctNums;
        }


        // sliding window with 3 pointers
        // Time = Space = O(n), n = length of 'nums'
        public static long CountSubarraysWithinBound(int[] nums, int minK, int maxK)
        {
            int minIdxQ = -1, maxIdxQ = -1;
            int ltFar = 0, ltNear = 0, rt = -1, l = nums.Length, n;
            long countSubArrayWithBounds = 0;
            while (++rt < l)
            {
                n = nums[rt];
                // out-of-bounds value
                if (n < minK || n > maxK)
                {
                    minIdxQ = maxIdxQ = -1;
                    ltFar = ltNear = rt + 1;
                }
                // within bounds value
                else
                {
                    if (n == minK)     // if found min store its index
                        minIdxQ = rt;
                    if (n == maxK)     // if found max store its index
                        maxIdxQ = rt;

                    ltNear = Math.Min(minIdxQ, maxIdxQ);
                    // if we have both boundry values
                    if (ltNear != -1)
                        // update the valid sub-array count
                        countSubArrayWithBounds += 1 + ltNear - ltFar;
                }
            }
            return countSubArrayWithBounds;
        }


        // Time O(n) 2-Pass | Space O(1), n = length total no of 'students'
        public static int CountStudentsUnableToEatLunch(int[] students, int[] sandwiches)
        {
            /* ALGO
            1. take 2 counters to track no of sandwichs avaliable of each type
            2. Now iterate from 0..N-1 index and keep increamenting the counter when you see a sandwich of that type
            3. Also keep decrementing the counter when a kid who wants a given sandwich shows up
            4. If after iterating once thru the array's the total count of both sandwiches is 0 means we have excatly the amount of sandwich as the studends wants.
            5. If not, we just need to find which type of sandwich is more as sandwich can only be one of the 2 given types
            6. whichever sandwich type is in excess we need to find out how many students have been fed of opp type till now,
                as once we found the excess one, post that students would keep going back to queue.
            7. we keep decreamenting from the total no of students of the opposite/minority type till be reach the excessive sandwich index
            8. return the remaingCount of minority preference which could not be fed once all the students are fed of sandwich type which are in excess.
             */
            int circularSandwichCount = 0, squareSandwichCount = 0, ableToEat = 0, l = students.Length, totalCircularKids = 0, totalSquareKids = 0;
            for (int i = 0; i < l; i++)            // O(n)
            {
                // update sandwich counters
                if (sandwiches[i] == 1) squareSandwichCount++;
                else circularSandwichCount++;

                // reduce the sandwich which is preferred by current student
                if (students[i] == 1)
                {
                    squareSandwichCount--;
                    totalSquareKids++;
                }
                else
                {
                    circularSandwichCount--;
                    totalCircularKids++;
                }
            }

            // all preferences can be matched, hence 0 hungry
            if (circularSandwichCount == 0 && squareSandwichCount == 0) return 0;

            // sandwich and preference NOT MATCHING | Find which kind of sandwich are in excess leading to hungry kids of opp choice
            if (circularSandwichCount > 0)                     // more circular sandwich than req
            {
                // find the 1st excess circular-sandwich
                for (int i = 0; i < l; i++)        // O(n)
                    if (sandwiches[i] == 0)
                    {
                        if (--totalCircularKids < 0)
                            return totalSquareKids;
                    }
                    else --totalSquareKids;
            }
            else                                            // more square sandwich than req
                                                            // find the 1st excess square-sandwich
                for (int i = 0; i < l; i++)        // O(n)
                    if (sandwiches[i] == 1)
                    {
                        if (--totalSquareKids < 0)
                            return totalCircularKids;
                    }
                    else --totalCircularKids;

            return 0;
        }


        // Time O(n) | Space O(1), n = length of 'tickets'
        public static int TimeRequiredToBuy(int[] tickets, int k)
        {
            /* ALGO
            1. People in front make us wait for Min of (no of ticket they want, no of tickets we want) // as we leave once we have r quota
            2. People in back of us made us wait for Min of (no of ticket they want, -1 + no of tickets we want) // as we don't wait for them for our last ticket
             */
            int waitTimeAfterGoingBackToQueue = 0, l = tickets.Length;
            for (int i = 0; i < l; i++)            // O(n)
                                                   // calculate time wasted for going around the line
                                                   // people will keep us waiting for the 1 * no of tickets they need with upper cap being our TotalReqTickets post which we leave the Q
                if (i <= k)
                    waitTimeAfterGoingBackToQueue += Math.Min(tickets[k], tickets[i]);
                else    // this would be for last ticket as don't need to queue back again
                    waitTimeAfterGoingBackToQueue += Math.Min(tickets[k] - 1, tickets[i]);

            return waitTimeAfterGoingBackToQueue;
        }


        // Time O(nlogn) | Space O(n), n = length of 'deck' array
        public static int[] DeckRevealedIncreasing(int[] deck)
        {
            /* ALGO
            1. Sort he deck in descresing order
            2. Create a Queue and add top-card from deck to its front
            3. Now for all the card 2nd index onwards, Pull the Q front and add it to the back before adding new card to queue
            4. Repeat Step #3 until deck is empty
            5. Return the reverse of the queue as result at the end
            */
            // Sort in decreasing order
            Array.Sort(deck, (x, y) => y.CompareTo(x));   // O(nlogn)
            int l = deck.Length;
            Queue<int> q = [];
            q.Enqueue(deck[0]); // enqueue the top card

            for (int i = 1; i < l; i++)
            {
                // remove from front and add to back
                q.Enqueue(q.Dequeue());
                // add new deck to the front
                q.Enqueue(deck[i]);
            }

            return q.Reverse().ToArray();
        }


        // Time = Space = O(n*m), n,m = rows and cols of "Land"
        public static int[][] FindFarmland(int[][] land)
        {
            /* ALGO
            1. We need to find no of grps of farmland i.e cells which are famrland and touching each other via atleast 1 common boundry
            2. so we iterate from 0..n-1 rows and from left i.e. 0..m-1 cols
            3. For each cell if its a Farmland we mark all the connected farmland as visited
            4. While doing above #3 we also keep updating the left-top and bottom right corner for each cell {r,c} we encounter while using DFS
            5. store the points in a list and convert to array and return.
            6. NOTE: BFS can also be used instead of DFS
             */

            // find islands/grp of farmland
            // foreach farmland get the top left and bottom rt corner
            List<int[]> result = [];
            int rows = land.Length, cols = land[0].Length, r1 = 0, c1 = 0, r2 = 0, c2 = 0;
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    if (land[r][c] == 1)   // found a farmland
                    {
                        r1 = c1 = int.MaxValue;
                        r2 = c2 = 0;
                        DFS(r, c);   // mark all linked area as visited
                        result.Add([r1, c1, r2, c2]);
                    }
            return result.ToArray();

            // local helper func
            void DFS(int r, int c)
            {
                if (land[r][c] == 0) return;
                // mark visited
                land[r][c] = 0;

                // update corners
                r1 = Math.Min(r, r1);
                c1 = Math.Min(c, c1);
                r2 = Math.Max(r, r2);
                c2 = Math.Max(c, c2);

                // move in all 4 valid directions
                if (r - 1 >= 0) DFS(r - 1, c);
                if (c - 1 >= 0) DFS(r, c - 1);
                if (r + 1 < rows) DFS(r + 1, c);
                if (c + 1 < cols) DFS(r, c + 1);
            }
        }


        // Time = Space = O(n), n = no of nodes in the tree/graph
        public static int[] SumOfDistancesInTree(int n, int[][] edges)
        {
            /* ALGO
            1. to calculate the sum of distance of all children we need to split the problem in 2 halves
            2. **FIRST** calculate the final/correct soln for 1 node lets take root as '0'
            3. to calculate partial ans for each node we need 2 things:
                - Make recursive call to children so during backtracking we have below 2
                - no of children a node has / total no of node below cur node
                - total of 'sum of distances for each child' to calculate cur node ans
            4. Above operation via DFS gives us correct result for only root node and partial ans for rest of the nodes
            5. **SECOND** we need to calculate final ans for each remaining node except root
            6. to update the final ans for each node we need:
                - correct ans for parent
                - no of child cur node has (note this was added to parents ans in 1st DFS call)
                - no of child the parents had i.e. N - no of child i have
                - ans[node] = ans[parent] - child[node] - (N-child[node])
                - than make recursive all to child of cur node as we have final ans for cur node
            7. return the ans at the end
             */
            List<int>[] g = new List<int>[n];
            int[] count = new int[n], sumOfAllcountDistance = new int[n];
            // Intializing graph and setting default count for each node
            for (int i = 0; i < n; i++)            // O(n)
            {
                g[i] = new();
                count[i] = 1; // so that even root node returns 1 child back to its parents (this can also be done before making recursive call to childresn)
            }
            // Create Graph
            foreach (var edge in edges)      // O(n)
            {
                var u = edge[0];
                var v = edge[1];
                g[u].Add(v);
                g[v].Add(u);
            }
            // calculate correct ans for root node
            DFSForRoot(0);
            // complete calculation for all other nodes except root
            DFSForAll(0);
            return sumOfAllcountDistance;

            // local helper func
            void DFSForRoot(int node, int parent = -1)
            {
                foreach (var ch in g[node])
                    if (ch != parent)  // call child 1st and then calculate for node
                    {
                        DFSForRoot(ch, node);
                        // increament count for cur node with what cur child has (initially it was set to 1 for all nodes)
                        count[node] += count[ch];
                        // add the sum of ans of cur child + total no of children child has
                        sumOfAllcountDistance[node] += sumOfAllcountDistance[ch] + count[ch];
                    }
            }
            void DFSForAll(int node, int parent = -1)
            {
                foreach (var ch in g[node])
                    if (ch != parent)  // calculate for cur node 1st and then call children(s)
                    {
                        sumOfAllcountDistance[ch] = sumOfAllcountDistance[node] - count[ch] + (n - count[ch]);
                        DFSForAll(ch, node);
                    }
            }
        }


        // Time O(n) | Space O(1), n = length of 'nums' array
        public static int MinOperations(int[] nums, int k)
        {
            /* ALGO
            1. get the xor of all the numbers in array and 'k'
            2. no we only need to flip bits that are still On
            3. count the no of bits that are still on by taking the & with 1 of the right most bit in 'xor'
            4. right shift the xor by 1 so we can evaluate the next bit
            5. repeat from #3 till xor !=0
             */
            int bitFlipsNeeded = 0;
            // get XOR of 'k' with all the numbers in the array
            for (int i = 0; i < nums.Length; i++)  // O(n)
                k ^= nums[i];

            // no of bits that needed to be flipped equal to no of bits that are still on
            while (k!= 0)                   // O(32)
            {
                // if right most bit is one we increament 1 to bits that need to be flipped
                bitFlipsNeeded += k & 1;
                k >>= 1;   // right shift the xor by 1 place
            }
            return bitFlipsNeeded;
        }


        // Time O(nlogk) | Space O(k), n = length of 'happiness'
        public static long MaximumHappinessSum(int[] happiness, int k)
        {
            /* ALGO
            1. Create a MinHeap/PQ of top 'k' happiness
            2. we iterate thru happniess from 0..n-1 idx
                a. if pq.Count < k we push to queue
                b. else pq.Count == k, we check if smallest/top is smaller than current than we remove top and push current
            3. now we have top 'k' values
            4. we store all the values in a array in descending orderr, biggest value comes first and smallest at end
            5. now just take the sum of all the happiness we have and reducing the each happiness by no of turns/picks we have completed so far.
             */
            // Create Min Heap of Top 'k' happiness
            var pq = new PriorityQueue<int, int>(k);
            foreach (var hIdx in happiness)              // O(n)
                if (pq.Count < k)
                    pq.Enqueue(hIdx, hIdx);  // <Element, Priority>
                                             // if new happinessIdx is greater than the smallest than remove top and insert higher happiness in Queue
                else if (pq.Peek() < hIdx)
                    pq.DequeueEnqueue(hIdx, hIdx);       // O(logk)

            // fill all values from Queue in a array in descending order
            int[] maxKHappy = new int[k];
            for (int i = 0; i < k; i++)                        // O(k)
                maxKHappy[k - 1 - i] = pq.Dequeue();

            long maxHappinessExtracted = 0;
            for (int i = 0; i < k; i++)                        // O(k)
            {
                var curHappiness = maxKHappy[i] - i;
                if (curHappiness <= 0) break;  // no point collecting more happiness after this point all we are going to get is 0
                maxHappinessExtracted += curHappiness;
            }
            return maxHappinessExtracted;
        }


        // Time O(n^2*logK) | Space O(k), n = lenght of 'arr'
        public static int[] KthSmallestPrimeFraction(int[] arr, int k)
        {
            /* ALGO
            1. create a maxHeap/Pq to hold top 'k' fractions
            2. keep pq key/element as the array [a,b] and priority as the double fraction value
            3. now we need 2 loops to iterate thru all possible combination of i,j
            4. for each valid pair of i,j we do below
                a. if pq.Count < k, add the Element and Fraction to pq
                b. if pq.Count == k, check the top/biggest fraction by Peeking,
                    if > than current fraction, remove top and add new entry to pq
            5. at the end return the top of pq as its the 'k' biggest fraction
             */
            var pq = new PriorityQueue<int[], double>(Comparer<double>.Create((a, b) => b.CompareTo(a)));
            for (int i = 0; i < arr.Length - 1; i++)
                for (int j = i + 1; j < arr.Length; j++)
                {
                    double curFraction = arr[i] / (double)arr[j];
                    // add to heap if total is < k
                    if (pq.Count < k)
                        pq.Enqueue([arr[i], arr[j]], curFraction);
                    // if we have 'k' max fractions
                    else if (pq.TryPeek(out int[] Element, out double Priority))
                        // replace the max/top if we found a fraction which is smaller than it
                        if (Priority > curFraction)
                            pq.DequeueEnqueue([arr[i], arr[j]], curFraction);
                }
            // return the kth fraction
            return pq.Dequeue();
        }


        // Time O(r*c) | Space O(1), r , c = no of rows and columns respectively
        public static int MatrixScore(int[][] g)
        {
            /* ALGO
            1. for all the rows from 0..Rows-1
                check and if 1st/0th/left most column is 0 than flip entire row else do not,
                (deciding here on basis of most significant bit
                as all other bit combined don't have same effect on final number)
            2. for all the cols from 0..Cols-1
                for each col count no of 1's by iterating from 0..Rows-1, 
                if they are less than half than flip entire column else no need, we need to maximize the no of 1's
            3. calculate the totalSum by calculating the number for each row basis on bits for each row
                and keep adding each row number to total.
             */
            int rows = g.Length, cols = g[0].Length, totalSum = 0;
            // flip the bits in a given row if the left most significant bit is 0 any other bit doesnt contribute as much as the left most does
            for (int r = 0; r < rows; r++)
                if (g[r][0] == 0)
                    // using XOR to flip bits
                    for (int c = 0; c < cols; c++)
                        g[r][c] ^= 1;
            // flip the bits in a given column if the no of zero's are more than one's
            for (int c = 0; c < cols; c++)
            {
                int countOnes = 0;
                for (int r = 0; r < rows; r++)
                    countOnes += g[r][c];
                if (countOnes * 2 < rows)   // flip the column as there are less 1's than 0's
                    for (int r = 0; r < rows; r++)
                        g[r][c] ^= 1;
            }
            // calculate the sum of all the rows (each row is a number)
            for (int r = 0; r < rows; r++)
            {
                int curRowNum = 0;
                for (int c = 0; c < cols; c++)
                    curRowNum = (curRowNum << 1) + g[r][c];
                totalSum += curRowNum;
            }
            return totalSum;
        }

        // Time O(logn * n^2) | Space O(n^2), n = length of 'grid'
        public static int MaximumSafenessFactor(int[][] grid)
        {
            /* ALGO
            1. Create and initialize an 2-D array 'distanceToClosedThief' with 2*n value for each cell which contains the distance to closet thief for each cell.
            2. Now go thru all the thief cell i.e. with value ==1 and add them to Queue to that we can in 1 BFS find out the min distance to a thief for all cells, use visited 2-D array to keep track of what cells have been already updated.
            3. Now using Binary Search we set the lower bound to 1 and upper bound to 2*n for safetyFactor
            4. Now using BFS try to find it their exists path b/w source to destination with no cell having distanceToClosedThief < safetyFactor
            5. if yes, update the 'safestPath' and lower bound for binary-search
            6. if no such path exists update the upper bound to safetyFactor-1
            7. return safestPath at end.
            8. NOTE: if 0,0 or n-1,n-1 cell itself has theif just return 0 as ans without any calculations.
             */
            int n = grid.Length;
            // base case, starting or ending cells has thief there is no safe path possible
            if (grid[0][0] == 1 || grid[n - 1][n - 1] == 1) return 0;
            int[,] distanceToClosedThief = new int[n, n];

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    distanceToClosedThief[r, c] = 2 * n;

            // calculate the distance of nearest thief for all cells using BFS
            Queue<int[]> q = [];
            bool[,] visited = new bool[n, n];
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    if (grid[r][c] == 1)
                        q.Enqueue([r, c, r, c]);

            while (q.TryDequeue(out int[] val))
            {
                int i = val[0], j = val[1], r = val[2], c = val[3];

                // if alredy visited continue
                if (visited[i, j]) continue;

                // else mark visited
                visited[i, j] = true;

                // update distance to cur thief
                distanceToClosedThief[i, j] = Math.Min(distanceToClosedThief[i, j], ManhattanDist(i, j, r, c));

                // traverse in all 4 valid directions
                if (i > 0 && !visited[i - 1, j])
                    q.Enqueue([i - 1, j, r, c]);
                if (i < n - 1 && !visited[i + 1, j])
                    q.Enqueue([i + 1, j, r, c]);
                if (j > 0 && !visited[i, j - 1])
                    q.Enqueue([i, j - 1, r, c]);
                if (j < n - 1 && !visited[i, j + 1])
                    q.Enqueue([i, j + 1, r, c]);
            }

            // starting from 0,0 using BFS find if there exists a safe path with safestFactor 'v'
            int safestPath = 0, minSafeness = 1, maxSafeness = 2 * (n - 1), safeFactor;
            // Binary-Search to max safeness factor 'sf'
            while (minSafeness <= maxSafeness)                 // O(logn * n^2)
            {
                safeFactor = (minSafeness + maxSafeness) / 2;
                visited = new bool[n, n];
                // if can reach destination from source for current safeness Factor 'sf'
                if (BFS(0, 0))
                {
                    safestPath = safeFactor;
                    minSafeness = safeFactor + 1;
                }
                else
                    maxSafeness = safeFactor - 1;
            }
            return safestPath;

            // local helper func
            bool BFS(int i, int j)
            {
                q.Clear();
                q.Enqueue([i, j]);
                while (q.TryDequeue(out int[] val))
                {
                    int r = val[0], c = val[1];
                    // return if already visited cell || or min distance is smaller than req safeness factor
                    if (visited[r, c] || distanceToClosedThief[r, c] < safeFactor) continue;
                    // mark visited
                    visited[r, c] = true;

                    // check if we reached destination, return true
                    if (r == n - 1 && c == n - 1)
                        return true;

                    // traverse in all 4 valid directions
                    if (r > 0 && !visited[r - 1, c])
                        q.Enqueue([r - 1, c]);
                    if (r < n - 1 && !visited[r + 1, c])
                        q.Enqueue([r + 1, c]);
                    if (c > 0 && !visited[r, c - 1])
                        q.Enqueue([r, c - 1]);
                    if (c < n - 1 && !visited[r, c + 1])
                        q.Enqueue([r, c + 1]);
                }
                return false;
            }

            static int ManhattanDist(int x1, int y1, int x2, int y2) => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }


        // Time = Space = O(n), n = length of 'nums'
        public static long MaximumValueSum(int[] nums, int k, int[][] edges)
        {
            /* ALGO
            1. XOR of any number 'A' with any other number 'K" twice results in original number i.e. A^k^k = A
            2. we can take any two 2 nodes as its a connected graph/tree all nodes are connected directly or indirectly
            3. Two nodes indirectly connected can be XOR here think of all nodes in b/w them would selected any XORed,
                all in b/w nodes get XOR twice hence retaining their original value.
            4. we just need to find all delta node pairs would increase the totalSum.
            5. in case of EVEN count of +ve delta, we know for sure adding all pair will definatly increase the sum
            6. in case of ODD count of +ve delta extra steps are needed
                we just create MaxHeap of size 3 to keep using the biggest 2 values as Pair
                and at the end we see if the (largestValue in -ve delta + only element left in heap) total is positive we add them too.
             */
            int n = nums.Length, firstPosIdx = -1, posDeltaCount = 0;
            long[] delta = new long[n];
            long originalSum = 0, increaseBy = 0;
            // calculate the delta for each node if its XOR with k
            // also calcualte the original total Sum of nodes
            for (int i = 0; i < n; i++)                // O(n)
            {
                originalSum += nums[i];
                delta[i] = (nums[i] ^ k) - nums[i];
                if (delta[i] > 0)
                    posDeltaCount++;
            }
            // we have ODD no of +ve delta's
            if (posDeltaCount % 2 == 1)
            {
                long smallestNeg = long.MinValue;
                var maxHeap = new PriorityQueue<long, long>(Comparer<long>.Create((x, y) => y.CompareTo(x)));
                // find the no of pair of +ve nodes we can XOR to achieve even greater sum
                // but also try to maximize the values in case of ODD count
                for (int i = 0; i < n; i++)                // O(n)
                    if (delta[i] > 0)
                    {
                        maxHeap.Enqueue(delta[i], delta[i]);
                        if (maxHeap.Count == 3)
                            increaseBy += maxHeap.Dequeue() + maxHeap.Dequeue();
                    }
                    else
                        smallestNeg = Math.Max(smallestNeg, delta[i]);

                // if only left +ve delta + largest-ve-delta total is +ve we add them too
                increaseBy += Math.Max(maxHeap.Dequeue() + smallestNeg, 0);
            }
            // we have EVEN no of +ve delta's
            else
                for (int i = 0; i < n; i++)                // O(n)
                    if (delta[i] > 0)
                        if (firstPosIdx == -1)
                            firstPosIdx = i;
                        else
                        {
                            increaseBy += delta[i] + delta[firstPosIdx];
                            firstPosIdx = -1;
                        }
            return originalSum + increaseBy;
        }


        // Time O(2^n) | Space O(n), n = length of 'nums'
        public static int BeautifulSubsets(int[] nums, int k)
        {
            /* ALGO - BruteForce
            1. Sort the input array so we only iterate thru nums in non-decreasing order (aka increasing but array can have duplicates)
            2. Create a HashSet to keep track of all the numbers included so far in cur subset
            3. Now start recursion from 0th index, at each index we have 2 choice
                a. skip taking cur num and make recursive call to next idx
                b. include cur num if there exists no other num in HashSet which is equal to 'curNum-k'
            4. whenever we include a number to a subset, add +1 to global 'beautiful' counter.
             */
            int l = nums.Length, beautiful = 0;
            Array.Sort(nums);            // O(nlogn)
            HashSet<int> prvNums = [];
            Get(0);                      // O(n^2)
            return beautiful;

            void Get(int idx)
            {
                if (idx == l) return;
                // skip current num
                Get(idx + 1);
                // take current num if possible
                if (!prvNums.Contains(nums[idx] - k))
                {
                    prvNums.Add(nums[idx]);
                    beautiful++;
                    Get(idx + 1);
                    prvNums.Remove(nums[idx]);
                }
            }
        }


        // Time O(2^n) | Space O(n), n = length of 'words'
        public static int MaxScoreWords(string[] words, char[] letters, int[] score)
        {
            /* ALGO
            1. first thing is to count unqCharacters and their freq for each word in words and save it into a array of Dictionary<char,int>
            2. next get the freq of all the avaliable characters from letter including duplicate so we know how many characters we have in Bank to construct new words
            3. Now we call a recursive func from 0th index
            4. for each index we do:
                a. get the max score we can get it we skip taking cur word and move to next index instead in 'scoreIfSkip'
                b. see if we have enouf words left in Bank to form cur words if yes get the score for current word and update Bank by decreasing freq of used words and this score to 'scoreIfInclude' and also include score by recursively calling next index with reduced Bank.
            5. while Backtracking when we used a word a given index remeber to add back the freq of used words as we are no longer using them
            6. At the end return the Max (scoreIfSkip / scoreIfInclude)
             */

            int l = words.Length;
            // Get the unqWordFreq in words
            Dictionary<char, int>[] unqWordFreq = new Dictionary<char, int>[l];
            for (int i = 0; i < l; i++)                                // O(n) = O(14)
            {
                unqWordFreq[i] = [];    // initialize the dictionary foreach index
                foreach (var ch in words[i])                     // O(15)
                    if (unqWordFreq[i].ContainsKey(ch))
                        unqWordFreq[i][ch]++;
                    else
                        unqWordFreq[i][ch] = 1;
            }


            // Get the freq of each character in letters
            int[] charBank = new int[26];
            foreach (var ch in letters)                          // O(m) = O(100)
                charBank[ch - 'a']++;

            return MaxScore(0);                                 // O(2^n)

            // local helper func
            int MaxScore(int idx)
            {
                if (idx == l) return 0;

                // skip adding cur word
                int scoreIfSkip = MaxScore(idx + 1);


                // add cur word is we have enouf characters left for it in charBank
                int scoreIfInclude = 0;
                // remvoe the characters that are need to carve cur word from bank
                var scoreFromCurWord = GetCurWordScore(idx);                // O(26) ~O(1)
                if (scoreFromCurWord > 0)
                {
                    scoreIfInclude = scoreFromCurWord + MaxScore(idx + 1);
                    // add back the characters that were used back to bank
                    foreach (var chFreq in unqWordFreq[idx])                // O(26) ~O(1)
                        charBank[chFreq.Key - 'a'] += chFreq.Value;
                }
                return Math.Max(scoreIfSkip, scoreIfInclude);
            }
            int GetCurWordScore(int idx)
            {
                var updatedBank = charBank.ToArray();
                int wordScore = 0;
                foreach (var chFreq in unqWordFreq[idx])
                {
                    updatedBank[chFreq.Key - 'a'] -= chFreq.Value;
                    if (updatedBank[chFreq.Key - 'a'] >= 0) wordScore += (score[chFreq.Key - 'a'] * chFreq.Value);
                    else return 0;
                }
                charBank = updatedBank;
                return wordScore;
            }
        }


        // Time O(nlogn) | Space O(1), n = length of 'nums'
        public static int SpecialArray(int[] nums)
        {
            /* ALGO
            1. sort the input array
            2. we need to try all the numbers from 0..largestNuminArray
                as possible 'X'
            3. 1st thing we need to check if number equal to array length
                and also equal to (smallestNum-1) i.e. 'nums[0]-1'
                satifies the condition as base case.
            4. so we try to see if any number 'X' from 0..MaxNum satifies
                the condition
            5. we keep iterating from 0th index to last idx in array to check
                if cur numbers == length - curIdx, if true return cur num
            6. we also have to account for missing numbers b/w last number
                read from array and cur numbers in array which we track
                using variable lastNum
            7. lastNum while loop execute till the point we have a numbers
                smaller than cur index number, if any of in b/w num satifies
                the condition return that as answer.
            8. else return -1 at the end
             */

            Array.Sort(nums);               // O(nlogn)
            int l = nums.Length, lastNum = -1;
            // check if 'X' equal to input array length and smaller 1st elements satisfies both the condition
            if (l < nums[0]) return l;

            // check for all possible 'X' values from 0..N-1 idx
            for (int x = 0; x < l; x++)       // O(n)
            {
                // check for all the X that are b/w the last idx number and cur idx number in array
                while (++lastNum < nums[x])
                    if (lastNum == l - x) return lastNum;

                // check if one of the numbers in array fulfils all 'X" conditions
                if (nums[x] == l - x) return nums[x];

                // skip all duplicates, as problem stats there has to be strictly 'X' numbers greather than equal to given num
                while (x + 1 < l && nums[x] == nums[x + 1])
                    x++;
            }
            return -1;
        }


        // Time O(n) | Space O(1), n = length of string 's'
        public static int EqualSubstring(string s, string t, int maxCost)
        {
            /* ALGO
            1. we need to find a substring whose total cost is within maxCost of update range
            2. So we start iterating from 0..N-1 index using rt pointer
            3. if we get a matching word we increase substring length and update global maxLen
            4. if we have to update characters at given index we add that cost to curTotalCost
            5. if curTotalCost is <= maxCost allowed we again update the global max substr len
            6. but if curTotalCost > maxCost we move the left pointers initially at 0th idx which reduce the update cost and also the substring length
            7. at the end return the global maxLen
             */
            int l = s.Length, maxEqualSubstringLen = 0, lt = -1, rt = -1, curCost = 0;
            while (++rt < l)
            {
                curCost += Math.Abs(s[rt] - t[rt]);
                // matching characters or added cost within bounds
                if (curCost <= maxCost)
                    maxEqualSubstringLen = Math.Max(maxEqualSubstringLen, rt - lt);
                else // cur index 'rt' update cost addition leads to curCost more than max allowed, then move the lt pointer to reduce the cost and length of substring
                    while (curCost > maxCost)
                        curCost -= Math.Abs(s[++lt] - t[lt]);
            }
            return maxEqualSubstringLen;
        }


        // Time O(nlogn) | Space O(n), = length of string 's'
        public static int NumSteps(string s)
        {
            /* ALGO
            1. Create list to represent the number fo same length as input 's'
            2. if bit is one the bool value of same index is true else false for '0'
            3. Now we start reducing the number, if the number is
                a. Even: Meaning divide /2 i.e. rt most bits is delete and all
                    other bits r rt shifted by 1
                    which i am simulating by reducing the length of num list by 1
                b. Odd: meaning we have to add 1 to last bit, if its zero we add
                    and break out if its already ON we update it to 0 and take the
                    carry and try repeating this process to left bit.

                    until we encounter a bit thats OFF or we run out of index
                    means a new ON bit has to be added to left most side (most sifnificant bit)
                    Also remeber to increase the length by 1 if new bit is added to left most side.
            4. keep increamenting the steps counter irrespective of what operation is performed above.
             */
            int l = s.Length, stepsNeeded = 0;
            List<bool> num = new List<bool>();
            for (int i = 0; i < l; i++)            // O(n)
                num.Add(s[i] == '1');
            while (l > 1)                      // O(nlogn)
            {
                // last bit is 0 means number is even
                // (hence divide by 2 i.e. right shift all bits by 1 also means lastBit is gone)
                if (!num[l - 1])
                    l--;
                // last bit is 1 means number is odd (add 1 to right most bit)
                else //if(num[l-1])
                {
                    var carry = true;
                    var idx = l - 1;
                    while (carry)
                        if (idx >= 0)
                        {
                            carry &= num[idx];
                            num[idx] = !num[idx--];
                        }
                        else
                        {
                            num.Insert(0, carry);
                            l++;
                            break;
                        }
                }
                // increament the step counter
                stepsNeeded++;
            }
            return stepsNeeded;
        }

        // Time O(nlogn) | Space O(n), n = no of task/length of 'profits'
        public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            /* ALGO
            1. store the captial as key and profit as value in min PQ 'minCapTask' for all the tasks from 0..n-1 index
            2. while k > 0 
                iterate thru the minCapTask PQ and till we reach a task for which we don't have enouf capital
                and keep adding [profit,profit] as key & value in maxPriorityQueue
            3. Take maxPriorityQueue top if NOT EMPTY and update total capital raised by adding its profit
                also now remove pq top
            3. if maxPriorityQueue is EMPTY break as we don't have any task which we can pick with captial we have
             */
            int l = profits.Length;
            PriorityQueue<int, int> minCapTask = new();
            for (int i = 0; i < l; i++)    // O(nlogn)
                minCapTask.Enqueue(profits[i], capital[i]);  // add <Element>, <Priority> to PQ

            PriorityQueue<int, int> maxTotalGain = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
            while (k-- > 0)
            {
                // pull all tasks for which we have enouf captial
                while (minCapTask.TryPeek(out int profit, out int minCapitalReq) && minCapitalReq <= w)
                {
                    minCapTask.Dequeue();
                    // and add them to maxTotalGain PQ
                    maxTotalGain.Enqueue(profit, profit);
                }
                // pick most profitable task that can be finished and add its profit to captial
                if (maxTotalGain.TryDequeue(out int newProfit, out int p))
                    w += newProfit;
                else break;
            }
            return w;
        }
    }
}
