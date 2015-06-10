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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelData data = new ExcelData();
            data.LoadingQuery(@"C:\users\dm666\desktop\nick.xlsx");

            MessageBox.Show(data.ShowCurrentQuest(1));
            MessageBox.Show(data.ShowCurrentQuest(2));
            MessageBox.Show(data.ShowCurrentQuest(3));
        }
    }
}
