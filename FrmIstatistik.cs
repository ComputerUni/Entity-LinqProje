using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityProjeUygulama
{
    public partial class FrmIstatistik : Form
    {
        public FrmIstatistik()
        {
            InitializeComponent();
        }
        DbEntityUrunEntities db = new DbEntityUrunEntities();
        private void FrmIstatistik_Load(object sender, EventArgs e)
        {
            LblToplamKategori.Text = db.Tbl_Kategori.Count().ToString();
            LblToplamUrun.Text = db.Tbl_Urun.Count().ToString();
            LblAktifMusteri.Text = db.Tbl_Musteri.Count(x=>x.Durum == true).ToString();
            LblPasifMusteri.Text = db.Tbl_Musteri.Count(x=>x.Durum == false).ToString();
            LblToplamStok.Text = db.Tbl_Urun.Sum(x => x.Stok).ToString();
            LblKasadakiTutar.Text = db.Tbl_Satis.Sum(x => x.Fiyat).ToString() + " TL";
            LblEnYuksekUrun.Text = (from x in db.Tbl_Urun orderby x.Fiyat descending select x.UrunAd).FirstOrDefault();
            LblDusukFiyat.Text = (from x in db.Tbl_Urun orderby x.Fiyat ascending select x.UrunAd).FirstOrDefault();
            LblBeyazEsya.Text = db.Tbl_Urun.Count(x => x.Kategori == 1).ToString();
            LblToplamBuzdolabi.Text = db.Tbl_Urun.Count(x => x.UrunAd == "Buzdolabi").ToString();
            LblSehirSayisi.Text = (from x in db.Tbl_Musteri select x.Sehir).Distinct().Count().ToString();
            LblMarka.Text = db.MarkaGetir().FirstOrDefault();
        }
    }
}
