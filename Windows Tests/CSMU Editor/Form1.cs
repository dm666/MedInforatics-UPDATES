using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMU_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ChangeEvent(object sender, EventArgs e)
        {
            EditingTest window = new EditingTest();
            window.Show();            
            this.Hide();
        }

        private void CreateNewTest(object sender, EventArgs e)
        {
            CreatingTest window = new CreatingTest();
            window.Show();
            this.Hide();
        }
    }
}
