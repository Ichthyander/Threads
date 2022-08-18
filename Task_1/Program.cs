using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/*1.    Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. 
 * Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом. 
 * Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз. 
 * Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
 * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно. 
 * Создать многопоточное приложение, моделирующее работу садовников.*/

namespace Task_1
{
    class Program
    {
        const int columns = 10;
        const int rows = 2;
        static int[,] gardenPlan = new int[rows, columns] { { 1000, 2000, 1500, 1000, 1800, 900, 200, 1400, 700, 1300 }, { 650, 1050, 500, 1300, 700, 800, 2100, 2200, 1100, 800 } };

        static void Main(string[] args)
        {
            PrintPlan();

            ThreadStart threadStart = new ThreadStart(Gardener1);
            Thread thread = new Thread(threadStart);
            thread.Start();

            Thread.Sleep(1000);     //Без этой задержки, по всей видимости, два потока запускаются одновременно, и на экране возникает сразу два вывода

            Gardener2();

            Console.ReadKey();
        }


        static void Gardener1()
        {
            /*Моделируется маршрут 1 садовника

            ----->
                 |
            <-----

            */
            for (int i = 0; i < rows; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (gardenPlan[i, j] >= 0)
                        {
                            int delay = gardenPlan[i, j];
                            gardenPlan[i, j] = -1;
                            Console.Clear();
                            PrintPlan();
                            Thread.Sleep(delay);
                        }
                    }
                }
                else
                {
                    for (int j = columns - 1; j >= 0; j--)
                    {
                        if (gardenPlan[i, j] >= 0)
                        {
                            int delay = gardenPlan[i, j];
                            gardenPlan[i, j] = -1;
                            Console.Clear();
                            PrintPlan();
                            Thread.Sleep(delay);
                        }
                    }
                }

            }
        }

        static void Gardener2()
        {
            /*Моделируется маршрут 2 садовника

            ^  |--^
            |  |  |
            |--v  |

            */
            for (int i = columns - 1; i >= 0; i--)
            {
                if (i%2 != 0)
                {
                    for (int j = rows - 1; j >= 0; j--)
                    {
                        if (gardenPlan[j, i] >= 0)
                        {
                            int delay = gardenPlan[j, i];
                            gardenPlan[j, i] = -2;
                            Console.Clear();
                            PrintPlan();
                            Thread.Sleep(delay);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < rows; j++)
                    {
                        if (gardenPlan[j, i] >= 0)
                        {
                            int delay = gardenPlan[j, i];
                            gardenPlan[j, i] = -2;
                            Console.Clear();
                            PrintPlan();
                            Thread.Sleep(delay);
                        }
                    }
                }
            }
        }

        static void PrintPlan()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{gardenPlan[i, j]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
