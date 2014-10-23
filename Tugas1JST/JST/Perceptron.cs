using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JST
{
    class Perceptron
    {
        public List<Pattern> ListPattern = new List<Pattern>();
      

        StringBuilder hasilTraining,hasilTesting,printExcel;
        int alpa = 1;
        float threshold = 0.5f;
        bool awal = true;
        float[] wakhir = new float[63];
        

        public Perceptron()
        {
            ListPattern = new List<Pattern>();
            hasilTesting = new StringBuilder();
            hasilTraining = new StringBuilder();
            printExcel = new StringBuilder();
        }

        public string Training()
        {
            int countEpoch = 1;
            bool belajar = true;
            while (isLearn())
            {
              
                hasilTraining.Append("\nEpoch Ke " + countEpoch+"\n\n");
                hasilTraining.Append("Nilai Wakhir  \n");
                printExcel.Append("\nEpoch Ke " + countEpoch+"\n\n");
                printExcel.Append("Nilai Wakhir  \n");

                foreach (var item in ListPattern)
                {
                    //menghitung nilai net
                    for (int i = 0; i < item.x.Length; i++)
                    {
                        item.net = item.net + (item.x[i] * wakhir[i]);
                    }
                    // menambahkan f.net dengan nilai w bobot bias
                    item.net = item.net + item.b;

                    //menghitung nilai y atau f(net)
                    if (item.net > threshold)
                    {
                        item.fnet = 1;
                    }
                    else if (item.net >= 0 && item.net <= threshold)
                    {
                        item.fnet = 0;
                    }
                    else
                    {
                        item.fnet = -1;
                    }


                    if (item.fnet != item.t)
                    {
                        belajar = true;
                        HitungPerubahanBobot(item, alpa);
                    }
                    else
                    {
                        belajar = false;
                        HitungPerubahanBobot(item, alpa, false);
                    }

                    Console.WriteLine(belajar);
                    
                   
                    for (int i = 0; i < item.x.Length; i++)
                    {
                        if (belajar == true)
                        {
                            //menghitung nilai perubahan bobot tiap inputan
                            item.w[i] = item.x[i] * item.t * alpa;
                            wakhir[i] = item.w[i] + wakhir[i];
                        }
                        else
                        {
                            //jika sesuai output sesuai target, nilai perubahan bobot = 0 karena tidak melakukan pembelajaran
                            item.w[i] = 0;
                        }

                        if ((i + 1) % 63 == 0)
                        {
                            hasilTraining.Append(item.w[i]+"\n");
                        }
                        else
                        {
                            hasilTraining.Append(item.w[i]);
                        }
                        printExcel.Append(wakhir[i]+",");
                    }

                    printExcel.Append("\n");
                }

                hasilTraining.Append(getBias());
                hasilTraining.Append(getNet());
                hasilTraining.Append(getY());
                hasilTraining.Append(getTarget());
                
             
                
                countEpoch++;
            }
            return hasilTraining.ToString();
        }

        public void HitungPerubahanBobot(Pattern item, float alpha/*, StringBuilder sb*/ , bool isLearn = true)
        {
            string delta = "";
            string bobotAkhir = "";
            for (int i = 0; i < item.x.Length; i++)
            {
                float deltaW = item.x[i] * item.t * alpha;
                if (!isLearn)
                    deltaW = 0;
                wakhir[i] = wakhir[i] + deltaW;
                delta += deltaW + ",";
                bobotAkhir += wakhir[i] + ",";
            }
            float deltaWBias = 1 * item.t * alpha;
            if (!isLearn)
                deltaWBias = 0;
            item.b = item.b + deltaWBias; //menghitung perubahan bobot bias

            delta += deltaWBias + ",";
            bobotAkhir += item.b + ",";
            //sb.Append(delta + bobotAkhir);
        }

        public string Testing() {

            hasilTesting.Append("Nilai Wakhir  \n");

            foreach (var item in ListPattern)
            {
                for (int i = 0; i < item.x.Length; i++)
                {

                    if ((i + 1) % 63 == 0)
                    {

                        hasilTesting.Append(wakhir[i] + "\n");
                    }
                    else
                    {

                        hasilTesting.Append(wakhir[i]);
                    }

                    item.net = item.net + (item.x[i] * wakhir[i]);

                }
            }

            printExcel.Append("\nHasil Testing\n");

            hasilTesting.Append(getBias());
            hasilTesting.Append(getNet());
            hasilTesting.Append(getY());
            hasilTesting.Append(getTarget());
          


            return hasilTesting.ToString();
        }

        public void Reset() {
            hasilTesting.Clear();
            hasilTraining.Clear();
            printExcel.Clear();
            ListPattern.Clear();
            awal = true;
            wakhir = new float[63];
        }

        public bool isLearn()
        {
            foreach (var item in ListPattern)
            {
                if (item.t != item.fnet)
                    return true;
            }
            
            return false;

        }

        public string getNet()
        {
            string net = "";
            net = net + "\nNilai Net tiap Patern\n";
            printExcel.Append("\nNilai Net tiap Pattern\n");
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai Net tiap pattern

                net = net + "Pattern " + (i + 1) + " : " + ListPattern[i].net + "\n";
                printExcel.Append("Pattern " + (i + 1) + " : " + ListPattern[i].net + "\n");
            }
            return net;
        }
        public string getBias()
        {
            string bias = "";
            bias = bias + "\nNilai Bias tiap Patern\n";
            printExcel.Append("\nNilai Bias tiap Pattern\n");
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai Bias tiap pattern

                bias = bias+ "Pattern " + (i + 1) + " : " + ListPattern[i].b + "\n";
                printExcel.Append("Pattern " + (i + 1) + " : " + ListPattern[i].b + "\n");
            }
            return bias;
        }

        public string getY() {
            string y = "";
            y = y + "\nNilai Y tiap Patern\n";
            printExcel.Append("\nNilai Fnet tiap Pattern\n");
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai y tiap pattern

                y = y + "Pattern " + (i + 1) + " : " + ListPattern[i].fnet + "\n";
                printExcel.Append("Pattern " + (i + 1) + " : " + ListPattern[i].fnet + "\n");
            }
            return y;
        }

        public string getTarget()
        {
            string y = "";
            y = y + "\nNilai Target tiap Patern\n";
            printExcel.Append(printExcel + "\nNilai Target tiap Pattern\n");
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai target tiap pattern

                y = y + "Pattern " + (i + 1) + " : " + ListPattern[i].t + "\n";
                printExcel.Append(printExcel + "Pattern " + (i + 1) + " : " + ListPattern[i].t + "\n");
            }
            return y;
        }

        public string getPrintToExcel() {
            string input="";
            for (int i = 0; i < 63; i++)
            {
                input = input + "X"+(i+1) + ",";
            }
            input += "\n";
            foreach (var item in ListPattern)
            {

                for (int i = 0; i < item.x.Length; i++)
                {
                    input = input + item.x[i]+",";
                }
                input += "\n";
            }
            string print = printExcel.ToString();
            printExcel.Clear();
            printExcel.Append(input + print);
            return printExcel.ToString();
        }

       

    }
}
