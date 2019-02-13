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
        static List<int>[] adjList;
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("pathbge1.in");
            string[] splittedInfo = data[0].Split(' ');
            int vertexCount = int.Parse(splittedInfo[0]);
            int edgeCount = int.Parse(splittedInfo[1]);
            vertexList = new int[vertexCount];
            visited = new bool[vertexCount];
            adjList = new List<int>[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                string[] splittedData = data[i + 1].Split(' ');
                int j = int.Parse(splittedData[0]) - 1;
                int k = int.Parse(splittedData[1]) - 1;
                if (adjList[j] == null)
                    adjList[j] = new List<int>();
                adjList[j].Add(k);
                if (adjList[k] == null)
                    adjList[k] = new List<int>();
                adjList[k].Add(j);
            }
            BFS(0, vertexCount);
            string answer = string.Join(" ", vertexList);
            Console.WriteLine(answer);
        }
        static void BFS(int startVertex, int vertexCount)
        {
            Queue<int> dfsqueue = new Queue<int>();
            dfsqueue.Enqueue(startVertex);
            visited[startVertex] = true;
            vertexList[startVertex] = 0;
            while (dfsqueue.Count != 0)
            {
                int curr = dfsqueue.Dequeue();
                if (adjList[curr] != null)
                   {
                    for (int i = 0; i < adjList[curr].Count; i++)
                    {
                        if (!visited[adjList[curr][i]])
                        {
                            visited[adjList[curr][i]] = true;
                            vertexList[adjList[curr][i]] = vertexList[curr] + 1;
                            dfsqueue.Enqueue(adjList[curr][i]);
                        }
                    }
                }
            }
        }
    }
}
