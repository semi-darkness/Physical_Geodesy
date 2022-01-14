using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Physical_Geodesy
{


    public class calculate
    {
        static double a = Ellipsoid_parameters.a;
        static double f = Ellipsoid_parameters.f;
        static double GM = Ellipsoid_parameters.GM;

        public static double cal_base_one(double B, double L, int n, double[,] p, double[,] C_T, double[,] S_T)
        {
            //计算最里层累加的单个值
            double theta;
            double lemda;
            double r;
            trans.B_L_H2theta_lemda_r(B, L, 0, out theta, out lemda, out r);
            double sum = 0;
            for (int m = 0; m <= n; m++)
            {
                sum = sum + (C_T[n, m] * Math.Cos(m * lemda) + S_T[n, m] * Math.Sin(m * lemda)) * p[n, m];
            }
            return sum;
        }
        public static void cal_base_all(int num, trans.degree B_start, trans.degree B_end, trans.degree L_start, trans.degree L_end, trans.degree Step_length)
        {
            int times = 0;
            //计算最里层累加的累加值，含有（a/r）^n
            string path_sgg = "SGG-UGM-2.gfc";
            string path_eigen = "EIGEN-5C.gfc";

            string path_disturbing_potential = "disturbing_potential.txt";//扰动位
            string path_Geoid_height = "Geoid_height.txt";//大地水准面高
            string path_Gravity_anomaly = "Gravity_anomaly.txt";//重力异常
            string path_Gravity_disturbance = "Gravity_disturbance.txt";//重力扰动

            StreamWriter file_disturbing_potential = new StreamWriter(path_disturbing_potential);
            StreamWriter file_Geoid_height = new StreamWriter(path_Geoid_height);
            StreamWriter file_Gravity_anomaly = new StreamWriter(path_Gravity_anomaly);
            StreamWriter file_Gravity_disturbance = new StreamWriter(path_Gravity_disturbance);

            double Bstart = B_start.To_rad();
            double Lstart = L_start.To_rad();
            double Bend = B_end.To_rad();
            double Lend = L_end.To_rad();
            double step = Step_length.To_rad();

            double[] J = new double[9];
            J[2] = 108263e-8;
            J[4] = -0.00000237091222;
            J[6] = 0.00000000608347;
            J[8] = -0.00000000001427;

            double[,] C;
            double[,] S;
            Read_Data.UGM_read(path_eigen, num, out C, out S);
            //读入重力场数据

            double[,] S_T = S;
            double[,] C_T = C;

            for (int i = 2; i <= 8; i += 2)
            {
                C_T[i, 0] = C[i, 0] + J[i] / Math.Sqrt(Convert.ToDouble(2 * (i)) + 1);
            }

            DateTime beforDT = System.DateTime.Now;

            for (double B = Bstart; B <Bend+step/2; B += step)
            {
                double theta;
                double lemda;
                double r;
                trans.B_L_H2theta_lemda_r(B, 0,0, out theta, out lemda, out r);
                double[,] p;
                lengendre.lengendreInCrossOrder(num, theta, out p);
                for (double L = Lstart; L <Lend + step / 2; L += step)
                {
                    double disturbing_potential = new double();
                    double Geoid_height = new double();
                    double Gravity_anomaly = new double();
                    double Gravity_disturbance = new double();
                    trans.B_L_H2theta_lemda_r(B, L, 0, out theta, out lemda, out r);
                    double T = 0;
                    double GH = 0;
                    double GA = 0;
                    double GD = 0;
                    for (int n = 2; n <= num; n++)
                    {
                      double rcsp= Math.Pow(a / r, n)*cal_base_one(B, L, n, p, C_T, S_T);
                        T = T + rcsp;
                        GH = GH + rcsp;
                        GA = GA + (n - 1) * rcsp;
                        GD = GD + (n + 1) * rcsp;

                    }
                    disturbing_potential= T * GM / (r);
                    file_disturbing_potential.WriteLine(L / Math.PI * 180 + " " + B / Math.PI * 180 + "  " + disturbing_potential);
                   
                    Geoid_height= GH * GM / (r * Normal_Gravity.OnSurface(Math.Abs(B)));//存在疑惑。用theta还是abs(B）
                    file_Geoid_height.WriteLine(L / Math.PI * 180 + " " + B / Math.PI * 180 + "  " + Geoid_height);

                    Gravity_anomaly = GA * GM / (r * r) * 100 * 1000;
                    file_Gravity_anomaly.WriteLine(L / Math.PI * 180 + " " + B / Math.PI * 180 + "  " + Gravity_anomaly);

                    Gravity_disturbance= GD * GM / (r * r) * 100 * 1000;
                    file_Gravity_disturbance.WriteLine(L / Math.PI * 180 + " " + B / Math.PI * 180 + "  " + Gravity_disturbance);

                    
                    double bd =Math.Abs( (Bend - Bstart) / step+1);
                    double ld = Math.Abs((Lend - Lstart) / step+1);
                    times = times + 1;
                    double percent = times / (bd * ld) * 100;
                    
                   // Console.WriteLine("已完成" + percent+"%");
                  //  DateTime afterDT = System.DateTime.Now;
                  //  TimeSpan ts = afterDT.Subtract(beforDT);
                  //  Console.WriteLine("DateTime总共花费{0}s.", ts.TotalMilliseconds / 1000);
                }
            }
            file_disturbing_potential.Close();
            file_Geoid_height.Close();
            file_Gravity_anomaly.Close();
            file_Gravity_disturbance.Close();
        }
        
    }
}
