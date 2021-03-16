using System;
using System.Collections.Generic;

namespace Bit
{
    public class BaseConvertSample
    {
        public void Run()
        {
            // RunBaseNum();

            BaseConvert convert = new BaseConvert();
            string s = convert.Convert("15", 5, 7);
            Console.WriteLine(s);

            convert = new BaseConvert();
            s = convert.Convert("5", 5, 7);
            Console.WriteLine(s);
        }

        private void RunBaseNum()
        {
            BaseNumber number = new BaseNumber("19ae", 15);
            Console.WriteLine(number.ToString());

            number = new BaseNumber("19ah", 18);
            Console.WriteLine(number.ToString());

            number = new BaseNumber(7, 5);
            Console.WriteLine(number.ToString());

            var sum = new BaseNumber("42135", 5) + new BaseNumber("42135", 5);
            Console.WriteLine(sum.ToString());

            var differ = new BaseNumber("42135", 5) - new BaseNumber("25451", 5);
            Console.WriteLine(differ.ToString());

            var quotient = new BaseNumber("42135", 5) / new BaseNumber("25451", 5);
            Console.WriteLine(quotient.ToString());

            var remainder = new BaseNumber("42135", 5) % new BaseNumber("25451", 5);
            Console.WriteLine(remainder.ToString());

            Console.WriteLine(new BaseNumber("3", 5) > new BaseNumber("12", 5));
        }
    }

    public class BaseConvert
    {
        public string Convert2To10(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert10To2(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert2To8(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert8To2(string s)
        {
            throw new NotImplementedException();
        }


        public string Convert2To16(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert16To2(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert16To10(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert10To16(string s)
        {
            throw new NotImplementedException();
        }

        public string Convert(string s, int srcBase, int destBase)
        {
            BaseNumber n = new BaseNumber(s, srcBase);
            BaseNumber destBaseNum = new BaseNumber(destBase, srcBase);
            Stack<string> stack = new Stack<string>();
            var zero = new BaseNumber(0, srcBase);
            while (n > zero)
            {
                stack.Push((n % destBaseNum).ToString());
                n = n / destBaseNum;
            }
            return string.Join("", stack);
        }
    }

    public class BaseNumber
    {
        private Node _head = new Node(-1);
        private int _baseNumber;

        private BaseNumber(Node head, int baseNumber)
        {
            _baseNumber = baseNumber;
            _head = head;
        }

        public BaseNumber(int n, int baseNumber)
        {
            _baseNumber = baseNumber;
            Node cur = _head;
            do
            {
                short v = (short)(n % baseNumber);
                cur.Next = new Node(v);
                cur = cur.Next;
                n = n / baseNumber;
            }
            while (n > 0);
        }

        public BaseNumber(string s, int baseNumber)
        {
            _baseNumber = baseNumber;
            Node cur = _head;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                char c = s[i];
                short v = -1;
                if (c >= '0' && c <= '9')
                {
                    v = (short)(c - '0');
                }
                else if (c >= 'a' && c <= 'z')
                {
                    v = (short)(c - 'a' + 10);
                }
                else
                {
                    throw new InvalidOperationException();
                }
                cur.Next = new Node(v);
                cur = cur.Next;
            }
        }

        public static BaseNumber operator +(BaseNumber b, BaseNumber c)
        {
            if (b._baseNumber != c._baseNumber)
            {
                throw new InvalidOperationException("can't add two number with different base");
            }
            int baseNum = b._baseNumber;
            Node n1 = b._head;
            Node n2 = c._head;
            Node head = new Node(-1);
            Node cur = head;
            short carry = 0;
            while (n1.Next != null || n2.Next != null)
            {
                short v1 = 0, v2 = 0;
                if (n1.Next == null)
                {
                    v2 = n2.Next.V;
                    n2 = n2.Next;
                }
                else if (n2.Next == null)
                {
                    v1 = n1.Next.V;
                    n1 = n1.Next;
                }
                else
                {
                    v1 = n1.Next.V;
                    v2 = n2.Next.V;
                    n1 = n1.Next;
                    n2 = n2.Next;
                }
                short sum = (short)(v1 + v2 + carry);
                short v = (short)(sum % baseNum);
                carry = (short)(sum / baseNum);
                cur.Next = new Node(v);
                cur = cur.Next;
            }
            if (carry > 0)
            {
                cur.Next = new Node(carry);
            }
            return new BaseNumber(head, baseNum);
        }

        public static BaseNumber operator -(BaseNumber b, BaseNumber c)
        {
            if (b._baseNumber != c._baseNumber)
            {
                throw new InvalidOperationException("can't add two number with different base");
            }
            int baseNum = b._baseNumber;
            Node n1 = b._head;
            Node n2 = c._head;
            Node head = new Node(-1);
            Node cur = head;
            short carry = 0;
            while (n1.Next != null || n2.Next != null)
            {
                short v1 = 0, v2 = 0;
                if (n1.Next == null)
                {
                    v2 = n2.Next.V;
                    n2 = n2.Next;
                }
                else if (n2.Next == null)
                {
                    v1 = n1.Next.V;
                    n1 = n1.Next;
                }
                else
                {
                    v1 = n1.Next.V;
                    v2 = n2.Next.V;
                    n1 = n1.Next;
                    n2 = n2.Next;
                }
                short differ = (short)(v1 - v2 - carry);
                if (differ < 0)
                {
                    differ += (short)baseNum;
                    carry = 1;
                }
                cur.Next = new Node(differ);
                cur = cur.Next;
            }
            cur = head;
            var next = cur.Next;
            while (next.Next != null)
            {
                cur = cur.Next;
                next = cur.Next;
            }
            if (next.V == 0)
            {
                cur.Next = null;
            }
            return new BaseNumber(head, baseNum);
        }

        public static BaseNumber operator *(BaseNumber b, BaseNumber c)
        {
            throw new NotImplementedException();
        }

        public static BaseNumber operator /(BaseNumber b, BaseNumber c)
        {
            if (b._baseNumber != c._baseNumber)
            {
                throw new InvalidOperationException("can't add two number with different base");
            }
            int i = 0;
            while (b > c)
            {
                b -= c;
                i++;
            }
            return new BaseNumber(i, b._baseNumber);
        }

        public static BaseNumber operator %(BaseNumber b, BaseNumber c)
        {
            if (b._baseNumber != c._baseNumber)
            {
                throw new InvalidOperationException("can't add two number with different base");
            }
            while (b > c)
            {
                b -= c;
            }
            return b;
        }

        public static bool operator >(BaseNumber b, BaseNumber c)
        {
            int len1 = Length(b._head.Next);
            int len2 = Length(c._head.Next);
            if (len1 != len2)
            {
                return len1 > len2;
            }
            Stack<short> stack1 = new Stack<short>();
            var cur1 = b._head;
            while (cur1.Next != null)
            {
                stack1.Push(cur1.Next.V);
                cur1 = cur1.Next;
            }

            Stack<short> stack2 = new Stack<short>();
            var cur2 = c._head;
            while (cur2.Next != null)
            {
                stack2.Push(cur2.Next.V);
                cur2 = cur2.Next;
            }
            while (stack1.Count > 0 && stack2.Count > 0)
            {
                short v1 = stack1.Pop();
                short v2 = stack2.Pop();
                if (v1 != v2)
                {
                    return v1 > v2;
                }
            }
            return stack1.Count > stack2.Count;
        }

        public static bool operator <(BaseNumber b, BaseNumber c)
        {
            return !(b > c);
        }

        private static int Length(Node root)
        {
            int len = 0;
            while (root != null)
            {
                len++;
                root = root.Next;
            }
            return len;
        }

        public override string ToString()
        {
            Node cur = _head;
            Stack<char> stack = new Stack<char>();
            while (cur.Next != null)
            {
                char c;
                if (cur.Next.V < 10)
                {
                    c = (char)('0' + cur.Next.V);
                }
                else
                {
                    c = (char)('a' + (cur.Next.V - 10));
                }
                stack.Push(c);
                cur = cur.Next;
            }
            return string.Join("", stack);
        }

        private class Node
        {
            public Node(short v)
            {
                V = v;
            }

            public short V { get; set; }

            public Node Next { get; set; }
        }
    }
}