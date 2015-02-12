using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework01
{
    class Program
    {
        static void Main(string[] args)
        {
            // check that the correct number of input arguments are entered
            if (args.Length > 1)
            {
                Console.WriteLine("Too many arguments.");
                return;
            } else if (args.Length == 0)
            {
                Console.WriteLine("No file name entered.");
                return;
            }
            string targetFile = @args[0];
            List<Target> TargetList = new List<Target>();
            INIReader reader;
            PigLatinWriter writer;
            string UserPrompt = " ";
            int listSize = 0;
            char[] delimiter = { ' ' };
            Target currentTarget = new Target();

            // Test that target file exists
            if (!File.Exists(@targetFile))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            // Initialize reader
            reader = new INIReader(@targetFile);
            // Read file, input to target list
            TargetList = reader.ReadTargets();
            listSize = TargetList.Count;

            // User prompts
            Console.WriteLine("Valid Commands: PRINT, PRINT SORT, PRINT <target name>,");
            Console.WriteLine("CONVERT <output file name>, ISFRIEND <target name>, EXIT");
            // Loop until user decides to exit program
            while (UserPrompt.ToUpper() != "EXIT")
            {
                Console.Write("Enter command: ");
                UserPrompt = Console.ReadLine();

                string[] words = UserPrompt.Split(delimiter);

                // check and process user input
                switch (words[0].ToUpper())
                {
                    case "PRINT":
                        if (words.Length == 1)
                        {
                            for (int i = 0; i < listSize; i++)
                            {
                                Console.WriteLine(TargetList[i].Name);
                            }
                        } else if (words[1].ToUpper() == "SORT")
                        {
                            var sortedList = TargetList.OrderBy(x => x.Name).ToList();
                            for (int i = 0; i < listSize; i++)
                            {
                                Console.WriteLine(sortedList[i].Name);
                            }
                        } else
                        {
                            if (TargetList.Exists(x => x.Name.ToUpper() == words[1].ToUpper()))
                            {
                                currentTarget = TargetList.Find(x => x.Name.ToUpper() == words[1].ToUpper());
                                currentTarget.PrintTarget();
                            } else
                            {
                                Console.WriteLine("Target does not exist");
                            }
                        }
                        break;
                    case "CONVERT":
                        if (words.Length == 1)
                        {
                            Console.WriteLine("Need an output file name. Try again.");
                        }
                        else
                        {
                            writer = new PigLatinWriter(words[1]);
                            writer.ConvertFile(targetFile);
                        }
                        break;
                    case "ISFRIEND":
                        if (words.Length == 1)
                        {
                            Console.WriteLine("Need a target name. Try again.");
                        } else
                        {
                            if (TargetList.Exists(x => x.Name.ToUpper() == words[1].ToUpper()))
                            {
                                currentTarget = TargetList.Find(x => x.Name.ToUpper() == words[1].ToUpper());
                                if (currentTarget.Friend)
                                {
                                    Console.WriteLine("Aye Captain!");
                                }
                                else
                                {
                                    Console.WriteLine("Nay, Scallywag!");
                                }
                            } else
                            {
                                Console.WriteLine("Target does not exist");
                            }
                        }
                        break;
                    case "EXIT":
                        break;
                    default:
                        Console.WriteLine("invalid command");
                        break;
                }
            }
        }
    }
}
