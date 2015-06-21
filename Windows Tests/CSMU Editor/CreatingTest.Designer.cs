namespace CSMU_Editor
{
    partial class CreatingTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.questBox = new System.Windows.Forms.TextBox();
            this.questType = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SaveDoc = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.testName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // questBox
            // 
            this.questBox.Location = new System.Drawing.Point(8, 26);
            this.questBox.Multiline = true;
            this.questBox.Name = "questBox";
            this.questBox.Size = new System.Drawing.Size(248, 79);
            this.questBox.TabIndex = 1;
            // 
            // questType
            // 
            this.questType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.questType.FormattingEnabled = true;
            this.questType.Items.AddRange(new object[] {
            "Один ответ",
            "Более 1 ответа"});
            this.questType.Location = new System.Drawing.Point(280, 26);
            this.questType.Name = "questType";
            this.questType.Size = new System.Drawing.Size(95, 21);
            this.questType.TabIndex = 3;
            this.questType.SelectedIndexChanged += new System.EventHandler(this.EnableWorkspace);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(6, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(443, 215);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Правильный ответ";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "Ответ";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 340;
            // 
            // Add
            // 
            this.Add.Enabled = false;
            this.Add.Location = new System.Drawing.Point(264, 68);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(82, 37);
            this.Add.TabIndex = 5;
            this.Add.Text = "Добавить ответ";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.AddRow);
            // 
            // Remove
            // 
            this.Remove.Enabled = false;
            this.Remove.Location = new System.Drawing.Point(361, 68);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(82, 37);
            this.Remove.TabIndex = 6;
            this.Remove.Text = "Удалить ответ";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.DeleteRow);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 22);
            this.label1.TabIndex = 9;
            this.label1.Text = "Вопрос";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(264, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 22);
            this.label3.TabIndex = 11;
            this.label3.Text = "Тип вопроса";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SaveDoc
            // 
            this.SaveDoc.Location = new System.Drawing.Point(368, 329);
            this.SaveDoc.Name = "SaveDoc";
            this.SaveDoc.Size = new System.Drawing.Size(82, 25);
            this.SaveDoc.TabIndex = 12;
            this.SaveDoc.Text = "Сохранить";
            this.SaveDoc.UseVisualStyleBackColor = true;
            this.SaveDoc.Click += new System.EventHandler(this.Save);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(280, 329);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(82, 25);
            this.Next.TabIndex = 13;
            this.Next.Text = "Далее";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.CNextQuest);
            // 
            // testName
            // 
            this.testName.Location = new System.Drawing.Point(132, 331);
            this.testName.Name = "testName";
            this.testName.Size = new System.Drawing.Size(135, 20);
            this.testName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 331);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 22);
            this.label2.TabIndex = 15;
            this.label2.Text = "Название теста";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreatingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 356);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.testName);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.SaveDoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.questType);
            this.Controls.Add(this.questBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatingTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreatingTest";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox questBox;
        private System.Windows.Forms.ComboBox questType;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button SaveDoc;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.TextBox testName;
        private System.Windows.Forms.Label label2;
    }
}