using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class MergeSort
    {
        private static int[] MergeSorting(int[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
            {
                return needToSortArray;
            }
            int midPosition = needToSortArray.Length / 2;
            return Merge(MergeSorting(needToSortArray.Take(midPosition).ToArray()), MergeSorting(needToSortArray.Skip(midPosition).ToArray()));
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int leftPointer = 0, rightPointer = 0;
            int[] mergedArray = new int[leftArray.Length + rightArray.Length];
            for (int i = 0; i < leftArray.Length + rightArray.Length; i++)
            {
                if (rightPointer < rightArray.Length && leftPointer < leftArray.Length)
                {
                    if (leftArray[leftPointer] > rightArray[rightPointer])
                    {
                        mergedArray[i] = rightArray[rightPointer++];
                    }
                    else
                    {
                        mergedArray[i] = leftArray[leftPointer++];
                    }
                }
                else
                {
                    if (rightPointer < rightArray.Length)
                    {
                        mergedArray[i] = rightArray[rightPointer++];
                    }
                    else
                    {
                        mergedArray[i] = leftArray[leftPointer++];
                    }
                }
            }
            return mergedArray;
        }
        public static void Solve(string[] args)
        {
            string[] inputData = File.ReadAllLines("sort.in").Select(k => k.Trim()).ToArray();
            int countOfElements = int.Parse(inputData[0]);
            int[] needToSortArray = inputData[1].Select(k => Convert.ToInt32(k)).ToArray();
            needToSortArray = MergeSorting(needToSortArray);
            for (int i = 0; i < needToSortArray.Length; i++)
            {
                Console.Write(needToSortArray[i] + " ");
            }
        }
    }
}