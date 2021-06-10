using System;
using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    public class CalendarNode
    {
        public int start, end;
        public CalendarNode left, right;
        public CalendarNode(int start, int end)
        {
            this.start = start;
            this.end = end;
            left = right = null;
        }
    }

    public class MyCalendar
    {
        CalendarNode root;
        public MyCalendar() => root = null;
        public bool Book(int start, int end) => Insert(ref root, start, end);

        bool Insert(ref CalendarNode parent, int s, int e)      // O(logn)
        {
            if (parent == null)
            {
                parent = new CalendarNode(s, e);
                return true;
            }
            // new entry endDate is <= current node startDate, try inserting at Left
            else if (e <= parent.start)
                return Insert(ref parent.left, s, e);
            // new entry startDate is >= current node endDate, try inserting at Right
            else if (s >= parent.end)
                return Insert(ref parent.right, s, e);
            // neither option works mean we have an over-lapping date, hence Return False & don't insert anything
            else
                return false;
        }
    }

    /**
     * Your MyCalendar object will be instantiated and called as such:
     * MyCalendar obj = new MyCalendar();
     * bool param_1 = obj.Book(start,end);
     */
}
