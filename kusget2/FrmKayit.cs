using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kusget2
{
    public partial class FrmKayit : Form
    {
        public FrmKayit()
        {
            InitializeComponent();
        }
        dbSecimEntities db = new dbSecimEntities();
        private void txtOyKullananTC_EditValueChanged(object sender, EventArgs e)
        {
            string kullananTC = txtOyKullananTC.Text;
            int len = kullananTC.Length;
            if (len < 11 || len > 11)
            {
                simpleButton1.Enabled = false;
                label1.Text = "....";
            }
            if (len == 11)
            {
                var kullanan = (from x in db.tblSecmen where x.TCno == txtOyKullananTC.Text select x).FirstOrDefault();
                if (kullanan == null)
                {
                    MessageBox.Show("Bu TC Kimlik numarası seçmen listesinde kayıtlı olmadığı için oy kullanamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    simpleButton1.Enabled = false;
                }
                else
                {
                    label1.Text = kullanan.adi.ToString();
                    var aramaKullanan = (from x in db.tblOylar where x.kullananTC == kullananTC select x).FirstOrDefault();
                    if (aramaKullanan != null)
                    {
                        var arama = (from x in db.tblSecmen where x.TCno == kullananTC select x).FirstOrDefault();
                        simpleButton1.Enabled = false;
                        if (aramaKullanan.vekalet == true)
                        {
                            MessageBox.Show(kullananTC + " TC kimlik numaralı " + aramaKullanan.kullananAdi + " adına " + aramaKullanan.tarih + " 'te " + aramaKullanan.vekilAdi + " vekaleten oy kullandığıiçin tekrar oy kullanmasına izin verilmemektedir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        else
                        {
                            MessageBox.Show(kullananTC + " TC kimlik numaralı kişi " + aramaKullanan.tarih + " te oy kullandığıiçin tekrar oy kullanmasına izin verilmemektedir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {

                        simpleButton1.Enabled = true;
                    }

                }






            }
        }

        private void txtVekilTC_EditValueChanged(object sender, EventArgs e)
        {
            txtOyKullananTC.BackColor = Color.White;
            int kullananTClen = txtOyKullananTC.Text.Length;
            string kullananTC = txtOyKullananTC.Text;
            string vekilTC = txtVekilTC.Text;
            int len = vekilTC.Length;
            if (len < 11 || len > 11)
            {
                simpleButton1.Enabled = false;
                label2.Text = "....";
            }
            if (len == 11)
            {
                if (kullananTClen != 11)
                {
                    txtOyKullananTC.BackColor = Color.Red;
                    MessageBox.Show("Adına oy kullanacak kişinin TC kimlik numarasını oy kullanan kutusuna giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    simpleButton1.Enabled = false;
                }
                else
                {
                    if (kullananTC != vekilTC)
                    {

                        txtOyKullananTC.BackColor = Color.White;
                        var vekil = (from x in db.tblSecmen where x.TCno == txtVekilTC.Text select x).FirstOrDefault();
                        if (vekil == null)
                        {
                            MessageBox.Show("Bu TC Kimlik numarası seçmen listesinde kayıtlı olmadığı için oy kullanamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            simpleButton1.Enabled = false;
                        }
                        else
                        {
                            label2.Text = vekil.adi.ToString();
                            var aramaKullanan = (from x in db.tblOylar where x.vekilTC == vekilTC select x).FirstOrDefault();
                            if (aramaKullanan != null)
                            {
                                var arama = (from x in db.tblSecmen where x.TCno == vekilTC select x).FirstOrDefault();
                                simpleButton1.Enabled = false;
                                if (aramaKullanan.vekalet == true)
                                {
                                    MessageBox.Show(vekilTC + " TC kimlik numaralı " + aramaKullanan.kullananAdi + " adına " + aramaKullanan.tarih + " 'te " + aramaKullanan.vekilAdi + " vekaleten oy kullandığıiçin tekrar oy kullanmasına izin verilmemektedir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                                else
                                {
                                    MessageBox.Show(vekilTC + " TC kimlik numaralı kişi " + aramaKullanan.tarih + " te oy kullandığıiçin tekrar oy kullanmasına izin verilmemektedir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            else
                            {

                                simpleButton1.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Oy kullanan ve vekil TC Kimlik numaraları aynı olamaz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }

        private void rdVekalet_CheckedChanged(object sender, EventArgs e)
        {
            if (rdVekalet.Checked == true)
            {
                groupControl1.Visible = true;
            }

            else
            {
                groupControl1.Visible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            tblOylar oylar = new tblOylar();
            oylar.kullananTC = txtOyKullananTC.Text;
            oylar.kullananAdi = label1.Text;
            if (rdVekalet.Checked)
            {
                oylar.vekilTC = txtVekilTC.Text;
                oylar.vekilAdi = label2.Text;
                oylar.vekalet = true;
            }          
            else
            {
                oylar.vekalet = false;
            }
            oylar.tarih = DateTime.Now;
            db.tblOylar.Add(oylar);
            db.SaveChanges();
            MessageBox.Show("Kayıt işlemi tamamlandı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtOyKullananTC.Clear();
            txtVekilTC.Clear();
        }
    }
}
