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

        private XLWorkbook book;
        private string Quest, CorrectAnswer;
        
        private int CorrectedCount;
        private List<string> AnswerList;
        private int IntQuestType;
        private ExcelFile _ExcelData;

        public Dictionary<int, ExcelFile> ExcelFileMgr;
        public ListBox multiple;
        public RadioButton[] radio;
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
        }

        public QuestType QType;

        public ExcelData()
        {
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

                _ExcelData.quest = intermediateData[0];
                IntQuestType = int.Parse(intermediateData[1]);
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
                _ExcelData.response = intermediateData;//.OrderBy(sort => rand.Next()).ToList();

                // add data of each quest to collection
                ExcelFileMgr.Add(counter, _ExcelData);
            }
        }

        public void Loading2(string file)
        {

            
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

            ClearLabel(workspace);
            ClearGroupBox(workspace);
            ClearListBox(workspace);
            ClearRadioButton(workspace);

            int count = ExcelFileMgr[rowId].response.Count;
            QuestType cast = ExcelFileMgr[rowId].QueType;

            // main build
            Label labelQuest = new Label();

            labelQuest.Text = ExcelFileMgr[rowId].quest;
            labelQuest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelQuest.Location = new System.Drawing.Point(0, 0);
            labelQuest.Name = "labelQuest";
            labelQuest.Size = new System.Drawing.Size(388, 56);
            labelQuest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            if (cast == QuestType.Single)
            {
                GroupBox groupBox1 = new GroupBox();

                groupBox1.Location = new System.Drawing.Point(1, 59);
                groupBox1.Name = "groupBox1";
                groupBox1.Size = new System.Drawing.Size(386, 121);
                groupBox1.TabIndex = 1;
                groupBox1.TabStop = false;
                groupBox1.Text = "Варианты ответов";
                groupBox1.Visible = true;

                radio = new RadioButton[count];

                for (int i = 0; i < count; i++)
                {
                    radio[i] = new RadioButton();

                    groupBox1.Controls.Add(radio[i]);

                    radio[i].AutoSize = true;
                    radio[i].Location = new System.Drawing.Point(6, 19 + i * 19);
                    radio[i].Name = "radioButton" + i.ToString();
                    radio[i].Size = new System.Drawing.Size(85, 17);
                    radio[i].TabIndex = 0;
                    radio[i].TabStop = true;
                    radio[i].Text = ExcelFileMgr[rowId].response[i];
                }

                workspace.Controls.Add(groupBox1);
            }
            else if (cast == QuestType.Multiple)
            {
                multiple = new ListBox();
                multiple.Items.AddRange(ExcelFileMgr[rowId].response.ToArray());
                multiple.Location = new System.Drawing.Point(1, 59);
                multiple.SelectionMode = SelectionMode.MultiSimple;

                workspace.Controls.Add(multiple);
            }

            workspace.Controls.Add(labelQuest);
        }

        public void CalculateAmount(int entry)
        {
            if (!ExcelFileMgr.ContainsKey(entry))
                throw new Exception("Not found!");

            int wrong = 0;

            if (ExcelFileMgr[entry].QueType == QuestType.Single)
            {
                for (int i = 0; i < radio.Count(); i++)
                {
                    if (radio[i].Checked)
                    {
                        if (!ExcelFileMgr[entry].correct.Contains(radio[i].Text))
                            wrong++;
                    }
                }

                UltimateResult = ((ExcelFileMgr[entry].correct.Count - wrong) / ExcelFileMgr[entry].correct.Count);
            }
            else if (ExcelFileMgr[entry].QueType == QuestType.Multiple)
            {
                for (int i = 0; i < multiple.SelectedItems.Count; i++)
                {
                    if (!ExcelFileMgr[entry].correct.Contains(multiple.GetItemText(multiple.SelectedItems[i])))
                        wrong++;
                }

                UltimateResult = ((ExcelFileMgr[entry].correct.Count - wrong) / ExcelFileMgr[entry].correct.Count);
            }

            ResultCollection.Add(entry, UltimateResult);
        }

        public double Result()
        {
            double lenght = 0;

            MessageBox.Show(ResultCollection.Count().ToString());

            for (int i = 1; i <= ResultCollection.Count + 1; i++)
            {
                if (!ResultCollection.ContainsKey(i))
                    continue;

                lenght += ResultCollection[i];
            }

            return lenght / ExcelFileMgr.Count;
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
