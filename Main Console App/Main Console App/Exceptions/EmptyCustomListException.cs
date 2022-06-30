using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class EmptyCustomListException : Exception
    {
        public EmptyCustomListException(string msg) : base(msg)
        {

        }
    }
}
