using Main_Console_App.Enums;
using Main_Console_App.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App
{
    public class Manager : ITodoManager
    {
        private List<TodoItem> _todoItems=new List<TodoItem>();
        public List<TodoItem> TodoItems
        {
            get { return _todoItems; }
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            _todoItems.Add(todoItem);
        }

        public void ChangeTodoItemStatus(int no, TodoStatus status)
        {
            TodoItem todoItem = _todoItems.Find(x => x.No == no);
            todoItem.Status = status;
            todoItem.StatusChangedAt= DateTime.Now;

        }
        public bool HasNo(int no)
        {
            TodoItem todoItem = _todoItems.Find(x => x.No == no);
            if (todoItem==null)
            {
                throw new TodoItemNotFoundException($"{no} nomreli TodoItem tapilmadi");
            }
            else
            {
                return true;
            }

        }

        public void DeleteTodoItem(int no)
        {
            TodoItem todoItem = _todoItems.Find(x => x.No == no);
            _todoItems.Remove(todoItem);
        }

        public void EditTodoItem(int no, string tittle, string description, DateTime? deadline)
        {
            TodoItem todo= _todoItems.Find(x => x.No == no);
            if (tittle != null)
            {
                todo.Tittle = tittle;
            }
            if (description != null)
            {
                todo.Description = description;
            }
            if (deadline != null)
            {
                todo.DeadLine =(DateTime)deadline;
            }
        }

        public List<TodoItem> FilterTodoItems(DateTime fromDate, DateTime toDate, TodoStatus? status)
        {
            if (fromDate>toDate)
            {
                throw new MistakeDateTimeException("toDate fromDate-den evvel ola bilmez !!!");
            }
            
            if (status != null)
            {
                List<TodoItem> todoItems = _todoItems.FindAll(x => x.Status == status && x.DeadLine > fromDate && x.DeadLine < toDate);
                if (todoItems.Count == 0)
                {
                    throw new EmptyCustomListException($"{fromDate} - {toDate} araliginda {status} statuslu tapsiriq yoxdur");
                }
                return todoItems;
            }
            else
            {
                List<TodoItem> todoItems = _todoItems.FindAll(x =>x.DeadLine > fromDate && x.DeadLine < toDate);
                if (todoItems.Count == 0)
                {
                    throw new EmptyCustomListException($"{fromDate} - {toDate} araliginda tapsiriq yoxdur");
                }
                return todoItems;
            }
        }

        public List<TodoItem> GetAllDelayedTasks()
        {
            List<TodoItem> todoItems = _todoItems.FindAll(x => x.DeadLine < DateTime.Now && x.Status != TodoStatus.Done);
            if (_todoItems.Count==0)
            {
                throw new EmptyListException("--------Hal hazirda tapsiriq yoxdur--------");
            }
            if (todoItems.Count==0)
            {
                throw new EmptyCustomListException("--------Vaxti kecmis tapsiriq yoxdur--------");
            }
            return todoItems;
        }

        public List<TodoItem> GetAllTodoItems()
        {
            if (_todoItems.Count==0)
            {
                throw new EmptyListException("---------Hal hazirda tapsiriq yoxdur.--------");
            }
            return _todoItems;
        }

        public void CheckEmpty()
        {
            if (_todoItems.Count==0)
            {
                throw new EmptyListException("---------Hal hazirda tapsiriq yoxdur---------");
            }
        }
        public List<TodoItem> GetAllTodoItemsByStatus(TodoStatus? status)
        {
            
            
            List<TodoItem> NewTodoItems = _todoItems.FindAll(x => x.Status == status);
            if (NewTodoItems.Count == 0)
            {
                
                throw new EmptyCustomListException($"--------{status} statuslu tapsiriq yoxdur--------");
            }
            return NewTodoItems;
        }
        public bool CheckHasStatus(string statusStr)
        {
            byte status;
            if (byte.TryParse(statusStr, out status) && Enum.IsDefined(typeof(TodoStatus), status))
            {
                return true;
            }
            else
            {
                throw new NoConvertException("------Yanlis daxil etdiniz asagidakilardan birini secin");
            }
        }

        public List<TodoItem> SearchTodoItems(string text)
        {
            List<TodoItem> NewTodoItems = _todoItems.FindAll(x => x.Tittle.Contains(text));
            if (NewTodoItems.Count==0)
            {
                throw new EmptyCustomListException($"Axtarisa uygun gelen tapsiriq tapilmadi");
            }
            return NewTodoItems;
        }
    }
}
