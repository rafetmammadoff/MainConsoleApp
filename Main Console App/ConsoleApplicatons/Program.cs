using Main_Console_App;
using Main_Console_App.Enums;
using System;
using System.Collections.Generic;

namespace ConsoleApplicatons
{
    class Program
    {
        static void Main(string[] args)
        {
            string option;
            do
            {
                Selection();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":

                        Console.WriteLine("Tapsiriq basligini daxil edin");
                        string tittle = Console.ReadLine();
                        string description;
                        do
                        {
                            Console.WriteLine("Tapsiriq aciqlama daxil edin");
                            description = Console.ReadLine();
                        } while (!TodoItem.CheckDescription(description));

                        DateTime deadline;
                        string deadlineStr;
                        bool check;
                        do
                        {
                            Console.WriteLine("Tapsiriq dedline vaxtini teyin edin");
                            deadlineStr = Console.ReadLine();
                            check = DateTime.TryParse(deadlineStr, out deadline);
                            if (check)
                            {
                                if (deadline<DateTime.Now)
                                {
                                    check = false;
                                    throw new Exception("a");
                                }
                            }
                        } while (!check);



                        break;
                }
            } while (option != "0");

        }
        static void Selection()
        {
            Console.WriteLine(" - 1.Tapşırıq yarat");
            Console.WriteLine(" - 2.Bütün tapşırıqlara bax");
            Console.WriteLine(" - 3.Vaxtı keçmiş tapşırıqlara bax");
            Console.WriteLine(" - 4.Seçilmiş statuslu tapşırıqlara bax");
            Console.WriteLine(" - 5.Tarix intervalına görə axtar");
            Console.WriteLine(" - 6.Tapşırığın statusunu dəyişmək");
            Console.WriteLine(" - 7.Tapşırığı editləmək");
            Console.WriteLine(" - 8.Tapşırığı silməl");
            Console.WriteLine(" - 9.Tapşırıqlarda axtarış");
            Console.WriteLine(" - 0.Çıxış");
        }
    }
}
