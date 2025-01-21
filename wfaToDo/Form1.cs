
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
        private System.Windows.Forms.Button[] buttons = new System.Windows.Forms.Button[4];
        //TaskParsing tp = new TaskParsing();
        public string? userName = "";
        //private int isDone = 0;
        private enum IS_DONE { ALL = 0, NOT_DONE, DONE };
        IS_DONE isDone = IS_DONE.ALL;
        public string myPath = @"..\..\..\___for_planner___\";

        // параметры для яндекса
        //string? authCode;
        //string? token;


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

            // контекстное меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem changeUserItem = new ToolStripMenuItem("Сменить аккаунт");
            ToolStripMenuItem leftItem = new ToolStripMenuItem("Выйти из аккаунта");

            changeUserItem.Click += (s, e) => 
            {
                startSync();
            };
            leftItem.Click += (s, e) => 
            {
                quitSync();
            };

            contextMenu.Items.Add(changeUserItem);
            contextMenu.Items.Add(leftItem);

            btnChangeUser.ContextMenuStrip = contextMenu;
            
            btnChangeUser.Click += (s, e) => {
                contextMenu.Show(btnChangeUser, new System.Drawing.Point(0, btnChangeUser.Height));
            };
        }

        private void quitSync()
        {
            lblName.Visible = false;
            label1.Visible = false;
            cbxIsSync.Checked = false;
            btnChangeUser.Visible = false;

            // TODO: удаление токена
            // APIYandex. logout
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
                    cbxIsSync.Checked = false;
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

            //List<MyTask> tasks = TaskParsing.Parse(@"C:\___for_planner___\" + $"{number}.json");
            List<MyTask> tasks = TaskParsing.Parse(myPath + $"{number}.json");

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
                        buttons[counter] = new System.Windows.Forms.Button();
                        //grids[counter]
                        this.Controls.Add(grids[counter]);
                        this.Controls.Add(labels[counter]);
                        this.Controls.Add(buttons[counter]);

                        grids[counter].ColumnCount = 3;
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.Name = "Статус";
                        checkBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                        grids[counter].Columns.Add(checkBoxColumn);

                        grids[counter].Columns[0].Name = "ID";
                        
                        grids[counter].Columns[1].Name = "Дата";
                        grids[counter].Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                        grids[counter].Columns[2].Name = "Задача";
                        grids[counter].AllowDrop = true;

                        grids[counter].AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        grids[counter].AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                        grids[counter].AllowUserToOrderColumns = true;
                        grids[counter].AllowUserToResizeColumns = true;

                        grids[counter].CellValueChanged += cellChange;
                        grids[counter].AllowUserToAddRows = false;
                        grids[counter].RowHeadersVisible = false;
                        grids[counter].Columns[0].Visible = false;
                        //grids[counter].AllowDrop = true;

                        
                        // Подключаем события для перетаскивания
                        grids[counter].MouseDown += dataGridView_MouseDown;
                        grids[counter].DragEnter += dataGridView_DragEnter;
                        grids[counter].DragDrop += dataGridView_DragDrop;
                        grids[counter].AllowDrop = true; // Разрешаем перетаскивание
                        
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
        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Clicks > 1)
            //{
            //    Console.WriteLine("Двойной клик. Перетаскивание отменено.");
            //    return;
            //}

            if (e.Button == MouseButtons.Right)
            {
                return;
            }
            var grid = sender as DataGridView;

            var hitTest = grid.HitTest(e.X, e.Y);
            if (hitTest.Type == DataGridViewHitTestType.Cell)
            {
                var row = grid.Rows[hitTest.RowIndex];

                grid.DoDragDrop(row, DragDropEffects.Move);
            }
        }

        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            // перетаскиваемый объект — это строка DataGridView
            if (e.Data.GetDataPresent(typeof(DataGridViewRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            var targetGrid = sender as DataGridView;

            var row = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;

            if (row == null)
            {
                Console.WriteLine("Перетаскиваемая строка не найдена.");
                return;
            }

            var sourceGrid = row.DataGridView;

            if (sourceGrid == null)
            {
                Console.WriteLine("Исходный DataGridView не найден.");
                return;
            }

            int sourceIndex = GetGridIndex(sourceGrid);
            int targetIndex = GetGridIndex(targetGrid);

            if (sourceIndex == -1 || targetIndex == -1)
            {
                Console.WriteLine("Исходный или целевой DataGridView не найден в массиве grids.");
                return;
            }

            var newRow = (DataGridViewRow)targetGrid.RowTemplate.Clone();
            newRow.CreateCells(targetGrid);
            for (int i = 0; i < row.Cells.Count; i++)
            {
                newRow.Cells[i].Value = row.Cells[i].Value;
            }

            targetGrid.Rows.Add(newRow);

            TaskParsing.AddTask(
                myPath + $"{targetIndex}.json",
                targetIndex,
                newRow.Cells["Статус"].Value as bool? ?? false,
                newRow.Cells["Дата"].Value?.ToString() ?? " ",
                newRow.Cells["Задача"].Value?.ToString() ?? " "
            );
            FillGrids(targetIndex);

            TaskParsing.DeleteTask(
                myPath + $"{sourceIndex}.json",
                (int)row.Cells[0].Value
            );
            FillGrids(sourceIndex);
        }
        private int GetGridIndex(DataGridView grid)
        {
            for (int i = 0; i < grids.Length; i++)
            {
                if (grids[i] == grid)
                {
                    return i;
                }
            }
            return -1; // Если grid не найден
        }
        private void btnAddRow(object sender, EventArgs e)
        {
            int index = Array.IndexOf(buttons, (System.Windows.Forms.Button)sender);
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
                TaskParsing.UpdateTask((myPath + $"{number}.json"), taskId, number, status, data, task);
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
                int newId = TaskParsing.AddTask((myPath + $"{index}.json"), index, status, data, task);
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
                TaskParsing.DeleteTask((myPath + $"{index}.json"), taskId);
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
            //startSync();

            
        }

        private void BtnChangeUser_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isDone = (IS_DONE)comboBox1.SelectedIndex;

            for (int i = 0; i < 4; i++)  
                FillGrids(i);
            
        }
    }
}
