using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the directory path where the sheets reside..");
            var dirPath = Console.ReadLine();

            Console.WriteLine("Enter the sheet1 Name (give the file extension too!)...");
            var sheet1Path = Console.ReadLine();

            Console.WriteLine("Enter the sheet2 Name (give the file extension too!)...");
            var sheet2Path = Console.ReadLine();

            string sFilePath;

            // C:\Users\shekhar.mittapelly\Downloads\
            // ~intersection of two files
            String directory = @"" + dirPath;
            String[] linesA = File.ReadAllLines(Path.Combine(directory, sheet1Path));
            String[] linesB = File.ReadAllLines(Path.Combine(directory, sheet2Path));

            string[] ID_A = linesA.Select(m => m.Split(',')[0].Trim()).ToArray();
            string[] ID_B = linesB.Select(m => m.Split(',')[0].Trim()).ToArray();

            var Uniq_ID = ID_A.Except(ID_B).ToArray();

            // this is an awesome linq check how i am doing things here..  basically checks if any line in linesA has any item in Uniq_ID as substring
            var mx = linesA.Where(x => Uniq_ID.Any(m => x.Contains(m))).ToArray();

            //IEnumerable<String> onlyB = linesA.Except(linesB);

            File.WriteAllLines(Path.Combine(directory, "Result.txt"), mx);

            sFilePath = Path.Combine(directory, "Result.txt");

            if (File.Exists(sFilePath))
            {
                File.WriteAllLines(sFilePath, File.ReadAllLines(sFilePath).Select(x => string.Format("({0}),", x)));
                //File.WriteAllLines(sFilePath, File.ReadAllLines(sFilePath).Select(x => string.Format("({0}),", x)));
                Console.WriteLine("Enter the column numbers in order whose values to be enclosed in quotes ' ' .......");

                var indexesString = Console.ReadLine();
                indexesString = indexesString.Trim();

                var indexes = indexesString.Split(',');

                var fileLines = File.ReadAllLines(sFilePath);

                for (int tt = 0; tt < fileLines.Length; tt++)
                {
                    var singleLineTrimmed = fileLines[tt].Substring(1, fileLines[tt].Length - 2);
                    var stringArr = singleLineTrimmed.Split(',');
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        var k = Int32.Parse(indexes[i]);
                        stringArr[k - 1] = "'" + stringArr[k - 1] + "'";
                    }
                    fileLines[tt] = "(" + string.Join(",", stringArr) + ",";
                }

                File.WriteAllLines(sFilePath, fileLines);

            }
            else
            {
                Console.WriteLine("Gimme the appropriate path, Damnit!!");
            }
        }
    }
}
