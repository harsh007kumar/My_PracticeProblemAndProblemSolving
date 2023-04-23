namespace InterviewProblemNSolutions
{
    public static class BitManipulation
    {
        // Time O(n) Space O(1)
        public static int[] ShuffleTheArray(int[] nums, int n)
        {
            /* Since the max int given in array is 10^3 thats will take max right most 10bits to store
            we still have 32-10 around 22 idle bits,
            so plan is to utilize unsed space and store 2 more in one (we can practically store 3 but 2 solves r purpose)
            rt most 10 bits for x nums 00000000-00000000-00000011-11111111
            10 bits after that fr y nums 00000000-00001111-11111100-00000000

            once we have store entire series in first n index (eaech index has 2 numbers one from x & one from y)
            we just finally fill the index from last i.e. 2n-1 to 0
            y nums must be at 2*i+1 index
            x numbers be at 2+i index
            */

            // store x & y series numbers together
            for (int i = 0; i < n; i++)
                nums[i] |= nums[i + n] << 10;
            int allOnes = 1023;
            for (int i = n - 1; i >= 0; i--)
            {
                nums[(2 * i) + 1] = nums[i] >> 10;    // update 2nd
                nums[2 * i] = nums[i] & allOnes;      // update 1st
            }
            return nums;
        }

        // Time O(n) 2 pass Space O(n)
        public static int[] ShuffleTheArrayExtraSpace(int[] nums, int n)
        {
            int[] firstHalf = new int[n];
            for (int i = 0; i < n; i++)
                firstHalf[i] = nums[i];

            for (int i = 0; i < 2 * n; i += 2)
            {
                // update first var
                nums[i] = firstHalf[i / 2];
                // update second var
                nums[i + 1] = nums[i / 2 + n];
            }
            return nums;
        }
    }
}
