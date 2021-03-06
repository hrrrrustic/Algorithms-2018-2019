﻿using System;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class HeapSort
    {
        public static void Solve()
        {
            string[] inputData = File
                .ReadAllLines("sort.in")
                .Select(k => k.Trim())
                .ToArray();
            int countOfElements = int.Parse(inputData[0]);
            int[] needToSortArray = inputData[1].Select(Convert.ToInt32).ToArray();
            needToSortArray = HeapSorting(needToSortArray);
            File.WriteAllText("sort.out", string.Join(" ", needToSortArray));
        }

        private static int[] HeapSorting(int[] needToSortArray)
        {
            needToSortArray = BuildMaxHeap(needToSortArray);
            for (int i = needToSortArray.Length - 1; i > 0; i--)
            {
                Swap(ref needToSortArray[i], ref needToSortArray[0]);
                MaxHeapify(needToSortArray, 0, i);
            }
            return needToSortArray;
        }

        private static int[] BuildMaxHeap(int[] needToSortArray)
        {
            for (int i = needToSortArray.Length / 2; i > -1; i--)
            {
                MaxHeapify(needToSortArray, i, needToSortArray.Length);
            }
            return needToSortArray;
        }

        private static int[] MaxHeapify(int[] needToSortArray, int i, int lastIndex)
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
                MaxHeapify(needToSortArray, largestChild, lastIndex);
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
