using DataPrepConsoleUI.Contracts;
using System;
using System.Collections.Generic;

namespace DataPrepConsoleUI
{
    public class DataCleaner : IDataCleaner
    {
        private readonly List<List<string>> _dataToClean;

        public DataCleaner(List<List<string>> dataToClean)
        {
            if (dataToClean is null)
            {
                throw new ArgumentNullException(nameof(dataToClean));
            }
            _dataToClean = dataToClean;
        }

        public List<List<string>> CleanData()
        {
            foreach (var lineOfData in _dataToClean)
            {
                // 4th index is the 'Value' column
                // 2nd index is the 'Round' column

                lineOfData[4] = lineOfData[4].Trim().Replace("$", "").Replace(" ", "");

                if (lineOfData[2].ToLower().Contains("final"))
                {
                    lineOfData[4] = "-1";
                    lineOfData[5] = "USD";
                }
            }

            return _dataToClean;
        }
    }
}
