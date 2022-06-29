using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class MistakeDeadlineException : Exception
    {
        public MistakeDeadlineException(string msg) : base(msg)
        {

        }
    }
}
