using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Physical_Geodesy
{
    class Read_Data
    {
        public static void UGM_read(string path,int num,out double[,] gravity_model_C, out double[,] gravity_model_S)
        {

            gravity_model_C = new double[num+1, num + 1];
            gravity_model_S = new double[num+1, num + 1];
            StreamReader gm_file = new StreamReader(path);
         while(!gm_file.EndOfStream )
            {   
                string strs = gm_file.ReadLine();
                if (strs != ""&& strs != " ") 
                {
                    string[] box = strs.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (box[0] != "gfc"  && box[0] != "gfct")
                    {
                        continue;
                    }
                    else
                    {
                        if (Convert.ToInt32(box[1]) <= num)
                        {
                            gravity_model_C[Convert.ToInt32(box[1]), Convert.ToInt32(box[2])] = Convert.ToDouble(box[3]);
                            gravity_model_S[Convert.ToInt32(box[1]), Convert.ToInt32(box[2])] = Convert.ToDouble(box[4]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                else
                {
                    continue;
                }

            }


        }

        public static void Z_read(string path,int i,out List<double> Z)
        {

            Z = new List<double>();
            StreamReader infile = new StreamReader (path);
            while (!infile.EndOfStream)
            {

                string strs = infile.ReadLine();
                string[] box = strs.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Z.Add(Convert.ToDouble(box[i]));
            }
        }

    }

}
