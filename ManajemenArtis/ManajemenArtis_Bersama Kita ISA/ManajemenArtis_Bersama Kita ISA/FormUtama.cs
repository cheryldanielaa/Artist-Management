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
    public partial class FormUtama : Form
    {
        public Artis artisLogin;
        public Manajer manajerLogin;
        public Admin adminLogin;

        List<JadwalPekerjaan> jp = new List<JadwalPekerjaan>();
        public FormUtama()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.IsMdiContainer = true;
            try
            {
                Koneksi koneksi = new Koneksi();
                FormLogin frm = new FormLogin();
                frm.Owner = this;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //cek apakah yg login artis
                    if (artisLogin != null && manajerLogin == null && adminLogin == null) //untuk artis
                    {
                        //menu yg bisa diakses artis apa aja
                        lihatJadwalPekerjaanToolStripMenuItem.Visible = true;
                        buatKontrakToolStripMenuItem.Visible = false;
                        tambahJadwalPekerjaanToolStripMenuItem.Visible = false;
                        registerAkunArtisToolStripMenuItem.Visible = false;
                        registerAkunManajerToolStripMenuItem.Visible = false;
                        daftarKaryaToolStripMenuItem.Visible = false;
                        daftarEventsToolStripMenuItem.Visible = false;
                        daftarKontrakToolStripMenuItem.Visible = false;
                        lihatTawaranKontrakToolStripMenuItem.Visible = true;
                        daftarArtisToolStripMenuItem.Visible = false;
                        ubahProfilToolStripMenuItem.Visible = true;
                    }
                    else if (artisLogin == null && manajerLogin != null && adminLogin == null)
                    {
                        //untuk manajer
                        //menu yg bisa diakses artis apa aja
                        lihatJadwalPekerjaanToolStripMenuItem.Visible = false;
                        buatKontrakToolStripMenuItem.Visible = true;
                        tambahJadwalPekerjaanToolStripMenuItem.Visible = true;
                        registerAkunArtisToolStripMenuItem.Visible = false;
                        registerAkunManajerToolStripMenuItem.Visible = false;
                        daftarKaryaToolStripMenuItem.Visible = true;
                        daftarEventsToolStripMenuItem.Visible = true;
                        daftarKontrakToolStripMenuItem.Visible = true;
                        lihatTawaranKontrakToolStripMenuItem.Visible = false;
                        daftarArtisToolStripMenuItem.Visible = true;
                        ubahProfilToolStripMenuItem.Visible = true;

                    }
                    else if (artisLogin == null && manajerLogin == null && adminLogin != null)
                    {
                        //admin hanya bisa buatin akun utk artis & manajer aja
                        lihatJadwalPekerjaanToolStripMenuItem.Visible = false;
                        buatKontrakToolStripMenuItem.Visible = false;
                        tambahJadwalPekerjaanToolStripMenuItem.Visible = false;
                        registerAkunArtisToolStripMenuItem.Visible = true;
                        registerAkunManajerToolStripMenuItem.Visible = true;
                        daftarKaryaToolStripMenuItem.Visible = false;
                        daftarEventsToolStripMenuItem.Visible = false;
                        daftarKontrakToolStripMenuItem.Visible = false;
                        lihatTawaranKontrakToolStripMenuItem.Visible = false;
                        kontrakToolStripMenuItem.Visible = false;
                        daftarArtisToolStripMenuItem.Visible = false;
                        profileToolStripMenuItem.Visible = false;
                        ubahProfilToolStripMenuItem.Visible = false;
                    }
                }
                else
                {
                    //jika gagal login
                    MessageBox.Show("Gagal Login");
                    Application.Exit();
                }

            }
            catch (Exception x)
            {
                MessageBox.Show("Koneksi Gagal.Pesan kesalahan : " + x.Message);
            }

        }

        private void lihatJadwalPekerjaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarJadwalPekerjaan"];
            if (form == null)
            {
                FormDaftarJadwalPekerjaan frm = new FormDaftarJadwalPekerjaan();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void tambahJadwalPekerjaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormTambahJadwalPekerjaan"];
            if (form == null)
            {
                FormTambahJadwalPekerjaan frm = new FormTambahJadwalPekerjaan();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void registerAkunArtisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormRegisterAkunArtis"];
            if (form == null)
            {
                FormRegisterAkunArtis frm = new FormRegisterAkunArtis();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void registerAkunManajerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormRegisterManager"];
            if (form == null)
            {
                FormRegisterManager frm = new FormRegisterManager();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void daftarKaryaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarKarya"];
            if (form == null)
            {
                FormDaftarKarya frm = new FormDaftarKarya();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void daftarEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarEvent"];
            if (form == null)
            {
                FormDaftarEvent frm = new FormDaftarEvent();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void lihatTawaranKontrakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarTawaranKontrakArtis"];
            if (form == null)
            {
                FormDaftarTawaranKontrakArtis frm = new FormDaftarTawaranKontrakArtis();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void buatKontrakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormTambahKontrak"];
            if (form == null)
            {
                FormTambahKontrak frm = new FormTambahKontrak();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void daftarKontrakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarKontrakManajer"];
            if (form == null)
            {
                FormDaftarKontrakManajer frm = new FormDaftarKontrakManajer();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void ubahPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormUbahPassword"];
            if (form == null)
            {
                FormUbahPassword frm = new FormUbahPassword();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void daftarArtisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarArtis"];
            if (form == null)
            {
                FormDaftarArtis frm = new FormDaftarArtis();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(artisLogin!=null && manajerLogin==null & adminLogin==null)
            {
                Form form = Application.OpenForms["FormProfilArtis"];
                if (form == null)
                {
                    FormProfilArtis frm = new FormProfilArtis();
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    form.Show();
                    form.BringToFront();
                }
            }
            else if (artisLogin == null && manajerLogin != null & adminLogin == null)
            {
                Form form = Application.OpenForms["FormProfilManajer"];
                if (form == null)
                {
                    FormProfilManajer frm = new FormProfilManajer();
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    form.Show();
                    form.BringToFront();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void ubahProfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (artisLogin != null && manajerLogin == null & adminLogin == null)
            {
                Form form = Application.OpenForms["FormUbahProfilArtis"];
                if (form == null)
                {
                    FormUbahProfilArtis frm = new FormUbahProfilArtis();
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    form.Show();
                    form.BringToFront();
                }
            }
            else if (artisLogin == null && manajerLogin != null & adminLogin == null)
            {
                Form form = Application.OpenForms["FormUbahProfilManajer"];
                if (form == null)
                {
                    FormUbahProfilManajer frm = new FormUbahProfilManajer();
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    form.Show();
                    form.BringToFront();
                }
            }
        }
    }
}
