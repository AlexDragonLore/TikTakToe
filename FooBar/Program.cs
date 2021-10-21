using System;

namespace FooBar
{
    class Program
    {
        static void Main(string[] args)
        {
            for ( ; ; )
            {
                Console.WriteLine("Введите целое число.");
                var inputValue = Console.ReadLine();
                if (int.TryParse(inputValue, out int value))
                {
                    Console.WriteLine($"Получилось - {FooBar(value)}\n");
                }
                else
                {
                    Console.WriteLine("Вы ввели не число.\n");
                }
            }
        }
        private static string FooBar(int value)
        {
            string foobarLine = "";
            foobarLine += value % 3 == 0 ? "foo" : null;
            foobarLine += value % 5 == 0 ? "bar" : null;
            return foobarLine;
        }
    }
}
