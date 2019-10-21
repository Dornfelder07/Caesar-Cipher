using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.IO;

namespace caesar_shift
{
    public partial class Form1 : Form
    {
        string[] chars;
        List<string> alphabet;
        string toEncode = "";
        string toDecode = "";

        public Form1()
        {
            chars = new string[]{ "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
             "v", "w", "x", "y", "z", " ", ".", ",", "-", ":"};
            alphabet = chars.ToList();

            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public string encode(List<string> alphabet, string text, int key)
        {
            string encoded = "";
            toEncode = text;
            int index;

            foreach (char c in toEncode)
            {
                index = alphabet.FindIndex(a => a.Contains(c));
                encoded += alphabet[(index * key) % alphabet.Capacity];
            }

            return encoded;
        }

        public string decode(List<string> alphabet, string text, int key)
        {
            string decoded = "";
            toDecode = text;
            int index;
            BigInteger kReversed = power(key, phi(alphabet.Capacity) - 1) % alphabet.Capacity;

            foreach (char c in toDecode)
            {
                index = alphabet.FindIndex(a => a.Contains(c));
                decoded += alphabet[(index * (int)kReversed) % alphabet.Capacity];
            }

            return decoded;
        }

        //RAISING x TO THE POWER OF n
        public static BigInteger power(BigInteger x, BigInteger n)
        {
            BigInteger a;

            if (n == 0)
            {
                return 1;
            }
            else if (n % 2 == 1)
            {
                return x * power(x, n - 1);
            }
            else
            {
                a = power(x, n / 2);
            }
            return a * a;
        }

        //SAVE TO FILE ENCODED TEXT
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox3.Text;
            System.IO.File.WriteAllText(@"C:\Users\macie\Desktop\encoded.txt", text);
        }

        //SAVE TO FILE DECODED TEXT
        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox4.Text;
            System.IO.File.WriteAllText(@"C:\Users\macie\Desktop\decoded.txt", text);
        }

        // ===============EULER=================
        static int gcd(int a, int b)
        {
            if (a == 0)
                return b;
            return gcd(b % a, a);
        }

        static int phi(int n)
        {
            int result = 1;
            for (int i = 2; i < n; i++)
            {
                if (gcd(i, n) == 1)
                {
                    result++;
                }
            }

            return result;
        }
        // ===============END=================

        // TRIGGER ENCODE FUNCTION
        private void button5_Click(object sender, EventArgs e)
        {
            string encoded;
            int key;
            toEncode = textBox1.Text.ToLower();
            textBox1.Text = toEncode;
            key = (int)numericUpDown1.Value;

            encoded = encode(alphabet, toEncode, key);

            textBox3.Text = encoded;
        }

        // TRIGGER DECODE FUNCTION
        private void button6_Click(object sender, EventArgs e)
        {
            string decoded;
            int key;
            toDecode = textBox2.Text.ToLower();
            textBox2.Text = toDecode;
            key = (int)numericUpDown2.Value;

            decoded = decode(alphabet, toDecode, key);

            textBox4.Text = decoded;
        }

        // UPLOAD TEXT TO ENCODE FROM FILE
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            string path;
            string text = "";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                path = OFD.FileName;

                var fileStream = OFD.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    text = reader.ReadToEnd();
                }

                textBox1.Text = text;

                string encoded;
                int key;
                toEncode = textBox1.Text.ToLower();
                textBox1.Text = toEncode;
                key = (int)numericUpDown1.Value;

                encoded = encode(alphabet, toEncode, key);

                textBox3.Text = encoded;

            }
        }

        // UPLOAD TEXT TO DECODE FROM FILE
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            string path;
            string text = "";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                path = OFD.FileName;

                var fileStream = OFD.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    text = reader.ReadToEnd();

                }
                textBox2.Text = text;

                string decoded;
                int key;
                toDecode = textBox2.Text.ToLower();
                textBox2.Text = toDecode;
                key = (int)numericUpDown2.Value;

                decoded = decode(alphabet, toDecode, key);

                textBox4.Text = decoded;
            }
        }
    }
}
