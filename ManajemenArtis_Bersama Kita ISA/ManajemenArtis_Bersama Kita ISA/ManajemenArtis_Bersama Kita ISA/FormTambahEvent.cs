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
    public partial class FormTambahEvent : Form
    {
        Eventss ev;
        FormDaftarEvent frm;
        public FormTambahEvent()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                ev = new Eventss(int.Parse(labelID.Text), txtNama.Text, dtpTglAwal.Value, dtpTglAkhir.Value);
                Eventss.TambahData(ev);
                MessageBox.Show("Selamat, data Anda berhasil ditambahkan!");
                this.Close();
                frm.FormDaftarEvent_Load(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            txtNama.Clear();
            dtpTglAkhir.Value = DateTime.Now;
            dtpTglAwal.Value = DateTime.Now;
            txtNama.Focus();
        }

        private void FormTambahEvent_Load(object sender, EventArgs e)
        {
            txtNama.Focus();
            frm = (FormDaftarEvent)this.Owner;
        }
    }
}
