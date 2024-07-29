using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class JadwalPekerjaan
    {
        #region Data Members
        private int id;
        private Artis idArtis;
        private Eventss idEvent;
        private string tanggal;
        private string jam;
        private string deskripsi;
        #endregion

        #region Constructors
        public JadwalPekerjaan(int id, string tanggal, string jam, string deskripsi, Eventss idEvent, Artis idArtis)
        {
            this.id = id;
            this.idArtis = idArtis;
            this.idEvent = idEvent;
            this.tanggal = tanggal;
            this.Jam = jam;
            this.deskripsi = deskripsi;
        }
        #endregion

        #region Properties
        public int Id { get => id; set => id = value; }
        public Artis IdArtis { get => idArtis; set => idArtis = value; }
        public Eventss IdEvent { get => idEvent; set => idEvent = value; }
        public string Tanggal { get => tanggal; set => tanggal = value; }
        public string Deskripsi { get => deskripsi; set => deskripsi = value; }
        public string Jam { get => jam; set => jam = value; }
        #endregion

        #region Methods
        //public static List<JadwalPekerjaan> BacaData(string kriteria, string nilaiKriteria)
        //{
        //    string sql = "";
        //    if (kriteria == "")
        //    {
        //        sql = "select * from jadwal_pekerjaans";
        //    }
        //    else
        //    {
        //        sql = "select * from jadwal_pekerjaans" + " where " + kriteria + "='" + nilaiKriteria + "'";
        //    }

        //    MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

        //    List<JadwalPekerjaan> listJadwalPekerjaan = new List<JadwalPekerjaan>();

        //    while (hasil.Read() == true)
        //    {
        //        Artis a = new Artis();
        //        int idArtis = int.Parse(hasil.GetValue(5).ToString());
        //        List<Artis> artis = new List<Artis>();
        //        artis = Artis.BacaDataSatu(idArtis.ToString());
        //        a = artis[0];
        //        Eventss e = new Eventss();
        //        int idEvent = int.Parse(hasil.GetValue(4).ToString());
        //        List<Eventss> ev = new List<Eventss>();
        //        ev = Eventss.BacaDataSatu(idEvent.ToString());
        //        e = ev[0];
        //        JadwalPekerjaan jp = new JadwalPekerjaan(int.Parse(hasil.GetValue(0).ToString()),
        //            DateTime.Parse(hasil.GetValue(1).ToString()),
        //            DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), e, a);
        //        listJadwalPekerjaan.Add(jp);
        //    }

        //    return listJadwalPekerjaan;
        //}

        public static void TambahData(JadwalPekerjaan jp)
        {
            string sql = "insert into jadwal_pekerjaans(tanggal, jam, deskripsi, idArtis, idEvents) values " +
                "('" + jp.Tanggal + "','" + jp.Jam + "','" + jp.Deskripsi + "','" +
                         jp.IdArtis.IdArtis + "','" + jp.IdEvent.Id + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UpdateData(JadwalPekerjaan jp)
        {
            string sql = "update jadwal_pekerjaans set tanggal='" + jp.Tanggal +
            "','jam='" + jp.Jam + "'deskripsi='" + jp.Deskripsi + "'idArtis='" + jp.IdArtis + "' and " +
            "idEvents='" + jp.IdEvent + "' and idJadwal_pekerjaans-'" + jp.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Boolean HapusData(JadwalPekerjaan j)
        {
            string sql = "delete from jadwal_pekerjaans where idJadwal_pekerjaans = '" + j.Id + "'";
            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(sql);
            if (jumlahDataBerubah == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //public static List<JadwalPekerjaan> BacaDataSatu(string nilaiKriteria)
        //{
        //    string sql = "select * from jadwal_pekerjaans where idArtis='" + nilaiKriteria + "'";
        //    MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

        //    List<JadwalPekerjaan> listJadwalPekerjaan = new List<JadwalPekerjaan>();

        //    while (hasil.Read() == true)
        //    {
        //        Artis a = new Artis();
        //        int idArtis = int.Parse(hasil.GetValue(5).ToString());
        //        List<Artis> artis = new List<Artis>();
        //        artis = Artis.BacaDataSatu(idArtis.ToString());
        //        a = artis[0];
        //        Eventss e = new Eventss();
        //        int idEvent = int.Parse(hasil.GetValue(4).ToString());
        //        List<Eventss> ev = new List<Eventss>();
        //        ev = Eventss.BacaDataSatu(idEvent.ToString());
        //        e = ev[0];
        //        JadwalPekerjaan jp = new JadwalPekerjaan(int.Parse(hasil.GetValue(0).ToString()),
        //            DateTime.Parse(hasil.GetValue(1).ToString()),
        //            DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), e, a);
        //        listJadwalPekerjaan.Add(jp);
        //    }

        //    return listJadwalPekerjaan;
        //}


        public static List<JadwalPekerjaan> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from jadwal_pekerjaans";
            }
            else
            {
                sql = "select * from jadwal_pekerjaans" + " where " + kriteria + "='" + nilaiKriteria + "'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<JadwalPekerjaan> listJadwalPekerjaan = new List<JadwalPekerjaan>();

            while (hasil.Read() == true)
            {
                Artis a = new Artis();
                int idArtis = int.Parse(hasil.GetValue(5).ToString());
                List<Artis> artis = new List<Artis>();
                artis = Artis.BacaDataSatu(idArtis.ToString());
                a = artis[0];
                Eventss e = new Eventss();
                int idEvent = int.Parse(hasil.GetValue(4).ToString());
                List<Eventss> ev = new List<Eventss>();
                ev = Eventss.BacaDataSatu(idEvent.ToString());
                e = ev[0];
                JadwalPekerjaan jp = new JadwalPekerjaan(int.Parse(hasil.GetValue(0).ToString()),
                    hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), e, a);
                listJadwalPekerjaan.Add(jp);
            }

            return listJadwalPekerjaan;
        }
        #endregion

    }
}
