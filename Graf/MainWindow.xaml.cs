using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace susunkuliah
{
    public class Graph
    {
        static public Dictionary<string, List<string>> graph;
        static public Dictionary<string, bool> visitedDict;
        static public Stack<string> matkulStack;
        static public Queue<string> matkulQueue;

        static void initGraph()
        {
            graph = new Dictionary<string, List<string>>();
            visitedDict = new Dictionary<string, bool>();
            string fileName = "test.txt";
            var lines = File.ReadLines(fileName);

            foreach (string line in lines)
            {
                string[] tokens = line.Split(' ');

                bool isNode = true;
                string nodeDestination = "";
                string nodeOrigin = "";
                foreach (string token in tokens)
                {
                    string node = token.Substring(0, 2);
                    char delimiter = token[2];

                    if (isNode)
                    {
                        isNode = false;
                        nodeDestination = node;
                        addNode(nodeDestination);
                    }
                    else
                    {
                        nodeOrigin = node;
                        addEdge(nodeOrigin, nodeDestination);
                    }

                    if (delimiter == '.')
                    {
                        isNode = true;
                    }
                }
            }
            matkulStack = new Stack<string>();
            matkulQueue = new Queue<string>();
        }

        static void addNode(string nodeOrigin)
        {
            if (!graph.ContainsKey(nodeOrigin))
            {
                graph[nodeOrigin] = new List<string>();
                visitedDict[nodeOrigin] = false;
            }
        }

        static void addEdge(string nodeOrigin, string nodeDestination)
        {
            addNode(nodeOrigin);
            graph[nodeOrigin].Add(nodeDestination);
        }

        static void dfs(string currNode)
        {
            visitedDict[currNode] = true;

            foreach (string x in graph[currNode])
            {
                if (!visitedDict[x])
                {
                    dfs(x);
                }
            }
            matkulStack.Push(currNode);
        }

        static void bfs()
        {
            string currNode = matkulQueue.Dequeue();
            if (!visitedDict[currNode])
            {
                visitedDict[currNode] = true;
                foreach (string x in graph[currNode])
                {
                    if (!visitedDict[x])
                    {
                        matkulQueue.Enqueue(x);
                    }
                }
                matkulStack.Push(currNode);
            }
        }

        static void doTheMagic()
        {
            Console.WriteLine("Choose one of below:");
            Console.WriteLine("1. DFS");
            Console.WriteLine("2. BFS");
            int option = Console.Read();
            foreach (var item in graph)
            {
                if (!visitedDict[item.Key])
                {
                    if (option == 1)
                    {
                        // dfs
                        dfs(item.Key);
                    }
                    else
                    {
                        // bfs
                        matkulQueue.Enqueue(item.Key);
                        while (matkulQueue.Count > 0)
                        {
                            bfs();
                        }
                    }
                }
            }
        }

        static void showResult()
        {
            while (!(matkulStack.Count == 0))
            {
                Console.WriteLine(matkulStack.Pop());
            }
        }

        static void Main(string[] args)
        {
            initGraph();
            doTheMagic();
            showResult();
        }
    }
}
