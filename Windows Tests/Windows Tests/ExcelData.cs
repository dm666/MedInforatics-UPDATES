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
        enum QuestType
        {
            Single = 1,
            Multiple,
        }

        private XLWorkbook book;
        private string Quest, CorrectAnswer;
        private QuestType Qtype;
        private int CorrectedCount;
        private List<string> AnswerList;
        private int IntQuestType;

        public ExcelData(string excel)
        {
            if (!File.Exists(excel))
                throw new Exception("Файл не найден.");

            book = new XLWorkbook(excel);
        }

        public void NextQuest(int questId, Form workspace)
        {
            AnswerList = new List<string>();
            List<string> allData = new List<string>();

            // get list of data
            var worksheet = book.Worksheet(1); // always 1 list!

            // get row by id
            var row = worksheet.Row(questId);

            // get all data from excel 
            for (int i = 1; i < row.CellCount(); i++)
            {
                if (row.Cell(i).IsEmpty())
                    continue;

                allData.Add(row.Cell(i).Value.ToString());
            }

            // now get each element
            // first - quest 
            Quest = allData[0];

            // get correct answer
            CorrectAnswer = allData[allData.Count - 3];

            // get quest type 
            IntQuestType = int.Parse(allData[allData.Count - 2]);

            // convert int type to QuestType
            switch (IntQuestType)
            {
                case 1:
                    Qtype = QuestType.Single;
                    break;
                case 2:
                    Qtype = QuestType.Multiple;
                    break;
                default:
                    Qtype = QuestType.Single;
                    break;
            }

            // get corrected answers
            CorrectedCount = int.Parse(allData[allData.Count - 1]);

            // now get answer's
            // remove quest value
            allData.RemoveAt(0);

            // remove questType, correctedCount and correct answer
            allData.RemoveRange(allData.Count - 3, 3);

            // then fill answer list
            AnswerList = allData;

            // to be continue..
        }

        private void BuildWorkspace(Form workspace, string quest, string correct, List<string> answers)
        {
            ClearLabel(workspace);
            ClearGroupBox(workspace);
            ClearCheckedListBox(workspace);
            ClearRadioButton(workspace);

            int count = answers.Count;

            // main build
            Label labelQuest = new Label();

            labelQuest.Text = quest;
            labelQuest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            labelQuest.Location = new System.Drawing.Point(0, 0);
            labelQuest.Name = "labelQuest";
            labelQuest.Size = new System.Drawing.Size(388, 56);
            labelQuest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            if (Qtype == QuestType.Single)
            {
                GroupBox groupBox1 = new GroupBox();

                groupBox1.Location = new System.Drawing.Point(1, 59);
                groupBox1.Name = "groupBox1";
                groupBox1.Size = new System.Drawing.Size(386, 121);
                groupBox1.TabIndex = 1;
                groupBox1.TabStop = false;
                groupBox1.Text = "Варианты ответов";
                groupBox1.Visible = true;

                RadioButton[] radio = new RadioButton[count];

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
                    radio[i].Text = answers[i];
                }

                workspace.Controls.Add(groupBox1);
            }
            else if (Qtype == QuestType.Multiple)
            {
                CheckedListBox multiple = new CheckedListBox();
                multiple.Items.AddRange(answers.ToArray());
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
