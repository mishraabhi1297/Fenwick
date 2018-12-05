using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenwick_Technical_Task
{
    public class Program
    {
        /// <summary>
        /// Display the help message when user inputs a invalid
        /// command or inputs the command for help
        /// </summary>
        public static void help()
        {
            string helpMessage = "\nIn order to use this command line application, you need to follow the instructions provided below:\n";
            helpMessage += "1. Every command must begin with keyword 'Stats.exe'\n";
            helpMessage += "2. To record a new number into a specific file, you must use the command 'Stats.exe record <filepath> <value 2..value n>'. For Example,\n";
            helpMessage += "\t2.1 Stats.exe record stats.txt 12.1\n";
            helpMessage += "\t2.2 Stats.exe record stats.txt 4.8 20.0\n";
            helpMessage += "\t2.3 Stats.exe record stats.txt 9.9\n";
            helpMessage += "3. You can only store integers and decimal numbers into a file\n";
            helpMessage += "4. To view the summary of a specific file, you must use the command 'Stats.exe summarise <filepath>'. For Example,\n";
            helpMessage += "\t4.1 Stats.exe summarise Stats.exe\n";
            helpMessage += "5. You can type and enter 'Stats.exe help' to print this message again";
            Console.WriteLine(helpMessage);
        }

        /// <summary>
        /// To insert the numeric values within
        /// a specific file 
        /// </summary>
        /// <param name="command"></param>
        public static void record(string[] command)
        {
            if (command.Length <= 2)
            {
                Console.WriteLine("The command does not contain the filepath");
            }
            else if ((command[2] == "") || (!File.Exists(command[2])))
            {
                Console.WriteLine("File does not exist in '" + command[2] + "' specified path");
            }
            else if(File.Exists(command[2]))
            {
                if (command[3] == "")
                {
                    Console.WriteLine("No numeric records found");
                }
                else
                {
                    StreamWriter output = File.AppendText(command[2]);
                    decimal s;

                    for (int i = 3; i < command.Length; i++)
                    {
                        bool check = decimal.TryParse(command[i], out s);
                        if (check)
                        {
                            output.WriteLine(command[i]);
                        }
                        else
                        {
                            Console.WriteLine("Inputted record (" + command[i] + ") is not numeric");
                        }
                    }

                    Console.WriteLine("Numeric records added successfully to " + command[2]);
                    output.Close();
                }
            }
        }

        /// <summary>
        /// To display the summary (max, min, average, number of entries)
        /// for a specific file
        /// </summary>
        /// <param name="command"></param>
        public static void summarise(string[] command)
        {
            if (command.Length <= 2)
            {
                Console.WriteLine("The command does not contain the filepath");
            }
            else if ((command[2] == "") || (!File.Exists(command[2])))
            {
                Console.WriteLine("File does not exist in '" + command[2] + "' specified path");
            }
            else if (File.Exists(command[2]) && (command.Length < 4))
            {
                string[] arrayOfValues = File.ReadAllLines(command[2]);
                decimal sum = 0, average = 0, max = 0, min = decimal.Parse(arrayOfValues[0]);

                for (int i = 0; i < arrayOfValues.Length; i++)
                {
                    sum += decimal.Parse(arrayOfValues[i]);

                    if (decimal.Parse(arrayOfValues[i]) > max)
                    {
                        max = decimal.Parse(arrayOfValues[i]); //maximum amongst all the numeric values
                    }

                    if (decimal.Parse(arrayOfValues[i]) < min)
                    {
                        min = decimal.Parse(arrayOfValues[i]); //minimum amongst all the numeric values
                    }
                }

                average = sum / arrayOfValues.Length; //average of all the numeric values

                //Calculate the space needed for table column
                List<int> numOfSpace = new List<int>();
                numOfSpace.Add(sum.ToString("0.##").Length);
                numOfSpace.Add(max.ToString("0.##").Length);
                numOfSpace.Add(min.ToString("0.##").Length);
                numOfSpace.Add(average.ToString("0.##").Length);
                int maxSpace = numOfSpace.Max();

                //To display the summary in a tabular form
                Console.WriteLine("+" + new String('-', 13) + "+" + new String('-', maxSpace + 1) + "+");
                Console.WriteLine("|{0,-13}| {1," + $"{maxSpace}" + "}|", "# of Entries", arrayOfValues.Length);
                Console.WriteLine("|{0,-13}| {1," + $"{maxSpace}" + "}|", "Min. value", min);
                Console.WriteLine("|{0,-13}| {1," + $"{maxSpace}" + "}|", "Max. value", max);
                Console.WriteLine("|{0,-13}| {1," + $"{maxSpace}" + "}|", "Avg. value", average.ToString("0.##"));
                Console.WriteLine("+" + new String('-', 13) + "+" + new String('-', maxSpace + 1) + "+");
            }
            else
            {
                Console.WriteLine("The summarise command must only contain 3 elements (stats.exe, summarise and <filepath>)");
            }
        }

        public static void Main(string[] args)
        {
            string input;
            do
            {
                string[] command;
                Console.WriteLine("\nPlease enter a command or enter 'exit' to quit the program: ");
                input = Console.ReadLine();

                if (input != "exit")
                {
                    command = input.Split(' ');

                    if ((command[0].ToLower() != "stats.exe") || ((command[1] != "record") && (command[1] != "summarise") && (command[1] != "help")))
                    {
                        help();
                    }
                    else
                    {
                        switch (command[1])
                        {
                            case "record":
                                //Console.WriteLine(command.Length);
                                record(command);
                                break;
                            case "summarise":
                                summarise(command);
                                break;
                            case "help":
                                help();
                                break;
                            default:
                                help();
                                break;
                        }
                    }
                }
            } while (input != "exit");
        }
    }
}
