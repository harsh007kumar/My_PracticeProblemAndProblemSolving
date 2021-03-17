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
}
