using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaToDo
{
    public partial class Synk : Form
    {
        public Synk()
        {
            InitializeComponent();
        }

        public string localFilePath = @"..\..\..\___for_planner___\";
        public string remoteFilePath = "___for_planner___/";
        public string? name = "";

        private async void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            APIYandex.authCode = await APIYandex.GetAuthCodeAsync();

            if (APIYandex.authCode != null)
            {
                MessageBox.Show("Авторизация успешна! Код: " + APIYandex.authCode);

                APIYandex.token = await APIYandex.GetOAuthToken();
                MessageBox.Show("Токен получен: " + APIYandex.token);

                for (int i = 0; i < 4; i++)
                {
                    string file = $"{i}.json";
                    await APIYandex.DownloadFileAsync(remoteFilePath + file, localFilePath + file);
                }

                // Получаем имя пользователя
                string userName = await APIYandex.GetUserNameAsync();
                if (userName != null)
                {
                    this.name = userName;
                    MessageBox.Show("Имя пользователя: " + userName);
                }
                else
                {
                    MessageBox.Show("Не удалось получить имя пользователя.");
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Ошибка авторизации.");
                this.DialogResult = DialogResult.Cancel;
            }

            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // download from local 
            {
                //APIYandex? yandexApi = new APIYandex();
                //string authCode = await yandexApi.GetAuthCodeAsync();

                //    if (authCode != null)
                //    {
                //        MessageBox.Show("Авторизация успешна! Код: " + authCode);

                //        string token = await yandexApi.GetOAuthToken(authCode);
                //        MessageBox.Show("Токен получен: " + token);


                //        await yandexApi.CreateFolderAsync(token, "___for_planner___");
                //        for (int i = 0; i < 4; i++)
                //        {
                //            string file = $"{i}.json";
                //            if (System.IO.File.Exists(localFilePath + file))
                //            {
                //                MessageBox.Show("Локальный файл найден: " + localFilePath + file);
                //                await yandexApi.UploadFileAsync(token, localFilePath + file, remoteFilePath + file);

                //            }
                //            else
                //            {
                //                MessageBox.Show("Локальный файл не найден: " + localFilePath + file);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Ошибка авторизации.");
                //    }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            //APIYandex? yandexApi = new APIYandex();
            APIYandex.authCode = await APIYandex.GetAuthCodeAsync();

            if (APIYandex.authCode != null)
            {
                MessageBox.Show("Авторизация успешна! Код: " + APIYandex.authCode);

                APIYandex.token = await APIYandex.GetOAuthToken();
                MessageBox.Show("Токен получен: " + APIYandex.token);


                await APIYandex.CreateFolderAsync("___for_planner___");
                for (int i = 0; i < 4; i++)
                {
                    string file = $"{i}.json";
                    if (System.IO.File.Exists(localFilePath + file))
                    {
                        MessageBox.Show("Локальный файл найден: " + localFilePath + file);
                        await APIYandex.UploadFileAsync(localFilePath + file, remoteFilePath + file);

                    }
                    else
                    {
                        MessageBox.Show("Локальный файл не найден: " + localFilePath + file);
                    }
                }
                // Получаем имя пользователя
                string userName = await APIYandex.GetUserNameAsync();
                if (userName != null)
                {
                    this.name = userName;
                    MessageBox.Show("Имя пользователя: " + userName);
                }
                else
                {
                    MessageBox.Show("Не удалось получить имя пользователя.");
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Ошибка авторизации.");
                this.DialogResult = DialogResult.Cancel;
            }

            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
