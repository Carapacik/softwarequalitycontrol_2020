using System;

namespace Triangle
{
    public static class Program
    {
        private static bool CheckMaxDouble(double a, double b, double c)
        {
            return a > double.MaxValue || b > double.MaxValue || c > double.MaxValue;
        }

        private static bool CheckNonPositiveNumber(double a, double b, double c)
        {
            return a <= 0 || b <= 0 || c <= 0;
        }

        private static bool CheckTriangleExist(double a, double b, double c)
        {
            return a + b > c && b + c > a && a + c > b;
        }

        private static bool CheckEquilateralTriangle(double a, double b, double c)
        {
            return a == b && a == c && b == c;
        }

        private static bool CheckIsoscelesTriangle(double a, double b, double c)
        {
            return a == b || b == c || a == c;
        }

        private static void GetTriangleType(double a, double b, double c)
        {
            if (CheckMaxDouble(a, b, c) || CheckNonPositiveNumber(a, b, c))
            {
                Console.WriteLine("Неизвестная ошибка");
                return;
            }

            if (!CheckTriangleExist(a, b, c))
            {
                Console.WriteLine("Это не треугольник");
                return;
            }

            if (CheckEquilateralTriangle(a, b, c))
            {
                Console.WriteLine("Треугольник равносторонний");
                return;
            }

            if (CheckIsoscelesTriangle(a, b, c))
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
                Console.WriteLine("Неизвестная ошибка");
                return;
            }

            try
            {
                var a = double.Parse(args[0]);
                var b = double.Parse(args[1]);
                var c = double.Parse(args[2]);

                GetTriangleType(a, b, c);
            }
            catch
            {
                Console.WriteLine("Неизвестная ошибка");
            }
        }
    }
}