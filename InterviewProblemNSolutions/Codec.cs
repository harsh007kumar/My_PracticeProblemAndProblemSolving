using System.Collections.Generic;
using System.Text;

namespace InterviewProblemNSolutions
{
    // Chunked Transfer Encoding
    public class Codec
    {

        // Encode integer into 4 byte char array
        string IntToString(int num)
        {
            char[] cArr = new char[4];
            for (int i = 3; i >= 0; i--)
                // most significant byte is stored first than remaing ones
                cArr[3 - i] = (char)((num >> (i * 8)) & 255);
            return new string(cArr);
        }
        // Time = O(n) || Space = O(n*(m+4)), n = len of strs & m = avg length of each word
        // Encodes a list of strings to a single string.
        public string Encode(IList<string> strs)
        {
            /* Iterate over the array of chunks, i.e. strings.
             * For each chunk compute its length, and convert that length into 4-bytes string.
             * Append to encoded string :
             *      4-bytes string with information about chunk size in bytes.
             *      Chunk itself.
             * Return encoded string.
             */
            StringBuilder sb = new StringBuilder();
            foreach (var str in strs)
            {
                sb.Append(IntToString(str.Length));
                sb.Append(str);
            }
            return sb.ToString();
        }


        // Time = O(n) || Space = O(m*k), n = len of s & m = avg length of each word & k = no of words
        // Decodes a single string to a list of strings.
        public IList<string> Decode(string s)
        {
            /* Iterate over the encoded string with a pointer i initiated as 0. While i < n:
             * Read 4 bytes s[i: i + 4]. It's chunk size in bytes. Convert this 4-bytes string to integer length.
             * Move the pointer by 4 bytes i += 4.
             * Append to the decoded array string s[i: i + length].
             * Move the pointer by length bytes i += length.
             * Return decoded array of strings.
             */
            IList<string> ans = new List<string>();
            int i = 0, slen = 0, readTill = 0;
            StringBuilder sb = new StringBuilder();

            while (i < s.Length)
            {
                //slen = StringToInt(s.Substring(i, 4));
                slen = StringToInt("" + s[i] + s[i + 1] + s[i + 2] + s[i + 3]);
                i += 4;   // move idx 4 byte/char frwd to point where actual string starts as per our encoding
                readTill = i + slen;
                while (i < readTill)
                    sb.Append(s[i++]);

                ans.Add(sb.ToString()); // add original word/string to list
                sb.Clear();             // clear for next time
            }
            return ans;
        }
        // Deccode integer from 4 byte char array in form of string
        int StringToInt(string s)
        {
            StringBuilder sb = new StringBuilder();
            int ans = 0;
            foreach (var ch in s)
                ans = (ans << 8) + (int)ch;
            return ans;
        }
    }

    // Your Codec object will be instantiated and called as such:
    // Codec codec = new Codec();
    // codec.decode(codec.encode(strs));
}
