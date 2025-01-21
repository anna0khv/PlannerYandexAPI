using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static wfaToDo.Form1;
namespace wfaToDo
{
    internal class APIYandex
    {
        public bool isAuth = false;
        private static string clientId = "199d556fdb27498b983479350480f64d";
        private static string redirectUri = "http://localhost:12345/auth";
        private string authUrl = $"https://oauth.yandex.ru/authorize?response_type=code&client_id={clientId}&redirect_uri={redirectUri}";
        private string clientSecret = "07800a989ab4485d9445ccfd0d2f0850";
        public APIYandex() { }

        public class TokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
        }

        public bool Auth()
        {
            if (!isAuth)
            {
                try
                {
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = authUrl,
                        UseShellExecute = true
                    };
                    Process.Start(processStartInfo);

                    isAuth = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при авторизации: {ex.Message}");
                    isAuth = false;
                }
            }

            return isAuth;
        }

        public async Task<string> GetAuthCodeAsync()
        {
            if (!isAuth)
            {
                try
                {
                    var processStartInfo = new ProcessStartInfo
                    {
                        FileName = authUrl,
                        UseShellExecute = true
                    };
                    Process.Start(processStartInfo);

                    string authCode = await WaitForAuthCodeAsync();
                    isAuth = true;
                    return authCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при авторизации: {ex.Message}");
                    isAuth = false;
                    return null;
                }
            }
            return null;
        }

        private async Task<string> WaitForAuthCodeAsync()
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add(redirectUri + "/");
                listener.Start();

                Console.WriteLine("Ожидание авторизации...");

                var context = await listener.GetContextAsync();
                var request = context.Request;
                var authCode = request.QueryString["code"];

                var response = context.Response;
                string responseString = "<html><body>Авторизация успешна! Закройте это окно.</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                response.OutputStream.Close();

                listener.Stop();

                return authCode;
            }
        }
        public async Task<string> GetOAuthToken(string code)
        {

            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri)
                });

                HttpResponseMessage response = await client.PostAsync("https://oauth.yandex.ru/token", content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Десериализация json
                    TokenResponse tokenData = JsonConvert.DeserializeObject<TokenResponse>(responseString);

                    return tokenData.access_token;
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при получении токена: " + error);
                    return null;
                }
            }
        }
        public async Task CreateFolderAsync(string token, string folderPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                var response = await client.PutAsync($"https://cloud-api.yandex.net/v1/disk/resources?path={Uri.EscapeDataString(folderPath)}", null);
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка при создании папки: " + error);
                }
            }
        }

        public async Task<bool> UploadFileAsync(string token, string localFilePath, string remoteFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                // Удаляем существующий файл
                await DeleteFileAsync(token, remoteFilePath);

                // Получаем ссылку для загрузки
                var response = await client.GetAsync($"https://cloud-api.yandex.net/v1/disk/resources/upload?path={Uri.EscapeDataString(remoteFilePath)}");
                if (response.IsSuccessStatusCode)
                {
                    var uploadUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<UploadResponse>(await response.Content.ReadAsStringAsync()).href;

                    // Загружаем файл
                    using (var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(localFilePath)))
                    {
                        var uploadResponse = await client.PutAsync(uploadUrl, fileContent);
                        if (uploadResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Файл загружен: " + remoteFilePath);
                            return true; // Успешно
                        }
                        else
                        {
                            string error = await uploadResponse.Content.ReadAsStringAsync();
                            Console.WriteLine("Ошибка при загрузке файла: " + error);
                            return false; // Ошибка
                        }
                    }
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при получении ссылки для загрузки: " + error);
                    return false; // Ошибка
                }
            }
        }

        public class UploadResponse
        {
            public string href { get; set; }
            public string method { get; set; }
            public bool templated { get; set; }
        }

        public async Task DownloadFileAsync(string token, string remoteFilePath, string localFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                // Ссылка для скачиванмия
                var response = await client.GetAsync($"https://cloud-api.yandex.net/v1/disk/resources/download?path={Uri.EscapeDataString(remoteFilePath)}");
                if (response.IsSuccessStatusCode)
                {
                    var downloadUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<DownloadResponse>(await response.Content.ReadAsStringAsync()).href;

                    var fileResponse = await client.GetAsync(downloadUrl);
                    if (fileResponse.IsSuccessStatusCode)
                    {
                        using (var fileStream = System.IO.File.Create(localFilePath))
                        {
                            await fileResponse.Content.CopyToAsync(fileStream);
                        }
                        Console.WriteLine("Файл скачан: " + localFilePath);
                    }
                    else
                    {
                        string error = await fileResponse.Content.ReadAsStringAsync();
                        Console.WriteLine("Ошибка при скачивании файла: " + error);
                    }
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при получении ссылки для скачивания: " + error);
                }
            }
        }

        public class DownloadResponse
        {
            public string href { get; set; }
            public string method { get; set; }
            public bool templated { get; set; }
        }

        public async Task SyncFolderWithYandexDisk(string token, string localFolderPath, string remoteFolderPath)
        {
            // not used
            MessageBox.Show("Перед папкой");
            await CreateFolderAsync(token, remoteFolderPath);
            MessageBox.Show("После папкой");
            foreach (var file in System.IO.Directory.GetFiles(localFolderPath))
            {
                string remoteFilePath = $"{remoteFolderPath}/{System.IO.Path.GetFileName(file)}";
                await UploadFileAsync(token, file, remoteFilePath);
            }
            var remoteFiles = await ListFilesAsync(token, remoteFolderPath);
            foreach (var remoteFile in remoteFiles)
            {
                string localFilePath = System.IO.Path.Combine(localFolderPath, System.IO.Path.GetFileName(remoteFile));
                await DownloadFileAsync(token, remoteFile, localFilePath);
            }
        }

        public async Task<List<string>> ListFilesAsync(string token, string remoteFolderPath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                var response = await client.GetAsync($"https://cloud-api.yandex.net/v1/disk/resources?path={Uri.EscapeDataString(remoteFolderPath)}&limit=1000");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var folderInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<FolderInfo>(responseData);
                    return folderInfo._embedded.items.Where(item => item.type == "file").Select(item => item.path).ToList();
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Ошибка при получении списка файлов: " + error);
                    return new List<string>();
                }
            }
        }

        public async Task<bool> DeleteFileAsync(string token, string remoteFilePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                var response = await client.DeleteAsync($"https://cloud-api.yandex.net/v1/disk/resources?path={Uri.EscapeDataString(remoteFilePath)}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Файл удалён: " + remoteFilePath);
                    return true; // Успешно
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при удалении файла: " + error);
                    return false; // Ошибка
                }
            }
        }
        public async Task<string> GetUserNameAsync(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                var response = await client.GetAsync("https://cloud-api.yandex.net/v1/disk");
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var diskInfo = JsonConvert.DeserializeObject<DiskInfo>(responseData);
                    return diskInfo.user.login;
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при получении информации о пользователе: " + error);
                    return null;
                }
            }
        }

        public async Task LogoutAsync(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("OAuth", token);

                var response = await client.DeleteAsync("https://oauth.yandex.ru/revoke_token");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Выход из аккаунта выполнен успешно.");
                    isAuth = false; // Обновляем статус авторизации
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Ошибка при выходе из аккаунта: " + error);
                }
            }
        }

        public class DiskInfo
        {
            public UserInfo user { get; set; }
        }

        public class UserInfo
        {
            public string login { get; set; }
        }

        public class FolderInfo
        {
            public Embedded _embedded { get; set; }
        }

        public class Embedded
        {
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public string path { get; set; }
            public string type { get; set; }
        }

    }
}