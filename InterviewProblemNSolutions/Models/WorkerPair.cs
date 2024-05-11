namespace InterviewProblemNSolutions.Models
{
    public class WorkerPair
    {
        public int quality, wage;
        public double costPerSingleUnitOfQuality;
        public WorkerPair(int q, int w)
        {
            quality = q;
            wage = w;
            costPerSingleUnitOfQuality = w / (double)q;
        }
    }
}
