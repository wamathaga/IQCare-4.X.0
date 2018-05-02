using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.Administration
{
    public interface IErrorLogging
    {
        bool LogError(string sSource, string sEvent, ErrorType tEntryType);
    }
    
    [Serializable]
    public enum ErrorType
    {
        Error,
        Critical,
        Warning,
        Information

    }
}
