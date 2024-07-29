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
    public partial class FormUbahPassword : Form
    {
        FormUtama frm;
        public FormUbahPassword()
        {
            InitializeComponent();
        }

        private void FormUbahPassword_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            if(frm.manajerLogin!=null && frm.artisLogin==null && frm.adminLogin == null)
            {
                txtUsername.Text = frm.manajerLogin.Username;
            }
            else if (frm.manajerLogin == null && frm.artisLogin != null && frm.adminLogin == null)
            {
                txtUsername.Text = frm.artisLogin.Username;
            }
            else if (frm.manajerLogin == null && frm.artisLogin == null && frm.adminLogin != null)
            {
                txtUsername.Text = frm.adminLogin.Username;
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (frm.manajerLogin != null && frm.artisLogin == null && frm.adminLogin == null)
            {
                #region encrypt password
                //PLANNING - ENKRIPSI PASSWORD DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                //IV DICAESAR >> SWIFT 5

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
                Manajer.UbahDataPassword(concat, txtUsername.Text, frm.manajerLogin.IdManajer.ToString());
            }
            else if (frm.manajerLogin == null && frm.artisLogin != null && frm.adminLogin == null)
            {
                #region encrypt password
                //PLANNING - ENKRIPSI PASSWORD DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                //IV DICAESAR >> SWIFT 5

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

                string cipheredFirstKey = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKey), 3);
                string cipheredSecKey = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKey), 7);
                string cipheredIV = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 5);

                //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                string concat = cipheredIV + "|" + cipheredSecKey + "|" + cipheredPassword + "|" + cipheredFirstKey;
                #endregion
                txtUsername.Text = frm.artisLogin.Username;
                //ubah password artis
                Artis.UbahDataPassword(concat, txtUsername.Text, frm.artisLogin.IdArtis.ToString());
                
            }
            else if (frm.manajerLogin == null && frm.artisLogin == null && frm.adminLogin != null)
            {
                #region encrypt password
                //PLANNING - ENKRIPSI PASSWORD DGN AES
                //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                //IV DICAESAR >> SWIFT 5

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

                string cipheredFirstKey = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKey), 3);
                string cipheredSecKey = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKey), 7);
                string cipheredIV = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 5);

                //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                string concat = cipheredIV + "|" + cipheredSecKey + "|" + cipheredPassword + "|" + cipheredFirstKey;
                #endregion
                txtUsername.Text = frm.adminLogin.Username;
                Admin.UbahData(concat, frm.adminLogin.Id.ToString());
            }
            MessageBox.Show("Selamat, Anda berhasil ubah password");
        }
    }
}
