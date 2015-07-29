using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Firstweb
{
    public partial class Form1 : Form
    {
        //关屏
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        //禁止鼠标键盘动作
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool BlockInput([In, MarshalAs(UnmanagedType.Bool)] bool fBlockIt);
        //锁屏
        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            listBox3.Items.AddRange(directory.GetDirectories());
            listBox1.Items.AddRange(directory.GetDirectories());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LockWorkStation();
            SendMessage(this.Handle, (uint)0x0112, (IntPtr)0xF170, (IntPtr)2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox6.Items.Count == 0)
            {
                MessageBox.Show("未选中文件！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Text = "NO";
                return;
            }
            try
            {
                foreach (var l in listBox1.Items)
                {
                    copy(listBox2.Items[0].ToString(), l.ToString());
                }
                MessageBox.Show("拷贝成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button1.Text = "OK";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.InnerException.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.Text = "Fail";
            }
            
        }
        private void copy(string pathA,string pathB)
        {
            DirectoryInfo directory = new DirectoryInfo(pathA);
            FileInfo[] files = directory.GetFiles();
            foreach (var file in files)
            {
                if (listBox6.Items.Contains(Path.GetFileName(file.ToString())))
                {
                File.Delete(pathB + "\\" + Path.GetFileName(file.ToString()));
                File.Copy(pathA +"\\" + Path.GetFileName(file.ToString()), pathB+"\\" + Path.GetFileName(file.ToString()));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox5.Items.Clear();
            foreach (var x in listBox3.SelectedItems)
            {
                listBox2.Items.Add(x);
            }
            listBox3.Items.Clear();
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            listBox3.Items.AddRange(directory.GetDirectories());
            foreach (var x in listBox2.Items)
            {
                listBox3.Items.Remove(x);
                DirectoryInfo dir = new DirectoryInfo(x.ToString());
                FileInfo[] files = dir.GetFiles();
                foreach (var file in files)
                {
                    listBox5.Items.Add(Path.GetFileName(file.ToString()));
                }
            }
            for (int i=0;i<listBox1.Items.Count;i++)
            {
                if (listBox2.Items.Count>0 && listBox1.Items[i].ToString() == listBox2.Items[0].ToString())
                {
                    listBox1.Items.RemoveAt(i);
                    listBox4.Items.Add(listBox2.Items[0]);
                }
            }
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                if (listBox2.Items.Count > 0 && listBox3.Items[i].ToString() == listBox2.Items[0].ToString())
                {
                    listBox3.Items.RemoveAt(i);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.AddRange(listBox3.Items);
            listBox3.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var x in listBox2.SelectedItems)
            {
                listBox3.Items.Add(x);
            }
            foreach (var x in listBox3.Items)
            {
                listBox2.Items.Remove(x);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox3.Items.AddRange(listBox2.Items);
            listBox2.Items.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (var x in listBox4.SelectedItems)
            {
                listBox1.Items.Add(x);
            }
            foreach (var x in listBox1.Items)
            {
                listBox4.Items.Remove(x);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(listBox4.Items);
            listBox4.Items.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (var x in listBox1.SelectedItems)
            {
                listBox4.Items.Add(x);
            }
            foreach (var x in listBox4.Items)
            {
                listBox1.Items.Remove(x);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listBox4.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (var x in listBox5.SelectedItems)
            {
                listBox6.Items.Add(x);
            }
            foreach (var x in listBox6.Items)
            {
                listBox5.Items.Remove(x);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox6.Items.AddRange(listBox5.Items);
            listBox5.Items.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (var x in listBox6.SelectedItems)
            {
                listBox5.Items.Add(x);
            }
            foreach (var x in listBox5.Items)
            {
                listBox6.Items.Remove(x);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            listBox5.Items.AddRange(listBox6.Items);
            listBox6.Items.Clear();
        }
    }
}
