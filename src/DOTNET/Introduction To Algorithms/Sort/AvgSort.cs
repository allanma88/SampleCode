using System;

namespace Sort
{
    public class AvgSort
    {
        public void Run()
        {
            int[] A = Make(10, 0, 20);
            Sort(A, 3);
            Console.WriteLine(string.Join(',', A));
        }

        public void Sort(int[] A, int k)
        {
            int n = A.Length / k;
            int m = A.Length % k;
            for (int i = 0; i < k; i++)
            {
                int len = m-- > 0 ? n + 1 : n;
                SortK(A, i, k, len);
            }
        }

        private void SortK(int[] A, int s, int k, int n)
        {
            MaxHeapify(A, s, k, n);
            while (n > 1)
            {
                Exchange(A, s, s + (n - 1) * k);
                MaxHeapify(A, s, k, --n);
            }
        }

        private void MaxHeapify(int[] A, int s, int k, int n)
        {
            for (int i = Index(n / 2, s, k); i >= s; i -= k)
            {
                while (i < s + (n - 1) * k)
                {
                    int j = i / k + 1;
                    int left = Index(2 * j - 1, s, k);
                    int right = Index(2 * j, s, k);
                    int max = i;
                    if (left <= Index(n, s, k) && A[left] > A[max])
                    {
                        max = left;
                    }
                    if (right <= Index(n, s, k) && A[right] > A[max])
                    {
                        max = right;
                    }
                    if (max != i)
                    {
                        Exchange(A, i, max);
                        i = max;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void Exchange(int[] A, int i, int j)
        {
            int temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }

        private int Index(int i, int s, int k)
        {
            return (i - 1) * k + s;
        }

        private int[] Make(int n, int min, int max)
        {
            Random random = new Random();
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(min, max);
            }
            return A;
        }
    }
}