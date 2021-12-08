using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        DataGridViewCellStyle renk = new DataGridViewCellStyle();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //************************************************************************=---- [FORM LOAD] =----
            textBox3.Visible = false;
            label8.Visible = false;
            t4.Visible = false;

            t2.ColumnCount = 6;
            // t2.Columns[0].Name = Convert.ToString("Id");
            // t2.Columns[1].Name = Convert.ToString("Max");

            //listBox1.Items.Add("a1");
            //listBox1.Items.Add("a2");
            //listBox1.Items.Add("a3");


            ////listBox2.Items.Add("Çok iyi");
            //listBox2.Items.Add("düşük");
            //listBox2.Items.Add("Orta");
            //listBox2.Items.Add("yüksek");
            ////listBox2.Items.Add("Cok Kötü");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //**********************************************************************=----[ KAYDET BUTONU] =----
                int satir, sutun, p, o;

                sutun = listBox2.Items.Count;
                satir = listBox1.Items.Count;

                tablom.Rows.Clear();//1. TABLOYU TEMİZLE
                t2.Rows.Clear();
                t3.Rows.Clear();
                t4.Rows.Clear();

                tablom.ColumnCount = sutun + 1;
                t4.ColumnCount = sutun + 1;

                for (p = 1; p <= sutun; p++) //sutun olustur
                {
                    tablom.Columns[p].Name = Convert.ToString(listBox2.Items[p - 1].ToString());
                    t4.Columns[p].Name = Convert.ToString(listBox2.Items[p - 1].ToString());
                    tablom.Columns[p].Width = 50;
                    t4.Columns[p].Width = 50;
                }
                for (o = 1; o < satir + 1; o++) //satır olustur
                {
                    tablom.Rows.Add(listBox1.Items[o - 1].ToString());
                    tablom.Rows[o - 1].Cells[0].Style.BackColor = Color.Aquamarine;
                    t4.Rows.Add(listBox1.Items[o - 1].ToString());
                }

                //#=========================== TABLO AYARLARI ===================================
                tablom.Columns[0].HeaderCell.Style.BackColor = Color.Blue;  //TABLO RENGİ
                tablom.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                tablom.Columns[1].HeaderCell.Style.BackColor = Color.Magenta;
                tablom.Columns[0].ReadOnly = true; //1.sütunu sadece okunabilir yapan
                tablom.Rows[0].Cells[1].Selected = true; //seçili helen hücre
                tablom.Rows[0].Cells[0].Selected = false; //seçili helen hücre
                //#==============================================================================
                tablom.Sort(ilksutun, ListSortDirection.Ascending);
            }
            catch 
            {
                MessageBox.Show("Lütfen Değer Giriniz", "Hata");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //********************************************************************************=[EKLE BUTONU ]=------
            if (textBox1.Text != "")
            {
                listBox1.Items.Add(textBox1.Text);
            }
            if (textBox2.Text != "")
            {
                listBox2.Items.Add(textBox2.Text);
            }
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //********************************************************************************=[LİSTBOX TEMİZLE BUTONU ]=------
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //********************************************************************************=[TEMİZLE BUTONU ]=------
            t2.Rows.Clear();
            t3.Rows.Clear();
            t4.Rows.Clear();
            tablom.Rows.Clear();
            label3.Text = "İyimserlik :";
            label4.Text = "Kötümserlik :";
            label5.Text = "Pişmanlık :";
            label6.Text = "Hurwicz :";
            label7.Text = "Laplace :";
        }
        private void button3_Click(object sender, EventArgs e)
        {   
            //********************************************************************************=[HESAPLA BUTONU ]=------
             try
            {
            t2.Rows.Clear();
            t3.Rows.Clear();
            t2.Sort(Alternatif, ListSortDirection.Ascending);

            int satir2, sutun2;
            satir2 = tablom.Rows.Count; 
            sutun2 = tablom.Columns.Count - 1; //6-1=  5

            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&[ Tablo 2: Satır Mak ve Min Tablosu Oluşturulması ]&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&           

            for (int b = 0; b < satir2; b++) // ==== "b" Satır Ara
            {
                double[] tablo2 = new double[sutun2];
                t2.Rows.Add(listBox1.Items[b].ToString());
                
                for (int a = 0; a < sutun2; a++)// ==== "a" Sütun ara
                {
                    tablo2[a] = Convert.ToDouble(tablom.Rows[b].Cells[a + 1].Value); //HÜCRE DEĞERİNİ AL                                           
                }
                Array.Sort(tablo2);
                t2.Rows[b].Cells[1].Value = tablo2[tablo2.Length - 1];     //Max 
                t2.Rows[b].Cells[2].Value = tablo2[0];     //Min 
            }
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&[ Tablo 3: Max Sütunlar Tablosu Oluşturulması ]&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            
            for (int m = 0; m < sutun2; m++) // ==== "m" Sütun Ara
            {
                double[] tablo3 = new double[satir2];
                t3.Rows.Add(tablom.Columns[m + 1].Name);              
                for (int n = 0; n < satir2; n++)// ==== "n" Satir ara
                {
                    tablo3[n] = Convert.ToDouble(tablom.Rows[n].Cells[m + 1].Value);
                }
                Array.Sort(tablo3);
                t3.Rows[m].Cells[1].Value = tablo3[tablo3.Length - 1]; //Max
            }
            //t3.Sort(sutunmax, ListSortDirection.Descending);
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

            // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<[HESAPLAMALARIN YAPILMASI]
            
                if (checkBox1.Checked)//*******************************-[ İYİMSERLİK ]-***************************************
                {
                    t2.Sort(Maksi, ListSortDirection.Descending);
                    label3.Text = "İyimserlik : " + Convert.ToString(t2.Rows[0].Cells[0].Value);
                    t2.Sort(Alternatif, ListSortDirection.Ascending);
                }
                if (checkBox2.Checked)//*******************************-[ KÖTÜMSERLİK ]-***************************************
                {
                    t2.Sort(Mini, ListSortDirection.Descending);
                    label4.Text = "Kötümserlik : " + Convert.ToString(t2.Rows[0].Cells[0].Value);
                    t2.Sort(Alternatif, ListSortDirection.Ascending);
                }
                if (checkBox3.Checked)//*****************************-[ PİŞMANLIK-SAVAGE ]-************************************
                {
                    double sonucsavage, hucret3, hucretablom;
                    for (int c = 0; c < sutun2; c++) //  ==== "c" Sütun Ara
                    {
                        for (int v = 0; v < satir2; v++)// ==== "v" Satır ara
                        {
                            hucret3 = Convert.ToDouble(t3.Rows[c].Cells[1].Value);
                            hucretablom = Convert.ToDouble(tablom.Rows[v].Cells[c + 1].Value);
                            sonucsavage = hucret3 - hucretablom;
                            t4.Rows[v].Cells[c + 1].Value = sonucsavage;
                        }
                    }

                    for (int z = 0; z < satir2; z++) // ==== "z" Satır Ara
                    {
                        double[] savagedizi = new double[sutun2];
                        for (int x = 0; x < sutun2; x++)// ==== "x" Sütun ara
                        {
                            savagedizi[x] = Convert.ToDouble(t4.Rows[z].Cells[x + 1].Value); //HÜCRE DEĞERİNİ AL                                           
                        }
                        Array.Sort(savagedizi);
                        t2.Rows[z].Cells[5].Value = savagedizi[savagedizi.Length - 1];     //Max 
                    }
                    t2.Sort(savage, ListSortDirection.Descending);
                    label5.Text = "Savage : " + Convert.ToString(t2.Rows[0].Cells[0].Value);
                    t2.Sort(Alternatif, ListSortDirection.Ascending);
                }

                if (checkBox4.Checked)//*********************************-[ HURWİCZ ]-******************************************
                {
                    if (textBox3.Text != "")
                    {
                        double sonuchurwicz, hucremax, hucremin, alfa, alfa1;
                        alfa = Convert.ToDouble(textBox3.Text);
                        alfa1 = 1 - alfa;

                        for (int h = 0; h <= t2.Rows.Count - 1; h++) //  ==== "h" Satır Ara
                        {
                            hucremax = Convert.ToDouble(t2.Rows[h].Cells[1].Value); //Max hücre değerini al
                            hucremin = Convert.ToDouble(t2.Rows[h].Cells[2].Value); //Min hücre değerini al
                            sonuchurwicz = (alfa * hucremax) + (alfa1 * hucremin);
                            t2.Rows[h].Cells[3].Value = sonuchurwicz;
                        }
                        t2.Sort(hurwicz, ListSortDirection.Descending);
                        label6.Text = "Hurwicz : " + Convert.ToString(t2.Rows[0].Cells[0].Value);
                        t2.Sort(Alternatif, ListSortDirection.Ascending);
                    }
                    else
                    {
                        MessageBox.Show("Alfa değeri giriniz", "Hata");
                    }
                }
                if (checkBox5.Checked)//***********************************-[ LAPLACE ]-****************************************
                {
                    double laplacehucre, sonuclaplace, payda, bolum;
                    payda = Convert.ToDouble(sutun2);
                    bolum = 1 / payda;
                    for (int k = 0; k < satir2; k++) // ==== "k" Satır Ara
                    {
                        sonuclaplace = 0;
                        for (int L = 0; L < sutun2; L++)// ==== "L" Sutun ara
                        {
                            laplacehucre = Convert.ToDouble(tablom.Rows[k].Cells[L + 1].Value); //HÜCRE DEĞERİNİ AL 
                            sonuclaplace = sonuclaplace + (laplacehucre * bolum);
                        }
                        t2.Rows[k].Cells[4].Value = sonuclaplace;
                    }
                    t2.Sort(laplace, ListSortDirection.Descending);
                    label7.Text = "Laplace : " + Convert.ToString(t2.Rows[0].Cells[0].Value);
                    t2.Sort(Alternatif, ListSortDirection.Ascending);
                }
                t2.Sort(Alternatif, ListSortDirection.Ascending); //*************------ En Son tekrar A-Z Sırala
            }
            catch
            {
                MessageBox.Show("Tablo boşken hesaplama yapılamaz.","Hata");
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //*********************************************************************-[ ALFA DEĞERİ GÖRÜNÜRLÜĞÜ ]-********
            if (textBox3.Visible == false)
            {
                textBox3.Visible = true;
                label8.Visible = true;
            }
            else
            {
                textBox3.Visible = false;
                label8.Visible = false;
            }    
        }
        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex!=0 && listBox2.SelectedIndex != -1)
            {
                string alt, ust;
                int a = listBox2.SelectedIndex;
                alt = listBox2.Items[a].ToString();
                ust = listBox2.Items[a - 1].ToString();
                listBox2.Items[a] = ust; //eskiye
                listBox2.Items[a - 1] = alt;
                listBox2.SelectedIndex = a-1;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != listBox2.Items.Count - 1 && listBox2.SelectedIndex != -1)
            {
                string alt2, ust2;
                int b = listBox2.SelectedIndex;
                 alt2 = listBox2.Items[b].ToString();
                 ust2 = listBox2.Items[b + 1].ToString();
                listBox2.Items[b] = ust2; //eskiye
                listBox2.Items[b + 1] = alt2;
                listBox2.SelectedIndex = b+1;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            listBox1.ClearSelected();
            listBox2.ClearSelected();
        }

        
    }
}
