using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Windows_Tests
{
    public partial class QuestionList : Form
    {
        public QuestionList()
        {
            InitializeComponent();
            LoadingQuestions();
        }

        private void LoadingQuestions()
        {
            if (Directory.Exists(@""))
                throw new Exception("Директория \"Вопросы\" не существует.");

            DirectoryInfo root = new DirectoryInfo(@"Вопросы\");
            DirectoryInfo[] files = root.GetDirectories();
            FileInfo[] info;
            for (int i = 0; i < files.Length; i++)
            {
                tree.Nodes.Add(files[i].Name);
                info = files[i].GetFiles();

                for (int z = 0; z < info.Length; z++)
                    tree.Nodes[i].Nodes.Add(info[z].Name.Substring(0, info[z].Name.Length - Path.GetExtension(info[z].Name).Length));
            }
        }
    }
}
