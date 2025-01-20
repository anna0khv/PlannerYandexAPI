
using System.Diagnostics.Metrics;

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
        TaskParsing tp = new TaskParsing();


        private async void button1_Click(object sender, EventArgs e)
        {
            Synk s = new Synk();
            s.ShowDialog();
            {
                //APIYandex? yandexApi = new APIYandex();
                //string authCode = await yandexApi.GetAuthCodeAsync();

                //if (authCode != null)
                //{
                //    MessageBox.Show("Авторизация успешна! Код: " + authCode);

                //    string token = await yandexApi.GetOAuthToken(authCode);
                //    MessageBox.Show("Токен получен: " + token);


                //    await yandexApi.CreateFolderAsync(token, "___for_planner___");

                //    string localFilePath = @"C:\___for_planner___\file.txt";
                //    string remoteFilePath = "___for_planner___/file.txt";

                //    if (System.IO.File.Exists(localFilePath))
                //    {
                //        MessageBox.Show("Локальный файл найден: " + localFilePath);
                //        await yandexApi.UploadFileAsync(token, localFilePath, remoteFilePath);

                //    }
                //    else
                //    {
                //        MessageBox.Show("Локальный файл не найден: " + localFilePath);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Ошибка авторизации.");
                //}
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

            UpdateGridsLayout();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateGridsLayout();
        }
        public void FillGrids(int number)
        {
            grids[number].Rows.Clear();

            List<MyTask> tasks = tp.Parse(@"C:\___for_planner___\" + $"{number}.json");
            foreach (MyTask task in tasks)
            {
                grids[number].Rows.Add(task.data, task.task, task.status);
            }
        }
        private void UpdateGridsLayout()
        {
            //string filePath = @"C:\___for_planner___\lol.json";
            //tp.AddTask(filePath, 0, true, "01.01.2000", "Ничего не делать");


            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            int startX = 10;
            int startY = 60 + 5;
            int gridWidth = (formWidth - startX) / 2 - 10;
            int gridHeight = (formHeight - startY) / 2 - 15;

            int counter = 0;
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (grids[counter] == null)
                    {
                        labels[counter] = new Label();
                        labels[counter].Width = 150;

                        grids[counter] = new DataGridView();
                        //grids[counter]
                        this.Controls.Add(grids[counter]);
                        this.Controls.Add(labels[counter]);

                        grids[counter].ColumnCount = 2;
                        DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                        checkBoxColumn.Name = "Статус";
                        grids[counter].Columns.Add(checkBoxColumn);
                        grids[counter].Columns[0].Name = "Дата";
                        grids[counter].Columns[1].Name = "Задача";
                        FillGrids(counter);
                    }

                    grids[counter].Size = new Size(gridWidth, gridHeight);
                    grids[counter].Location = new Point(
                        startX + (j % 2 * (gridWidth + 10)),
                        startY + (i % 2 * (gridHeight + 20))
                    );
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
