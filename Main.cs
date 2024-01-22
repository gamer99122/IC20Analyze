using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS2Module.ControlObjectCustom;
using HIS2Module.UtilExtension;
using IC20Analyze.Class;

namespace IC20Analyze
{
    public partial class Main : Form
    {

        AnalyticsTxt _anaTxt = new AnalyticsTxt();

        csBasicSetting _basSetting = new csBasicSetting();

        //string _FilePath { get; set; }


        public Main()
        {
            InitializeComponent();


            chkProSearch.Checked = true;

        }


        private void Main_Load(object sender, EventArgs e)
        {
            txtPath.Text = _basSetting.GetFilePath;
            GetFiles();

            //this.pSetAutoReSize(true);     //一定要在最後
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


        private void cmbXMLFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrign.Text = _dicDoc[cmbXMLFile.Text];
            if (txtOrign.Text.Length <= 0) return;

            SetAnalyzeText();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            _basSetting.GetFilePath = txtPath.Text.pNullOrTrimTwo();
            OpenFolder(_basSetting.GetFilePath);
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


            dataGridView1.Visible = true;
            dataGridView2.Visible = false;

            _anaTxt.Get全文解析(str);

            label8.Text = _anaTxt._str上傳日期_時間;
            label5.Text = _anaTxt._str檔案大小;
            label7.Text = _anaTxt._str實際接收筆數;
            label10.Text = _anaTxt._str有效明細;
            label12.Text = _anaTxt._str有效醫令;
            label14.Text = _anaTxt._str無效明細;


            DataTable dt = _anaTxt._dt錯誤訊息明細;

            bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            dataGridView1.DataSource = dt;
            dataGridView1.pColorRow();


            //if (chkProSearch.Checked == true)
            //{
            //    dataGridView1.Visible = true;

            //    _anaTxt.Get總排行數量(str);
            //    DataTable dt總排行數量 = _anaTxt._dt總排行數量;

            //    dataGridView1.DataSource = dt總排行數量;
            //}
            //else
            //{
            //    richTextBox1.Visible = true;
            //    str = str.Replace("錯誤原因:", "");
            //    str = str.Replace(" ", "");
            //    str = str.Replace("\r\n", "");

            //    string[] arrContent = str.pSplit(";");

            //    string OutPut = string.Empty;
            //    richTextBox1.Clear();
            //    foreach (var i in arrContent)
            //    {
            //        if (i.Length <= 0)
            //        {
            //            continue;
            //        }
            //        Run解析(i);
            //    }
            //}

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
            _basSetting.GetFilePath = txtPath.Text.pNullOrTrimTwo();
            _dicDoc = LoadZipFiles(_basSetting.GetFilePath);

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

        /// <summary>
        /// 開啟路行資料夾
        /// </summary>
        /// <param name="folderPath"></param>
        void OpenFolder(string folderPath)
        {
            // 使用 Explorer 打开文件夹
            Process.Start("explorer.exe", folderPath);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchWord();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string msg = string.Empty;
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string column1Value = selectedRow.Cells["原始內容"].Value.ToString();
                msg += column1Value;

                string column錯誤原因 = selectedRow.Cells["錯誤原因"].Value.ToString();
                msg += "\r\n\r\n=====================================================================\r\n";
                msg += _anaTxt.Run解析(column錯誤原因);

                msg += "\r\n\r\n=====================================================================\r\n";
                string strM15 = selectedRow.Cells["就醫識別碼"].Value.ToString();
                msg += _anaTxt.GetSQL(strM15);
                msg += "\r\n\r\n";

                txtOrign.Text = msg;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAndFocus();
                //SearchWord();
            }
        }

        public void SearchWord()
        {
            string KeyWord = txtSearch.Text.pSQLValidator();

            if (KeyWord.Length < 5)
            {
                return;
            }

            DataTable dt = (DataTable)dataGridView1.DataSource;

            if (dt.pEmpty())
            {
                return;
            }

            dataGridView1.pSearch(KeyWord);
        }

        private int currentRowIndex = -1;
        private BindingSource bindingSource;

        private void SearchAndFocus()
        {
            string KeyWord = txtSearch.Text.pSQLValidator();

            if (KeyWord.Length < 2)
            {
                return;
            }

            DataTable dt = (DataTable)dataGridView1.DataSource;
            var query = from row in dt.AsEnumerable()
                        where row.ItemArray.Any(field => field.ToString().Contains(KeyWord))
                        select row;

            if (query.Any())
            {
                DataRow[] matchingRows = query.ToArray();
                if (currentRowIndex == -1 || currentRowIndex == matchingRows.Length - 1)
                {
                    currentRowIndex = 0;
                }
                else
                {
                    currentRowIndex++;
                }
                if (currentRowIndex > matchingRows.Length)
                {
                    currentRowIndex = 0;
                }

                DataRow selectedRow = matchingRows[currentRowIndex];
                int rowIndex = dt.Rows.IndexOf(selectedRow);
                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;



            try
            {
                SetAnalyzeText();
                string str = _dicDoc[cmbXMLFile.Text];
                _anaTxt.Get總排行數量(str);
                DataTable dt = _anaTxt._dt總排行數量;

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                $"發生錯誤:{ex.ToString()}".pShow();
                button1.Enabled = true;
            }

            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
            txtOrign.Text = _dicDoc[cmbXMLFile.Text];
            button1.Enabled = true;
        }
    }
}
