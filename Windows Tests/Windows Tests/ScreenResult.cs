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
    public partial class ScreenResult : Form
    {
        public ScreenResult()
        {
            InitializeComponent();
        }
        MainWindow window;

        private void Loading(object sender, EventArgs e)
        {
            window = this.Owner as MainWindow;

            if (window != null)
            {
                for (int index = 1; index <= window.data.ExcelFileMgr.Count; index++)
                    LoadData(index);

                tableOfResult.Rows.Add("Итог теста:", window.data.AllTestResult());

                this.Text = "Результаты тестирования — [студент " + window.StudentName + "]";
            }
        }

        private void LoadData(int id)
        {
            if (!window.data.ExcelFileMgr.ContainsKey(id))
                return;

            tableOfResult.Rows.Add(id, window.data.ExcelFileMgr[id].quest, window.data.GetPercentOfQuestByEntry(id));
        }
    }
}
