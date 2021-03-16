using System;
using System.Text;

namespace DevideConquer
{
    public class Closest
    {
        public void Run()
        {
            int[][] points = MakePoints(5);
            int n = 3;
            int[][] closest = KClosest(points, n);
            Console.WriteLine($"{ToString(points)} closest {n} is {ToString(closest)}");
        }

        public int[][] KClosest(int[][] points, int k)
        {
            Sort(points);
            Console.WriteLine(ToString(points));

            int[][] closest = new int[k][];
            for (int i = 0; i < k; i++)
            {
                closest[i] = points[i];
            }
            return closest;
        }

        private void Sort(int[][] points)
        {
            int[][] temp = new int[points.Length][];
            for (int i = 0; i < points.Length; i++)
            {
                temp[i] = new int[2];
            }
            MergeSort(points, temp, 0, points.Length - 1);
        }

        private void MergeSort(int[][] points, int[][] temp, int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }
            int mid = (lo + hi) / 2;
            MergeSort(points, temp, lo, mid);
            MergeSort(points, temp, mid + 1, hi);
            Merge(points, temp, lo, mid, hi);
        }

        private void Merge(int[][] points, int[][] temp, int lo, int mid, int hi)
        {
            for (int k = lo; k <= hi; k++)
            {
                temp[k] = points[k];
            }
            int i = lo, j = mid + 1;
            for (int k = lo; k <= hi; k++)
            {
                if (i > mid)
                {
                    points[k] = temp[j++];
                }
                else if (j > hi)
                {
                    points[k] = temp[i++];
                }
                else if (Math.Pow(temp[i][0], 2) + Math.Pow(temp[i][1], 2) < Math.Pow(temp[j][0], 2) + Math.Pow(temp[j][1], 2))
                {
                    points[k] = temp[i++];
                }
                else
                {
                    points[k] = temp[j++];
                }
            }
        }

        private int[][] MakePoints(int n)
        {
            int[][] points = new int[n][];

            for (int i = 0; i < n; i++)
            {
                points[i] = new int[2];
            }

            points[0][0] = -1;
            points[0][1] = -2;

            points[1][0] = 2;
            points[1][1] = 3;

            points[2][0] = -4;
            points[2][1] = 7;

            points[3][0] = 11;
            points[3][1] = 5;

            points[4][0] = 6;
            points[4][1] = -9;

            return points;
        }

        private string ToString(int[][] points)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[ ");
            for (int i = 0; i < points.Length; i++)
            {
                builder.Append("[");
                builder.Append(points[i][0]);
                builder.Append(",");
                builder.Append(points[i][1]);
                builder.Append("] ");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}