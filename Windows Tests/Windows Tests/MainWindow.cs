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
            data = new ExcelData();
            data.LoadingQuery(@"C:\users\dm666\desktop\nick.xlsx");
            data.NextQuest(1, this);
        }

        ExcelData data;
        int rowId = 2;

        private void button1_Click(object sender, EventArgs e)
        {
            if (rowId <= data.ExcelFileMgr.Count)
            {
                data.NextQuest(rowId, this);
                rowId++;
            }
            else
                MessageBox.Show(data.Result());
        }
    }
}
