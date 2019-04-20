using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class HeapSort
    {
        static void Solve()
        {
            int ArrayaySize;
            int[] Array;
            using (var file = new StreamReader("INPUT.TXT"))
            {
                ArrayaySize = int.Parse(file.ReadLine());
                Array = file.ReadLine().Split(' ').Select(n => int.Parse(n)).ToArray();
            }
            Array = heapSort(Array);
            using (var OutFile = new StreamWriter("OUTPUT.TXT"))
            {
                OutFile.WriteLine(string.Join(" ", Array));
            }
        }
        static int[] heapSort(int[] Array)
        {
            Array = buildMaxHeap(Array);
            for (int i = Array.Length - 1; i >= 1; i--)
            {
                int swapHelper = Array[i];
                Array[i] = Array[0];
                Array[0] = swapHelper;
                maxHeapify(Array, 0, i);
            }
            return Array;
        }
        static int[] buildMaxHeap(int[] Array)
        {
            for (int i = Array.Length / 2; i >= 0; i--)
            {
                maxHeapify(Array, i, Array.Length);
            }
            return Array;
        }
        static int[] maxHeapify(int[] Array, int i, int lastIndex)
        {
            int left = 2 * i + 1;
            int largest;
            int right = 2 * i + 2;
            if (left <= lastIndex - 1 && Array[left] > Array[i])
            {
                largest = left;
            }
            else
            {
                largest = i;
            }
            if (right <= lastIndex - 1 && Array[right] > Array[largest])
            {
                largest = right;
            }
            if (largest != i)
            {
                int swapHelper = Array[largest];
                Array[largest] = Array[i];
                Array[i] = swapHelper;
                maxHeapify(Array, largest, lastIndex);
            }
            return Array;
        }
    }
}
