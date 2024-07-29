using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManajemenArtis_LIB;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormTambahKontrak : Form
    {
        string fileName;
        List<Eventss> listEvent = new List<Eventss>();
        FormUtama frm;
        List<Artis> listArtis = new List<Artis>();
        public FormTambahKontrak()
        {
            InitializeComponent();
        }

        private void FormTambahKontrak_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.MdiParent;

            //DISPLAY SEMUA EVENTS YANG ADA
            listEvent = Eventss.BacaData("", "");
            comboBoxEvent.DataSource = listEvent;
            comboBoxEvent.DisplayMember = "nama";

            //DISPLAY NAMA MANAJER YG SEDANG LOGIN
            comboBoxManajer.Items.Add(frm.manajerLogin);
            comboBoxManajer.DisplayMember = "Nama";
            comboBoxManajer.SelectedIndex = 0;

            //DISPLAY NAMA ARTIS YANG DIA NAUNGI
            listArtis = Artis.BacaDataArtisManajer(frm.manajerLogin.IdManajer.ToString());
            comboBoxArtis.DataSource = listArtis;
            comboBoxArtis.DisplayMember = "Nama";

            dtpTglDibuat.Value = DateTime.Now;

        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm.manajerLogin != null && frm.artisLogin == null)
                {
                    // Ambil events yang dipilih
                    Eventss selectedEvent = (Eventss)comboBoxEvent.SelectedItem;

                    // Ambil artis yang dipilih
                    Artis selectedArtis = (Artis)comboBoxArtis.SelectedItem;
                    // Ambil manajer yang sedang login
                    Manajer selectedManajer = (Manajer)comboBoxManajer.SelectedItem;

                    if (pictureBoxTTDEvents.Image == null)
                    {
                        // Tampilkan pesan kesalahan jika tanda tangan events tidak ada
                        MessageBox.Show("Bubuhkan tanda tangan pihak events pada kontrak Anda!");
                    }
                    else if (pictureBoxTTTDManajer.Image == null)
                    {
                        // Tampilkan pesan kesalahan jika tanda tangan manajer tidak ada
                        MessageBox.Show("Bubuhkan tanda tangan pihak manajer pada kontrak Anda!");
                    }
                    else
                    {
                        if (dtpTglMulai.Value.Date < selectedEvent.TanggalAwal.Date || dtpTglMulai.Value.Date > selectedEvent.TanggalAkhir.Date)
                        {
                            MessageBox.Show("Cek kembali tanggal mulai kontrak!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (dtpTglAkhir.Value.Date > selectedEvent.TanggalAkhir.Date || dtpTglAkhir.Value.Date < selectedEvent.TanggalAwal.Date)
                        {
                            MessageBox.Show("Cek kembali tanggal akhir kontrak!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            #region stegano1
                            // Simpan informasi yang akan dihide di tanda tangan manajer
                            string hideMessage = comboBoxManajer.Text + " menandatangani dokumen kontrak untuk artis " + comboBoxArtis.SelectedText +
                                " dengan " +
                                comboBoxEvent.SelectedText + " pada tanggal " + DateTime.Now.ToString();
                            Bitmap img1 = new Bitmap(pictureBoxTTTDManajer.Image);
                            for (int i = 0; i < img1.Width; i++)
                            {
                                for (int j = 0; j < img1.Height; j++)
                                {
                                    Color pixel = img1.GetPixel(i, j);
                                    if (i < 1 && j < hideMessage.Length)
                                    {
                                        char letter = Convert.ToChar(hideMessage.Substring(j, 1));
                                        int value = Convert.ToInt32(letter);

                                        img1.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                                    }
                                    if (i == img1.Width - 1 && j == img1.Height - 1)
                                    {
                                        img1.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, hideMessage.Length));
                                    }
                                }
                            }
                            // Simpan informasi yang akan dihide di tanda tangan events
                            #endregion

                            #region stegano2
                            string hideMessage2 = comboBoxEvent.Text + " mengajukan kontrak untuk artis " + comboBoxArtis.SelectedText +
                                " dengan persetujuan " + comboBoxManajer.SelectedText + " selaku manajer dari artis tersebut " +
                                 " pada tanggal " + DateTime.Now.ToString();
                            Bitmap img2 = new Bitmap(pictureBoxTTDEvents.Image);
                            for (int i = 0; i < img2.Width; i++)
                            {
                                for (int j = 0; j < img2.Height; j++)
                                {
                                    Color pixel = img2.GetPixel(i, j);
                                    if (i < 1 && j < hideMessage2.Length)
                                    {
                                        char letter = Convert.ToChar(hideMessage2.Substring(j, 1));
                                        int value = Convert.ToInt32(letter);

                                        img2.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                                    }
                                    if (i == img2.Width - 1 && j == img2.Height - 1)
                                    {
                                        img2.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, hideMessage2.Length));
                                    }
                                }
                            }
                            // Simpan yang sudah distegano ke database
                            #endregion

                            #region encrypt nominal
                            //PLANNING - ENKRIPSI PASSWORD DGN AES
                            //KEY DIBELAH JD 2 ARRAY, TRS MASING-MASING DI CAESAR CIPHER >> 1ST HALF (3), 2ND HALF(7)
                            //IV DICAESAR >> SWIFT 5

                            //GENERATE RANDOM KEY DAN IV
                            byte[] key = new byte[32]; //kunci sepanjang 256 bit
                            byte[] iv = new byte[16]; //128 bit IV

                            using (var rng = new RNGCryptoServiceProvider())
                            {
                                rng.GetBytes(key);
                                rng.GetBytes(iv);
                            }

                            byte[] encryptedNominal = Keamanan.EncryptAES(txtNominal.Text, key, iv);
                            string cipheredNominal = Convert.ToBase64String(encryptedNominal);

                            // Determine the length of each split
                            int splitLength = key.Length / 2;

                            //// Pisah key jadi 2 part array
                            byte[] firstHalfKey = new byte[splitLength];
                            byte[] secondHalfKey = new byte[key.Length - splitLength];

                            //// Copy the appropriate portions of the original array into the new arrays
                            Array.Copy(key, 0, firstHalfKey, 0, splitLength);
                            Array.Copy(key, splitLength, secondHalfKey, 0, key.Length - splitLength);

                            ////convert byte key ke string dan convert ke caesar

                            string cipheredFirstKey = Keamanan.EncryptCaesar(Convert.ToBase64String(firstHalfKey), 3);
                            string cipheredSecKey = Keamanan.EncryptCaesar(Convert.ToBase64String(secondHalfKey), 7);
                            string cipheredIV = Keamanan.EncryptCaesar(Convert.ToBase64String(iv), 5);

                            //CONCAT CIPHERED STRING, CIPHERED PASSWORD, CIPHERED KEY >>YANG DISIMPEN DI DATABASE
                            string concat = cipheredIV + "|" + cipheredSecKey + "|" + cipheredNominal + "|" + cipheredFirstKey;
                            #endregion

                            Kontrak kntrk = new Kontrak(int.Parse(labelID.Text), txtDeskEvent.Text, dtpTglDibuat.Value,
                                                        concat, "PENDING", dtpTglMulai.Value, dtpTglAkhir.Value,
                                                        selectedManajer, selectedEvent, selectedArtis);
                            Kontrak.TambahData(kntrk);
                            byte[] TandaTanganEvent = ConvertImageToBinary(img2);
                            byte[] TandaTanganManajer = ConvertImageToBinary(img1);

                            Kontrak idTerbaru = Kontrak.AmbilIdTerbaru();
                            idTerbaru.TandaTanganEvent = TandaTanganEvent;
                            idTerbaru.TandaTanganManajer = TandaTanganManajer;
                            Kontrak.UpdateTTDEvents(idTerbaru);
                            Kontrak.UpdateTTDManajer(idTerbaru);
                            MessageBox.Show("Tanda tangan Manajer dan Events untuk kontrak dengan ID " + idTerbaru.Id + " berhasil disimpan!");
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnTTDManajer_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "*.jpeg, *.jpg)|*.jpeg;*.jpg|PNG Images (*.png)|*.png",
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    pictureBoxTTTDManajer.Image = Image.FromFile(fileName);
                }
            }
        }

        private void btnTTDEvents_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "*.jpeg, *.jpg)|*.jpeg;*.jpg|PNG Images (*.png)|*.png",
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    pictureBoxTTDEvents.Image = Image.FromFile(fileName);
                }
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            comboBoxArtis.SelectedItem = -1;
            comboBoxManajer.SelectedItem = -1;
            comboBoxArtis.SelectedItem = -1;
            txtDeskEvent.Text = "";
            txtNominal.Text = "";
            dtpTglMulai.Value = DateTime.Now;
            dtpTglAkhir.Value = DateTime.Now;
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
