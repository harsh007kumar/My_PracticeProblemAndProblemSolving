using System.Collections.Generic;

namespace InterviewProblemNSolutions
{
    /* The Knows API is defined in the parent class Relation.
      bool Knows(int a, int b); */
    public class Relation
    {
        public bool Knows(int a, int b) => true;    // this API was provided in question
    }

    public class Celebrity : Relation
    {
        public int FindCelebritySlower(int n)
        {
            var celebList = new Dictionary<int, int>(n);
            var notACeleb = new HashSet<int>(n);

            for (int i = 0; i < n; i++)
                celebList.Add(i,0);

            // Algo
            for (int firstP = 0; firstP < n; firstP++)
            {
                for (int secondP = 0; secondP < n; secondP++)
                {
                    if (secondP == firstP || notACeleb.Contains(firstP)) continue;

                    var secondPKnowFirstP = Knows(secondP, firstP);
                    if (secondPKnowFirstP)  // 'secondP' definitely is not a celebrity
                    {
                        celebList.Remove(secondP);
                        notACeleb.Add(secondP);

                        var firstPKnowSecondP = Knows(firstP, secondP);
                        // 'firstP' also doesn't know secondP we can now increment possibleCelebrity 1st follower count
                        if (!firstPKnowSecondP)
                        {
                            // add one person to count of ppl who know 'firstP'
                            celebList[firstP]++;

                            // Celebrity Found
                            if (celebList[firstP] >= n - 1) return firstP;
                        }
                        else
                        {
                            celebList.Remove(secondP);
                            notACeleb.Add(firstP);
                        }
                    }
                    else                    // 'firstP' is definitely not a celebrity
                    {
                        celebList.Remove(firstP);
                        notACeleb.Add(firstP);
                    }

                    // no possible celebrity left to check or all person are already added to notACeleb set
                    if (celebList.Count <= 0 || notACeleb.Count >= n) return -1;
                }
            }
            return -1;
        }

        public int FindCelebrityOptimized(int n)
        {
            var celebList = new HashSet<int>(n);
            var notACeleb = new HashSet<int>(n);

            for (int person = 0; person < n; person++)
                celebList.Add(person);

            // Each Knows results in addition of either one of the person to 'NotACeleb' list
            int possibleCeleb = 0, anotherPerson = n - 1;
            while (possibleCeleb < anotherPerson)
            {

                var firstKnowsSecond = Knows(possibleCeleb, anotherPerson);
                if (firstKnowsSecond)
                {
                    // first can't be possibleCeleb, as he knows atleast one person in party
                    notACeleb.Add(possibleCeleb);
                    celebList.Remove(possibleCeleb);
                    possibleCeleb++;
                }
                else
                {
                    // second can't be possibleCeleb, since there is atleast 1 person who doesn't knows him
                    notACeleb.Add(anotherPerson);
                    celebList.Remove(anotherPerson);
                    anotherPerson--;
                }
            }

            // check only remaining person in celeb List he/she is a celeb
            foreach (var person in celebList)
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    if (person == i) continue;
                    // if celeb candidate doesn't know anybody & all other person know celeb ++count
                    if (!Knows(person, i) && Knows(i, person)) count++;
                    else break;
                }
                if (count >= n - 1) return person;
            }
            return -1;
        }
    }
}
