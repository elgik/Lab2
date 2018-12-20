using System;
using System.Globalization;

namespace Lab2
{
    public class Controller
    {
        private double st = 18f; //коэффициент
        private int N = 13; //размер сетки        
        private double L = 1;
        private double T = 1;
        private double h;
        private double tau;
        private double[][] U; //сетка
        private int padCount = 0;

        public void Process()
        {
            h = L / ((double) N - 1f); //шаг
            Console.WriteLine($"h = {h}");
            tau = T / ((double) N - 1f); //шаг
            Console.WriteLine($"tau = {tau}");

            U = new double[N][];
            for (int i = 0; i < N; ++i)
            {
                U[i] = new double[N];
            }
            //границы
            for (int i = 0; i < N; ++i)
            {
                U[i][0] = 0.0f;
                U[0][i] = -st * (Math.Pow((i * tau), 2f) - (2 * T * (i * tau)));
                U[N - 1][i] = -(st + 1) * (Math.Pow((i * tau), 2f) - 2 * T * (i * tau));
            }
            //заполнение сетки
            for (int j = 1; j < N; ++j)
            {
                for (int i = 1; i < N - 1; ++i)
                {
                    U[i][j] = U[i][j - 1] + h * (U[i + 1][j - 1] - 2.0f * U[i][j - 1] + U[i - 1][j - 1]) - tau *
                              (st + 3)
                              * (Math.Pow((j * tau), 2) - 2 * T * (j * tau)) * Math.Pow(Math.Exp(1f), (-i * h / st));
                    if (padCount < U[i][j].ToString(CultureInfo.InvariantCulture).Length)
                        padCount = U[i][j].ToString(CultureInfo.InvariantCulture).Length;
                }
            }

            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    Console.Write(U[i][j].ToString(CultureInfo.InvariantCulture).PadRight(padCount + 1));
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}