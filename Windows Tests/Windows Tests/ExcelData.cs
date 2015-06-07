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

        public ExcelData(string excel)
        {
            if (!File.Exists(excel))
                throw new Exception("Файл не найден.");

            book = new XLWorkbook(excel);
        }

        public void NextQuest(int questId)
        {
            AnswerList = new List<string>();

            // get list of data
            var worksheet = book.Worksheet(1); // always 1 list!

            // get row by id
            var row = worksheet.Row(questId);

            // get quest 
            Quest = row.FirstCellUsed().Value.ToString();

            // get correct answer
            CorrectAnswer = row.LastCellUsed().Value.ToString();

            // get  answers
            for (int index = 2; index < row.Cells().Count() - 1; index++)
            {
                // skip null value
                if (row.Cell(index).IsEmpty())
                    continue;

                // add answer to list
                AnswerList.Add(row.Cell(index).Value.ToString());
            }

            // get quest type
            Qtype = (QuestType)Enum.Parse(typeof(QuestType), row.Cell(row.Cells().Count() - 1).Value.ToString());


        }

    }
}
