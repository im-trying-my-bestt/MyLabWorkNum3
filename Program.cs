using System;

namespace Labrob
{
    class Program
    {
        // Oбчислення довжини сторони за координатами її кінців
        static double CalcSideLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        // Oбчислення площі трикутника за формулою Герона
        static double CalcTriangleSquareByPoints(double ax, double ay, double bx, double by, double cx, double cy)
        {
            // Довжини сторін
            double sideAB = CalcSideLength(ax, ay, bx, by);
            double sideBC = CalcSideLength(bx, by, cx, cy);
            double sideCA = CalcSideLength(cx, cy, ax, ay);

            // Напівпериметр
            double p = (sideAB + sideBC + sideCA) / 2;

            // Формула Герона
            double ploshcha = Math.Sqrt(p * (p - sideAB) * (p - sideBC) * (p - sideCA));
            return ploshcha;
        }

        // Пошук мінімальної площі трикутника (перебором)
        static void CalcMinOfSquares(int a, int b)
        {
            double minPloshcha = double.MaxValue;
            int minX = 0, minY = 0;

            // Обмежуємо область пошуку розумними межами
            int searchPromij = Math.Max(a, b) + 10;
            for (int x = -searchPromij; x <= searchPromij; x++)
            {
                for (int y = -searchPromij; y <= searchPromij; y++)
                {
                    // Перевірка на колінеарність за векторним добутком. Якщо колінеарні - 0.
                    int crossProduct = a * y - b * x;

                    if (crossProduct != 0)
                    {
                        double ploshcha = CalcTriangleSquareByPoints(0, 0, a, b, x, y);
                        if (ploshcha > 0 && ploshcha < minPloshcha)
                        {
                            minPloshcha = ploshcha;
                            minX = x;
                            minY = y;
                        }
                    }
                }
            }
            Console.WriteLine($"Координати вершини C: ({minX}, {minY})");
            Console.WriteLine($"Мінімальна площа трикутника: {minPloshcha:F6}");
            /*
             Console.WriteLine("Вершини трикутника:");
             Console.WriteLine($"A = (0, 0)");
             Console.WriteLine($"B = ({a}, {b})");
             Console.WriteLine($"C = ({minX}, {minY})");
            */
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Пошук мінімальної площі трикутника ABC, де A = (0, 0), B = (a, b), C = (x, y)");

            // Kоординати точки B (натуральні числа)
            Console.Write("Введіть координату a: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введіть координату b: ");
            int b = int.Parse(Console.ReadLine());
            if (a <= 0 || b <= 0)
            {
                Console.WriteLine("Помилка: a та b повинні бути натуральними (строго додатними) числами!");
                return;
            }
            // Пошук мінімальної площі
            CalcMinOfSquares(a, b);
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}