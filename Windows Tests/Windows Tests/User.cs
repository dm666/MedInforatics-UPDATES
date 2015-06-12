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
                    QL._MainWindow.data.NextQuest(QL._MainWindow.rowId, QL._MainWindow);
                }

                this.Close();
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrepareUserForm(object sender, EventArgs e)
        {
            QL = this.Owner as QuestionList;
        }
    }
}
