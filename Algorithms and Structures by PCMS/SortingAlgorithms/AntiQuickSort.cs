using System;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class QuickSortForAnti
    {
        public static void Solve(string[] args)
        {
            int countOfElements = int.Parse(File.ReadAllText("antiqs.in"));
            int[] antiQsArray = new int[countOfElements];
            for (int i = 0; i < countOfElements; i++)
                antiQsArray[i] = i + 1;
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
