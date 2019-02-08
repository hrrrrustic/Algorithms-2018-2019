using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathbge1
{
    class Program
    {
        static bool[] visited;
        static int[] vertexList;
        static void Main(string[] args)
        {
            int path = 0;
            string[] data = File.ReadAllLines("pathbge1.in");
            int vertexCount = int.Parse(data[0].Split(' ').First());
            int edgeCount = int.Parse(data[0].Split(' ').Last());
            vertexList = new int[vertexCount];
            visited = new bool[vertexCount];
            int[,] matrix = new int[vertexCount, vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int j = int.Parse(data[i + 1].Split(' ').First()) - 1;
                int k = int.Parse(data[i + 1].Split(' ').Last()) - 1;
                matrix[j, k] = 1;
                matrix[k, j] = 1;
            }
            BFS(ref matrix, 0, vertexCount, path);
            using (var outFile = new StreamWriter("pathbge1.out"))
            {
                for (int i = 0; i < vertexCount; i++)
                {
                    outFile.Write(vertexList[i] + " ");
                }
            }
        }
        static void BFS(ref int[,] matrix, int startVertex, int vertexCount, int path)
        {
            Queue<int> dfsqueue = new Queue<int>();
            dfsqueue.Enqueue(startVertex);
            visited[startVertex] = true;
            vertexList[startVertex] = path;
            path++;
            while (dfsqueue.Count != 0)
            {
                int curr = dfsqueue.Dequeue();
                for (int i = startVertex; i < vertexCount; i++)
                {
                    if (matrix[curr, i] == 1 && visited[i] == false)
                    {
                        visited[i] = true;
                        vertexList[i] = path;
                        dfsqueue.Enqueue(i);
                    }
                }
                path++;
            }
        }
    }
}
