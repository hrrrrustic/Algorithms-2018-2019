﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AlgorithmsAndStructuresByPCMS.GraphAlgorithms
{
    public class DepthFirstSearch
    {
        // Doesn't work, but really near to correct solution
        public static void Solve(string[] args)
        {
            List<Edge> edges = new List<Edge>();
            int[][] file = File
                .ReadAllLines("chinese.in")
                .Select(k => k.Split(' ').Select(int.Parse).ToArray())
                .ToArray();

            int vertexCount = file[0][0];
            int edgeCount = file[0][1];
            for (int i = 0; i < edgeCount; i++)
            {
                int from = file[i + 1][0] - 1;
                int to = file[i + 1][1] - 1;
                int weight = file[i + 1][2] - 1;
                edges.Add(new Edge(from, to, weight));
            }

            if (DFSChecking(0, vertexCount, ref edges))
            {
                long answer = MST(vertexCount, ref edges, 0);
                Console.WriteLine("YES\r\n" + answer.ToString());
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        private static bool DFSChecking(int start, int vertexCount, ref List<Edge> edges)
        {
            bool[] used = new bool[vertexCount];
            Stack<int> stack = new Stack<int>();
            stack.Push(start);
            List<int>[] adjList = new List<int>[vertexCount];
            EdgesToAdjList(ref edges, ref adjList, false);
            while (stack.Count != 0)
            {
                int current = stack.Pop();
                used[current] = true;
                if (adjList[current] != null)
                    foreach (var item in adjList[current])
                    {
                        if (!used[item])
                            stack.Push(item);
                    }
            }
            for (int i = 0; i < vertexCount; i++)
            {
                if (!used[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static long MST(int vertexCount, ref List<Edge> edges, int start)
        {
            long answer = 0;
            long[] bestDist = new long[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                bestDist[i] = 10000000000;
            }
            foreach (var t in edges)
            {
                bestDist[t.To] = t.Weight > bestDist[t.To] ? bestDist[t.To] : t.Weight;
            }
            for (int i = 0; i < vertexCount; i++)
            {
                if(i != start)
                    answer += bestDist[i];
            }
            List<Edge> zeroEdges = new List<Edge>();
            foreach (var t in edges)
            {
                if (t.Weight == bestDist[t.To])
                    zeroEdges.Add(t);
            }
            if (!DFSChecking(start, vertexCount, ref zeroEdges))
            {
                int[] components = new int[vertexCount];
                int comp = 0;
                Condensation(ref zeroEdges, vertexCount, ref components, ref comp);
                List<Edge> newEdges = new List<Edge>();
                foreach (var item in edges)
                {
                    if (components[item.To] != components[item.From])
                    {
                        newEdges.Add(new Edge(components[item.From], components[item.To], item.Weight - bestDist[item.To]));
                    }
                }
                answer += MST(comp, ref newEdges, components[start]);
            }
            return answer;
        }

        private static void Condensation(ref List<Edge> graph, int vertexCount, ref int[] components, ref int comp)
        {
            for (int i = 0; i < vertexCount; i++)
            {
                components[i] = 0;
            }
            bool[] used = new bool[vertexCount];
            List<int> sort = new List<int>();
            List<int>[] adjList = new List<int>[vertexCount];
            EdgesToAdjList(ref graph, ref adjList, false);
            for (int i = 0; i < vertexCount; i++)
            {
                if (!used[i])
                    TopSort(i, ref used, ref sort, ref adjList);
            }
            for (int i = 0; i < vertexCount; i++)
            {
                used[i] = false;
            }
            int num = 0;
            List<int> component = new List<int>();
            List<int>[] notAdjList = new List<int>[vertexCount];
            EdgesToAdjList(ref graph, ref notAdjList, true);
            for (int i = 0; i < vertexCount; i++)
            {
                int q = sort[vertexCount - 1 - i];
                if (!used[q])
                {
                    Components(ref notAdjList, q, ref used, ref component);
                    foreach (var item in component)
                    {
                        components[q] = num;
                    }
                    num++;
                    component.Clear();
                }
            }
            comp = num;
        }

        private static void TopSort(int start, ref bool[] used, ref List<int> sort, ref List<int>[] adjList)
        {
            used[start] = true;
            foreach (var item in adjList[start])
            {
                if (!used[item])
                    TopSort(item, ref used, ref sort, ref adjList);
            }
            sort.Add(start);
        }

        private static void Components(ref List<int>[] graphList, int start, ref bool[] used, ref List<int> component)
        {
            used[start] = true;
            component.Add(start);
            foreach (var item in graphList[start])
            {
                if (!used[item])
                    Components(ref graphList, item, ref used, ref component);
            }
        }

        private static void EdgesToAdjList(ref List<Edge> graph, ref List<int>[] adjList, bool inv)
        {
            foreach (var item in graph)
            {
                if(inv)
                {
                    if (adjList[item.To] == null)
                        adjList[item.To] = new List<int>();
                    adjList[item.To].Add(item.From);
                }
                else
                {
                    if (adjList[item.From] == null)
                        adjList[item.From] = new List<int>();
                    adjList[item.From].Add(item.To);
                }
            }
        }
    }
    public struct Edge
    {
        public readonly int From;
        public readonly int To;
        public readonly long Weight;
        public Edge(int f, int t, long w)
        {
            From = f;
            To = t;
            Weight = w;
        }
    }
}
