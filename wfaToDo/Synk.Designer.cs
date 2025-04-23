namespace wfaToDo
{
    partial class Synk
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
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(12, 98);
            button1.Name = "button1";
            button1.Size = new Size(445, 29);
            button1.TabIndex = 0;
            button1.Text = "Авторизироваться";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(12, 168);
            button2.Name = "button2";
            button2.Size = new Size(445, 30);
            button2.TabIndex = 1;
            button2.Text = "Продолжить без авторизации";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(143, 35);
            label1.MaximumSize = new Size(200, 0);
            label1.Name = "label1";
            label1.Size = new Size(191, 40);
            label1.TabIndex = 2;
            label1.Text = "Потребуется авторизация на Яндекс.Диск.";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // button3
            // 
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(12, 133);
            button3.Name = "button3";
            button3.Size = new Size(445, 29);
            button3.TabIndex = 3;
            button3.Text = "Авторизироваться и загрузить локальные изменения";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Synk
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 211);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Synk";
            Text = "Synk";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private Button button3;
    }
}