using System.Collections.Generic;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.DataStructures
{
    public class StackForBrackets
    {
        public static void Solve()
        {
            List<string> answers = new List<string>();
            string[] sequences = File.ReadAllLines("brackets.in");

            for (int i = 0; i < sequences.Length; i++)
            {
                answers.Add(CheckSequence(sequences[i]) ? "YES" : "NO");
            }

            File.WriteAllText("brackets.out", string.Join("\r\n", answers));
        }

        public static bool CheckSequence(string sequence)
        {
            int sequenceLength = sequence.Length;
            for (int j = 1; j < sequenceLength; j++)
            {
                if (j < 1)
                    j = 1;

                if (sequence.Length > 1)
                {
                    if (sequence[j - 1] + 1 == sequence[j] || sequence[j - 1] + 2 == sequence[j]) // ASCII table (, ), [, ]
                    {
                        sequence = sequence.Remove(j - 1, 2);
                        j -= 2;
                        sequenceLength -= 2;
                    }
                }
            }

            return string.IsNullOrEmpty(sequence);
        }
    }
}