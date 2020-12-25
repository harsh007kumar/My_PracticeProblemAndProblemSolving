﻿using System;
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


        // Iterative InOrder traversal, Time O(N) Space O(H)
        public static TreeNode IncreasingOrderSearchTree(TreeNode root)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode newHead = null, prv = null;
            while (true)
            {
                while(root!=null)
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
            int seen_once=0, seen_twice = 0;
            for(int i=0;i<nums.Length;i++)
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
                switch(direction)
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
            
            return sb[sb.Length-1];
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

            return S[i-1];
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

    }
}
