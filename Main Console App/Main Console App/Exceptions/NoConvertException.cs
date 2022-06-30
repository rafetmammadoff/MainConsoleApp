using System;
using System.Collections.Generic;
using System.Text;

namespace Main_Console_App.Exceptions
{
    public class NoConvertException : Exception
    {
        public NoConvertException(string msg) : base(msg)
        {
                
        }
    }
}
