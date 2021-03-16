using System;

namespace Bit
{
    public class BitArithSample
    {
        public void Run()
        {
            BitArith bitArith = new BitArith();
            byte b = bitArith.Swap(6);
            Console.WriteLine(b);

            int parity = bitArith.ParityCheck(180);
            Console.WriteLine(parity);
            parity = bitArith.ParityCheck(181);
            Console.WriteLine(parity);
            parity = bitArith.ParityCheck(10);
            Console.WriteLine(parity);
            parity = bitArith.ParityCheck(11);
            Console.WriteLine(parity);
        }
    }

    public class BitArith
    {
        public byte Swap(byte i)
        {
            byte n = (byte)(((i << 1) & 0xAA) | ((i >> 1) & 0x55));
            return n;
        }

        public int ParityCheck(byte b)
        {
            int j = 0;
            for (int i = 7; i >= 0; i--)
            {
                j ^= (b & (1 << i)) >> i;
            }
            return j;
        }
    }
}