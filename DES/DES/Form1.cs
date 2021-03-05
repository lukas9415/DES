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
            var random = new Random();
            byte[] IV = new byte[8];
            random.NextBytes(IV);
            Encoding encoding = Encoding.GetEncoding("437");
            textBox3.Text = encoding.GetString(IV);
            IV = encoding.GetBytes(textBox3.Text);
        }

        public static string Encrypt(string text, string key, byte[] IV)
        {
            if (key.Length == 8)
            { //test
                // Encode message and password
                byte[] messageBytes = ASCIIEncoding.ASCII.GetBytes(text);
                byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(key);
/*                var random = new Random();
                byte[] IV = new byte[8];
                random.NextBytes(IV);*/

                // Set encryption settings -- Use password for both key and init. vector
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                ICryptoTransform transform = provider.CreateEncryptor(passwordBytes, IV);
                CryptoStreamMode mode = CryptoStreamMode.Write;

                // Set up streams and encrypt
                MemoryStream memStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
                cryptoStream.Write(messageBytes, 0, messageBytes.Length);
                cryptoStream.FlushFinalBlock();

                // Read the encrypted message from the memory stream
                byte[] encryptedMessageBytes = new byte[memStream.Length];
                memStream.Position = 0;
                memStream.Read(encryptedMessageBytes, 0, encryptedMessageBytes.Length);

                // Encode the encrypted message as base64 string
                string encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);

                return encryptedMessage;
            }
            else throw new Exception("Key must be 8 symbols lenght ! (8 Bits)");
        }

        public static string Decrypt(string encryptedMessage, string key, byte[] IV)
        {
            if (key.Length == 8)
            {
                // Convert encrypted message and password to bytes
                byte[] encryptedMessageBytes = Convert.FromBase64String(encryptedMessage);
                byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(key);
/*                var random = new Random();
                byte[] IV = new byte[8];
                random.NextBytes(IV);*/


            // Set encryption settings -- Use password for both key and init. vector
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateDecryptor(passwordBytes, IV);
            CryptoStreamMode mode = CryptoStreamMode.Write;

            // Set up streams and decrypt
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
            cryptoStream.Write(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Read decrypted message from memory stream
            byte[] decryptedMessageBytes = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(decryptedMessageBytes, 0, decryptedMessageBytes.Length);

            // Encode deencrypted binary data to base64 string
            string decryptedMessage = ASCIIEncoding.ASCII.GetString(decryptedMessageBytes);
                return decryptedMessage;
        }
            else throw new Exception("Key must be 8 symbols lenght ! (8 Bits)");
    }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string key = textBox2.Text;
            Encoding encoding = Encoding.GetEncoding("437");
            byte[] IV = encoding.GetBytes(textBox3.Text);

            try
            {
                string encryptedString = Encrypt(text, key, IV);
                resultTextBox.Text = encryptedString;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            resultTextBox.Clear();
            string text = textBox1.Text;
            string key = textBox2.Text;
            Encoding encoding = Encoding.GetEncoding("437");
            byte[] IV = encoding.GetBytes(textBox3.Text);
            try
            {
                string decryptedString = Decrypt(text, key, IV);
                resultTextBox.Text = decryptedString;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var random = new Random();
            byte[] IV = new byte[8];
            random.NextBytes(IV);
            Encoding encoding = Encoding.GetEncoding("437");
            textBox3.Text = encoding.GetString(IV);
            IV = encoding.GetBytes(textBox3.Text);
        }
    }
}
