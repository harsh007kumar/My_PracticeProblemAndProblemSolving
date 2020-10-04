using System;
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


        /// <summary>
        /// Time O(n*m), where n = length of first number and m = length of second number || Space O(m)
        /// Returns multiplication of two numbers very long numbers(can't fit in memory, numbers can be of 500+ digits long)
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="len1"></param>
        /// <param name="num2"></param>
        /// <param name="len2"></param>
        /// <returns></returns>
        public static string MultiplyLargeNumbersRepresentedAsString(string num1,int len1,string num2, int len2)
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
                if(carry>0) listOfMultiplications[i] = carry + listOfMultiplications[i];
            }

            // Now we have the result of multiplications with individual digits at each index,
            // sum all these rows(list of strings to get final result)
            return SumUpNumbersStoredAsListOfStrings(listOfMultiplications, len2);
        }

        public static string SumUpNumbersStoredAsListOfStrings(string[] numList, int len)
        {
            for(int i=1;i<len;i++)
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
                    int digitsSum = (int)Char.GetNumericValue(numList[i][j]) + (int)Char.GetNumericValue(numList[i-1][j]);
                    var carryAdded = digitsSum + carry;
                    sum = carryAdded % 10 + sum;
                    carry = carryAdded / 10;
                }
                // append any left over carry to the result
                if (carry > 0) sum = carry + sum;
                numList[i] = sum;
            }
            return numList[len-1];          // last string now has sum of all the numbers above it in the list
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
            if (input == null ) return -1;

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

            while (q.Count>0)
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
    }
}
