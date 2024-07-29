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
    public partial class FormDaftarEvent : Form
    {
        List<Eventss> listEvent = new List<Eventss>();
        public FormDaftarEvent()
        {
            InitializeComponent();
        }

        public void FormDaftarEvent_Load(object sender, EventArgs e)
        {
            listEvent = Eventss.BacaData("", "");
            if (listEvent.Count > 0)
            {
                dataGridViewDaftarEvent.DataSource = listEvent;
                if (dataGridViewDaftarEvent.Columns.Count == 4)
                {
                    DataGridViewButtonColumn buttonEdit = new DataGridViewButtonColumn();
                    buttonEdit.HeaderText = "Aksi";
                    buttonEdit.Name = "buttonEdit";
                    buttonEdit.Text = "Edit";
                    buttonEdit.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarEvent.Columns.Add(buttonEdit);

                    DataGridViewButtonColumn buttonHapus = new DataGridViewButtonColumn();
                    buttonHapus.HeaderText = "Aksi";
                    buttonHapus.Name = "buttonHapus";
                    buttonHapus.Text = "Hapus";
                    buttonHapus.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarEvent.Columns.Add(buttonHapus);
                }
            }
            else
            {
                dataGridViewDaftarEvent.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahEvent frm = new FormTambahEvent();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKriteria.Text == "Nama")
            {
                listEvent = Eventss.BacaDataEvent("nama", textBoxKriteria.Text);
            }

            if (listEvent.Count > 0)
            {
                dataGridViewDaftarEvent.DataSource = listEvent;
            }
            else
            {
                dataGridViewDaftarEvent.DataSource = null;
            }
        }

        private void dataGridViewDaftarEvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarEvent.CurrentRow.Cells["Id"].Value.ToString();
            Eventss events = Eventss.AmbilDataByKode(id);

            if (!(events == null))
            {
                if (e.ColumnIndex == dataGridViewDaftarEvent.Columns["buttonEdit"].Index && e.RowIndex >= 0)
                {
                    FormUbahEvent frm = new FormUbahEvent();
                    frm.Owner = this;
                    frm.lblID.Text = events.Id.ToString();
                    frm.txtNama.Text = events.Nama;
                    frm.dtpTglAkhir.Value = events.TanggalAwal;
                    frm.dtpTglAkhir.Value = events.TanggalAkhir;

                    frm.ShowDialog();
                }
                else if (e.ColumnIndex == dataGridViewDaftarEvent.Columns["buttonHapus"].Index && e.RowIndex >= 0)
                {
                    string idHapus = dataGridViewDaftarEvent.CurrentRow.Cells["Id"].Value.ToString();
                    string namaHapus = dataGridViewDaftarEvent.CurrentRow.Cells["Nama"].Value.ToString();
                    string tglAwalHapus = dataGridViewDaftarEvent.CurrentRow.Cells["TanggalAwal"].Value.ToString();
                    string tglAkhirHapus = dataGridViewDaftarEvent.CurrentRow.Cells["TanggalAkhir"].Value.ToString();

                    DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                        , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Eventss ev = new Eventss(int.Parse(idHapus), namaHapus, DateTime.Parse(tglAwalHapus), DateTime.Parse(tglAkhirHapus));
                        Boolean hapus = Eventss.HapusData(ev);
                        if (hapus == true)
                        {
                            MessageBox.Show("Berhasil hapus data");
                            FormDaftarEvent_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Gagal hapus data");
                        }
                    }
                }

            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
