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

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormRegisterAkunArtis : Form
    {
        List<Artis> listArtis = new List<Artis>();
        List<Manajer> listManajer = new List<Manajer>();
        public FormRegisterAkunArtis()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                #region encrypt password
                //PLANNING - ENKRIPSI PASSWORD DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                //IV DICAESAR >> SWIFT 5

                //GENERATE RANDOM KEY DAN IV
                byte[] keyPass = new byte[32]; //kunci sepanjang 256 bit
                byte[] ivPass = new byte[16]; //128 bit IV

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(keyPass);
                    rng.GetBytes(ivPass);
                }

                byte[] encryptedPassword = Keamanan.EncryptAES(txtPassword.Text, keyPass, ivPass);
                string cipheredPassword = Convert.ToBase64String(encryptedPassword);

                // Determine the length of each split
                int splitLengthPass = keyPass.Length / 2;

                //// Pisah key jadi 2 part array
                byte[] firstHalfKeyPass = new byte[splitLengthPass];
                byte[] secondHalfKeyPass = new byte[keyPass.Length - splitLengthPass];

                //// Copy the appropriate portions of the original array into the new arrays
                Array.Copy(keyPass, 0, firstHalfKeyPass, 0, splitLengthPass);
                Array.Copy(keyPass, splitLengthPass, secondHalfKeyPass, 0, keyPass.Length - splitLengthPass);

                ////convert byte key ke string dan convert ke caesar

                string cipheredFirstKeyPass = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKeyPass), 3);
                string cipheredSecKeyPass = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKeyPass), 7);
                string cipheredIVPass = Keamanan.EncryptCaesar(Convert.ToBase64String(ivPass), 5);

                //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                string concatPass = cipheredIVPass + "|" + cipheredSecKeyPass + "|" + cipheredPassword + "|" + cipheredFirstKeyPass;
                #endregion

                #region encrypt alamat
                //PLANNING - ENKRIPSI ALAMAT DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                //IV DICAESAR >> SWIFT 5

                //GENERATE RANDOM KEY DAN IV
                byte[] keyAlamat = new byte[32]; //kunci sepanjang 256 bit
                byte[] ivAlamat = new byte[16]; //128 bit IV

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(keyAlamat);
                    rng.GetBytes(ivAlamat);
                }

                byte[] encryptedAlamat = Keamanan.EncryptAES(txtAlamat.Text, keyAlamat, ivAlamat);
                string cipheredAlamat = Convert.ToBase64String(encryptedAlamat);

                // Determine the length of each split
                int splitLengthAlamat = keyAlamat.Length / 2;

                //// Pisah key jadi 2 part array
                byte[] firstHalfKeyAlamat = new byte[splitLengthAlamat];
                byte[] secondHalfKeyAlamat = new byte[keyAlamat.Length - splitLengthAlamat];

                //// Copy the appropriate portions of the original array into the new arrays
                Array.Copy(keyAlamat, 0, firstHalfKeyAlamat, 0, splitLengthAlamat);
                Array.Copy(keyAlamat, splitLengthAlamat, secondHalfKeyAlamat, 0, keyAlamat.Length - splitLengthAlamat);

                ////convert byte key ke string dan convert ke caesar

                string cipheredFirstKeyAlamat = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKeyAlamat), 3);
                string cipheredSecKeyAlamat = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKeyAlamat), 7);
                string cipheredIVAlamat = Keamanan.EncryptCaesar(Convert.ToBase64String(ivAlamat), 5);

                //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                string concatAlamat = cipheredIVAlamat + "|" + cipheredSecKeyAlamat + "|" + cipheredAlamat + "|" + cipheredFirstKeyAlamat;
                #endregion

                #region enkripsi notelp
                byte[] keyTelp = new byte[32]; //kunci sepanjang 256 bit
                byte[] ivTelp = new byte[16]; //128 bit IV

                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(keyTelp);
                    rng.GetBytes(ivTelp);
                }

                byte[] encryptedNoTelp = Keamanan.EncryptAES(txtNoTelp.Text, keyTelp, ivTelp);
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

                string gender = "";
                if (comboBoxGender.Text == "Laki-Laki")
                {
                    gender = "L";
                }
                else if (comboBoxGender.Text == "Perempuan")
                {
                    gender = "P";
                }

                ////pilih manajer secara random utk membawahi artis yg bru daftar
                //var random = new Random();
                //int index = random.Next(listManajer.Count);
                //Manajer m = new Manajer();
                //m = listManajer[index];
                Manajer selectedManajer = (Manajer)comboBoxManajer.SelectedItem;
                Artis artis = new Artis(int.Parse(labelID.Text), txtNama.Text, txtUsername.Text, concatPass, txtEmail.Text,
                    DateTime.Parse(dtpTglLahir.Value.ToShortDateString()), gender, concatAlamat, concatTelp, selectedManajer);

                if (Artis.CekArtis("username", txtUsername.Text) == false)
                {
                    MessageBox.Show("Username sudah digunakan. Silahkan gunakan username lainnya", "Informasi");
                }
                else
                {
                    Artis.TambahData(artis);
                    MessageBox.Show("Selamat, Anda berhasil registrasi akun di ISA Town", "Informasi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtAlamat.Clear();
            txtNama.Clear();
            dtpTglLahir.Value = DateTime.Now;
            txtPassword.Clear();
            txtNoTelp.Clear();
            txtNama.Focus();
            txtEmail.Clear();
            comboBoxGender.SelectedIndex = -1;
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegisterAkunArtis_Load(object sender, EventArgs e)
        {
            //baca list manajer yg tersedia
            listManajer = Manajer.BacaData("", "");
            comboBoxManajer.DataSource = listManajer;
            comboBoxManajer.DisplayMember = "Nama";
        }
    }
}
