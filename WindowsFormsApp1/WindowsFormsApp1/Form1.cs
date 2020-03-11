using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        //Kategori Ekle -> ekle butonuna basılınca yollancak..
        TextBox kategoriGir = null;
        TextBox isimAl = null;
        TextBox markaAl = null;
        TextBox modelAl = null;
        TextBox stokSayısıAl = null;
        TextBox maliyetAl = null;
        TextBox satışFiyatıAl = null;
        TextBox özellik1 = null;
        TextBox özellik2 = null;
        TextBox özellik3 = null;
        TextBox özellik4 = null;
        TextBox özellik5 = null;
        TextBox özellik6 = null;
        //Kaydol
        TextBox kullanıcıAdı;
        TextBox şifre;
        TextBox yaş;
        TextBox maaş;
        TextBox cinsiyet;
        TextBox şehir;
        //GİRİŞ YAP
        TextBox şifreGir;
        TextBox ID;
        TextBox aramaCubugu;
        //GİRİŞ YAPILDIĞI AN ATANIYOR
        string mevcutKullanıcı;


        List<List<String>> arananİçKelime;


        Hashtable kategoriler;
        Hashtable urunAcıklaması;
        List<string> sıralıKategoriler = new List<string>();


        List<List<String>> bulunanÜrünler = new List<List<string>>();
        List<List<String>> tümÜrünListesi = new List<List<string>>();
        List<List<String>> tümÜrünlerVeOzellikleri = null;
        string silinenUrunKategorisi = null;
        public Form1()
        {
            InitializeComponent();

            kategoriler = new Hashtable();







            StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/market.txt");
            string nextline = reader.ReadLine();
            while (nextline != null)
            {
                if (!nextline.Contains(" "))
                {
                    sıralıKategoriler.Add(nextline);
                    Tree agac = new Tree();
                    agac = agacHazırla(nextline);
                    kategoriler.Add(nextline, agac);
                }
                nextline = reader.ReadLine();
            }
            reader.Close();
            urunAcıklamasıHashTableHazırla();

            textBox1.ForeColor = System.Drawing.Color.Gray;
            textBox1.Text = "Ne aramıştınız?                                                                                                                 🔍";


        }


        //BİZİM YAZDIĞIMIZ METOTLAR

        public void urunAcıklamasıHashTableHazırla()
        {
            urunAcıklaması = new Hashtable();

            try
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    List<TreeNode> HashValue;
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.Contains(nextline))
                        {
                            nextline = sr.ReadLine();
                        }

                        HashValue = new List<TreeNode>();
                        string[] urunAcıklamasıİcerenListe = nextline.Split(' ');

                        for (int i = 0; i < urunAcıklamasıİcerenListe.Length; i++)
                        {
                            if (urunAcıklaması.ContainsKey(urunAcıklamasıİcerenListe[i]))
                            {
                                continue;
                            }
                            HashValue = new List<TreeNode>();
                            urunAcıklaması.Add(urunAcıklamasıİcerenListe[i], HashValue);

                        }
                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            Hashtable urunAcıklamasıKopya = new Hashtable(urunAcıklaması);
            List<TreeNode> treeNodeList = new List<TreeNode>();
            foreach (Tree agac in kategoriler.Values)
            {
                agac.toStack(agac.getRoot(), treeNodeList);
            }

            foreach (string aranankelime in urunAcıklamasıKopya.Keys)
            {
                List<List<String>> kelimeninGectigiTreeNode = new List<List<String>>();
                TreeNode listIcındeki = new TreeNode();
                List<string> NodeIcındeki = new List<string>();

                for (int i = 0; i < treeNodeList.Count; i++)
                {
                    listIcındeki = treeNodeList[i];
                    for (int k = 0; k < listIcındeki.data.Count; k++)
                    {
                        NodeIcındeki = listIcındeki.data[k];
                        if (NodeIcındeki.Contains(aranankelime))
                        {
                            kelimeninGectigiTreeNode.Add(listIcındeki.data[k]);
                        }
                    }
                }
                urunAcıklaması[aranankelime] = kelimeninGectigiTreeNode;
            }

        }  //DONE!

        private Tree agacHazırla(string kategori)
        {


            int counter;
            Tree temp = new Tree();
            List<List<String>> dışListe = null;

            counter = 0;

            try
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextLine = "";
                    string txtKategorisi = "";
                    while (kategori != txtKategorisi)//başlık gibi olan kategoriler eşleşmediği sürece sonraki line a gidecek
                    {
                        txtKategorisi = sr.ReadLine();
                    }
                    while (kategori == txtKategorisi)
                    {  //kategoriler eşitlenince
                        dışListe = new List<List<String>>(); // bu dizüstü mesela bunun içine asuslar falan atılcak daha sonra bu bilgisayar ağacına atılcak sonra değişcek tablet olcak tablet içlistesi buna atılcak bu bilgisayar ağacına atılcak
                        List<String> içListe = null;
                        counter++;
                        if (counter == 1)
                        {
                            nextLine = sr.ReadLine();
                        }

                        string[] wordByWord = nextLine.Split(' ');
                        string kategorizeEdenKey = wordByWord[0];

                        while (kategorizeEdenKey == wordByWord[0])
                        { //dışlisteyi kategorize etmeye yarıyo işte eğer bidahaki satırda dizüstü varsa demek ki aynı dış listenin içine atılacak bir iç liste yapılması lazım

                            içListe = new List<String>();

                            for (int i = 0; i < wordByWord.Length; i++)
                            {
                                içListe.Add(wordByWord[i]);
                            }

                            List<String> içListeKopya = new List<String>();
                            for (int i = 0; i < içListe.Count; i++)
                            {
                                içListeKopya.Add(içListe[i]);
                            }

                            dışListe.Add(içListeKopya);
                            içListe.Clear();
                            nextLine = sr.ReadLine();
                            if (nextLine == null)
                            {
                                break;
                            }
                            if (!nextLine.Contains(" "))
                            {
                                txtKategorisi = nextLine;
                                break;
                            }

                            kategorizeEdenKey = wordByWord[0];
                            wordByWord = nextLine.Split(' ');
                        }
                        temp.insert(dışListe); // ağaca dış listeyi atıyo

                    }
                    sr.Close();
                }

                return temp;
            }

            catch (Exception e)
            {

                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return temp;
            }
            return temp;

        } //rpogram başlatıldığında hashtable içindeki ağaçları dosyadan dolduruyor



        public Heap heapHazırla(Hashtable kategoriler, string alınanKategori)
        {
            Heap heap = null;
            //alınan kategori textboxtan çekilip metoda parametre yollancak
            //foreachle kategorilerin keylerini dolaşıp alınankategori kategorilerin keyine eşit olunca çıkıp işlemleri yapcak
            foreach (string kategori in kategoriler.Keys)
            {

                if (alınanKategori == kategori)
                {
                    Tree agac = (Tree)kategoriler[kategori];
                    List<TreeNode> dugumler = new List<TreeNode>();
                    agac.toStack(agac.getRoot(), dugumler);
                    int maxSize = 0;
                    for (int i = 0; i < dugumler.Count; i++)
                    {
                        List<List<String>> heapKategorisi = dugumler[i].data;
                        maxSize += heapKategorisi.Count;

                    }

                    heap = new Heap(maxSize);
                    for (int i = 0; i < dugumler.Count; i++)
                    {
                        List<List<String>> heapKategorisi = dugumler[i].data;
                        for (int j = 0; j < heapKategorisi.Count; j++)
                        {
                            List<string> içListe = heapKategorisi[j];
                            heap.insert(içListe);
                        }

                    }



                }
            }
            return heap;
        } //heap doldurma metodu

        public void yeniKategoriEkle(List<string> yeniKategori)
        {
            string ürününKategorisi = yeniKategori[0];
            if (kategoriler.ContainsKey(yeniKategori[0]))
            {
                tümÜrünListesi.Clear();
                //Zaten kategori önceden varsa
                bool öncedenVarMı = false;
                Tree temp = (Tree)kategoriler[ürününKategorisi];
                List<TreeNode> tempData = new List<TreeNode>();
                temp.toStack(temp.getRoot(), tempData);
                for (int i = 0; i < tempData.Count; i++)
                {
                    List<List<String>> templistListString = tempData[i].data;
                    for (int k = 0; k < templistListString.Count; k++)
                    {
                        List<string> templistString = templistListString[k];
                        if (templistString[0] == yeniKategori[1]) //Ürün Genel Adı Önceden Varsa Buradan devam ediyor. 
                        {
                            öncedenVarMı = true;
                            List<string> yeniÜrün = new List<string>();
                            for (int z = 1; z < yeniKategori.Count; z++)
                            {
                                yeniÜrün.Add(yeniKategori[z]);
                            }
                            tempData[i].data.Add(yeniÜrün);
                            tümÜrünListesi.Clear();
                            //DOSYAYA YAZDIRMA İŞLEMİ
                            try
                            {
                                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                                {
                                    string nextline = sr.ReadLine();
                                    while (nextline != null)
                                    {

                                        List<string> sonrakiÜrün = new List<string>();
                                        sonrakiÜrün.Add(nextline);

                                        for (int a = 0; a < sonrakiÜrün.Count; a++)
                                        {
                                            tümÜrünListesi.Add(sonrakiÜrün);

                                        }
                                        nextline = sr.ReadLine();
                                    }


                                }
                            }
                            catch (Exception ee)
                            {
                                Console.WriteLine("The file could not be read:");
                                Console.WriteLine(ee.Message);
                            }//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
                            int eklenecekİndex = 0;
                            for (int z = 0; z < tümÜrünListesi.Count; z++)
                            {
                                List<string> geçiciListe = tümÜrünListesi[z];
                                if (kategoriler.ContainsKey(geçiciListe[0]))
                                {
                                    z++;
                                }
                                List<string> satır = tümÜrünListesi[z];
                                string[] satırArray = new string[12];
                                satırArray = satır[0].Split(' ');
                                if (satırArray[0] == yeniÜrün[0])
                                {
                                    eklenecekİndex = z;
                                    break;
                                }

                            }


                            tümÜrünListesi.Insert(eklenecekİndex, yeniÜrün);
                            using (StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/market.txt")) //dosyaya tüm ürün listesini attığımız veri yapısından tekrar yazdırıyor..
                            {
                                int count = 0;
                                foreach (List<String> yaz in tümÜrünListesi)
                                {
                                    if (count != 0)
                                    {
                                        writer.WriteLine();
                                    }
                                    count++;
                                    for (int j = 0; j < yaz.Count; j++)
                                    {
                                        if (j == yaz.Count - 1)
                                        {
                                            writer.Write(yaz[j]);
                                        }
                                        else
                                        {
                                            writer.Write(yaz[j] + " ");
                                        }
                                    }

                                }
                                writer.Close();
                            }


                            break;

                        }
                    }

                }
                if (öncedenVarMı == false) //Ürün Genel Adı Önceden Yoksa Burdan devam edicek.
                {
                    List<List<string>> yeniÜrünTreeNodeData = new List<List<string>>();
                    List<string> yeniÜrün = new List<string>();
                    for (int z = 1; z < yeniKategori.Count; z++)
                    {
                        yeniÜrün.Add(yeniKategori[z]);
                    }
                    yeniÜrünTreeNodeData.Add(yeniÜrün);
                    temp.insert(yeniÜrünTreeNodeData);
                    tümÜrünListesi.Clear();
                    //DOSYAYA YAZDIRMA İŞLEMİ
                    try
                    {
                        using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                        {
                            string nextline = sr.ReadLine();
                            while (nextline != null)
                            {

                                List<string> sonrakiÜrün = new List<string>();
                                sonrakiÜrün.Add(nextline);

                                for (int a = 0; a < sonrakiÜrün.Count; a++)
                                {
                                    tümÜrünListesi.Add(sonrakiÜrün);

                                }
                                nextline = sr.ReadLine();
                            }


                        }
                    }
                    catch (Exception ee)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(ee.Message);
                    }//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
                    int eklenecekİndex = 0;
                    for (int z = 0; z < tümÜrünListesi.Count; z++)
                    {
                        List<string> geçiciListe = tümÜrünListesi[z];
                        if (ürününKategorisi == geçiciListe[0])
                        {
                            z++;
                            eklenecekİndex = z;
                            break;

                        }



                    }

                    tümÜrünListesi.Insert(eklenecekİndex, yeniÜrün);

                    using (StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/market.txt")) //dosyaya tüm ürün listesini attığımız veri yapısından tekrar yazdırıyor..
                    {
                        int count = 0;
                        foreach (List<String> yaz in tümÜrünListesi)
                        {
                            if (count != 0)
                            {
                                writer.WriteLine();
                            }
                            count++;
                            for (int j = 0; j < yaz.Count; j++)
                            {
                                if (j == yaz.Count - 1)
                                {
                                    writer.Write(yaz[j]);
                                }
                                else
                                {
                                    writer.Write(yaz[j] + " ");
                                }
                            }

                        }
                        writer.Close();
                    }

                }


            }
            else //YENİ KATEGORİ EKLİYOR.
            {
                tümÜrünListesi.Clear();
                Tree yeniKategoriAgacı = new Tree();
                List<List<string>> yeniÜrünTreeNodeData = new List<List<string>>();
                List<string> yeniÜrün = new List<string>();
                for (int z = 1; z < yeniKategori.Count; z++)
                {
                    yeniÜrün.Add(yeniKategori[z]);
                }
                yeniÜrünTreeNodeData.Add(yeniÜrün);
                yeniKategoriAgacı.insert(yeniÜrünTreeNodeData);
                kategoriler.Add(ürününKategorisi, yeniKategoriAgacı);
                try
                {
                    using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                    {
                        string nextline = sr.ReadLine();
                        while (nextline != null)
                        {

                            List<string> sonrakiÜrün = new List<string>();
                            sonrakiÜrün.Add(nextline);

                            for (int a = 0; a < sonrakiÜrün.Count; a++)
                            {
                                tümÜrünListesi.Add(sonrakiÜrün);

                            }
                            nextline = sr.ReadLine();
                        }

                        sr.Close();
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(ee.Message);
                }//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.



                List<string> ürünkategoriList = new List<string>();
                ürünkategoriList.Add(ürününKategorisi);
                sıralıKategoriler.Add(ürününKategorisi);
                tümÜrünListesi.Add(ürünkategoriList);
                tümÜrünListesi.Add(yeniÜrün);
                using (StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/market.txt")) //dosyaya tüm ürün listesini attığımız veri yapısından tekrar yazdırıyor..
                {
                    int count = 0;
                    foreach (List<String> yaz in tümÜrünListesi)
                    {
                        if (count != 0)
                        {
                            writer.WriteLine();
                        }
                        count++;
                        for (int j = 0; j < yaz.Count; j++)
                        {
                            if (j == yaz.Count - 1)
                            {
                                writer.Write(yaz[j]);
                            }
                            else
                            {
                                writer.Write(yaz[j] + " ");
                            }

                        }

                    }
                    writer.Close();
                }

            }


            urunAcıklamasıHashTableHazırla();
        } //yeni Kategoride ürün ekleme metodu

        public void dosyayaYaz(List<string> kullanıcıOzellikleri)
        {
            string dosyaYolu = "C:/Users/ASUS/Desktop/kullanıcı.txt";
            StreamReader sr = new StreamReader(dosyaYolu);
            bool icindeVarMı = false;
            string nextline = sr.ReadLine();

            if (nextline != null)
            {

                while (nextline != null)
                {
                    string[] satırOzellikleri = nextline.Split(' ');
                    if (satırOzellikleri[0].Equals(kullanıcıOzellikleri[0]))
                    {
                        icindeVarMı = true;
                        break;
                    }
                    nextline = sr.ReadLine();
                }
            }
            sr.Close();
            if (icindeVarMı)
            {
                Form2 hata = new Form2();
                hata.Height = 125;
                Label hataYazısı = new Label();
                hataYazısı.Text = "!! KULLANICI ADI ZATEN VAR !!";
                hataYazısı.Font = new Font(hataYazısı.Font.FontFamily, hataYazısı.Font.Size + 5f, hataYazısı.Font.Style);
                hataYazısı.Width = 300;
                hata.Controls.Add(hataYazısı);
                hata.Show();

            }
            else
            {
                StreamReader reader = new StreamReader(dosyaYolu);
                string line = reader.ReadLine();
                List<string> txtFile = new List<string>();

                while (line != null)
                {
                    txtFile.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();

                FileStream fs = new FileStream(dosyaYolu, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                for (int i = 0; i < txtFile.Count; i++)
                {
                    sw.WriteLine(txtFile[i]);
                }
                for (int i = 0; i < kullanıcıOzellikleri.Count; i++)
                {
                    if (i == kullanıcıOzellikleri.Count - 1)
                    {
                        sw.Write(kullanıcıOzellikleri[i]);
                    }
                    else
                    {
                        sw.Write(kullanıcıOzellikleri[i] + " ");
                    }
                }
                sr.Close();
                sw.Close();
                fs.Close();
                Form2 kayıtolundu = new Form2();
                Label tamamlandı = new Label();
                tamamlandı.Text = "Kaydınız Başarıyla Gerçekleştirildi";
                tamamlandı.Width = 300;
                kayıtolundu.Controls.Add(tamamlandı);
                kayıtolundu.Show();

            }


        } //yeni kayıtları dosyaya yazdırıyor

        public void hashTabledaUrunBilgisiDegistir(List<List<String>> tumUrunlerVeOzellikleri)
        {
            int count = 0;

            for (int j = 0; j < sıralıKategoriler.Count; j++)
            {
                string kategori = sıralıKategoriler[j];
                List<TreeNode> treeNodes = new List<TreeNode>();
                Tree agac = (Tree)kategoriler[kategori];
                agac.toStack(agac.getRoot(), treeNodes);
                for (int i = 0; i < treeNodes.Count; i++)
                {

                    TreeNode geçiciNode = treeNodes[i];

                    for (int k = 0; k < geçiciNode.data.Count; k++)
                    {

                        List<string> içListe = geçiciNode.data[k];
                        List<string> içListeYerine = tumUrunlerVeOzellikleri[count];
                        count++;
                        for (int z = 0; z < içListeYerine.Count; z++)
                        {
                            içListe[z + 1] = içListeYerine[z];
                        }
                    }
                }
            }
        } //ürün bilgisi değiştir metodu için..

        public void hashtabledanDosyayaYazdır(List<List<string>> tumUrunlerVeOzellikleri)
        {

            FileStream fs = new FileStream("C:/Users/ASUS/Desktop/market.txt", FileMode.Truncate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < sıralıKategoriler.Count; i++)
            {
                string kategori = sıralıKategoriler[i];
                sw.WriteLine(kategori);
                Tree agac = (Tree)kategoriler[kategori];
                List<TreeNode> agacNodes = new List<TreeNode>();
                agac.toStack(agac.getRoot(), agacNodes);
                for (int k = 0; k < agacNodes.Count; k++)
                {
                    List<List<string>> dışListe = agacNodes[k].data;

                    for (int j = 0; j < dışListe.Count; j++)
                    {
                        List<string> içliste = dışListe[j];
                        for (int z = 0; z < içliste.Count; z++)
                        {
                            if (z != içliste.Count - 1)
                            {
                                sw.Write(içliste[z] + " ");
                            }
                            else
                            {
                                sw.Write(içliste[z]);
                                sw.WriteLine();
                            }
                        }
                    }
                }
            }


            sw.Close();
            fs.Close();


        }

        private void agacDengele(int low, int high, Tree agac, List<TreeNode> liste)
        {
            if (low == high)
            {
                return;
            }
            int midpoint = (low + high) / 2;
            agac.insert(liste[midpoint].data);
            agacDengele(midpoint + 1, high, agac, liste);
            agacDengele(low, midpoint, agac, liste);

        }
        //ARAYÜZ ARAÇLARININ METOTLARI

        private void button1_Click(object sender, EventArgs e)
        {

            int rowCount = 0;
            Form2 bulunanÜrünEkranı = null;
            List<string> anahtarKelimeler = null;
            List<List<String>> arananUrun = null;
            arananİçKelime = new List<List<string>>();
            string aranan = textBox1.Text;
            anahtarKelimeler = new List<string>();
            foreach (string anahtar in urunAcıklaması.Keys)
            {

                anahtarKelimeler.Add(anahtar);
            }
            for (int o = 0; o < anahtarKelimeler.Count; o++)
            {
                if (anahtarKelimeler[o].Contains(aranan))
                {

                    rowCount++;
                    TableLayoutPanel layout = new TableLayoutPanel();

                    arananUrun = (List<List<String>>)urunAcıklaması[anahtarKelimeler[o]];




                    for (int k = 0; k < arananUrun.Count; k++)
                    {
                        if (!arananİçKelime.Contains(arananUrun[k]))
                        {
                            arananİçKelime.Add(arananUrun[k]);
                            bulunanÜrünler.Add(arananUrun[k]);
                        }

                    }
                    bulunanÜrünEkranı = new Form2();
                    bulunanÜrünEkranı.Width = 750;
                    layout.ColumnCount = 1;
                    layout.RowCount = rowCount;
                    layout.Dock = DockStyle.Fill;
                    bulunanÜrünEkranı.Controls.Add(layout);
                    Button sepeteEkle;


                    for (int i = 0; i < arananİçKelime.Count; i++)
                    {
                        string fiyat = "";
                        string text = "";
                        var idx = i;
                        List<String> stringListe;
                        stringListe = arananİçKelime[i];
                        sepeteEkle = new Button();
                        sepeteEkle.Click += new EventHandler((sender2, e2) => ListItemClicked(idx));



                        sepeteEkle.Width = 700;
                        for (int z = 0; z < arananİçKelime[i].Count; z++)
                        {
                            if (z == 3 || z == 4)
                            {
                                continue;
                            }
                            else if (z == 5)
                            {
                                fiyat = stringListe[z] + "TL ";

                            }
                            else
                            {
                                text += stringListe[z] + " ";
                            }

                        }
                        text += fiyat;
                        sepeteEkle.Text = text;


                        layout.Controls.Add(sepeteEkle, 0, i + 1);

                    }
                }



            }
            if (bulunanÜrünEkranı != null)
                bulunanÜrünEkranı.Show();
            else
            {
                Form2 dikkat = new Form2();
                Label uyarı = new Label();
                uyarı.Scale(2F, 2F);
                uyarı.Text = "ARADIĞINIZ ÜRÜN BULUNAMADI";
                dikkat.Controls.Add(uyarı);
                dikkat.Show();
            }

        } //İLK GİRİŞTEKİ ARA BUTONUNA TIKLANDIĞINDA ARADIĞIMIZ KELİMEYE GÖRE BULDURDUK YA DA BULDURMADIĞIMIZDA HATA EKRANI VERDİRTTİK.

        private void ListItemClicked(int idx)
        {

        }



        //İterative oluşturulan butonların onclick metodu

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e) //PERSONEL GİRİŞİ
        {
            Form2 personelGirisi = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            personelGirisi.Scale(2.2F, 0.85F);
            layout.ColumnCount = 2;
            layout.RowCount = 6;
            layout.Width = 700;
            layout.Height = 500;
            Button yeniKategorEkle = new Button();
            yeniKategorEkle.Click += new EventHandler(this.yeniKategoriEkle_Click);
            yeniKategorEkle.Text = "Yeni Kategoride Ürün Ekle";
            yeniKategorEkle.Width = 250;
            Button yeniMarkaEkle = new Button();
            yeniMarkaEkle.Click += new EventHandler(this.yeniKategoriEkle_Click);
            yeniMarkaEkle.Text = "Yeni Marka veya Modelde Ürün Ekle";
            yeniMarkaEkle.Width = 250;
            Button ürünAraSil = new Button();
            ürünAraSil.Click += new EventHandler(this.ürünAraSil_click);
            ürünAraSil.Text = "Ürün Ara/Sil";
            ürünAraSil.Width = 250;
            Button ürünBilgisiDegistir = new Button();
            ürünBilgisiDegistir.Click += new EventHandler(this.ürünBilgisiDegistir_click);
            ürünBilgisiDegistir.Text = "Ürün Bilgisi Değiştir";
            ürünBilgisiDegistir.Width = 250;
            TextBox gelirTextBox = new TextBox();
            gelirTextBox.ForeColor = System.Drawing.Color.Gray;
            gelirTextBox.Text = "GELİR";
            TextBox giderTextBox = new TextBox();
            giderTextBox.ForeColor = System.Drawing.Color.Gray;
            giderTextBox.Text = "GİDER";
            TextBox karTextBox = new TextBox();
            karTextBox.ForeColor = System.Drawing.Color.Gray;
            karTextBox.Text = "KAR";
            Button hesapla = new Button();
            hesapla.Click += new EventHandler((sender2, e2) => hesapla_click(gelirTextBox, giderTextBox, karTextBox));
            hesapla.Text = "Hesapla";
            layout.Controls.Add(yeniKategorEkle);
            layout.Controls.Add(yeniMarkaEkle);
            layout.Controls.Add(ürünAraSil);
            layout.Controls.Add(ürünBilgisiDegistir);
            layout.Controls.Add(gelirTextBox);
            layout.Controls.Add(giderTextBox);
            layout.Controls.Add(karTextBox);
            layout.Controls.Add(hesapla);
            personelGirisi.Controls.Add(layout);
            personelGirisi.Show();
            personelGirisi.Show();
        }

        private void hesapla_click(TextBox gelirTextBox, TextBox giderTextBox, TextBox karTextBox)
        {
            StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/kullanıcı.txt");
            string nextline = reader.ReadLine();
            int gelir = 0;
            int gider = 0;
            while (nextline != null)
            {
                string[] satır = nextline.Split(' ');
                if (satır.Length == 12)//ürüne denk gelmiş demektir
                {
                    int maliyet = Int32.Parse(satır[4]);
                    int satışFiyatı = Int32.Parse(satır[5]);
                    gelir += satışFiyatı;
                    gider += maliyet;
                }
                nextline = reader.ReadLine();
            }
            gelirTextBox.Text = "Gelir:" + gelir + "";
            giderTextBox.Text = "Gider:" + gider + "";
            int kar = gelir - gider;
            karTextBox.Text = "Kar:" + kar + "";
            reader.Close();
            karTextBox.ForeColor = System.Drawing.Color.Black;
            gelirTextBox.ForeColor = System.Drawing.Color.Black;
            giderTextBox.ForeColor = System.Drawing.Color.Black;
        }//PERSONEL GİRİŞİ -> KAR GELİR GİDER HESAPLA

        private void ürünBilgisiDegistir_click(object sender, EventArgs e)
        {
            tümÜrünlerVeOzellikleri = new List<List<string>>();
            using (StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/market.txt")) //DOSYAYI OKUYOR BÜTÜN ÜRÜNLERİN ÖZELLİKLERİNİ AYRI AYRI LİST<STRİNG>E ATIP BÜTÜN LİSTELERİ DE TÜMÜRÜNLERVEÖZELLİKLERİ LİSTESİNDE SAKLIYOR.
            {
                string nextline = reader.ReadLine();
                while (nextline != null)
                {

                    if (kategoriler.ContainsKey(nextline))
                    {
                        nextline = reader.ReadLine();
                    }
                    string[] geçiciSatırınOzellikleri = nextline.Split(' ');
                    List<string> değiştirilecekOzellikler = new List<string>();
                    for (int i = 1; i < 12; i++)
                    {
                        değiştirilecekOzellikler.Add(geçiciSatırınOzellikleri[i]);
                    }
                    tümÜrünlerVeOzellikleri.Add(değiştirilecekOzellikler);
                    nextline = reader.ReadLine();
                }
                reader.Close();
            }

            Form2 ürünBilgiDegistirEkranı = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            Button degistirButon = new Button();
            degistirButon.Text = "Değiştir";
            layout.ColumnCount = 11;
            layout.RowCount = tümÜrünlerVeOzellikleri.Count() + 1;
            for (int satır = 0; satır < tümÜrünlerVeOzellikleri.Count; satır++)
            {
                for (int sütun = 0; sütun < tümÜrünlerVeOzellikleri[satır].Count; sütun++)
                {
                    TextBox ürünOzelligiTextBox = new TextBox();
                    List<string> ozellik = tümÜrünlerVeOzellikleri[satır];
                    ürünOzelligiTextBox.Text = ozellik[sütun];
                    ürünOzelligiTextBox.Width = 160;
                    var idx = satır;
                    var idx2 = sütun;

                    layout.Controls.Add(ürünOzelligiTextBox);
                    degistirButon.Click += new EventHandler((sender2, e2) => ListItemClicked4(ürünOzelligiTextBox, idx, idx2));

                }
            }




            layout.Controls.Add(degistirButon);

            layout.Width = 2000;
            layout.Height = 250 * tümÜrünlerVeOzellikleri.Count;
            ürünBilgiDegistirEkranı.Height = 300 * tümÜrünlerVeOzellikleri.Count;
            ürünBilgiDegistirEkranı.Width = 2500;
            ürünBilgiDegistirEkranı.Controls.Add(layout);
            ürünBilgiDegistirEkranı.Deactivate += new EventHandler(ürünBilgiDegistirEkranı_deactivate);
            ürünBilgiDegistirEkranı.Show();
        } //ürün bilgisi değiştir ekranında değiştir butonuna basıldığında..

        private void ürünBilgiDegistirEkranı_deactivate(object sender, EventArgs e)
        {
            hashTabledaUrunBilgisiDegistir(tümÜrünlerVeOzellikleri);
            hashtabledanDosyayaYazdır(tümÜrünlerVeOzellikleri);
            Form2 bilgiekranı = new Form2();
            bilgiekranı.Height = 100;
            Label bilgi = new Label();
            bilgi.Text = "Ürünlerin Bilgileri Güncellendi.";
            bilgi.Width = 500;
            bilgi.Font = new Font(bilgi.Font.FontFamily, bilgi.Font.Size + 4f, bilgi.Font.Style);
            bilgiekranı.Controls.Add(bilgi);
            bilgiekranı.Show();

        } //ürün bilgisi değiştir ekranı kapatıldığında..

        private void ListItemClicked4(TextBox kutu, int satır, int sütun) //ürün bilgisi kopya bir veri yapısında değiştirildi.
        {
            List<string> ozellik = tümÜrünlerVeOzellikleri[satır];
            ozellik[sütun] = kutu.Text;

        }



        private void ListItemClicked2(int idx) //Personel -> Ürün ara Sil -> ekrana çıkan butonlardan herhangi birine tıklandığında..
        {
            List<string> silinecekÜrün = tümÜrünListesi[idx];
            string line = null;
            string silinecek = silinecekÜrün[0];
            List<string> eskiDosya = null;
            using (StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/market.txt")) //dosyayı okuyor. bütün ürünleri eskidosya veri yapısına aktarıyor silinecek ürünü bulduğunda eskidosya veri yapısından ürünü sildiriyor.
            {
                eskiDosya = new List<string>();

                while ((line = reader.ReadLine()) != null)
                {
                    eskiDosya.Add(line);

                }
                for (int i = 0; i < eskiDosya.Count; i++)
                {
                    if (kategoriler.ContainsKey(eskiDosya[i]))
                    {
                        silinenUrunKategorisi = eskiDosya[i];
                    }
                    if (eskiDosya[i].Equals(silinecek))
                    {
                        eskiDosya.RemoveAt(i);
                        break;
                    }
                    reader.Close();
                }

            }
            using (StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/market.txt")) //dosyaya tüm ürün listesini attığımız veri yapısından tekrar yazdırıyor..
            {
                foreach (string yaz in eskiDosya)
                {
                    writer.WriteLine(yaz);
                }
                writer.Close();
            }//DOSYADAN SİLME İŞLEMİ

            //KATEGORİLER HASHTABLEINDAN SİLME İŞLEMİ
            Tree agac = (Tree)kategoriler[silinenUrunKategorisi];
            List<TreeNode> silinecekElemanınAgacınınNodeları = new List<TreeNode>();
            agac.toStack(agac.getRoot(), silinecekElemanınAgacınınNodeları);
            for (int nodeDolas = 0; nodeDolas < silinecekElemanınAgacınınNodeları.Count; nodeDolas++)
            {
                TreeNode temp = silinecekElemanınAgacınınNodeları[nodeDolas];
                List<List<string>> nodeData = temp.data;
                for (int i = 0; i < nodeData.Count; i++)
                {
                    List<string> içListe = nodeData[i];
                    string tekSatır = "";
                    for (int k = 0; k < içListe.Count; k++)
                    {
                        tekSatır += içListe[k];
                    }
                    tekSatır = tekSatır.Replace(" ", "");
                    silinecek = silinecek.Replace(" ", "");
                    if (tekSatır.Equals(silinecek))
                    {
                        nodeData.RemoveAt(i);
                    }
                }


            }
            Form2 silindiEkranı = new Form2();
            Label silindiBilgi = new Label();
            silindiBilgi.Text = "Ürün Silme İşlemi Başarıyla Gerçekleştirildi!";

            silindiBilgi.Font = new Font(silindiBilgi.Font.FontFamily, silindiBilgi.Font.Size + 3f, silindiBilgi.Font.Style);
            silindiBilgi.Width = 500;
            silindiBilgi.Height = 500;
            silindiEkranı.Controls.Add(silindiBilgi);
            silindiEkranı.Scale(1.1f, 0.4f);
            urunAcıklamasıHashTableHazırla();
            silindiEkranı.Show();

        }
        private void ürünAraSil_click(object sender, EventArgs e) //Personel Girişi -> ürünAraSil
        {
            Form2 ürünAraSilEkranı = new Form2();



            Label ürünBilgi = new Label();
            ürünBilgi.Width = 600;
            ürünBilgi.Text = "Silmek İstediğiniz Ürüne Tıklayın.";
            TableLayoutPanel layout = new TableLayoutPanel();
            ürünAraSilEkranı.Width = 750;
            layout.ColumnCount = 1;
            layout.Dock = DockStyle.Fill;
            Button ürünSilButon;


            tümÜrünListesi.Clear();
            try//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.ContainsKey(nextline))
                        {

                            nextline = sr.ReadLine();

                        }

                        List<string> sonrakiÜrün = new List<string>();
                        sonrakiÜrün.Add(nextline);



                        tümÜrünListesi.Add(sonrakiÜrün);


                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }


            int rowCount = 0;
            ürünBilgi.Font = new Font(ürünBilgi.Font.FontFamily, ürünBilgi.Font.Size + 3f, ürünBilgi.Font.Style);

            layout.Controls.Add(ürünBilgi);
            foreach (List<string> ürün in tümÜrünListesi)  //ürün listesi kadar buton oluşturuluyor.
            {
                string fiyat = "";
                string text = "";
                string[] ürünOzellikleri = ürün[0].Split(' ');
                for (int z = 0; z < ürünOzellikleri.Length; z++)
                {
                    if (z == 3 || z == 4)
                    {
                        continue;
                    }
                    else if (z == 5)
                    {
                        fiyat = ürünOzellikleri[z] + "TL ";

                    }
                    else
                    {
                        text += ürünOzellikleri[z] + " ";
                    }

                }
                text += fiyat;


                ürünSilButon = new Button();
                ürünSilButon.Text = ürün[0];
                ürünSilButon.Width = 500;
                ürünSilButon.Text = text;
                var idx = rowCount;
                ürünSilButon.Click += new EventHandler((sender2, e2) => ListItemClicked2(idx));

                rowCount++;
                layout.Controls.Add(ürünSilButon);
            }
            layout.RowCount = rowCount;

            ürünAraSilEkranı.Controls.Add(layout);

            ürünAraSilEkranı.Show();

        }




        private void yeniKategoriEkle_Click(object sender, EventArgs e)
        {
            Form2 kategoriEklemeEkranı = new Form2();
            kategoriEklemeEkranı.Scale(4F, 0.5F);
            Label kategoriBilgisi = new Label();
            Label kategoriBilgisiSıgmadı = new Label();
            kategoriBilgisi.Text = "Var olan kategoriler: ";
            int count = 0;
            foreach (string kategori in kategoriler.Keys)
            {
                count++;
                if (count > 1)
                    kategoriBilgisi.Text += ", " + kategori;
                else
                {
                    kategoriBilgisi.Text += kategori;
                }
            }
            kategoriBilgisiSıgmadı.Width = 500;
            kategoriBilgisiSıgmadı.Text = "Eklemek istediğiniz kategori adını yazın.(Var olan kategorilerden değilse yeni kategorinin adını yazın)";
            kategoriBilgisi.Width = 170 * count;
            kategoriGir = new TextBox();
            Button kategoriEkle = new Button();
            kategoriEkle.Click += new EventHandler(this.kategoriEkle_Click);
            kategoriEkle.Width = 100;
            kategoriGir.Width = 250;
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Controls.Add(kategoriBilgisiSıgmadı);
            layout.Width = 300 * count;
            layout.ColumnCount = 1;
            layout.RowCount = 4;
            layout.Controls.Add(kategoriBilgisi);
            layout.Controls.Add(kategoriGir);
            layout.Controls.Add(kategoriEkle);
            kategoriEkle.Text = "Kategoriyi Ekle";
            kategoriEklemeEkranı.Controls.Add(layout);

            kategoriEklemeEkranı.Show();
        }//Personel Girişi -> yeni kategori ekleme ekran açan buton
        private void kategoriEkle_Click(object sender, EventArgs e)//Personel Girişi -> yeni kategori ekleme -> kategori ekle butonu.
        {
            string yeniKategori = kategoriGir.Text;
            Form2 yeniOzellikAlmaEkranı = new Form2();
            yeniOzellikAlmaEkranı.Scale(1F, 2F);
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.RowCount = 14;
            layout.ColumnCount = 1;
            layout.Width = 500;
            layout.Height = 1000;
            Label ürünKategori = new Label();
            ürünKategori.Text = "Girilen Kategori: " + yeniKategori;
            isimAl = new TextBox();
            isimAl.ForeColor = System.Drawing.Color.Gray;
            isimAl.Click += new EventHandler(this.İsimAl_Click);
            isimAl.Text = "ürün genel ismi";


            markaAl = new TextBox();
            markaAl.ForeColor = System.Drawing.Color.Gray;
            markaAl.Text = "ürünün markası";
            markaAl.Click += new EventHandler(this.MarkaAl_Click);

            modelAl = new TextBox();
            modelAl.ForeColor = System.Drawing.Color.Gray;
            modelAl.Text = "ürünün modeli";
            modelAl.Click += new EventHandler(this.modelAl_Click);

            stokSayısıAl = new TextBox();
            stokSayısıAl.ForeColor = System.Drawing.Color.Gray;
            stokSayısıAl.Text = "stoktaki sayı";
            stokSayısıAl.Click += new EventHandler(this.stokSayısıAl_Click);


            maliyetAl = new TextBox();
            maliyetAl.ForeColor = System.Drawing.Color.Gray;
            maliyetAl.Text = "ürünün maliyet";
            maliyetAl.Click += new EventHandler(this.maliyetAl_Click);


            satışFiyatıAl = new TextBox();
            satışFiyatıAl.ForeColor = System.Drawing.Color.Gray;
            satışFiyatıAl.Text = "ürünün satış fiyatı";
            satışFiyatıAl.Click += new EventHandler(this.satışFiyatıAl_Click);

            özellik1 = new TextBox();
            özellik1.ForeColor = System.Drawing.Color.Gray;
            özellik1.Text = "özellik1";
            özellik1.Click += new EventHandler(this.özellik1_Click);

            özellik2 = new TextBox();
            özellik2.ForeColor = System.Drawing.Color.Gray;
            özellik2.Text = "özellik2";
            özellik2.Click += new EventHandler(this.özellik2_Click);

            özellik3 = new TextBox();
            özellik3.ForeColor = System.Drawing.Color.Gray;
            özellik3.Text = "özellik3";
            özellik3.Click += new EventHandler(this.özellik3_Click);

            özellik4 = new TextBox();
            özellik4.ForeColor = System.Drawing.Color.Gray;
            özellik4.Text = "özellik4";
            özellik4.Click += new EventHandler(this.özellik4_Click);

            özellik5 = new TextBox();
            özellik5.ForeColor = System.Drawing.Color.Gray;
            özellik5.Text = "özellik5";
            özellik5.Click += new EventHandler(this.özellik5_Click);

            özellik6 = new TextBox();
            özellik6.ForeColor = System.Drawing.Color.Gray;
            özellik6.Text = "özellik6";
            özellik6.Click += new EventHandler(this.özellik6_Click);
            Button ekle = new Button();
            ekle.Click += new EventHandler(this.ekle_Click);
            ekle.Text = "Ekle";
            ürünKategori.Width = 250;
            layout.Controls.Add(ürünKategori);
            layout.Controls.Add(isimAl);
            layout.Controls.Add(markaAl);
            layout.Controls.Add(modelAl);
            layout.Controls.Add(stokSayısıAl);
            layout.Controls.Add(maliyetAl);
            layout.Controls.Add(satışFiyatıAl);
            layout.Controls.Add(özellik1);
            layout.Controls.Add(özellik2);
            layout.Controls.Add(özellik3);
            layout.Controls.Add(özellik4);
            layout.Controls.Add(özellik5);
            layout.Controls.Add(özellik6);
            layout.Controls.Add(ekle);
            yeniOzellikAlmaEkranı.Controls.Add(layout);
            yeniOzellikAlmaEkranı.Show();

        }


        // BUTONLARIN ONCLİCKLERİ
        private void İsimAl_Click(object sender, EventArgs e)
        {
            isimAl.ForeColor = System.Drawing.Color.Black;
            isimAl.Text = "";
        }
        private void MarkaAl_Click(object sender, EventArgs e)
        {
            markaAl.ForeColor = System.Drawing.Color.Black;
            markaAl.Text = "";
        }
        private void modelAl_Click(object sender, EventArgs e)
        {
            modelAl.ForeColor = System.Drawing.Color.Black;
            modelAl.Text = "";
        }
        private void stokSayısıAl_Click(object sender, EventArgs e)
        {
            stokSayısıAl.ForeColor = System.Drawing.Color.Black;
            stokSayısıAl.Text = "";
        }
        private void maliyetAl_Click(object sender, EventArgs e)
        {
            maliyetAl.ForeColor = System.Drawing.Color.Black;
            maliyetAl.Text = "";
        }
        private void satışFiyatıAl_Click(object sender, EventArgs e)
        {
            satışFiyatıAl.ForeColor = System.Drawing.Color.Black;
            satışFiyatıAl.Text = "";
        }
        private void özellik1_Click(object sender, EventArgs e)
        {
            özellik1.ForeColor = System.Drawing.Color.Black;
            özellik1.Text = "";
        }
        private void özellik2_Click(object sender, EventArgs e)
        {
            özellik2.ForeColor = System.Drawing.Color.Black;
            özellik2.Text = "";
        }
        private void özellik3_Click(object sender, EventArgs e)
        {
            özellik3.ForeColor = System.Drawing.Color.Black;
            özellik3.Text = "";
        }
        private void özellik4_Click(object sender, EventArgs e)
        {
            özellik4.ForeColor = System.Drawing.Color.Black;
            özellik4.Text = "";
        }
        private void özellik5_Click(object sender, EventArgs e)
        {
            özellik5.ForeColor = System.Drawing.Color.Black;
            özellik5.Text = "";
        }
        private void özellik6_Click(object sender, EventArgs e)
        {
            özellik6.ForeColor = System.Drawing.Color.Black;
            özellik6.Text = "";
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = System.Drawing.Color.Black;
            textBox1.Text = "";
        }

        private void kullanıcıAdı_click(object sender, EventArgs e)
        {
            kullanıcıAdı.ForeColor = System.Drawing.Color.Black;
            kullanıcıAdı.Text = "";
        }
        private void şifre_click(object sender, EventArgs e)
        {
            şifre.ForeColor = System.Drawing.Color.Black;
            şifre.Text = "";
        }
        private void yaş_click(object sender, EventArgs e)
        {
            yaş.ForeColor = System.Drawing.Color.Black;
            yaş.Text = "";
        }
        private void maaş_click(object sender, EventArgs e)
        {
            maaş.ForeColor = System.Drawing.Color.Black;
            maaş.Text = "";
        }
        private void cinsiyet_click(object sender, EventArgs e)
        {
            cinsiyet.ForeColor = System.Drawing.Color.Black;
            cinsiyet.Text = "";
        }
        private void şehir_click(object sender, EventArgs e)
        {
            şehir.ForeColor = System.Drawing.Color.Black;
            şehir.Text = "";
        }
        private void NsayısıTextBox_click(TextBox nsayısıTextBox)
        {
            nsayısıTextBox.Text = "";
        }

        private void fiyatListeleTextBoxMax_click(TextBox fiyatListeleTextBoxMax)
        {
            fiyatListeleTextBoxMax.Text = "";
        }
        private void fiyatListeleTextBoxMin_click(TextBox fiyatListeleTextBoxMin)
        {
            fiyatListeleTextBoxMin.Text = "";
        }
        private void isimListeleTextBox_click(TextBox isimListeleTextBox)
        {
            isimListeleTextBox.Text = "";
        }
        private void kategoriListeleTextBox_click(TextBox kategoriListeleTextBox)
        {
            kategoriListeleTextBox.Text = "";
        }
        private void aramaCubuguButon_click(TextBox aramaCubugu)
        {
            aramaCubugu.Text = "";
            aramaCubugu.ForeColor = System.Drawing.Color.Black;
        }


        private void ekle_Click(object sender, EventArgs e) //yeni ürün eklerken en son basılan buton ->Personel Girişi ->yeni kategoride veya isimde ürün ekle -> bilgileri aldıktan sonra bastığımız ekle butonu
        {
            List<string> yeniKategorideÜrünÖzellikleri = new List<string>();
            yeniKategorideÜrünÖzellikleri.Add(kategoriGir.Text);
            yeniKategorideÜrünÖzellikleri.Add(isimAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(markaAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(modelAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(stokSayısıAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(maliyetAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(satışFiyatıAl.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik1.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik2.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik3.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik4.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik5.Text);
            yeniKategorideÜrünÖzellikleri.Add(özellik6.Text);


            yeniKategoriEkle(yeniKategorideÜrünÖzellikleri);
            Form2 eklendi = new Form2();
            Label eklendiLabel = new Label();
            eklendiLabel.Text = "Ürün Ekleme İşlemi Başarıyla Gerçekleştirildi!";
            eklendiLabel.Width = 500;
            eklendiLabel.Height = 500;
            eklendi.Scale(1.1F, 0.4F);
            eklendiLabel.Font = new Font(eklendiLabel.Font.FontFamily, eklendiLabel.Font.Size + 3f, eklendiLabel.Font.Style);
            eklendi.Controls.Add(eklendiLabel);
            eklendi.Show();
        }

        private void button2_Click(object sender, EventArgs e) //MÜŞTERİ GİRİŞİ
        {
            Form2 musteriGirisi = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.RowCount = 3;
            layout.ColumnCount = 1;
            ID = new TextBox();
            ID.ForeColor = Color.Gray;
            ID.Click += new EventHandler(ıd_click);
            ID.Width = 200;
            ID.Height = 200;
            ID.Text = "Kullanıcı adı";
            şifreGir = new TextBox();
            şifreGir.ForeColor = Color.Gray;
            şifreGir.Click += new EventHandler(şifregir_click);
            şifreGir.Width = 200;
            şifreGir.Height = 200;
            şifreGir.Text = "Şifre";
            Button girişYap = new Button();
            girişYap.Height = 25;
            girişYap.Width = 100;
            girişYap.Text = "Giriş Yap";
            girişYap.Click += new EventHandler(this.girişYap_click);
            layout.Width = 250;
            layout.Height = 250;
            layout.Controls.Add(ID);
            layout.Controls.Add(şifreGir);
            layout.Controls.Add(girişYap);
            musteriGirisi.Controls.Add(layout);
            musteriGirisi.Show();
        }

        private void ıd_click(object sender, EventArgs e)
        {
            ID.Text = "";
            ID.ForeColor = Color.Black;
        }

        private void şifregir_click(object sender, EventArgs e)
        {
            şifreGir.Text = "";
            şifreGir.ForeColor = Color.Black;
        }

        private void girişYap_click(object sender, EventArgs e)
        {
            string kullanıcıAdı = ID.Text;
            string şifre = şifreGir.Text;
            StreamReader reader = new StreamReader("C: /Users/ASUS/Desktop/kullanıcı.txt");
            string nextline = reader.ReadLine();
            bool girişYapıldıMı = false;
            while (nextline != null)
            {
                string[] satırtut = null;
                if (nextline != null)
                {
                    satırtut = nextline.Split(' ');
                }

                if (kullanıcıAdı.Equals(satırtut[0]) && şifre.Equals(satırtut[1])) // kullanıcı girişi başarılı
                {
                    mevcutKullanıcı = kullanıcıAdı;
                    Form2 müşteriEkranı = new Form2();
                    girişYapıldıMı = true;
                    TableLayoutPanel layout = new TableLayoutPanel();
                    layout.RowCount = 7;
                    layout.ColumnCount = 2;

                    aramaCubugu = new TextBox();
                    aramaCubugu.Width = 300;
                    layout.Controls.Add(aramaCubugu); aramaCubugu.Text = "Ne aramıştınız?                                                                      🔍";
                    aramaCubugu.ForeColor = Color.Gray;
                    aramaCubugu.Click += new EventHandler((sender2, e2) => aramaCubuguButon_click(aramaCubugu));

                    Button araButon = new Button();
                    araButon.Click += new EventHandler(this.araButon_click);
                    layout.Controls.Add(araButon);
                    araButon.Text = "Ara";
                    araButon.Width = 50;
                    TextBox kategoriListeleTextBox = new TextBox();
                    layout.Controls.Add(kategoriListeleTextBox);
                    kategoriListeleTextBox.Text = "Kategori Giriniz";
                    kategoriListeleTextBox.Click += new EventHandler((sender2, e2) => kategoriListeleTextBox_click(kategoriListeleTextBox));

                    Button kategoriListeleButon = new Button();
                    kategoriListeleButon.Click += new EventHandler((sender2, e2) => kategoriListeleButon_click(kategoriListeleTextBox));
                    layout.Controls.Add(kategoriListeleButon);
                    kategoriListeleButon.Width = 250;
                    kategoriListeleButon.Text = "Kategoriye Göre Listele";

                    TextBox isimListeleTextBox = new TextBox();
                    layout.Controls.Add(isimListeleTextBox);
                    isimListeleTextBox.Text = "İsim Giriniz";
                    isimListeleTextBox.Click += new EventHandler((sender2, e2) => isimListeleTextBox_click(isimListeleTextBox));

                    Button isimListeleButon = new Button();


                    layout.Controls.Add(isimListeleButon);
                    isimListeleButon.Width = 250;
                    isimListeleButon.Text = "İsime Göre Listele";
                    isimListeleButon.Click += new EventHandler((sender2, e2) => isimListeleButon_click(isimListeleTextBox));


                    TextBox fiyatListeleTextBoxMin = new TextBox();
                    layout.Controls.Add(fiyatListeleTextBoxMin);
                    fiyatListeleTextBoxMin.Text = "Minimum fiyat";
                    fiyatListeleTextBoxMin.Click += new EventHandler((sender2, e2) => fiyatListeleTextBoxMin_click(fiyatListeleTextBoxMin));


                    Button fiyatListeleButon = new Button();

                    layout.Controls.Add(fiyatListeleButon);
                    fiyatListeleButon.Width = 250;
                    fiyatListeleButon.Text = "Fiyat Aralığına Göre Listele";

                    TextBox fiyatListeleTextBoxMax = new TextBox();
                    layout.Controls.Add(fiyatListeleTextBoxMax);
                    fiyatListeleTextBoxMax.Text = "Maximum fiyat";
                    fiyatListeleTextBoxMax.Click += new EventHandler((sender2, e2) => fiyatListeleTextBoxMax_click(fiyatListeleTextBoxMax));

                    fiyatListeleButon.Click += new EventHandler((sender2, e2) => fiyatListeleButon_click(fiyatListeleTextBoxMin, fiyatListeleTextBoxMax));

                    Button isimFiyatListeleButon = new Button();
                    isimFiyatListeleButon.Click += new EventHandler((sender2, e2) => isimFiyatListeleButon_click(fiyatListeleTextBoxMin, fiyatListeleTextBoxMax, isimListeleTextBox));

                    layout.Controls.Add(isimFiyatListeleButon);
                    isimFiyatListeleButon.Width = 250;
                    isimFiyatListeleButon.Text = "İsim-Fiyata Göre Listele";

                    Button siparişVer = new Button();
                    siparişVer.Click += new EventHandler(siparişVer_click);
                    siparişVer.Text = "Sipariş Ver";
                    layout.Controls.Add(siparişVer);



                    Button agacDengele = new Button();
                    agacDengele.Click += new EventHandler(agacDengele_click);
                    agacDengele.Width = 250;
                    layout.Controls.Add(agacDengele);

                    TextBox NsayısıTextBox = new TextBox();
                    NsayısıTextBox.Text = "Bir sayı giriniz(N)";
                    NsayısıTextBox.Click += new EventHandler((sender2, e2) => NsayısıTextBox_click(NsayısıTextBox));
                    NsayısıTextBox.Width = 100;
                    layout.Controls.Add(NsayısıTextBox);

                    TextBox heapKategori = new TextBox();
                    heapKategori.Text = "Kategori giriniz";
                    heapKategori.Click += new EventHandler((sender2, e2) => heapKategori_click(heapKategori));
                    heapKategori.Width = 100;
                    layout.Controls.Add(heapKategori);

                    Button heapButton = new Button();
                    heapButton.Click += new EventHandler((sender2, e2) => heapButtoneButon_click(NsayısıTextBox, heapKategori));

                    heapButton.Text = "En ucuz N ürün";
                    heapButton.Width = 250;
                    layout.Controls.Add(heapButton);

                    agacDengele.Text = "Ağaç Dengele";

                    müşteriEkranı.Width = 700;
                    müşteriEkranı.Height = 400;
                    layout.Width = 750;
                    layout.Height = 750;
                    müşteriEkranı.Controls.Add(layout);
                    müşteriEkranı.Show();
                    break;
                }
                else
                {
                    nextline = reader.ReadLine();
                }



            }
            if (girişYapıldıMı == false)
            {
                Form2 kullanıcıyok = new Form2();
                Label kullanıcıyokbilgi = new Label();
                kullanıcıyokbilgi.Width = 500;
                kullanıcıyokbilgi.Text = "BÖYLE BİR KULLANICI BULUNAMADI";
                kullanıcıyok.Controls.Add(kullanıcıyokbilgi);
                kullanıcıyok.Show();
            }
            reader.Close();
        }//MÜŞTERİ ÜYE GİRİŞ EKRANI

        private void heapKategori_click(TextBox heapKategori)
        {
            heapKategori.Text = "";
        }





        private void heapButtoneButon_click(TextBox nsayısıTextBox, TextBox heapKategori) //N tane en ucuz ürün heapten çekilerek kullanıcının sepetine (dosyaya yazdırılarak) ekleniyor.
        {
            string girilenKategori = heapKategori.Text;
            int girilenNsayısı = Int32.Parse(nsayısıTextBox.Text);
            Heap oluşturulanHeap = heapHazırla(kategoriler, girilenKategori);
            List<List<string>> sıralıÜrünler = oluşturulanHeap.NtaneÜrünüSırala(girilenNsayısı);

            List<string> kullanıcıtxt = new List<string>();

            StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/kullanıcı.txt");
            string nextline = reader.ReadLine();
            string urun = "";
            while (nextline != null)
            {
                kullanıcıtxt.Add(nextline);
                string[] kullanıcıBilgi = nextline.Split(' ');
                if (kullanıcıBilgi.Length < 12)
                {
                    if (kullanıcıBilgi[0].Equals(mevcutKullanıcı))
                    {

                        for (int i = 0; i < sıralıÜrünler.Count; i++)
                        {
                            urun = "";
                            for (int k = 0; k < sıralıÜrünler[i].Count; k++)
                            {
                                if (k == sıralıÜrünler[i].Count - 1)
                                {
                                    urun += sıralıÜrünler[i][k];
                                }
                                else
                                {
                                    urun += sıralıÜrünler[i][k] + " ";
                                }

                            }
                            kullanıcıtxt.Add(urun);
                        }


                    }

                }

                nextline = reader.ReadLine();
            }

            reader.Close();
            StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/kullanıcı.txt");
            for (int k = 0; k < kullanıcıtxt.Count; k++)
            {

                writer.WriteLine(kullanıcıtxt[k]);

            }
            writer.Close();
            Form2 eklendi = new Form2();
            Label eklendiBilgi = new Label();
            eklendiBilgi.Text = "En ucuz " + sıralıÜrünler.Count + " tane ürün sepetinize eklendi.";
            eklendiBilgi.Width = 500;
            eklendi.Width = 250;
            eklendi.Height = 100;
            eklendi.Controls.Add(eklendiBilgi);
            eklendi.Show();
        }


        private void kategoriListeleButon_click(TextBox kategoriListeleTextBox)
        {
            string girilenKategori = kategoriListeleTextBox.Text;
            Tree agac = (Tree)kategoriler[girilenKategori];
            List<TreeNode> postOrderNodelar = new List<TreeNode>();
            postOrderNodelar = agac.postOrder(agac.getRoot(), postOrderNodelar);

            List<TreeNode> preOrderNodelar = new List<TreeNode>();
            preOrderNodelar = agac.preOrder(agac.getRoot(), preOrderNodelar);
            List<TreeNode> a = new List<TreeNode>();

            agac.elemanSayıları = new int[Int16.MaxValue];
            agac.inOrder(agac.getRoot(), -1, a); //agac.elemanSayılarını oluşturması için.


            int[] düzeydekiElemanSayıları = agac.elemanSayıları;


            int maxDerinlik = agac.maxDepth;

            Form2 kategoriListeleEkran = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();

            Button inorderGoster = new Button();
            inorderGoster.Click += new EventHandler((sender2, e2) => inorderGoster_click(agac));
            inorderGoster.Text = "İnorder Sırala";
            inorderGoster.Width = 250;

            Button preorderGoster = new Button();
            preorderGoster.Click += new EventHandler((sender2, e2) => preorderGoster_click(agac));
            preorderGoster.Text = "Preorder Sırala";
            preorderGoster.Width = 250;

            Button postorderGoster = new Button();
            postorderGoster.Click += new EventHandler((sender2, e2) => postorderGoster_click(agac));
            postorderGoster.Text = "Postorder Sırala";
            postorderGoster.Width = 250;

            Button elemanSayılarınıGoster = new Button();
            elemanSayılarınıGoster.Click += new EventHandler((sender2, e2) => elemanSayılarınıGoster_click(agac));
            elemanSayılarınıGoster.Text = "Her Düzeyde Kaç Eleman Olduğunu Göster";
            elemanSayılarınıGoster.Width = 250;

            Label maxderinlik = new Label();
            maxderinlik.Width = 300;
            maxderinlik.Text = "Maximum Derinlik:" + maxDerinlik;

            layout.ColumnCount = 1;
            layout.RowCount = 5;
            layout.Controls.Add(inorderGoster);
            layout.Controls.Add(preorderGoster);
            layout.Controls.Add(postorderGoster);
            layout.Controls.Add(elemanSayılarınıGoster);
            layout.Controls.Add(maxderinlik);

            layout.Width = 1000;
            layout.Height = 700;
            kategoriListeleEkran.Controls.Add(layout);
            kategoriListeleEkran.Show();
        } //MÜŞTERİ -> KATEGORİ YAZ -> KATEGORİYE GÖRE LİSTELE

        private void elemanSayılarınıGoster_click(Tree agac)
        {
            Form2 elemansayısıEkranı = new Form2();
            Label bilgi = new Label();
            bilgi.Text = "DÜZEY:            ";
            Label bilgi2 = new Label();
            bilgi2.Width = 500;
            bilgi.Width = 500;
            bilgi2.Text = "ELEMAN SAYISI: ";
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 1;
            layout.RowCount = 2;
            layout.Width = 1000;
            for (int i = 0; i < agac.maxDepth + 1; i++)
            {
                int düzey = i;
                int düzeydekiElemanSayısı = agac.elemanSayıları[i];
                bilgi.Text += "   " + düzey;
                bilgi2.Text += düzeydekiElemanSayısı + "   ";

            }
            layout.Controls.Add(bilgi);
            layout.Controls.Add(bilgi2);
            elemansayısıEkranı.Controls.Add(layout);
            elemansayısıEkranı.Show();

        }//MÜŞTERİ -> KATEGORİ YAZ -> KATEGORİYE GÖRE LİSTELE -> HER BİR DÜZEYDEKİ ELEMAN SAYISI YAZDIR

        //AĞAÇTAKİ ELEMANLARI FARKLI TRAVERSAL YÖNETEMLERİ İLE EKRANA YAZDIRMA

        private void postorderGoster_click(Tree agac)
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            Form2 postOrderekran = new Form2();
            postOrderekran.Width = 500;
            postOrderekran.Controls.Add(layout);
            Label bilgi = new Label();
            bilgi.Text = "POSTORDER SIRA: ";
            layout.Controls.Add(bilgi);
            layout.ColumnCount = 1;
            List<TreeNode> postOrderNodelar = new List<TreeNode>();
            postOrderNodelar = agac.postOrder(agac.getRoot(), postOrderNodelar);
            Label ürünYazısı = null;
            string ürün = "";

            int ürünSay = 0;
            for (int i = 0; i < postOrderNodelar.Count; i++)
            {

                TreeNode node = postOrderNodelar[i];
                for (int k = 0; k < node.data.Count; k++)
                {
                    List<string> data = node.data[k];
                    ürün = "";
                    for (int j = 0; j < data.Count; j++)
                    {
                        ürünSay++;
                        ürün += data[j] + " ";
                    }
                    ürünYazısı = new Label();
                    ürünYazısı.Width = 500;
                    layout.Height = 1000;
                    layout.Width = 500;
                    ürünYazısı.Text = ürün;
                    layout.RowCount = ürünSay;
                    layout.Controls.Add(ürünYazısı);

                }
            }
            postOrderekran.Show();
        }

        private void preorderGoster_click(Tree agac)
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            Form2 preOrderekran = new Form2();
            preOrderekran.Width = 500;
            preOrderekran.Controls.Add(layout);
            Label bilgi = new Label();
            bilgi.Text = "PREORDER SIRA: ";
            layout.Controls.Add(bilgi);
            layout.ColumnCount = 1;
            List<TreeNode> preOrderNodelar = new List<TreeNode>();
            preOrderNodelar = agac.preOrder(agac.getRoot(), preOrderNodelar);
            Label ürünYazısı = null;
            string ürün = "";

            int ürünSay = 0;
            for (int i = 0; i < preOrderNodelar.Count; i++)
            {

                TreeNode node = preOrderNodelar[i];
                for (int k = 0; k < node.data.Count; k++)
                {
                    List<string> data = node.data[k];
                    ürün = "";
                    for (int j = 0; j < data.Count; j++)
                    {
                        ürünSay++;
                        ürün += data[j] + " ";
                    }
                    ürünYazısı = new Label();
                    ürünYazısı.Width = 500;
                    layout.Height = 1000;
                    layout.Width = 500;
                    ürünYazısı.Text = ürün;
                    layout.RowCount = ürünSay;
                    layout.Controls.Add(ürünYazısı);

                }
            }


            layout.RowCount = ürünSay + 1;
            preOrderekran.Show();
        }

        private void inorderGoster_click(Tree agac)
        {
            TableLayoutPanel layout = new TableLayoutPanel();
            Form2 inorderekran = new Form2();
            inorderekran.Width = 500;

            inorderekran.Controls.Add(layout);
            Label bilgi = new Label();
            bilgi.Text = "İNORDER SIRA: ";
            layout.Controls.Add(bilgi);
            layout.ColumnCount = 1;
            List<TreeNode> inOrderNodelar = new List<TreeNode>();
            inOrderNodelar = agac.inOrder(agac.getRoot(), -1, inOrderNodelar);
            Label ürünYazısı = null;
            string ürün = "";

            int ürünSay = 0;
            for (int i = 0; i < inOrderNodelar.Count; i++)
            {

                TreeNode node = inOrderNodelar[i];
                for (int k = 0; k < node.data.Count; k++)
                {
                    List<string> data = node.data[k];
                    ürün = "";
                    for (int j = 0; j < data.Count; j++)
                    {
                        ürünSay++;
                        ürün += data[j] + " ";
                    }
                    ürünYazısı = new Label();
                    ürünYazısı.Width = 500;
                    layout.Height = 1000;
                    layout.Width = 500;
                    ürünYazısı.Text = ürün;
                    layout.RowCount = ürünSay;
                    layout.Controls.Add(ürünYazısı);

                }
            }


            layout.RowCount = ürünSay + 1;
            inorderekran.Show();
        }

        private void isimFiyatListeleButon_click(TextBox min, TextBox max, TextBox isimListeleTextBox)
        {
            tümÜrünListesi.Clear();
            try//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.ContainsKey(nextline))
                        {

                            nextline = sr.ReadLine();

                        }

                        List<string> sonrakiÜrün = new List<string>();
                        sonrakiÜrün.Add(nextline);



                        tümÜrünListesi.Add(sonrakiÜrün);


                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }
            int minFiyat = Int32.Parse(min.Text);
            int maxFiyat = Int32.Parse(max.Text);
            List<string> isimfiyatAralığındakiÜrünler = new List<string>();
            for (int i = 0; i < tümÜrünListesi.Count; i++)
            {
                for (int k = 0; k < tümÜrünListesi[i].Count; k++)
                {
                    string[] ürünOzellikleri = tümÜrünListesi[i][k].Split(' ');
                    string stringÜrünFiyat = ürünOzellikleri[5];
                    int ürünFiyat = Int32.Parse(stringÜrünFiyat);
                    if (ürünFiyat < maxFiyat && ürünFiyat > minFiyat && ürünOzellikleri[0].Equals(isimListeleTextBox.Text))
                    {
                        isimfiyatAralığındakiÜrünler.Add(tümÜrünListesi[i][k]);

                    }
                }
            }
            Form2 ürünleriGoruntule = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            Button ürün;



            for (int i = 0; i < isimfiyatAralığındakiÜrünler.Count; i++)
            {

                ürün = new Button();
                string[] ozellikler = isimfiyatAralığındakiÜrünler[i].Split(' ');
                string text = "";
                string fiyat = "";
                for (int z = 0; z < ozellikler.Length; z++)
                {
                    if (z == 3 || z == 4)
                    {
                        continue;
                    }
                    else if (z == 5)
                    {
                        fiyat = ozellikler[z] + "TL ";

                    }
                    else
                    {
                        text += ozellikler[z] + " ";
                    }

                }
                text += fiyat;
                ürün.Text = text;
                var idx = i;
                ürün.Click += new EventHandler((sender2, e2) => satınAl(idx, isimfiyatAralığındakiÜrünler));
                ürün.Width = 750;
                layout.Controls.Add(ürün);
            }
            layout.RowCount = isimfiyatAralığındakiÜrünler.Count;
            layout.Width = 1000;
            layout.Height = 1000;
            ürünleriGoruntule.Controls.Add(layout);
            ürünleriGoruntule.Width = 1000;
            ürünleriGoruntule.Height = 750;
            ürünleriGoruntule.Show();
        } //MÜŞTERİ -> İSİM VE FİYAT GİRİŞİ -> İSİME VE FİYAT ARALIĞINA GÖRE LİSTELE

        private void isimListeleButon_click(TextBox isimListeleTextBox)//MÜŞTERİ -> İSİM GİRİŞİ -> İSİME  GÖRE LİSTELE
        {
            string ürünİsmi = isimListeleTextBox.Text;

            tümÜrünListesi.Clear();
            try//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.ContainsKey(nextline))
                        {

                            nextline = sr.ReadLine();

                        }

                        List<string> sonrakiÜrün = new List<string>();
                        sonrakiÜrün.Add(nextline);



                        tümÜrünListesi.Add(sonrakiÜrün);


                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }
            int count = 0;
            List<string> ismiİçerenÜrünler = new List<string>();
            for (int i = 0; i < tümÜrünListesi.Count; i++)
            {
                for (int k = 0; k < tümÜrünListesi[i].Count; k++)
                {
                    string[] ürünOzellikleri = tümÜrünListesi[i][k].Split(' ');
                    string stringÜrünFiyat = ürünOzellikleri[0];
                    if (ürünOzellikleri[0].Equals(ürünİsmi))
                    {
                        ismiİçerenÜrünler.Add(tümÜrünListesi[i][k]);
                        count++;
                    }
                }
            }
            Form2 ürünleriGoruntule = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            Button ürün;



            for (int i = 0; i < ismiİçerenÜrünler.Count; i++)
            {

                ürün = new Button();
                string[] ozellikler = ismiİçerenÜrünler[i].Split(' ');
                string text = "";
                string fiyat = "";
                for (int z = 0; z < ozellikler.Length; z++)
                {
                    if (z == 3 || z == 4)
                    {
                        continue;
                    }
                    else if (z == 5)
                    {
                        fiyat = ozellikler[z] + "TL ";

                    }
                    else
                    {
                        text += ozellikler[z] + " ";
                    }

                }
                text += fiyat;
                ürün.Text = text;
                var idx = i;
                ürün.Click += new EventHandler((sender2, e2) => satınAl(idx, ismiİçerenÜrünler));
                ürün.Width = 750;
                layout.Controls.Add(ürün);
            }
            if (count > 0)
            {
                layout.RowCount = ismiİçerenÜrünler.Count;
                layout.Width = 1000;
                layout.Height = 1000;
                ürünleriGoruntule.Controls.Add(layout);
                ürünleriGoruntule.Width = 1000;
                ürünleriGoruntule.Height = 750;
                ürünleriGoruntule.Show();
            }
            else
            {
                Label bilgi = new Label();
                bilgi.Text = "Aradığınız isimde ürün mevcut değildir.";
                bilgi.Width = 600;
                ürünleriGoruntule.Controls.Add(bilgi);
                ürünleriGoruntule.Show();
            }


        }

        private void siparişVer_click(object sender, EventArgs e)
        {
            bool siparişVereTıklandıMı = true;
            Form2 tümÜrünlerEkranı = new Form2();
            Label ürünBilgi = new Label();
            ürünBilgi.Text = "Sipariş vermek istediğiniz ürüne tıklayınız.";
            ürünBilgi.Font = new Font(ürünBilgi.Font.FontFamily, ürünBilgi.Font.Size + 3f, ürünBilgi.Font.Style);
            ürünBilgi.Width = 500;

            Button ürünSiparişButon;
            TableLayoutPanel layout = new TableLayoutPanel();
            tümÜrünListesi.Clear();
            try//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.ContainsKey(nextline))
                        {

                            nextline = sr.ReadLine();

                        }

                        List<string> sonrakiÜrün = new List<string>();
                        sonrakiÜrün.Add(nextline);



                        tümÜrünListesi.Add(sonrakiÜrün);


                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }


            int rowCount = 0;
            layout.Controls.Add(ürünBilgi);
            foreach (List<string> ürün in tümÜrünListesi)  //ürün listesi kadar buton oluşturuluyor.
            {
                string fiyat = "";
                string text = "";
                string[] ürünOzellikleri = ürün[0].Split(' ');
                for (int z = 0; z < ürünOzellikleri.Length; z++)
                {
                    if (z == 3 || z == 4)
                    {
                        continue;
                    }
                    else if (z == 5)
                    {
                        fiyat = ürünOzellikleri[z] + "TL ";

                    }
                    else
                    {
                        text += ürünOzellikleri[z] + " ";
                    }

                }
                text += fiyat;


                ürünSiparişButon = new Button();
                ürünSiparişButon.Text = ürün[0];
                ürünSiparişButon.Width = 500;
                ürünSiparişButon.Text = text;

                ürünSiparişButon.Width = 500;
                var idx = rowCount;
                ürünSiparişButon.Click += new EventHandler((sender2, e2) => siparişiAl(idx, siparişVereTıklandıMı));
                rowCount++;
                layout.Controls.Add(ürünSiparişButon);
            }
            layout.RowCount = rowCount + 1;
            tümÜrünlerEkranı.Controls.Add(layout);
            layout.Width = 1000;
            layout.Height = rowCount * 250;
            tümÜrünlerEkranı.Width = 800;
            tümÜrünlerEkranı.Height = rowCount * 250;
            tümÜrünlerEkranı.Show();
        }//MÜŞTERİ -> BÜTÜN ÜRÜNLER ARASINDAN SİPARİŞ VERME

        private void siparişiAl(int idx, bool siparişVereTıklandıMı)//MÜŞTERİ -> GİRİŞ YAPILDI -> ÜRÜN SİPARİŞİ AL -> TÜM ÜRÜNLER  BUTON ŞEKLİNDE LİSTELENDİ HERHANGİ BİRİ SEÇİLDİĞİNDE BU ÇALIŞIYOR.
        {
            List<string> kullanıcıtxt = new List<string>();
            List<string> bulunanÜrünler = new List<string>();

            if (siparişVereTıklandıMı)
            {
                string ürün = "";
                for (int i = 0; i < tümÜrünListesi.Count; i++)
                {

                    for (int k = 0; k < tümÜrünListesi[i].Count; k++)
                    {
                        if (k == tümÜrünListesi[i].Count - 1)
                        {
                            ürün += tümÜrünListesi[i][k];
                        }
                        else
                        {
                            ürün += tümÜrünListesi[i][k] + " ";
                        }

                    }

                    bulunanÜrünler.Add(ürün);
                    ürün = "";
                }

            }
            else
            {
                string ürün = "";
                for (int i = 0; i < arananİçKelime.Count; i++)
                {

                    for (int k = 0; k < arananİçKelime[i].Count; k++)
                    {
                        if (k == arananİçKelime[i].Count - 1)
                        {
                            ürün += arananİçKelime[i][k];
                        }
                        else
                        {
                            ürün += arananİçKelime[i][k] + " ";
                        }

                    }

                    bulunanÜrünler.Add(ürün);
                    ürün = "";
                }

            }

            string ürünOzellikleri = bulunanÜrünler[idx];


            StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/kullanıcı.txt");
            string nextline = reader.ReadLine();

            while (nextline != null)
            {
                kullanıcıtxt.Add(nextline);
                string[] kullanıcıBilgi = nextline.Split(' ');
                if (kullanıcıBilgi.Length < 12)
                {
                    if (kullanıcıBilgi[0].Equals(mevcutKullanıcı))
                    {

                        kullanıcıtxt.Add(ürünOzellikleri);
                    }

                }

                nextline = reader.ReadLine();
            }

            reader.Close();
            StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/kullanıcı.txt");
            for (int i = 0; i < kullanıcıtxt.Count; i++)
            {
                writer.WriteLine(kullanıcıtxt[i]);
            }
            writer.Close();
            Form2 eklendi = new Form2();
            Label eklendiLabel = new Label();
            eklendiLabel.Text = "Ürün Ekleme İşlemi Başarıyla Gerçekleştirildi!";
            eklendiLabel.Width = 500;
            eklendiLabel.Height = 500;
            eklendi.Scale(1.1F, 0.4F);
            eklendiLabel.Font = new Font(eklendiLabel.Font.FontFamily, eklendiLabel.Font.Size + 3f, eklendiLabel.Font.Style);
            eklendi.Controls.Add(eklendiLabel);
            eklendi.Show(); ;
        }

        private void fiyatListeleButon_click(TextBox min, TextBox max)
        {
            tümÜrünListesi.Clear();
            try//BÜTÜN ÜRÜNLER LİSTEYE ATILIYOR.
            {
                using (StreamReader sr = new StreamReader("C:/Users/ASUS/Desktop/market.txt"))
                {
                    string nextline = sr.ReadLine();
                    while (nextline != null)
                    {
                        if (kategoriler.ContainsKey(nextline))
                        {

                            nextline = sr.ReadLine();

                        }

                        List<string> sonrakiÜrün = new List<string>();
                        sonrakiÜrün.Add(nextline);



                        tümÜrünListesi.Add(sonrakiÜrün);


                        nextline = sr.ReadLine();
                    }


                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ee.Message);
            }
            int minFiyat = Int32.Parse(min.Text);
            int maxFiyat = Int32.Parse(max.Text);
            List<string> fiyatAralığındakiÜrünler = new List<string>();
            for (int i = 0; i < tümÜrünListesi.Count; i++)
            {
                for (int k = 0; k < tümÜrünListesi[i].Count; k++)
                {
                    string[] ürünOzellikleri = tümÜrünListesi[i][k].Split(' ');
                    string stringÜrünFiyat = ürünOzellikleri[5];
                    int ürünFiyat = Int32.Parse(stringÜrünFiyat);
                    if (ürünFiyat < maxFiyat && ürünFiyat > minFiyat)
                    {
                        fiyatAralığındakiÜrünler.Add(tümÜrünListesi[i][k]);

                    }
                }
            }
            Form2 ürünleriGoruntule = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            Button ürün;



            for (int i = 0; i < fiyatAralığındakiÜrünler.Count; i++)
            {

                ürün = new Button();
                string[] ozellikler = fiyatAralığındakiÜrünler[i].Split(' ');
                string text = "";
                string fiyat = "";
                for (int z = 0; z < ozellikler.Length; z++)
                {
                    if (z == 3 || z == 4)
                    {
                        continue;
                    }
                    else if (z == 5)
                    {
                        fiyat = ozellikler[z] + "TL ";

                    }
                    else
                    {
                        text += ozellikler[z] + " ";
                    }

                }
                text += fiyat;
                ürün.Text = text;
                var idx = i;
                ürün.Click += new EventHandler((sender2, e2) => satınAl(idx, fiyatAralığındakiÜrünler));
                ürün.Width = 750;
                layout.Controls.Add(ürün);
            }
            layout.RowCount = fiyatAralığındakiÜrünler.Count;
            layout.Width = 1000;
            layout.Height = 1000;
            ürünleriGoruntule.Controls.Add(layout);
            ürünleriGoruntule.Width = 1000;
            ürünleriGoruntule.Height = 750;
            ürünleriGoruntule.Show();
        }

        private void satınAl(int i, List<string> fiyatAralığındakiÜrünler)
        {
            string ürünOzellikleri = fiyatAralığındakiÜrünler[i];
            List<string> kullanıcıtxt = new List<string>();

            StreamReader reader = new StreamReader("C:/Users/ASUS/Desktop/kullanıcı.txt");
            string nextline = reader.ReadLine();

            while (nextline != null)
            {
                kullanıcıtxt.Add(nextline);
                string[] kullanıcıBilgi = nextline.Split(' ');
                if (kullanıcıBilgi.Length < 12)
                {
                    if (kullanıcıBilgi[0].Equals(mevcutKullanıcı))
                    {

                        kullanıcıtxt.Add(ürünOzellikleri);
                    }

                }

                nextline = reader.ReadLine();
            }

            reader.Close();
            StreamWriter writer = new StreamWriter("C:/Users/ASUS/Desktop/kullanıcı.txt");
            for (int k = 0; k < kullanıcıtxt.Count; k++)
            {
                writer.WriteLine(kullanıcıtxt[k]);
            }
            writer.Close();
            Form2 eklendi = new Form2();
            Label eklendiLabel = new Label();
            eklendiLabel.Text = "Ürün Ekleme İşlemi Başarıyla Gerçekleştirildi!";
            eklendiLabel.Width = 500;
            eklendiLabel.Height = 500;
            eklendi.Scale(1.1F, 0.4F);
            eklendiLabel.Font = new Font(eklendiLabel.Font.FontFamily, eklendiLabel.Font.Size + 3f, eklendiLabel.Font.Style);
            eklendi.Controls.Add(eklendiLabel);
            eklendi.Show(); ;
        }

        private void agacDengele_click(object sender, EventArgs e)
        {
            List<TreeNode> Nodes = new List<TreeNode>();
            foreach (Tree agac in kategoriler.Values)
            {
                Nodes.Clear();
                agac.toStack(agac.getRoot(), Nodes);
                agacDengele(0, Nodes.Count, agac, Nodes);
            }
        }









        private void araButon_click(object sender, EventArgs e) //MÜŞERİ GİRİŞİ -> ARA BUTONU  
        {
            int rowCount = 0;
            Form2 bulunanÜrünEkranı = null;
            List<string> anahtarKelimeler = null;
            List<List<String>> arananUrun = null;
            arananİçKelime = new List<List<string>>();
            string aranan = aramaCubugu.Text;
            anahtarKelimeler = new List<string>();
            foreach (string anahtar in urunAcıklaması.Keys)
            {

                anahtarKelimeler.Add(anahtar);
            }
            for (int o = 0; o < anahtarKelimeler.Count; o++)
            {
                if (anahtarKelimeler[o].Contains(aranan))
                {

                    rowCount++;
                    TableLayoutPanel layout = new TableLayoutPanel();

                    arananUrun = (List<List<String>>)urunAcıklaması[anahtarKelimeler[o]];




                    for (int k = 0; k < arananUrun.Count; k++)
                    {
                        if (!arananİçKelime.Contains(arananUrun[k]))
                        {
                            arananİçKelime.Add(arananUrun[k]);
                            bulunanÜrünler.Add(arananUrun[k]);
                        }

                    }
                    bulunanÜrünEkranı = new Form2();
                    Label ürünBilgi = new Label();
                    ürünBilgi.Width = 600;
                    ürünBilgi.Text = "Sepete Eklemek İstediğiniz Ürüne Tıklayın.";
                    bulunanÜrünEkranı.Width = 750;
                    layout.ColumnCount = 1;
                    layout.RowCount = rowCount;
                    layout.Dock = DockStyle.Fill;
                    bulunanÜrünEkranı.Controls.Add(layout);
                    Button sepeteEkle;



                    for (int i = 0; i < arananİçKelime.Count; i++)
                    {
                        string fiyat = "";
                        string text = "";
                        var idx = i;
                        List<String> stringListe;
                        stringListe = arananİçKelime[i];
                        sepeteEkle = new Button();
                        sepeteEkle.Click += new EventHandler((sender2, e2) => siparişiAl(idx, false));



                        sepeteEkle.Width = 700;
                        for (int z = 0; z < arananİçKelime[i].Count; z++)
                        {
                            if (z == 3 || z == 4)
                            {
                                continue;
                            }
                            else if (z == 5)
                            {
                                fiyat = stringListe[z] + "TL ";

                            }
                            else
                            {
                                text += stringListe[z] + " ";
                            }

                        }
                        text += fiyat;
                        sepeteEkle.Text = text;

                        layout.Controls.Add(ürünBilgi, 0, 0);
                        layout.Controls.Add(sepeteEkle, 0, i + 1);

                    }
                }



            }
            if (bulunanÜrünEkranı != null)
                bulunanÜrünEkranı.Show();
            else
            {
                Form2 dikkat = new Form2();
                Label uyarı = new Label();
                uyarı.Scale(2F, 2F);
                uyarı.Text = "ARADIĞINIZ ÜRÜN BULUNAMADI";
                dikkat.Controls.Add(uyarı);
                dikkat.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e) //KAYDOL
        {
            Form2 kayıtEkranı = new Form2();
            TableLayoutPanel layout = new TableLayoutPanel();
            layout.ColumnCount = 1;
            layout.RowCount = 7;

            layout.Width = 600;
            layout.Height = 800;

            kayıtEkranı.Scale(1.2F, 0.7F);

            Button kaydol = new Button();
            kaydol.Text = "Kaydol";
            kaydol.Click += new EventHandler(this.kaydol_click);

            kullanıcıAdı = new TextBox();
            kullanıcıAdı.Text = "Kullanıcı Adı Gir";
            kullanıcıAdı.ForeColor = System.Drawing.Color.Gray;
            kullanıcıAdı.Click += new EventHandler(this.kullanıcıAdı_click);

            şifre = new TextBox();
            şifre.Text = "Şifre Gir";
            şifre.ForeColor = System.Drawing.Color.Gray;
            şifre.Click += new EventHandler(this.şifre_click);

            yaş = new TextBox();
            yaş.Text = "Yaşınızı Giriniz";
            yaş.ForeColor = System.Drawing.Color.Gray;
            yaş.Click += new EventHandler(this.yaş_click);

            maaş = new TextBox();
            maaş.Text = "Maaşınızı Giriniz";
            maaş.ForeColor = System.Drawing.Color.Gray;
            maaş.Click += new EventHandler(this.maaş_click);

            cinsiyet = new TextBox();
            cinsiyet.Text = "Cinsiyetinizi Giriniz (kadın/erkek)";
            cinsiyet.ForeColor = System.Drawing.Color.Gray;
            cinsiyet.Click += new EventHandler(this.cinsiyet_click);

            şehir = new TextBox();
            şehir.Text = "Yaşadığınız şehri giriniz.";
            şehir.ForeColor = System.Drawing.Color.Gray;
            şehir.Click += new EventHandler(this.şehir_click);


            kullanıcıAdı.Width = 250;
            şifre.Width = 250;
            yaş.Width = 250;
            maaş.Width = 250;
            cinsiyet.Width = 250;
            şehir.Width = 250;

            layout.Controls.Add(kullanıcıAdı);
            layout.Controls.Add(şifre);
            layout.Controls.Add(yaş);
            layout.Controls.Add(maaş);
            layout.Controls.Add(cinsiyet);
            layout.Controls.Add(şehir);
            layout.Controls.Add(kaydol);
            kayıtEkranı.Controls.Add(layout);
            kayıtEkranı.Show();
        }

        private void kaydol_click(object sender, EventArgs e)
        {
            List<string> kullanıcıOzellikleri = new List<string>();
            //DOSAYAYA YAZDIRMA İŞLEMLERİ
            kullanıcıOzellikleri.Add(kullanıcıAdı.Text);
            kullanıcıOzellikleri.Add(şifre.Text);
            kullanıcıOzellikleri.Add(yaş.Text);
            kullanıcıOzellikleri.Add(maaş.Text);
            kullanıcıOzellikleri.Add(cinsiyet.Text);
            kullanıcıOzellikleri.Add(şehir.Text);
            dosyayaYaz(kullanıcıOzellikleri);


        }
    }

}


public class Node
{
    public List<String> iData;             // data item (key)
                                           // -------------------------------------------------------------
    public Node(List<String> key)           // constructor
    { iData = key; }
    // -------------------------------------------------------------
    public List<String> getKey()
    { return iData; }
    // -------------------------------------------------------------
    public void setKey(List<String> id)
    { iData = id; }
    // -------------------------------------------------------------
}  // end class Node
////////////////////////////////////////////////////////////////
public class Heap
{
    private Node[] heapArray;
    private int maxSize;           // size of array
    private int currentSize;       // number of nodes in array
                                   // -------------------------------------------------------------
    public Heap(int mx)            // constructor
    {
        maxSize = mx;
        currentSize = 0;
        heapArray = new Node[maxSize];  // create array
    }
    // -------------------------------------------------------------
    public bool isEmpty()
    { return currentSize == 0; }
    // -------------------------------------------------------------
    public bool insert(List<String> key) //SIRALI ATIYOR
    {
        if (currentSize == maxSize)
            return false;
        Node newNode = new Node(key);
        heapArray[currentSize] = newNode;
        trickleUp(currentSize++);
        return true;
    }  // end insert()
       // -------------------------------------------------------------
    public void trickleUp(int index)
    {
        int parent = (index - 1) / 2;
        Node bottom = heapArray[index];

        List<String> temp = heapArray[parent].getKey();
        List<String> temp2 = bottom.getKey();

        int fiyat = Int32.Parse(temp[5]);
        int fiyat2 = Int32.Parse(temp2[5]);

        while (index > 0 && fiyat > fiyat2)
        {
            heapArray[index] = heapArray[parent];  // move it down
            temp2 = heapArray[index].getKey();
            fiyat2 = Int32.Parse(temp2[5]);
            index = parent;
            parent = (parent - 1) / 2;
        }  // end while
        heapArray[index] = bottom;
    }  // end trickleUp()
       // -------------------------------------------------------------

    public void displayHeap()
    {
        Console.Write("heapArray: ");    // array format
        for (int m = 0; m < currentSize; m++)
            if (heapArray[m] != null)
                Console.Write(heapArray[m].getKey() + " ");
            else
                Console.Write("-- ");
        Console.WriteLine();
        // heap format
        int nBlanks = 32;
        int itemsPerRow = 1;
        int column = 0;
        int j = 0;                          // current item
        String dots = "...............................";
        Console.WriteLine(dots + dots);      // dotted top line

        while (currentSize > 0)              // for each heap item
        {
            if (column == 0)                  // first item in row?
                for (int k = 0; k < nBlanks; k++)  // preceding blanks
                    Console.Write(' ');
            // display item
            Console.Write(heapArray[j].getKey());

            if (++j == currentSize)           // done?
                break;

            if (++column == itemsPerRow)        // end of row?
            {
                nBlanks /= 2;                 // half the blanks
                itemsPerRow *= 2;             // twice the items
                column = 0;                   // start over on
                Console.WriteLine();         //    new row
            }
            else                             // next item on row
                for (int k = 0; k < nBlanks * 2 - 2; k++)
                    Console.Write(' ');     // interim blanks
        }  // end for
        Console.WriteLine("\n" + dots + dots); // dotted bottom line
    }  // end displayHeap()
       // -------------------------------------------------------------
    public List<List<string>> NtaneÜrünüSırala(int n)
    {
        List<List<string>> heapData = new List<List<string>>();
        for (int i = 0; i < n; i++)
        {
            heapData.Add(heapArray[i].iData);
        }
        return heapData;
    }
}  // end class Heap



class TreeNode
{
    public List<List<String>> data;
    public TreeNode leftChild;
    public TreeNode rightChild;
    public TreeNode displayNode() { return this; }
}

// Agaç Sınıfı
class Tree
{
    private TreeNode root;
    public int sayi = 0;
    public int düzey;
    public int[] elemanSayıları = new int[Int16.MaxValue];
    public int maxDepth = 0;
    public List<int> elemanSayılarıToplamları = new List<int>();


    public Tree() { root = null; }

    public TreeNode getRoot()
    { return root; }

    // Agacın preOrder Dolasılması

    public List<TreeNode> preOrder(TreeNode localRoot, List<TreeNode> nodelar)
    {
        if (localRoot != null)
        {
            TreeNode a = localRoot.displayNode();
            nodelar.Add(a);
            preOrder(localRoot.leftChild, nodelar);
            preOrder(localRoot.rightChild, nodelar);

        }
        return nodelar;
    }
    public List<TreeNode> düzeyListele(TreeNode etkin, List<TreeNode> nodelar)
    {
        if (etkin != null)
        {
            düzey = düzey + 1;
            düzeyListele(etkin.leftChild, nodelar);
            TreeNode a = etkin.displayNode();
            düzeyListele(etkin.rightChild, nodelar);
            düzey = düzey - 1;
        }
        return nodelar;
    }

    public void toStack(TreeNode localRoot, List<TreeNode> list)
    {
        if (localRoot != null)
        {
            toStack(localRoot.leftChild, list);
            list.Add(localRoot);
            toStack(localRoot.rightChild, list);

        }

    }

    // Agacın inOrder Dolasılması
    public List<TreeNode> inOrder(TreeNode localRoot, int düzey, List<TreeNode> nodelar)
    {

        if (localRoot != null)
        {
            düzey++;
            inOrder(localRoot.leftChild, düzey, nodelar);
            elemanSayıları[düzey]++;
            if (düzey > maxDepth)
                maxDepth = düzey;
            TreeNode a = localRoot.displayNode();
            nodelar.Add(a);
            inOrder(localRoot.rightChild, düzey, nodelar);

        }
        return nodelar;
    }



    // Agacın postOrder Dolasılması
    public List<TreeNode> postOrder(TreeNode localRoot, List<TreeNode> nodelar)
    {
        if (localRoot != null)
        {
            postOrder(localRoot.leftChild, nodelar);
            postOrder(localRoot.rightChild, nodelar);
            TreeNode a = localRoot.displayNode();
            nodelar.Add(a);
        }
        return nodelar;
    }

    // Agaca bir dügüm eklemeyi saglayan metot
    public void insert(List<List<String>> newdata)
    {
        TreeNode newNode = new TreeNode();
        newNode.data = newdata;
        if (root == null)
        {
            root = newNode;

        }
        else
        {
            TreeNode current = root;
            TreeNode parent;
            while (true)
            {
                parent = current;
                List<String> temp = newdata[0]; //dışarıdan gelen listenin içindeki ilk liste
                List<String> temp2 = current.data[0]; // node'daki dış listenin içindeki ilk liste
                if (temp[0].CompareTo(temp2[0]) < 0)
                {
                    current = current.leftChild;
                    if (current == null)
                    {

                        parent.leftChild = newNode;

                        return;
                    }
                }
                else
                {
                    current = current.rightChild;
                    if (current == null)
                    {
                        parent.rightChild = newNode;

                        return;
                    }
                }
            } // end while
        } // end else not root


    } // end insert()


}



