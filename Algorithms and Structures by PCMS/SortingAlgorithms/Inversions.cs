using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class InversionsResult
    {
        public long InversionsCount = 0;
    }

    public class MergeSortForInversions
    {
        private static int[] MergeSorting(int[] needToSortArray, InversionsResult inversions)
        {
            if (needToSortArray.Length == 1)
                return needToSortArray;
            int midPosition = needToSortArray.Length / 2;
            return Merge(MergeSorting(needToSortArray.Take(midPosition).ToArray(), inversions),
                MergeSorting(needToSortArray.Skip(midPosition).ToArray(), inversions), inversions);
        }
        private static int[] Merge(int[] leftArray, int[] rightArray, InversionsResult inversions)
        {
            int leftPointer = 0, rightPointer = 0;
            int[] mergedArray = new int[leftArray.Length + rightArray.Length];
            for (int i = 0; i < mergedArray.Length; i++)
            {
                if (rightPointer < rightArray.Length && leftPointer < leftArray.Length)
                {
                    if (leftArray[leftPointer] > rightArray[rightPointer])
                    {
                        mergedArray[i] = rightArray[rightPointer];
                        rightPointer++;
                        inversions.InversionsCount += (leftArray.Length - leftPointer);
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
            InversionsResult inversions = new InversionsResult();
            int[] inputArray = inputData[1].Split(' ').Select(int.Parse).ToArray();
            inputArray = MergeSorting(inputArray, inversions);
            File.WriteAllText("inversions.out", inversions.InversionsCount.ToString());
        }
    }
}