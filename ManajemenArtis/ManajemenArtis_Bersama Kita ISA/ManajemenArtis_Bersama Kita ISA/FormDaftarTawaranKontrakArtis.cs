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
    public partial class FormDaftarTawaranKontrakArtis : Form
    {
        List<Kontrak> listTawaranKontrakSaya = new List<Kontrak>();
        FormUtama frm;
        Kontrak k;
        public FormDaftarTawaranKontrakArtis()
        {
            InitializeComponent();
        }

        public void FormDaftarTawaranKontrakArtis_Load(object sender, EventArgs e)
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

        private void dataGridViewDaftarTawaranKontrak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarTawaranKontrak.CurrentRow.Cells["IdKontrak"].Value.ToString();
            if (comboBoxKriteria.Text == "PENDING")
            {
                k = Kontrak.AmbilDataByKode(id);
                if (e.ColumnIndex == dataGridViewDaftarTawaranKontrak.Columns["buttonSetujuGrid"].Index && e.RowIndex >= 0)
                {
                    //open new form dan isi tanda tangan dia
                    FormTTDKontrak frm = new FormTTDKontrak();

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
                    frm.Owner = this;
                    frm.ShowDialog();
                }
                else if (e.ColumnIndex == dataGridViewDaftarTawaranKontrak.Columns["buttonTolakGrid"].Index && e.RowIndex >= 0)
                {
                    Kontrak.Batal(k);
                    this.comboBoxKriteria_SelectedIndexChanged_1(sender, e);
                }
            }
            else if (comboBoxKriteria.Text == "SETUJU")
            {
                k = Kontrak.StatusSukses(id);
                if (e.ColumnIndex == dataGridViewDaftarTawaranKontrak.Columns["buttonPrintGrid"].Index && e.RowIndex >= 0)
                {
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

        private void comboBoxKriteria_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            if(comboBoxKriteria.SelectedIndex != -1)
            {
                dataGridViewDaftarTawaranKontrak.DataSource = null;

                listTawaranKontrakSaya = Kontrak.TawaranKontrakSaya(comboBoxKriteria.Text, frm.artisLogin.IdArtis.ToString());
                dataGridViewDaftarTawaranKontrak.DataSource = listTawaranKontrakSaya;
                if (listTawaranKontrakSaya.Count > 0)
                {
                    FormatDataGrid();

                }
                else
                {
                    dataGridViewDaftarTawaranKontrak.DataSource = null;
                }
                if (listTawaranKontrakSaya.Count > 0)
                {
                    for (int i = 0; i < dataGridViewDaftarTawaranKontrak.RowCount; i++)
                    {
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["IdKontrak"].Value = listTawaranKontrakSaya[i].Id.ToString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalPengajuan"].Value = listTawaranKontrakSaya[i].TanggalPengajuan.ToShortDateString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Events"].Value = listTawaranKontrakSaya[i].IdEvent.Nama;
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Deskripsi"].Value = listTawaranKontrakSaya[i].Deskripsi;
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalMulai"].Value = listTawaranKontrakSaya[i].TanggalMulai.ToShortDateString();
                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["TanggalAkhir"].Value = listTawaranKontrakSaya[i].TanggalBerakhir.ToShortDateString();

                        string nominal = listTawaranKontrakSaya[i].Nominal.ToString();

                        string[] result = nominal.Split('|');
                        byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[0], 5));
                        byte[] secKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[1], 7));
                        byte[] decryptText = Convert.FromBase64String(result[2]);
                        byte[] firstKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[3], 3));
                        byte[] resKeyDecrypt = firstKeyDecrypt.Concat(secKeyDecrypt).ToArray();

                        string decryptedNominal = Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);

                        dataGridViewDaftarTawaranKontrak.Rows[i].Cells["Nominal"].Value = decryptedNominal;

                        if (comboBoxKriteria.Text == "PENDING")
                        {
                            //klo status masih pending kasih opsi mau setuju atau tolak kontrak
                            if (dataGridViewDaftarTawaranKontrak.Columns.Count == 7)
                            {
                                //buat button opsi setuju atau tolak
                                DataGridViewButtonColumn btnSetuju = new DataGridViewButtonColumn();
                                btnSetuju.Name = "buttonSetujuGrid";//nama objek button
                                btnSetuju.HeaderText = "SETUJU";
                                btnSetuju.Text = "Setuju";//tulisan di button
                                btnSetuju.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                                dataGridViewDaftarTawaranKontrak.Columns.Add(btnSetuju);//tambahkan button ke grid
                                dataGridViewDaftarTawaranKontrak.Rows[i].Cells["buttonSetujuGrid"].Value = "SETUJU";
                                DataGridViewButtonColumn btnTolak = new DataGridViewButtonColumn();
                                btnTolak.Name = "buttonTolakGrid";//nama objek button
                                btnTolak.HeaderText = "TOLAK";
                                btnTolak.Text = "Tolak";//tulisan di button
                                btnTolak.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                                dataGridViewDaftarTawaranKontrak.Columns.Add(btnTolak);//tambahkan button ke grid
                                                                                       //dataGridViewDaftarTawaranKontrak.Rows[i].Cells["buttonTolakGrid"].Value = "TOLAK";
                            }
                        }
                        else if (comboBoxKriteria.Text == "SETUJU")
                        {
                            if (dataGridViewDaftarTawaranKontrak.Columns.Count == 7)
                            {
                                DataGridViewButtonColumn btnPrint = new DataGridViewButtonColumn();
                                btnPrint.Name = "buttonPrintGrid";//nama objek button
                                btnPrint.HeaderText = "PRINT";
                                btnPrint.Text = "Print";
                                btnPrint.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                                dataGridViewDaftarTawaranKontrak.Columns.Add(btnPrint);//tambahkan button ke grid
                            }
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
