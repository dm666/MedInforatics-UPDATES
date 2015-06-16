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

                this.Text = "Результаты тестирования — [студент " + window.StudentName + "]";

                tableOfResult.Rows.Add("Итог теста:", window.data.AllTestResultDouble());

                tableOfResult.RowHeadersVisible = false;
                
                tableOfResult.Height = tableOfResult.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                       tableOfResult.ColumnHeadersHeight + 2;

                int i = tableOfResult.Rows.Count - 1;

                if (window.data.Result() >= 0.7)
                    tableOfResult.Rows[i].Cells[1].Style.ForeColor = Color.Green;
                else
                    tableOfResult.Rows[i].Cells[1].Style.ForeColor = Color.Red;             
            }
        }

        private void LoadData(int id)
        {
            if (!window.data.ExcelFileMgr.ContainsKey(id))
                return;

            tableOfResult.Rows.Add(id, window.data.ExcelFileMgr[id].quest, window.data.GetPercentOfQuestByEntry(id), window.data.ExcelFileMgr[id].time);
        }

        private void EndTest(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tableOfResult_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(tableOfResult.Size.Width + 17, tableOfResult.Size.Height + 40);
        }
    }
}
