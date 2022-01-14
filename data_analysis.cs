using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Physical_Geodesy
{
    class data_analysis
    {

        public static double Average(string path)
        {
            List<double> Z;
            Read_Data.Z_read(path, 2, out Z);
            double aver=new double();
            aver = Z.Average();
            return aver;
        }
        public static double Max(string path)
        {
            List<double> Z;
            Read_Data.Z_read(path, 2, out Z);
            double max = new double();
            max = Z.Max();
            return max;
        }
        public static double Min(string path)
        {
            List<double> Z;
            Read_Data.Z_read(path, 2, out Z);
            double min = new double();
            min = Z.Min();
            return min;
        }
        public static double standard_deviation(string path)
        {
            List<double> Z;
            Read_Data.Z_read(path, 2, out Z);
            double dev = 0;
            double aver = Z.Average();
            for(int i=0;i<Z.Count;i++)
            {
                dev = dev + Math.Pow((Z[i] - aver), 2);
            }
            dev = Math.Sqrt(dev / Z.Count);
            return dev;
        }
        public static double RMS(string path)
        {
            List<double> Z;
            Read_Data.Z_read(path, 2, out Z);
            double dev = 0;
           
            for (int i = 0; i < Z.Count; i++)
            {
                dev = dev + Math.Pow(Z[i], 2);
            }
            dev = Math.Sqrt(dev / Z.Count);
            return dev;
        }
        public static void analysis(string path)
        {
            string outpath = "data_analysis_" + path;
            StreamWriter outfile = new StreamWriter(outpath);
            outfile.WriteLine("max: " + Max(path) + "\n");
            outfile.WriteLine("min: " + Min(path) + "\n");
            outfile.WriteLine("Average: " + Average(path) + "\n");
            outfile.WriteLine("standard_deviation: " + standard_deviation(path) + "\n");
            outfile.WriteLine("RMS: " + RMS(path) + "\n");
            outfile.Close();
        }
    }
}
