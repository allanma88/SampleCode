using System;
using System.Collections.Generic;
using System.Linq;

namespace Classic
{
    public class MyQueue
    {
        private HashSet<Point> diagonals = new HashSet<Point>();
        private int n;

        public void Run(int n)
        {
            this.n = n;
            var pointList = GetQueues(n);
            foreach (var points in pointList)
            {
                foreach (var p in points)
                {
                    Console.Write(p + "  ");
                }
                Console.WriteLine();
            }
        }

        public List<List<Point>> GetQueues(int n)
        {
            List<List<Point>> pointList = new List<List<Point>>();
            int x = 0;
            int y = 0;
            Point p;
            while (true)
            {
                p = new Point(x, y);
                var initRect = new Rect(new Point(0, 0), new Point(n - 1, n - 1));
                var points = GetPoints(p, initRect);
                if (points.Count == n)
                {
                    pointList.Add(points);
                }
                diagonals.Clear();
                x = x + 1;
                if (x == n - 1 && y == n - 1)
                {
                    break;
                }
                if (x >= n - 1)
                {
                    x = 0;
                    y = (y + 1) % n;
                }
            }
            return pointList;
        }

        private List<Point> GetPoints(Point point, Rect initRect)
        {
            List<Point> points = new List<Point>();
            Stack<Rect> stack = new Stack<Rect>();
            stack.Push(initRect);
            points.Add(point);
            FillDiagonals(point);
            while (stack.Count > 0)
            {
                var rect = stack.Pop();
                var subRects = Cut(rect, points);
                if (subRects.Count > 0)
                {
                    var p = GetPoint(subRects[subRects.Count - 1]);
                    if (p != null)
                    {
                        points.Add(p);
                        FillDiagonals(p);
                        stack.Push(subRects[0]);
                    }
                    for (int i = 1; i < subRects.Count; i++)
                    {
                        stack.Push(subRects[i]);
                    }
                }
            }
            return points;
        }

        public List<Rect> Cut(Rect rect, List<Point> points)
        {
            List<Rect> rects = new List<Rect>();
            Point start = rect.Start;
            Point end = rect.End;
            var xCuts = points.Select(x => x.X).Where(x => x >= start.X && x <= end.X).OrderBy(x => x).ToList();
            var yCuts = points.Select(y => y.Y).Where(y => y >= start.Y && y <= end.Y).OrderBy(y => y).ToList();
            xCuts.Add(end.X + 1);
            yCuts.Add(end.Y + 1);
            int sx = start.X, ex;
            int xc = 0;
            while (xc < xCuts.Count)
            {
                if (sx == xCuts[xc])
                {
                    sx++;
                }
                else
                {
                    ex = xCuts[xc] - 1;
                    int sy = start.Y, ey;
                    int yc = 0;
                    while (yc < yCuts.Count)
                    {
                        if (sy == yCuts[yc])
                        {
                            sy++;
                        }
                        else
                        {
                            ey = yCuts[yc] - 1;
                            var r = new Rect(sx, ex, sy, ey);
                            rects.Add(r);
                            sy = yCuts[yc] + 1;
                        }
                        yc++;
                    }
                    sx = xCuts[xc] + 1;
                }
                xc++;
            }
            return rects;
        }

        private Point GetPoint(Rect rect)
        {
            Point start = rect.Start;
            Point end = rect.End;
            int x = start.X;
            int y = start.Y;
            Point p = null;
            do
            {
                p = new Point(x, y);
                if (!diagonals.Contains(p))
                {
                    break;
                }
                x = x + 1;
                if (x > end.X)
                {
                    x = start.X;
                    y = y + 1;
                }
            }
            while (x != end.X || y < end.Y);

            if (p != null && !diagonals.Contains(p))
            {
                return p;
            }
            return null;
        }

        private void FillDiagonals(Point p)
        {
            diagonals.Add(p);
            int x = p.X + 1;
            int y = p.Y + 1;
            while (x >= 0 && x < n && y >= 0 && y < n)
            {
                diagonals.Add(new Point(x, y));
                x = x + 1;
                y = y + 1;
            }
            x = p.X + 1;
            y = p.Y - 1;
            while (x >= 0 && x < n && y >= 0 && y < n)
            {
                diagonals.Add(new Point(x, y));
                x = x + 1;
                y -= 1;
            }
            x = p.X - 1;
            y = p.Y + 1;
            while (x >= 0 && x < n && y >= 0 && y < n)
            {
                diagonals.Add(new Point(x, y));
                x -= 1;
                y += 1;
            }
            x = p.X - 1;
            y = p.Y - 1;
            while (x >= 0 && x < n && y >= 0 && y < n)
            {
                diagonals.Add(new Point(x, y));
                x -= 1;
                y -= 1;
            }
        }

        public class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }

            public int Y { get; set; }

            public override int GetHashCode()
            {
                return (((X << 5) + X) ^ Y);
            }

            public override bool Equals(object obj)
            {
                Point p = obj as Point;
                if (p != null)
                {
                    return p.X == X && p.Y == Y;
                }
                return false;
            }

            public override string ToString()
            {
                return string.Format("x={0},y={1}", X, Y);
            }
        }

        public class Rect
        {
            public Rect(int startX, int endX, int startY, int endY)
            {
                Start = new Point(startX, startY);
                End = new Point(endX, endY);
            }

            public Rect(Point start, Point end)
            {
                Start = start;
                End = end;
            }

            public Point Start { get; set; }

            public Point End { get; set; }
        }
    }

    public class Queue_BackTrack
    {
        public void Run(int n)
        {
            List<int[]> queues = GetQueues(n);
            foreach (var queue in queues)
            {
                for (int y = 0; y < queue.Length; y++)
                {
                    int x = queue[y];
                    Console.Write("x = {0}, y = {1} ", x, y);
                }
                Console.WriteLine();
            }
        }

        public List<int[]> GetQueues(int n)
        {
            List<int[]> queues = new List<int[]>();
            int[] queue = new int[n];
            for (int i = 0; i < n; i++)
            {
                queue[i] = -1;
            }
            int[] regs = new int[4];
            GetQueues(n, 0, regs, queue, queues);
            return queues;
        }

        private void GetQueues(int n, int y, int[] regs, int[] queue, List<int[]> queues)
        {
            for (int x = 0; x < n; x++)
            {
                if (!TestBit(regs[0], x) && !TestBit(regs[1], y) && !TestBit(regs[2], x + y) && !TestBit(regs[3], (x - y) + (n - 1)))
                {
                    queue[y] = x;
                    regs[0] = SetBit(regs[0], x);
                    regs[1] = SetBit(regs[1], y);
                    regs[2] = SetBit(regs[2], x + y);
                    regs[3] = SetBit(regs[3], (x - y) + (n - 1));

                    //可以优化为全局数组，选择新的行时修改数组，backtrack的时候restore
                    int[] newQueue = new int[queue.Length];
                    queue.CopyTo(newQueue, 0);

                    if (y == n - 1)
                    {
                        queues.Add(newQueue);
                    }
                    else
                    {
                        GetQueues(n, y + 1, regs, newQueue, queues);
                    }
                    queue[y] = -1;
                    regs[0] = UnSetBit(regs[0], x);
                    regs[1] = UnSetBit(regs[1], y);
                    regs[2] = UnSetBit(regs[2], x + y);
                    regs[3] = UnSetBit(regs[3], (x - y) + (n - 1));
                }
            }
        }

        private bool TestBit(int b, int i)
        {
            return ((b >> i) & 1) > 0;
        }

        private int SetBit(int b, int i)
        {
            int j = 1 << i;
            return b | j;
        }

        private int UnSetBit(int b, int i)
        {
            int j = 1 << i;
            return b ^ j;
        }
    }

    public class Queue
    {
        public void Run(int n)
        {
            List<int[]> queues = GetQueues(n);
            foreach (var queue in queues)
            {
                for (int y = 0; y < queue.Length; y++)
                {
                    int x = queue[y];
                    Console.Write("x = {0}, y = {1} ", x, y);
                }
                Console.WriteLine();
            }
        }

        public List<int[]> GetQueues(int n)
        {
            List<int[]> matricList = new List<int[]>();
            int[] row = new int[4 * n];
            row[0] = 1;
            row[n] = 1;
            row[2 * n] = 1;
            row[3 * n] = 1;
            return null;
        }
    }
}