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
    public partial class FormDaftarArtis : Form
    {
        List<Artis> listArtis = new List<Artis>();
        FormUtama frm;
        public FormDaftarArtis()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDaftarArtis_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;
            listArtis = Artis.BacaDataArtisManajer(frm.manajerLogin.IdManajer.ToString());
            if (listArtis.Count > 0)
            {
                dataGridViewArtis.DataSource = listArtis;
                dataGridViewArtis.Columns["IdManajer"].Visible = false;
                dataGridViewArtis.Columns["Username"].Visible = false;
                dataGridViewArtis.Columns["Password"].Visible = false;
                if (dataGridViewArtis.Columns.Count == 10)
                {
                    DataGridViewButtonColumn buttonJadwal = new DataGridViewButtonColumn();
                    buttonJadwal.HeaderText = "Aksi";
                    buttonJadwal.Name = "buttonJadwal";
                    buttonJadwal.Text = "Jadwal Artis";
                    buttonJadwal.UseColumnTextForButtonValue = true;
                    dataGridViewArtis.Columns.Add(buttonJadwal);
                }

                for (int i = 0; i < dataGridViewArtis.RowCount; i++)
                {
                    #region decrypt alamat
                    string concatAlamat = dataGridViewArtis.Rows[i].Cells["alamat"].Value.ToString();
                    // Define the delimiters
                    char[] delimitersAlamat = { '|' }; //BUAT PISAHIN PER PART BIAR BISA DIPAHAMI

                    // List to store the substrings
                    List<string> substringsAlamat = new List<string>();

                    // Temporary string to build each substring
                    string substringAlamat = "";

                    // Iterate through the characters of the concat string
                    foreach (char c in concatAlamat)
                    {
                        // Check if the character is a delimiter
                        if (Array.IndexOf(delimitersAlamat, c) != -1)
                        {
                            // Add the substring to the list if it's not empty
                            if (substringAlamat != "")
                            {
                                substringsAlamat.Add(substringAlamat);
                                substringAlamat = ""; // Reset the temporary string
                            }
                        }
                        else
                        {
                            // Append the character to the temporary string
                            substringAlamat += c;
                        }
                    }

                    // Add the last substring to the list if it's not empty
                    if (substringAlamat != "")
                    {
                        substringsAlamat.Add(substringAlamat);
                    }

                    // Convert the list to an array
                    string[] resultAlamat = substringsAlamat.ToArray();
                    byte[] ivDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[0], 5));
                    byte[] secKeyDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[1], 7));
                    byte[] decryptTextAlamat = Convert.FromBase64String(resultAlamat[2]);
                    byte[] firstKeyDecryptAlamat = Convert.FromBase64String(Keamanan.DecryptCaesar(resultAlamat[3], 3));
                    byte[] resKeyDecryptAlamat = firstKeyDecryptAlamat.Concat(secKeyDecryptAlamat).ToArray();

                    dataGridViewArtis.Rows[i].Cells["alamat"].Value = Keamanan.DecryptAES(decryptTextAlamat, resKeyDecryptAlamat, ivDecryptAlamat);
                    #endregion

                    #region decrypt no telp
                    string noTelp = dataGridViewArtis.Rows[i].Cells["No_Telp"].Value.ToString();
                    string[] pisah = noTelp.Split('|');

                    // Konversi kembali string yang telah dihapus padding menjadi byte array
                    byte[] sepuluhpertama = Convert.FromBase64String(pisah[0]);
                    byte[] sisanya = Convert.FromBase64String(pisah[3]);
                    byte[] combined = sepuluhpertama.Concat(sisanya).ToArray();
                    string comb = Convert.ToBase64String(combined);

                    byte[] caesarKey = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[1], 4));
                    byte[] caesarIv = Convert.FromBase64String(Keamanan.DecryptCaesar(pisah[2], 5));
                    dataGridViewArtis.Rows[i].Cells["No_Telp"].Value = Keamanan.DecryptAES(combined, caesarKey, caesarIv);
                    //string yyy = Convert.ToBase64String(cipheredNoTelp);
                }
            }
            else
            {
                dataGridViewArtis.DataSource = null;
            }
        }

        private void dataGridViewArtis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewArtis.CurrentRow.Cells["IdArtis"].Value.ToString();
            Artis artis = Artis.AmbilDataByKode(id);

            if (!(artis == null))
            {
                if (e.ColumnIndex == dataGridViewArtis.Columns["buttonJadwal"].Index && e.RowIndex >= 0)
                {
                    FormDaftarJadwalManajerArtis form = new FormDaftarJadwalManajerArtis();
                    form.Owner = this;
                    List<JadwalPekerjaan> jadwal = new List<JadwalPekerjaan>();
                    jadwal = JadwalPekerjaan.BacaData("idArtis", artis.IdArtis.ToString());
                    if (jadwal.Count > 0)
                    {
                        form.dgvJadwal.DataSource = jadwal;
                            for (int i = 0; i < form.dgvJadwal.RowCount; i++)
                            {
                                string tanggal = form.dgvJadwal.Rows[i].Cells["Tanggal"].Value.ToString();

                                //string tgl = listJadwalPekerjaanTiapArtis[i].Tanggal;
                                string[] result1 = tanggal.Split('|');
                                byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result1[0], 21));
                                byte[] KeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result1[1], 9));
                                byte[] decryptText = Convert.FromBase64String(result1[2]);

                                string decryptedtanggal = Keamanan.DecryptAES(decryptText, KeyDecrypt, ivDecrypt);
                                form.dgvJadwal.Rows[i].Cells["tanggal"].Value = decryptedtanggal;

                                string jam = form.dgvJadwal.Rows[i].Cells["jam"].Value.ToString();

                                //string tgl = listJadwalPekerjaanTiapArtis[i].Tanggal;
                                string[] result2 = jam.Split('|');
                                byte[] ivDecrypt2 = Convert.FromBase64String(Keamanan.DecryptCaesar(result2[1], 18));
                                byte[] KeyDecrypt2 = Convert.FromBase64String(Keamanan.DecryptCaesar(result2[2], 13));
                                byte[] decryptText2 = Convert.FromBase64String(result2[0]);

                                string decryptedJam = Keamanan.DecryptAES(decryptText2, KeyDecrypt2, ivDecrypt2);
                                form.dgvJadwal.Rows[i].Cells["jam"].Value = decryptedJam;
                            }
                    }
                    else
                    {
                        form.dgvJadwal.DataSource = null;
                    }
                    form.ShowDialog();
                }
            }
        }
    }
}
#endregion