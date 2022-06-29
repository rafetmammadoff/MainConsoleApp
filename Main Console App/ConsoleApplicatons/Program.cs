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
                        ShowTodoItemsByStatus(manager);
                        break;
                    case "5":
                        FilterTodoItems(manager);
                        break;
                    case "6":
                        string noStr;
                        int no;

                        do
                        {
                            Console.WriteLine("Deyismek istediyiniz tapsirigin nomresini daxil edin.");
                            noStr = Console.ReadLine();
                        } while (!int.TryParse(noStr,out no) || !manager.HasNo(no));
                        string secimStr;
                        byte secim;
                        do
                        {
                            Console.WriteLine("Deyiseceyiniz statusu secin");
                            foreach (var item in Enum.GetValues(typeof(TodoStatus)))
                            {
                                Console.WriteLine($"{(byte)item} - {item}");
                            }
                            secimStr = Console.ReadLine();
                        } while (!byte.TryParse(secimStr, out secim) || !Enum.IsDefined(typeof(TodoStatus), secim));
                        manager.ChangeTodoItemStatus(no, (TodoStatus)secim);
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
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
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
        static void ShowTodoItemsByStatus(Manager manager)
        {
            string secimStr;
            byte secim;

            do
            {
                Console.WriteLine("Axtaris etmek istediyiniz statusu secin");
                foreach (var item in Enum.GetValues(typeof(TodoStatus)))
                {
                    Console.WriteLine($"{(byte)item} - {item}");
                }
                secimStr = Console.ReadLine();
            } while (!byte.TryParse(secimStr, out secim) || !Enum.IsDefined(typeof(TodoStatus), secim));

            List<TodoItem> list = manager.GetAllTodoItemsByStatus((TodoStatus)secim);
            foreach (var item in list)
            {
                Console.WriteLine(item.Tittle);
            }
        }
        static void FilterTodoItems(Manager manager)
        {
            DateTime fromDate;
            string fromDateStr;
            do
            {
                Console.WriteLine("FromDate daxil edin");
                fromDateStr = Console.ReadLine();
            } while (!DateTime.TryParse(fromDateStr, out fromDate));

            DateTime toDate;
            string toDateStr;
            do
            {
                Console.WriteLine("ToDate daxil edin");
                toDateStr = Console.ReadLine();
            } while (!DateTime.TryParse(toDateStr, out toDate) || fromDate > toDate);
            Console.WriteLine("Bir status secseniz hemin status uzre,secmeseniz hamisi uzre filtirlenecek;");
            string secimStr;
            byte secim = 0;
            do
            {
                foreach (var item in Enum.GetValues(typeof(TodoStatus)))
                {
                    Console.WriteLine($"{(byte)item} - {item}");
                }
                secimStr = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(secimStr))
                {
                    secimStr = null;
                    break;
                }
            } while (!byte.TryParse(secimStr, out secim) || !Enum.IsDefined(typeof(TodoStatus), secim));
            if (secimStr == null)
            {
                List<TodoItem> todos = manager.FilterTodoItems(fromDate, toDate, null);
                foreach (var item in todos)
                {
                    Console.WriteLine(item.Tittle + " " + item.DeadLine);
                }
            }
            else
            {
                List<TodoItem> todos = manager.FilterTodoItems(fromDate, toDate, (TodoStatus)secim);
                foreach (var item in todos)
                {
                    Console.WriteLine(item.Tittle + " " + item.DeadLine);
                }
            }
        }
    }
}
