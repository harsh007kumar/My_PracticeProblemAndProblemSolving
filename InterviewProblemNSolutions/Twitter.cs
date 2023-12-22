using System.Collections.Generic;
using System.Linq;

namespace InterviewProblemNSolutions
{
    public class Twitter
    {
        const int recentTweetLimit = 10;
        static int tweetAge = 0;
        readonly Dictionary<int, HashSet<int>> followDict = null;
        readonly Dictionary<int, Queue<Pair>> userTweetDict = null;
        readonly PriorityQueue<int, int> minHeap = null;

        // Constructor
        public Twitter()
        {
            followDict = new Dictionary<int, HashSet<int>>();
            userTweetDict = new Dictionary<int, Queue<Pair>>();
            minHeap = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
        }
        // O(1)
        public void PostTweet(int userId, int tweetId)
        {
            if (!userTweetDict.ContainsKey(userId))
                userTweetDict[userId] = new Queue<Pair>();
            else if (userTweetDict[userId].Count >= recentTweetLimit)
                userTweetDict[userId].Dequeue();    // remove oldest tweet from start as we don't need more than 10

            // update the tweet timeline & add latest tweet at end
            userTweetDict[userId].Enqueue(new Pair(tweetId, ++tweetAge)); // add latest tweet at end
        }
        // O(m*recentTweetLimit*log(recentTweetLimit)), m = avg no of followees followed by a user & recentTweetLimit = 10
        public IList<int> GetNewsFeed(int userId)
        {
            minHeap.Clear();    // flush exisiting elements
                                // first add self tweets to the Heap
            if (userTweetDict.TryGetValue(userId, out Queue<Pair> tweets))
                foreach (var selfTweetId in tweets)       // O(recentTweetLimit)
                    // since we are keeping only recentTweetLimit tweets for each user we are not checking Heap size
                    minHeap.Enqueue(selfTweetId.tweet, selfTweetId.time);           // O(log(recentTweetLimit))

            // now add tweets by followee's to the Heap
            if (followDict.TryGetValue(userId, out HashSet<int> followeeSet))
                foreach (var followee in followeeSet)             // O(m)
                    if (userTweetDict.TryGetValue(followee, out Queue<Pair> followeeTweets))
                        foreach (var followeeTweet in followeeTweets)       // O(recentTweetLimit)
                            if (minHeap.Count < recentTweetLimit)
                                minHeap.Enqueue(followeeTweet.tweet, followeeTweet.time);       // O(log(recentTweetLimit))
                            // more recent tweet found
                            else if (minHeap.TryPeek(out int tweet, out int time) && followeeTweet.time > time)
                                // remove Heap top and immediately add new element
                                minHeap.DequeueEnqueue(followeeTweet.tweet, followeeTweet.time);// O(log(recentTweetLimit))

            // Create the result of most recent tweets followed by user
            var result = new List<Pair>();

            // pull all the elements from the Heap to a list
            while (minHeap.TryDequeue(out int tweet, out int time))                 // O(recentTweetLimit)
                result.Add(new Pair(tweet, time));              // O(log(recentTweetLimit))

            // Sort the tweets in list by descending order i.e. most recent being at the start
            var sortedTweets = (from tweetPair in result        // O(log(recentTweetLimit))
                                orderby -tweetPair.time
                                select tweetPair.tweet).ToList();
            return sortedTweets;
        }

        // O(1)
        public void Follow(int followerId, int followeeId)
        {
            if (!followDict.ContainsKey(followerId)) followDict[followerId] = new HashSet<int>() { followeeId };
            else followDict[followerId].Add(followeeId);
        }

        // O(1)
        public void Unfollow(int followerId, int followeeId)
        {
            if (followDict.ContainsKey(followerId) && followDict[followerId].Contains(followeeId))
                followDict[followerId].Remove(followeeId);
        }
    }

    public class Pair
    {
        public int tweet, time;
        public Pair(int tw, int t)
        {
            tweet = tw;
            time = t;
        }
    }
}
