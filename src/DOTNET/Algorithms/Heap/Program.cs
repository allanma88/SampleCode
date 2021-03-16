using System;

namespace Heap
{
    class Program
    {
        static void Main(string[] args)
        {
            // StoneWeight stoneWeight = new StoneWeight();
            // stoneWeight.Run();

            // KthLargest largest = new KthLargest(3, new int[] { 4, 5, 8, 2 });
            // int num = largest.Add(3);
            // Console.WriteLine(num); 
            // num = largest.Add(5);
            // Console.WriteLine(num); 
            // num = largest.Add(10);
            // Console.WriteLine(num); 
            // num = largest.Add(9);
            // Console.WriteLine(num);
            // num = largest.Add(4);
            // Console.WriteLine(num);

            // Closest closest = new Closest();
            // closest.Run();

            // Frequency frequency = new Frequency();
            // frequency.Run();

            // TopK topK = new TopK();
            // topK.Run();

            // Matrix matrix = new Matrix();
            // matrix.Run();

            KLargest kLargest = new KLargest();
            kLargest.Run();
        }
    }
}
