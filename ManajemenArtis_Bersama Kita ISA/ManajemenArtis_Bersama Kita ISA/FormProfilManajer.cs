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
    public partial class FormProfilManajer : Form
    {
        FormUtama frm;
        public FormProfilManajer()
        {
            InitializeComponent();
        }

        private void FormProfilManajer_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            labelNama.Text = frm.manajerLogin.Nama;
            labelEmail.Text = frm.manajerLogin.Email;
            labelUsername.Text = frm.manajerLogin.Username;

            #region decrypt no telp
            string[] pisah = frm.manajerLogin.No_telp.Split('|');

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
