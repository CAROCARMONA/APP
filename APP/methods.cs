using System;
using System.Collections.Generic;
using System.Text;

namespace APP
{
    class methods
    {

        List<double> Sales = new List<double>();
        public List<double> Regression = new List<double>();
        public methods(List<double> sales)
        {
            this.Sales = sales;
           
        }

        // function to change and view sales
        public List<double> watchSales { get => Sales; set => Sales = value; }
        
        // forecast function
        public List<double> Forecast()
        {
          
            List<double> TRIMESTERS = new List<double>() { 1,2,3,4,5,6,7,8,9,10,11,12};

            List<double> forecast = new List<double>();
            List<double> Iseasonal = new List<double>();//  seasonal index
            List<double> SeasonallyAdjust = new List<double>();//   seasonally adjust


            double totalx = 0;//  class mark
            double totaly = 0;// total sales
            double totalDES = 0;// total and seasonally adjusted

            List<double> XX = new List<double>();
            List<double> XY = new List<double>();
           
            double totalXX = 0;//total the sales XX
            double totalXY = 0;//total de sales XY



            for (int i = 0; i < Sales.Count; i++)
            {
                totaly += Sales[i];
                totalx += TRIMESTERS[i];
            }

            double avarageY = totaly / Sales.Count;// average quarterly sales
          
            double[] I = new double[Sales.Count];
           
          for (int i = 0; i < Sales.Count; i = i + 4)
            {
                //               Specific quarterly average            iseasonal
                I[i] = ((Sales[0] + Sales[4] + Sales[8]) / 3) / avarageY ;
                I[i + 1] = ((Sales[1] + Sales[5] + Sales[9]) / 3) / avarageY;
                I[i + 2] = ((Sales[2] + Sales[6] + Sales[10]) / 3) / avarageY;
                I[i + 3] = ((Sales[3] + Sales[7] + Sales[11]) / 3) / avarageY;

            }

            double[] S = new double[Sales.Count];
            double[] X= new double[Sales.Count];
            double[] Y = new double[Sales.Count];

            for (int i = 0; i < Sales.Count; i++)
            {
                // adding to seasonal index list
                Iseasonal.Add(I[i]);

                S[i] = Sales[i] / Iseasonal[i];
                SeasonallyAdjust.Add(S[i]);
                totalDES += SeasonallyAdjust[i];// sum of all the seasonally adjusted

                // x a la 2    y    XY
                X[i] = TRIMESTERS[i] * TRIMESTERS[i];
                XX.Add(X[i]);
                totalXX += XX[i];
                Y[i] = TRIMESTERS[i] * SeasonallyAdjust[i];
                XY.Add(Y[i]);
                totalXY += XY[i];
            }
            //regression equation a and b
            double a = ((totalDES * totalXX) - (totalx * totalXY)) / ((Sales.Count * totalXX) - (totalx * totalx));
            double b = ((Sales.Count * totalXY) - (totalx * totalDES)) / ((Sales.Count * totalXX) - (totalx * totalx));

            // y =a + b X
            double[] R = new double[Sales.Count];
            double[] f = new double[Sales.Count];
            for (int i = 0; i < Sales.Count; i++)
            {

                R[i] = a + b * TRIMESTERS[i];
                Regression.Add(R[i]);
                f[i] = R[i] * Iseasonal[i];
                forecast.Add(f[i]);
            }

            return forecast;

        }

    }
}

