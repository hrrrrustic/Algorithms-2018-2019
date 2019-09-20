using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class MergeSortForRunners
    {
        private static List<string> MergeSorting(List<string> needToSortArray)
        {
            if (needToSortArray.Count == 1)
                return needToSortArray;

            int midPosition = needToSortArray.Count / 2;
            return Merge(MergeSorting(needToSortArray.Take(midPosition).ToList()), MergeSorting(needToSortArray.Skip(midPosition).ToList()));
        }
        private static List<string> Merge(List<string> leftArray, List<string> rightArray)
        {
            int leftPointer = 0, rightPointer = 0;
            List<string> mergedArray = new List<string>(leftArray.Count + rightArray.Count);
            for (int i = 0; i < leftArray.Count + rightArray.Count; i++)
            {
                if (rightPointer < rightArray.Count && leftPointer < leftArray.Count)
                    if (string.CompareOrdinal(leftArray[leftPointer], rightArray[rightPointer]) > 0)
                    {
                        mergedArray.Add(rightArray[rightPointer]);
                        rightPointer++;
                    }
                    else
                    {
                        mergedArray.Add(leftArray[leftPointer]);
                        leftPointer++;
                    }
                else
                {
                    if (rightPointer < rightArray.Count)
                    {
                        mergedArray.Add(rightArray[rightPointer]);
                        rightPointer++;
                    }
                    else
                    {
                        mergedArray.Add(leftArray[leftPointer]);
                        leftPointer++;
                    }
                }
            }
            return mergedArray;
        }
        public static void Solve()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            string[] inputData = File.ReadAllLines("race.in");
            int stringCount = int.Parse(inputData[0]);
            for (int i = 0; i < stringCount; i++)
            {
                string[] splittedData = inputData[i + 1].Split(' ');
                if (dict.ContainsKey(splittedData[0]))
                {
                    dict[splittedData[0]].Add(splittedData[1]);
                }
                else
                {
                    dict[splittedData[0]] = new List<string> { splittedData[1] };
                }
            }
            List<string> sortedCountry = MergeSorting(dict.Keys.ToList());
            using (var outFile = new StreamWriter("race.out"))
            {
                foreach (string country in sortedCountry)
                {
                    List<string> runners = dict[country];
                    outFile.WriteLine("=== " + country + " ===");
                    for (int i = 0; i < runners.Count; i++)
                    {
                        outFile.WriteLine(runners[i]);
                    }
                }
            }
        }
    }
}