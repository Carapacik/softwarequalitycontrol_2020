using System;

namespace Triangle
{
    public class Program
    {
        static void GetTriangleType (double a, double b, double c)
        {
            if (a + b <= c || b + c <= a || a + c <= b)
            {
                Console.WriteLine("Это не треугольник");
                return;
            }
            else if (a == b && a == c && b == c)
            {
                Console.WriteLine("Треугольник равносторонний");
                return;
            }
            else if (a == b || b == c || a == c)
            {
                Console.WriteLine("Треугольник равнобедренный");
                return;
            }
            Console.WriteLine("Треугольник обычный");
        }

        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid input");
                return;
            }
            try
            {
                double a = double.Parse(args[0]);
                double b = double.Parse(args[1]);
                double c = double.Parse(args[2]);

                GetTriangleType(a, b, c);
            }
            catch
            {
                Console.WriteLine("Неизвестная ошибка");
            }
        }
    }
}
