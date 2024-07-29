using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Kontrak
    {
        #region datamembers
        private int id;
        private string deskripsi; //DESKRIPSI KEGIATAN ARTIS
        private DateTime tanggalPengajuan;
        private string nominal;
        private string status;
        private DateTime tanggalMulai;
        private DateTime tanggalBerakhir;
        private Manajer idManajer;
        private Eventss idEvent;
        private Artis idArtis;
        private byte[] tandaTanganArtis;
        private byte[] tandaTanganEvent;
        private byte[] tandaTanganManajer;
        #endregion

        #region constructors
        public Kontrak(int id, string deskripsi, DateTime tanggalPengajuan,
            string nominal, string status,
            DateTime tanggalMulai, DateTime tanggalBerakhir,
            Manajer idManajer, Eventss idEvent, Artis idArtis, byte[] tandaTanganEvent, byte[] tandaTanganManajer)
        {
            this.Id = id;
            this.Deskripsi = deskripsi;
            this.TanggalPengajuan = tanggalPengajuan;
            this.Nominal = nominal;
            this.Status = status;
            this.TanggalMulai = tanggalMulai;
            this.TanggalBerakhir = tanggalBerakhir;
            this.IdManajer = idManajer;
            this.IdEvent = idEvent;
            this.IdArtis = idArtis;
            this.TandaTanganArtis = tandaTanganArtis;
            this.TandaTanganEvent = tandaTanganEvent;
            this.TandaTanganManajer = tandaTanganManajer;
        }

        public Kontrak(int id, string deskripsi, DateTime tanggalPengajuan,
            string nominal, string status,
            DateTime tanggalMulai, DateTime tanggalBerakhir,
            Manajer idManajer, Eventss idEvent, Artis idArtis, byte[] tandaTanganArtis,
            byte[] tandaTanganEvent, byte[] tandaTanganManajer)
        {
            this.Id = id;
            this.Deskripsi = deskripsi;
            this.TanggalPengajuan = tanggalPengajuan;
            this.Nominal = nominal;
            this.Status = status;
            this.TanggalMulai = tanggalMulai;
            this.TanggalBerakhir = tanggalBerakhir;
            this.IdManajer = idManajer;
            this.IdEvent = idEvent;
            this.IdArtis = idArtis;
            this.TandaTanganArtis = tandaTanganArtis;
            this.TandaTanganEvent = tandaTanganEvent;
            this.TandaTanganManajer = tandaTanganManajer;
        }

        public Kontrak(int id, string deskripsi, DateTime tanggalPengajuan,
           string nominal, string status,
           DateTime tanggalMulai, DateTime tanggalBerakhir,
           Manajer idManajer, Eventss idEvent, Artis idArtis)
        {
            this.Id = id;
            this.Deskripsi = deskripsi;
            this.TanggalPengajuan = tanggalPengajuan;
            this.Nominal = nominal;
            this.Status = status;
            this.TanggalMulai = tanggalMulai;
            this.TanggalBerakhir = tanggalBerakhir;
            this.IdManajer = idManajer;
            this.IdEvent = idEvent;
            this.IdArtis = idArtis;
            this.TandaTanganArtis = tandaTanganArtis;
            this.TandaTanganEvent = tandaTanganEvent;
            this.TandaTanganManajer = tandaTanganManajer;
        }

        public Kontrak() //constructor kosong
        {
            this.Id = id;
            this.Deskripsi = deskripsi;
            this.TanggalPengajuan = tanggalPengajuan;
            this.Nominal = nominal;
            this.Status = status;
            this.TanggalMulai = tanggalMulai;
            this.TanggalBerakhir = tanggalBerakhir;
            this.IdManajer = idManajer;
            this.IdEvent = idEvent;
            this.IdArtis = idArtis;
            this.TandaTanganArtis = tandaTanganArtis;
            this.TandaTanganEvent = tandaTanganEvent;
            this.TandaTanganManajer = tandaTanganManajer;
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public DateTime TanggalPengajuan { get => tanggalPengajuan; set => tanggalPengajuan = value; }
        public string Nominal { get => nominal; set => nominal = value; }
        public string Status { get => status; set => status = value; }
        public DateTime TanggalMulai { get => tanggalMulai; set => tanggalMulai = value; }
        public DateTime TanggalBerakhir { get => tanggalBerakhir; set => tanggalBerakhir = value; }
        public Manajer IdManajer { get => idManajer; set => idManajer = value; }
        public Eventss IdEvent { get => idEvent; set => idEvent = value; }
        public Artis IdArtis { get => idArtis; set => idArtis = value; }
        public byte[] TandaTanganArtis { get => tandaTanganArtis; set => tandaTanganArtis = value; }
        public byte[] TandaTanganEvent { get => tandaTanganEvent; set => tandaTanganEvent = value; }
        public byte[] TandaTanganManajer { get => tandaTanganManajer; set => tandaTanganManajer = value; }
        public string Deskripsi { get => deskripsi; set => deskripsi = value; }
        #endregion

        #region methods
        //BACA DATA
        public static List<Kontrak> BacaDataSatu(string kriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from kontraks";
            }
            else
            {
                sql = "select * from kontraks where status='SETUJU' and idArtis=" + kriteria;
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Kontrak> listKontrak = new List<Kontrak>();
            while (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                //Manajer manajer = new Manajer();
                int idManajer = int.Parse(hasil.GetValue(7).ToString());
                List<Manajer> manajer = new List<Manajer>();
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int idEvent = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(idEvent.ToString());
                //Artis artis = new Artis();
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());
                byte[] ttdEvent = (byte[])hasil[10];
                byte[] ttdManajer = (byte[])hasil[11];
                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()), manajer[0], events[0],
                    artis[0], ttdEvent, ttdManajer);
                k.TandaTanganArtis = (byte[])hasil[10];
                k.TandaTanganEvent = (byte[])hasil[11];
                k.TandaTanganManajer = (byte[])hasil[12];

                listKontrak.Add(k);
            }
            return listKontrak;
        }

        public static List<Kontrak> TawaranKontrakSaya(string kriteria, string nilaiKriteria)
        {

            string sql = "select idkontraks, deskripsi, tanggal_pengajuan, nominal, status, tanggal_mulai," +
                "tanggal_berakhir, idManajers, idEvents, idArtis, tanda_tangan_events, tanda_tangan_manager from kontraks where status='" + kriteria + "' and " +
                "idArtis ='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Kontrak> listKontrak = new List<Kontrak>();
            while (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                Manajer manajer = new Manajer();
                manajer.IdManajer = int.Parse(hasil.GetValue(7).ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int id = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(id.ToString());
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());
                byte[] ttdEvent = (byte[])hasil[10];
                byte[] ttdManajer = (byte[])hasil[11];
                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()), manajer, events[0],
                    artis[0], ttdEvent, ttdManajer);
                listKontrak.Add(k);
            }
            return listKontrak;
        }
        public static List<Kontrak> DaftarKontrakManajer(string kriteria, string nilaiKriteria)
        {

            string sql = "select idkontraks, deskripsi, tanggal_pengajuan, nominal, status, tanggal_mulai," +
                "tanggal_berakhir, idManajers, idEvents, idArtis from kontraks where status='" + kriteria + "' and " +
                "idManajers='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Kontrak> listKontrak = new List<Kontrak>();
            while (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                Manajer manajer = new Manajer();
                manajer.IdManajer = int.Parse(hasil.GetValue(7).ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int id = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(id.ToString());
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());
                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()), manajer, events[0],
                    artis[0]);
                listKontrak.Add(k);
            }
            return listKontrak;
        }
        public static void TambahData(Kontrak k)
        {
            // Kemudian masukkan string Base64 ke dalam pernyataan SQL
            string sql = "insert into kontraks(idkontraks, deskripsi, tanggal_pengajuan, nominal," +
                "status, tanggal_mulai, tanggal_berakhir, idmanajers, idevents, idartis) values" +
                "('" + k.Id + "','" + k.Deskripsi + "','" + k.TanggalPengajuan.ToString("yyyy-MM-dd") + "','" +
                k.Nominal + "','" + "PENDING','" + k.TanggalMulai.ToString("yyyy-MM-dd") + "','" +
                k.TanggalBerakhir.ToString("yyyy-MM-dd") + "'," + k.IdManajer.IdManajer + "," +
                k.IdEvent.Id + "," + k.IdArtis.IdArtis + ")";

            // Kemudian jalankan pernyataan SQL seperti biasa

            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //BATAL
        public static void Batal(Kontrak k)
        {
            string sql = "";
            sql = "update kontraks set status ='TOLAK' where idKontraks=" +
                k.Id;
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //UPDATE STATUS SETUJU KONTRAK
        public static void Setuju(Kontrak k)
        {
            string sql = "";
            sql = "update kontraks set status ='SETUJU' where idKontraks=" +
                k.Id;
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //UPDATE TTD ARTIS
        public static void UpdateTTDArtis(Kontrak k)
        {
            byte[] imageData = k.TandaTanganArtis;
            string sql = "update kontraks set tanda_tangan_artis=" + "@imageData" + " where idKontraks='" +
                k.Id + "'";
            Koneksi.JalankanDMLValue(sql, imageData);
        }

        //UPDATE TTD MANAJER
        public static void UpdateTTDManajer(Kontrak k)
        {
            byte[] imageData = k.TandaTanganManajer;
            string sql = "update kontraks set tanda_tangan_manager=" + "@imageData" + " where idKontraks='" +
                k.Id + "'";
            Koneksi.JalankanDMLValue(sql, imageData);
        }
        public static void UpdateTTDEvents(Kontrak k)
        {
            byte[] imageData = k.TandaTanganEvent;
            string sql = "update kontraks set tanda_tangan_events=" + "@imageData" + " where idKontraks='" +
                k.Id + "'";
            Koneksi.JalankanDMLValue(sql, imageData);
        }

        public static Kontrak AmbilDataByKode(string kode)
        {
            string sql = "select idkontraks, deskripsi, tanggal_pengajuan, nominal, status, tanggal_mulai," +
                "tanggal_berakhir, idManajers, idEvents, idArtis, tanda_tangan_events, tanda_tangan_manager from kontraks where idKontraks = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                //Manajer manajer = new Manajer();
                int idManajer = int.Parse(hasil.GetValue(7).ToString());
                List<Manajer> manajer = new List<Manajer>();
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int idEvent = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(idEvent.ToString());
                //Artis artis = new Artis();
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());
                byte[] ttdEvent = (byte[])hasil[10];
                byte[] ttdManajer = (byte[])hasil[11];
                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()),
                    manajer[0], events[0],
                    artis[0], ttdEvent, ttdManajer);
                return k;
            }
            else
            {
                return null;
            }
        }
        public static Kontrak StatusSukses(string kode)
        {
            string sql = "select * from kontraks where idKontraks = '" + kode + "' and status = 'SETUJU';";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                //Manajer manajer = new Manajer();
                int idManajer = int.Parse(hasil.GetValue(7).ToString());
                List<Manajer> manajer = new List<Manajer>();
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int idEvent = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(idEvent.ToString());
                //Artis artis = new Artis();
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());
                byte[] ttdArtis = (byte[])hasil[10];
                byte[] ttdEvent = (byte[])hasil[11];
                byte[] ttdManajer = (byte[])hasil[12];
                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()), manajer[0], events[0],
                    artis[0], ttdArtis, ttdEvent, ttdManajer);
                return k;
            }
            else
            {
                return null;
            }
        }

        public static Kontrak AmbilIdTerbaru()
        {
            string sql = "select idkontraks, deskripsi, tanggal_pengajuan, nominal, status, tanggal_mulai," +
                "tanggal_berakhir, idManajers, idEvents, idArtis from kontraks where idKontraks = (select max(idkontraks) from kontraks)";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //buat nampilin manajer_id
                //Manajer manajer = new Manajer();
                int idManajer = int.Parse(hasil.GetValue(7).ToString());
                List<Manajer> manajer = new List<Manajer>();
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                //buat nampilin events_id
                List<Eventss> events = new List<Eventss>();
                int idEvent = int.Parse(hasil.GetValue(8).ToString());
                events = Eventss.BacaDataSatu(idEvent.ToString());
                //Artis artis = new Artis();
                List<Artis> artis = new List<Artis>();
                int idArtis = int.Parse(hasil.GetValue(9).ToString());
                artis = Artis.BacaDataSatu(idArtis.ToString());

                Kontrak k = new Kontrak(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    DateTime.Parse(hasil.GetValue(2).ToString()), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()), DateTime.Parse(hasil.GetValue(6).ToString()),
                    manajer[0], events[0],
                    artis[0]);
                return k;
            }
            else
            {
                return null;
            }
        }


        public override string ToString()
        {
            return IdEvent.Nama;
        }
        #endregion
    }
}
