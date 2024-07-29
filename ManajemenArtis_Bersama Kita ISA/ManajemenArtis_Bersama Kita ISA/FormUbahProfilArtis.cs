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
    public partial class FormUbahProfilArtis : Form
    {
        Artis artis;
        FormUtama frm;
        List<Manajer> listManajer = new List<Manajer>();
        public FormUbahProfilArtis()
        {
            InitializeComponent();
        }

        private void FormUbahProfil_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            if (frm.manajerLogin == null && frm.artisLogin != null & frm.adminLogin == null)
            {
                labelID.Text = frm.artisLogin.IdArtis.ToString();
                txtNama.Text = frm.artisLogin.Nama;
                dtpTglLahir.Value = frm.artisLogin.Tgl_Lahir;
                if (frm.artisLogin.Jenis_Kelamin == "P")
                {
                    comboBoxGender.SelectedItem = "Perempuan";
                }
                else if (frm.artisLogin.Jenis_Kelamin == "L")
                {
                    comboBoxGender.SelectedItem = "Laki-Laki";
                }
                txtEmail.Text = frm.artisLogin.Email;

                #region decrypt alamat
                string concatAlamat = frm.artisLogin.Alamat;
                // Define the delimiters
                char[] delimitersAlamat = { '|' }; //BUAT PISAHIN PER PART BIAR BISA DIPAHAMI

                // List to store the substrings
                List<string> substringsAlamat = new List<string>();

                // Temporary string to build each substring
                string substringAlamat = "";

                // Iterate through the characters of the concat string
                foreach (char c in concatAlamat)
                {
                    // Check if the character is a delimiter
                    if (Array.IndexOf(delimitersAlamat, c) != -1)
                    {
                        // Add the substring to the list if it's not empty
                        if (substringAlamat != "")
                        {
                            substringsAlamat.Add(substringAlamat);
                            substringAlamat = ""; // Reset the temporary string
                        }
                    }
                    else
                    {
                        // Append the character to the temporary string
                        substringAlamat += c;
                    }
                }

                // Add the last substring to the list if it's not empty
                if (substringAlamat != "")
                {
                    substringsAlamat.Add(substringAlamat);
                }

                // Convert the list to an array
                string[] resultAlamat = substringsAlamat.ToArray();
                byte[] ivDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[0], 5));
                byte[] secKeyDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[1], 7));
                byte[] decryptTextAlamat = Convert.FromBase64String(resultAlamat[2]);
                byte[] firstKeyDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[3], 3));
                byte[] resKeyDecryptAlamat = firstKeyDecryptAlamat.Concat(secKeyDecryptAlamat).ToArray();

                txtAlamat.Text = Keamanan.DecryptAES(decryptTextAlamat, resKeyDecryptAlamat, ivDecryptAlamat);
                #endregion

                #region decrypt no telp
                string[] pisah = frm.artisLogin.No_telp.Split('|');

                // Konversi kembali string yang telah dihapus padding menjadi byte array
                byte[] sepuluhpertama = Convert.FromBase64String(pisah[0]);
                byte[] sisanya = Convert.FromBase64String(pisah[3]);
                byte[] combined = sepuluhpertama.Concat(sisanya).ToArray();
                string comb = Convert.ToBase64String(combined);

                byte[] caesarKey = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[1], 4));
                byte[] caesarIv = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[2], 5));
                txtNoTelp.Text = Keamanan.DecryptAES(combined, caesarKey, caesarIv);
                //string yyy = Convert.ToBase64String(cipheredNoTelp);
                #endregion

                txtUsername.Text = frm.artisLogin.Username;
                txtPassword.Text = frm.artisLogin.Password;

                listManajer = Manajer.BacaDataManajerArtis(frm.artisLogin.IdArtis.ToString());
                comboBoxManajer.DataSource = listManajer;
                comboBoxManajer.DisplayMember = "Nama";
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
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

            #region encrypt notelp
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

            if (frm.manajerLogin == null && frm.artisLogin != null & frm.adminLogin == null)
            {
                string gender = "";
                if (comboBoxGender.Text == "Laki-Laki")
                {
                    gender = "L";   
                }
                else if (comboBoxGender.Text == "Perempuan")
                {
                    gender = "P";
                }

                Manajer selectedManajer = (Manajer)comboBoxManajer.SelectedItem;
                artis = new Artis(int.Parse(labelID.Text), txtNama.Text, txtUsername.Text, txtPassword.Text, txtEmail.Text, 
                    dtpTglLahir.Value, gender, concatAlamat, concatTelp, selectedManajer);
                Artis.UbahData(artis, labelID.Text);

                frm.artisLogin = artis; //set supaya setelah ubah data lgsg berubah di profile
            }

            MessageBox.Show("Selamat, Anda berhasil ubah profil");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
