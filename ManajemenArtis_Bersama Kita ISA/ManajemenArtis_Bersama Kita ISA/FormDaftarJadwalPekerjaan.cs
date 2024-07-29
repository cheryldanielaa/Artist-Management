using ManajemenArtis_LIB;
using Org.BouncyCastle.Crypto;
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
    public partial class FormDaftarJadwalPekerjaan : Form
    {
        FormUtama frm;
        List<JadwalPekerjaan> listJadwalPekerjaanTiapArtis = new List<JadwalPekerjaan>();

        public FormDaftarJadwalPekerjaan()
        {
            InitializeComponent();
        }

        public void FormDaftarJadwalPekerjaan_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            listJadwalPekerjaanTiapArtis = JadwalPekerjaan.BacaData("idArtis", frm.artisLogin.IdArtis.ToString());
            //dgvJadwal.DataSource = listJadwalPekerjaanTiapArtis;
            if (listJadwalPekerjaanTiapArtis.Count > 0)
            {
                FormatDataGrid();
            }
            else
            {
                dgvJadwal.DataSource = null;
            }
            if (listJadwalPekerjaanTiapArtis.Count > 0)
            {
                dgvJadwal.RowCount = listJadwalPekerjaanTiapArtis.Count;
                for (int i = 0; i < listJadwalPekerjaanTiapArtis.Count; i++)
                {
                    dgvJadwal.Rows[i].Cells["Nama Artis"].Value = listJadwalPekerjaanTiapArtis[i].IdArtis.Nama;
                    dgvJadwal.Rows[i].Cells["Tanggal"].Value = listJadwalPekerjaanTiapArtis[i].Tanggal;
                    dgvJadwal.Rows[i].Cells["Events"].Value = listJadwalPekerjaanTiapArtis[i].IdEvent.Nama;
                    dgvJadwal.Rows[i].Cells["Jam"].Value = listJadwalPekerjaanTiapArtis[i].Jam;
                    dgvJadwal.Rows[i].Cells["Deskripsi"].Value = listJadwalPekerjaanTiapArtis[i].Deskripsi;
                }
                for (int i = 0; i < dgvJadwal.RowCount; i++)
                {
                    string tanggal = dgvJadwal.Rows[i].Cells["Tanggal"].Value.ToString();
                    

                    //string tgl = listJadwalPekerjaanTiapArtis[i].Tanggal;
                    string[] result1 = tanggal.Split('|');
                    byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result1[0], 21));
                    byte[] KeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result1[1], 9));
                    byte[] decryptText = Convert.FromBase64String(result1[2]);

                    string decryptedtanggal = Keamanan.DecryptAES(decryptText, KeyDecrypt, ivDecrypt);
                    dgvJadwal.Rows[i].Cells["tanggal"].Value = decryptedtanggal;


                    string jam = dgvJadwal.Rows[i].Cells["jam"].Value.ToString();


                    //string tgl = listJadwalPekerjaanTiapArtis[i].Tanggal;
                    string[] result2 = jam.Split('|');
                    byte[] ivDecrypt2 = Convert.FromBase64String(Keamanan.DecryptCaesar(result2[1],18));
                    byte[] KeyDecrypt2 = Convert.FromBase64String(Keamanan.DecryptCaesar(result2[2], 13));
                    byte[] decryptText2 = Convert.FromBase64String(result2[0]);

                    string decryptedJam = Keamanan.DecryptAES(decryptText2, KeyDecrypt2, ivDecrypt2);
                    dgvJadwal.Rows[i].Cells["jam"].Value = decryptedJam;
                }
            }

        }

        private void FormatDataGrid()
            {
                dgvJadwal.Columns.Clear();

                dgvJadwal.Columns.Add("Nama Artis", "Nama Artis");
                dgvJadwal.Columns["Nama Artis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvJadwal.Columns["Nama Artis"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvJadwal.Columns.Add("Tanggal", "Tanggal");
                dgvJadwal.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvJadwal.Columns["Tanggal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvJadwal.Columns.Add("Events", "Nama Event");
                dgvJadwal.Columns["Events"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvJadwal.Columns["Events"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvJadwal.Columns.Add("Jam", "Jam");
                dgvJadwal.Columns["Jam"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvJadwal.Columns["Jam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvJadwal.Columns.Add("Deskripsi", "Deskripsi");
                dgvJadwal.Columns["Deskripsi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvJadwal.Columns["Deskripsi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

        private void dgvJadwal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgvJadwal.CurrentRow.Cells["IdArtis"].Value.ToString();
            listJadwalPekerjaanTiapArtis = JadwalPekerjaan.BacaData("Id", comboBoxKriteria.Text);
            dgvJadwal.DataSource = listJadwalPekerjaanTiapArtis;
            if (listJadwalPekerjaanTiapArtis.Count > 0)
            {
                FormatDataGrid();
            }
            else
            {
                dgvJadwal.DataSource = null;
            }
            if (listJadwalPekerjaanTiapArtis.Count > 0)
            {
                for (int i = 0; i < dgvJadwal.RowCount; i++)
                {
                    dgvJadwal.Rows[i].Cells["NamaArtis"].Value = listJadwalPekerjaanTiapArtis[i].IdArtis.Nama;
                    dgvJadwal.Rows[i].Cells["Tanggal"].Value = listJadwalPekerjaanTiapArtis[i].Tanggal;
                    dgvJadwal.Rows[i].Cells["Events"].Value = listJadwalPekerjaanTiapArtis[i].IdEvent.Nama;
                    dgvJadwal.Rows[i].Cells["Jam"].Value = listJadwalPekerjaanTiapArtis[i].Jam;
                    dgvJadwal.Rows[i].Cells["Deskripsi"].Value = listJadwalPekerjaanTiapArtis[i].Deskripsi;
                }
            }

        }

        private void dgvJadwal_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

      
    }
