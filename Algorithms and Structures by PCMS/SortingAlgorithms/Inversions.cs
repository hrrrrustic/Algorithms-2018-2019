using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public struct InversionsResult
    {
        //TODO:5 Da fuq? Запустить дважды сортировку на разных массивах и убедить, что ты будешь получать WA
        public static int inversionsCount = 0;
    }

    public class MergeSortForInversions
    {
        private static int[] MergeSorting(int[] needToSortArray)
        {
            if (needToSortArray.Length == 1)
                return needToSortArray;
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
                        mergedArray[i] = rightArray[rightPointer];
                        rightPointer++;
                        InversionsResult.inversionsCount = InversionsResult.inversionsCount + (leftArray.Length - leftPointer);
                        //TODO:6 Что по отступам?
                       }
                    else
                    {
                        mergedArray[i] = leftArray[leftPointer];
                        leftPointer++;
                    }
                }
                else
                {
                    if (rightPointer < rightArray.Length)
                    {
                        mergedArray[i] = rightArray[rightPointer];
                        rightPointer++;
                    }
                    else
                    {
                        mergedArray[i] = leftArray[leftPointer];
                        leftPointer++;
                    }
                }
            }
            return mergedArray;
        }
        public static void Solve()
        {
            string[] inputData = File.ReadAllLines("inversions.in").Select(k => k.Trim()).ToArray();
            int countOfElements = int.Parse(inputData[0]);
            int[] inputArray = inputData[1].Select(Convert.ToInt32).ToArray();
            inputArray = MergeSorting(inputArray);
            File.WriteAllText("inversions.out", InversionsResult.inversionsCount.ToString());
        }
    }
}