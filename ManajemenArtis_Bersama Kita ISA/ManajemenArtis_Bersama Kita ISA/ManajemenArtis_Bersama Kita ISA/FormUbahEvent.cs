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
    public partial class FormUbahEvent : Form
    {
        FormDaftarEvent frm;
        public FormUbahEvent()
        {
            InitializeComponent();
        }

        private void FormUbahEvent_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarEvent)this.Owner;
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Eventss ev = new Eventss(int.Parse(lblID.Text), txtNama.Text, dtpTglAwal.Value, dtpTglAkhir.Value);
                Eventss.UbahData(ev);
                MessageBox.Show("Perubahan data karya pada ID " + ev.Id.ToString() + " berhasil disimpan!");
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
            dtpTglAwal.Value = DateTime.Now;
            dtpTglAkhir.Value = DateTime.Now;
        }
    }
}
