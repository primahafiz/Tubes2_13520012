using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DFS
{
    static List<string> ans = new List<string>();
    static List<Tuple<string, string, int>> edge = new List<Tuple<string, string, int>>();
    static int flag = 1;

    public string[] DFSMain(string dirPath, string searchFile, bool isAll) 
    {
        // list path of searchFile
        // ans = new List<string>();
        // list of edge <Node1,Node2,Color>
        // Color : 0 => black, 1 => red, 2 => blue
        // edge = new List<Tuple<string, string, int>>();

        DFSRecursive(dirPath, searchFile, isAll);
        
        // mark edge with blue color if edge is a path to the searchFile
        foreach(string pathAns in ans)
        {
            for(int i = 0; i < edge.Count; i++)
            {
                // check if pathAns is inside edge[i].Item1 and edge[i].Item2
                if(isPrefixSubs(edge[i].Item1,pathAns) && isPrefixSubs(edge[i].Item2, pathAns))
                {
                    edge[i] = Tuple.Create(edge[i].Item1, edge[i].Item2, 2);
                }
            }
        }

        printList(edge);
        Folder_Crawling.SearchingGraph.buildGraph(edge);
        return ans.ToArray();
    }

    public void DFSRecursive(string dirPath, string searchFile, bool isAll)
    {
        // get all directory inside
        string[] pathsDir = Directory.GetDirectories(dirPath);
        // get all file inside
        string[] pathsFile = Directory.GetFiles(dirPath);

        foreach (string path in pathsFile)
        {
            if (flag == 1)
            {
                edge.Add(Tuple.Create(dirPath,path,0));
                if (isFile(path))
                {
                    // check if file=searchFile
                    if (Path.GetFileName(path) == searchFile)
                    {
                        ans.Add(path);
                        if (!isAll)
                        {
                            flag = 0;
                            break;
                        }
                    }
                }
            }
        }

        foreach (string path in pathsDir)
        {
            if (flag == 1) 
            {
                edge.Add(Tuple.Create(dirPath, path, 0));
                DFSRecursive(path, searchFile, isAll);
            }
        }
    }

    public static bool isFile(string path)
    {
        return File.Exists(path);
    }
    public static bool isPrefixSubs(string s1,string s2)
    {
        string[] split1 = s1.Split('\\');
        string[] split2 = s2.Split('\\');
        if (split1.Length > split2.Length)
        {
            return false;
        }
        else
        {
            for(int i = 0; i < split1.Length; i++)
            {
                if (split1[i] != split2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
    public static void printArr(string[] arr)
    {
        Console.WriteLine("List ans :");
        foreach (string element in arr)
        {
            Console.WriteLine(element);
        }
    }
    public static void printList(List<Tuple<string, string, int>> edge)
    {
        Console.WriteLine("List edge :");
        foreach (Tuple<string, string, int> element in edge)
        {
            Console.Write(Path.GetFileName(element.Item1)+" "+ Path.GetFileName(element.Item2)+" "+element.Item3+"\n");
        }
    }
}
    
