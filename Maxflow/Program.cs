using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maxflow
{
    class Program
    {
        static bool[] visited;
        static int t;
        static List<Tuple<int,int,int>>[] adjList;
        static void Main(string[] args)
        {
            int[][] data = File.ReadAllLines("maxflow.in").Select(k => k.Trim().Split(' ').Select(e => int.Parse(e)).ToArray()).ToArray();
            int vertexCount = data[0][0];
            int edgeCount = data[0][1];
            adjList = new List<Tuple<int, int, int>>[vertexCount];
            t = vertexCount - 1;
            visited = new bool[vertexCount];
            for (int i = 0; i < edgeCount; i++)
            {
                int from = data[i + 1][0] - 1;
                int to = data[i + 1][1] - 1;
                if (adjList[from] == null)
                    adjList[from] = new List<Tuple<int,int,int>>(999);
                adjList[from].Add(Tuple.Create(to, data[i + 1][2], 0));
                if (adjList[to] == null)
                    adjList[to] = new List<Tuple<int, int, int>>(999);
                adjList[to].Add(Tuple.Create(from, data[i + 1][2], data[i + 1][2]));
            }
            int answer = 0;
            while(true)
            {
                int currentanswer = answer;
                answer += FFByDfs(0, (int)1e5);
                if (currentanswer == answer)
                    break;
                for (int i = 0; i < visited.Length; i++)
                {
                    visited[i] = false;
                }
            }
            Console.WriteLine(answer);
        }

        static int FFByDfs(int u, int Cmin)
        {
            if (u == t)
                return Cmin;
            visited[u] = true;
            for (int i = 0; i < adjList[u].Count; i++)
            {
                if (!visited[adjList[u][i].Item1] && adjList[u][i].Item3 < adjList[u][i].Item2)
                {
                    int min = Cmin > adjList[u][i].Item2 - adjList[u][i].Item3 ? adjList[u][i].Item2 - adjList[u][i].Item3 : Cmin;
                    int delta = FFByDfs(adjList[u][i].Item1, min);
                    if (delta > 0)
                    {
                        adjList[u][i] = Tuple.Create(adjList[u][i].Item1, adjList[u][i].Item2, adjList[u][i].Item3 + delta);
                        int pos;
                        for (pos = 0; pos < adjList[adjList[u][i].Item1].Count; pos++)
                        {
                            if (adjList[adjList[u][i].Item1][pos].Item1 == u)
                                break;
                        }
                        adjList[adjList[u][i].Item1][pos] = Tuple.Create(adjList[adjList[u][i].Item1][pos].Item1, adjList[adjList[u][i].Item1][pos].Item2, adjList[adjList[u][i].Item1][pos].Item3 - delta);
                        return delta;
                    }
                }
            }
            return 0;
        }
    }
}
