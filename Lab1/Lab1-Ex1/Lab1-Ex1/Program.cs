using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1_Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader("DataFile.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line = line.Replace(",", " y:");
                        line = "x:" + line;
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            }
        }
    }

