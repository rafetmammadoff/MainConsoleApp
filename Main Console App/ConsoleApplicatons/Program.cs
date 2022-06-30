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
            Console.OutputEncoding= System.Text.Encoding.UTF8;
            Manager manager = new Manager();
            string option;
            do
            {
                Selection();
                Console.ForegroundColor = ConsoleColor.Green;
                option = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                
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
                        ChangeTodoStatus(manager);
                        break;
                    case "7":
                       EditTodoItems(manager);
                        break;
                    case "8":
                        DeleteTodoItem(manager);
                        break;
                    case "9":
                        SearchTodoItem(manager);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Yalnis secim etdiniz");
                        Console.ForegroundColor = ConsoleColor.White;
                        
                        break;
                }
            } while (option != "0");

        }
        static void Selection()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void AddTodoItem(Manager manager)
        {
            Console.WriteLine("Tapsiriq basligini daxil edin");
            Console.ForegroundColor = ConsoleColor.Green;
            string tittle = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            string description;
            do
            {
                Console.WriteLine("Tapsiriga aciqlama daxil edin (Minimum 2 soz olmalidir)");
                Console.ForegroundColor = ConsoleColor.Green;
                description = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            } while (!TodoItem.CheckDescription(description));

            DateTime deadline;
            string deadlineStr;
            bool check;
            do
            {
                Console.WriteLine("Tapsiriq dedline vaxtini teyin edin");
                Console.ForegroundColor = ConsoleColor.Green;
                deadlineStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    check = TodoItem.CheckDeadline(deadlineStr);
                }
                catch (MistakeDeadlineException exp)
                {
                    check = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (MistakeDateTimeException exp)
                {
                    check = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Added successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void ShowAllTodoItems(Manager manager)
        {
            List<TodoItem> allTodoItems=new List<TodoItem>();
            try
            {
                 allTodoItems = manager.GetAllTodoItems();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return; 
            }
            foreach (var item in allTodoItems)
            {
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
            }
        }
        static void ShowDelayedTodoItems(Manager manager)
        {
            List<TodoItem> delayedTodo = new List<TodoItem>();
            try
            {
                delayedTodo = manager.GetAllDelayedTasks();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch(EmptyCustomListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (TodoItem item in delayedTodo)
            {
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
            }
        }
        static void ShowTodoItemsByStatus(Manager manager)
        {
            string secimStr;
            byte secim;
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            bool check;
            do
            {
                Console.WriteLine("Axtaris etmek istediyiniz statusu secin");
                foreach (var item in Enum.GetValues(typeof(TodoStatus)))
                {
                    Console.WriteLine($"{(byte)item} - {item}");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                secimStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    check = manager.CheckHasStatus(secimStr);
                }
                catch (NoConvertException exp)
                {
                    check = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (!check);
            secim = byte.Parse(secimStr);

            List<TodoItem> list = new List<TodoItem>();
            try
            {
                list = manager.GetAllTodoItemsByStatus((TodoStatus)secim);
            }
            catch(LimitException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (EmptyCustomListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (var item in list)
            {
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
            }
        }
        static void FilterTodoItems(Manager manager)
        {
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            DateTime fromDate;
            string fromDateStr;
            do
            {
                Console.WriteLine("FromDate daxil edin");
                Console.ForegroundColor = ConsoleColor.Green;
                fromDateStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
            } while (!DateTime.TryParse(fromDateStr, out fromDate));

            DateTime toDate;
            string toDateStr;
            do
            {
                Console.WriteLine("ToDate daxil edin");
                Console.ForegroundColor = ConsoleColor.Green;
                toDateStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
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
                Console.ForegroundColor = ConsoleColor.Green;
                secimStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (String.IsNullOrWhiteSpace(secimStr))
                {
                    secimStr = null;
                    break;
                }
            } while (!byte.TryParse(secimStr, out secim) || !Enum.IsDefined(typeof(TodoStatus), secim));
            if (secimStr == null)
            {
                List<TodoItem> todos = new List<TodoItem>();
                try
                {
                    todos = manager.FilterTodoItems(fromDate, toDate, null);
                }
                catch (EmptyCustomListException exp)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                foreach (var item in todos)
                {
                    Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
                }
            }
            else
            {
                List<TodoItem> todos = new List<TodoItem>();
                try
                {
                    todos = manager.FilterTodoItems(fromDate, toDate, (TodoStatus)secim);
                }
                catch (EmptyCustomListException exp)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                foreach (var item in todos)
                {
                    Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
                }
            }
        }
        static void ChangeTodoStatus(Manager manager)
        {
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            string noStr;
            int no;
            bool check = false;
            do
            {
                Console.WriteLine("Deyismek istediyiniz tapsirigin nomresini daxil edin.");
                Console.ForegroundColor = ConsoleColor.Green;
                noStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                check = int.TryParse(noStr, out no);
                if (check)
                {
                    try
                    {
                        manager.HasNo(no);
                    }
                    catch (TodoItemNotFoundException exp)
                    {
                        check = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exp.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            } while (!check);
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Change successfully");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void EditTodoItems(Manager manager)
        {
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            string noStr;
            int no;
            bool check = false;
            do
            {
                Console.WriteLine("Editlemek istediyiniz tapsirigin nomresini daxil edin.");
                Console.ForegroundColor = ConsoleColor.Green;
                noStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                check = int.TryParse(noStr, out no);
                if (check)
                {
                    try
                    {
                        manager.HasNo(no);
                    }
                    catch (TodoItemNotFoundException exp)
                    {
                        check = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exp.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            } while (!check);
            DateTime? deadline = null;
            string deadlineStr;
            bool checkDeadline;
            do
            {
                Console.WriteLine("Yeni dedline vaxtini teyin edin (Bos buraxa bilersiniz)");
                Console.ForegroundColor = ConsoleColor.Green;
                deadlineStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (String.IsNullOrWhiteSpace(deadlineStr))
                {
                    deadline = null;
                    break;
                }
                try
                {
                    checkDeadline = TodoItem.CheckDeadline(deadlineStr);
                }
                catch (MistakeDeadlineException exp)
                {
                    checkDeadline = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exp.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (MistakeDateTimeException exp)
                {
                    checkDeadline = false;
                    Console.WriteLine(exp.Message);
                }
            } while (!checkDeadline);
            if (!String.IsNullOrWhiteSpace(deadlineStr))
            {
                deadline = DateTime.Parse(deadlineStr);
            }

            Console.WriteLine("Yeni basligi daxil edin (Bos buraxa bilersiniz)");
            Console.ForegroundColor = ConsoleColor.Green;
            string tittle = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            if (String.IsNullOrWhiteSpace(tittle))
            {
                tittle = null;
            }
            string description;
            do
            {
                Console.WriteLine("Yeni aciqlamani daxil edin (Bos buraxa bilersiniz)");
                Console.ForegroundColor = ConsoleColor.Green;
                description = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (String.IsNullOrWhiteSpace(description))
                {
                    description = null;
                    break;
                }
            } while (!TodoItem.CheckDescription(description));
            manager.EditTodoItem(no, tittle, description, deadline);
        }
        static void DeleteTodoItem(Manager manager)
        {
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            string noStr;
            int no;
            bool check = false;
            do
            {
                Console.WriteLine("Silmek istediyiniz tapsirigin nomresini daxil edin.");
                Console.ForegroundColor = ConsoleColor.Green;
                noStr = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                check = int.TryParse(noStr, out no);
                if (check)
                {
                    try
                    {
                        manager.HasNo(no);
                    }
                    catch (TodoItemNotFoundException exp)
                    {
                        check = false;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exp.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            } while (!check);
            manager.DeleteTodoItem(no);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Delete successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void SearchTodoItem(Manager manager)
        {
            try
            {
                manager.CheckEmpty();
            }
            catch (EmptyListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            Console.WriteLine("Axtaris deyerini daxil edin");
            Console.ForegroundColor = ConsoleColor.Green;
            string text = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            List<TodoItem> list = new List<TodoItem>();
            try
            {
                list = manager.SearchTodoItems(text);
            }
            catch (EmptyCustomListException exp)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exp.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach (var item in list)
            {
                Console.WriteLine($"Tittle: {item.Tittle} - Description: {item.Description} - DeadLine: {item.DeadLine} - Status: {item.Status}");
            }
        }
    }
}
