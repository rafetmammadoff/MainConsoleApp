using Main_Console_App;
using Main_Console_App.Enums;
using Main_Console_App.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplicatons
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture=new System.Globalization.CultureInfo("az-Az");
            Manager manager = new Manager();
            string option;
            do
            {
                Selection();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddTodoItem(manager);
                        break;
                    case "2":
                        ShowAllTodoItems(manager);
                        break;
                    case "3":
                        ShowDelayedTodoItems(manager);
                        break;
                    case "4":
                        Console.WriteLine();
                        break;
                }
            } while (option != "0");

        }
        static void Selection()
        {
            Console.WriteLine(" - 1.Tapşırıq yarat");
            Console.WriteLine(" - 2.Bütün tapşırıqlara bax");
            Console.WriteLine(" - 3.Vaxtı keçmiş tapşırıqlara bax");
            Console.WriteLine(" - 4.Seçilmiş statuslu tapşırıqlara bax");
            Console.WriteLine(" - 5.Tarix intervalına görə axtar");
            Console.WriteLine(" - 6.Tapşırığın statusunu dəyişmək");
            Console.WriteLine(" - 7.Tapşırığı editləmək");
            Console.WriteLine(" - 8.Tapşırığı silməl");
            Console.WriteLine(" - 9.Tapşırıqlarda axtarış");
            Console.WriteLine(" - 0.Çıxış");
        }
        static void AddTodoItem(Manager manager)
        {
            Console.WriteLine("Tapsiriq basligini daxil edin");
            string tittle = Console.ReadLine();
            string description;
            do
            {
                Console.WriteLine("Tapsiriq aciqlama daxil edin");
                description = Console.ReadLine();
            } while (!TodoItem.CheckDescription(description));

            DateTime deadline;
            string deadlineStr;
            bool check;
            do
            {
                Console.WriteLine("Tapsiriq dedline vaxtini teyin edin");
                deadlineStr = Console.ReadLine();
                try
                {
                    check = TodoItem.CheckDeadline(deadlineStr);
                }
                catch (MistakeDeadlineException exp)
                {
                    check = false;
                    Console.WriteLine(exp.Message);
                }
                catch (MistakeDateTimeException exp)
                {
                    check = false;
                    Console.WriteLine(exp.Message);
                }
            } while (!check);
            deadline = DateTime.Parse(deadlineStr);
            TodoItem todoItem = new TodoItem
            {
                Tittle = tittle,
                Description = description,
                DeadLine = deadline
            };
            manager.AddTodoItem(todoItem);
        }
        static void ShowAllTodoItems(Manager manager)
        {
            List<TodoItem> allTodoItems = manager.GetAllTodoItems();
            foreach (var item in allTodoItems)
            {
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine}");
            }
        }
        static void ShowDelayedTodoItems(Manager manager)
        {
            List<TodoItem> delayedTodo = manager.GetAllDelayedTasks();
            foreach (TodoItem item in delayedTodo)
            {
                Console.WriteLine(item.DeadLine);
            }
        }
    }
}
