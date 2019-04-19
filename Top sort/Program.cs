using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Top_sort
{
    class Program
    {
        static bool[] visited;
        static int[] vertexList;
        static List<int>[] adjList;
        static List<int> answer = new List<int>();
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("topsort.in");
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
            }
            for (int i = 0; i < vertexList.Length; i++)
            {
                if(!visited[i])
                    DFS(i, vertexCount);
            }
            File.WriteAllText("topsort.out", string.Join(" ", answer));
        }
        static void DFS(int startVertex, int vertexCount)
        {
            Stack<int> dfsstack = new Stack<int>();
            dfsstack.Push(startVertex);
            visited[startVertex] = true;
            while (dfsstack.Count != 0)
            {
                int curr = dfsstack.Pop();
                if (adjList[curr] != null)
                {
                    for (int i = 0; i < adjList[curr].Count; i++)
                    {
                        if (!visited[adjList[curr][i]])
                        {
                            visited[adjList[curr][i]] = true;
                            dfsstack.Push(adjList[curr][i]);
                            answer.Add(curr);
                        }
                        else
                        {
                            answer.Clear();
                            answer.Add(-1);
                            return;
                        }
                    }
                }
            }
        }
    }
}