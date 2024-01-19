using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS2Module.ControlObjectCustom;
using HIS2Module.UtilExtension;

namespace IC20Analyze
{
    public partial class Main : Form
    {

        AnalyticsTxt _anaTxt = new AnalyticsTxt();

        string _FilePath { get; set; }


        public Main()
        {
            InitializeComponent();




        }


        private void Main_Load(object sender, EventArgs e)
        {
            txtPath.Text = @"D:\aa_專案\17_小專案\e.IC2.0上傳\Check";
            GetFiles();

            this.pSetAutoReSize(true, true);     //一定要在最後
        }



        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            SetAnalyzeText();
        }


        private void btnFileReload_Click(object sender, EventArgs e)
        {
            // 重新載入
            GetFiles();

        }


        //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--**-*-*-
        //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--**-*-*-
        //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*--**-*-*-


        void SetAnalyzeText()
        {

            btnAnalyze.Enabled = false;


            dataGridView1.Visible = false;
            richTextBox1.Visible = false;
            richTextBox1.Clear();

            string str = txtOrign.Text.pSQLValidator();

            if (chkProSearch.Checked == true)
            {
                dataGridView1.Visible = true;

                _anaTxt.Get總排行數量(str);
                DataTable dt總排行數量 = _anaTxt._dt總排行數量;

                dataGridView1.DataSource = dt總排行數量;
            }
            else
            {
                richTextBox1.Visible = true;
                str = str.Replace("錯誤原因:", "");
                str = str.Replace(" ", "");
                str = str.Replace("\r\n", "");

                string[] arrContent = str.pSplit(";");

                string OutPut = string.Empty;
                richTextBox1.Clear();
                foreach (var i in arrContent)
                {
                    Run解析(i);
                }
            }

            btnAnalyze.Enabled = true;
        }


        private void Run解析(string str)
        {
            bool IsMB2 = str.Contains("[");
            bool IsOKData = str.Contains(":");

            if (str.pEmpty())
            {
                return;
            }

            if (IsOKData == false)
            {
                //return $"資料有問題\n{str}\n";
                richTextBox1.SelectionColor = Color.Red;
                richTextBox1.AppendText($"資料有問題\r\n{str}");
            }

            string strField = str.pSplit(":")[0];

            bool isOK = _anaTxt._dicFieldCH.ContainsKey(strField.pLeft(3));

            if (isOK == false)
            {
                return;
            }


            string strCode = str.pSplit(":")[1];
            string strResult = string.Empty;
            string strFieldCH = string.Empty;
            string strCodeCH = string.Empty;

            //OpdBasicIC
            if (IsMB2 == false)
            {
                bool haveFieldKey = _anaTxt._dicFieldCH.ContainsKey(strField);
                if (haveFieldKey)
                {
                    strFieldCH = _anaTxt._dicFieldCH[strField];
                }
                else
                {
                    strFieldCH = $"查無此欄位Key:[{strField}]";
                }

            }
            else
            {
                string tmp = strField.pLeft(3);

                bool haveFieldKey = _anaTxt._dicFieldCH.ContainsKey(tmp);
                if (haveFieldKey)
                {
                    strFieldCH = _anaTxt._dicFieldCH[tmp];
                }
                else
                {
                    strFieldCH = $"查無此欄位Key:[{strField}]";
                }
            }

            bool haveCodeKey = _anaTxt._dicErrCodeCH.ContainsKey(strCode);
            if (haveCodeKey == false)
            {
                strCodeCH = $"查無此ErrCode Key:[{strCode}]";
            }
            else
            {
                strCodeCH = _anaTxt._dicErrCodeCH[strCode];
            }

            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.AppendText($"{strField} - {strFieldCH}\r\n");

            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText($"{strCode} - {strCodeCH}\r\n\r\n");
        }


        //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-


        Dictionary<string, string> _dicDoc = new Dictionary<string, string>();


        /// <summary>
        /// 讀取zip
        /// </summary>
        void GetFiles()
        {
            _FilePath = txtPath.Text.pNullOrTrimTwo();
            _dicDoc = LoadZipFiles(_FilePath);

            var docTitle = _dicDoc.Select(o => o.Key);
            cmbXMLFile.DataSource = docTitle.ToList();


            // 打印读取的文件内容
            foreach (var kvp in _dicDoc)
            {
                Console.WriteLine($"File Name: {kvp.Key}\nContent:\n{kvp.Value}\n");
            }
        }

        Dictionary<string, string> LoadZipFiles(string folderPath)
        {
            Dictionary<string, string> disZip = new Dictionary<string, string>();

            // 获取指定文件夹中的所有 ZIP 文件
            string[] zipFiles = Directory.GetFiles(folderPath, "*.zip");

            foreach (string zipFilePath in zipFiles)
            {
                // 打开 ZIP 文件
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    // 遍历 ZIP 文件中的每个条目
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // 读取文件内容，并指定正确的编码
                        string fileContent = ReadFileContent(entry);

                        // 将文件名和内容存入字典
                        disZip[entry.FullName] = fileContent;
                    }
                }
            }

            return disZip;
        }

        string ReadFileContent(ZipArchiveEntry entry)
        {
            using (Stream entryStream = entry.Open())
            using (StreamReader reader = new StreamReader(entryStream, DetectFileEncoding(entryStream)))
            {
                return reader.ReadToEnd();
            }
        }

        Encoding DetectFileEncoding(Stream stream)
        {
            // 在这里你可以使用更复杂的算法来检测文件编码
            // 这里简单地使用 UTF-8 编码，你可能需要根据实际情况进行调整
            return Encoding.GetEncoding("big5");
        }

        private void cmbXMLFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrign.Text = _dicDoc[cmbXMLFile.Text];
            SetAnalyzeText();
        }
    }
}
