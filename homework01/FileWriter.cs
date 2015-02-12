using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework01
{
    public interface FileWriter
    {
        void ConvertFile(string inputPath);
    }

    public class PigLatinWriter : FileWriter
    {
        public string outputPath;

        public PigLatinWriter(string outputFile)
        {
            outputPath = outputFile;
        }
        public void ConvertFile(string inputPath)
        {
            int counter;
            int stringLength;
            int i;
            char printChar;
            char firstChar = ' ';
            string fileLine;
            string tab = "\t";
            char tabDelim = Convert.ToChar(tab);
            char[] delimiter = {'=', ' ', tabDelim};
            char[] vowels = {'A', 'a', 'E', 'e', 'I', 'i', 'O', 'o', 'U', 'u'};

            string[] lines = System.IO.File.ReadAllLines(@inputPath);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@outputPath))
            {
                for (counter = 0; counter < lines.Length; counter++)
                {
                    fileLine = lines[counter];
                    string[] words = fileLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                    if (words.Length == 0)
                    {
                        file.WriteLine();
                    }
                    else
                    {
                        switch (words[0].ToUpper())
                        {
                            case "[TARGET]":
                                stringLength = lines[counter].Length;
                                for (i = 0; i < stringLength; i++)
                                {
                                    printChar = lines[counter][i];
                                    if (printChar == '[')
                                    {
                                        file.Write(printChar);
                                    }
                                    else if (i == 1)
                                    {
                                        firstChar = lines[counter][i];
                                    }
                                    else if (printChar == ']')
                                    {
                                        file.Write(firstChar);
                                        file.WriteLine("ay]");
                                    }
                                    else
                                    {
                                        file.Write(printChar);
                                    }
                                }
                                break;
                            case "NAME":
                                file.Write(words[0]);
                                file.Write("=");
                                stringLength = words[1].Length;
                                firstChar = words[1][0];
                                for (i = 0; i < stringLength; i++)
                                {
                                    if (i == 0)
                                    {
                                        if (vowels.Contains(firstChar))
                                        {
                                            file.Write(firstChar);
                                        }
                                    }
                                    else
                                    {
                                        file.Write(words[1][i]);
                                    }
                                }
                                if (vowels.Contains(firstChar))
                                {
                                    file.WriteLine("way");
                                }
                                else
                                {
                                    file.Write(firstChar);
                                    file.WriteLine("ay");
                                }
                                break;
                            default:
                                file.WriteLine(fileLine);
                                break;
                        }
                    }
                }
            }
        }
    }
}
