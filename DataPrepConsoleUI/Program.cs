using System;

namespace DataPrepConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Give full path of the data you want to clean (with extension):");
            string filepath = Console.ReadLine();

            Console.WriteLine("Give delimiter of your file:");
            string delimiter = Console.ReadLine();

            DataCleaningManager manager = new DataCleaningManager(filepath, delimiter);
            manager.GetCleanData();

            Console.WriteLine("Check for your new file in your folder.");
        }


    }
}
