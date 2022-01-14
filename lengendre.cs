using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Physical_Geodesy
{
    class lengendre
    {
      public  static  void lengendreInCrossOrder(int num, double theta,out double[,] p)
        {
            DateTime beforDT = System.DateTime.Now;
            p =new double[num + 3, num + 3];
            for (int n = 0; n < num + 3; n++)
            {
                for (int m = 0; m < num + 3; m++)
                {
                    p[n, m] = 0;
                }
            }
            double theta_j =Math.Round( theta*1000000);
            p[0, 0] = 1.0;
            p[1, 0] = Math.Sqrt(3) * Math.Cos(theta);
            p[1, 1] = Math.Sqrt(3) * Math.Sin(theta);
       
            for (int n = 2; n <= num; n++)
            {
                for (int m = 0; m <= n; m++)
                {
                    double t = Math.Cos(theta);
                    if (m == 1 || m == 0)
                    {
                        double bnm = Math.Sqrt((2 * n + 1) * (n + m - 1) * (n - m - 1)) / Math.Sqrt((2 * n - 3) * (n + m) * (n - m));
                        double anm = Math.Sqrt((2 * n - 1) * (2 * n + 1)) / Math.Sqrt((n - m) * (n + m));
                        p[n, m] = anm * t * p[n - 1, m] - bnm * p[n - 2, m];
                    }
                    else
                    {
                        int M = m - 2;
                        double b0 = new double();
                        if (M == 0)
                            b0 = 1;
                        else
                            b0 = 0;
                        double Cnm = Math.Sqrt(1 + b0) * Math.Sqrt((2 * n + 1) * (n + m - 2) * (n + m - 3)) / Math.Sqrt((2 * n - 3) * (n + m) * (n + m - 1));
                        double Dnm = Math.Sqrt(1 + b0) * Math.Sqrt((n - m + 1) * (n - m + 2)) / Math.Sqrt((n + m) * (n + m - 1));
                        double Hnm = Math.Sqrt((2 * n + 1) * (n - m) * (n - m - 1)) / Math.Sqrt((2 * n - 3) * (n + m) * (n + m - 1));
                        p[n, m] = Cnm * p[n - 2, m - 2] - Dnm * p[n, m - 2] + Hnm * p[n - 2, m];
                    }
                }
            }

          
        }
    }
     
}
