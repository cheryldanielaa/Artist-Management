using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Manajer
    {
        #region data members
        private int idManajer;
        private string nama;
        private string username;
        private string password;
        private string email;
        private string no_telp;
        #endregion

        #region constructor
        public Manajer(int idManajer, string nama, string username, string password, string email, string no_telp)
        {
            this.IdManajer = idManajer;
            this.Nama = nama;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.No_telp = no_telp;
        }

        public Manajer()
        {
            this.IdManajer = idManajer;
            this.Nama = nama;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.No_telp = no_telp;
        }
        #endregion

        #region properties
        public int IdManajer { get => idManajer; set => idManajer = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string No_telp { get => no_telp; set => no_telp = value; }
        #endregion

        #region methods
        public static List<Manajer> BacaDataSatu(string nilaiKriteria)
        {

            string sql = "select * from manajers where idManajers='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Manajer> listManajer = new List<Manajer>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                listManajer.Add(m);
            }
            return listManajer;
        }
        public static List<Manajer> BacaDataManajerArtis(string nilaiKriteria)
        {

            string sql = sql = "select m.idManajers from manajers m inner join artis a on a.idManajers = m.idManajers " +
                "where a.idArtis='" + nilaiKriteria + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Manajer> listManajer = new List<Manajer>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer();
                m.IdManajer = int.Parse(hasil.GetValue(0).ToString());
                listManajer.Add(m);
            }
            return listManajer;
        }

        public static List<Manajer> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from manajers";

            }
            else
            {
                sql = "select * from manajers where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Manajer> listManajer = new List<Manajer>();

            while (hasil.Read() == true)
            {
                Manajer m = new Manajer(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                listManajer.Add(m);
            }
            return listManajer;
        }

        public static void TambahData(Manajer m)
        {
            
            string sql = "insert into Manajers(nama, username, password, email, no_telp) values('" + m.Nama + "','" + 
                m.Username + "','" + m.Password + "','" + m.Email + "','" + m.No_telp + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Manajer CekLogin(string username, string password)
        {
            string sql = "";
            sql = "select * from Manajers" + " where username='" + username + "' and password = '" + password + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            while (hasil.Read() == true)
            {
                Manajer m = new Manajer(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                          (string)hasil[3], hasil.GetValue(4).ToString(),
                          hasil.GetValue(5).ToString());
                return m;
            }
            return null;
        }

        public static bool CekManajer(string kriteria, string nilaiKriteria)
        {
            string sql = "select * from Manajers where " + kriteria + "='" + nilaiKriteria + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            bool akhir = true;
            while (hasil.Read() == true)
            {
                akhir = false;
            }
            return akhir;
        }

        public static bool HapusData(Manajer m)
        {
            string sql = "delete from Manajers where id = '" + m.IdManajer + "'";

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

        public static void UbahData(Manajer m, string kode)
        {
            string sql = "update manajers set nama='" + m.Nama + "', " +
                "username='" + m.Username + "', password='" + m.Password +
                "', email='" + m.Email + "', no_telp='" + m.No_telp + "' " +
                "where idManajers='" + kode + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahDataPassword(string kriteria1, string kriteria2, string kriteria3)
        {
            string sql = "update manajers set password='" +kriteria1 + "', username='" + kriteria2 + "'" +
                " where idManajers='" + kriteria3 + "';";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Manajer AmbilDataByKode(string kode)
        {
            string sql = "select * from Manajers where id = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Manajer m = new Manajer(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                         (string)hasil[3], hasil.GetValue(4).ToString(),
                         hasil.GetValue(5).ToString());
                return m;
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

        public static Manajer AmbilDataLogin(string kode)
        {
            string sql = "select * from Manajers where username = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Manajer m = new Manajer(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                         (string)hasil[3], hasil.GetValue(4).ToString(),
                         hasil.GetValue(5).ToString());
                return m;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
