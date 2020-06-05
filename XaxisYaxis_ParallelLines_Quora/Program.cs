using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace XaxisYaxis_ParallelLines_Quora
{
    //Quora https://www.quora.com/What-are-some-interesting-questions-asked-in-an-interview/answer/Mohit-Agrawal-95?ch=10&share=c6667cc3&srid=uC93P
    class Program
    {
        static void Main(string[] args)
        {
            int NoOfLines = 5,
                index = 0,
                yAxis = 1,
                firstPointOnXAxis = 4,
                lastPointOnXAxis,
                i,
                lineLength = 5,
                noOfVerticalLines = 0,
                noOfLinesStillVisible = NoOfLines;
            Line[] lineArray = new Line[NoOfLines];

            //loop to initializes/create lines in array
            for (i = firstPointOnXAxis; i < 10000; i = i * index)
            {
                // inserting lines of length = (lineLength + 1) and each line is 1 coordinate above the previous line wrt to Y axis.
                lineArray[index++] = new Line(i, yAxis, i + lineLength, yAxis++);
                if(index>=NoOfLines) break;
            }

            lastPointOnXAxis = i+lineLength;
            int maxNoOfLineIntersectingOnAnyPoint = 0, x_CoordinateIntersectingMaxLines = 0;
            //Loop until all lines parallel to X axis are covered/hidden
            while (noOfLinesStillVisible > 0)
            {
                maxNoOfLineIntersectingOnAnyPoint = 0;
                //Loop from Starting point on X axis to last point on X axis (all lines are lying are within these two points)
                for (int x = firstPointOnXAxis; x <= lastPointOnXAxis; x++)
                {
                    int NoOfLineIntersectingOnGivenPoint = 0;

                    //loop to traverse thru array of lines
                    for (int eachLine = 0; eachLine < NoOfLines; eachLine++)
                        // Check if given point x lies within start and end of a line which are still visible
                        if (lineArray[eachLine].IsVisible && lineArray[eachLine].PointLiesOnXAxisOfLine(x))
                                NoOfLineIntersectingOnGivenPoint++;

                    if (NoOfLineIntersectingOnGivenPoint > maxNoOfLineIntersectingOnAnyPoint)
                    {
                        maxNoOfLineIntersectingOnAnyPoint = NoOfLineIntersectingOnGivenPoint;
                        x_CoordinateIntersectingMaxLines = x;
                    }
                }
                //Func to hide all lines which contain X point i.e point which passes thru max no of lines
                HideLine(ref lineArray, x_CoordinateIntersectingMaxLines, ref noOfVerticalLines, ref noOfLinesStillVisible);
            }

            Console.WriteLine($"No Of Vertical Lines : {noOfVerticalLines}\nrequired to Hide all lines parallel to x axis");
            Console.ReadKey();
        }

        /// <summary>
        /// Hide Line which contain Given X Coordinate
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="x"></param>
        /// <param name="noOfVerticalLines"></param>
        /// <param name="noOfLinesStillVisible"></param>
        public static void HideLine(ref Line[] lines, int x,ref int noOfVerticalLines,ref int noOfLinesStillVisible)
        {
            for (int eachLine = 0; eachLine < lines.Length; eachLine++)
                if (lines[eachLine].IsVisible)
                    if (lines[eachLine].PointLiesOnXAxisOfLine(x))
                    {
                        lines[eachLine].IsVisible = false;                                          // Hiding all lines which with pass thru given X coordinate
                        noOfLinesStillVisible--;
                    }
            noOfVerticalLines++;
        }

    }

    // To store a point on X-Y axis graph
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    // Class to represent Lines
    public class Line
    {
        public Point Start;
        public Point End;
        public int Length { get; set; }
        public bool IsVisible { get; set; }

        public Line(int x1, int y1, int x2, int y2)
        {
            Start = new Point(x1, y1);
            End = new Point(x2, y2);
            IsVisible = true;
            Length = x2 - x1 + 1;
        }

        /// <summary>
        /// Returns True if Point Lies On X Axis Of the Line else returns False
        /// </summary>
        /// <param name="xPoint"></param>
        /// <returns></returns>
        public bool PointLiesOnXAxisOfLine(int xPoint)
        {
            return (xPoint >= Start.X && xPoint <= End.X) ? true : false;
        }
    }

}
