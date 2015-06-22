using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using ClosedXML;
using ClosedXML.Excel;
using ClosedXML.Excel.Misc;

namespace CSMU_Editor
{
    public class ExcelEditor
    {
        XLWorkbook book;
        IXLWorksheet work;

        public class ExcelFile
        {
            public ExcelFile()
            {
                response = new List<string>();
                correct = new List<string>();
            }

            public string quest;
            public List<string> correct;
            public int QueType;
            public List<string> response;
            public int NumberOfCorrect;
        }

        // for create
        public ExcelEditor()
        {
            book = new XLWorkbook();
            work = book.Worksheets.Add("1");
        }

        public void AddRow(string file, int row, int index,  List<ExcelFile> data)
        {
            work.Cell(row, 1).Value = data[index].quest;
            work.Cell(row, 2).Value = data[index].QueType;
            work.Cell(row, 3).Value = data[index].NumberOfCorrect;

            int responseRange = data[index].response.Count + 4;
            int correctRange = responseRange + data[index].correct.Count;

            for (int x = 4, i = 0; x < responseRange; i++, x++)
                work.Cell(row, x).Value = data[index].response[i];

            for (int z = responseRange, i = 0; z < correctRange; i++, z++)
                work.Cell(row, z).Value = data[index].correct[i];
        }

        public void Save(string file) { book.SaveAs(file + ".xlsx"); }

        private void AddToMgr()
        {
            Dictionary<int, int> t = new Dictionary<int, int>();
            KeyValuePair<int, int>[] ara = t.ToArray();

            Int32 newSize = (Int32)t.Count + 1;
            Array.Resize(ref ara, newSize);

            ara[ara.Count() - 1] = new KeyValuePair<int, int>(3, 21230);
            t = ara.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
