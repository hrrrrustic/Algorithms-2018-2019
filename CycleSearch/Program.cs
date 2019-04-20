using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CycleSearch
{
    class Program
    {
        static int[] visited;
        static int[,] adjMatrix;
        static List<int> answer = new List<int>();
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("cycle.in");
            string[] splittedInfo = data[0].Split(' ');
            int vertexCount = int.Parse(splittedInfo[0]);
            int edgeCount = int.Parse(splittedInfo[1]);
            visited = new int[vertexCount];
            adjMatrix = new int[vertexCount, vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                string[] splittedData = data[i + 1].Split(' ');
                int j = int.Parse(splittedData[0]) - 1;
                int k = int.Parse(splittedData[1]) - 1;
                adjMatrix[j,k] = 1;
            }
            for (int i = 0; i < visited.Length; i++)
            {
                if (visited[i] == 0)
                    DFS(i, vertexCount);
            }

            answer.Reverse();
            Console.WriteLine(answer.Count == 0 ? "NO" : "YES\r\n" + string.Join(" ", answer));
        }
        static void DFS(int startVertex, int vertexCount)
        {
            Stack<int> localvisited = new Stack<int>();
            Stack<int> dfsstack = new Stack<int>();
            dfsstack.Push(startVertex);
            while (dfsstack.Count != 0)
            {
                int curr = dfsstack.Pop();
                visited[curr] = 1;
                localvisited.Push(curr);
                for (int i = 0; i < vertexCount; i++)
                {
                    if (i == curr)
                        continue;
                    if (visited[i] == 0 && adjMatrix[curr,i] == 1)
                    {
                        dfsstack.Push(i);
                    }
                    else if (visited[i] == 1 && adjMatrix[curr, i] == 1)
                    {
                        int anscurr = i;
                        answer.Add(anscurr + 1);
                        while (anscurr != visited[i])
                        {
                            anscurr = localvisited.Pop();
                            answer.Add(anscurr + 1);
                        }
                    }
                }
            }

            foreach (int i in localvisited)
            {
                visited[i] = 2;
            }
            localvisited.Clear();
        }
    }
}
