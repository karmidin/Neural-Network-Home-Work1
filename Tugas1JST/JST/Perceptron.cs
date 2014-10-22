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
      

        string hasilTraining,hasilTesting,printExcel;
        int alpa = 1;
        float threshold = 0.5f;
        float[] wakhir = new float[63];
        int bias = 0;
        

        public Perceptron()
        {
            ListPattern = new List<Pattern>();

        }

        public string Training()
        {
            int countEpoch = 1;
            bool belajar = true;
            hasilTraining += "Hasil Training\n";
            printExcel+="Hasil Training\n";
            while (isLearn())
            {
              
                hasilTraining += "\nEpoch Ke " + countEpoch+"\n\n";
                hasilTraining += "Nilai Bobot Baru\n";
                printExcel += "\nEpoch Ke " + countEpoch+"\n\n";
                printExcel += "Nilai Bobot Baru\n";

                for (int i = 0; i < 63; i++)
                {
                    printExcel = printExcel + "W" + (i + 1) + ",";
                }

                printExcel += "\n";

                foreach (var item in ListPattern)
                {
                   
                    for (int i = 0; i < item.x.Length; i++)
                    {
                        
                        item.net = item.net + (item.x[i] * wakhir[i]);
                    }
                    
                    item.net = item.net + bias;

                    
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
                        bias = bias + (1 * item.t * alpa);
                       
                    }
                    else
                    {
                        belajar = false;
                    }

                   

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
                            
                            hasilTraining += wakhir[i]+"\n";
                            
                        }
                        else
                        {
                            hasilTraining += wakhir[i];
                           
                        }
                        printExcel += wakhir[i]+",";
                    }

                    item.b = bias;
                    printExcel += "\n";
                   
                }

                hasilTraining += getBias();
                hasilTraining += getNet();
                hasilTraining += getY();
                hasilTraining += getTarget();
                
                countEpoch++;
            }
            return hasilTraining;
        }

        public string Testing() {

            hasilTesting += "Hasil Testing\n";
            hasilTesting += "\nNilai Bobot Akhir\n";
            
            printExcel += "\nHasil Testing\n";
            printExcel += "\nNilai Bobot Akhir\n";

            for (int i = 0; i < 63; i++)
            {
                printExcel = printExcel + "W" + (i + 1) + ",";
            }

            printExcel += "\n";

            foreach (var item in ListPattern)
            {
                for (int i = 0; i < item.x.Length; i++)
                {
                    item.net = item.net + (item.x[i] * wakhir[i]);
                }

                item.net = item.net + bias;
               
            }

            

            for (int i = 0; i < 63; i++)
            {
                if ((i + 1) % 63 == 0)
                {

                    hasilTesting += wakhir[i] + "\n";
                    printExcel += wakhir[i] + "\n";
                }
                else
                {

                    hasilTesting += wakhir[i];
                    printExcel += wakhir[i] + ",";
                }
            }

            hasilTesting += getBiasAkhir();
            hasilTesting += getNet();
            hasilTesting += getY();
            hasilTesting += getTarget();
          


            return hasilTesting;
        }

        public void Reset() {
            hasilTesting = "";
            hasilTraining = "";
            printExcel = "";
            bias = 0;
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
            printExcel = printExcel + "\nNilai Net tiap Pattern\n";
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai Net tiap pattern

                net = net + "Pattern " + (i + 1) + " : " + ListPattern[i].net + "\n";
                printExcel = printExcel + "Pattern, " + (i + 1) + " :, " + ListPattern[i].net + "\n";
            }
            return net;
        }
        public string getBias()
        {
            string bias = "";
            bias = bias + "\nNilai Bias Baru tiap Patern\n";
            printExcel = printExcel + "\nNilai Bias tiap Pattern\n";
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai Bias tiap pattern

                bias = bias+ "Pattern " + (i + 1) + " : " + ListPattern[i].b + "\n";
                printExcel = printExcel + "Pattern, " + (i + 1) + " :, " + ListPattern[i].b + "\n";
            }
            return bias;
        }

        public string getBiasAkhir()
        {
            string biasAkhir = "";
            biasAkhir = biasAkhir + "\nNilai Bias Akhir : " + bias.ToString()+"\n" ;
            printExcel = printExcel + "\nBias Akhir," + bias+"\n";
            return biasAkhir;
        }

        public string getY() {
            string y = "";
            y = y + "\nNilai Y tiap Patern\n";
            printExcel = printExcel + "\nNilai Fnet tiap Pattern\n";
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai y tiap pattern

                y = y + "Pattern " + (i + 1) + " : " + ListPattern[i].fnet + "\n";
                printExcel = printExcel + "Pattern, " + (i + 1) + " :, " + ListPattern[i].fnet + "\n";
            }
            return y;
        }

        public string getTarget()
        {
            string y = "";
            y = y + "\nNilai Target tiap Patern\n";
            printExcel = printExcel + "\nNilai Target tiap Pattern\n";
            for (int i = 0; i < 6; i++)
            {
                //Menampilkan nilai target tiap pattern

                y = y + "Pattern " + (i + 1) + " : " + ListPattern[i].t + "\n";
                printExcel = printExcel + "Pattern, " + (i + 1) + " :, " + ListPattern[i].t + "\n";
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
            printExcel = input + printExcel;
            return printExcel;
        }
    }
}
