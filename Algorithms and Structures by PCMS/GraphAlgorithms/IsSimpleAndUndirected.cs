using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class IsUndirectedAndSimpleByMatrix
    {
        public static void Solve()
        {
            
            int[][] inputData = File
                .ReadAllLines("input.txt")
                .Select(k => k.Trim().Split(' ').Select(int.Parse).ToArray())
                .ToArray();
            int vertexCount = inputData[0][0];
            File.WriteAllText("output.txt", 
                IsUndirectedAndSimple(inputData.Skip(1).ToArray(), vertexCount) == true ? "YES" : "NO");
        }
        private static bool IsUndirectedAndSimple(int[][] adjMatrix, int vertexCount)
        {
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    if (adjMatrix[i][j] != adjMatrix[j][i] || (i == j && adjMatrix[i][j] == 1))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
