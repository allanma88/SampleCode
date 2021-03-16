using System;
using System.Collections.Generic;

namespace Dp
{
    public class CoinExchangeSample
    {
        public void Run()
        {
            CoinExchange exchange = new CoinExchange();
            int[] coins;
            coins = new int[] { 1, 3, 4 };
            Test(exchange, coins, 6);

            coins = new int[] { 9, 3, 8, 6, 5 };
            Test(exchange, coins, 10);

            Test(exchange, new int[] { 2 }, 3);

            Test(exchange, new int[] { 3, 2 }, 3);

            for (int i = 0; i < 10; i++)
            {
                Random random = new Random(i);
                coins = GetNums(random, 5);
                int money = 10;
                Test(exchange, coins, money);
            }
        }

        private void Test(CoinExchange exchange, int[] coins, int money)
        {
            int num = exchange.Exchange1(coins, money);
            int numWithOpt = exchange.ExchangeWithOpt1(coins, money);
            int numWithRecur = exchange.ExchangeWithRecur(coins, money);
            Console.WriteLine($"coins of ({string.Join(",", coins)}), money of {money}, the num is {num}, {numWithOpt}(Opt) and {numWithRecur}(Recur)");
        }

        private int[] GetNums(Random random, int n)
        {
            HashSet<int> set = new HashSet<int>();
            while (set.Count < n)
            {
                set.Add(random.Next(1, 10));
            }
            int[] nums = new int[n];
            int i = 0;
            foreach (int num in set)
            {
                nums[i++] = num;
            }
            return nums;
        }
    }

    public class CoinExchange
    {
        public int ExchangeWithRecur(int[] coins, int money)
        {
            if (money == 0) return 0;
            int min = -1;
            foreach (int coin in coins)
            {
                if (money >= coin)
                {
                    int num = ExchangeWithRecur(coins, money - coin);
                    if (min == -1 || (num != -1 && num < min))
                    {
                        min = num;
                    }
                }
            }
            if (min == -1)
            {
                return -1;
            }
            else
            {
                return min + 1;
            }
        }

        public int ExchangeWithOpt(int[] coins, int money)
        {
            int[] matrix = new int[money + 1];
            for (int i = 1; i <= money; i++)
            {
                int min = -1;
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i >= coins[j])
                    {
                        min = Min(min, matrix[i - coins[j]]);
                    }
                }
                matrix[i] = min;
            }
            return matrix[money];
        }

        public int ExchangeWithOpt1(int[] coins, int money)
        {
            int[] matrix = new int[money + 1];
            int max = money + 1;
            for (int i = 1; i <= money; i++)
            {
                int min = max;
                for (int j = 0; j < coins.Length; j++)
                {
                    if (i >= coins[j])
                    {
                        min = Math.Min(min, matrix[i - coins[j]] + 1);
                    }
                }
                matrix[i] = min;
            }
            return matrix[money] > money ? -1 : matrix[money];
        }

        public int Exchange(int[] coins, int money)
        {
            int[,] matrix = new int[coins.Length + 1, money + 1];
            for (int i = 1; i < money + 1; i++)
            {
                matrix[0, i] = -1;
            }
            for (int i = 1; i < coins.Length + 1; i++)
            {
                for (int j = 1; j < money + 1; j++)
                {
                    if (coins[i - 1] > j)
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                    else
                    {
                        matrix[i, j] = Min(matrix[i - 1, j], matrix[i, j - coins[i - 1]]);
                    }
                }
            }
            return matrix[coins.Length, money];
        }

        public int Exchange1(int[] coins, int money)
        {
            int[,] matrix = new int[coins.Length + 1, money + 1];
            int max = money + 1;
            for (int i = 1; i < money + 1; i++)
            {
                matrix[0, i] = max;
            }
            for (int i = 1; i < coins.Length + 1; i++)
            {
                for (int j = 1; j < money + 1; j++)
                {
                    if (coins[i - 1] > j)
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                    else
                    {
                        matrix[i, j] = Math.Min(matrix[i - 1, j], matrix[i, j - coins[i - 1]] + 1);
                    }
                }
            }
            return matrix[coins.Length, money] > money ? -1 : matrix[coins.Length, money];
        }


        private int Min(int x, int y)
        {
            if (x == -1 && y == -1)
            {
                return -1;
            }
            else if (x == -1)
            {
                return y + 1;
            }
            else if (y == -1)
            {
                return x;
            }
            else
            {
                return Math.Min(x, y + 1);
            }
        }
    }
}