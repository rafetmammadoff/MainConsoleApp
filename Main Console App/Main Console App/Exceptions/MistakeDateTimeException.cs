using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class MistakeDateTimeException : Exception
    {
        public MistakeDateTimeException(string msg)   : base(msg)   
        {

        }
    }
}
