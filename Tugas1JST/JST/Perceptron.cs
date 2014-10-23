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
        float bias = 0;
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
            printExcel.Append(SetHeaderTable());
            while (isLearn())
            {
              
                hasilTraining.Append("\nEpoch Ke " + countEpoch+"\n\n");
                hasilTraining.Append("Nilai Wakhir  \n");
                printExcel.Append("\nEpoch Ke " + countEpoch+"\n");
                printExcel.Append(PrintBobotAwal() + "\n");
                //printExcel.Append("Nilai Wakhir  \n");

                foreach (var item in ListPattern)
                {
                    //menghitung nilai net
                    item.net = 0;
                    for (int i = 0; i < item.x.Length; i++)
                    {
                        item.net = item.net + (item.x[i] * wakhir[i]);
                        printExcel.Append(item.x[i] + ",");
                    }
                    // menambahkan f.net dengan nilai w bobot bias
                    item.net = item.net + bias;
                    printExcel.Append(1 + "," + item.t + "," + item.net + ",");

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
                    printExcel.Append(item.fnet + ",");

                    if (item.fnet != item.t)
                    {
                        HitungPerubahanBobot(item, alpa);
                    }
                    else
                    {
                        HitungPerubahanBobot(item, alpa, false);
                    }
                    printExcel.Append("\n");
                }
                countEpoch++;
            }
            return hasilTraining.ToString();
        }

        public void HitungPerubahanBobot(Pattern item, float alpha, bool isLearn = true)
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
            bias = bias + deltaWBias; //menghitung perubahan bobot bias

            delta += deltaWBias + ",";
            bobotAkhir += item.b + ",";
            printExcel.Append(delta + bobotAkhir);
        }

        public string SetHeaderTable()
        {
            string item = "";
            string deltaW = "";
            string newW = "";
            for (int i = 1; i <= 63; i++)
            {
                item += "x" + i + ",";
                deltaW += "dw" + i + ",";
                newW += "w" + i + ",";
            }
            item += 1 + ",";
            deltaW += "db,";
            newW += "b,";
            return (item + "t,net,y=f(net)," + deltaW + newW);
        }

        public string PrintBobotAwal()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 63; j++)
                {
                    sb.Append(",");
                }
            }
            sb.Append(",,,,,");
            for(int k = 0; k < wakhir.Length; k++)
            {
                sb.Append(wakhir[k] + ",");
            }
            sb.Append(bias);
            return sb.ToString();
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
            //string input="";
            //for (int i = 0; i < 63; i++)
            //{
            //    input = input + "X"+(i+1) + ",";
            //}
            //input += "\n";
            //foreach (var item in ListPattern)
            //{

            //    for (int i = 0; i < item.x.Length; i++)
            //    {
            //        input = input + item.x[i]+",";
            //    }
            //    input += "\n";
            //}
            //string print = printExcel.ToString();
            //printExcel.Clear();
            //printExcel.Append(input + print);
            return printExcel.ToString();
        }

       

    }
}
