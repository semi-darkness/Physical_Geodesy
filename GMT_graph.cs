using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Physical_Geodesy
{
    class GMT_graph
    {
        public static string paint(double Bstart,double Bend,double Lstart,double Lend,string path)
        {
            List<double> Z;
            List<double> L,B;
            Read_Data.Z_read(path,2, out Z);
            Read_Data.Z_read(path, 0, out L);
            Read_Data.Z_read(path, 1, out B);
            double bstep =Math.Abs(B[0]-B[1]);
            double lstep = Math.Abs(L[0] - L[1]);
            double sstep;
            if (lstep > bstep) sstep = lstep;
            else sstep = bstep;
            double Z_max = Z.Max();
            double Z_min = Z.Min();
            int step = Convert.ToInt32(Z_max - Z_min) / 10;
            string name = path.Split('.')[0];
            string outpath =  name + ".bat";
            StreamWriter outfile = new StreamWriter(outpath);
            string script = "gmt begin "+name+" png\n"+
                            "gmt xyz2grd "+name+".txt -Ggrd1.grd -I"+ sstep + "d -R"+Lstart+"/"+Lend+"/"+Bstart+"/"+Bend+"\n"+
                            "gmt grdsample grd1.grd -Ggrd2.grd -I" +sstep/5+"d\n"+
                            "gmt grd2cpt  grd2.grd -Cjet -Z\n" +
                            "gmt grdimage -R"+Lstart+"/"+Lend+"/"+Bstart+"/"+Bend+" -JCyl_stere/"+(Lstart+Lend)/2+"/"+ (Bstart + Bend) / 2 + "/50c -Ba30 grd2.grd -I+d -B+t"+name+"\n"+
                            "gmt colorbar  -B" + step + "\n" + 
                            "gmt coast -R" + Lstart + "/" + Lend + "/" + Bstart + "/" + Bend + " -JCyl_stere/" + (Lstart + Lend) / 2 + "/" + (Bstart + Bend) / 2 + "/50c -Baf -W1p,black -A5000 \n" +
                            "gmt end show\n"+
                            "del grd1.grd grd2.grd";
            outfile.WriteLine(script);
            outfile.Close();
            return outpath;
        }

        public static string RunCmd(string path)
        {
            string str;
            Process p = new Process();
            p.StartInfo.FileName = path;         //确定程序名
            p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
            p.Start();//启动程序
                      //向cmd窗口写入命令
                      //  p.StandardInput.WriteLine(data);
                      //  p.StandardInput.AutoFlush = true;
                      //获取cmd窗口的输出信息
            str = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return str;//返回bat脚本输出
        }

    }
}
