using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Physical_Geodesy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {   



            trans.degree L_start = new trans.degree(Convert.ToDouble(textBox4.Text), 0, 0);
            trans.degree L_end = new trans.degree(Convert.ToDouble(textBox2.Text), 0, 0);
            trans.degree B_start = new trans.degree(Convert.ToDouble(textBox3.Text), 0, 0);
            trans.degree B_end = new trans.degree(Convert.ToDouble(textBox1.Text), 0, 0);
            trans.degree Step_length = new trans.degree(Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text), Convert.ToDouble(textBox7.Text));
            double Bstart = B_start.To_rad();
            double Lstart = L_start.To_rad();
            double Bend = B_end.To_rad();
            double Lend = L_end.To_rad();
            double step = Step_length.To_rad();

            string  str1 = "起始纬度"+textBox3.Text+"°\n";
            str1 += "终止纬度" + textBox1.Text + "°\n";
            str1 += "起始经度" + textBox4.Text + "°\n";
            str1 += "终止经度" + textBox2.Text + "°\n";
            str1 += "间隔" + textBox5.Text + "°"+ textBox6.Text + "'"+ textBox7.Text + "''"+" \n";
            str1 += "地球重力场模型：eigen5c \n";
            str1 += "椭球参数：GRS1980 \n";
            this.textBox8.Text = str1;
            this.textBox8.Show();

            string[] path = new string[] { "disturbing_potential.txt", "Geoid_height.txt", "Gravity_anomaly.txt", "Gravity_disturbance.txt" };

            double bd = Math.Abs((Bend - Bstart) / step + 1);
            double ld = Math.Abs((Lend - Lstart) / step + 1);
            double timeusing = bd * ld / 64800 * 355 / 60;

            this.textBox9.Text = "计算中\n"+"本次计算在插电情况下大约需要"+timeusing+"分钟";
            this.textBox9.Show();
            DateTime beforDT = System.DateTime.Now;

            calculate.cal_base_all(360, B_start, B_end, L_start, L_end, Step_length);

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("DateTime总共花费{0}s.", ts.TotalMilliseconds / 1000);
         
            GMT_graph.RunCmd(GMT_graph.paint(Bstart * 180 / Math.PI, Bend * 180 / Math.PI, Lstart * 180 / Math.PI, Lend * 180 / Math.PI, path[0]));
            GMT_graph.RunCmd(GMT_graph.paint(Bstart * 180 / Math.PI, Bend * 180 / Math.PI, Lstart * 180 / Math.PI, Lend * 180 / Math.PI, path[1]));
            GMT_graph.RunCmd(GMT_graph.paint(Bstart * 180 / Math.PI, Bend * 180 / Math.PI, Lstart * 180 / Math.PI, Lend * 180 / Math.PI, path[2]));
            GMT_graph.RunCmd(GMT_graph.paint(Bstart * 180 / Math.PI, Bend * 180 / Math.PI, Lstart * 180 / Math.PI, Lend * 180 / Math.PI, path[3]));
            


            string str2 = "文件已保存至  " + path[0] + " " + path[1] + " " + path[2] + " " + path[3];
            this.textBox9.Text = "已完成\n" + "总共耗时" + ts.TotalMilliseconds / 60000 + "分钟"+str2;
            this.textBox9.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] path = new string[] { "disturbing_potential.txt", "Geoid_height.txt", "Gravity_anomaly.txt", "Gravity_disturbance.txt" };
            for(int i=0;i<4;i++)
            data_analysis.analysis(path[i]);
        }
    }
}
