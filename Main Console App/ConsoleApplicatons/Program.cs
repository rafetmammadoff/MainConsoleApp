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
