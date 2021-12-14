using System;
using System.Linq;

namespace Taxi
{
    class Program
    {
        /// <summary>
        /// Вывод ошибки.
        /// </summary>
        /// <param name="msg">Текст ошибки.</param>
        static void PrintErr(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        /// <summary>
        /// Проверяет входные данные.
        /// </summary>
        /// <returns>Введенное пользователем число от 1 до max</returns>
        static int InputInt(int max, string msg)
        {
            int value;

            while (true)
            {
                Console.Write(msg);

                if (!Int32.TryParse(Console.ReadLine(), out value))
                {
                    PrintErr("Введите число!");
                    continue;
                }

                if (!(value >= 1 && value <= max))
                {
                    PrintErr($"Число вне диапазона! (1 <= n <= {max})");
                    continue;
                }

                return value;
            }
        }

        static void TaxiDistribution(int[] kils, int[] tariffs, ref int[] taxis, int count)
        {
            for(int emp = 0; emp < count; emp++)
            {
                taxis[Array.IndexOf(kils, kils.Min())] = Array.IndexOf(tariffs, tariffs.Max()) + 1;
                kils[Array.IndexOf(kils, kils.Min())] = Int32.MaxValue; 
                tariffs[Array.IndexOf(tariffs, tariffs.Max())] = Int32.MinValue;
            }
        }

        static void Main(string[] args)
        {
            // Количество сотрудников, совпадающих с количеством машин.
            int countEmps;            

            countEmps = InputInt(1000, "Введите количество сотрудников: ");

            // Количество километров до дома для каждого сотрудника.
            int[] kils = new int[countEmps];
            // Тариф для каждого такси.
            int[] tariffs = new int[countEmps];
            // Номера такси для каждого сотрудника.
            int[] taxis = new int[countEmps];
 
            for (int i = 0; i < countEmps; i++)
            {
                kils[i] = InputInt(1000, "Введите сколько километров требуется преодолеть "
                    + (i + 1) + " сотруднику до дома: ");
            }

            for (int i = 0; i < countEmps; i++)
            {
                tariffs[i] = InputInt(10000, "Введите тариф для " + (i + 1) + " такси: ");
            }

            TaxiDistribution(kils, tariffs, ref taxis, countEmps);

            // Вывод.
            Console.Clear();
            Console.WriteLine("Номер сотрудника\tНомер такси");
            for (int i = 0; i < countEmps; i++)
            {
                Console.WriteLine($"{i + 1, 16}{taxis[i], 19}");
            }

            Console.ReadKey();
        }
    }
}
