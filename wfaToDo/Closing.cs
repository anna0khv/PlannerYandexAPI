﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaToDo
{
    public partial class Closing : Form
    {
        public Closing()
        {
            InitializeComponent();
        }

        public void updateProgress(int i)
        {
            this.label2.Text = $"Прогресс: {i + 1}/4";
        }
    }
}
