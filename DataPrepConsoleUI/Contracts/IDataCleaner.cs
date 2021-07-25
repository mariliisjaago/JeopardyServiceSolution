using System.Collections.Generic;

namespace DataPrepConsoleUI.Contracts
{
    public interface IDataCleaner
    {
        List<List<string>> CleanData();
    }
}