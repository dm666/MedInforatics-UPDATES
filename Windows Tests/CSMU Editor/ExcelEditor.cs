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
        public enum QuestType
        {
            Single = 1,
            Multiple,
        }

        private int IntQuestType;
        private ExcelFile _ExcelData;
        private string _file;

        public Dictionary<int, ExcelFile> ExcelFileMgr;
        public double UltimateResult;
        public Dictionary<int, double> ResultCollection = new Dictionary<int, double>();


        public class ExcelFile
        {
            public ExcelFile()
            {
                response = new List<string>();
                correct = new List<string>();
            }

            public string quest;
            public List<string> correct;
            public QuestType QueType;
            public List<string> response;
            public int NumberOfCorrect;
            public int time;
        }

        // for create
        public ExcelEditor() { }

        // for edit
        public ExcelEditor(string file)
        {
            _file = file;
        }

        public bool Load(string file)
        {
            if (!File.Exists(file))
                throw new Exception("File not found!");

            ExcelFileMgr = new Dictionary<int, ExcelFile>();

            var workbook = new XLWorkbook(file);
            var worksheet = workbook.Worksheet(1);

            for (int counter = 1; counter <= worksheet.Rows().Count(); counter++)
            {
                _ExcelData = new ExcelFile();
                List<string> intermediateData = new List<string>();
                IntQuestType = 0;

                var row = worksheet.Row(counter);

                if (row.IsEmpty())
                    continue;

                for (int i = 1; i <= row.Cells().Count(); i++)
                {
                    if (row.Cell(i).IsEmpty())
                        continue;

                    intermediateData.Add(row.Cell(i).Value.ToString());
                }

                // get quest
                _ExcelData.quest = intermediateData[0];

                // get quest type
                IntQuestType = int.Parse(intermediateData[1]);

                // get correct count
                _ExcelData.NumberOfCorrect = int.Parse(intermediateData[2]);

                // convert int type to QuestType
                switch (IntQuestType)
                {
                    case 1:
                        _ExcelData.QueType = QuestType.Single;
                        break;
                    case 2:
                        _ExcelData.QueType = QuestType.Multiple;
                        break;
                    default:
                        _ExcelData.QueType = QuestType.Single;
                        break;
                }

                // get index of last correct
                int LastIndex = intermediateData.Count - 1;

                // get index of first correct
                int FirstIndex = intermediateData.Count - _ExcelData.NumberOfCorrect;

                // get correct answer
                if (_ExcelData.QueType == QuestType.Single)
                    _ExcelData.correct.Add(intermediateData[LastIndex]);
                else if (_ExcelData.QueType == QuestType.Multiple)
                {
                    for (int index = LastIndex; index >= FirstIndex; index--)
                        _ExcelData.correct.Add(intermediateData[index]);

                }

                intermediateData.RemoveRange(FirstIndex, _ExcelData.NumberOfCorrect);

                intermediateData.RemoveRange(0, 3);        

                // then fill answer list random
                _ExcelData.response = intermediateData;

                // add data of each quest to collection
                ExcelFileMgr.Add(counter, _ExcelData);
            }

            return ExcelFileMgr.Count > 0;
        }

        public void Save(string file) { }

        public void AddQuestList(ListBox box)
        {
            for (int i = 1; i <= ExcelFileMgr.Count; i++)
                box.Items.Add(i + " — " + ExcelFileMgr[i].quest);
        }

        public void AddRow()
        {
        }

        public void DeleteRow() { }

        private void daw()
        {
            Dictionary<int, int> t = new Dictionary<int, int>();
            KeyValuePair<int, int>[] ara = t.ToArray();
            Int32 newSize = (Int32)t.Count + 1;

            Array.Resize(ref ara, newSize);


            // t = ara.ToDictionary(x => x.Key, x => x.Value);

    //        button1.Text = ara.Count().ToString();

            ara[ara.Count() - 1] = new KeyValuePair<int, int>(3, 21230);

        //    for (int i = 0; i < ara.Count(); i++)
            //    dataGridView1.Rows.Add(ara[i].Key, ara[i].Value);

            t = ara.ToDictionary(x => x.Key, x => x.Value);

       //     for (int i = 0; i < t.Count; i++)
           //     dataGridView1.Rows.Add(i, t[i]);
        }
    }
}
