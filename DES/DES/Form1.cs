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

        }

        public static string Encrypt(string text, string key)
        {
            // Encode message and password
            byte[] messageBytes = ASCIIEncoding.ASCII.GetBytes(text);
            byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(key);

            // Set encryption settings -- Use password for both key and init. vector
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateEncryptor(passwordBytes, passwordBytes);
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

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            string encryptedString;

            string key = textBox2.Text;

            encryptedString = Encrypt(text, key);
            resultTextBox.Text = encryptedString;
        }

    }
}
