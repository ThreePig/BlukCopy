using System;
using System.Windows.Forms;
using System.IO;

namespace Firstweb
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            //listBox2.Items.AddRange(directory.GetDirectories());
            foreach (var x in directory.GetDirectories())
            {
                if (x.ToString() == "EBMSWeb")
                {
                    listBox1.Items.Add(x);
                    listBox1.SelectedItem = x;
                }
            }
            foreach (var x in directory.GetDirectories())
            {
                if (x.ToString() != "EBMSWeb")
                {
                    listBox2.Items.Add(x);
                    listBox2.SelectedItem = x;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0 || listBox2.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中文件！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                foreach (var l in listBox2.SelectedItems)
                {
                    Application.DoEvents();
                    DirectoryInfo di = new DirectoryInfo(l.ToString() + "\\");
                    foreach (var f in di.GetFiles())
                    {
                        if (f.Name != textBox1.Text.ToString())
                        {
                            f.Delete();
                        }
                    }
                    foreach (var d in di.GetDirectories())
                    {
                        d.Delete(true);
                    }
                    Application.DoEvents();
                    //di.Delete(true);
                    Application.DoEvents();
                    CopyFolder(listBox1.SelectedItem.ToString()+"\\", l.ToString() + "\\");
                    Application.DoEvents();
                }
                MessageBox.Show("拷贝成功！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
            }
            catch (Exception err)
            {
                MessageBox.Show(err.InnerException.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }
        }
        private void CopyFolder(string from, string to)
        {
            if (!Directory.Exists(to))
                Directory.CreateDirectory(to);

            // 子文件夹
            foreach (string sub in Directory.GetDirectories(from))
                CopyFolder(sub + "\\", to + Path.GetFileName(sub) + "\\");

            // 文件
            foreach (string file in Directory.GetFiles(from))
            {
                if (Path.GetFileName(file).ToString() != textBox1.Text.ToString())
                {
                    Application.DoEvents();
                    
                    //MessageBox.Show(Path.GetFullPath(to) + Path.GetFileName(to));
                    
                    
                    File.Copy(file, to + Path.GetFileName(file), true);
                    Application.DoEvents();
                    label4.Text = Path.GetFullPath(to)+ Path.GetFileName(file);
                    Application.DoEvents();
                    label4.Refresh();
                    Application.DoEvents();
                }
            }
        }
    }
}
