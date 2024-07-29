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
    public partial class FormDaftarKarya : Form
    {
        List<Karya> listKarya = new List<Karya>();
        public FormDaftarKarya()
        {
            InitializeComponent();
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxKriteria.Text == "Jenis Karya")
                {
                    listKarya = Karya.BacaData("jenis_karya", textBoxKriteria.Text);
                }
                else if (comboBoxKriteria.Text == "Judul")
                {
                    listKarya = Karya.BacaData("judul", textBoxKriteria.Text);
                }

                if (listKarya.Count > 0) //jika list kategori terisi data
                {
                    dataGridViewDaftarKarya.DataSource = listKarya;
                }
                else
                {
                    dataGridViewDaftarKarya.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }

        }

        private void dataGridViewDaftarKarya_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarKarya.CurrentRow.Cells["IdKarya"].Value.ToString();
            Karya k = Karya.AmbilDataByKode(id);

            if (!(k == null))
            {
                if (e.ColumnIndex == dataGridViewDaftarKarya.Columns["buttonUbahGrid"].Index && e.RowIndex >= 0)
                {
                    FormUbahKarya form = new FormUbahKarya();
                    form.Owner = this;
                    form.lblID.Text = k.IdKarya.ToString();
                    form.txtNama.Text = k.Judul;
                    form.txtPemilikNama.Text = k.Artis.Nama;
                    form.txtJenisKarya.Text = k.JenisKarya;
                    form.dtpTglDibuat.Value = k.Tanggal;
                    form.ShowDialog();
                }
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahKarya frm = new FormTambahKarya();
            frm.Owner = this;
            frm.Show();
        }

        public void FormDaftarKarya_Load(object sender, EventArgs e)
        {
            try
            {
                listKarya = Karya.BacaData("", "");
                if (listKarya.Count > 0)
                {
                    dataGridViewDaftarKarya.DataSource = listKarya;

                    if (dataGridViewDaftarKarya.ColumnCount < 6)
                    {
                        DataGridViewButtonColumn btnUbah = new DataGridViewButtonColumn();
                        btnUbah.Name = "buttonUbahGrid";//nama objek button
                        btnUbah.HeaderText = "Aksi";
                        btnUbah.Text = "Edit";//tulisan di button
                        btnUbah.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                        dataGridViewDaftarKarya.Columns.Add(btnUbah);//tambahkan button ke grid
                    }
                }
                else
                {
                    dataGridViewDaftarKarya.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
