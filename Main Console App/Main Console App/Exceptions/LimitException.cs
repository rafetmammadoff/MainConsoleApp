using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class LimitException : Exception
    {
        public LimitException(string msg) : base(msg)
        {

        }
    }
}
