namespace InterviewProblemNSolutions
{
    class CircularQueue
    {
        int[] A;
        int count = 0, front, rear, len;

        public CircularQueue(int k)
        {
            A = new int[k];
            front = rear = -1;
            len = k;
        }

        public bool EnQueue(int value)
        {
            if (IsFull()) return false;
            if (++count == 1)   // 1st entry
                front = 0;
            if (++rear >= len)
                rear = 0;
            A[rear] = value;    // add to queue back
            return true;
        }

        public bool DeQueue()
        {
            if (IsEmpty()) return false;
            if (--count == 0)   // last entry
                front = rear = -1;
            else                // remove from queue front
                if (++front >= len)
                front = 0;
            return true;
        }

        public int Front() => IsEmpty() ? -1 : A[front];

        public int Rear() => IsEmpty() ? -1 : A[rear];

        public bool IsEmpty() => count == 0;

        public bool IsFull() => count == len;
    }

    /**
     * Your MyCircularQueue object will be instantiated and called as such:
     * MyCircularQueue obj = new MyCircularQueue(k);
     * bool param_1 = obj.EnQueue(value);
     * bool param_2 = obj.DeQueue();
     * int param_3 = obj.Front();
     * int param_4 = obj.Rear();
     * bool param_5 = obj.IsEmpty();
     * bool param_6 = obj.IsFull();
     */
}
