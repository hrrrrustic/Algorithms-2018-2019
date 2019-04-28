using System;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class HeapSort
    {
        public static void Solve()
        {
            string[] inputData = File.ReadAllLines("sort.in").Select(k => k.Trim()).ToArray();
            int countOfElements = int.Parse(inputData[0]);
            int[] needToSortArray = inputData[1].Select(Convert.ToInt32).ToArray();
            needToSortArray = heapSort(needToSortArray);
            File.WriteAllText("sort.out", string.Join(" ", needToSortArray));
        }
        private static int[] HeapSort(int[] needToSortArray)
        {
            needToSortArray = buildMaxHeap(needToSortArray);
            for (int i = needToSortArray.Length - 1; i > 0; i--)
            {
                Swap(ref needToSortArray[i], ref needToSortArray[0]);
                maxHeapify(needToSortArray, 0, i);
            }
            return needToSortArray;
        }

        private static int[] buildMaxHeap(int[] needToSortArray)
        {
            for (int i = needToSortArray.Length / 2; i > -1; i--)
            {
                maxHeapify(needToSortArray, i, needToSortArray.Length);
            }
            return needToSortArray;
        }
        private static int[] maxHeapify(int[] needToSortArray, int i, int lastIndex)
        {
            int leftChild = 2 * i + 1;
            int largestChild;
            int rightChild = 2 * i + 2;
            if (leftChild <= lastIndex - 1 && needToSortArray[leftChild] > needToSortArray[i])
            {
                largestChild = leftChild;
            }
            else
            {
                largestChild = i;
            }
            if (rightChild <= lastIndex - 1 && needToSortArray[rightChild] > needToSortArray[largestChild])
            {
                largestChild = rightChild;
            }
            if (largestChild != i)
            {
                Swap(ref needToSortArray[largestChild], ref needToSortArray[i]);
                maxHeapify(needToSortArray, largestChild, lastIndex);
            }
            return needToSortArray;
        }
        private static void Swap(ref int firstElement, ref int secondElement)
        {
            int swapHelper = firstElement;
            firstElement = secondElement;
            secondElement = swapHelper;
        }
    }
}
