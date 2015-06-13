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

        public string StudentName, Group; 

        private void button1_Click(object sender, EventArgs e)
        {
            data.CalculateAmount(rowId, listBox1);
            if (rowId < data.ExcelFileMgr.Count)
            {
                rowId++;
                data.NextQuest(rowId, this);
            }
            else
            {
                ScreenResult res = new ScreenResult();
                res.Owner = this;
                res.ShowDialog();
            }
        }
    }
}
