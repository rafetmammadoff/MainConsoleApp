using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string msg) : base(msg)
        {

        }
    }
}
