using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    // Helpful Article: https://leetcode.com/discuss/interview-question/124658/Design-URL-Shortening-service-like-TinyURL

    public class TinyURL
    {
        Dictionary<long, URL> dict;
        long counter = 1;
        static char[] cMap = new char[62]; // for Base 62 Mapping

        // Static Constructor
        static TinyURL()
        {
            int k = 0;
            char[] types = { 'a', 'A', '0' };
            foreach (var characterType in types)
                for (int i = 0; i < 26 && k < 62; i++)
                    cMap[k++] = (char)(i + characterType);
        }

        // Instance Constructor
        public TinyURL() => dict = new Dictionary<long, URL>();

        string GetShortURLFromID(long c)
        {
            List<char> ls = new List<char>();
            while(c>0)
            {
                ls.Add(cMap[c % 62]);
                c /= 62;
            }
            return new string(ls.Reverse<char>().ToArray());
        }

        // Encodes a URL to a shortened URL
        public string Encode(string longUrl)
        {
            var shortURL = GetShortURLFromID(counter);
            dict.Add(counter, new URL(shortURL, longUrl));
            counter++;
            return shortURL;
        }

        long GetIDFromShortURL(string s)
        {
            long id = 0;
            for (int i = 0; i < s.Length; i++)
                if ('a' <= s[i] && s[i] <= 'z')
                    id = id * 62 + (s[i] - 'a');
                else if ('A' <= s[i] && s[i] <= 'Z')
                    id = id * 62 + (s[i] - 'a') + 26;
                else // if ('0' <= s[i] && s[i] <= '9')
                    id = id * 62 + (s[i] - 'a') + 52;
            
            return id;
        }

        // Decodes a shortened URL to its original URL.
        public string Decode(string shortUrl)
        {
            var id = GetIDFromShortURL(shortUrl);
            return dict[id].l;
        }
    }

    public class URL
    {
        public string s, l;
        public URL(string shortURL, string longURL)
        {
            s = shortURL;
            l = longURL;
        }
    }

}
