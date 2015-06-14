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
using ClosedXML;
using ClosedXML.Excel;
using ClosedXML.Excel.Misc;

namespace Windows_Tests
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            QuestionList quest = new QuestionList();
            quest.Owner = this;
            quest.ShowDialog();
        }

        public ExcelData data = new ExcelData();
        public int rowId;
        public int SECOND = 1000;
        int diff;

        public string StudentName, Group; 

        private void button1_Click(object sender, EventArgs e)
        {
            data.CalculateAmount(rowId, listBox1);
            if (rowId < data.ExcelFileMgr.Count)
            {
                resetTime();
                rowId++;
                data.NextQuest(rowId, this, label1, listBox1);
                label2.Text = "Вопрос " + rowId.ToString() + " из " + data.ExcelFileMgr.Count.ToString();
            }
            else
                Table();
        }

        private void Table()
        {
            ScreenResult res = new ScreenResult();
            res.Owner = this;
            res.ShowDialog();
            this.Hide();
        }

        private string timeleft(int second)
        {
            string value = "";

            switch (second % 10)
            {
                case 1:
                    value = "секунда";
                    break;
                case 2:
                case 3:
                case 4:
                    value = "секунды";
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 0:
                    value = "секунд";
                    break;
            }

            return string.Format("Осталось {0} {1}", second, value);
        }

        private void resetTime()
        {
            timer.Stop();
            label3.Text = "";
            label3.ForeColor = Color.Black;
            progressBar1.Value = 0;
            
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
                timer.Enabled = false;

            diff = (progressBar1.Maximum - progressBar1.Value) / SECOND;

            if (diff > 0)
            {
                if (diff < 10)
                    label3.ForeColor = Color.Red;

                label3.Text = timeleft(diff);
                progressBar1.Increment(timer.Interval);
            }
            else
            {
                label3.Text = "Время вышло.";
                data.CalculateAmount(rowId, listBox1);
                if (rowId < data.ExcelFileMgr.Count)
                {
                    resetTime();
                    rowId++;
                    data.NextQuest(rowId, this, label1, listBox1);
                }
                else
                    Table();
            }
        }
    }
}
