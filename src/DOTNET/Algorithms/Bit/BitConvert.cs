using System;
using System.Collections;
using System.Collections.Generic;

namespace Bit
{
    public class BitConvertSample
    {
        public void Run()
        {
            BitConvert convert = new BitConvert();
            float f = 10.25f;
            byte[] bytes = convert.FloatToOriginBytes(f);
            PrintBytes(bytes, $"Orgin({f}): ");

            // bytes = convert.FloatToBytes(8.25f);
            // PrintBytes(bytes, "My Memory:  ");
            // bytes = BitConverter.GetBytes(8.25f);
            // PrintBytes(bytes, "Sys Memory: ");

            // f = 8.4f;
            // bytes = convert.FloatToBytes(f);
            // PrintBytes(bytes, $"My Memory({f}):  ");
            // bytes = BitConverter.GetBytes(f);
            // PrintBytes(bytes, $"Sys Memory({f}): ");

            // f = -8.4f;
            // bytes = convert.FloatToBytes(f);
            // PrintBytes(bytes, $"My Memory({f}):  ");
            // bytes = BitConverter.GetBytes(f);
            // PrintBytes(bytes, $"Sys Memory({f}): ");

            // f = 800.4f;
            // bytes = convert.FloatToBytes(f);
            // PrintBytes(bytes, $"My Memory({f}):  ");
            // bytes = BitConverter.GetBytes(f);
            // PrintBytes(bytes, $"Sys Memory({f}): ");


            f = float.MaxValue;
            // bytes = convert.FloatToBytes(f);
            // PrintBytes(bytes, $"My Memory({f}):  ");
            bytes = BitConverter.GetBytes(f);
            PrintBytes(bytes, $"Sys Memory({f}): ");

            // f = float.MinValue;
            // bytes = convert.FloatToBytes(f);
            // PrintBytes(bytes, $"My Memory({f}):  ");
            // bytes = BitConverter.GetBytes(f);
            // PrintBytes(bytes, $"Sys Memory({f}): ");
        }

        private void PrintBytes(byte[] bytes, string pre)
        {
            Console.Write(pre);
            foreach (var b in bytes)
            {
                Console.Write($"{b} ");
            }
            Console.WriteLine();
        }
    }

    public class BitConvert
    {
        public byte[] FloatToOriginBytes(float d)
        {
            float integer = (float)Math.Floor(d);
            float deci = d - integer;
            List<byte> bytes = new List<byte>();
            Stack<byte> stack = new Stack<byte>();
            byte b = 0;
            int bitCount = 0;
            while (integer > 0)
            {
                if (integer % 2 == 1)
                {
                    b = (byte)(b | (1 << bitCount));
                }
                bitCount++;
                if (bitCount > 7)
                {
                    stack.Push(b);
                    bitCount = 0;
                    b = 0;
                }
                integer = (float)Math.Floor(integer / 2);
            }
            stack.Push(b);
            bytes.AddRange(stack);
            Queue<byte> queue = new Queue<byte>();
            int preciCount = 0;
            b = 0;
            bitCount = 7;
            while (deci != 0 && preciCount < 23)
            {
                deci = deci * 2;
                float i = (float)Math.Floor(deci);
                if (i == 1)
                {
                    b = (byte)(b | (1 << bitCount));
                }
                bitCount--;
                if (bitCount < 0)
                {
                    queue.Enqueue(b);
                    bitCount = 7;
                    b = 0;
                }
                deci = deci - i;
                preciCount++;
            }
            queue.Enqueue(b);
            bytes.AddRange(queue);
            return bytes.ToArray();
        }

        public byte[] FloatToBytes(float d)
        {
            byte[] bytes = new byte[4];
            if (d < 0)
            {
                //设置符号位
                bytes[0] |= 1 << 7;
            }
            d = Math.Abs(d);
            double integer = Math.Floor(d);
            double deci = d - integer;
            byte intCount = 0;
            while (integer > 1)
            {
                integer = Math.Floor(integer / 2);
                //计算指数位
                intCount++;
            }
            //移位
            int shitCount = 0;
            if (intCount == 0)
            {
                while (deci < 0)
                {
                    deci = deci * 2;
                    //计算小于1时的指数位
                    intCount--;
                }
                deci -= 1;
            }
            else
            {
                shitCount = intCount;
            }

            //移位存储
            intCount += 127;
            //设置指数位
            bytes[0] |= (byte)(intCount >> 1);
            if (intCount % 2 == 1)
            {
                bytes[1] |= 1 << 7;
            }
            int byteCount = shitCount / 8 + 1;
            shitCount %= 8;
            byte b = 0;
            int bitCount = 7 - shitCount - 1;
            while (deci != 0 && byteCount < 4)
            {
                //计算小数位的二进制，逐个byte计算
                deci = deci * 2;
                double i = Math.Floor(deci);
                if (i == 1)
                {
                    b = (byte)(b | (1 << bitCount));
                }
                bitCount--;
                if (bitCount < 0)
                {
                    //逐个byte的设置小数位
                    bytes[byteCount] |= b;
                    bitCount = 7;
                    b = 0;
                    byteCount++;
                }
                deci = deci - i;
            }
            //逐个byte的设置小数位
            bytes[byteCount] |= b;
            Swap(bytes, 0, 3);
            Swap(bytes, 1, 2);
            return bytes;
        }

        private void Swap(byte[] bytes, int i, int j)
        {
            byte temp = bytes[i];
            bytes[i] = bytes[j];
            bytes[j] = temp;
        }

        public float BytesToFloat(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}