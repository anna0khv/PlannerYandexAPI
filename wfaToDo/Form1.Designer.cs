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
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // cbxIsSync
            // 
            cbxIsSync.AutoSize = true;
            cbxIsSync.BackColor = Color.White;
            cbxIsSync.Location = new Point(15, 17);
            cbxIsSync.Margin = new Padding(4, 3, 4, 3);
            cbxIsSync.Name = "cbxIsSync";
            cbxIsSync.Size = new Size(156, 27);
            cbxIsSync.TabIndex = 1;
            cbxIsSync.Text = "Синхронизация";
            cbxIsSync.UseVisualStyleBackColor = false;
            cbxIsSync.CheckedChanged += cbxIsSync_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(689, 21);
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
            lblName.Location = new Point(822, 21);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(56, 23);
            lblName.TabIndex = 3;
            lblName.Text = "label2";
            lblName.Visible = false;
            // 
            // btnChangeUser
            // 
            btnChangeUser.BackgroundImage = Properties.Resources.user_icon;
            btnChangeUser.BackgroundImageLayout = ImageLayout.Zoom;
            btnChangeUser.FlatStyle = FlatStyle.Flat;
            btnChangeUser.Location = new Point(640, 9);
            btnChangeUser.Name = "btnChangeUser";
            btnChangeUser.Size = new Size(42, 40);
            btnChangeUser.TabIndex = 4;
            btnChangeUser.UseVisualStyleBackColor = true;
            btnChangeUser.Visible = false;
            btnChangeUser.Click += btnChangeUser_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.ControlLight;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Все задачи", "Невыполненные", "Выполненные" });
            comboBox1.Location = new Point(202, 17);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 31);
            comboBox1.TabIndex = 5;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 518);
            Controls.Add(comboBox1);
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
        private ComboBox comboBox1;
    }
}
