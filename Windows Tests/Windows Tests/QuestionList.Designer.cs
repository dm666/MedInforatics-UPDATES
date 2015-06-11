namespace Windows_Tests
{
    partial class QuestionList
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
            this.tree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(1, 0);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(257, 241);
            this.tree.TabIndex = 0;
            // 
            // QuestionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 242);
            this.Controls.Add(this.tree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "QuestionList";
            this.Text = "QuestionList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tree;
    }
}