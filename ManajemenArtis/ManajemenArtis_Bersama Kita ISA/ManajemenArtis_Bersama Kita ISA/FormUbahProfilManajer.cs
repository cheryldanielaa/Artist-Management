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
    public partial class FormUbahProfilManajer : Form
    {
        Manajer m;
        FormUtama frm;
        public FormUbahProfilManajer()
        {
            InitializeComponent();
        }

        private void FormUbahProfilManajer_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            if (frm.manajerLogin != null && frm.artisLogin == null & frm.adminLogin == null)
            {
                labelID.Text = frm.manajerLogin.IdManajer.ToString();
                txtNama.Text = frm.manajerLogin.Nama;
                txtEmail.Text = frm.manajerLogin.Email;
                #region decrypt no telp
                string[] pisah = frm.manajerLogin.No_telp.Split('|');

                // Konversi kembali string yang telah dihapus padding menjadi byte array
                byte[] sepuluhpertama = Convert.FromBase64String(pisah[0]);
                byte[] sisanya = Convert.FromBase64String(pisah[3]);
                byte[] combined = sepuluhpertama.Concat(sisanya).ToArray();
                string comb = Convert.ToBase64String(combined);

                byte[] caesarKey = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[1], 4));
                byte[] caesarIv = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[2], 5));
                txtTelp.Text = Keamanan.DecryptAES(combined, caesarKey, caesarIv);
                //string yyy = Convert.ToBase64String(cipheredNoTelp);
                #endregion
                txtUsername.Text = frm.manajerLogin.Username;
                txtPassword.Text = frm.manajerLogin.Password;
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (frm.manajerLogin != null && frm.artisLogin == null & frm.adminLogin == null)
            {
                #region encrypt notelp
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

                m = new Manajer(int.Parse(labelID.Text), txtNama.Text, txtUsername.Text, txtPassword.Text, txtEmail.Text, concatTelp);
                Manajer.UbahData(m, frm.manajerLogin.IdManajer.ToString());
                frm.manajerLogin = m; //update value manajer spy auto keupdate
            }

            MessageBox.Show("Selamat, Anda berhasil ubah profil");
        }
    }
}
