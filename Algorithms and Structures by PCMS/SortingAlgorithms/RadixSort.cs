﻿using System;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.SortingAlgorithms
{
    public class RadixSort
    {
        public static void Solve()
        {

            string[] inputData = File.ReadAllLines("radixsort.in");

            //TODO:9 Как на счет писать не в одну строчку?
            int[] paramsArray = inputData[0].Split(' ').Select(int.Parse).ToArray();
            string[] inputStrings = inputData.Skip(1).ToArray();
            inputStrings = inputStrings.Select(k => new string(k.Reverse().ToArray())).ToArray();

            inputStrings = RadixSorting(inputStrings, paramsArray[2]);

            inputStrings = inputStrings.Select(k => new string(k.Reverse().ToArray())).ToArray();

            foreach (string item in inputStrings)
            {
                Console.WriteLine(item);
            }
        }
        private static string[] RadixSorting(string[] inputArray, int countOfSteps)
        {
            //TODO:10 Как на счет вынести в константу charCount = 26;
            string[] outputArray = new string[inputArray.Length];
            for (int i = 0; i < countOfSteps; i++)
            {
                int[] letterList = Enumerable.Repeat(0, 26).ToArray();
                int counter = 0;
                for (int j = 0; j < inputArray.Length; j++)
                {
                    letterList[inputArray[j][i] - 'a']++;
                }

                //TODO:11 Кажется, с этим кодом нужно что-то делать, если даже ты не понял, что он делает. Выноси в метод, пиши им описание
                for (int j = 0; j < 26; j++)
                {
                    int helper = letterList[j];
                    letterList[j] = counter;
                    counter += helper;
                }
                for (int j = 0; j < inputArray.Length; j++)
                {
                    outputArray[letterList[inputArray[j][i] - 'a']] = inputArray[j];
                    letterList[inputArray[j][i] - 'a']++;
                }
                outputArray.CopyTo(inputArray, 0);
            }
            return inputArray;
        }
    }
}
