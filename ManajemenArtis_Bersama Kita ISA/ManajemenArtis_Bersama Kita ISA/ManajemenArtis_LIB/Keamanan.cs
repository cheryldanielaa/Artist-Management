using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class Keamanan
    {
        public static byte[] EncryptAES(string plainText, byte[] key, byte[] iv) ///METHOD UTK ENCRYPT MENGGUNAKAN AES
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encryptedBytes;
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
                return encryptedBytes;
            }
        }
        public static string DecryptAES(byte[] cipherText, byte[] key, byte[] iv) //METHOD UTK DECRYPT MENGGUNAKAN AES
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decryptedBytes;
                using (var msDecrypt = new System.IO.MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var msPlain = new System.IO.MemoryStream())
                        {
                            csDecrypt.CopyTo(msPlain);
                            decryptedBytes = msPlain.ToArray();
                        }
                    }
                }
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        // Fungsi untuk mengenkripsi pesan menggunakan CIPHER
        public static string EncryptCaesar(string plainText, int shift)
        {

            string cipherText = "";
            foreach (char c in plainText)
            {
                if (char.IsLetter(c))
                {
                    char shiftedChar = (char)(((int)char.ToUpper(c) - 65 + shift) % 26 + 65);
                    cipherText += char.IsLower(c) ? char.ToLower(shiftedChar) : shiftedChar;
                }
                else if (char.IsNumber(c))
                {
                    cipherText += c; // Keep numbers unchanged
                }
                else
                {
                    cipherText += c; // Keep other characters unchanged
                }
            }
            return cipherText;
        }

        // Fungsi untuk mendekripsi pesan
        public static string DecryptCaesar(string cipherText, int shift)
        {
            return EncryptCaesar(cipherText, 26 - shift); // Kembali shift 26 - shift kali
        }





        public static string EncryptNumbers(string numbers, string key)
        {
            StringBuilder encryptedNumbers = new StringBuilder();
            key = key.ToUpper(); // Normalisasi kunci ke huruf besar
            int keyIndex = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                char currentChar = numbers[i];
                if (char.IsDigit(currentChar))
                {
                    int num = int.Parse(currentChar.ToString());
                    // Sesuaikan operasi enkripsi dengan karakter angka
                    int encryptedNum = (num + (int)(key[keyIndex] - 'A')) % 10;
                    encryptedNumbers.Append(encryptedNum);
                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    // Jika karakter bukan angka, biarkan seperti apa adanya
                    encryptedNumbers.Append(currentChar);
                }
            }
            return encryptedNumbers.ToString();
        }

        public static string DecryptNumbers(string encryptedNumbers, string key)
        {
            StringBuilder decryptedNumbers = new StringBuilder();
            key = key.ToUpper(); // Normalize the key to uppercase
            int keyIndex = 0;
            for (int i = 0; i < encryptedNumbers.Length; i++)
            {
                char currentChar = encryptedNumbers[i];
                if (char.IsDigit(currentChar))
                {
                    int num = int.Parse(currentChar.ToString());
                    int keyOffset = key[keyIndex] - 'A';
                    int decryptedNum = (num - keyOffset) % 10;
                    if (decryptedNum < 0)
                    {
                        decryptedNum += 10; // Ensure the decrypted number is non-negative
                    }
                    decryptedNumbers.Append(decryptedNum);
                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    decryptedNumbers.Append(currentChar);
                }
            }
            return decryptedNumbers.ToString();
        }
    }
}
