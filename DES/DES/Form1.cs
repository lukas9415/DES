using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public enum Mode
        {
            ENCRYPT,
            DECRYPT
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            button2.Visible = true;

            button3.Visible = false;
            button1.Visible = false;
            label3.Visible = false;
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            button2.Visible = false;

            button3.Visible = true;
            button1.Visible = true;
            label3.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            var random = new Random();
            byte[] IV = new byte[8];
            random.NextBytes(IV);
            byte[] key = new byte[8];
            byte[] encrypted = DESCrypto(Mode.ENCRYPT, IV, key, Encoding.UTF8.GetBytes(text));

            resultTextBox.Text = BitConverter.ToString(encrypted).Replace("-", "");
        }

        static byte[] DESCrypto(Mode mode, byte[] IV, byte[] key, byte[] text)
        {
            using(var DES = new DESCryptoServiceProvider())
            {
                DES.IV = IV;
                DES.Key = key;
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;

                using(var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = null;

                    if (mode == Mode.ENCRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                    else if (mode == Mode.DECRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);

                    if (cryptoStream == null)
                        return null;

                    cryptoStream.Write(text, 0, text.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();
                }
            }
            return null;

        }

    }
}
