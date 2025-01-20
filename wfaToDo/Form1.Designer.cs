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
            btnChangeUser = new Button();
            SuspendLayout();
            // 
            // cbxIsSync
            // 
            cbxIsSync.AutoSize = true;
            cbxIsSync.Location = new Point(15, 17);
            cbxIsSync.Margin = new Padding(4, 3, 4, 3);
            cbxIsSync.Name = "cbxIsSync";
            cbxIsSync.Size = new Size(156, 27);
            cbxIsSync.TabIndex = 1;
            cbxIsSync.Text = "Синхронизация";
            cbxIsSync.UseVisualStyleBackColor = true;
            cbxIsSync.CheckedChanged += cbxIsSync_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(496, 18);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(128, 23);
            label1.TabIndex = 2;
            label1.Text = "Пользователь:";
            label1.Visible = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(629, 18);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(56, 23);
            lblName.TabIndex = 3;
            lblName.Text = "label2";
            lblName.Visible = false;
            // 
            // btnChangeUser
            // 
            btnChangeUser.Location = new Point(280, 18);
            btnChangeUser.Name = "btnChangeUser";
            btnChangeUser.Size = new Size(209, 29);
            btnChangeUser.TabIndex = 4;
            btnChangeUser.Text = "Сменить пользователя";
            btnChangeUser.UseVisualStyleBackColor = true;
            btnChangeUser.Visible = false;
            btnChangeUser.Click += btnChangeUser_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 518);
            Controls.Add(btnChangeUser);
            Controls.Add(lblName);
            Controls.Add(label1);
            Controls.Add(cbxIsSync);
            Font = new Font("Sitka Small", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 3, 4, 3);
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
        private Button btnChangeUser;
    }
}
