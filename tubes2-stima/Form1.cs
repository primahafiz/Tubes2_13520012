using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace GUI
{
	public partial class Form1 : Form
	{
		bool click = false;
		public Form1()
		{
			InitializeComponent();
			linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_Click);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CommonOpenFileDialog dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				textBox1.Text = dialog.FileName;
			}
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private async void button2_Click(object sender, EventArgs e)
		{
			String path = textBox1.Text;
			String file = textBox2.Text;
			String method;
			Boolean occurences = checkBox1.Checked;
			Boolean isChecked1 = radioButton1.Checked;
			Boolean isChecked2 = radioButton2.Checked;
			String picture = Directory.GetCurrentDirectory();
			Tuple<string[], Bitmap> res;

			while (Path.GetFileName(picture) != "tubes2-stima")
			{
				picture = Directory.GetParent(picture).FullName;
			}

			picture = picture + @"\pictures\";

			if (isChecked1)
				method = radioButton1.Text;
			else if (isChecked2)
				method = radioButton2.Text;
			else 
				method = "";

			linkLabel1.Links.Clear();
			if (path == null || file == null) {
				MessageBox.Show("Fill the required form !!", "Error");
			} else {
				if (method != "BFS" && method != "DFS") {
					MessageBox.Show("Fill the required form !!", "Error");
				} else {
					click = true;
					if (method == "BFS") {
						linkLabel1.Text = "";
						BFS bfs = new BFS();
						res = bfs.BFSMain(path, file, occurences, pictureBox1);
					} else {
						linkLabel1.Text = "";
						DFS dfs = new DFS();
						res = dfs.DFSMain(path, file, occurences, pictureBox1);
					} 
					
					int i = 1;
					int count = 0;
					int start;
					foreach (String str in res.Item1) {
						linkLabel1.Text += i.ToString() + ". " + res.Item1[i-1] + "\n";
						count += 2 + i.ToString().Length;
						start = count;
						linkLabel1.Links.Add(start, res.Item1[i-1].Length, toAccesableLink(res.Item1[i-1]));
						count += res.Item1[i-1].Length + 1;
						i++;
					}
					label4.Text = "File path : (" + res.Item1.Length.ToString() + ")";
					label5.Text = "click to download";
				}
			}
		}

		public String toAccesableLink(String str)
		{
			String res = "";
			int idx = 0;
			for (int i = str.Length-1; i >= 0; i--)
			{
				if (str[i] == '\\')
				{
					idx = i;
					break;
				}
			}
			for (int i = 0; i < idx; i++)
			{
				res += str[i];
			}
			return res;
		}
		
		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			String picture = Directory.GetCurrentDirectory();
			while (Path.GetFileName(picture) != "tubes2-stima")
			{
				picture = Directory.GetParent(picture).FullName;
			}

			picture = picture + @"\pictures\";
			Bitmap bitmap = new Bitmap(picture + "graph.jpg");
			if (click == false) {
				MessageBox.Show("Fill the required form !!", "Error");
			} else {
				CommonOpenFileDialog dialog = new CommonOpenFileDialog();
				dialog.IsFolderPicker = true;
				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					File.WriteAllText("graph.jpg", "");
					try
					{
						File.Delete(dialog.FileName + "\\graph.jpg");
					}
					catch
					{
				
					}
					try
					{
						bitmap.Save(dialog.FileName + "\\graph.jpg");
					}
					catch
					{

					}
				}
			}
		}
		private void linkLabel1_Click(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// Determine which link was clicked within the LinkLabel.
			this.linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;

			// Display the appropriate link based on the value of the 
			// LinkData property of the Link object.
			string target = e.Link.LinkData as string;
 
			Process.Start("explorer.exe", target);
		}
	}
}
