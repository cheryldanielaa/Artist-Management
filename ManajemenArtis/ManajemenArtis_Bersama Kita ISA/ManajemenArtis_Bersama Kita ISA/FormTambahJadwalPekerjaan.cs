using ManajemenArtis_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormTambahJadwalPekerjaan : Form
    {
        FormUtama frmUtama;
        List<Artis> listArtisSaya = new List<Artis>(); //list artis yang saya naungi
        Manajer manajerArtis;
        List<Kontrak> listEvent = new List<Kontrak>();
        Artis selArtis;
        Kontrak selEvent;
       
        public FormTambahJadwalPekerjaan()
        {
            InitializeComponent();
        }

        private void FormTambahJadwalPekerjaan_Load(object sender, EventArgs e)
        {
            try
            {
                comboBoxNama.SelectedIndex = -1;
                frmUtama = (FormUtama)this.MdiParent;
                manajerArtis = frmUtama.manajerLogin;
                listArtisSaya = Artis.BacaDataArtisManajer(manajerArtis.IdManajer.ToString());
                if (listArtisSaya.Count > 0)
                {
                    comboBoxNama.DataSource = listArtisSaya;
                    comboBoxNama.DisplayMember = "Nama";
                }
                else
                {
                    comboBoxNama.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                
                    byte[] key = new byte[32];
                    byte[] iv = new byte[16];

                    using (var rng = new RNGCryptoServiceProvider())
                    {
                        rng.GetBytes(key);
                        rng.GetBytes(iv);
                    }

                    //simpan data jadwal pekerjaan
                    selArtis = (Artis)comboBoxNama.SelectedItem;
                    selEvent = (Kontrak)comboBoxEvent.SelectedItem;
                    byte[] tanggal = Keamanan.EncryptAES(dtpTanggal.Text, key, iv);
                    byte[] jam = Keamanan.EncryptAES(dtpJam.Text, key, iv);
                    string tanggal1 = Convert.ToBase64String(tanggal);
                    string cipheredKeyTgl = Keamanan.EncryptCaesar(Convert.ToBase64String(key), 9);
                    string cipheredIVTgl = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 21);

                    string jam1 = Convert.ToBase64String(jam);
                    string cipheredKeyJam = Keamanan.EncryptCaesar(Convert.ToBase64String(key), 13);
                    string cipheredIVJam = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 18);

                    string concatTanggal = cipheredIVTgl + "|" + cipheredKeyTgl + "|" + tanggal1;
                    string concatJam = jam1 + "|" + cipheredIVJam + "|" + cipheredKeyJam;
                    JadwalPekerjaan jadwal = new JadwalPekerjaan(int.Parse(labelID.Text), concatTanggal, concatJam,
                        txtDeskripsi.Text, selEvent.IdEvent, selArtis);
                    JadwalPekerjaan.TambahData(jadwal);
                    MessageBox.Show("Selamat jadwal pekerjaan baru untuk " + selArtis.Nama + " pada event " + selEvent.IdEvent.Nama +
                        " berhasil ditambahkan!");
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pesan Kesalahan");
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            comboBoxNama.SelectedIndex = -1;
            comboBoxEvent.SelectedIndex = -1;
            dtpTanggal.Value = DateTime.Now;
            dtpJam.Value = DateTime.Now;
            txtDeskripsi.Clear();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxNama_SelectedIndexChanged(object sender, EventArgs e)
        {
            selArtis = (Artis)comboBoxNama.SelectedItem;
            //display nama events yg ttd kontrak statusnya sdh sukses
            if (comboBoxNama.SelectedIndex != -1)
            {
                listEvent = Kontrak.BacaDataSatu(selArtis.IdArtis.ToString());
                if (listEvent.Count > 0)
                {
                    comboBoxEvent.DataSource = listEvent;
                    comboBoxEvent.DisplayMember = "idEvents.nama";
                }
                else
                {
                    comboBoxEvent.DataSource = null;
                }
            }
            
        }
    }
}
