﻿
namespace IC20Analyze
{
    partial class Main
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.btnAllXmlErr = new System.Windows.Forms.Button();
            this.cmbXMLFile10 = new System.Windows.Forms.ComboBox();
            this.chkExcludeM16 = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.chkCreateSQL = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnErrRanking = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFileReload = new System.Windows.Forms.Button();
            this.cmbXMLFile20 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtOrign = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sourceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.txtOrign10 = new System.Windows.Forms.TextBox();
            this.狀態10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.第幾筆 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.身分證 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.就醫識別碼 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卡號 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.錯誤原因 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.原始內容 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAnalyze.Location = new System.Drawing.Point(794, 0);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(103, 196);
            this.btnAnalyze.TabIndex = 10;
            this.btnAnalyze.Text = "解析";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.狀態10,
            this.第幾筆,
            this.身分證,
            this.就醫識別碼,
            this.卡號,
            this.錯誤原因,
            this.原始內容});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(465, 382);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtOrign10);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.btnAllXmlErr);
            this.panel1.Controls.Add(this.cmbXMLFile10);
            this.panel1.Controls.Add(this.chkExcludeM16);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.chkCreateSQL);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.btnErrRanking);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.txtPath);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnFileReload);
            this.panel1.Controls.Add(this.cmbXMLFile20);
            this.panel1.Controls.Add(this.btnAnalyze);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 196);
            this.panel1.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 80);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(99, 20);
            this.label20.TabIndex = 93;
            this.label20.Text = "● 檔案1.0：";
            // 
            // btnAllXmlErr
            // 
            this.btnAllXmlErr.Location = new System.Drawing.Point(661, 151);
            this.btnAllXmlErr.Name = "btnAllXmlErr";
            this.btnAllXmlErr.Size = new System.Drawing.Size(95, 32);
            this.btnAllXmlErr.TabIndex = 90;
            this.btnAllXmlErr.Text = "統計XML";
            this.btnAllXmlErr.UseVisualStyleBackColor = true;
            this.btnAllXmlErr.Click += new System.EventHandler(this.btnAllXmlErr_Click);
            // 
            // cmbXMLFile10
            // 
            this.cmbXMLFile10.FormattingEnabled = true;
            this.cmbXMLFile10.Location = new System.Drawing.Point(115, 77);
            this.cmbXMLFile10.Name = "cmbXMLFile10";
            this.cmbXMLFile10.Size = new System.Drawing.Size(594, 28);
            this.cmbXMLFile10.TabIndex = 91;
            this.cmbXMLFile10.Text = "請選擇";
            // 
            // chkExcludeM16
            // 
            this.chkExcludeM16.AutoSize = true;
            this.chkExcludeM16.Location = new System.Drawing.Point(537, 153);
            this.chkExcludeM16.Name = "chkExcludeM16";
            this.chkExcludeM16.Size = new System.Drawing.Size(124, 24);
            this.chkExcludeM16.TabIndex = 89;
            this.chkExcludeM16.Text = "排除其他因素";
            this.chkExcludeM16.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(686, 128);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 20);
            this.label18.TabIndex = 88;
            this.label18.Text = "[0     ]";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(567, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 20);
            this.label19.TabIndex = 87;
            this.label19.Text = "排除M16因素:";
            // 
            // chkCreateSQL
            // 
            this.chkCreateSQL.AutoSize = true;
            this.chkCreateSQL.Location = new System.Drawing.Point(409, 156);
            this.chkCreateSQL.Name = "chkCreateSQL";
            this.chkCreateSQL.Size = new System.Drawing.Size(122, 24);
            this.chkCreateSQL.TabIndex = 86;
            this.chkCreateSQL.Text = "懶人SQL語法";
            this.chkCreateSQL.UseVisualStyleBackColor = true;
            this.chkCreateSQL.CheckedChanged += new System.EventHandler(this.chkCreateSQL_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 85;
            this.label1.Text = "● 檔案名稱：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 154);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(109, 20);
            this.label17.TabIndex = 84;
            this.label17.Text = "● 全文搜尋：";
            // 
            // btnErrRanking
            // 
            this.btnErrRanking.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnErrRanking.Location = new System.Drawing.Point(715, 4);
            this.btnErrRanking.Name = "btnErrRanking";
            this.btnErrRanking.Size = new System.Drawing.Size(73, 47);
            this.btnErrRanking.TabIndex = 83;
            this.btnErrRanking.Text = "最多錯誤\r\n原因排名";
            this.btnErrRanking.UseVisualStyleBackColor = true;
            this.btnErrRanking.Click += new System.EventHandler(this.btnErrRanking_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(115, 151);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(288, 29);
            this.txtSearch.TabIndex = 82;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 128);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 20);
            this.label16.TabIndex = 81;
            this.label16.Text = "● 檢核結果：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(141, 20);
            this.label15.TabIndex = 80;
            this.label15.Text = "● 上傳申報資訊：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(495, 128);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 20);
            this.label14.TabIndex = 79;
            this.label14.Text = "[xxx     ]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(412, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 20);
            this.label13.TabIndex = 78;
            this.label13.Text = "無效明細:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(339, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 20);
            this.label12.TabIndex = 77;
            this.label12.Text = "[4948     ]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 20);
            this.label11.TabIndex = 76;
            this.label11.Text = "有效醫令:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(194, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 20);
            this.label10.TabIndex = 75;
            this.label10.Text = "[0       ]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(111, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 74;
            this.label9.Text = "有效明細:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(270, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 20);
            this.label8.TabIndex = 73;
            this.label8.Text = "[1130119154546]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(702, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 72;
            this.label7.Text = "[xxx         ]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(587, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 20);
            this.label6.TabIndex = 71;
            this.label6.Text = "實際接收筆數:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(495, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 70;
            this.label5.Text = "[4179388 ]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.TabIndex = 69;
            this.label4.Text = "檔案大小:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 68;
            this.label3.Text = "上傳日期/時間:";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOpenFile.Location = new System.Drawing.Point(638, 40);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(71, 32);
            this.btnOpenFile.TabIndex = 67;
            this.btnOpenFile.Text = "開啟路徑";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(115, 40);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(517, 29);
            this.txtPath.TabIndex = 66;
            this.txtPath.Tag = "可以先打在這邊";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "● 檔案路徑:";
            // 
            // btnFileReload
            // 
            this.btnFileReload.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFileReload.Location = new System.Drawing.Point(638, 3);
            this.btnFileReload.Name = "btnFileReload";
            this.btnFileReload.Size = new System.Drawing.Size(71, 32);
            this.btnFileReload.TabIndex = 64;
            this.btnFileReload.Text = "重新載入";
            this.btnFileReload.UseVisualStyleBackColor = true;
            this.btnFileReload.Click += new System.EventHandler(this.btnFileReload_Click);
            // 
            // cmbXMLFile20
            // 
            this.cmbXMLFile20.FormattingEnabled = true;
            this.cmbXMLFile20.Location = new System.Drawing.Point(115, 7);
            this.cmbXMLFile20.Name = "cmbXMLFile20";
            this.cmbXMLFile20.Size = new System.Drawing.Size(517, 28);
            this.cmbXMLFile20.TabIndex = 63;
            this.cmbXMLFile20.Text = "請選擇";
            this.cmbXMLFile20.SelectedIndexChanged += new System.EventHandler(this.cmbXMLFile_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtOrign);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(475, 196);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(422, 382);
            this.panel3.TabIndex = 15;
            // 
            // txtOrign
            // 
            this.txtOrign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOrign.Font = new System.Drawing.Font("Courier New", 12F);
            this.txtOrign.Location = new System.Drawing.Point(0, 0);
            this.txtOrign.Margin = new System.Windows.Forms.Padding(5);
            this.txtOrign.MaxLength = 327679999;
            this.txtOrign.Multiline = true;
            this.txtOrign.Name = "txtOrign";
            this.txtOrign.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOrign.Size = new System.Drawing.Size(422, 382);
            this.txtOrign.TabIndex = 8;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Blue;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(465, 196);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 382);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView2);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 196);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(465, 382);
            this.panel2.TabIndex = 14;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sourceColumn,
            this.Cnt});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(465, 382);
            this.dataGridView2.TabIndex = 13;
            // 
            // sourceColumn
            // 
            this.sourceColumn.DataPropertyName = "sourceColumn";
            this.sourceColumn.HeaderText = "sourceColumn";
            this.sourceColumn.Name = "sourceColumn";
            this.sourceColumn.ReadOnly = true;
            this.sourceColumn.Width = 600;
            // 
            // Cnt
            // 
            this.Cnt.DataPropertyName = "Cnt";
            this.Cnt.HeaderText = "Cnt";
            this.Cnt.Name = "Cnt";
            this.Cnt.ReadOnly = true;
            this.Cnt.Width = 80;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(465, 382);
            this.richTextBox1.TabIndex = 12;
            this.richTextBox1.Text = "";
            // 
            // txtOrign10
            // 
            this.txtOrign10.Location = new System.Drawing.Point(742, 76);
            this.txtOrign10.Name = "txtOrign10";
            this.txtOrign10.Size = new System.Drawing.Size(46, 29);
            this.txtOrign10.TabIndex = 94;
            // 
            // 狀態10
            // 
            this.狀態10.DataPropertyName = "1.0狀態";
            this.狀態10.HeaderText = "狀態10";
            this.狀態10.Name = "狀態10";
            this.狀態10.ReadOnly = true;
            this.狀態10.Width = 84;
            // 
            // 第幾筆
            // 
            this.第幾筆.DataPropertyName = "第幾筆";
            this.第幾筆.HeaderText = "第幾筆";
            this.第幾筆.Name = "第幾筆";
            this.第幾筆.ReadOnly = true;
            this.第幾筆.Width = 82;
            // 
            // 身分證
            // 
            this.身分證.DataPropertyName = "身分證M03";
            this.身分證.HeaderText = "身分證";
            this.身分證.Name = "身分證";
            this.身分證.ReadOnly = true;
            this.身分證.Width = 82;
            // 
            // 就醫識別碼
            // 
            this.就醫識別碼.DataPropertyName = "就醫識別碼M15";
            this.就醫識別碼.HeaderText = "就醫識別碼";
            this.就醫識別碼.Name = "就醫識別碼";
            this.就醫識別碼.ReadOnly = true;
            this.就醫識別碼.Width = 114;
            // 
            // 卡號
            // 
            this.卡號.DataPropertyName = "卡號";
            this.卡號.HeaderText = "卡號";
            this.卡號.Name = "卡號";
            this.卡號.ReadOnly = true;
            this.卡號.Width = 66;
            // 
            // 錯誤原因
            // 
            this.錯誤原因.DataPropertyName = "錯誤原因";
            this.錯誤原因.HeaderText = "錯誤原因";
            this.錯誤原因.Name = "錯誤原因";
            this.錯誤原因.ReadOnly = true;
            this.錯誤原因.Width = 98;
            // 
            // 原始內容
            // 
            this.原始內容.DataPropertyName = "原始內容";
            this.原始內容.HeaderText = "原始內容";
            this.原始內容.Name = "原始內容";
            this.原始內容.ReadOnly = true;
            this.原始內容.Visible = false;
            this.原始內容.Width = 98;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 578);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Main";
            this.Text = "IC20Analyze衛生福利部中央健康保險署特約醫事服務機構-健保IC卡資料上傳作業檢核結果明細表";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnFileReload;
        private System.Windows.Forms.ComboBox cmbXMLFile20;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtOrign;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnErrRanking;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cnt;
        private System.Windows.Forms.CheckBox chkCreateSQL;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chkExcludeM16;
        private System.Windows.Forms.Button btnAllXmlErr;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbXMLFile10;
        private System.Windows.Forms.TextBox txtOrign10;
        private System.Windows.Forms.DataGridViewTextBoxColumn 狀態10;
        private System.Windows.Forms.DataGridViewTextBoxColumn 第幾筆;
        private System.Windows.Forms.DataGridViewTextBoxColumn 身分證;
        private System.Windows.Forms.DataGridViewTextBoxColumn 就醫識別碼;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卡號;
        private System.Windows.Forms.DataGridViewTextBoxColumn 錯誤原因;
        private System.Windows.Forms.DataGridViewTextBoxColumn 原始內容;
    }
}

