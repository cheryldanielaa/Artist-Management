using iTextSharp.text.pdf;
using iTextSharp.text;
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
using ManajemenArtis_LIB;
using Image = System.Drawing.Image;

namespace ManajemenArtis_Bersama_Kita_ISA
{
    public partial class FormFixedKontrak : Form
    {
        public FormFixedKontrak()
        {
            InitializeComponent();
        }

        private void FormFixedKontrak_Load(object sender, EventArgs e)
        {
            //baca image artis
            Bitmap imgArtis = new Bitmap(pictureBoxTTDArtis.Image);
            string messageArtis = "";
            Color lastPixelArtis = imgArtis.GetPixel(imgArtis.Width - 1, imgArtis.Height - 1);
            int msgLengthArtis = lastPixelArtis.B;
            for (int i = 0; i < imgArtis.Width; i++)
            {
                for (int j = 0; j < imgArtis.Height; j++)
                {
                    Color pixel = imgArtis.GetPixel(i, j);
                    if (i < 1 && j < msgLengthArtis)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        messageArtis += letter;
                    }
                }
            }

            //baca image events
            Bitmap imgEvents = new Bitmap(pictureBoxTTDEvents.Image);
            string messageEvents = "";
            Color lastPixelEvents = imgEvents.GetPixel(imgEvents.Width - 1, imgEvents.Height - 1);
            int msgLengthEvents = lastPixelEvents.B;
            for (int i = 0; i < imgEvents.Width; i++)
            {
                for (int j = 0; j < imgEvents.Height; j++)
                {
                    Color pixel = imgEvents.GetPixel(i, j);
                    if (i < 1 && j < msgLengthEvents)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        messageEvents += letter;
                    }
                }
            }

            //baca image manajer
            Bitmap imgManajer = new Bitmap(pictureBoxTTTDManajer.Image);
            string messageManajer = "";
            Color lastPixelManajer = imgManajer.GetPixel(imgManajer.Width - 1, imgManajer.Height - 1);
            int msgLengthManajer = lastPixelManajer.B;
            for (int i = 0; i < imgManajer.Width; i++)
            {
                for (int j = 0; j < imgManajer.Height; j++)
                {
                    Color pixel = imgManajer.GetPixel(i, j);
                    if (i < 1 && j < msgLengthManajer)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        messageManajer += letter;
                    }
                }

            }

            //cek isi details contains informasi yang sama atau ga
            if (messageEvents.Contains(labelNamaEvent.Text) && 
                messageArtis.Contains(labelNamaArtis.Text) &&
                messageManajer.Contains(labelNamaManajer.Text))
            {
                buttonPrint.Enabled = true;
            }
        }

        private Image ConvertBinaryToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true })
            {
                iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();
                        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 14);
                        //BUAT PARAGRAPH HEADER
                        //FONT UNTUK HEADER 28
                        iTextSharp.text.Font font1 = new iTextSharp.text.Font(bf, 28);
                        iTextSharp.text.Font font2 = new iTextSharp.text.Font(bf, 18);
                        Paragraph heading = new Paragraph("KONTRAK KERJASAMA", font1);
                        Paragraph alamat = new Paragraph("ISA TOWN \nSurabaya - 123456", font2);
                        heading.Alignment = Element.ALIGN_CENTER;
                        alamat.Alignment = Element.ALIGN_CENTER;
                        doc.Add(new Paragraph(heading));
                        doc.Add(new Paragraph(alamat));
                        //kasih jarak satu garis
                        doc.Add(new Paragraph("==============================================================", font));
                        Paragraph tanggalSurat = new Paragraph("Surabaya, " + labelTanggalPengajuan.Text, font);
                        tanggalSurat.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(tanggalSurat);
                        doc.Add(new Paragraph("Yang bertanda tangan di bawah ini :", font));
                        doc.Add(new Paragraph("Perusahaan : " + labelNamaEvent.Text, font));
                        doc.Add(new Paragraph("Menyatakan bahwa nama yang tertera diatas mewakili " + labelNamaEvent.Text +
                            " sebagai PIHAK 1 / PIHAK YANG MENGAJUKAN KONTRAK", font));
                        doc.Add(new Paragraph(""));
                        doc.Add(new Paragraph("Dan yang bertanda tangan di bawah ini : ", font));
                        doc.Add(new Paragraph(""));
                        doc.Add(new Paragraph("Nama : " + labelNamaArtis.Text, font));
                        doc.Add(new Paragraph("Menyatakan bahwa nama yang tertera diatas tersebut sebagai PIHAK 2/TALENT", font));
                        doc.Add(new Paragraph(""));
                        Paragraph tambahan = new Paragraph("Dengan ini membuat suatu perjanjian kerja / kontrak kerjasama antara PIHAK 1 / " +
                            labelNamaEvent.Text + " dengan PIHAK 2/" + labelNamaArtis.Text + ", dengan persetujuan dari manajer  dari saudara/i " +
                            labelNamaArtis.Text + " " +
                            "yaitu " + labelNamaManajer.Text + " sebagai PIHAK KETIGA.", font);
                        tambahan.Alignment = Element.ALIGN_JUSTIFIED;

                        doc.Add(new Paragraph(tambahan));

                        Paragraph details = new Paragraph("Kedua belah pihak telah sepakat untuk mengadakan " +
                            "ikatan perjanjian kerjasama dimana " +
                            labelNamaArtis.Text + " akan bekerja dengan detail " + labelDeskripsi.Text +
                            " pada event " + labelNamaEvent.Text + " yang diselenggarakan pada tanggal " +
                            labelTanggalMulai.Text + " hingga tanggal " + labelTanggalAkhir.Text + " dengan nominal kontrak sebesar " + labelNominal.Text, font);

                        details.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(new Paragraph(details));
                        doc.Add(new Paragraph("\n"));
                        doc.Add(new Paragraph("Tanda Tangan Artis                  Tanda Tangan Manajer                  Tanda Tangan Events", font));
                        
                        //TAMBAHIN SPACE BUAT TTD DAN NAMA TERANG
                        #region gambar ttd
                        // Tentukan jalur file gambar
                        Image ttdArtis = pictureBoxTTDArtis.Image; //atur ttd artis
                        // Buat objek gambar
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ttdArtis,
                            System.Drawing.Imaging.ImageFormat.Png);
                        // Atur posisi dan ukuran gambar
                        img.SetAbsolutePosition(40f, 225f); // Atur posisi gambar (koordinat x, y)
                        img.ScaleAbsolute(100f, 100f); // Atur ukuran gambar (lebar, tinggi)

                        // Tambahkan gambar ke dokumen
                        doc.Add(img);

                        // Tentukan jalur file gambar
                        Image ttdManajer = pictureBoxTTTDManajer.Image; //atur ttd manajer
                        // Buat objek gambar
                        iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(ttdManajer,
                            System.Drawing.Imaging.ImageFormat.Png);
                        // Atur posisi dan ukuran gambar
                        img2.SetAbsolutePosition(225f, 225f); // Atur posisi gambar (koordinat x, y)
                        img2.ScaleAbsolute(100f, 100f); // Atur ukuran gambar (lebar, tinggi)

                        // Tambahkan gambar ke dokumen
                        doc.Add(img2);

                        // Tentukan jalur file gambar
                        Image ttdEvents = pictureBoxTTDEvents.Image; //atur ttd events
                        // Buat objek gambar
                        iTextSharp.text.Image img3 = iTextSharp.text.Image.GetInstance(ttdEvents,
                            System.Drawing.Imaging.ImageFormat.Png);
                        // Atur posisi dan ukuran gambar
                        img3.SetAbsolutePosition(415f, 225f); // Atur posisi gambar (koordinat x, y)
                        img3.ScaleAbsolute(100f, 100f); // Atur ukuran gambar (lebar, tinggi)

                        // Tambahkan gambar ke dokumen
                        doc.Add(img3);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message); ;
                    }
                    finally
                    {
                        doc.Close();
                    }
                }
            }
        }

        private void btnDetailsTTDArtis_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBoxTTDArtis.Image);
            string message = "";
            Color lastPixel = img.GetPixel(img.Width - 1, img.Height - 1);
            int msgLength = lastPixel.B;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (i < 1 && j < msgLength)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        message += letter;
                    }
                }
            }
            MessageBox.Show(message);
        }

        private void buttonDetailsManajer_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBoxTTTDManajer.Image);
            string message = "";
            Color lastPixel = img.GetPixel(img.Width - 1, img.Height - 1);
            int msgLength = lastPixel.B;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (i < 1 && j < msgLength)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        message += letter;
                    }
                }
            }
            MessageBox.Show(message);
        }

        private void buttonDetailsEvent_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBoxTTDEvents.Image);
            string message = "";
            Color lastPixel = img.GetPixel(img.Width - 1, img.Height - 1);
            int msgLength = lastPixel.B;
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    if (i < 1 && j < msgLength)
                    {
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = System.Text.Encoding.ASCII.GetString(new byte[]
                        {
                            Convert.ToByte(c)
                        });
                        message += letter;
                    }
                }
            }
            MessageBox.Show(message);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(labelIDKontrak.Text);
        }
    }
}
