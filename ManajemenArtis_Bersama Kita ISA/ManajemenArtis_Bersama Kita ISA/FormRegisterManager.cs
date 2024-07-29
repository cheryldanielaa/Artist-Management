using ManajemenArtis_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormRegisterManager : Form
    {

        List<Manajer> listManajer = new List<Manajer>();
        FormUtama frm;
        public byte[] key;
        public byte[] iv;
        public FormRegisterManager()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                #region encrypt password
                //PLANNING - ENKRIPSI PASSWORD DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (10), 2ND HALF(17)
                //IV DICAESAR >> SWIFT 23

                //GENERATE RANDOM KEY DAN IV
                byte[] key = new byte[32]; //kunci sepanjang 256 bit
                byte[] iv = new byte[16]; //128 bit IV

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(key);
                    rng.GetBytes(iv);
                }

                byte[] encryptedPassword = Keamanan.EncryptAES(txtPassword.Text, key, iv);
                string cipheredPassword = Convert.ToBase64String(encryptedPassword);

                // Determine the length of each split
                int splitLength = key.Length / 2;

                //// Pisah key jadi 2 part array
                byte[] firstHalfKey = new byte[splitLength];
                byte[] secondHalfKey = new byte[key.Length - splitLength];

                //// Copy the appropriate portions of the original array into the new arrays
                Array.Copy(key, 0, firstHalfKey, 0, splitLength);
                Array.Copy(key, splitLength, secondHalfKey, 0, key.Length - splitLength);

                ////convert byte key ke string dan convert ke caesar

                string cipheredFirstKey = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKey), 10);
                string cipheredSecKey = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKey), 17);
                string cipheredIV = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 23);

                //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                string concat = cipheredIV + "|" + cipheredSecKey + "|" + cipheredPassword + "|" + cipheredFirstKey;
                #endregion
                #region enkripsi notelp
                byte[] keyTelp = new byte[32]; //kunci sepanjang 256 bit
                byte[] ivTelp = new byte[16]; //128 bit IV

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(keyTelp);
                    rng.GetBytes(ivTelp);
                }

                byte[] encryptedNoTelp = Keamanan.EncryptAES(txtTelp.Text, keyTelp, ivTelp);
                //string hahah = Convert.ToBase64String(encryptedNoTelp);
                byte[] a = new byte[10];
                byte[] b = new byte[encryptedNoTelp.Length - 10];
                Array.Copy(encryptedNoTelp, 0, a, 0, 10);
                Array.Copy(encryptedNoTelp, 10, b, 0, encryptedNoTelp.Length - 10);

                string cipheredkey = Keamanan.EncryptCaesar(Convert.ToBase64String(keyTelp), 4);
                string cipherediv = Keamanan.EncryptCaesar(Convert.ToBase64String(ivTelp), 5);
                string sepuluh = Convert.ToBase64String(a);
                string sisa = Convert.ToBase64String(b);

                string concatTelp = sepuluh + "|" + cipheredkey + "|" + cipherediv + "|" + sisa;
                #endregion
                Manajer m = new Manajer(int.Parse(labelID.Text), txtNama.Text, txtUsername.Text, concat ,
                    txtEmail.Text, concatTelp);
                if (Manajer.CekManajer("username", txtUsername.Text) == false)
                {
                    MessageBox.Show("Username sudah digunakan. Silahkan gunakan username lainnya", "Informasi");
                }
                else
                {
                    Manajer.TambahData(m);
                    MessageBox.Show("Selamat, Anda berhasil registrasi akun di ISA Town", "Informasi");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pesan Kesalahan");
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            txtNama.Clear();
            txtPassword.Clear();
            txtTelp.Clear();
            txtUsername.Clear();
            txtEmail.Clear();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegisterManager_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.Owner.Owner;
        }
    }
}
