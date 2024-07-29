using ManajemenArtis_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormProfilArtis : Form
    {
        FormUtama frm;
        public FormProfilArtis()
        {
            InitializeComponent();
        }

        private void FormProfilArtis_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            labelNama.Text = frm.artisLogin.Nama;

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

            labelAlamat.Text = Keamanan.DecryptAES(decryptTextAlamat, resKeyDecryptAlamat, ivDecryptAlamat);
            #endregion

            if (frm.artisLogin.Jenis_Kelamin == "P")
            {
                labelGender.Text = "Perempuan";
                pictureBox1.Image = Properties.Resources.propil;
            }
            else
            {
                labelGender.Text = "Laki-laki";
                pictureBox1.Image = Properties.Resources.profile;
            }
            labelEmail.Text = frm.artisLogin.Email;
            labelUsername.Text = frm.artisLogin.Username;

            #region decrypt no telp
            string[] pisah = frm.artisLogin.No_telp.Split('|');

            // Konversi kembali string yang telah dihapus padding menjadi byte array
            byte[] sepuluhpertama = Convert.FromBase64String(pisah[0]);
            byte[] sisanya = Convert.FromBase64String(pisah[3]);
            byte[] combined = sepuluhpertama.Concat(sisanya).ToArray();
            string comb = Convert.ToBase64String(combined);

            byte[] caesarKey = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[1], 4));
            byte[] caesarIv = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[2], 5));
            labelTelp.Text = Keamanan.DecryptAES(combined, caesarKey, caesarIv);
            //string yyy = Convert.ToBase64String(cipheredNoTelp);
            #endregion
        }
    }
}
