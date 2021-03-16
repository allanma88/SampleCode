using System;

namespace Dp
{
    public class EditDistanceSample
    {
        public void Test()
        {
            EditDistance editDistance = new EditDistance();
            string s, t;
            s = "intention";
            t = "execution";
            Test(editDistance, s, t);
        }

        private void Test(EditDistance editDistance, string s, string t)
        {
            int d = editDistance.MinDistance(s, t);
            Console.WriteLine($"{s} and {t} edit distance is : {d}");
        }
    }

    public class EditDistance
    {
        public int MinDistance(string s, string t)
        {
            throw new NotImplementedException();
        }
    }
}