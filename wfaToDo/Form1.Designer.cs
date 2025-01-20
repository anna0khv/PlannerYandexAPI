namespace wfaToDo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbxIsSync = new CheckBox();
            label1 = new Label();
            lblName = new Label();
            SuspendLayout();
            // 
            // cbxIsSync
            // 
            cbxIsSync.AutoSize = true;
            cbxIsSync.Location = new Point(12, 15);
            cbxIsSync.Name = "cbxIsSync";
            cbxIsSync.Size = new Size(142, 24);
            cbxIsSync.TabIndex = 1;
            cbxIsSync.Text = "Синхронизация";
            cbxIsSync.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(397, 16);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 2;
            label1.Text = "Пользователь:";
            label1.Visible = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(503, 16);
            lblName.Name = "lblName";
            lblName.Size = new Size(50, 20);
            lblName.TabIndex = 3;
            lblName.Text = "label2";
            lblName.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 450);
            Controls.Add(lblName);
            Controls.Add(label1);
            Controls.Add(cbxIsSync);
            Name = "Form1";
            Text = "Мой Планнер";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox cbxIsSync;
        private Label label1;
        private Label lblName;
    }
}
