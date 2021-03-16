using System;

namespace LinkedList
{
    public class CompareSample
    {
        public void RunCompareSample()
        {
            Compare compare = new Compare();

            Node p = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            Node q = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            int result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { 1, 2, 3, 4 });
            q = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { 1, 2, 3, 5 });
            q = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { 2 });
            q = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { 2 });
            q = Common.BuildList(new int[] { 1 });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { 2 });
            q = Common.BuildList(new int[] { });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { });
            q = Common.BuildList(new int[] { 2 });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));

            p = Common.BuildList(new int[] { });
            q = Common.BuildList(new int[] { });
            result = compare.CompareList(p, q);
            Console.WriteLine("{0} {1} {2}", Common.ToString(p), ToString(result), Common.ToString(q));
        }

        private string ToString(int result)
        {
            if (result == 0)
            {
                return "=";
            }
            else if (result < 0)
            {
                return "<";
            }
            else
            {
                return ">";
            }
        }
    }

    public class Compare
    {
        public int CompareList(Node p, Node q)
        {
            Node pn = p;
            Node qn = q;
            int result = 0;
            while (pn != null || qn != null)
            {
                if (pn == null && qn != null)
                {
                    result = -1;
                    break;
                }
                else if (pn != null && qn == null)
                {
                    result = 1;
                    break;
                }
                else if (pn.V == qn.V)
                {
                    pn = pn.Next;
                    qn = qn.Next;
                }
                else if (pn.V > qn.V)
                {
                    result = 1;
                    break;
                }
                else
                {
                    result = -1;
                    break;
                }
            }
            return result;
        }
    }
}