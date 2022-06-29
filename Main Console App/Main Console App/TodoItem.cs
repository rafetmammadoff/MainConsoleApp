using Main_Console_App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App
{
    public class TodoItem
    {
        public TodoItem()
        {
            _totalCount++;
            _no = _totalCount;
            Status = TodoStatus.Todo;
        }
        private int _no;
        private static int _totalCount;
        public int No => _no;
        public string Tittle;
        private string _description;
        public TodoStatus Status;
        public DateTime DeadLine;
        public DateTime TypeChangeAt;
        public string Description
        {
            get => _description;
            set
            {
                if (CheckDescription(value))
                {
                    _description = value;
                }
            }
        }
        public static bool CheckDescription(string desc)
        {
            int count = 0;
            if (!String.IsNullOrWhiteSpace(desc))
            {
                string[] words = desc.Split(' ', ',');
                foreach (var item in words)
                {
                    if (item.Length>=2)
                    {
                        count++;
                    }
                }
                if (count>=2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
