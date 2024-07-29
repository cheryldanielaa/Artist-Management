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
    public partial class FormTTDKontrak : Form
    {
        FormUtama frm;
        string fileName;
        Kontrak k;
        public FormTTDKontrak()
        {
            InitializeComponent();
        }

        public static Image ConvertBinaryToImage(byte[] imageData)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    // Membaca gambar dari aliran memori
                    Image image = Image.FromStream(ms);
                    return image;//exception error
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting binary to image: " + ex.Message);
                return null;
            }
        }

        private void FormTTDKontrak_Load(object sender, EventArgs e)
        {
            frm = (FormUtama)this.Owner.MdiParent;
            //baca data apakah ada username dengan nama yang diisikan
            Kontrak k = Kontrak.AmbilDataByKode(labelIDKontrak.Text);
            if (!(k == null))
            {
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
                labelNominal.Text = decryptedNominal;
                pictureBoxTTDEvents.Image = ConvertBinaryToImage(k.TandaTanganEvent);
                pictureBoxTTTDManajer.Image = ConvertBinaryToImage(k.TandaTanganEvent);
            }
        }

        private byte[] ConvertImageToBinary (Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm.manajerLogin == null && frm.artisLogin != null)
                {
                    if (pictureBoxTTDArtis.Image != null)
                    {
                        k = Kontrak.AmbilDataByKode(labelIDKontrak.Text);
                        Kontrak.Setuju(k); //ubah status kontrak jadi setuju

                        #region steganography ttdArtis -- HIDE MESSAGE TGL TTD, UTK EVENT APA, NAMA YG TTD SIAPA

                        //simpan informasi yang mau dihide
                        string hideMessage = labelNamaArtis.Text + " menandatangani dokumen kontrak dengan " +
                            labelNamaEvent.Text + " pada tanggal " + DateTime.Now.ToString();
                        Bitmap img = new Bitmap(pictureBoxTTDArtis.Image);
                        for (int i = 0; i < img.Width; i++)
                        {
                            for (int j = 0; j < img.Height; j++)
                            {
                                Color pixel = img.GetPixel(i, j);
                                if (i < 1 && j < hideMessage.Length)
                                {


                                    char letter = Convert.ToChar(hideMessage.Substring(j, 1));
                                    int value = Convert.ToInt32(letter);

                                    img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));

                                }
                                if (i == img.Width - 1 && j == img.Height - 1)
                                {
                                    img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, hideMessage.Length));
                                }
                            }

                        }
                        //SIMPAN YANG SUDAH DISTEGANO KE DATABASE
                        #endregion

                        byte[] TandaTanganArtis = ConvertImageToBinary(img);
                        Kontrak idTerbaru = Kontrak.AmbilIdTerbaru();
                        idTerbaru.TandaTanganArtis = TandaTanganArtis;
                        Kontrak.UpdateTTDArtis(idTerbaru);
                        MessageBox.Show("Tanda tangan Anda untuk kontrak dengan ID " + k.Id.ToString() + " berhasil disimpan!");
                        this.Close();
                        FormDaftarTawaranKontrakArtis form1 = (FormDaftarTawaranKontrakArtis)this.Owner;
                        form1.FormDaftarTawaranKontrakArtis_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Bubuhkan tanda tangan pada kontrak Anda!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void btnTTDArtis_Click(object sender, EventArgs e)
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
                    pictureBoxTTDArtis.Image = Image.FromFile(fileName);
                }
            }
        }
    }
}
