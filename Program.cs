using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Folder_Crawling
{
    static class Program
    {
        public static void Main()
        {
            string[] ans;

            BFS mainBFS = new BFS();
            ans= mainBFS.BFSMain("C:\\Institut Teknologi Bandung\\Semester 4\\Kuliah\\Praktikum", "main.cpp", false);
            BFS.printArr(ans);

            DFS mainDFS = new DFS();
            ans = mainDFS.DFSMain("C:\\Institut Teknologi Bandung\\Semester 4\\Kuliah\\Praktikum", "main.cpp", false);
            DFS.printArr(ans);
        }
    }
}
