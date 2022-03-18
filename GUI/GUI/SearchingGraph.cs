using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folder_Crawling
{
    class SearchingGraph
    {
        public static void buildGraph(List<Tuple<string, string, int>> edges)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content
            foreach (Tuple<string, string, int> edge in edges)
            {
                if (edge.Item3 == 0)
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                    graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                }
                else if (edge.Item3 == 1)
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    if (graph.FindNode(edge.Item1).Attr.Color == Microsoft.Msagl.Drawing.Color.Black)
                    {
                        graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                    if (graph.FindNode(edge.Item2).Attr.Color == Microsoft.Msagl.Drawing.Color.Black)
                    {
                        graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
                    }
                }
                else
                {
                    graph.AddEdge(edge.Item1, edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    if (graph.FindNode(edge.Item1).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue)
                    {
                        graph.FindNode(edge.Item1).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }
                    if (graph.FindNode(edge.Item2).Attr.Color != Microsoft.Msagl.Drawing.Color.Blue)
                    {
                        graph.FindNode(edge.Item2).Attr.Color = Microsoft.Msagl.Drawing.Color.Blue;
                    }

                }
                graph.FindNode(edge.Item1).LabelText = Path.GetFileName(edge.Item1);
                graph.FindNode(edge.Item2).LabelText = Path.GetFileName(edge.Item2);
            }
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }
    }
}
