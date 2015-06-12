using System;
using System.Collections.Generic;
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
    public class ExcelData
    {
        public enum QuestType
        {
            Single = 1,
            Multiple,
        }

        private int IntQuestType;
        private ExcelFile _ExcelData;

        public Dictionary<int, ExcelFile> ExcelFileMgr;
        public ListBox multiple;
        public RadioButton[] radio;
        public double UltimateResult;
        public Dictionary<int, double> ResultCollection = new Dictionary<int, double>();
        MainWindow _MainWindow;

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
        }

        public QuestType QType;

        public ExcelData()
        {
            //_MainWindow = new MainWindow();
        }

        public void LoadingQuestions(TreeView tree)
        {
            DirectoryInfo root = new DirectoryInfo(@"Data\");
            DirectoryInfo[] files = root.GetDirectories();
            FileInfo[] info;
            for (int i = 0; i < files.Length; i++)
            {
                tree.Nodes.Add(files[i].Name);
                info = files[i].GetFiles();

                for (int z = 0; z < info.Length; z++)
                    tree.Nodes[i].Nodes.Add(info[z].Name.Substring(0, info[z].Name.Length - Path.GetExtension(info[z].Name).Length));
            }
        }

        public void LoadingQuery(string file)
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

                // get random 
                var rand = new Random();

                // then fill answer list random
                _ExcelData.response = intermediateData.OrderBy(sort => rand.Next()).ToList();

                // add data of each quest to collection
                ExcelFileMgr.Add(counter, _ExcelData);
            }
        }

        public string ShowCurrentQuest(int id)
        {
            if (!ExcelFileMgr.ContainsKey(id))
                return string.Empty;

            string cor = "";
            string an = "";

            for (int i = 0; i < ExcelFileMgr[id].correct.Count; i++)
                cor += ", " + ExcelFileMgr[id].correct[i];

            for (int i = 0; i < ExcelFileMgr[id].response.Count; i++)
                an += ", " + ExcelFileMgr[id].response[i];

            return string.Format("Quest: {0}\r\nType: {1}\r\nCorrectedCount: {2}\r\nCorrect: {3}\r\nAnswers: {4}\r\n",
                ExcelFileMgr[id].quest, ExcelFileMgr[id].QueType, ExcelFileMgr[id].NumberOfCorrect, cor, an);
        }

		// all = 3; (all - not correct) / all
        public void NextQuest(int rowId, Form workspace)
        {
            if (!ExcelFileMgr.ContainsKey(rowId))
                throw new Exception("Row not found!");

            // main build
            Label labelQuest = new Label();
            foreach (Control c in workspace.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    c.Text = ExcelFileMgr[rowId].quest;
                }
            }

            foreach (Control c in workspace.Controls)
            {
                if (c.GetType() == typeof(ListBox))
                {
                    ((ListBox)c).Items.Clear();
                    ((ListBox)c).Items.AddRange(ExcelFileMgr[rowId].response.ToArray());
                }
            }

    //        _MainWindow.label1.Text = ExcelFileMgr[rowId].quest;
    //        _MainWindow.listBox1.Items.AddRange(ExcelFileMgr[rowId].response.ToArray());
        }

        public void CalculateAmount(int entry, ListBox listBox1)
        {
            if (!ExcelFileMgr.ContainsKey(entry))
                throw new Exception("Not found!");

            int wrong = 0;
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                if (!ExcelFileMgr[entry].correct.Contains(listBox1.GetItemText(listBox1.SelectedItems[i])))
                    wrong++;
            }

            UltimateResult = ((ExcelFileMgr[entry].correct.Count - wrong) / ExcelFileMgr[entry].correct.Count);

            ResultCollection.Add(entry, UltimateResult);
        }

        public string Result()
        {
            double lenght = 0;

            for (int i = 1; i <= ResultCollection.Count; i++)
            {
                if (!ResultCollection.ContainsKey(i))
                    continue;

                lenght += ResultCollection[i];
            }
            double result = lenght / ExcelFileMgr.Count;
            return string.Format("{0:0.00%}", result);
        }

        private void ClearLabel(Form owner)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.GetType() == typeof(Label))
                {
                    c.Dispose();
                    owner.Controls.Remove(c);
                }
            }

        }

        private void ClearListBox(Form owner)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.GetType() == typeof(ListBox))
                {
                    c.Dispose();
                    owner.Controls.Remove(c);
                }
            }
        }

        private void ClearRadioButton(Form owner)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.GetType() == typeof(RadioButton))
                {
                    c.Dispose();
                    owner.Controls.Remove(c);
                }
            }
        }

        private void ClearGroupBox(Form owner)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.GetType() == typeof(GroupBox))
                {
                    c.Dispose();
                    owner.Controls.Remove(c);
                }
            }
        }

    }
}
