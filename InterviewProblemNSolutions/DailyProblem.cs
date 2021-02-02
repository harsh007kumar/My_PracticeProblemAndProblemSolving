using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                //if (i < k - 1)
                //{
                //    // first remove all elements smallers than current element from back of queue till Queue is not empty
                //    while (q.Count > 0 && nums[q[q.Count - 1]] < nums[i])
                //        q.RemoveAt(q.Count - 1);
                //    // Insert new element index at end of the Queue
                //    q.Add(i);
                //}
                //else
                //{
                // first remove all elements smallers than current element from back of queue till Queue is not empty, also remove element which is out of the window
                while (q.Count > 0 && (nums[q[q.Count - 1]] < nums[i] || q.Count == k || q[0] == i - k))
                    if (nums[q[q.Count - 1]] < nums[i])
                        q.RemoveAt(q.Count - 1);
                    else //if (q.Count == k || q[0] == i - k)
                        q.RemoveAt(0);
                // Insert new element at end of the Queue
                q.Add(i);

                // Check to not add elements to result till initial Queue is created
                if (i < k - 1) continue;

                // add largest from last window i.e. Front of Queue to result array
                result[i - k + 1] = nums[q[0]];
                //}
            }
            return result;
        }

        // Time O(n) || Space O(1)
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
            // Stores each character as Graph Vertex(u) and the alphabets which should come after it i.e. its adjacent vertex(v)
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
                        // first check if reverse/cycle dependencies won't be created by adding edge from lastword[i]] -> Add(word[i]
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
                 * signifies there exists some index i whose sumI % k=x & index j whose sumJ % k=x
                 * Than there must be numbers in b/w i+1..j which have sum as in multiple of 'k'
                 * a%k = x
                 * b%k = x
                 * (a - b) %k = x -x = 0
                 * here a - b = the sum between i and j.
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
                    else // if(nums[start] + nums[last] + nums[i]==0)
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

        // Given four arrays A, B, C, D of integer values, compute how many tuples (i, j, k, l) there are such that A[i] + B[j] + C[k] + D[l] is zero.
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
             * while adding word to Dictionary escape last char if its ASCII value is <65 i.e. Not a alphabet
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
        // Time O(n^2) || Space O(n), where n = no of rows/col of Sudoko i.e. 9 on reducing we can express both time & space as O(1) as it's always going to be fixed val of 9x9=81 
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
        /// For 5 i.e. 101
        /// 'NoOfBitsToRepresent' = 2 + 1 = 3
        /// Now 'Mask' can be calculated by right shifting 1 NoOfBitsToRepresent times i.e. 1<< 3 = 8 i.e. 1000 in binary
        /// Now subtracting 1 from 'Mask' us all bits reversed except the left most bit
        /// 8 - 1 or 1000 - 1 = 0111
        /// XOR of (Mask-1) & num = 111 ^ 101 = 010 = 2 in decimal
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int ComplimenetBase10Fastest(int num) => (num == 0) ? 1 : (num ^ ((1 << (int)(Math.Log(num, 2) + 1)) - 1));

        // Reads the elements from all four border n spiralling inwards till no more element is left to read
        // Time O(N), N = rows*cols i.e, No of elements in matrix || Space O(N)
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
        public static int TrapRainWater(int[] height)
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
                    DFS(board, currWord, result, t, rows, cols, r, c);

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
                                            // to save space consider prv row value i.e cell[row-1,col] as current value of cell[row,col]
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

        // Returns 1st missing +ve number (i.e. num which is not part of the input array)
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

            for (int i = 1; i <= largest; i++)              // O(n) will terminate on finding 1st +ve
                if (!allPositiveNums.Contains(i)) return i;
            return largest + 1;
        }
        // Returns 1st missing +ve number (i.e. num which is not part of the input array)
        // Time O(n) Space O(1) Approach using input array itself as HashTable
        public static int FirstMissingPositive(int[] nums)
        {
            int len = nums.Length;
            if (len == 0) return 1;
            //else if (len==1) return nums[0]==1? 2:1;

            // check if '1' not present return 1
            bool foundOne = false;
            for (int i = 0; i < len; i++) if (nums[i] == 1) { foundOne = true; break; }
            if (!foundOne) return 1;

            // update all elements less than 0 or more than len as 1, if len found update nums[0] to -len
            for (int i = 0; i < len; i++) if (nums[i] <= 0 || nums[i] > len) nums[i] = 1;


            // Use all remaining elements value as index and update nums value at those index to -ve if not already -ve
            for (int i = 0; i < len; i++)
                if (Math.Abs(nums[i]) == len) nums[0] = -len;
                else if (nums[Math.Abs(nums[i])] > 0) nums[Math.Abs(nums[i])] *= -1;

            // return 1st non negative index
            for (int i = 1; i < len; i++) if (nums[i] > 0) return i;

            // check if array had an element with value = len of array than 0th index must be having -len value
            // hence return len+1, if above is not the case simnply return len
            return nums[0] < 0 ? len + 1 : len;
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
                //if(!toTrack.Contains(S[i]))    toTrack.Add(S[i]);
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
            // we can move this above by replacing the else if with => if ((s[i]!=' ' && !Char.IsDigit(s[i])) || i==s.Length-1)
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
                // Call heapify for each element present at index 'i'
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
        // There are N piles of stones arranged in a row.  The i-th pile has stones[i] stones.
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
                        // above only calculate cost of merging K piles, i.e. 1 on left * K-1 piles on rt (cost of creating those 2 piles)
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


        // Time O(n) || Space O(1)
        public static int ReverseInt(int num)
        {
            if (num == int.MinValue) return 0;
            bool isPositive = num >= 0;
            num = Math.Abs(num);
            int reverse = 0;
            while (num != 0)
            {
                if (isPositive)
                { if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 8)) return 0; }
                else
                { if (reverse > int.MaxValue / 10 || (reverse == int.MinValue && num % 10 > 7)) return 0; }
                reverse = reverse * 10 + num % 10;
                num /= 10;
            }
            return isPositive ? reverse : reverse * -1;

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

                // create parent ListNode with same value as cur ListNode
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
            IList<string> result = new List<string>();
            if (digits.Length == 0) return result;

            // Create dict from where letters corrosponding to each num would be stored
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>(8);
            char startingLetter = 'a';
            for (int inputDigit = 2; inputDigit < 10; inputDigit++)
            {
                dict.Add(inputDigit, new List<char>(3));
                for (int i = 0; i < (((inputDigit == 7) || (inputDigit == 9)) ? 4 : 3); i++)
                    dict[inputDigit].Add(startingLetter++);
            }

            LetterCombinations(digits, 0, dict, "", ref result);
            return result;
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
            //for (int i = 0; i < nums.Length; i++) rightBoundry = Math.Max(rightBoundry, nums[i]);   // O(N) time

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
            //for (int i = 0; i < leapYears1970To2100.Length; i++) leapYr.Add(leapYears1970To2100[i]);

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
            Stack<TreeNode> st = new Stack<TreeNode>(100);
            int maxSortedLen = 1, currSortedLen = 1;
            TreeNode last = null;
            bool isAscending = true;
            while (true)
            {
                while (root != null)
                {
                    st.Push(root);
                    root = root.left;
                }
                if (st.Count == 0) break;
                var temp = st.Pop();
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


        // Time O(n^2) || Space O(1)
        public static void SetMatrixZeroes(int[][] matrix)
        {
            bool firstColZero = false;
            // Mark Rows and Cols which should be made Zeroes
            for (int r = 0; r < matrix.Length; r++)
            {
                if (matrix[r][0] == 0) firstColZero = true;
                for (int c = 1; c < matrix[0].Length; c++)
                    if (matrix[r][c] == 0)
                        matrix[0][c] = matrix[r][0] = 0;
            }

            // Now update Zeroes starting from 2nd row and 2nd col onwards
            for (int r = 1; r < matrix.Length; r++)
                for (int c = 1; c < matrix[0].Length; c++)
                    if (matrix[r][0] == 0 || matrix[0][c] == 0)
                        matrix[r][c] = 0;

            // Check for 1st row
            if (matrix[0][0] == 0)
                for (int c = 0; c < matrix[0].Length; c++)
                    matrix[0][c] = 0;

            // Check for 1st col
            if (firstColZero)
                for (int r = 0; r < matrix.Length; r++)
                    matrix[r][0] = 0;
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



        // Return Max Possible Sum of "nums[i]*(i+1)" for the input array
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
                    else // (Math.Abs(nums[i]) < positiveSum)   // [NO impact, but need tfurther increases as we iterate left]
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
        // DP Bottom-Up Approach
        public static bool RegularExpressionMatchingTab(string s, string p) => true;


        // Time O(1) || Space O(1)
        public static int PoorPigs(int buckets, int minutesToDie, int minutesToTest)
        {
            /* SOLUTION FROM LEETCODE
             * How many states does a pig have
             * If there is no time to test, i.e.minutesToTest / minutesToDie = 0, the pig has only one state -alive.
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
             * Let's consider as an example one pig with 3 states, i.e. s = minutesToTest / minutesToDie + 1 = 2 + 1 = 3, and show that he could test 3 buckets.
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
                else // if (A[i-1]==A[i])
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


        // Time O(n) || Space O(1)
        public static int[][] InsertIntervals(int[][] intervals, int[] newInterval)
        {
            if (intervals.Length == 0) return new int[][] { newInterval };

            List<int[]> sortedIntervals = new List<int[]>(intervals.Length);
            // newInterval End is smaller than 1st interval
            if (newInterval[1] < intervals[0][0])
            {
                sortedIntervals.Add(newInterval);
                sortedIntervals.AddRange(intervals);
                return sortedIntervals.ToArray();
            }
            // newInterval Start is larger than last interval
            else if (newInterval[0] > intervals[intervals.Length - 1][1])
            {
                sortedIntervals.AddRange(intervals);
                sortedIntervals.Add(newInterval);
                return sortedIntervals.ToArray();
            }

            int i = 0, lastEnd = intervals[0][1];
            while (i < intervals.Length)
            {
                // newInterval lies b/w two existing intervals
                if (lastEnd < newInterval[0] && newInterval[1] < intervals[i][0])
                {
                    sortedIntervals.Add(newInterval);
                    sortedIntervals.Add(intervals[i++]);
                }

                // If input end smaller than newInterval or input start greater than newInterval add input
                else if (intervals[i][1] < newInterval[0] || intervals[i][0] > newInterval[1])
                    sortedIntervals.Add(intervals[i++]);

                // If input startTime <= newInterval and endTime >= than newInterval add input Interval
                else if (intervals[i][0] <= newInterval[0] && intervals[i][1] >= newInterval[1])
                    sortedIntervals.Add(intervals[i++]);

                // Add 1 || startTime of interval is < startTime newInterval
                else if (intervals[i][0] <= newInterval[0])
                {
                    int startTime = intervals[i][0];
                    while (i < intervals.Length && intervals[i][0] <= newInterval[1]) i++;
                    sortedIntervals.Add(new int[] { startTime, Math.Max(intervals[i - 1][1], newInterval[1]) });
                }
                // Add 1 || endTime of interval is <= endTime newInterval
                else //if (intervals[i][1] <= newInterval[1])
                {
                    int last = Math.Max(newInterval[1], intervals[i++][1]);
                    while (i < intervals.Length && intervals[i][0] <= newInterval[1])
                        last = Math.Max(last, intervals[i++][1]);
                    sortedIntervals.Add(new int[] { newInterval[0], last });
                }
                lastEnd = intervals[i - 1][1];
            }

            return sortedIntervals.ToArray();
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

        // first attempt, fails when buying price for 1st tranx is best buying price even for 2nd tranx, i.e. making single buy + sell is better than 2
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
            /* Divide the problem into 2 parts
             * 1st tranx in left
             * 2nd tranx in right
             * Return maxProfit = left + right
             * 
             * create Lmin array which hold min value to BUY at so far
             * create Rmax array which hold max value to SELL at so far
             * we try to find maximize profit in left and right half
             */

            var len = prices.Length;
            if (len < 2) return 0;
            int Lmin = prices[0], Rmax = prices[len - 1];

            int[] left = new int[len];
            // left to right pass [Sell after considering we have previously bought]
            for (int i = 1; i < len; i++)
            {
                // max profit possilble at current index is max of either profit at i-1 or curr selling price my Lmin
                left[i] = Math.Max(left[i - 1], prices[i] - Lmin);
                Lmin = Math.Min(Lmin, prices[i]);
            }

            int[] right = new int[len];
            // right to left pass [Buy after considering we have previously sold]
            for (int i = len - 2; i >= 0; i--)
            {
                // max profit possilble at current index is max of either profit at i+1 or curr buying price my Rmax
                right[i] = Math.Max(right[i + 1], Rmax - prices[i]);
                Rmax = Math.Max(Rmax, prices[i]);
            }

            /* One optimization is calculating the total profit while calculating max profit for 'right array' above*/
            int totalProfit = 0;
            // taking each index as mid point for dividing the entire array to maximize totalProfit
            for (int i = 0; i < len; i++)
                totalProfit = Math.Max(totalProfit, left[i] + right[i]);
            return totalProfit;
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
                /* Compute dp[i] using below formula
                 * Lets dp[i] be the no of ways to write valid Number if N becomes N[i], N[i+1], N[i+2],...
                 * Ex int n = 2345 = string num = "2345", then 
                 * dp[0] would be maximum 2345
                 * dp[1] would be max 345
                 * dp[2] would be max 45
                 * dp[3] would be max 5
                 * 
                 * By above we can infer that formula :
                 *  dp[i] = (no of single_d in digits < num[i]) * Math.Power(digits.Length,K-i-1)
                 *          +
                 *          if any single_d in digits == num[i] than add dp[i+1] as well.
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
             *  Hence to speed up the execution, save the result of current Node in a data-structure (i used Dictionary/HashTable)
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
                    // search 1st number from last index which is great than nums[i]
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
            // Sort the array in ascending from last => i+1 to End
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
             * we can re-phrase it as we need to find a song j whose time[j]%60 + current song i time[i]%60 is multiple of 60
             * or in other words (time[i] + time[j]) % 60 == 0
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
            /* The Problem is to HIRE 'K' workers in least total amt:
             * Following Rules apply:
             *      Every worker in the paid group should be paid in the ratio of their quality compared to other workers in the paid group.
             *      Every worker in the paid group must be paid at least their minimum wage expectation.
             *  
             *  first find and sort workers in increasing order of their ratio of 'wage/quality'
             *  Now add first 'K' workers to MaxHeap (based upon their 'quality'), also keep total sum of their quality
             *  we can calculate Min Cost to HIRE these first K workers by seleting ratio of highest wage/quality worker i.e. Kth worker(remeber sorted list)
             *  and multiplying by total quality sum.
             *  
             *  After Kth index, now we check new worker has quality smaller than worker on Top of MaxHeap, repalce n Heapify
             *  also update the sum of quality of K workers
             *  calculate MinCost = Math.Min(minCost,sumOfQuality * ratioOfKthWorker)
             */
            int len = quality.Length;
            Worker[] workers = new Worker[len];                 // Space O(n)
            for (int i = 0; i < len; i++)                       // Time O(n)
                workers[i] = new Worker(quality[i], wage[i]);

            Array.Sort(workers);                                // Time (nLogn)

            int qualitySum = 0;
            MaxHeap h = new MaxHeap(k);
            for (int i = 0; i < k; i++)                         // Time O(kLogk)
            {
                h.Insert(workers[i].quality);
                qualitySum += workers[i].quality;
            }
            // compute initial result with sum of 0...k-1 index ratio sorted workers
            double minTotalCost = (double)qualitySum * workers[k - 1].WageQualityRatio();

            for (int i = k; i < len; i++)                       // Time O((n-k)Logk)
                if (h.arr[0] > workers[i].quality)
                {
                    qualitySum += (workers[i].quality - h.arr[0]);
                    h.arr[0] = workers[i].quality;
                    h.MaxHeapify();
                    minTotalCost = Math.Min(minTotalCost, qualitySum * workers[i].WageQualityRatio());
                }

            return minTotalCost;
        }
        class Worker : IComparable<Worker>
        {
            public int quality, wage;
            public Worker(int q, int w)
            { quality = q; wage = w; }
            public double WageQualityRatio() => (double)wage / (double)quality;
            int IComparable<Worker>.CompareTo(Worker another) => this.WageQualityRatio().CompareTo(another.WageQualityRatio());
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
                    // replace current digit with least significant digit greater than current digit i.e. ls[i]
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

            //for (int i = 1; i <= V; i++)
            //    if (graph[i].Count > 1)  // In Order for Cycle to Pass thru Node it must have atleast 2 edges
            //        if (DetectCycle(graph, i, cycle, new int[V + 1]))
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


        // Time O(Max(E,V^3)) || Space O(V), V = no of productsNodes & E = no of edges i.e. Total no of Connections b/w 2 products
        public static int GetMinTrioSum(int productsNodes, List<int> productsFrom, List<int> productsTo)
        {
            int ans = int.MaxValue;
            HashSet<int>[] graph = new HashSet<int>[productsNodes + 1];
            // Initialize each Vertex
            for (int i = 0; i < productsNodes; i++)
                graph[i + 1] = new HashSet<int>();  // Note: Products are 1 indexed

            // Create the mapping i.e Graph as per given relations
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
                        // given i,j,k form an Trio i.e. Triangle than compute their Outer Product Sum
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
        public static void ReOrderList(ListNode A)
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
        public static int[] DailyTemperatures(int[] T)
        {
            int[] ans = new int[T.Length];
            Stack<int> nextHigher = new Stack<int>(T.Length);
            for (int i = T.Length - 1; i >= 0; i--)
            {
                while (nextHigher.Count > 0 && T[nextHigher.Peek()] <= T[i])
                    nextHigher.Pop();
                // if stack is empty means no higher value exists else take the Stack Top
                ans[i] = nextHigher.Count > 0 ? nextHigher.Peek() - i : 0;
                nextHigher.Push(i);      // add current val to Stack
            }
            return ans;
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
                else // if(chars[i]!=lastChar)
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
             * at end of each level update maxWidth i.e. if distance b/w 1st node in queue n last added node in queue + 1 is greater
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
                if (arr[i] == larger) i = (i + 1) % l;  // if at any point arr[i]==larger then increament i by 1 more
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
            /*  Main idea is that if we fix two points i and j can we do something in between ??
                considering the fact that `a` and `b` values asked in question are xor values of
                two consecutive contiguous subarrays(i meant those two subarrays share a border).. if we can somehow find third point which gives us
                two equal xor values as asked..In that case total xor must be zero(for whatever segment considered by i and j values)
                this implies we can consider xor for any point k i.e(k > i and k <= j) forming segments[i..k - 1] and[k..j]
                which are values of `a` and `b` given in question and of course equal as total segment xor is 0   
                there can be points like(i, k, j), (i, k + 1, j)...(i, k, k) which will all have total xor as 0   
                i.e xor[i..k - 1] = xor[k..j] and xor[i..k] = xor[k + 1..j] and so on   
                but they are different triplets.
            */

            int l = arr.Length, triplets = 0, xor;
            for (int i = 0; i < l; i++)
            {
                // calculate cur_segment_xor between i..j and j > i
                xor = arr[i];
                for (int j = i + 1; j < l; j++)
                {
                    xor ^= arr[j];
                    // xor of cur_segment [i..j] is 0
                    if (xor == 0)
                        // total number of possible values of k in between as k goes from i + 1..j
                        // i.e j - (i + 1) + 1 ==> j - i values in total
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
            int l = word1.Length;
            if (l != word2.Length) return false;    // different length words


            int[] map1 = new int[26];
            int[] map2 = new int[26];
            for (int i = 0; i < l; i++)
            {
                map1[word1[i] - 'a']++;
                map2[word2[i] - 'a']++;
            }

            Dictionary<int, int> countMap1 = new Dictionary<int, int>(26);
            Dictionary<int, int> countMap2 = new Dictionary<int, int>(26);
            for (int i = 0; i < 26; i++)
            {
                // check if all unique characters are same in both strings
                if ((map1[i] == 0 && map2[i] != 0) || (map2[i] == 0 && map1[i] != 0)) return false;

                if (countMap1.ContainsKey(map1[i])) countMap1[map1[i]]++;
                else countMap1.Add(map1[i], 1);

                if (countMap2.ContainsKey(map2[i])) countMap2[map2[i]]++;
                else countMap2.Add(map2[i], 1);
            }
            // check if all unique characters have same count i.e. 3 characters from word1
            // will only match if there is any characters in word2 which is also present 3 times
            foreach (var kvp in countMap1)
                if (!countMap2.ContainsKey(kvp.Key) || countMap2[kvp.Key] != kvp.Value) return false;

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
                    if (i == len)               // if valid IP i.e. no more remaining numbers
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
            //even.Add('a'); even.Add('e'); even.Add('i'); even.Add('o'); even.Add('u');
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
            // Local func to get length of integer in binary i.e. no of 1's & 0's by using formula => 1 + Log (base2) of Num
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

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < min)
                    // if curr num is odd try multiple by 2 to reduce deviation
                    if (nums[i] % 2 == 1 && (nums[i] <= max / 2 || Math.Abs(nums[i] * 2 - max) < Math.Abs(nums[i] - min))) nums[i] *= 2;
                else if (nums[i] > max)
                    // if curr num is even try reducing by half to reduce deviation
                    while (nums[i] % 2 == 0 && (nums[i] / 2 >= min || Math.Abs(nums[i] / 2 - min) < Math.Abs(nums[i] - max))) nums[i] /= 2;
                min = Math.Min(min, nums[i]);
                max = Math.Max(max, nums[i]);
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
            /* Traversing from the starting index
             * if stack is empty add current digit to stack if num is not '0'
             * else if check while Last Inserted digit is > current digit than remove stack top and decreament 'k'
             * 
             * Now if k > 0 remove st.Top() & decreament 'k' untill k == 0
             * 
             * at end pull out all digits from stack and reverse & return
             */
            int l = num.Length;
            if (k >= l) return "0";
            Stack<char> st = new Stack<char>();

            for (int i = 0; i < l; i++)
                if (k > 0)
                {
                    // while we have digit greater than current digit as stack top, Pop stack
                    while (st.Count > 0 && st.Peek() - '0' > num[i] - '0')
                    {
                        st.Pop();
                        if (--k == 0) break;
                    }

                    // if stack is empty and next digit to be pushed it 0 than skip adding leading zeros
                    while (st.Count == 0 && num[i] == '0' && i < l - 1) i++;
                    st.Push(num[i]);
                }
                else st.Push(num[i]);
            
            while (k-- > 0 && st.Count > 1) st.Pop();

            return new string(st.Reverse().ToArray());
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
                // Now check each letter for window starting at index 'i' and ending at 'i+m-1'
                for (int j = 0; j < m; j++)         // O(m)
                    if (target[i + j] == stamp[j])
                        made.Add(i + j);
                    else
                        todo.Add(i + j);
                
                ls.Add(new StampWindow(made, todo));// update made and todo list for window starting at 'i' index in list

                if (todo.Count == 0)                // all letters matched
                {
                    ans.Push(i);                    // push starting index of window in 'ans' Stack
                    foreach (int index in made)     // also mark all the indicies in Target which have been stamped now
                        if(!stamped[index])
                        {
                            indiciesStamped++;
                            q.Enqueue(index);
                            stamped[index] = true;
                        }
                }
            }
            
            while(q.Count>0)
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
            while (ans.Count > 0) result[i++] = ans.Pop();      // starting indicies are saved into result in reverse order i.e from first stamp to last stamp made

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


        // Time O(m*klogk) Space O(n), n = length of nums, m = length of l, k = avg diff r[i]-l[i]
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



    }
}
