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

            user = new User();
            user.Owner = this;

            LoadingQuestions();
        }

        public MainWindow _MainWindow;
        User user;
        public string message = "";

        private void LoadingQuestions()
        {
            if (!Directory.Exists(@"Вопросы"))
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

        private void QuestionList_DoubleClick(object sender, EventArgs e)
        {
            message = string.Format(@"Вопросы\{0}.xlsx", ((TreeView)sender).SelectedNode.FullPath);

            if (File.Exists(message))
            {
                if (_MainWindow != null)
                    _MainWindow.Text = ((TreeView)sender).SelectedNode.Text;

                user.ShowDialog();
            }
            else
                throw new Exception(string.Format("File {0} not found.", message));
        }

        private void InitializeQuestionForm(object sender, EventArgs e)
        {
            _MainWindow = this.Owner as MainWindow;
        }
    }
}
