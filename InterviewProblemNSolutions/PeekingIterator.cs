using System;
using System.Collections;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class PeekingIterator
    {
        int currElement;
        bool cachedLast = false, hasNext = false;
        private readonly IEnumerator iter;

        // iterators refers to the first element of the array.
        public PeekingIterator(IEnumerator iterator)
        {
            // initialize any member here.
            iter = iterator;
        }


        // Returns the next element in the iteration without advancing the iterator.
        public int Peek()
        {
            if (!cachedLast)            // if next element is not already cached, save next in 'peekNext' and return peekNext
            {
                hasNext = iter.MoveNext();
                currElement = hasNext ? (int)iter.Current : -1;
                cachedLast = true;
            }
            return currElement;
        }

        // Returns the next element in the iteration and advances the iterator.
        public int Next()
        {
            if (!cachedLast)
            {
                hasNext = iter.MoveNext();
                currElement = hasNext ? (int)iter.Current : -1;
            }
            cachedLast = false;         // since we have moved 1 position ahead, clear cache
            return currElement;
        }

        // Returns false if the iterator is refering to the end of the array else return true otherwise.
        public bool HasNext()
        {
            if (!cachedLast) Peek();
            return hasNext;
        }

    }
}
