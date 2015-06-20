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

namespace CSMU_Editor
{
    public partial class CreatingTest : Form
    {
        public CreatingTest()
        {
            InitializeComponent();
            excel = new ExcelEditor();
            data = new List<ExcelEditor.ExcelFile>();
            RData = new List<string>();
        }

        ExcelEditor excel;
        int row = 1, index = 0;
        List<ExcelEditor.ExcelFile> data;
        List<string> RData;

        private void AddRow(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void DeleteRow(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
                return;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                dataGridView1.Rows.RemoveAt(row.Index);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
                return;

            if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].Value) == false)
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
            else
                dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
        }

        private void CNextQuest(object sender, EventArgs e)
        {
            int chckCount = 0;

            if (questBox.Text.Length <= 0)
            {
                MessageBox.Show("Добавьте вопрос.");
                return;
            }

            if (questType.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип вопроса.");
                return;
            }

            if (dataGridView1.Rows.Count < 2)
            {
                MessageBox.Show("Ответов должно быть минимум 2.");
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == false)
                    chckCount++;

                if (dataGridView1.Rows[i].Cells[1].Value == null)
                {
                    MessageBox.Show("Заполните все поля в вариантах ответов.");
                    return;
                }

                if (chckCount == dataGridView1.Rows.Count)
                {
                    MessageBox.Show("Должен быть минимум 1 ответ");
                    return;
                }

            }

            if (testName.Text.Length < 1 || string.IsNullOrWhiteSpace(testName.Text))
            {
                MessageBox.Show("Введите название файла.");
                return;
            }

            ExcelEditor.ExcelFile EXfile = new ExcelEditor.ExcelFile();

            EXfile.quest = questBox.Text;
            EXfile.QueType = questType.SelectedIndex == 0 ? 1 : 2;
            EXfile.response = new List<string>();
            EXfile.correct = new List<string>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)
                    EXfile.correct.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());

                EXfile.response.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }

            EXfile.NumberOfCorrect = EXfile.correct.Count;

            data.Add(EXfile);
            excel.AddRow(testName.Text, row, index, data);
            row++;
            index++;
        }

        private void Save(object sender, EventArgs e)
        {
            if (data.Count <= 0)
            {
                MessageBox.Show("Невозможно создать пустой тест.");
                return;
            }

            if (testName.Text.Length <= 0 || string.IsNullOrWhiteSpace(testName.Text))
            {
                MessageBox.Show("Введите название файла.");
                return;
            }

            if (File.Exists(testName.Text))
            {
                if (MessageBox.Show("Файл с таким именем существует. Хотите перезаписать?",
                    "Сохранение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    excel.Save(testName.Text);
                    return;
                }
            }

            if (MessageBox.Show("Вы точно хотите сохранить?", "Сохранение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                excel.Save(testName.Text);
        }
    }
}
