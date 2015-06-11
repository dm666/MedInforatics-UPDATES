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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            data = new ExcelData();
            data.LoadingQuery(@"C:\users\dm666\desktop\nick.xlsx");
          //  MessageBox.Show(data.ShowCurrentQuest(3));
            data.NextQuest(rowId, this);
        }

        ExcelData data;
        int rowId = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            if (rowId < data.ExcelFileMgr.Count)
            {
                data.CalculateAmount(rowId);
                rowId++;
                data.NextQuest(rowId, this);
            }
            else
                MessageBox.Show(data.Result().ToString());
        }
    }
}
