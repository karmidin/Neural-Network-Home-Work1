using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JST
{
    class Pattern
    {
        public int t;                       //t adalah target isinya sesuai yg ditetapkan tiap pattern
        public int fnet;                    //f(net)
        public float b, net;                //b adalah bias pada tiap pattern, net adalah hasil dari (wakhir * x) pada tiap pattern
        public float[] w, wakhir;           // wakhir adalah hasil penjumlahan Δw
                                            //w adalah perubahan bobot merupakan hasil x * alpha * target 
        public float[] x;                   //x adalah input tiap pattern

        public Pattern(int t, string pattern_filename)
        {
            w = new float[63];
            wakhir = new float[63];
            x = new float[63];
            this.t = t;
            b = 0;
            ReadPattern(pattern_filename);
        }

        public void ReadPattern(string filename)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        char[] pattern = sr.ReadLine().ToArray();
                        foreach (char item in pattern)
                        {
                            if (item == '-')
                            {
                                x[i] = -1;
                            }
                            else
                            {
                                x[i] = 1;
                            }
                            i++;
                        }
                    }
                }
            }
            catch { 
            }
        }
    }
}
