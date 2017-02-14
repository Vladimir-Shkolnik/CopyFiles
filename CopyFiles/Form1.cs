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
using System.Diagnostics;

namespace CopyFiles
{
    public partial class Form1 : Form
    {
        List<string> _items = new List<string>();
        string selectfile = "";


        public Form1()
        {
            InitializeComponent();
            //https://msdn.microsoft.com/ru-ru/library/system.windows.forms.progressbar(v=vs.110).aspx
            //http://csharp.net-informations.com/gui/cs-radiobutton.htm

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //http://professorweb.ru/my/csharp/thread_and_files/level3/3_6.php
            //https://msdn.microsoft.com/ru-ru/library/system.io.file.delete(v=vs.110).aspx
            //http://stackoverflow.com/questions/771564/how-to-create-a-sub-directory-inside-a-directory

        }

        string sourceDir = AppDomain.CurrentDomain.BaseDirectory;
        private void button1_Click(object sender, EventArgs e)
        {
            if (selectfile != "" && selectfile != null)
            {
                string[] picList;
                string backupDir = sourceDir + "output";
                string errorFile = "Error.txt";
                string errorFilePatch = Path.Combine(backupDir, errorFile);

                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                try
                {
                    SearchOption searchOption;
                    bool overwrite;
                    if (checkBox1.Checked == true)
                    {
                        searchOption = SearchOption.AllDirectories;
                    }
                    else
                    {
                        searchOption = SearchOption.TopDirectoryOnly;
                    }
                    if (checkBox2.Checked == true)
                    {
                        overwrite = true;
                    }
                    else
                    {
                        overwrite = false;
                    }

                    if (radioButton1.Checked == true)
                    {
                        picList = Directory.GetFiles(sourceDir, "*.jpg", searchOption);
                    }
                    else if (radioButton2.Checked == true)
                    {
                        picList = Directory.GetFiles(sourceDir, "*.txt", searchOption);
                    }
                    else
                    {
                        picList = Directory.GetFiles(sourceDir, "*.pdf", searchOption);
                    }
                    List<string> notfound = new List<string>();
                    int countError = 0;
                    int countSuccess = 0;
                    string val = "";
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = _items.Count;
                    progressBar1.Value = 1;
                    progressBar1.Step = 1;
                    var watch = Stopwatch.StartNew();
                    foreach (var item in _items)
                    {
                        val = progressBar1.Value.ToString();
                        progressBar1.PerformStep();
                        val = progressBar1.Value.ToString();

                        //CopyWithProgress(countSuccess);
                        //CopyWithProgress(picList);
                        //CopyWithProgress(picList.Length);
                        string name = sourceDir + item;
                        if (picList.Contains(name))
                        {
                            try
                            {
                                File.Copy(Path.Combine(sourceDir, item), Path.Combine(backupDir, item), overwrite);
                                countSuccess += 1;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            countError += 1;
                            notfound.Add(item);                            
                            string data = item + Environment.NewLine;
                            File.AppendAllText(errorFile, data);
                        }
                    }
                    string[] copiedList = Directory.GetFiles(backupDir);
                    if (checkBox3.Checked == true)
                    {
                        foreach (string f in copiedList)
                        {
                            string name = sourceDir + Path.GetFileName(f); ;
                            File.Delete(name);
                        }
                    }
                    else
                    {

                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    TimeSpan ts = watch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

                    MessageBox.Show("Elapsed time" + elapsedTime + Environment.NewLine +
                        "Copied successfully: " + countSuccess + Environment.NewLine +
                        "Not found - " + countError + " files: " + Environment.NewLine +
                        string.Join(Environment.NewLine, notfound.ToArray()), "Error");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pleasure select file");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectfile != "" && selectfile != null)
            {
                string[] picList;
                string backupDir = sourceDir + "output";
                string errorFile = "Error.txt";
                string errorFilePatch = Path.Combine(backupDir, errorFile);

                if (!Directory.Exists(backupDir))
                {
                    Directory.CreateDirectory(backupDir);
                }

                try
                {
                    SearchOption searchOption;
                    bool overwrite;
                    if (checkBox1.Checked == true)
                    {
                        searchOption = SearchOption.AllDirectories;
                    }
                    else
                    {
                        searchOption = SearchOption.TopDirectoryOnly;
                    }
                    if (checkBox2.Checked == true)
                    {
                        overwrite = true;
                    }
                    else
                    {
                        overwrite = false;
                    }

                    picList = Directory.GetFiles(sourceDir);

                       
                    List<string> notfound = new List<string>();
                    int countError = 0;
                    int countSuccess = 0;
                    string val = "";
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 1;
                    progressBar1.Maximum = _items.Count;
                    progressBar1.Value = 1;
                    progressBar1.Step = 1;
                    var watch = Stopwatch.StartNew();
                    foreach (var item in _items)
                    {
                        val = progressBar1.Value.ToString();
                        progressBar1.PerformStep();
                        val = progressBar1.Value.ToString();

                        string name = sourceDir + item;
                        if (picList.Contains(name))
                        {
                            try
                            {
                                File.Copy(Path.Combine(sourceDir, item), Path.Combine(backupDir, item), overwrite);
                                countSuccess += 1;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            countError += 1;
                            notfound.Add(item);
                            string data = item + Environment.NewLine;
                            File.AppendAllText(errorFile, data);
                        }
                    }
                    string[] copiedList = Directory.GetFiles(backupDir);
                    if (checkBox3.Checked == true)
                    {
                        foreach (string f in copiedList)
                        {
                            string name = sourceDir + Path.GetFileName(f); ;
                            File.Delete(name);
                        }
                    }
                    else
                    {

                    }
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;

                    TimeSpan ts = watch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

                    MessageBox.Show("Elapsed time" + elapsedTime + Environment.NewLine +
                        "Copied successfully: " + countSuccess + Environment.NewLine +
                        "Not found - " + countError + " files: " + Environment.NewLine +
                        string.Join(Environment.NewLine, notfound.ToArray()), "Error");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Pleasure select file");
            }
        }

        private static void ReadTXTFile()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("TestFile.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show("The file could not be read:");
                MessageBox.Show(eee.Message);
            }
        }

        private void CopyWithProgress(string[] filenames)
        {

            // Display the ProgressBar control.
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.
            progressBar1.Maximum = filenames.Length;
            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;

            // Loop through all files to copy.
            //for (int x = 1; x <= filenames.Length; x++)
            // {
            // Copy the file and increment the ProgressBar if successful.
            //if (CopyFile(filenames[x - 1]) == true)
            //{
            //    // Perform the increment on the ProgressBar.
            //    progressBar1.PerformStep();
            //}
            //  }
            progressBar1.PerformStep();
        }

        private void CopyWithProgress(int count)
        {
            string val;
            progressBar1.Visible = true;
            // Set Minimum to 1 to represent the first file being copied.
            progressBar1.Minimum = 1;
            // Set Maximum to the total number of files to copy.

            //progressBar1.Maximum = filenames.Length;
            progressBar1.Maximum = count;

            // Set the initial value of the ProgressBar.
            progressBar1.Value = 1;
            // Set the Step property to a value of 1 to represent each file being copied.
            progressBar1.Step = 1;
            progressBar1.PerformStep();

            //val = progressBar1.Value.ToString();
            //progressBar1.Increment(+1);
            //val = progressBar1.Value.ToString();
        }

        private void open_File_Click(object sender, EventArgs e)
        {
            _items.Clear();

            openFileDialog1.Title = "Выберите файл";
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = openFileDialog1.ShowDialog();
            selectfile = Path.GetFileName(openFileDialog1.FileName);
            label2.Text = selectfile;
            string[] massiveOfString;

            if (result == DialogResult.OK && selectfile != "")
            {
                try
                {
                    massiveOfString = System.IO.File.ReadAllLines(selectfile);
                    foreach (var line in massiveOfString)
                    {
                        _items.Add(line);
                    }
                }
                catch (Exception eee)
                {
                    MessageBox.Show("The file could not be read:");
                    MessageBox.Show(eee.Message);
                }

                listBox1.Items.Clear();

                foreach (var item in _items)
                {
                    listBox1.Items.Add(item);
                }
            }
            else
            {
                // listBox1.Refresh();
            }



        }


    }
}
