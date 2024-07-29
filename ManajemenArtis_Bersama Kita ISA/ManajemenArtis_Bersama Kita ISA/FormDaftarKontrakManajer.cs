using ManajemenArtis_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormDaftarKontrakManajer : Form
    {
        FormUtama frm;
        List<Kontrak> listKontrak = new List<Kontrak>();
        Kontrak k;
        public FormDaftarKontrakManajer()
        {
            InitializeComponent();
        }

        private void dataGridViewDaftarTawaranKontrak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarTawaranKontrak.CurrentRow.Cells["IdKontrak"].Value.ToString();
            if (comboBoxKriteria.Text == "SETUJU")
            {
                k = Kontrak.StatusSukses(id);
                FormFixedKontrak frm = new FormFixedKontrak();
                //send value
                frm.labelIDKontrak.Text = k.Id.ToString();
                frm.labelNamaArtis.Text = k.IdArtis.Nama;
                frm.labelNamaManajer.Text = k.IdManajer.Nama;
                frm.labelNamaEvent.Text = k.IdEvent.Nama;
                frm.labelTanggalAkhir.Text = k.TanggalBerakhir.ToShortDateString();
                frm.labelTanggalMulai.Text = k.TanggalMulai.ToShortDateString();
                frm.labelTanggalPengajuan.Text = k.TanggalPengajuan.ToShortDateString();

                #region decrypted-nominal
                string concat = k.Nominal;
                // Define the delimiters
                char[] delimiters = { '|' }; //BUAT PISAHIN PER PART BIAR BISA DIPAHAMI

                // List to store the substrings
                List<string> substrings = new List<string>();

                // Temporary string to build each substring
                string substring = "";

                // Iterate through the characters of the concat string
                foreach (char c in concat)
                {
                    // Check if the character is a delimiter
                    if (Array.IndexOf(delimiters, c) != -1)
                    {
                        // Add the substring to the list if it's not empty
                        if (substring != "")
                        {
                            substrings.Add(substring);
                            substring = ""; // Reset the temporary string
                        }
                    }
                    else
                    {
                        // Append the character to the temporary string
                        substring += c;
                    }
                }

                // Add the last substring to the list if it's not empty
                if (substring != "")
                {
                    substrings.Add(substring);
                }

                // Convert the list to an array
                string[] result = substrings.ToArray();
                byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[0], 5));
                byte[] secKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[1], 7));
                byte[] decryptText = Convert.FromBase64String(result[2]);
                byte[] firstKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[3], 3));
                byte[] resKeyDecrypt = firstKeyDecrypt.Concat(secKeyDecrypt).ToArray();

                string decryptedNominal = Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);
                #endregion

                frm.labelNominal.Text = "IDR." + decryptedNominal;
                frm.labelDeskripsi.Text = k.Deskripsi;
                frm.pictureBoxTTDArtis.Image = ConvertBinaryToImage(k.TandaTanganArtis);
                frm.pictureBoxTTDEvents.Image = ConvertBinaryToImage(k.TandaTanganEvent);
                frm.pictureBoxTTTDManajer.Image = ConvertBinaryToImage(k.TandaTanganManajer);
                frm.Owner = this;
                frm.ShowDialog();
            }
        }

        private void FormDaftarKontrakManajer_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            comboBoxKriteria.SelectedIndex = -1;

        }

        private void FormatDataGrid()
        {
            dataGridViewDaftarTawaranKontrak.Columns.Clear();

            dataGridViewDaftarTawaranKontrak.Columns.Add("IdKontrak", "ID Kontrak");
            dataGridViewDaftarTawaranKontrak.Columns["IdKontrak"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["IdKontrak"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("TanggalPengajuan", "Tanggal Pengajuan");
            dataGridViewDaftarTawaranKontrak.Columns["TanggalPengajuan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["TanggalPengajuan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("NamaArtis", "Nama Artis");
            dataGridViewDaftarTawaranKontrak.Columns["NamaArtis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["NamaArtis"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("Events", "Nama Event");
            dataGridViewDaftarTawaranKontrak.Columns["Events"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["Events"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("Deskripsi", "Deskripsi");
            dataGridViewDaftarTawaranKontrak.Columns["Deskripsi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["Deskripsi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("TanggalMulai", "Tanggal Mulai Kontrak");
            dataGridViewDaftarTawaranKontrak.Columns["TanggalMulai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["TanggalMulai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("TanggalAkhir", "Tanggal Akhir Kontrak");
            dataGridViewDaftarTawaranKontrak.Columns["TanggalAkhir"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["TanggalAkhir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarTawaranKontrak.Columns.Add("Nominal", "Nominal Kontrak");
            dataGridViewDaftarTawaranKontrak.Columns["Nominal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarTawaranKontrak.Columns["Nominal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private byte[] ConvertImageToBinary(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void comboBoxKriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxKriteria.SelectedIndex != -1)
            {
                dataGridViewDaftarTawaranKontrak.Rows.Clear();
                dataGridViewDaftarTawaranKontrak.Columns.Clear();
                //lihat tawaran kontrak yang masih berstatus setuju
                listKontrak = Kontrak.DaftarKontrakManajer(comboBoxKriteria.Text, frm.manajerLogin.IdManajer.ToString());
                //dataGridViewDaftarTawaranKontrak.DataSource = listKontrak;
                if (listKontrak.Count > 0)
                {
                    FormatDataGrid();
                }
                else
                {
                    dataGridViewDaftarTawaranKontrak.DataSource = null;
                }
                if (listKontrak.Count > 0)
                {
                    dataGridViewDaftarTawaranKontrak.RowCount = listKontrak.Count();
                    for (int i = 0; i < listKontrak.Count; i++)
                    {
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["IdKontrak"].Value = listKontrak[i].Id.ToString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalPengajuan"].Value = listKontrak[i].TanggalPengajuan.ToShortDateString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["NamaArtis"].Value = listKontrak[i].IdArtis.Nama;
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Events"].Value = listKontrak[i].IdEvent.Nama;
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Deskripsi"].Value = listKontrak[i].Deskripsi;
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalMulai"].Value = listKontrak[i].TanggalMulai.ToShortDateString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalAkhir"].Value = listKontrak[i].TanggalBerakhir.ToShortDateString();

                        //nampilin nominal
                        string nominal = listKontrak[i].Nominal.ToString();

                        string[] result = nominal.Split('|');
                        byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[0], 5));
                        byte[] secKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[1], 7));
                        byte[] decryptText = Convert.FromBase64String(result[2]);
                        byte[] firstKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[3], 3));
                        byte[] resKeyDecrypt = firstKeyDecrypt.Concat(secKeyDecrypt).ToArray();

                        string decryptedNominal = Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);

                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Nominal"].Value = decryptedNominal;

                        if (comboBoxKriteria.Text == "SETUJU")
                        {
                            //klo sudah setuju
                            if (dataGridViewDaftarTawaranKontrak.ColumnCount == 8)
                            {
                                //tambahin ttd
                                DataGridViewButtonColumn btnTTD = new DataGridViewButtonColumn();
                                btnTTD.Name = "buttonTTDGrid";//nama objek button
                                btnTTD.HeaderText = "Aksi";
                                btnTTD.Text = "Print";
                                btnTTD.UseColumnTextForButtonValue = true;//agar tulisan muncul di button

                                dataGridViewDaftarTawaranKontrak.Columns.Add(btnTTD);//tambahkan button ke grid
                            }
                            else if (dataGridViewDaftarTawaranKontrak.ColumnCount == 9)
                            {
                                dataGridViewDaftarTawaranKontrak.Rows[i].Cells["buttonTTDGrid"].Value = "Print";
                            }

                        }
                    }
                }
            }
        }

    }
}
