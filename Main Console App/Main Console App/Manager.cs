using Main_Console_App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App
{
    class Manager : ITodoManager
    {
        public List<TodoItem> TodoItems => new List<TodoItem>();

        public void AddTodoItem(TodoItem todoItem)
        {
            TodoItems.Add(todoItem);
        }

        public void ChangeTodoItemStatus(int no, TodoStatus status)
        {
            TodoItem todoItem = TodoItems.Find(x => x.No == no);
            todoItem.Status = status;

        }

        public void DeleteTodoItem(int no)
        {
            TodoItem todoItem = TodoItems.Find(x => x.No == no);
            TodoItems.Remove(todoItem);
        }

        public void EditTodoItem(int no, string tittle, string description, DateTime deadline)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> FilterTodoItems(DateTime fromDate, DateTime toDate, TodoStatus? status)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetAllDelayedTasks()
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetAllTodoItems()
        {
            return TodoItems;
        }

        public List<TodoItem> GetAllTodoItemsByStatus(TodoStatus status)
        {
            List<TodoItem> NewTodoItems = TodoItems.FindAll(x => x.Status == status);
            return NewTodoItems;
        }

        public List<TodoItem> SearchTodoItems(string text)
        {
            List<TodoItem> NewTodoItems = TodoItems.FindAll(x => x.Tittle.Contains(text));
            return NewTodoItems;
        }
    }
}
