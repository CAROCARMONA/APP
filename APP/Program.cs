using System;
using System.Collections.Generic;

namespace APP
{
    class Program
    {

        static void Main(string[] args)
        {
            List<double> SalesXthree = new List<double>() { 600, 1550, 1500, 1500, 2400, 3100, 2600, 2900, 3800, 4500, 4000, 4900 };
            methods f = new methods(SalesXthree);

            List<double> Fores = f.Forecast();

            for (int i = 0; i < SalesXthree.Count; i++)
            {
                
                Console.WriteLine("Pronostico " + Fores[i] + " trimestre " + (i + 1));
                Console.WriteLine("regre" + f.Regression[i]);
                Console.WriteLine("ventas" + f.watchSales[i]);
            }



        }
    }
}