using System;
using System.IO;
using System.Linq;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class AntiQuickSort
    {
        public static void Solve(string[] args)
        {
            int countOfElements = int.Parse(File.ReadAllText("antiqs.in"));

            int[] antiQsArray = Enumerable.Range(1, countOfElements).ToArray();

            for (int i = 2; i < countOfElements; i++)
            {
                Swap(ref antiQsArray[i], ref antiQsArray[i / 2]);
            }

            Console.WriteLine(string.Join(" ", antiQsArray));
        }

        private static void Swap(ref int firstElement, ref int secondElement)
        {
            int swapHelper = firstElement;
            firstElement = secondElement;
            secondElement = swapHelper;
        }
    }
}
