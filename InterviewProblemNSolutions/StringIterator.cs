
namespace InterviewProblemNSolutions
{
    public class StringIterator
    {
        int len = 0;
        int curCharPos = -1;        // stores current Character index in string
        int lastDigitPos = 0;       // stores the lastIndex + 1 of current Characters Digit Count from string
        int currCount = 0;          // how many time current characters needs to be iterated
        private string str;

        public StringIterator(string compressedString)
        {
            len = compressedString.Length;
            str = compressedString;
        }

        // Returns the next character if the original string still has uncompressed characters, otherwise returns a white space.
        public char Next()
        {
            if (currCount <= 0 && lastDigitPos >= len)
                return ' ';
            if (currCount > 0)
            {
                currCount--;
                return str[curCharPos];
            }
            curCharPos = lastDigitPos;      // next character is after last digit last index
            lastDigitPos = curCharPos;

            while (++lastDigitPos < len)    // stop when 1st letter shows up
                if (char.IsLetter(str[lastDigitPos])) break;

            // no of times curr Character needs to be iterated again is calculated by conversting the string of digits right after current character
            // & subtracting 1 as we are reading it once right now
            currCount = int.Parse(str.Substring(curCharPos + 1, lastDigitPos - curCharPos - 1)) - 1;
            return str[curCharPos];
        }

        // Returns true if there is any letter needs to be uncompressed in the original string, otherwise returns false.
        public bool HasNext() => (currCount > 0 || lastDigitPos < len);
    }
}
