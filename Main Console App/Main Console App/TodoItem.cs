using Main_Console_App.Enums;
using Main_Console_App.Exceptions;
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
        private DateTime _deadLine;
        public DateTime DeadLine
        {
            get => _deadLine;
            set
            {
                string date_str = value.ToString();
                if (CheckDeadline(date_str))
                {
                    _deadLine= value;
                }
            }
        }
        public static bool CheckDeadline(string date)
        {
            DateTime deadline;

            if (!DateTime.TryParse(date,out deadline))
            {
                throw new MistakeDateTimeException("Yanlis daxil etdiniz mm:dd:yyy kimi daxil edin ");
            }
            else
            {
                if (deadline < DateTime.Now)
                {
                    throw new MistakeDeadlineException("Deadline vaxti indiki zamandan evvel ola bilmez");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            
        }
        public DateTime StatusChangedAt;
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
