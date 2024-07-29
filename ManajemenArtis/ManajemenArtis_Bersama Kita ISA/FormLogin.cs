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
    public partial class FormLogin : Form
    {
        List<Artis> listArtis = new List<Artis>();
        List<Manajer> listManajer = new List<Manajer>();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            listArtis = Artis.BacaData("", "");
            listManajer = Manajer.BacaData("", "");
        }


        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi k = new Koneksi();
                if (comboBoxUserType.Text == "Artis")
                {
                    Artis a = Artis.AmbilDataLogin(txtUsername.Text);
                    if (!(a == null))
                    {
                        #region decrypted-password
                        string concat = a.Password;
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

                        string decryptedPassword = Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);
                        #endregion

                        if (decryptedPassword == txtPassword.Text)
                        {
                            FormUtama frmUtama = (FormUtama)this.Owner;
                            frmUtama.artisLogin = a;
                            MessageBox.Show("Welcome " + a.Nama + " to ISA Town");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Maaf, password yang Anda masukkan salah!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Akun dengan username : " + txtUsername.Text + " belum terdaftar!");
                    }
                }
                else if (comboBoxUserType.Text == "Manajer") 
                {
                    //baca data apakah ada username dengan nama yang diisikan
                    Manajer mLogin = Manajer.AmbilDataLogin(txtUsername.Text);
                    if (!(mLogin == null))
                    {
                        #region decrypted-password
                        string concat = mLogin.Password;
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
                        byte[] ivDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[0], 23));
                        byte[] secKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[1],17));
                        byte[] decryptText = Convert.FromBase64String(result[2]);
                        byte[] firstKeyDecrypt = Convert.FromBase64String(Keamanan.DecryptCaesar(result[3],10));
                        byte[] resKeyDecrypt = firstKeyDecrypt.Concat(secKeyDecrypt).ToArray();

                        string decryptedPassword=Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);
                        #endregion
                        if (decryptedPassword == txtPassword.Text) //BERHASIL LOGIN
                        {
                            FormUtama frmUtama = (FormUtama)this.Owner;
                            frmUtama.manajerLogin = mLogin;
                            MessageBox.Show("Welcome " + mLogin.Nama + " to ISA Town");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Maaf, password yang Anda masukkan salah!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Akun dengan username : " + txtUsername.Text + " belum terdaftar!");
                    }
                    
                }
                else if (comboBoxUserType.Text == "Admin")
                {
                    //baca data apakah ada username dengan nama yang diisikan
                    Admin aLogin = Admin.CekLogin(txtUsername.Text);
                    if (!(aLogin == null))
                    {
                        #region decrypted-password
                        string concat = aLogin.Password;
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

                        string decryptedPassword = Keamanan.DecryptAES(decryptText, resKeyDecrypt, ivDecrypt);
                        #endregion
                        if (decryptedPassword == txtPassword.Text) //BERHASIL LOGIN
                        {
                            FormUtama frmUtama = (FormUtama)this.Owner;
                            frmUtama.adminLogin = aLogin;
                            MessageBox.Show("Welcome " + aLogin.Username + " to ISA Town");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Maaf, password yang Anda masukkan salah!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Akun dengan username : " + txtUsername.Text + " belum terdaftar!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
