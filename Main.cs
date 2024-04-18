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
using Microsoft.VisualBasic.FileIO;

namespace IC20Analyze
{
    public partial class Main : Form
    {
        AnalyticsTxt _anaTxt = new AnalyticsTxt();
        csBasicSetting _basSetting = new csBasicSetting();
        public bool _IsCreateSQL = false;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtPath.Text = _basSetting.GetFilePath;
            GetFiles();
            GetCreateSQL();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            GetFiles();
            SetAnalyzeText();
        }

        private void btnFileReload_Click(object sender, EventArgs e)
        {
            if ("是否刪除??".pAsk() == true)
            {
                CleanUpFiles();
            }

            // 重新載入
            GetFiles();
        }

        private void cmbXMLFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrign.Text = _dicDoc[cmbXMLFile.Text];
            if (txtOrign.Text.Length <= 0)
            {
                return;
            }
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

            bool IsExclude = chkExcludeM16.Checked;

            //缺少M16
            List<string> m16Errors = new List<string>();
            m16Errors.Add("M16:AA;");
            m16Errors.Add("M16:AA;M33:D024;");
            m16Errors.Add("M16:D051;M34:D024;");


            //雙軌期間，如無合併其他錯誤，本筆仍會收載
            List<string> m16OKOK = new List<string>();
            m16OKOK.Add("M16:D051;");
            m16OKOK.Add("M16:D051;M33:D024;");

            int iEmptyM16 = 0;
            int iAnotherM16 = 0;

            var rowsToRemove = dt.AsEnumerable()
                                .Where(row => m16Errors.Contains(row.Field<string>("錯誤原因")))
                                .ToList();
            foreach (var rowToRemove in rowsToRemove)
            {
                iEmptyM16 += 1;

                if (IsExclude)
                {
                    dt.Rows.Remove(rowToRemove);
                }
            }


            var rowsToRemove1 = dt.AsEnumerable()
                                .Where(row => m16OKOK.Contains(row.Field<string>("錯誤原因")))
                                .ToList();
            foreach (var rowToRemove1 in rowsToRemove1)
            {
                iAnotherM16 += 1;

                if (IsExclude)
                {
                    dt.Rows.Remove(rowToRemove1);
                }
            }

            string strTmp有效明細 = _anaTxt._str有效明細.pReplace("[", "", StringComparison.OrdinalIgnoreCase).pReplace("]", "", StringComparison.OrdinalIgnoreCase);
            int int有效明細 = strTmp有效明細.pToInt();
            int New有效明細 = iEmptyM16 + int有效明細 - iAnotherM16;
            label18.Text = $"[{iEmptyM16}] + [{strTmp有效明細.pNullOrTrim()} - {iAnotherM16}] = [{New有效明細}]";

            bindingSource = new BindingSource();
            bindingSource.DataSource = dt;
            dataGridView1.DataSource = dt;
            dataGridView1.pColorRow();

            btnAnalyze.Enabled = true;
        }

        //*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-


        static Dictionary<string, string> _dicDoc = new Dictionary<string, string>();


        /// <summary>
        /// 讀取zip
        /// </summary>
        void GetFiles()
        {
            _basSetting.GetFilePath = txtPath.Text.pNullOrTrimTwo();
            _dicDoc = LoadZipFiles(_basSetting.GetFilePath);

            var docTitle = _dicDoc.Select(o => o.Key).OrderByDescending(o => o);
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
                        disZip[entry.FullName.pSplit(".")[0]] = fileContent;
                    }
                }
                Application.DoEvents();
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
            //SearchWord();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAndFocus();
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

            if (KeyWord.Length < 3)
            {
                return;
            }

            DataTable dt = (DataTable)dataGridView1.DataSource;

            var query = from row in dt.AsEnumerable()
                        where row.Table.Columns.Cast<DataColumn>()
                            .Where(col => col.ColumnName != "原始內容")
                            .Any(col => row[col].ToString().Contains(KeyWord))
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

        private void btnErrRanking_Click(object sender, EventArgs e)
        {
            btnErrRanking.Enabled = false;

            try
            {
                DataTable dt = GetErrRanking();

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                $"發生錯誤:{ex.ToString()}".pShow();
                btnErrRanking.Enabled = true;
            }

            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
            txtOrign.Text = _dicDoc[cmbXMLFile.Text];
            btnErrRanking.Enabled = true;
        }

        #region 刪除並保留最新一筆
        /// <summary>
        /// 刪除並保留最新一筆
        /// </summary>
        /// <param name="folderPath"></param>
        void CleanUpFiles()
        {
            string folderPath = _basSetting.GetFilePath;
            var arrFile = _dicDoc.Select(o => o.Key);


            string[] files = Directory.GetFiles(folderPath, "*.zip");
            // 找到每个组的最新修改文件
            string latestFile = GetLatestModifiedFile(files);

            // 遍历 A1、A2、A3 的文件
            foreach (string group in arrFile)
            {
                string pattern = $"{group}.zip";

                // 删除并移动到回收站
                foreach (string file in files)
                {
                    if (file != latestFile)
                    {
                        //DeleteFileToRecycleBin(file);
                        SetCmd(file);
                    }
                }
            }
        }

        string GetLatestModifiedFile(string[] files)
        {
            DateTime latestTime = DateTime.MinValue;
            string latestFile = null;

            foreach (string file in files)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(file);
                if (lastWriteTime > latestTime)
                {
                    latestTime = lastWriteTime;
                    latestFile = file;
                }
            }

            return latestFile;
        }

        void DeleteFileToRecycleBin(string filePath)
        {
            try
            {
                FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                Console.WriteLine($"Deleted to Recycle Bin: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }
        #endregion

        Process p = new Process();
        void SetCmd(string path)
        {

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true; //不跳出cmd視窗
            p.Start();
            p.StandardInput.WriteLine($"del \"{path}\"");//呼叫工作管理員
            p.WaitForExit(100);
            p.Close();
        }

        public void GetCreateSQL()
        {
            // 獲取應用程序的基本目錄路徑
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string Filetxt = "CreateSQL.txt";
            string FullPath = $@"{baseDirectory}{Filetxt}";

            _IsCreateSQL = File.Exists(FullPath) ? true : false;

            chkCreateSQL.Checked = _IsCreateSQL;
        }

        private void chkCreateSQL_CheckedChanged(object sender, EventArgs e)
        {
            // 獲取應用程序的基本目錄路徑
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string Filetxt = "CreateSQL.txt";
            string FullPath = $@"{baseDirectory}{Filetxt}";

            // 自動產生SQL
            if (chkCreateSQL.Checked == true && File.Exists(FullPath) == false)
            {
                _IsCreateSQL = true;
                using (StreamWriter writer = File.CreateText(FullPath))
                {
                    //....
                }
            }

            // 取消產生SQL
            if (chkCreateSQL.Checked == false && File.Exists(FullPath) == true)
            {
                _IsCreateSQL = false;
                File.Delete(FullPath);
            }

        }

        private void ShowDetail(int rowIndex)
        {
            string msg = string.Empty;

            DataGridViewRow selectedRow = dataGridView1.Rows[rowIndex];

            string column錯誤原因 = selectedRow.Cells["錯誤原因"].Value.ToString();
            msg += "=====================================================================\r\n";
            msg += _anaTxt.Run解析(column錯誤原因);
            msg += "=====================================================================\r\n\r\n";

            string column1Value = selectedRow.Cells["原始內容"].Value.ToString();
            msg += column1Value;

            string strM15 = selectedRow.Cells["就醫識別碼"].Value.ToString();
            msg += _anaTxt.GetSQL(strM15, _IsCreateSQL);
            msg += "\r\n\r\n";

            txtOrign.Text = msg;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex != -1)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                ShowDetail(rowIndex);
            }
        }

        public DataTable GetErrRanking()
        {
            SetAnalyzeText();
            string str = _dicDoc[cmbXMLFile.Text];
            _anaTxt.Get總排行數量(str);
            DataTable dt = _anaTxt._dt總排行數量;

            List<string> LByPass = new List<string>();
            LByPass.Add("M16 - 原就醫識別碼 *\r\nAA - 欄位資料必填寫");
            LByPass.Add("M16 - 原就醫識別碼 *\r\nD051 - 原處方服務機構代碼為本院，M16查無原就醫紀錄或原就醫紀錄之院所代號非M17。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            LByPass.Add("M17 - 原處方服務機構代號 *\r\nAA - 欄位資料必填寫");
            LByPass.Add("M19 - 原就診日期時間 *\r\nAA - 欄位資料必填寫");
            LByPass.Add("M33 - 已調劑連續處方箋次數 *\r\nD024 - 查無開立端上傳之慢性病連續處方箋料，不可執行。無處方聯次資料，不可執行。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            LByPass.Add("M34 - 已調劑連續處方箋次數 *\r\nD024 - 查無開立端上傳之慢性病連續處方箋料，不可執行。無處方聯次資料，不可執行。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            if (chkExcludeM16.Checked)
            {
                var rowsToRemove = dt.AsEnumerable()
                .Where(row => LByPass.Contains(row.Field<string>("sourceColumn")))
                .ToList();
                foreach (var rowToRemove in rowsToRemove)
                {
                    dt.Rows.Remove(rowToRemove);
                }
            }

            return dt;
        }

        private void btnAllXmlErr_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();

            foreach (var i in _dicDoc)
            {
                string XmlFile = i.Key;

                if (cmbXMLFile.Items.Contains(XmlFile))
                {
                    cmbXMLFile.SelectedItem = XmlFile;
                    Application.DoEvents();
                }

                DataTable dtNew = GetErrRanking();

                dtResult = Getdt合併統計(dtNew, dtResult);
            }

            dataGridView2.DataSource = dtResult;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        public DataTable Getdt合併統計(DataTable dt1, DataTable dt2)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("sourceColumn", typeof(string));
            dtResult.Columns.Add("Cnt", typeof(int));


            //var combinedData = from row1 in dt1.AsEnumerable()
            //                   join row2 in dt2.AsEnumerable()
            //                   on row1.Field<string>("sourceColumn") equals row2.Field<string>("sourceColumn") into joinedData
            //                   from data in joinedData.DefaultIfEmpty()
            //                   select new
            //                   {
            //                       sourceColumn = row1.Field<string>("sourceColumn"),
            //                       Cnt = (row1.Field<int>("Cnt") + (data == null ? 0 : data.Field<int>("Cnt")))
            //                   };

            var leftJoin = from row1 in dt1.AsEnumerable()
                           join row2 in dt2.AsEnumerable()
                           on row1.Field<string>("sourceColumn") equals row2.Field<string>("sourceColumn") into joinedData
                           from data in joinedData.DefaultIfEmpty()
                           select new
                           {
                               sourceColumn = row1.Field<string>("sourceColumn"),
                               Cnt = (row1.Field<int>("Cnt") + (data == null ? 0 : data.Field<int>("Cnt")))
                           };

            var rightJoin = from row2 in dt2.AsEnumerable()
                            join row1 in dt1.AsEnumerable()
                            on row2.Field<string>("sourceColumn") equals row1.Field<string>("sourceColumn") into joinedData
                            from data in joinedData.DefaultIfEmpty()
                            select new
                            {
                                sourceColumn = row2.Field<string>("sourceColumn"),
                                Cnt = (row2.Field<int>("Cnt") + (data == null ? 0 : data.Field<int>("Cnt")))
                            };

            // Merge the results of the left and right joins
            var fullJoin = leftJoin.Concat(rightJoin.Where(rj => !leftJoin.Any(lj => lj.sourceColumn == rj.sourceColumn)));


            foreach (var item in fullJoin)
            {
                dtResult.Rows.Add(item.sourceColumn, item.Cnt);
            }

            return dtResult;
        }
    }
}
