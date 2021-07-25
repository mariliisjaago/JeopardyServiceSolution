using DataPrepConsoleUI.Contracts;
using System;
using System.Collections.Generic;

namespace DataPrepConsoleUI
{
    public class DataCleaningManager
    {
        private readonly string _filepath;
        private readonly string _delimiter;

        public DataCleaningManager(string filepath, string delimiter)
        {
            if (filepath is null)
            {
                throw new ArgumentNullException(nameof(filepath));
            }

            if (delimiter is null)
            {
                throw new ArgumentNullException(nameof(delimiter));
            }

            _filepath = filepath;
            _delimiter = delimiter;
        }

        public void GetCleanData()
        {
            IFileIO fileIO = new FileIO(_filepath, _delimiter);

            IDataCleaner dataCleaner = new DataCleaner(fileIO.ReadIn());

            fileIO.WriteOut(dataCleaner.CleanData(), "_clean");
        }
    }
}
