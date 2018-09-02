using System;
using System.IO;
using System.Linq;



namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Считывание текста из файла 
            string textIN = "";
            try
            {      
                Console.WriteLine("Введите путь к исходному файлу (пример: D:\\Task\\in.txt)");
                string filePath = Console.ReadLine();
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Выбранный файл не существует!");
                }
                textIN = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
            #endregion
            
            #region Создание массива Separators небуквенных символов для метода Split
            string stringSymbolSeparators = "";
            for (int i = 0; i < 128; i++)
            {
                if ((i < 97 || i > 122) && i != 45)
                    stringSymbolSeparators = stringSymbolSeparators + Convert.ToChar(i);
            }
            char[] Separators = stringSymbolSeparators.ToCharArray();
            #endregion


            var PairsWordFrequency =
                 from word in textIN.ToLower().Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                 group word by word into g
                 orderby g.Key[0] ascending, g.Count() descending
                 select new { Word = g.Key, Count = g.Count() };
            var Group_FirstLetter_Pairs =
                from Pair in PairsWordFrequency
                group Pair by Pair.Word[0] into groupPair
                orderby groupPair.Key ascending
                select new { FirstLetter = groupPair.Key, Pairs = groupPair };

            #region Формирование выходного текста textOUT
            string textOUT = "";
            foreach (var item in Group_FirstLetter_Pairs)
            {
                textOUT = textOUT + item.FirstLetter + Environment.NewLine;
                foreach (var item1 in item.Pairs)
                {
                    textOUT = textOUT + item1.Word + "\t" + item1.Count + Environment.NewLine;
                }
            }
            #endregion

            #region Запись результата в файл   
            try
            {
                Console.WriteLine("Введите адрес для сохранения вычислений (пример: D:\\Task\\out.txt)");               
                string pathOut = Console.ReadLine();
                File.WriteAllText(pathOut, textOUT);
                Console.WriteLine("Файл успешно сохранен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            Console.ReadKey();
        }
    }
}


