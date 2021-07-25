using DataPrepConsoleUI.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataPrepConsoleUI
{
    public class FileIO : IFileIO
    {
        private readonly string _filepath;
        private readonly string _delimiter;

        public FileIO(string filepath, string delimiter)
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

        public List<List<string>> ReadIn()
        {
            var reader = new StreamReader(_filepath);

            List<List<string>> fileContents = GetContentsLineByLine(reader);

            return fileContents;
        }

        private List<List<string>> GetContentsLineByLine(StreamReader reader)
        {
            List<List<string>> output = new List<List<string>>();

            // just go over the first header row.
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                List<string> lineParts = line.Split(_delimiter).ToList();

                output.Add(lineParts);
            }

            reader.Close();

            return output;
        }

        public void WriteOut(List<List<string>> fileContents, string filenameSuffix)
        {
            // determine output file's path
            string outputFilepath = GenerateFilepath(filenameSuffix);

            List<string> contentsToWrite = TransformIntoWritableForm(fileContents);

            StreamWriter writer = new StreamWriter(outputFilepath);

            foreach (var line in contentsToWrite)
            {
                writer.WriteLine(line);
            }

            writer.Close();
        }

        private List<string> TransformIntoWritableForm(List<List<string>> fileContents)
        {
            List<string> output = new List<string>();

            foreach (var lineOfContent in fileContents)
            {
                string oneLine = String.Join(_delimiter, lineOfContent.ToArray());

                output.Add(oneLine);
            }

            return output;
        }

        private string GenerateFilepath(string filenameSuffix)
        {
            // most text-based extensions most likely to encounter have . + 3 letters - csv, txt
            string fileExtensionWithPoint = _filepath.Substring(_filepath.Length - 4);

            return _filepath.Replace(fileExtensionWithPoint, $"{ filenameSuffix }{ fileExtensionWithPoint }");
        }
    }
}
