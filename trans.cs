using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physical_Geodesy
{
  public  class trans
    {
       static double e_2= Ellipsoid_parameters.e_2;
        static double f = Ellipsoid_parameters.f;
        static double a = Ellipsoid_parameters.a;
 
        public double degree2rad(double d,double m,double s)
        {
            double D = d + m / 60 + s / 3600;
            return D / 180 * Math.PI;
        }
        public static void B_L_H2theta_lemda_r(double B,double L,double H,out double theta,out double lemda,out double r)
        {
            double W = Math.Sqrt(1 - e_2 * Math.Sin(B) * Math.Sin(B));
            double N = a / W;
            double Y = (N + H) * Math.Cos(B) * Math.Sin(L);
            double X= (N + H) * Math.Cos(B) * Math.Cos(L);
            double Z = (N * (1 - e_2) + H) * Math.Sin(B);

            r = Math.Sqrt(X * X + Y * Y + Z * Z);
            theta = Math.Atan2(Math.Sqrt(X * X + Y * Y),Z);
            lemda = Math.Atan2(Y, X);
        }

         public class  degree
        {
            public double d = new double();
            public double m = new double();
            public double s = new double();
            public degree(double d,double m,double s)
            {
                this.d = d;
                this.m = m;
                this.s = s;
            }
            public double To_rad()
            {
                double D = d + m / 60 + s / 3600;
                return D / 180 * Math.PI;
            }
        }
        public static degree rad_to_dms(double rad)
        {
            double d = new double();
            double m = new double();
            double s = new double();
            double box = rad / Math.PI * 180;
            d =Math.Round(box);
            m = Math.Round((box - d) * 60);
            s = Math.Round((box - d-m/60) * 3600);
            degree dms=new degree(d, m, s);
            return dms;
        }

    }
}
