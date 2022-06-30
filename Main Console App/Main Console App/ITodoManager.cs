using Main_Console_App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App
{
    interface ITodoManager
    {
        List<TodoItem> TodoItems { get; }
        public void AddTodoItem(TodoItem todoItem);
        public List<TodoItem> GetAllTodoItems();
        public List<TodoItem> GetAllDelayedTasks();
        public void ChangeTodoItemStatus(int no,TodoStatus status);
        public void EditTodoItem(int no,string tittle,string description,DateTime? deadline);
        public void DeleteTodoItem(int no);
        public List<TodoItem> GetAllTodoItemsByStatus(TodoStatus? status);
        public List<TodoItem> SearchTodoItems(string text);
        public List<TodoItem> FilterTodoItems(DateTime fromDate,DateTime toDate, TodoStatus? status);
    }
}
