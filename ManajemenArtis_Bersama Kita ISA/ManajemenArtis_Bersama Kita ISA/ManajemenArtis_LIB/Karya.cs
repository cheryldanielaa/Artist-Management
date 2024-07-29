using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Karya
    {
        #region data members
        private int idKarya;
        private string judul;
        private DateTime tanggal;
        private string jenisKarya;
        private Artis artis;
        #endregion

        #region constructors
        public Karya(int idKarya, string judul, DateTime tanggal, string jenisKarya, Artis artis)
        {
            this.IdKarya = idKarya;
            this.Judul = judul;
            this.Tanggal = tanggal;
            this.JenisKarya = jenisKarya;
            this.Artis = artis;
        }

        public Karya()
        {
            this.IdKarya = idKarya;
            this.Judul = judul;
            this.Tanggal = tanggal;
            this.JenisKarya = jenisKarya;
            this.Artis = artis;
        }
        #endregion

        #region properties
        public int IdKarya { get => idKarya; set => idKarya = value; }
        public string Judul { get => judul; set => judul = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string JenisKarya { get => jenisKarya; set => jenisKarya = value; }
        public Artis Artis { get => artis; set => artis = value; }
        #endregion

        #region methods
        public static List<Karya> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from karyas";
            }
            else
            {
                sql = "select * from karyas where " + kriteria + " Like '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Karya> listKarya = new List<Karya>();

            while (hasil.Read() == true)
            {
                Artis a = new Artis();
                int idArtis = int.Parse(hasil.GetValue(4).ToString());
                List<Artis> artis = new List<Artis>();
                artis = Artis.BacaDataSatu(idArtis.ToString());
                a = artis[0];
                Karya k = new Karya();
                k.IdKarya = int.Parse(hasil.GetValue(0).ToString());
                k.Judul = hasil.GetValue(1).ToString();
                k.Tanggal = DateTime.Parse(hasil.GetValue(2).ToString());
                k.JenisKarya = hasil.GetValue(3).ToString();
                k.Artis = a;
                listKarya.Add(k);
            }
            return listKarya;
        }

        public static void TambahData(Karya k)
        {
            string sql = "INSERT INTO karyas (`idKaryas`, `judul`, `tanggal`, `jenis_karya`, `idArtis`)" +
            "VALUES ('" + k.IdKarya + "', '" + k.judul + "', '" + k.Tanggal.ToString("yyyy-MM-dd") + "', '" + k.JenisKarya + "', '" + k.Artis.IdArtis + "');";

            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahData(Karya k, string kode)
        {
            string sql = "";
            sql = "UPDATE karyas SET judul ='" + k.Judul + "', tanggal='" + k.Tanggal.ToString("yyyy-MM-dd") + "' WHERE idKaryas='" + kode + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Karya AmbilDataByKode(string kode)
        {
            string sql = "select * from karyas where idKaryas = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Artis a = new Artis();
                int idArtis = int.Parse(hasil.GetValue(4).ToString());
                List<Artis> artis = new List<Artis>();
                artis = Artis.BacaDataSatu(idArtis.ToString());
                a = artis[0];
                Karya k = new Karya();
                k.IdKarya = int.Parse(hasil.GetValue(0).ToString());
                k.Judul = hasil.GetValue(1).ToString();
                k.Tanggal = DateTime.Parse(hasil.GetValue(2).ToString());
                k.JenisKarya = hasil.GetValue(3).ToString();
                k.Artis = a;
                return k;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
