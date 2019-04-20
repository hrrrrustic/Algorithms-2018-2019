using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphAlgorithms
{
    class DepthFirstSearchForMaze
    {
        static void Solve(string[] args)
        {
            List<char> commands = new List<char>();
            string[] mazeInfo;
            bool isFound = false;
            int finishColPos = 0;
            int finishRowPos = 0;
            int startRowPos = 0;
            int startColPos = 0;
            int rowCount;
            int colCount;
            int[][] data;
            using (var file = new StreamReader("input.txt"))
            {
                mazeInfo = file.ReadLine().Split(' ');
                rowCount = int.Parse(mazeInfo[0]);
                colCount = int.Parse(mazeInfo[1]);
                data = new int[rowCount][];
                for (int i = 0; i < rowCount; i++)
                {
                    data[i] = new int[colCount];
                }
                for (int i = 0; i < rowCount; i++)
                {
                    string buffer = file.ReadLine();
                    for (int j = 0; j < colCount; j++)
                    {
                        switch (buffer[j])
                        {
                            case '.':
                                data[i][j] = -2;
                                break;
                            case 'T':
                                finishRowPos = i;
                                finishColPos = j;
                                data[i][j] = 10001;
                                break;
                            case 'S':
                                startRowPos = i;
                                startColPos = j;
                                data[i][j] = 0;
                                break;
                            case '#':
                                data[i][j] = -1;
                                break;
                        }

                    }
                }
            }
            isFound = BFS(data, startRowPos, startColPos, rowCount, colCount, ref finishColPos, ref finishRowPos);

            if (isFound)
                FindShortestWay(data, finishRowPos, finishColPos, colCount, rowCount, ref commands);
            else commands.Clear();
            using (var outFile = new StreamWriter("output.txt"))
            {
                commands.Reverse();
                outFile.WriteLine(commands.Count == 0 ? -1 : commands.Count);
                outFile.Write(string.Join("", commands));
            }
        }
        static bool BFS(int[][] map, int startRowPos, int startColPos, int rowCount, int colCount, ref int finishColPos, ref int finishRowPos)
        {
            bool isFound = false;
            Queue<int> dfsqueue = new Queue<int>();
            dfsqueue.Enqueue(startRowPos);
            dfsqueue.Enqueue(startColPos);
            while (dfsqueue.Count != 0)
            {
                int currRowPos = dfsqueue.Dequeue();
                int currColPos = dfsqueue.Dequeue();
                for (int i = currRowPos - 1; i < currRowPos + 2; i++)
                {
                    for (int j = currColPos - 1; j < currColPos + 2; j++)
                    {
                        if (Math.Abs(currColPos - j) + Math.Abs(currRowPos - i) != 1 || j > colCount - 1|| j < 0 || i > rowCount - 1 || i < 0)
                            continue;
                        if (map[i][j] == -2)
                        {
                            dfsqueue.Enqueue(i);
                            dfsqueue.Enqueue(j);
                            map[i][j] = map[currRowPos][currColPos] + 1;
                        }
                        if (map[i][j] == 10001)
                        {
                            finishRowPos = i;
                            finishColPos = j;
                            isFound = true;
                        }
                    }
                }
            }
            return isFound;
        }
        static void FindShortestWay(int[][] data, int rowPos, int colPos, int colCount, int rowCount, ref List<char> commands)
        {
            int minNeighb = 10000;
            int minCol = 0;
            int minRow = 0;
            while (minNeighb != 0)
            {
                for (int i = rowPos - 1; i < rowPos + 2; i++)
                {
                    for (int j = colPos - 1; j < colPos + 2; j++)
                    {
                        if (Math.Abs(colPos - j) + Math.Abs(rowPos - i) > 1 || j > colCount - 1 || j < 0 || i > rowCount - 1 || i < 0 || data[i][j] == -1)
                            continue;
                        if (data[i][j] < minNeighb)
                        {

                            minNeighb = data[i][j];
                            minRow = i;
                            minCol = j;
                        }
                    }
                }
                if (minRow > rowPos)
                    commands.Add('U');
                else if (minRow < rowPos)
                    commands.Add('D');
                else if (minCol > colPos)
                    commands.Add('L');
                else commands.Add('R');
                rowPos = minRow;
                colPos = minCol;
            }
        }
    }
}