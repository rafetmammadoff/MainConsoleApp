using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class TodoItemNotFoundException : Exception
    {
        public TodoItemNotFoundException(string msg) : base(msg)
        {

        }
    }
}
