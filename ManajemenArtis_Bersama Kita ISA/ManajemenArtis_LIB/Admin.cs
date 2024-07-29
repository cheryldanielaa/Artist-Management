using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Admin
    {
        private int id;
        private string username;
        private string password;

        public Admin(int id, string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }


        public static Admin CekLogin(string kode)
        {
            string sql = "select * from admins where username = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                Admin a = new Admin(int.Parse(hasil.GetValue(0).ToString()), 
                    hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString());
                return a;
            }
            else
            {
                return null;
            }
        }

        public static void TambahData(Admin a)
        {
            string sql = "insert into admins(idAdmins, username, password) values('" + a.Id + "','" +
                a.Username + "','" + a.Password + "')";
           Koneksi.JalankanPerintahNonQuery(sql);
           
        }

        public static void UbahData(string kriteria1, string kriteria2)
        {
            string sql = "update admins set password='" +
               kriteria1 + "' where idAdmins=" + kriteria2;
            Koneksi.JalankanPerintahNonQuery(sql);

        }
    }
}
