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
        public bool isBegin = false;
        public string message = "";

        private void LoadingQuestions()
        {
            if (!Directory.Exists(@"Вопросы"))
                throw new Exception("Директория \"Вопросы\" не существует.");

            string[] root = Directory.GetDirectories(@"Вопросы\");
            string[] files = new string[root.Length];

            for (int i = 0; i < root.Length; i++)
            {
                tree.Nodes.Add(root[i].Remove(0, 8));
                files = Directory.GetFileSystemEntries(root[i], "*.xlsx", SearchOption.AllDirectories);

                for (int ix = 0; ix < files.Length; ix++)
                    tree.Nodes[i].Nodes.Add(Path.GetFileNameWithoutExtension(files[ix]));
            }
        }

        private void QuestionList_DoubleClick(object sender, EventArgs e)
        {
            // check for parent
            if (((TreeView)sender).SelectedNode.LastNode != null)
                return;

            message = string.Format(@"Вопросы\{0}.xlsx", ((TreeView)sender).SelectedNode.FullPath);

            if (File.Exists(message))
            {
                if (_MainWindow != null)
                    _MainWindow.Text = ((TreeView)sender).SelectedNode.Text;

                isBegin = true;
                user.ShowDialog();
            }
            else
                throw new Exception(string.Format("File {0} not found.", message));
        }

        private void InitializeQuestionForm(object sender, EventArgs e)
        {
            _MainWindow = this.Owner as MainWindow;
        }

        private void PrepareClosing(object sender, FormClosingEventArgs e)
        {
            if (!isBegin)
                Application.Exit();
        }
    }
}
