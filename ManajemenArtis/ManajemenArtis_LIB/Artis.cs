using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public  class Artis
    {
        #region data members
        private int idArtis;
        private string nama;
        private string username;
        private string password;
        private string email;
        private DateTime tgl_Lahir;
        private string jenis_Kelamin;
        private string alamat;
        private string no_telp;
        private Manajer idManajer;
        #endregion

        #region constructor
        public Artis(int idArtis, string nama, string username, string password, string email, DateTime tgl_Lahir,
            string jenis_Kelamin, string alamat, string no_telp, Manajer idManajer)
        {
            this.IdArtis = idArtis;
            this.Nama = nama;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Tgl_Lahir = tgl_Lahir;
            this.Jenis_Kelamin = jenis_Kelamin;
            this.Alamat = alamat;
            this.No_telp = no_telp;
            this.IdManajer = idManajer;
        }

        public Artis()
        {
            this.IdArtis = idArtis;
            this.Nama = nama;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Tgl_Lahir = tgl_Lahir;
            this.Jenis_Kelamin = jenis_Kelamin;
            this.Alamat = alamat;
            this.No_telp = no_telp;
            this.IdManajer = idManajer;
        }
        #endregion

        #region properties
        public int IdArtis { get => idArtis; set => idArtis = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Tgl_Lahir { get => tgl_Lahir; set => tgl_Lahir = value; }
        public string Jenis_Kelamin { get => jenis_Kelamin; set => jenis_Kelamin = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public string No_telp { get => no_telp; set => no_telp = value; }
        public Manajer IdManajer { get => idManajer; set => idManajer = value; }
        #endregion

        public override string ToString()
        {
            return Nama;
        }

        public static List<Artis> BacaDataSatu(string nilaiKriteria)
        {

            string sql = "select * from artis where idArtis='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Artis> listArtis = new List<Artis>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                     hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                      hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                listArtis.Add(a);
            }
            return listArtis;
        }

        public static List<Artis> BacaDataDua(string nilaiKriteria)
        {

            string sql = "select * from artis where nama='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Artis> listArtis = new List<Artis>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                     hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                      hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                listArtis.Add(a);
            }
            return listArtis;
        }

        public static List<Artis> BacaDataArtisManajer(string nilaiKriteria)
        {

            string sql = "select * from artis where idManajers='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Artis> listArtis = new List<Artis>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                     hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                      hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                listArtis.Add(a);
            }
            return listArtis;
        }
        public static List<Artis> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {

                sql = "select * from artis";
            }
            else
            {
                sql = "select * from artis where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Artis> listArtis = new List<Artis>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                     hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                      hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                listArtis.Add(a);
            }
            return listArtis;
        }
        public static void TambahData(Artis a)
        {
            string sql = "insert into artis(nama, username, password, email, tgl_lahir, jenis_kelamin, alamat, no_telp, " +
                " idManajers) values('" +
                         a.Nama + "','" + a.Username + "','" + a.Password + "','" + a.Email + "','" + a.Tgl_Lahir.ToString("yyyy-MM-dd") + "','" + a.Jenis_Kelamin + "','" +
                         a.Alamat + "','" + a.No_telp + "','" + a.IdManajer.IdManajer + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Artis CekLogin(string username, string password)
        {
            string sql = "";
            sql = "select * from Artis where username = '" + username + "' and password = '" + password + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                    hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                    hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                return a;
            }
            return null;
        }

        public static bool CekArtis(string kriteria, string nilaiKriteria)
        {
            string sql = "select * from artis where " + kriteria + "='" + nilaiKriteria + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            bool akhir = true;
            while (hasil.Read() == true)
            {
                akhir = false;
            }
            return akhir;
        }

        public static bool HapusData(Artis a)
        {
            string sql = "delete from artis where id = '" + a.IdArtis + "'";

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

        public static void UbahData(Artis a, string kode)
        {
            string sql = "update artis set nama ='" + a.Nama + "', username ='" + a.Username + "', password ='" + 
                        a.Password + "', email ='" +
                         a.Email + "', " +
                         "tgl_lahir ='" + a.Tgl_Lahir.ToString("yyyy-MM-dd") + "', jenis_kelamin =" +
                         "'" + a.Jenis_Kelamin + "', alamat ='" + a.Alamat +
                         "', no_telp='" + a.No_telp + "', idManajers ='" + a.IdManajer.IdManajer +
                         "' where idArtis='" + kode + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        public static Artis AmbilDataLogin(string kode)
        {
            string sql = "select * from artis where username = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                     hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                      hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                return a;
            }
            else
            {
                return null;
            }
        }
        public static Artis AmbilDataByKode(string kode)
        {
            string sql = "select * from artis where idArtis = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                List<Manajer> manajer = new List<Manajer>();
                int idManajer = int.Parse(hasil.GetValue(9).ToString());
                manajer = Manajer.BacaDataSatu(idManajer.ToString());
                m = manajer[0];
                Artis a = new Artis(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                    hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), DateTime.Parse(hasil.GetValue(5).ToString()),
                    hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString(), m);
                return a;
            }
            else
            {
                return null;
            }
        }

        public static void UbahDataPassword(string kriteria1, string kriteria2, string kriteria3)
        {
            string sql = "update artis set password='" + kriteria1 + "', username='" + kriteria2 + "'" +
                " where idArtis='" + kriteria3 + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
    }
}
