
using System;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static wfaToDo.TaskParsing;

namespace wfaToDo
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Load += MainForm_Load;
            Resize += MainForm_Resize;
        }

        private DataGridView[] grids = new DataGridView[4];
        private Label[] labels = new Label[4];
        private Button[] buttons = new Button[4];
        //TaskParsing tp = new TaskParsing();
        public string? userName = "";
        //private int isDone = 0;
        private enum IS_DONE { ALL = 0, NOT_DONE, DONE };
        IS_DONE isDone = IS_DONE.ALL;


        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    Synk s = new Synk();
        //    using (var form = new Synk()) // Замените YourForm на имя вашей формы
        //    {
        //        if (form.ShowDialog() == DialogResult.OK)
        //        {
        //            // Получаем имя пользователя из формы
        //            string userName = form.name;
        //            lblName.Text = userName;
        //            lblName.Invalidate();
        //            lblName.Visible = true;
        //            label1.Visible = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Операция отменена.");
        //        }
        //    }
        //    {
        //        //APIYandex? yandexApi = new APIYandex();
        //        //string authCode = await yandexApi.GetAuthCodeAsync();

        //        //if (authCode != null)
        //        //{
        //        //    MessageBox.Show("Авторизация успешна! Код: " + authCode);

        //        //    string token = await yandexApi.GetOAuthToken(authCode);
        //        //    MessageBox.Show("Токен получен: " + token);


        //        //    await yandexApi.CreateFolderAsync(token, "___for_planner___");

        //        //    string localFilePath = @"C:\___for_planner___\file.txt";
        //        //    string remoteFilePath = "___for_planner___/file.txt";

        //        //    if (System.IO.File.Exists(localFilePath))
        //        //    {
        //        //        MessageBox.Show("Локальный файл найден: " + localFilePath);
        //        //        await yandexApi.UploadFileAsync(token, localFilePath, remoteFilePath);

        //        //    }
        //        //    else
        //        //    {
        //        //        MessageBox.Show("Локальный файл не найден: " + localFilePath);
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    MessageBox.Show("Ошибка авторизации.");
        //        //}
        //    }
        //}
        private void MainForm_Load(object sender, EventArgs e)
        {
            cbxIsSync.Checked = true;
            UpdateGridsLayout();
            comboBox1.SelectedIndex = 0;
        }
        private void cbxIsSync_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxIsSync.Checked)
            {
                startSync();
            }
            else
            {
                endSync();
            }
        }

        private void startSync()
        {
            // Авторизация, скачивание с диска, получение имени пользователя

            Synk s = new Synk();
            using (var form = new Synk())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string userName = form.name;
                    lblName.Text = userName;
                    lblName.Invalidate();
                    lblName.Visible = true;
                    label1.Visible = true;
                    cbxIsSync.Checked = true;
                    btnChangeUser.Visible = true;
                }
                else
                {
                    MessageBox.Show("Операция отменена.");
                }
            }
        }
        private void endSync()
        {
            lblName.Visible = false;
            label1.Visible = false;
            btnChangeUser.Visible = false;
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateGridsLayout();
        }
        public void FillGrids(int number)
        {
            grids[number].Rows.Clear();

            List<MyTask> tasks = TaskParsing.Parse(@"C:\___for_planner___\" + $"{number}.json");

            foreach (MyTask task in tasks)
            {
                if (isDone == IS_DONE.ALL
                    || (isDone == IS_DONE.NOT_DONE && task.status == false)
                    || (isDone == IS_DONE.DONE && task.status == true))
                    grids[number].Rows.Add(task.ID, task.data, task.task, task.status);
                
            }
        }
        private void UpdateGridsLayout()
        {
            
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            int startX = 10;
            int startY = 60 + 5;
            int gridWidth = (formWidth - startX) / 2 - 10;
            int gridHeight = (formHeight - startY) / 2 - 15;

            int btnSize = 30;

            int counter = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (grids[counter] == null)
                    {
                        labels[counter] = new Label();
                        labels[counter].Width = gridWidth / 2;

                        grids[counter] = new DataGridView();
                        buttons[counter] = new Button();
                        //grids[counter]
                        this.Controls.Add(grids[counter]);
                        this.Controls.Add(labels[counter]);
                        this.Controls.Add(buttons[counter]);

                        grids[counter].ColumnCount = 3;
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.Name = "Статус";
                        grids[counter].Columns.Add(checkBoxColumn);

                        grids[counter].Columns[0].Name = "ID";
                        grids[counter].Columns[1].Name = "Дата";
                        grids[counter].Columns[2].Name = "Задача";
                        grids[counter].AllowDrop = true;

                        grids[counter].CellValueChanged += cellChange;
                        grids[counter].AllowUserToAddRows = false;
                        
                        buttons[counter].Text = "+";
                        buttons[counter].BringToFront();
                        buttons[counter].BackColor = Color.Blue;
                        buttons[counter].FlatStyle = FlatStyle.Flat;
                        buttons[counter].Click += btnAddRow;
                        buttons[counter].Size = new Size(btnSize, btnSize);

                        // контекстное меню
                        ContextMenuStrip contextMenu = new ContextMenuStrip();
                        ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Удалить задачу");

                        int currentCounter = counter;
                        deleteMenuItem.Click += (s, e) => DeleteSelectedRow(grids[currentCounter]);

                        contextMenu.Items.Add(deleteMenuItem);

                        grids[counter].ContextMenuStrip = contextMenu;

                        FillGrids(counter);
                    }

                    grids[counter].Size = new Size(gridWidth, gridHeight);
                    grids[counter].Location = new Point(
                        startX + (j % 2 * (gridWidth + 10)),
                        startY + (i % 2 * (gridHeight + 20))
                    );
                    buttons[counter].Location = new Point(grids[counter].Right - btnSize - 20,
                        grids[counter].Bottom - btnSize - 20);
                    switch (counter)
                    {
                        case 0:
                            labels[counter].Text = "Важные && Срочные";
                            break;
                        case 1:
                            labels[1].Text = "Важные ";
                            break;
                        case 2:
                            labels[2].Text = "Срочные";
                            break;
                        case 3:
                            labels[3].Text = "Другие";
                            break;
                    }
                    labels[counter].Location = new Point(
                        startX + (j % 2 * (gridWidth + 10)),
                        startY + i % 2 * ((gridHeight + 10)) + (i == 0 ? -20 : -10)
                    );

                    counter++;
                }
        }

        private void btnAddRow(object sender, EventArgs e)
        {
            int index = Array.IndexOf(buttons, (Button)sender);
            addRow(grids[index], null);
        }
        private void cellChange(object sender, DataGridViewCellEventArgs e)
        {


            // событие вызвано изменением значения в ячейке, а не заголовке
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridView dataGridView = (DataGridView)sender;
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            int taskId = Convert.ToInt32(row.Cells["ID"].Value);

            // новые строки, которые ещё не сохранены
            if (taskId == 0)
            {
                return;
            }

            string data = row.Cells["Дата"].Value?.ToString() ?? string.Empty;
            string task = row.Cells["Задача"].Value?.ToString() ?? string.Empty;
            bool status = (row.Cells["Статус"].Value as bool?) ?? false;

            int number = Array.IndexOf(grids, dataGridView);

            try
            {
                TaskParsing.UpdateTask((@"C:\___for_planner___\" + $"{number}.json"), taskId, number, status, data, task);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении задачи: {ex.Message}");
            }
        }
        private void addRow(object sender, DataGridViewRowEventArgs e)
        {
            int index = Array.IndexOf(grids, (DataGridView)sender);

            bool status = false;
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            string task = "Новая задача";

            try
            {
                int newId = TaskParsing.AddTask((@"C:\___for_planner___\" + $"{index}.json"), index, status, data, task);
                FillGrids(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении задачи: {ex.Message}");
            }
        }

        private void DeleteSelectedRow(DataGridView grid)
        {
            if (grid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    DeleteRowFromGrid(grid, row);
                }
            }
            else if (grid.CurrentRow != null)
            {
                DeleteRowFromGrid(grid, grid.CurrentRow);
            }
        }

        // Вспомогательный
        private void DeleteRowFromGrid(DataGridView grid, DataGridViewRow row)
        {
            int index = Array.IndexOf(grids, grid);

            int taskId = Convert.ToInt32(row.Cells["ID"].Value);

            try
            {
                TaskParsing.DeleteTask((@"C:\___for_planner___\" + $"{index}.json"), taskId);
                FillGrids(index);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении задачи: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            startSync();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isDone = (IS_DONE)comboBox1.SelectedIndex;

            for (int i = 0; i < 4; i++)  
                FillGrids(i);
            
        }
    }
}
