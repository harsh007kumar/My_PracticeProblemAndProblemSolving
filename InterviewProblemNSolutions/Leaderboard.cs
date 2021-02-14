using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class Leaderboard
    {
        readonly Dictionary<int, int> playerScores;
        int totalScore = 0;
        // Constructor
        public Leaderboard() => playerScores = new Dictionary<int, int>();

        // Time O(1)
        public void AddScore(int playerId, int score)
        {
            Console.WriteLine($" Adding/Updating score for player: \tId: {playerId} || Score: {score} ");
            if (!playerScores.ContainsKey(playerId)) playerScores.Add(playerId, score);
            else playerScores[playerId] += score;
            totalScore += score;
        }

        // Time O(nlogk), worst case when K = playerScores.Count-1
        public int Top(int K)
        {
            Console.Write($" Returning Sum of Score for Top \'{K}\' players: ");
            if (K == playerScores.Count) return totalScore;

            int topK = 0;
            // Idea here is to maintain MinHeap of size 'K' for highest scores of players
            MinHeap h = new MinHeap(K);
            foreach (var individualScore in playerScores.Values)
                if (h.Count < K)
                {
                    h.Insert(individualScore);
                    topK += individualScore;
                }
                else if (h.arr[0] < individualScore)
                {
                    topK -= h.ExtractMin();         // Remove current HeapMin
                    topK += individualScore;        // Update totalScore so far
                    h.Insert(individualScore);      // Insert new highScore
                }
            return topK;
        }

        // Time O(1)
        public void Reset(int playerId)
        {
            Console.WriteLine($" Reseting/Removing Player with id: \'{playerId}\' from leadership board");
            totalScore -= playerScores[playerId];
            playerScores.Remove(playerId);
        }
    }
}
