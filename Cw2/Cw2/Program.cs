using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string resulttype = "";
            string sourcepath = "";
            string resultpath = "";
                for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Length == 3)
                {
                    resulttype = args[i];
                }
                else if (args[i].EndsWith("csv"))
                {
                    sourcepath = args[i];
                }
                else
                {
                    resultpath = args[i];
                }

            }

            if (resulttype == "")
            {
                resulttype = "xml";
            }
            if (resultpath == "")
            {
                resultpath = @"żesult.xml"; 
            }
            if (sourcepath == "")
            {
                sourcepath = @"data.csv";
            }

            string logFile = @"log.txt";
            FileInfo filecsv = new FileInfo(sourcepath);
            FileInfo filelog = new FileInfo(logFile);
            var list = new List<Student>();
            try
            {
                StreamWriter logStream = new StreamWriter(filelog.OpenWrite());
                try
                {
                    StreamReader stream = new StreamReader(filecsv.OpenRead());
                    string line = "";
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] words = line.Split(',');
                        bool tmp = true;
                        words[1] = Regex.Replace(words[1], "[0-9]", "");
                        foreach (Student student in list)
                        {
                            if ((words[0].Equals(student.Imie) && words[1].Equals(student.Nazwisko) && ("s" + words[4]).Equals(student.index)) || words.Length != 9)
                                tmp = false;
                        }
                        for (int i = 0; i < words.Length; i++)
                        {
                            if (words[i].Equals(" "))
                                tmp = false;
                        }
                        words[2] = words[2].Replace("dzienne", "");
                        words[2] = words[2].Replace("zaoczne", "");
                        if (tmp)
                        {
                            list.Add(new Student
                            {
                                Imie = words[0],
                                Nazwisko = words[1],
                                studies = new Studies(words[2], words[3]),
                                index = "s" + words[4],
                                birthDate = words[5],
                                email = words[6],
                                mothersName = words[7],
                                fathersName = words[8]
                            });
                        }
                        else
                        {
                            logStream.Write("Problem with:" + line + "\n");
                        }
                        
                    }
                    stream.Close();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Podana ścieżka jest niepoprawna");
                    logStream.Write(ex.Message + "\n");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Plik " + sourcepath + " nie istnieje");
                    logStream.Write(ex.Message + "\n");
                }
                logStream.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //XML
            if (resulttype == "xml")
            {

                using (var sw = new FileStream(resultpath, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(Uczelnia));
                    var uczelnia = new Uczelnia()
                    {
                        Author = "Marcin Wałachowski",
                        Studenci = list
                    };
                    serializer.Serialize(sw, uczelnia);
                }
            }
        }
    }
}
