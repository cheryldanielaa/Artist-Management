using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Eventss
    {
        #region data members
        private int id;
        private string nama;
        private DateTime tanggalAwal;
        private DateTime tanggalAkhir;
        #endregion

        #region constructor
        public Eventss(int id, string nama, DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            this.Id = id;
            this.Nama = nama;
            this.TanggalAwal = tanggalAwal;
            this.TanggalAkhir = tanggalAkhir;
        }

        public Eventss()
        {
            this.Id = id;
            this.Nama = nama;
            this.TanggalAwal = tanggalAwal;
            this.TanggalAkhir = tanggalAkhir;
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public DateTime TanggalAwal { get => tanggalAwal; set => tanggalAwal = value; }
        public DateTime TanggalAkhir { get => tanggalAkhir; set => tanggalAkhir = value; }
        #endregion

        #region methods
        public static void TambahData(Eventss e)
        {
            string sql = "insert into events(nama, tanggal_awal, tanggal_akhir) values" +
                "('" + e.Nama + "','" + e.TanggalAwal.ToString("yyyy-MM-dd") + "','" + e.TanggalAkhir.ToString("yyyy-MM-dd") + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahData(Eventss e)
        {
            string sql = "update events set nama='" + e.Nama + "', tanggal_awal='" + e.TanggalAwal.ToString("yyyy-MM-dd") +
                "', tanggal_akhir='" + e.TanggalAkhir.ToString("yyyy-MM-dd") + "' where idEvents='" + e.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Boolean HapusData(Eventss e)
        {
            string perintah = "delete from events where idEvents = '" + e.Id + "'";
            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(perintah);
            Boolean status;
            if (jumlahDataBerubah == 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        public static List<Eventss> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from events";
            }
            else
            {
                sql = "select * from events where " + kriteria + "='" + nilaiKriteria + "'";
            }


            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Eventss> listEvent = new List<Eventss>();
            while (hasil.Read() == true)
            {
                Eventss e = new Eventss(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), DateTime.Parse(hasil.GetValue(3).ToString()));
                listEvent.Add(e);
            }
            return listEvent;
        }

        public static List<Eventss> BacaDataSatu(string nilaiKriteria)
        {

            string sql = "select * from events where idEvents='" + nilaiKriteria + "'";



            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Eventss> listEvent = new List<Eventss>();
            while (hasil.Read() == true)
            {
                Eventss e = new Eventss(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), DateTime.Parse(hasil.GetValue(3).ToString()));
                listEvent.Add(e);
            }
            return listEvent;
        }


        public static Eventss AmbilDataByKode(string kode)
        {
            string sql = "select * from events where idEvents = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Eventss e = new Eventss(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), DateTime.Parse(hasil.GetValue(3).ToString()));
                return e;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return nama;
        }
        #endregion
    }
}
