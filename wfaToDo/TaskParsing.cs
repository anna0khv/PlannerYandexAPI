using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wfaToDo
{
    internal class MyTask
    {
        public int ID { get; set; }
        public int rang { get; set; }
        public bool status { get; set; }
        public string? data { get; set; }
        public string? task { get; set; }

    }
    internal static class TaskParsing
    {
        

        public static List<MyTask> Parse(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден", filePath);
            }

            string json = File.ReadAllText(filePath);

            List<MyTask> tasks = JsonConvert.DeserializeObject<List<MyTask>>(json);

            return tasks;
        }
        public static int AddTask(string filePath, int rang, bool status, string data, string task)
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            string json = File.ReadAllText(filePath);

            List<MyTask> tasks = JsonConvert.DeserializeObject<List<MyTask>>(json) ?? new List<MyTask>();

            int newId = tasks.Count > 0 ? tasks.Max(t => t.ID) + 1 : 1;

            MyTask newTask = new MyTask
            {
                ID = newId,
                rang = rang,
                status = status,
                data = data,
                task = task
            };

            tasks.Add(newTask);

            string updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);

            return newId;
        }

        public static void DeleteTask(string filePath, int taskId)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден", filePath);
            }

            string json = File.ReadAllText(filePath);

            List<MyTask> tasks = JsonConvert.DeserializeObject<List<MyTask>>(json) ?? new List<MyTask>();

            MyTask? taskToRemove = tasks.FirstOrDefault(t => t.ID == taskId);

            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                string updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
            }
            else
            {
                throw new ArgumentException($"Задача с ID {taskId} не найдена.");
            }
        }

        public static void UpdateTask(string filePath, int taskId, int number, bool status, string data, string task)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Файл не найден", filePath);
            }

            string json = File.ReadAllText(filePath);
            List<MyTask> tasks = JsonConvert.DeserializeObject<List<MyTask>>(json) ?? new List<MyTask>();
            MyTask? taskToUpdate = tasks.FirstOrDefault(t => t.ID == taskId);

            if (taskToUpdate != null)
            {
                taskToUpdate.rang = number;
                taskToUpdate.status = status;
                taskToUpdate.data = data;
                taskToUpdate.task = task;

                string updatedJson = JsonConvert.SerializeObject(tasks, Formatting.Indented);

                File.WriteAllText(filePath, updatedJson);
            }
            else
            {
                throw new ArgumentException($"Задача с ID {taskId} не найдена.");
            }
        }
    }
}
