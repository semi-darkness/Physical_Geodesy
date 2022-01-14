using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physical_Geodesy
{
    class Normal_Gravity
    {

        static  double a = Ellipsoid_parameters.a;
        static double f = Ellipsoid_parameters.f;
        static double GM = Ellipsoid_parameters.GM;
        static double w = Ellipsoid_parameters.w;
        static double b = a - a * f;
        static double e = Math.Sqrt(a * a - b * b) / a;
        static double e1 = Math.Sqrt(a * a - b * b) / b;
        static double m = w * w * a * a * b / GM;
        public static double OnSurface(double B)
        {
            double r = new double();

            double E = Math.Sqrt(a * a - b * b);
            double u = b;

            double q = 0.5 * ((1 + 3 * u * u / (E * E)) * Math.Atan2(E, u) - 3 * u / E);
            double q0 = 0.5 * ((1 + 3 * b * b / (E * E)) * Math.Atan2(E, b) - 3 * b / E);
            double q01 = 3 * (1 + b * b / (E * E)) * (1 - b / E * Math.Atan2(E, b)) - 1;
            double ra = GM / (a * b) * (1 - m - m * e1 * q01 / (6 * q0));
            double rb = GM / (a * a) * (1 + m * e1 * q01 / (3 * q0));
            r = (a * ra * Math.Cos(B) * Math.Cos(B) + b * rb * Math.Sin(B) * Math.Sin(B)) / Math.Sqrt(a * a * Math.Cos(B) * Math.Cos(B) + b * b * Math.Sin(B) * Math.Sin(B));
            return r;
        }
        public static double h(double B, double h)
        {
            double rh = new double();
            double r = OnSurface(B);
            rh = r * (1 - 2 / a * (1 + f + m - 2 * f * Math.Sin(B) * Math.Sin(B)) * h + 3 * h * h / (a * a));
            return rh;
        }


    }


  
}
