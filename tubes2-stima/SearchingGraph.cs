using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Core.Geometry.Curves;

namespace Folder_Crawling
{
    class SearchingGraph
    {
        public static void buildGraph(List<Tuple<string, string, int>> edges)
        {
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
            Microsoft.Msagl.GraphViewerGdi.GraphRenderer renderer = new Microsoft.Msagl.GraphViewerGdi.GraphRenderer(graph);
            renderer.CalculateLayout();
            int width = 500;
            int height = 500;
            Bitmap bitmap = new Bitmap(width, (int)(graph.Height * (width / graph.Width)), PixelFormat.Format32bppPArgb);
            renderer.Render(bitmap);

            String picture = Directory.GetCurrentDirectory();

            while (Path.GetFileName(picture) != "tubes2-stima")
            {
                picture = Directory.GetParent(picture).FullName;
            }

            picture = picture + @"\pictures\";

            File.WriteAllText("graph.jpg", "");
            try
            {
                File.Delete(picture + "graph.jpg");
            }
            catch
            {
        
            }
            try
            {
                bitmap.Save(picture + "graph.jpg");
            }
            catch
            {

            }
            Console.WriteLine(picture + "graph.jpg");
        }
    }
}
