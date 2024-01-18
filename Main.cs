using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS2Module.UtilExtension;

namespace IC20Analyze
{
    public partial class Main : Form
    {

        AnalyticsTxt _anaTxt = new AnalyticsTxt();

        public Main()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, EventArgs e)
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

            bool isOK = _anaTxt._dicFieldCH.ContainsKey(strField);

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
    }
}
