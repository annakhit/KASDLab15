using System;
using System.IO;
using System.Linq;

namespace solution15
{
    internal class Program
    {
        static void Main()
        {        
            string path = @"..\..\Data\input.txt";
            string pathOut= @"..\..\Data\sorted.txt";

            string[] rows = File.ReadAllLines(path);

            MyArrayDeque<string> arrayDeque = new MyArrayDeque<string>();

            Console.WriteLine("Введите n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Наш исход файл: ");

            foreach (string row in rows)
            {
                Console.WriteLine(row);
                if (row.Count(char.IsDigit) > arrayDeque.Peek()?.Count(char.IsDigit)) arrayDeque.Add(row);
                else arrayDeque.AddFirst(row);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Вставка нужных строк: ");
            arrayDeque.Print();

            File.WriteAllLines(pathOut, arrayDeque.ToArray());

            foreach (string row in rows)
            {
                if (row.Count(char.IsWhiteSpace) > n) arrayDeque.Remove(row);
            }

            Console.WriteLine();
            Console.WriteLine("Удаление неподходящих по n: ");
            arrayDeque.Print();

            File.WriteAllLines(pathOut, arrayDeque.ToArray());
            Console.ReadKey();
        }
    }
}
