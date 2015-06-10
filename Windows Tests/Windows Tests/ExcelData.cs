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
        public CheckedListBox multiple;
        public RadioButton[] radio;

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

        public QuestType Qtype;

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
            List<string> intermediateData = new List<string>();

            var workbook = new XLWorkbook(file);
            var worksheet = workbook.Worksheet(1);

            for (int counter = 1; counter < worksheet.Rows().Count(); counter++)
            {
                _ExcelData = new ExcelFile();

                var row = worksheet.Row(counter);

                for (int i = 1; i < row.CellCount(); i++)
                {
                    if (row.Cell(i).IsEmpty())
                        continue;

                    intermediateData.Add(row.Cell(i).Value.ToString());
                }

                _ExcelData.quest = intermediateData[0];

                //get number of correct answer's
                _ExcelData.NumberOfCorrect = int.Parse(intermediateData[intermediateData.Count - 1]);

                // get quest type 
                IntQuestType = int.Parse(intermediateData[intermediateData.Count - 2]);

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
                int LastIndex = intermediateData.Count - 2;

                // get correct answer
                if (_ExcelData.QueType == QuestType.Multiple)
                {
                    for (int i = LastIndex - 1; i >= (LastIndex - _ExcelData.NumberOfCorrect); i--)
                        _ExcelData.correct.Add(intermediateData[i]);
                }
                else
                    _ExcelData.correct.Add(intermediateData[LastIndex]);

                // remove questType, correctedCount and correct answer
                intermediateData.RemoveRange(intermediateData.Count - (_ExcelData.NumberOfCorrect + 2), _ExcelData.NumberOfCorrect + 2 );

                // remove quest value
                intermediateData.RemoveAt(0);

                // get random 
                var rand = new Random();

                // then fill answer list random
                _ExcelData.response = intermediateData.OrderBy(sort => rand.Next()).ToList();

                // add data of each quest to collection
                ExcelFileMgr.Add(counter, _ExcelData);
            }
        }

		// all = 3; (all - not correct) / all
        public void NextQuest(int rowId, Form workspace)
        {
            if (!ExcelFileMgr.ContainsKey(rowId))
                throw new Exception("Row not found!");

            ClearLabel(workspace);
            ClearGroupBox(workspace);
            ClearCheckedListBox(workspace);
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
                multiple = new CheckedListBox();
                multiple.Items.AddRange(ExcelFileMgr[rowId].response.ToArray());
                multiple.Location = new System.Drawing.Point(1, 59);

                workspace.Controls.Add(multiple);
            }

            workspace.Controls.Add(labelQuest);
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

        private void ClearCheckedListBox(Form owner)
        {
            foreach (Control c in owner.Controls)
            {
                if (c.GetType() == typeof(CheckedListBox))
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
