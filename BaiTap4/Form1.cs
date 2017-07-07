using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using System.Xml;

namespace TTSDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text.ToString();
            string[] unicodeText = new string[] {"ấ", "ầ", "ẩ", "ẫ", "ậ", "â", "á", "à", "ả", "ã", "ạ", "ắ", "ằ", "ẳ", "ẵ", "ặ", "ă", "ú", 
                "ù", "ủ", "ũ", "ụ", "ứ", "ừ", "ử", "ữ", "ự", "ư", "ế", "ề", "ể", "ễ", "ệ", "ê", "é", "è","ẻ", "ẽ",
                "ẹ", "ố", "ồ", "ổ", "ỗ", "ộ", "ô", "ó", "ò", "ỏ", "õ", "ọ", "ớ", "ờ", "ở", "ỡ", "ợ", "ơ", 
                "í", "ì", "ỉ", "ĩ", "ị", "ý", "ỳ", "ỷ", "ỹ", "ỵ", "đ", "Ấ", "Ầ", "Ẩ", "Ẫ", "Ậ", "Â", "Á", "À", "Ả", 
                "Ã", "Ạ", "Ắ", "Ằ", "Ẳ", "Ẵ", "Ặ", "Ă", "Ú", "Ù", "Ủ", "Ũ", "Ụ", "Ứ", "Ừ", "Ử", "Ữ", "Ự", "Ư", "Ế",
                "Ề", "Ể", "Ễ", "Ệ", "Ê", "É", "È", "Ẻ", "Ẽ", "Ẹ", "Ố", "Ồ", "Ổ", "Ỗ", "Ộ", "Ô", "Ó", "Ò", "Ỏ", "Õ",
                "Ọ", "Ớ", "Ờ", "Ở", "Ỡ", "Ợ", "Ơ", "Í", "Ì", "Ỉ", "Ĩ", "Ị", "Ý", "Ỳ", "Ỷ", "Ỹ", "Ỵ", "Đ"};
            string[] telexText = new string[]{"aas", "aaf", "aar", "aax", "aaj", "aa", "as", "af", "ar", "ax", "aj", "aws", "awf", "awr",
                "awx", "awj", "aw", "us", "uf", "ur", "ux", "uj", "uws", "uwf", "uwr", "uwx", "uwj", "uw", "ees", 
                "eef","eer", "eex", "eej", "ee", "es", "ef", "er", "ex", "ej", "oos", "oof", "oor", "oox", "ooj",
                "oo", "os", "of", "or", "ox", "oj", "ows", "owf", "owr", "owx", "owj", "ow", "is", "if", "ir", "ix", 
                "ij", "ys", "yf", "yr", "yx", "yj", "dd", "AAS", "AAF", "AAR", "AAX", "AAJ", "AA", "AS", "AF", "AR",
                "AX", "AJ", "AWS", "AWF", "AWR", "AWX", "AWJ", "AW", "US", "UF", "UR", "UX", "UJ", "UWS", "UWF", "UWR",
                "UWX", "UWJ", "UW", "EES", "EEF", "EER", "EEX", "EEJ", "EE", "ES", "EF", "ER", "EX", "EJ", "OOS", 
                "OOF", "OOR", "OOX", "OOJ", "OO", "OS", "OF", "OR", "OX", "OJ", "OWS", "OWF", "OWR", "OWX", "OWJ", 
                "OW", "IS", "IF", "IR", "IX", "IJ", "YS", "YF", "YR", "YX", "YJ", "DD"};
            string[] list = text.Split(' ');
            string str = "";
            for (int i = 0; i < list.Length; i++){

                string x = list[i];
                for(int j=0; j<x.Length; j++) {

                    string temp = x[j].ToString();
                    bool flag = true;
                    for(int k=0; k<134; k++) {

                        if (temp.Equals(unicodeText[k])){

                            str += telexText[k];
                            flag = false;
                        }
                    }
                    if (flag)
                        str += temp;
                }
                str += " ";
            }
            textBox1.Text = str;

            var path = (System.IO.Directory.GetParent(Environment.CurrentDirectory)).Parent.FullName;
            XmlDocument xml = new XmlDocument();
            xml.Load(path + "/myXmFile.xml");

            XmlNodeList xnList = xml.GetElementsByTagName("word");
            ArrayList arr = new ArrayList();
            string[] ds = str.Trim().Split(' ');
            for (int i = 0; i < ds.Length; i++){

                bool isAdd = true;
                foreach (XmlNode xn in xnList){

                    string amtiet = xn["amtiet"].InnerText;
                    string ngucanhtrai = xn["ngucanhtrai"].InnerText;
                    string ngucanhphai = xn["ngucanhphai"].InnerText;

                    if (ds[i].ToUpper().Equals(amtiet) && (i != 0) && (i != ds.Length - 1)
                        && ds[i - 1].ToUpper().Equals(ngucanhtrai) && ds[i + 1].ToUpper().Equals(ngucanhphai)){

                        string tenfile = xn["tenfile"].InnerText;
                        string vitribatdau = xn["vitribatdau"].InnerText;
                        string vitriketthuc = xn["vitriketthuc"].InnerText;
                        Word word = new Word(tenfile, amtiet, vitribatdau, vitriketthuc, ngucanhtrai, ngucanhphai);
                        arr.Add(word);
                        isAdd = false;
                        break;
                    }
                }

                if (isAdd) { 

                    foreach (XmlNode xn in xnList){

                        string amtiet = xn["amtiet"].InnerText;
                        string ngucanhtrai = xn["ngucanhtrai"].InnerText;
                        string ngucanhphai = xn["ngucanhphai"].InnerText;
                        if ((i != 0) && ds[i].ToUpper().Equals(amtiet) && ds[i - 1].ToUpper().Equals(ngucanhtrai)){

                            string tenfile = xn["tenfile"].InnerText;
                            string vitribatdau = xn["vitribatdau"].InnerText;
                            string vitriketthuc = xn["vitriketthuc"].InnerText;
                            Word word = new Word(tenfile, amtiet, vitribatdau, vitriketthuc, ngucanhtrai, ngucanhphai);
                            arr.Add(word);
                            break;
                        }
                    }
                }

                if (isAdd){

                    foreach (XmlNode xn in xnList){

                        string amtiet = xn["amtiet"].InnerText;
                        string ngucanhtrai = xn["ngucanhtrai"].InnerText;
                        string ngucanhphai = xn["ngucanhphai"].InnerText;
                        if ((i != ds.Length - 1) && ds[i].ToUpper().Equals(amtiet) && ds[i + 1].ToUpper().Equals(ngucanhphai)){

                            string tenfile = xn["tenfile"].InnerText;
                            string vitribatdau = xn["vitribatdau"].InnerText;
                            string vitriketthuc = xn["vitriketthuc"].InnerText;
                            Word word = new Word(tenfile, amtiet, vitribatdau, vitriketthuc, ngucanhtrai, ngucanhphai);
                            arr.Add(word);
                            isAdd = false;
                            break;
                        }
                    }
                }

                if (isAdd) {
                    Word word = new Word("NULL", "NULL", "NULL", "NULL", "NULL", "NULL");
                    arr.Add(word);
                }
            }

            foreach (Word word in arr) {

                if (!word.tenfile.Equals("NULL")){
                    callSpeak(word);
                }
                else {
                   // speak word unknown
                }
            }
        }

        private void callSpeak(Word word)
        {
            var path = (System.IO.Directory.GetParent(Environment.CurrentDirectory)).Parent.FullName;
            path = path + "/wav/" + word.tenfile;
            var wo = new NAudio.Wave.WaveOutEvent();
            int vitribatdau = Convert.ToInt32(word.vitribatdau);
            int vitriketthuc = Convert.ToInt32(word.vitriketthuc);
            var reader = new StartEndLoopReader(new WaveFileReader(path), new TimeSpan(0, 0, vitribatdau), new TimeSpan(0, 0, vitriketthuc));
            wo.Init(reader);
            wo.Play();
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            List<NguCanh> listNguCanh = TaoNguCanh();
            var path = (System.IO.Directory.GetParent(Environment.CurrentDirectory)).Parent.FullName;
            string[] lines = System.IO.File.ReadAllLines(path + "/ketqua.txt");
            using (XmlWriter writer = XmlWriter.Create(path + "/myXmFile.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("audios");
                int number = 0;
                string filename = "";
                foreach (string line in lines)
                {
                    if (line.Contains("VIVOSSPK"))
                    {
                        string line1 = line.Substring(0, line.Length - 5);
                        line1 = line1.Remove(0, 3);
                        filename = line1 + ".wav";
                    }
                    else if (!line.Equals("#!MLF!#") && !line.Equals("."))
                    {
                        writer.WriteStartElement("word");
                        string[] temp = line.Split(' ');
                        number++;
                        writer.WriteAttributeString("id", number.ToString());
                        writer.WriteElementString("tenfile", filename);
                        writer.WriteElementString("amtiet", temp[2]);
                        double x1 = Convert.ToDouble(temp[0]) / 1000;
                        double x2 = Convert.ToDouble(temp[1]) / 1000;
                        writer.WriteElementString("vitribatdau", x1.ToString());
                        writer.WriteElementString("vitriketthuc", x2.ToString());

                        int vt = number - 1;
                        if (vt == 0){
                            // xet phan tu dau tien
                            if ((listNguCanh[vt].tenfile).Equals(listNguCanh[1].tenfile)){
                                writer.WriteElementString("ngucanhtrai", "NULL");
                                writer.WriteElementString("ngucanhphai", listNguCanh[1].amtiet);
                            }
                            else{
                                writer.WriteElementString("ngucanhtrai", "NULL");
                                writer.WriteElementString("ngucanhphai", "NULL");
                            }
                        }
                        else if (vt == (listNguCanh.Count - 1)){
                            // xet phan tu cuoi
                            if ((listNguCanh[vt].tenfile).Equals(listNguCanh[listNguCanh.Count - 2].tenfile)){
                                writer.WriteElementString("ngucanhtrai", listNguCanh[listNguCanh.Count - 2].amtiet);
                                writer.WriteElementString("ngucanhphai", "NULL");
                            }
                            else{
                                writer.WriteElementString("ngucanhtrai", "NULL");
                                writer.WriteElementString("ngucanhphai", "NULL");
                            }
                        }
                        else {

                            if ((listNguCanh[vt].tenfile).Equals(listNguCanh[vt - 1].tenfile))
                                writer.WriteElementString("ngucanhtrai", listNguCanh[vt - 1].amtiet);
                            else
                                writer.WriteElementString("ngucanhtrai", "NULL");

                            if ((listNguCanh[vt].tenfile).Equals(listNguCanh[vt + 1].tenfile)){
                                writer.WriteElementString("ngucanhphai", listNguCanh[vt + 1].amtiet);
                            }
                            else{
                                writer.WriteElementString("ngucanhphai", "NULL");
                            }
                        }
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        private List<NguCanh> TaoNguCanh()
        {
            var path = (System.IO.Directory.GetParent(Environment.CurrentDirectory)).Parent.FullName;
            string[] lines = System.IO.File.ReadAllLines(path + "/ketqua.txt");
            List<NguCanh> list = new List<NguCanh>();
            string filename = "";

            foreach (string line in lines)
            {
                if (line.Contains("VIVOSSPK"))
                {
                    string line1 = line.Substring(0, line.Length - 5);
                    line1 = line1.Remove(0, 3);
                    filename = line1 + ".wav";
                }
                else if (!line.Equals("#!MLF!#") && !line.Equals("."))
                {
                    string[] temp = line.Split(' ');
                    NguCanh ngucanh = new NguCanh(temp[2], filename);
                    list.Add(ngucanh);
                }
            }
            return list;
        }
    }
}
