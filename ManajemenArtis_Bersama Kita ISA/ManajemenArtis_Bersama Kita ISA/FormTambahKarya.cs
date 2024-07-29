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
    public partial class FormTambahKarya : Form
    {
        List<Artis> listArtis = new List<Artis>();
        public FormTambahKarya()
        {
            InitializeComponent();
        }

        private void FormTambahKarya_Load(object sender, EventArgs e)
        {
            FormUtama frm = (FormUtama)this.Owner.MdiParent;
            listArtis = Artis.BacaDataArtisManajer(frm.manajerLogin.IdManajer.ToString());
            comboBoxArtis.DataSource = listArtis;
            txtJudul.Focus();
            comboBoxArtis.DisplayMember = "Nama";
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Artis selectedArtis = (Artis)comboBoxArtis.SelectedItem;
                Karya k = new Karya(int.Parse(labelID.Text), txtJudul.Text, DateTime.Parse(dtpTglDibuat.Value.ToString()),
                    comboBoxJenisKarya.Text, selectedArtis);
                Karya.TambahData(k);
                MessageBox.Show("Data Karya " + selectedArtis.Nama + " berhasil disimpan", "Informasi");
                //refresh halaman daftar
                FormDaftarKarya frm = (FormDaftarKarya)this.Owner;
                frm.FormDaftarKarya_Load(sender, e);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informasi");
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
