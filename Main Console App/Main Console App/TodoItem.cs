using Main_Console_App.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App
{
    class TodoItem
    {
        public TodoItem()
        {
            _totalCount++;
            _no = _totalCount;
        }
        private int _no;
        private static int _totalCount;
        public int No => _no;
        public string Tittle;
        public string Description;
        public TodoStatus Status;
        public DateTime DeadLine;
        public DateTime TypeChangeAt;

    }
}
