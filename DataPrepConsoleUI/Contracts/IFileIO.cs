using System.Collections.Generic;

namespace DataPrepConsoleUI.Contracts
{
    public interface IFileIO
    {
        List<List<string>> ReadIn();

        void WriteOut(List<List<string>> fileContents, string filenameSuffix);
    }
}