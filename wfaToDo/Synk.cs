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

        public string localFilePath = @"C:\___for_planner___\";
        public string remoteFilePath = "___for_planner___/";

        private async void button1_Click(object sender, EventArgs e)
        {
            // download from disk

            APIYandex? yandexApi = new APIYandex();
            string authCode = await yandexApi.GetAuthCodeAsync();

            if (authCode != null)
            {
                MessageBox.Show("Авторизация успешна! Код: " + authCode);

                string token = await yandexApi.GetOAuthToken(authCode);
                MessageBox.Show("Токен получен: " + token);

                for (int i = 0; i < 4; i++)
                {
                    string file = $"{i}.json";
                    await yandexApi.DownloadFileAsync(token, remoteFilePath + file, localFilePath + file);
                }
                

            }
            else
            {
                MessageBox.Show("Ошибка авторизации.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // download from local 

            APIYandex? yandexApi = new APIYandex();
            string authCode = await yandexApi.GetAuthCodeAsync();

            if (authCode != null)
            {
                MessageBox.Show("Авторизация успешна! Код: " + authCode);

                string token = await yandexApi.GetOAuthToken(authCode);
                MessageBox.Show("Токен получен: " + token);


                await yandexApi.CreateFolderAsync(token, "___for_planner___");
                for (int i = 0; i < 4; i++)
                {
                    string file = $"{i}.json";
                    if (System.IO.File.Exists(localFilePath + file))
                    {
                        MessageBox.Show("Локальный файл найден: " + localFilePath + file);
                        await yandexApi.UploadFileAsync(token, localFilePath + file, remoteFilePath + file);

                    }
                    else
                    {
                        MessageBox.Show("Локальный файл не найден: " + localFilePath + file);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка авторизации.");
            }
        }
    }
}
