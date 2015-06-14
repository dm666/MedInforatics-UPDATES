using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Tests
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();

            QL = this.Owner as QuestionList;
        }

        QuestionList QL;

        private void BeginTest(object sender, EventArgs e)
        {
            if (QL._MainWindow != null)
            {
                if (SName.Text == string.Empty || Group.Text == string.Empty)
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }

                QL._MainWindow.StudentName = SName.Text;
                QL._MainWindow.Group = Group.Text;

                if (QL._MainWindow.data != null)
                {
                    QL._MainWindow.rowId = 1;
                    QL._MainWindow.data.LoadingQuery(QL.message);
                    QL._MainWindow.data.NextQuest(QL._MainWindow.rowId, QL._MainWindow, QL._MainWindow.label1, QL._MainWindow.listBox1);
                    QL._MainWindow.label2.Text = "Вопрос 1 из " + QL._MainWindow.data.ExcelFileMgr.Count.ToString();
                    QL._MainWindow.timer.Enabled = true;
                }

                this.Close();
                QL.Close();
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
            QL.Show();
        }

        private void PrepareUserForm(object sender, EventArgs e)
        {
            QL = this.Owner as QuestionList;
        }
    }
}
