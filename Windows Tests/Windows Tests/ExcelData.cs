﻿using System;
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

        public ExcelData() { }

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

                // check for error's
                if (ErrorLog(counter, IntQuestType, _ExcelData.NumberOfCorrect))
                {
                    MessageBox.Show("В загрузке данного теста произошло ошибка. Обратитесь к администратору");
                    Application.Exit();
                }

                // get random 
                var rand = new Random();

                // then fill answer list random
                _ExcelData.response = intermediateData.OrderBy(sort => rand.Next()).ToList();

                // add data of each quest to collection
                ExcelFileMgr.Add(counter, _ExcelData);
            }

            // random sorting
            ExcelFileMgr = RandomizeDictio(ExcelFileMgr);

        }

        private bool ErrorLog(int id, int type, int count)
        {
            using (StreamWriter wr = File.AppendText("ErrorLog.txt"))
            {
                //wr.WriteLine(DateTime.Now);

                if (type > 2 || type < 1)
                {
                    wr.WriteLine(DateTime.Now);
                    wr.WriteLine("Вопрос № {0} содержит некорректный тип.", id);
                    return true;
                }
                else if (type == 1 && count > 1)
                {
                    wr.WriteLine(DateTime.Now);
                    wr.WriteLine("Вопрос № {0} не должен превышать 1-го ответа.", id);
                    return true;
                }
                else if (type == 1 && count < 1)
                {
                    wr.WriteLine(DateTime.Now);
                    wr.WriteLine("Вопрос № {0}: не указано количество правильных ответов.", id);
                    return true;
                }
                else if (type == 2 && count < 2)
                {
                    wr.WriteLine(DateTime.Now);
                    wr.WriteLine("Вопрос № {0} должен содержать минимум 2 ответа.", id);
                    return true;
                }
            }

            return false;
        }

        private Dictionary<int, ExcelFile> RandomizeDictio(Dictionary<int, ExcelFile> dict)
        {
            var r = new Random();

            for (int i = dict.Count; i > 0; i--)
            {
                int j = r.Next(1, i);
                var t = dict[i];
                dict[i] = dict[j];
                dict[j] = t;
            }
            return dict;
        }

        public void NextQuest(int rowId, Form workspace, Label LQuest, ListBox ListAnswer)
        {
            if (!ExcelFileMgr.ContainsKey(rowId))
                throw new Exception("Row not found!");

            LQuest.Text = ExcelFileMgr[rowId].quest;
            ListAnswer.Items.Clear();
            ListAnswer.Items.AddRange(ExcelFileMgr[rowId].response.ToArray());
        }

        public void CalculateAmount(int entry, ListBox listBox1, int diff)
        {
            if (!ExcelFileMgr.ContainsKey(entry))
                throw new Exception("Not found!");

            int wrong = 0;

            if (listBox1.SelectedItems.Count < 1)
            {
                ResultCollection.Add(entry, 0);
                return;
            }

            if (ExcelFileMgr[entry].QueType == QuestType.Single)
            {
                if (!ExcelFileMgr[entry].correct.Contains(listBox1.GetItemText(listBox1.SelectedItem)))
                    wrong++;
            }
            else if (ExcelFileMgr[entry].QueType == QuestType.Multiple)
            {
                if (listBox1.SelectedItems.Count > 1)
                {
                    for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                    {
                        if (!ExcelFileMgr[entry].correct.Contains(listBox1.GetItemText(listBox1.SelectedItems[i])))
                            wrong++;
                    }
                }

                if (listBox1.SelectedItems.Count == 1)
                {
                    if (!ExcelFileMgr[entry].correct.Contains(listBox1.GetItemText(listBox1.SelectedItem.ToString())))
                        wrong = ExcelFileMgr[entry].correct.Count;
                }

                if (listBox1.SelectedItems.Count == ExcelFileMgr[entry].response.Count)
                {
                    ResultCollection.Add(entry, 0);
                    return;
                }

                if (listBox1.SelectedItems.Count == wrong)
                    wrong = ExcelFileMgr[entry].correct.Count;
            }

            UltimateResult = (ExcelFileMgr[entry].correct.Count - wrong);
            UltimateResult /= ExcelFileMgr[entry].correct.Count;

            ResultCollection.Add(entry, UltimateResult);
            ExcelFileMgr[entry].time = ((60 - diff) == 0 ? 1 : (60 - diff));
        }

        public string GetPercentOfQuestByEntry(int questId)
        {
            if (!ExcelFileMgr.ContainsKey(questId))
                throw new Exception("Not found!");

            return string.Format("{0:0.0%}", ResultCollection[questId]);
        }

        public void TestResultData(int id, DataGridView dataGrid)
        {
            if (!ExcelFileMgr.ContainsKey(id))
                throw new Exception("Not found!");

            string[] quest = new string[3];

            quest[0] = id.ToString();
            quest[1] = ExcelFileMgr[id].quest;
            quest[2] = GetPercentOfQuestByEntry(id);

            dataGrid.Rows.Add(quest);
        }

        public double Result()
        {
            double lenght = 0;

            for (int i = 1; i <= ResultCollection.Count; i++)
            {
                if (!ResultCollection.ContainsKey(i))
                    continue;

                lenght += ResultCollection[i];
            }
            double result = lenght / ExcelFileMgr.Count;
            return result;
        }

        public string AllTestResultDouble()
        {
            return string.Format("{0:0.0%}", Result());
        }
    }
}
