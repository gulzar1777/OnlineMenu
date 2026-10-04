using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace OnlineMenu
{
    public partial class Form1 : Form
    {
        class Yemek
        {
            public string Ad { get; set; }
            public decimal Qiymet { get; set; }

            public Yemek(string ad, decimal qiymet)
            {
                Ad = ad;
                Qiymet = qiymet;
            }

            public override string ToString()
            {
                return Ad + " - " + Qiymet.ToString("0.00") + " AZN";
            }
        }

        List<Yemek> sebet = new List<Yemek>();

        public Form1()
        {
            InitializeComponent();

            btnYemek1.Click += (s, e) => YemekElaveEt("Burger", 6.00m);
            btnYemek2.Click += (s, e) => YemekElaveEt("Tort", 5.00m);
            btnYemek3.Click += (s, e) => YemekElaveEt("İçki", 3.00m);

            btnYemek4.Click += (s, e) => YemekElaveEt("Hot-Dog", 4.00m);
            btnYemek5.Click += (s, e) => YemekElaveEt("Pizza", 7.00m);
            btnYemek6.Click += (s, e) => YemekElaveEt("Şirə", 3.00m);

            btnYemek7.Click += (s, e) => YemekElaveEt("Sendviç", 4.50m);
            btnYemek8.Click += (s, e) => YemekElaveEt("Keks", 3.50m);
            btnYemek9.Click += (s, e) => YemekElaveEt("Peçenye", 3.00m);

            btnSebetdenSil.Click += btnSebetdenSil_Click;
            btnYenile.Click += btnYenile_Click;
            btnYekunHesab.Click += btnYekunHesab_Click;
            btnHesabla.Click += btnHesabla_Click;
            btnTemizle.Click += btnTemizle_Click;
        }

        private void YemekElaveEt(string ad, decimal qiymet)
        {
            Yemek yemek = new Yemek(ad, qiymet);

            sebet.Add(yemek);
            lstSebet.Items.Add(yemek);
        }

        private void btnSebetdenSil_Click(object sender, EventArgs e)
        {
            if (lstSebet.SelectedIndex == -1)
            {
                MessageBox.Show("Səbətdən silmək üçün yemək seçin!");
                return;
            }

            int index = lstSebet.SelectedIndex;
            Yemek yemek = sebet[index];

            sebet.RemoveAt(index);
            lstSebet.Items.RemoveAt(index);

            MessageBox.Show(yemek.Ad + " səbətdən silindi");
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            DialogResult cavab = MessageBox.Show(
                "Xanalar sıfırlansınmı?",
                "Yenilə",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (cavab == DialogResult.Yes)
            {
                sebet.Clear();
                lstSebet.Items.Clear();

                txtMebleg.Clear();
                txtQaliq.Clear();
                txtHesab.Clear();
            }
        }

        private void btnYekunHesab_Click(object sender, EventArgs e)
        {
            if (sebet.Count == 0)
            {
                MessageBox.Show("Səbətdə yemək yoxdur!");
                return;
            }

            decimal cem = 0;

            foreach (Yemek yemek in sebet)
            {
                cem += yemek.Qiymet;
            }

            txtHesab.Text = cem.ToString("0.00") + " AZN";
        }

        private void btnHesabla_Click(object sender, EventArgs e)
        {
            if (sebet.Count == 0)
            {
                MessageBox.Show("Səbətdə yemək yoxdur!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtHesab.Text))
            {
                MessageBox.Show("Əvvəlcə Yekun hesab düyməsinə basın!");
                return;
            }

            decimal mebleg;

            string meblegText = txtMebleg.Text.Replace("AZN", "").Trim();

            if (!decimal.TryParse(
                meblegText,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out mebleg))
            {
                if (!decimal.TryParse(
                    meblegText,
                    NumberStyles.Any,
                    new CultureInfo("az-Latn-AZ"),
                    out mebleg))
                {
                    MessageBox.Show("Daxil edilən məbləğ düzgün deyil!");
                    return;
                }
            }

            decimal hesab = 0;

            foreach (Yemek yemek in sebet)
            {
                hesab += yemek.Qiymet;
            }

            if (mebleg < hesab)
            {
                MessageBox.Show("Daxil edilən məbləğ hesabdan azdır");
                return;
            }

            decimal qaliq = mebleg - hesab;

            txtQaliq.Text = qaliq.ToString("0.00") + " AZN";
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtMebleg.Clear();
            txtQaliq.Clear();
        }
    }
}