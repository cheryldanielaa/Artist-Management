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
    public partial class FormUbahKarya : Form
    {
        FormDaftarKarya frm;
        public FormUbahKarya()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                //cari data artis yg namaanya ada di situ
                List<Artis> pemilikKarya = new List<Artis>();
                pemilikKarya = Artis.BacaDataDua(txtPemilikNama.Text);
                Karya k = new Karya(int.Parse(lblID.Text), txtNama.Text, dtpTglDibuat.Value, txtNama.Text, pemilikKarya[0]);
                Karya.UbahData(k, lblID.Text);
                MessageBox.Show("Perubahan data karya pada ID " + k.IdKarya.ToString() + " berhasil disimpan!");
                this.Close();
                FormDaftarKarya frm = (FormDaftarKarya)this.Owner;
                frm.FormDaftarKarya_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormUbahKarya_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarKarya)this.Owner;
        }
    }
}
