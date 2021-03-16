using System;

namespace QuickSort
{
    public class QuickSorter
    {
        public void Run()
        {
            int[] nums = new int[] { 13, 19, 9, 5, 12, 8, 7, 4, 21, 2, 6, 11 };
            QuickSort(nums);
            Console.WriteLine($"{string.Join(',', nums)}");

            nums = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            QuickSort(nums);
            Console.WriteLine($"{string.Join(',', nums)}");

            nums = new int[] { 4, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 2, 4 };
            QuickSort(nums);
            Console.WriteLine($"{string.Join(',', nums)}");
        }

        public void QuickSort(int[] nums)
        {
            QuickSort2(nums, 0, nums.Length - 1);
        }

        public void QuickSort(int[] nums, int p, int r)
        {
            if (p >= r)
            {
                return;
            }
            int q = Partition1(nums, p, r);
            Console.WriteLine(q);
            QuickSort(nums, p, q - 1);
            QuickSort(nums, q + 1, r);
        }

        public void QuickSort2(int[] nums, int p, int r)
        {
            if (p >= r)
            {
                return;
            }
            var pair = Hoare_Partition2(nums, p, r);
            Console.WriteLine(pair.Item1 + "," + pair.Item2);
            QuickSort2(nums, p, pair.Item1 - 1);
            QuickSort2(nums, pair.Item2 + 1, r);
        }

        private int Partition(int[] nums, int p, int r)
        {
            int i = p - 1;
            int x = nums[r];
            for (int j = p; j < r; j++)
            {
                if (nums[j] <= x)
                {
                    i++;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
            }

            int temp1 = nums[i + 1];
            nums[i + 1] = nums[r];
            nums[r] = temp1;

            return i + 1;
        }

        private int Partition1(int[] nums, int p, int r)
        {
            int i = p - 1;
            int e = p - 1;
            int x = nums[r];
            for (int j = p; j < r; j++)
            {
                if (nums[j] < x)
                {
                    i++;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;

                    e++;
                    temp = nums[e];
                    nums[e] = nums[j];
                    nums[j] = temp;
                }
                else if (nums[j] == x)
                {
                    e++;
                    int temp = nums[e];
                    nums[e] = nums[j];
                    nums[j] = temp;
                }
            }

            int temp1 = nums[e + 1];
            nums[e + 1] = nums[r];
            nums[r] = temp1;

            return (e + 1 + i + 1) / 2;
        }

        private Tuple<int, int> Partition2(int[] nums, int p, int r)
        {
            int i = p - 1;
            int e = p - 1;
            int x = nums[r];
            for (int j = p; j < r; j++)
            {
                if (nums[j] < x)
                {
                    i++;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;

                    e++;
                    if (i < e)
                    {
                        temp = nums[e];
                        nums[e] = nums[j];
                        nums[j] = temp;
                    }
                }
                else if (nums[j] == x)
                {
                    e++;
                    int temp = nums[e];
                    nums[e] = nums[j];
                    nums[j] = temp;
                }
            }

            int temp1 = nums[e + 1];
            nums[e + 1] = nums[r];
            nums[r] = temp1;

            return new Tuple<int, int>(i + 1, e + 1);
        }

        private int Hoare_Partition(int[] nums, int p, int r)
        {
            int x = nums[r];
            int i = p, j = r - 1;
            while (true)
            {
                while (i <= r - 1 && nums[i] <= x)
                {
                    i++;
                }
                while (j >= 0 && nums[j] > x)
                {
                    j--;
                }
                if (i < j)
                {
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                    i++;
                    j--;
                }
                else
                {
                    break;
                }
            }

            int temp1 = nums[i];
            nums[i] = nums[r];
            nums[r] = temp1;
            return i;
        }

        private Tuple<int, int> Hoare_Partition1(int[] nums, int p, int r)
        {
            int x = nums[r];
            int i = p - 1, j = r - 1;
            int q = i;
            while (true)
            {
                while (i <= r - 1 && nums[++i] <= x)
                {
                    if (nums[i] < x)
                    {
                        q++;
                        int temp = nums[i];
                        nums[i] = nums[q];
                        nums[q] = temp;
                    }
                }
                while (j >= 0 && nums[j] > x)
                {
                    j--;
                }
                if (i < j)
                {
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;

                    if (nums[i] < x)
                    {
                        q++;
                        int temp1 = nums[i];
                        nums[i] = nums[q];
                        nums[q] = temp1;
                    }
                    i++;
                    j--;
                }
                else
                {
                    break;
                }
            }

            int temp2 = nums[i];
            nums[i] = nums[r];
            nums[r] = temp2;
            return new Tuple<int, int>(q + 1, i);
        }

        private Tuple<int, int> Hoare_Partition2(int[] nums, int p, int r)
        {
            int q = p;
            int i = p;
            int j = r;
            int x = nums[r];
            while (i < j)
            {
                if (nums[i] < x)
                {
                    int temp = nums[i];
                    nums[i] = nums[q];
                    nums[q] = temp;
                    i++;
                    q++;
                }
                else if (nums[i] > x)
                {
                    j--;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
                else
                {
                    i++;
                }
            }
            int temp1 = nums[i];
            nums[i] = nums[r];
            nums[r] = temp1;
            return new Tuple<int, int>(q, i);
        }

        private int RandomizedPartition(int[] nums, int p, int r)
        {
            int i = new Random().Next(p, r);
            int temp = nums[i];
            nums[i] = nums[r];
            nums[r] = temp;
            return Partition(nums, p, r);
        }
    }
}