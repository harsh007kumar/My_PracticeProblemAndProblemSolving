using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class GenerateRandomPointInACircle
    {
        private readonly double r, x, y;
        private readonly Random rand;
        
        // Constructor
        public GenerateRandomPointInACircle(double radius, double x_center, double y_center)
        {
            r = radius;
            x = x_center;
            y = y_center;
            rand = new Random();
        }

        // Time = Space = O(1)
        public double[] RandPointInsideCircle()
        {
            /* Generating random length radius [b/w  0 & radius].
             * 
             * Than randomly generate an integer to use an angle with X-Axis [b/w 0 to 360]
             * 
             * Than using Sin & Tan theta calculate Opposite & Adjacent side length 
             *      (height & base of rt-angle triangle) using hypotenuse & Angle
             * 
             * Next find Qudrent where point would lie based upon 'randomAngle / 90'
             * 
             * and Return the newly generating point.
             * 
             * LeetCode doesnt accept this approach as its not random enouf for their test-cases
             */
            var randomLenPercentageOfOriginalRadius = rand.Next(101);

            if (randomLenPercentageOfOriginalRadius == 0)
                return new double[] { 0, 0 };
            var randomLen = r * (randomLenPercentageOfOriginalRadius / 100);

            var randomAngle = rand.Next(360);
            
            // calculate Oppsite side len of rt-angle triangle
            var oppSideLen = Math.Sin(randomAngle) * r;

            // calculate Adjacent side len of rt-angle triangle
            var adjSideLen = oppSideLen / Math.Tan(randomAngle);

            double x1 = 0, y1 = 0;
            var quadrent = randomAngle / 90;
            switch(quadrent)
            {
                case 0:
                    x1 = x + adjSideLen;
                    y1 = y + oppSideLen;
                    break;
                case 1:
                    x1 = x - oppSideLen;
                    y1 = y + adjSideLen;
                    break;
                case 2:
                    x1 = x - adjSideLen;
                    y1 = y - oppSideLen;
                    break;
                case 3:
                    x1 = x + oppSideLen;
                    y1 = y - adjSideLen;
                    break;
            }

            return new double[] { x1, y1 };
        }
    }

    public class RandomPointInACircle
    {
        private readonly double r, x, y;
        private readonly Random rand;
        public RandomPointInACircle(double radius, double x_center, double y_center)
        {
            r = radius;
            x = x_center;
            y = y_center;
            rand = new Random();
        }

        public double[] RandPoint()
        {
            double a = 0, b = 0;
            do
            {
                a = GetRandom(x, r);
                b = GetRandom(y, r);
            } while (!IsValidCorrdinate(a, b));

            return new double[] { a, b };
        }

        // lowest valid X-cordinate would be (x - radius) & max valid X-cordinate would be (x + radius)
        // lowest valid Y-cordinate would be (Y - radius) & max valid Y-cordinate would be (Y + radius)
        // Hence we can conclude that => Random Point can be calculated as lowest possible corrdinate + (random length b/w 0 to twice of radius)
        double GetRandom(double a, double radius) => a - radius + rand.NextDouble() * 2 * radius;

        // Pythagoras theorem gives us the upper bound [ a^2 + b^2 = c^2 ]
        bool IsValidCorrdinate(double a, double b) => ((a - x) * (a - x) + (b - y) * (b - y)) <= r * r;
    }

}
