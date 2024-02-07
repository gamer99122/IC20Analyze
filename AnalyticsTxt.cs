using HIS2Module;
using HIS2Module.DB;
using HIS2Module.UtilExtension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IC20Analyze
{
    class AnalyticsTxt
    {
        #region G等變數
        private APSecurity _G = APSecurityInstance.G;
        private DBConn _db = APSecurityInstance.DB;
        private SqlConnection _conn = APSecurityInstance.Conn;
        #endregion

        /// <summary>
        /// 欄位與中文對照表
        /// </summary>
        public Dictionary<string, string> _dicFieldCH;

        /// <summary>
        /// 錯誤代碼:中文對照註釋
        /// </summary>
        public Dictionary<string, string> _dicErrCodeCH;


        public DataTable _dt總排行數量;
        public int i總共幾個錯誤原因列 = 0;
        public DataTable _dt錯誤訊息總表;
        public DataTable _dt錯誤訊息明細;
        public string _str上傳日期_時間;
        public string _str檔案大小;
        public string _str實際接收筆數;
        public string _str有效明細;
        public string _str有效醫令;
        public string _str無效明細;
        public string _strTxt純文字內容;

        public AnalyticsTxt()
        {
            LoadFieldCH();
            LoadErrCodeCH();
            LoadDt錯誤訊息總表();
            LoadDt錯誤訊息明細();


            _str上傳日期_時間 = "";
            _str檔案大小 = "";
            _str實際接收筆數 = "";
            _str有效明細 = "";
            _str有效醫令 = "";
            _str無效明細 = "";
        }

        private void LoadFieldCH()
        {
            _dicFieldCH = new Dictionary<string, string>();
            _dicFieldCH.Add("M01", "安全模組代碼");
            _dicFieldCH.Add("M02", "卡片號碼");
            _dicFieldCH.Add("M03", "身分證號或身分證明文");
            _dicFieldCH.Add("M04", "出生日期");
            _dicFieldCH.Add("M05", "醫療院所代碼");
            _dicFieldCH.Add("M06", "醫事人員身分證號");
            _dicFieldCH.Add("M07", "就醫類別");
            _dicFieldCH.Add("M08", "新生兒出生日期");
            _dicFieldCH.Add("M09", "新生兒胞胎註記");
            _dicFieldCH.Add("M10", "新生兒就醫註記");
            _dicFieldCH.Add("M11", "就診日期時間");
            _dicFieldCH.Add("M12", "補卡註記");
            _dicFieldCH.Add("M13", "就醫序號");
            _dicFieldCH.Add("M14", "安全簽章");
            _dicFieldCH.Add("M15", "就醫識別碼 *");
            _dicFieldCH.Add("M16", "原就醫識別碼 *");
            _dicFieldCH.Add("M17", "原處方服務機構代號 *");
            _dicFieldCH.Add("M18", "原處方就醫序號 *");
            _dicFieldCH.Add("M19", "原就診日期時間 *");
            _dicFieldCH.Add("M20", "給藥日份 *");
            _dicFieldCH.Add("M21", "慢性病連續處方箋總給 *");
            _dicFieldCH.Add("M22", "管制藥品專用處方箋 *");
            _dicFieldCH.Add("M23", "處方調劑方式 *");
            _dicFieldCH.Add("M24", "可調劑次數_A-一般處 *");
            _dicFieldCH.Add("M25", "可調劑次數_B-慢性病 *");
            _dicFieldCH.Add("M26", "連續處方箋可調劑次數 *");
            _dicFieldCH.Add("M27", "可調劑次數_D-管制藥 *");
            _dicFieldCH.Add("M28", "可調劑次數_E-管制藥 *");
            _dicFieldCH.Add("M29", "連續處方箋可調劑次數 *");
            _dicFieldCH.Add("M30", "物理治療數量/已執行 *");
            _dicFieldCH.Add("M31", "職能治療數量/已執行 *");
            _dicFieldCH.Add("M32", "語言治療數量/已執行 *");
            _dicFieldCH.Add("M33", "已調劑連續處方箋次數 *");
            _dicFieldCH.Add("M34", "已調劑連續處方箋次數 *");
            _dicFieldCH.Add("M35", "主要診斷碼");
            _dicFieldCH.Add("M36", "次要診斷碼一");
            _dicFieldCH.Add("M37", "次要診斷碼二");
            _dicFieldCH.Add("M38", "次要診斷碼三");
            _dicFieldCH.Add("M39", "次要診斷碼四");
            _dicFieldCH.Add("M40", "次要診斷碼五");
            _dicFieldCH.Add("M41", "主手術(處置)代碼 *");
            _dicFieldCH.Add("M42", "次手術(處置)代碼(一) *");
            _dicFieldCH.Add("M43", "次手術(處置)代碼(二) *");
            _dicFieldCH.Add("M44", "門診醫療費用（當次）");
            _dicFieldCH.Add("M45", "門診部分負擔費用");
            _dicFieldCH.Add("M46", "住院醫療費用(當次)");
            _dicFieldCH.Add("M47", "住院部分負擔費用");
            _dicFieldCH.Add("M48", "健保資料段8-10-5.住");
            _dicFieldCH.Add("M49", "實際就醫（調劑或檢查)");
            _dicFieldCH.Add("M50", "病床號");
            _dicFieldCH.Add("M51", "給付類別");
            _dicFieldCH.Add("M52", "實際就醫（調劑或檢查) *");
            _dicFieldCH.Add("M53", "部分負擔-2 *");
            _dicFieldCH.Add("M54", "部分負擔-3 *");
            _dicFieldCH.Add("M55", "部分負擔-4 *");
            _dicFieldCH.Add("M56", "醫事類別 *");
            _dicFieldCH.Add("D01", "就診日期時間");
            _dicFieldCH.Add("D02", "醫令類別(更新定義)");
            _dicFieldCH.Add("D03", "醫令序號 *");
            _dicFieldCH.Add("D04", "處方種類 *");
            _dicFieldCH.Add("D05", "醫令調劑方式 *");
            _dicFieldCH.Add("D06", "診療項目代號");
            _dicFieldCH.Add("D07", "診療部位");
            _dicFieldCH.Add("D08", "用法(藥品使用頻率)");
            _dicFieldCH.Add("D09", "天數");
            _dicFieldCH.Add("D10", "總量");
            _dicFieldCH.Add("D11", "處方簽章(須配合法規)");
            _dicFieldCH.Add("D12", "委託或受託執行轉(代)");
            _dicFieldCH.Add("D13", "藥品批號");
            _dicFieldCH.Add("D14", "給藥途徑/作用部位");
            _dicFieldCH.Add("D15", "備註說明");
            _dicFieldCH.Add("V01", "疫苗批號");
            _dicFieldCH.Add("V02", "疫苗種類");
            _dicFieldCH.Add("E01", "過敏藥物上傳註記");
            _dicFieldCH.Add("E02", "過敏藥品成分代碼");
            _dicFieldCH.Add("E03", "過敏藥品類別代碼");
            _dicFieldCH.Add("E04", "過敏藥物(非健保給付");
            _dicFieldCH.Add("E05", "過敏或不良反應症狀代");
            _dicFieldCH.Add("E06", "其他不良反應症狀說明");
            _dicFieldCH.Add("E07", "嚴重程度代碼");
            _dicFieldCH.Add("E08", "資料來源代碼");
            _dicFieldCH.Add("E09", "資料來源說明");
            _dicFieldCH.Add("E10", "過敏或不良反應症狀發");
            _dicFieldCH.Add("E11", "刪除過敏藥物註記原因");
            _dicFieldCH.Add("E12", "其他刪除過敏藥物註記");
            _dicFieldCH.Add("E13", "基因檢測結果");
            _dicFieldCH.Add("H00", "資料型態");
            _dicFieldCH.Add("H01", "資料格式");
        }

        private void LoadErrCodeCH()
        {
            _dicErrCodeCH = new Dictionary<string, string>();
            _dicErrCodeCH.Add("01", "資料型態檢核錯誤");
            _dicErrCodeCH.Add("02", "資料格式檢核錯誤");
            _dicErrCodeCH.Add("03", "上傳版本檢核錯誤");
            _dicErrCodeCH.Add("04", "安全簽章驗證不通過");
            _dicErrCodeCH.Add("05", "卡片上的個人基本資料與本署資料庫不符");
            _dicErrCodeCH.Add("06", "安全模組上的院所基本資料與本署資料庫不符");
            _dicErrCodeCH.Add("07", "院所代碼與上傳之sam卡內之院所代號不符");
            _dicErrCodeCH.Add("08", "醫事機構主檔無此代碼");
            _dicErrCodeCH.Add("09", "醫事機構不在特約期間內或就診日期長度或資料形態不正確");
            _dicErrCodeCH.Add("10", "藥品主檔無此代碼");
            _dicErrCodeCH.Add("11", "支付標準檔無此代碼");
            _dicErrCodeCH.Add("12", "特材主檔無此代碼");
            _dicErrCodeCH.Add("13", "醫事人員主檔無此代碼");
            _dicErrCodeCH.Add("14", "醫事人員代碼不在合約期間");
            _dicErrCodeCH.Add("15", "診斷代碼不符規定內容");
            _dicErrCodeCH.Add("16", "處方簽章驗證不通過");
            _dicErrCodeCH.Add("17", "就醫資料異動平台已異動，不可補正");
            _dicErrCodeCH.Add("19", "(D06)為R001~R007，S001~S004，醫令類別(D02)為G或H。");
            _dicErrCodeCH.Add("20", "主、次診斷不符性別限制");
            _dicErrCodeCH.Add("22", "此就醫日期時間為該ID他院就醫紀錄，不可上傳");
            _dicErrCodeCH.Add("23", "無此虛擬醫令或此虛擬醫令已失效");
            _dicErrCodeCH.Add("AA", "欄位資料必填寫");
            _dicErrCodeCH.Add("AA01", "欄位資料必填且不得為0");
            _dicErrCodeCH.Add("AA02", "欄位資料必填寫，此筆上傳資料不採計，請重新上傳");
            _dicErrCodeCH.Add("AA03", "欄位資料必填寫，此筆上傳資料不採計，請重新上傳(附表二填寫邏輯)");
            _dicErrCodeCH.Add("AA04", "醫令類別(D02)為1:藥品主檔或M:釋出處方之未調劑藥品時，本欄必填且須>0");
            _dicErrCodeCH.Add("AB", "欄位資料型態錯誤");
            _dicErrCodeCH.Add("AC", "欄位資料長度不符合");
            _dicErrCodeCH.Add("AD", "欄位資料範圍不存在或無完整的就醫日期時間(M11/M49)可比對");
            _dicErrCodeCH.Add("AD01", "就醫類別(M07)為01~09，就醫序號(M13)需為0001~1500");
            _dicErrCodeCH.Add("AD02", "資料格式(H01)為B，就醫序號(M13)需為公告之異常代碼");
            _dicErrCodeCH.Add("AD08", "診療項目代號(D06)與(D07)牙位代碼不吻合");
            _dicErrCodeCH.Add("AD09", "D07牙位代碼與治療項目代號(D06)不吻合");
            _dicErrCodeCH.Add("AD13", "就醫類別(M07)為AC時，就醫序號(M13)需為IC**");
            _dicErrCodeCH.Add("AD14", "資料格式(H01)為A且就醫類別(M07)非[01-09，AC]時，就醫序號(M13)需為空值");
            _dicErrCodeCH.Add("AD19", "非指定就醫案例，請使用正確的就醫類別。");
            _dicErrCodeCH.Add("AD20", "指定就醫患者至非指定院所看診。");
            _dicErrCodeCH.Add("AD21", "指定就醫患者，請使用就醫類別00上傳。");
            _dicErrCodeCH.Add("AD22", "矯正機關代碼非貴院所承做或參與。");
            _dicErrCodeCH.Add("AD23", "藥局調劑就醫類別應為AF(藥局調劑)。");
            _dicErrCodeCH.Add("AD24", "就醫序號填報值或就醫類別填報值與戒菸服務規定不一");
            _dicErrCodeCH.Add("AD25", "A2001C須配合填入診療項目代碼(D06)流感疫苗藥品代碼");
            _dicErrCodeCH.Add("AD26", "流感疫苗藥品之醫令類別(D02)必為1");
            _dicErrCodeCH.Add("AD30", "同一ID同日同時段已上傳報告資料");
            _dicErrCodeCH.Add("AD31", "就醫類別應為CA且醫令類別應為G");
            _dicErrCodeCH.Add("AD32", "補卡註記(M12)應為2、資料格式(H01)應為2");
            _dicErrCodeCH.Add("AD33", "補卡註記(M12)應為1");
            _dicErrCodeCH.Add("AD34", "僅提示本筆未於報告日期次日23:59前上傳。如無合併其他錯誤，本筆仍會收載。");
            _dicErrCodeCH.Add("AD35", "就醫序號(M13)應為CV19或FORE");
            _dicErrCodeCH.Add("AD36", "每筆資料限一筆結果");
            _dicErrCodeCH.Add("AD37", "就醫序號(M13)為FORE/CV19時，診療項目代碼(D06)限FSTP-COVID19、FSTN-COVID19、PCRP-COVID19、PCRN-COVID19、CV19-S-Ab-N、CV19-S-Ab-P、HSTP-COVID19、HPCP-COVID19、RPCRPCOVID19、RFSTPCOVID19、RHSTPCOVID19、RHPCPCOVID19");
            _dicErrCodeCH.Add("AD38", "本欄僅為空白或PAY");
            _dicErrCodeCH.Add("AD39", "此項目代碼相同就醫日期僅接受1筆且數量1.0");
            _dicErrCodeCH.Add("AD40", "此項目代碼總量(D10)限1.0。");
            _dicErrCodeCH.Add("AD41", "M51非W或X，主、次診斷不可為U071。");
            _dicErrCodeCH.Add("AD42", "無此給付類別(M51)代碼。");
            _dicErrCodeCH.Add("AD43", "核對醫事機構資料檔無此病床號資料。");
            _dicErrCodeCH.Add("AD44", "M51非W或X，M13不可為IC09/CV19。");
            _dicErrCodeCH.Add("AD45", "COVID-19快篩及PCR結果之民眾電話，僅限半形「數字」及半形「#」");
            _dicErrCodeCH.Add("AD46", "核酸檢測結果未上傳採件日期時間/採檢日期時間不符");
            _dicErrCodeCH.Add("AD47", "核酸檢驗結果未填檢驗醫事機構代號。如無合併其他錯誤，本筆仍會收載。");
            _dicErrCodeCH.Add("AD48", "D06=ViT-COVID19、PhT-COVID19且H01=2、4時，M13不可為Z000");
            _dicErrCodeCH.Add("AD49", "D06為XCOVID0001且M07為AF、AE時，就醫序號(M13)之異常就醫序號不可為Z000、G000、IC98、F00B、CV19、FORE、TM01、J000。");
            _dicErrCodeCH.Add("AD50", "D06為XCOVID0001、XCOVID0002且A23為BC，就醫序號(M13)之異常就醫序號限J000、IC09。");
            _dicErrCodeCH.Add("AD51", "抗病毒口服藥:(1)門急診給藥，D02限1、M。(2)藥局、院所調劑，D02限1。");
            _dicErrCodeCH.Add("AD52", "抗病毒口服藥，住院期間開藥，D02限1。");
            _dicErrCodeCH.Add("AD53", "抗病毒口服藥:門急診給藥，限(D02=1且D05=0)或(D02=M且D05=1)。");
            _dicErrCodeCH.Add("AD54", "抗病毒口服藥:藥局或院所調劑，限D02=1且D05=A。");
            _dicErrCodeCH.Add("AD55", "抗病毒口服藥，門急診給藥:限D02=1且D05=0，住院期間開藥:限D02=1且D05=0。");
            _dicErrCodeCH.Add("AD56", "D06為XCOVID0001、XCOVID0002時，D08用法必填且限填BID或Q12H或ASORDER。");
            _dicErrCodeCH.Add("AD57", "D06為XCOVID0001、XCOVID0002且M07非BC時，D09天數限5天。");
            _dicErrCodeCH.Add("AD58", "D06為XCOVID0001、XCOVID0002且M07為BC時，1天<=D09天數<=5天。");
            _dicErrCodeCH.Add("AD61", "XCOVID0001、XCOVID0002僅限門/急診給藥(M07為01、04、06、07、08、09、AI、BD)、藥局調劑(M07=AF)、院所調劑(M07=AE)、住院期間(M07=BC)開藥。");
            _dicErrCodeCH.Add("AD62", "當診療項目代號(D06)=XCOVID0001或XCOVID0002時，醫令類別(D02)必為1或M。");
            _dicErrCodeCH.Add("AD63", "院所調劑、院所調劑僅限XCOVID0001");
            _dicErrCodeCH.Add("AD64", "就醫序號(M13)HVIT限試辦計畫代碼：PB(COVID-19(武漢肺炎)疫情期間之視訊診療)院所。");
            _dicErrCodeCH.Add("AD65", "D06為XCOVID0001、XCOVID0002時，(1)M07為01、06、07、08、09、AI時，就醫序號(M13)之異常就醫序號不可為Z000、G000、IC98、F00B、CV19、FORE、TM01、J000;(2)M07為04、BD時，就醫序號(M13)之異常就醫序號不可為Z000、G000、IC98、F00B、CV19、FORE、TM01。");
            _dicErrCodeCH.Add("AD66", "HB病床後限助產所院所且就醫類別須為05");
            _dicErrCodeCH.Add("AD67", "異常就醫序號「MSPT」限試辦計畫代碼HU使用，且僅得上傳「追蹤管理費(P7502C)」且就醫日期需於效期內");
            _dicErrCodeCH.Add("AD68", "就醫日期8月1日起(含)，D06(診療項目代碼)為E5012C時，D10(總量)需大於等於1.0且小於等於5.0");
            _dicErrCodeCH.Add("AD69", "請依肺中指字第1123700039號函:「嚴重特殊傳染性肺炎」病例定義修正辦理。");
            _dicErrCodeCH.Add("AD70", "請依疾管檢驗字第1121300831號函:核酸檢驗(12215C)、抗原檢驗(14084C)已納入健保支付標準，請依支付標準規範及上傳方式辦理。");
            _dicErrCodeCH.Add("AD71", "當M13=ICC4時，M07限CA且D06_限P7701C-P7715B。(請依全民健康保險癌症治療改善計畫上傳。)");
            _dicErrCodeCH.Add("AD72", "當D06為「CHEX11209PIN」時，給付類別M51欄位必為1或U");
            _dicErrCodeCH.Add("AE", "欄位代碼不存在");
            _dicErrCodeCH.Add("AE01", "資料格式(H01)為A，就醫類別(M07)為非累計就醫序號時，就醫序號(M13)不可為公告之異常代碼");
            _dicErrCodeCH.Add("AF", "欄位資料不填寫");
            _dicErrCodeCH.Add("AF01", "欄位資料不填寫(附表二填寫邏輯)");
            _dicErrCodeCH.Add("AG", "欄位資料內超出範圍或MB2筆數超出範圍(健保就醫資料500筆，預防接種資料10筆)");
            _dicErrCodeCH.Add("AH", "欄位不得為0或空白");
            _dicErrCodeCH.Add("AH01", "就醫類別(M07)=01.02.03.04.06.07.08.09且醫令類別(D02)不為空值時，門診醫療費用(M44)不得為0或空白");
            _dicErrCodeCH.Add("AH02", "就醫類別(M07)=BB.DC.BF時，住院醫療費用(M46)不得為0或空白");
            _dicErrCodeCH.Add("AH03", "就醫類別(M07)=01.02.03.04.06.07.08.09，住院醫療費用(M46)或住院部分負擔費用(M47)應為0或空白");
            _dicErrCodeCH.Add("AH04", "就醫類別(M07)=BB.DC.BF時，門診醫療費用(M44)或門診部分負擔費用(M45)應為0或空白");
            _dicErrCodeCH.Add("AH05", "就醫類別(M07)=01.02.03.04.06.07.08.09，且醫令類別(D02)不為空值時，主要診斷碼(M35)不得為空白");
            _dicErrCodeCH.Add("BA", "大於等於新生兒胞胎註記。");
            _dicErrCodeCH.Add("BB", "有新生兒就醫註記時，新生兒胞胎註記及新生兒出生日期不可空白");
            _dicErrCodeCH.Add("BB01", "新生兒胞胎註記(M09)需大於0小於等於5");
            _dicErrCodeCH.Add("BB02", "新生兒就醫註記(M10)代表之胎次不可大於新生兒胞胎註記M09");
            _dicErrCodeCH.Add("BB03", "生產醫令之D15必填。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BB04", "M13為ICND時，D15欄位應包含「新生兒胞胎註記」及「被依附者非產婦之身分證號」。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BB05", "M13非ICND時，D15之「被依附者非產婦之身分證號」不可填。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BB06", "「被依附者非產婦之身分證號」需為本保險對象。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BB07", "限未具健保身分生產案件使用(D06必須有一筆且只能一筆且限生產醫令)。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BB08", "限未具健保身分生產案件使用(M03具健保身分不可使用ICND異常就醫序號)。112/09/01日前，如無合併其它錯誤，本筆就醫紀錄仍會收載，112/09/01日起檢核錯誤，予以退件處理。");
            _dicErrCodeCH.Add("BC", "有新生兒就醫註記時，新生兒出生日期不可空白");
            _dicErrCodeCH.Add("BD", "大於上傳日期時間");
            _dicErrCodeCH.Add("BD01", "大於等於上傳日期時間");
            _dicErrCodeCH.Add("BE", "大於就診日期時間或M11與D01不一致");
            _dicErrCodeCH.Add("BE01", "不可小於就醫日期(M11)");
            _dicErrCodeCH.Add("BF", "大於接種日期時間");
            _dicErrCodeCH.Add("BG", "小於出生日期");
            _dicErrCodeCH.Add("BH", "上傳資料已逾期可處理日期範圍(3個月內)");
            _dicErrCodeCH.Add("BK", "欄位不得補正");
            _dicErrCodeCH.Add("C001", "資料重複：鍵值資料已存在");
            _dicErrCodeCH.Add("C002", "資料重複：補正上傳(正常資料)無法取代正常上傳資料");
            _dicErrCodeCH.Add("C003", "資料重複：90日內已有生產紀錄");
            _dicErrCodeCH.Add("C004", "資料重複：同一包就醫資料生產醫令重複");
            _dicErrCodeCH.Add("D000", "D02非範圍值，D06無法檢核");
            _dicErrCodeCH.Add("D001", "本署資料庫無該就醫紀錄或慢連箋處方紀錄，無法刪除或註銷或取消註銷");
            _dicErrCodeCH.Add("D002", "M15、M16、M52就醫識別碼驗證不通過。");
            _dicErrCodeCH.Add("D003", "就醫識別碼反解之身分證號與上傳之身分證號欄位不一致。");
            _dicErrCodeCH.Add("D004", "就醫識別碼反解之院所代號與上傳之院所代號欄位不一致。");
            _dicErrCodeCH.Add("D005", "就醫識別碼反解之就醫日期時間與上傳之就醫日期時間欄位不一致。");
            _dicErrCodeCH.Add("D006", "就醫類別M07為01~09且D06有R001~R008、S001~S004時，本欄必填。");
            _dicErrCodeCH.Add("D007", "應小於M11/M49。但M07=AI時，M19應小於M11/M49且M19前7碼需等於M11/M49前7碼。");
            _dicErrCodeCH.Add("D008", "需為M20的倍數。");
            _dicErrCodeCH.Add("D009", "M26大於1時，本欄必須大於7。");
            _dicErrCodeCH.Add("D010", "M29大於1時，本欄必須大於7。");
            _dicErrCodeCH.Add("D011", "M20>1時，M23不可為2、E、F");
            _dicErrCodeCH.Add("D013", "如有值，只可為0、1。");
            _dicErrCodeCH.Add("D014", "M21大於1時，本欄必填且需大於1且小於等於4。");
            _dicErrCodeCH.Add("D015", "M21空值或0時，本欄不可填。");
            _dicErrCodeCH.Add("D016", "欲減少的處方聯次已被調劑不可取消。");
            _dicErrCodeCH.Add("D017", "M22大於1時，本欄必填且需大於1且小於等於4。");
            _dicErrCodeCH.Add("D018", "M22空值或0時，本欄不可填。");
            _dicErrCodeCH.Add("D019", "調劑之聯次需小於等於可調劑次數");
            _dicErrCodeCH.Add("D020", "M13=G000，M12須為1");
            _dicErrCodeCH.Add("D021", "就醫類別非AE、AF時，本欄如>=1，則M21(M22)需大於0且M26(M29)需大於1。");
            _dicErrCodeCH.Add("D022", "聯次範圍或順序錯誤。");
            _dicErrCodeCH.Add("D023", "此處方聯次已被註銷不可執行");
            _dicErrCodeCH.Add("D024", "查無開立端上傳之慢性病連續處方箋料，不可執行。無處方聯次資料，不可執行。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            _dicErrCodeCH.Add("D025", "此聯次處方已被他院執行，不可執行");
            _dicErrCodeCH.Add("D027", "M23為1、C、D時，D02不可為1(排除D14=HD、ID、IA、IE、IM、IV、IP、IPLE、ICV、IS、IT、IVA、IV、IVI、IVP、LA、LI、SC、SCI、VAG)，M23為0、6、A、B時，D02不可為M。");
            _dicErrCodeCH.Add("D028", "同一XML且同一筆就醫紀錄內之醫令序號不得重複。");
            _dicErrCodeCH.Add("D029", "醫令類別(D02)為1:藥品主檔或M:釋出處方之未調劑藥品時，本欄必填。");
            _dicErrCodeCH.Add("D032", "原就醫序號錯誤。");
            _dicErrCodeCH.Add("D033", "D06為R001~R008或S001~S004時，就醫類別需為01~09、AE、AF");
            _dicErrCodeCH.Add("D034", "所有處方聯次已執行或註銷，無法註銷");
            _dicErrCodeCH.Add("D035", "欲處理之處方聯次已被他院執行或註銷，不可刪除調劑");
            _dicErrCodeCH.Add("D036", "M23=1、2、C、D時，M20必為0");
            _dicErrCodeCH.Add("D037", "非同日同醫師就診;查無同日同醫師就醫之第１次就醫紀錄");
            _dicErrCodeCH.Add("D038", "所有處方聯次狀態非註銷狀態，無法取消註銷");
            _dicErrCodeCH.Add("D039", "欲處理之處方聯次已被他院執行或註銷，不可刪除開立");
            _dicErrCodeCH.Add("D040", "醫令類別(D02)為1:藥品主檔或M:釋出處方之未調劑藥品時，本欄需大於0小於等於90。");
            _dicErrCodeCH.Add("D041", "醫令類別(D02)為1:藥品主檔時，本欄需小於等於M20。");
            _dicErrCodeCH.Add("D042", "M13非無健保身分之異常就醫序號");
            _dicErrCodeCH.Add("D043", "M21/M22大於1且M23為為1、C、D時，M33/M34必為0");
            _dicErrCodeCH.Add("D044", "M23=1，D02不可為1");
            _dicErrCodeCH.Add("D045", "M23=2時，D02不可為1、M");
            _dicErrCodeCH.Add("D046", "M18=FORE，D06限採檢結果、CV19-S-Ab-N、CV19-S-Ab-P");
            _dicErrCodeCH.Add("D047", "D02與D05交叉邏輯檢核錯誤，請參考健保卡資料上傳格式2.0作業說明之D05資料說明。");
            _dicErrCodeCH.Add("D049", "原處方服務機構代碼非本院，M16查無原就醫紀錄或原就醫紀錄之院所代號非M17。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            _dicErrCodeCH.Add("D050", "原處方服務機構代碼非本院，原就醫紀錄就醫類別非01~09、AD、BA、BG、BE。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            _dicErrCodeCH.Add("D051", "原處方服務機構代碼為本院，M16查無原就醫紀錄或原就醫紀錄之院所代號非M17。(雙軌期間，如無合併其他錯誤，本筆仍會收載)");
            _dicErrCodeCH.Add("D052", "原處方服務機構代碼為本院，原就醫紀錄就醫類別非01~09、AD、BA、BG、BE");
            _dicErrCodeCH.Add("D053", "藥局及院所調劑，H01不可為C、E");
            _dicErrCodeCH.Add("D054", "M12=4時，M52必為MISS0000000000000000");
            _dicErrCodeCH.Add("D055", "M12=3時，M49減M04需>60日且<=92日");
            _dicErrCodeCH.Add("D056", "D02=1時，M23須為0或A或B或6(排除D14=HD、ID、IA、IE、IM、IV、IP、IPLE、ICV、IS、IT、IVA、IV、IVI、IVP、LA、LI、SC、SCI、VAG)");
            _dicErrCodeCH.Add("D057", "D06為PCRP-COVID19、PCRN-COVID19、RPCRPCOVID19、FSTP-COVID19、FSTN-COVID19、RFSTPCOVID19、HSTP-COVID19、RHSTPCOVID19、HPCP-COVID19、RHPCPCOVID19、CV19-S-Ab-N、CV19-S-Ab-P時，H00限5");
            _dicErrCodeCH.Add("D058", "M05或M17之院所代號第7、8、9碼有G~Z英文字母者，本署刻正處理其就醫識別碼解碼問題，待修正後會重新檢核貴機構資料。造成不便，尚祈見諒！");
            _dicErrCodeCH.Add("D059", "補卡案件，依醫療辦法第14條，未攜帶健保卡就醫，不得開立慢性病連續處方箋");
            _dicErrCodeCH.Add("D060", "M49需小於M11");
            _dicErrCodeCH.Add("D061", "H00=5時，D06限PCRP-COVID19、PCRN-COVID19、RPCRPCOVID19、FSTP-COVID19、FSTN-COVID19、RFSTPCOVID19、HSTP-COVID19、RHSTPCOVID19、HPCP-COVID19、RHPCPCOVID19、CV19-S-Ab-N、CV19-S-Ab-P");
            _dicErrCodeCH.Add("D062", "M24、M025、M26、M027、M28、M029>0時MB2需有對應的處方種類(D04)");
            _dicErrCodeCH.Add("D063", "減少之聯次非該院調劑，不可處理");
            _dicErrCodeCH.Add("D064", "D04=A時，M24需>0;D04=B時，M25需>0;D04=C時，M26需＞0;D04=D時，M27需>0，D04=E時，M28需>0;D04=F時，M29需>0");
            _dicErrCodeCH.Add("D065", "當處方醫師釋出處方之藥品(D02=1或M)不可被替代時，於此欄填入「N」，無上述情況免填。處方箋藥品未註明不可替代，藥局之藥品未備或缺乏時，藥師執行調劑業務，以同成分、同含量、同劑量或同劑型之其他廠牌藥品替代時(D05=A時)，於此欄填入「Y」，非上述情境免填。");
            _dicErrCodeCH.Add("D066", "醫事類別錯誤，請參照註1-2");
            _dicErrCodeCH.Add("D067", "M18=ICND時，M16、M17、M19不可填");
            _dicErrCodeCH.Add("D068", "M15異常就醫識別碼「SM000000000000000000」使用限格式2.0上線前之就醫紀錄");
            _dicErrCodeCH.Add("D069", "此就醫識別碼編碼服務之就醫識別碼不可使用於H01=A-正常上傳");
            _dicErrCodeCH.Add("D070", "此就醫識別碼編碼服務之就醫識別碼不可使用於H01=B-異常上傳");
            _dicErrCodeCH.Add("D071", "此就醫識別碼編碼服務之就醫識別碼不可使用於H01=A-正常上傳");
            _dicErrCodeCH.Add("D072", "此就醫識別碼編碼服務之就醫識別碼不可使用於M52");
            _dicErrCodeCH.Add("D073", "此處方聯次已被註銷不可執行");
            _dicErrCodeCH.Add("D074", "就醫識別碼有誤，無法執行處方狀態檢查");
            _dicErrCodeCH.Add("D075", "新特約(G000)之就醫識別碼反解之就醫日期時間欄位不合理。");
            _dicErrCodeCH.Add("D076", "就醫類別AC之就醫識別碼反解之就醫日期時間欄位不合理。");
            _dicErrCodeCH.Add("E001", "當E01有值時，E02、E03及E04其中之一需有值。");
            _dicErrCodeCH.Add("E002", "當E01=D「刪除」，需檢核為同病人(M03)、同院所(M05)、原就診日期時間(M11)、原就醫識別碼(M15)及前次新增過敏藥物資料(E02、E03或E04)任一欄位，始得刪除。");
            _dicErrCodeCH.Add("E003", "E02、E03及E04僅可有一欄有值。");
            _dicErrCodeCH.Add("E004", "當E02、E03及E04其中之一有值，此欄為必填欄位。");
            _dicErrCodeCH.Add("E005", "當E08=99「其他來源」，此欄為必填欄位。");
            _dicErrCodeCH.Add("E006", "當E01=D「刪除」，此欄為必填欄位。");
            _dicErrCodeCH.Add("E007", "當E11=9「其他」，此欄為必填欄位。");
            _dicErrCodeCH.Add("E008", "當E02=12196B時，本欄筆填");
            _dicErrCodeCH.Add("E010", "(曾)具健保身分民眾，M13不可為FORE");
            _dicErrCodeCH.Add("E011", "M13限CV19、FORE");
            _dicErrCodeCH.Add("E012", "M07限CA");
            _dicErrCodeCH.Add("Y000", "已用2.0格式上傳，不可再用1.0格式上傳");
            _dicErrCodeCH.Add("Y001", "上傳資料內容與定義不符");
            _dicErrCodeCH.Add("Y002", "檔案表頭內容格式錯誤");
            _dicErrCodeCH.Add("Y003", "上傳內容格式檢核錯誤");
            _dicErrCodeCH.Add("Y004", "上傳的資料欄位ID檢核錯誤");
            _dicErrCodeCH.Add("Y005", "上傳格式資料層次檢核錯誤");
            _dicErrCodeCH.Add("Y006", "未有</REC>");
            _dicErrCodeCH.Add("Y007", "未有</RECS>");
            _dicErrCodeCH.Add("Y008", "H01或M07或M23資料不正確，無法檢核其餘欄位");
            _dicErrCodeCH.Add("Y009", "查無[正常上傳，H01=1]資料，不得上傳[補正上傳，H01=3]資料");
            _dicErrCodeCH.Add("Y010", "查無[異常上傳，H01=2]資料，不得上傳[異常補正上傳，H01=4]資料");
            _dicErrCodeCH.Add("Y011", "上傳IP與醫事機構檔內IP不符，請見說明進行修正。");
            _dicErrCodeCH.Add("Y012", "當就醫類別為AE（慢性病連續處方箋領藥）、AF（藥局調劑）、AG（排程檢查）或BC（急診中、住院中執行項目），則MB2（醫令段）為必填。");
            _dicErrCodeCH.Add("Y013", "就醫類別AI僅需上傳MB1段資料，修改的相關醫令，請於原就醫類別(01-09/AD)之該筆就醫資料進行增修，並重新上傳。");
            _dicErrCodeCH.Add("Y014", "非BIG5碼");
            _dicErrCodeCH.Add("Y015", "多個RECS。整批退件");
            _dicErrCodeCH.Add("Y016", "符合H1方可改版2.0");
        }

        private void LoadDt錯誤訊息總表()
        {
            _dt錯誤訊息總表 = new DataTable();
            _dt錯誤訊息總表.Columns.Add("Content");
        }

        private void LoadDt錯誤訊息明細()
        {
            _dt錯誤訊息明細 = new DataTable();
            _dt錯誤訊息明細.Columns.Add("第幾筆");
            _dt錯誤訊息明細.Columns.Add("身分證M03");
            _dt錯誤訊息明細.Columns.Add("就醫識別碼M15");
            _dt錯誤訊息明細.Columns.Add("錯誤原因");
            _dt錯誤訊息明細.Columns.Add("原始內容");
        }


        public void Get全文解析(string str)
        {
            _strTxt純文字內容 = str;
            TxtToTable錯誤訊息總表(str);


            _dt錯誤訊息明細.Clear();
            //第一筆檔案資訊
            if (_dt錯誤訊息總表.pEmpty())
            {
                return;
            }

            string TxtContent = _dt錯誤訊息總表.Rows[0].pCol("Content");
            // 使用正規表達式尋找匹配的字串
            string pattern = @"\[.*?\]";
            MatchCollection matches = Regex.Matches(TxtContent, pattern);
            if (matches.Count != 12)
            {
                //解析失敗
                return;
            }

            _str上傳日期_時間 = matches[3].pNullOrTrimTwo();
            _str檔案大小 = matches[4].pNullOrTrimTwo();
            _str實際接收筆數 = matches[5].pNullOrTrimTwo();
            _str有效明細 = matches[9].pNullOrTrimTwo();
            _str有效醫令 = matches[10].pNullOrTrimTwo();
            _str無效明細 = matches[11].pNullOrTrimTwo();

            //上傳資料細項
            int iNum = 0;
            foreach (var i in _dt錯誤訊息總表.AsEnumerable())
            {
                //第0筆是檔案基本資訊，略過不解析
                if (iNum == 0)
                {
                    iNum++;
                    continue;
                }

                TxtContent = i.pCol("Content");


                if (TxtContent.pEmpty())
                {
                    continue;
                }
                DataRow row = _dt錯誤訊息明細.NewRow();
                row["原始內容"] = TxtContent;

                //第幾筆
                // 使用正規表達式尋找匹配的字串
                pattern = @"第\[.*?\]筆";
                matches = Regex.Matches(TxtContent, pattern);

                if (matches.Count > 0)
                {
                    string str第幾筆 = matches[0].ToString();
                    str第幾筆 = str第幾筆.pReplace("第[", "", StringComparison.OrdinalIgnoreCase);
                    str第幾筆 = str第幾筆.pReplace("]筆", "", StringComparison.OrdinalIgnoreCase);

                    row["第幾筆"] = str第幾筆;
                }

                //身分證M03
                pattern = @"M03=\[.*?\]";
                matches = Regex.Matches(TxtContent, pattern);
                if (matches.Count > 0)
                {
                    string str身分證M03 = matches[0].ToString();
                    str身分證M03 = str身分證M03.pReplace("M03=[", "", StringComparison.OrdinalIgnoreCase);
                    str身分證M03 = str身分證M03.pReplace("]", "", StringComparison.OrdinalIgnoreCase);

                    row["身分證M03"] = str身分證M03;
                }


                //就醫識別碼M15
                pattern = @"M15=\[.*?\]";
                matches = Regex.Matches(TxtContent, pattern);
                if (matches.Count > 0)
                {
                    string str就醫識別碼M15 = matches[0].ToString();
                    str就醫識別碼M15 = str就醫識別碼M15.pReplace("M15=[", "", StringComparison.OrdinalIgnoreCase);
                    str就醫識別碼M15 = str就醫識別碼M15.pReplace("]", "", StringComparison.OrdinalIgnoreCase);
                    row["就醫識別碼M15"] = str就醫識別碼M15;
                }

                //錯誤原因
                // 找到錯誤原因的索引
                int errorIndex = TxtContent.IndexOf("錯誤原因:");
                if (errorIndex != -1)
                {
                    string result = TxtContent.Substring(errorIndex);
                    result = result.Replace("錯誤原因:", "");
                    result = result.Replace(" ", "");
                    result = result.Replace("\r\n", "");
                    row["錯誤原因"] = result;
                }
                _dt錯誤訊息明細.Rows.Add(row);
            }
        }

        public string Run解析(string str)
        {
            string[] arrContent = str.pSplit(";");

            string msg = string.Empty;

            foreach (var i in arrContent)
            {
                if (i.Length <= 0)
                {
                    continue;
                }

                bool IsMB2 = i.Contains("[");
                bool IsOKData = i.Contains(":");

                if (str.pEmpty())
                {
                    return "沒有內容";
                }

                if (IsOKData == false)
                {
                    return $"資料有問題\r\n{i}";
                }

                string strField = i.pSplit(":")[0];
                bool isOK = true;
                if (IsMB2)
                {
                    isOK = _dicFieldCH.ContainsKey(strField.pLeft(3));
                }
                else
                {
                    isOK = _dicFieldCH.ContainsKey(strField);
                }

                if (strField.Contains("MB2"))
                {
                    isOK = true;
                }


                if (isOK == false)
                {
                    return $"資料有問題\r\n{i}";
                }


                string strCode = i.pSplit(":")[1];
                string strResult = string.Empty;
                string strFieldCH = string.Empty;
                string strCodeCH = string.Empty;

                //OpdBasicIC
                if (IsMB2 == false)
                {
                    bool haveFieldKey = _dicFieldCH.ContainsKey(strField);
                    if (haveFieldKey)
                    {
                        strFieldCH = _dicFieldCH[strField];
                    }
                    else
                    {
                        strFieldCH = $"查無此欄位Key:[{strField}]";
                    }

                }
                else
                {
                    if (strField.Contains("MB2"))
                    {
                        strFieldCH = "MB2";
                    }
                    else
                    {
                        string tmp = strField.pLeft(3);

                        bool haveFieldKey = _dicFieldCH.ContainsKey(tmp);
                        if (haveFieldKey)
                        {
                            strFieldCH = _dicFieldCH[tmp];
                        }
                        else
                        {
                            strFieldCH = $"查無此欄位Key:[{strField}]";
                        }
                    }
                }

                bool haveCodeKey = _dicErrCodeCH.ContainsKey(strCode);
                if (haveCodeKey == false)
                {
                    strCodeCH = $"查無此ErrCode Key:[{strCode}]";
                }
                else
                {
                    strCodeCH = _dicErrCodeCH[strCode];
                }


                msg += $"{strField} - {strFieldCH}\r\n{strCode} - {strCodeCH}\r\n\r\n";
            }

            return msg;
        }


        private void TxtToTable錯誤訊息總表(string str)
        {
            _dt錯誤訊息總表.Clear();

            List<string> List未處理資料 = str.pSplit("===========================================================================").ToList();

            if (List未處理資料[0].ToString().Contains("檢核結果"))
            {
                DataRow row = _dt錯誤訊息總表.NewRow();
                row["Content"] = List未處理資料[0].ToString();
                _dt錯誤訊息總表.Rows.Add(row);
            }

            foreach (var i in List未處理資料)
            {
                string pattern = @"第\[\d+\]筆"; // \d+ 表示匹配一個或多個數字
                bool containsTarget = Regex.IsMatch(i, pattern);

                if (containsTarget)
                {
                    DataRow row = _dt錯誤訊息總表.NewRow();
                    row["Content"] = i;
                    _dt錯誤訊息總表.Rows.Add(row);
                }
            }
        }

        public void Get總排行數量(string str)
        {
            //_dt總排行比數
            _dt總排行數量 = new DataTable();
            _dt總排行數量.Columns.Add("欄位代碼");
            _dt總排行數量.Columns.Add("欄位代碼中文");
            _dt總排行數量.Columns.Add("錯誤代碼");
            _dt總排行數量.Columns.Add("錯誤代碼中文");
            _dt總排行數量.Columns.Add("Content");

            // 定義正規表達式
            string pattern = @"錯誤原因:(.*?=)";

            // 使用正規表達式搜尋
            MatchCollection matches = Regex.Matches(str, pattern, RegexOptions.Singleline);

            List<string> errorReasons = new List<string>();
            foreach (Match match in matches)
            {
                string errorReason = match.Groups[1].Value.Trim();
                errorReason = Regex.Replace(errorReason, " ", "").Trim();
                errorReason = Regex.Replace(errorReason, "\r\n", "").Trim();
                errorReason = Regex.Replace(errorReason, "=", "").Trim();
                errorReasons.Add(errorReason);
            }

            //沒有資料就離開
            if (errorReasons.Count == 0)
            {
                return;
            }

            i總共幾個錯誤原因列 = errorReasons.Count();

            _dt總排行數量.Clear();

            foreach (var i in errorReasons)
            {
                string Content = i;

                if (Content.pEmpty())
                {
                    continue;
                }

                //ex.M49:AF;M51:AA;D05[1]:AF;
                //去除最後一個字;，這樣就可以取M49:AF、M51:AA、D05[1]:AF，不會多取空白
                Content = Content.Substring(0, Content.Length - 1);

                var arrContent = Content.pSplit(";").ToList();

                foreach (var j in arrContent)
                {
                    string F欄位中文 = string.Empty;
                    string F欄位代碼 = string.Empty;
                    string F錯誤訊息中文 = string.Empty;
                    string F錯誤訊息代碼 = string.Empty;
                    Run進階解析比對字典(j, out F欄位中文, out F欄位代碼, out F錯誤訊息中文, out F錯誤訊息代碼);

                    if (F欄位中文.pAny())
                    {
                        DataRow row = _dt總排行數量.NewRow();
                        row["欄位代碼"] = F欄位代碼;
                        row["欄位代碼中文"] = F欄位中文;
                        row["錯誤代碼"] = F錯誤訊息代碼;
                        row["錯誤代碼中文"] = F錯誤訊息中文;
                        row["Content"] = $"{F欄位代碼} - {F欄位中文}\r\n{F錯誤訊息代碼} - {F錯誤訊息中文}\r\n\r\n";
                        _dt總排行數量.Rows.Add(row);
                    }
                }
            }


            if (_dt總排行數量.pAny())
            {
                _dt總排行數量 = _dt總排行數量.pGroupBy("Content");
            }
        }


        private void Run進階解析比對字典(string str, out string F欄位中文, out string F欄位代碼, out string F錯誤訊息中文, out string F錯誤訊息代碼)
        {
            F欄位中文 = "";
            F欄位代碼 = "";
            F錯誤訊息中文 = "";
            F錯誤訊息代碼 = "";

            bool IsOKData = str.Contains(":");

            if (IsOKData == false)
            {
                return;
            }

            F欄位代碼 = str.pSplit(":")[0];
            F錯誤訊息代碼 = str.pSplit(":")[1];

            bool IsMB2 = F欄位代碼.Contains("[");

            if (IsMB2 == false)
            {
                bool haveFieldKey = _dicFieldCH.ContainsKey(F欄位代碼);
                if (haveFieldKey)
                {
                    F欄位中文 = _dicFieldCH[F欄位代碼];
                }
                else
                {
                    F欄位中文 = $"查無此欄位Key:[{F欄位代碼}]";
                }
            }
            else
            {

                if (F欄位代碼.Contains("MB2"))
                {
                    F欄位中文 = "MB2";
                }
                else
                {
                    string tmp = F欄位代碼.pLeft(3);

                    bool haveFieldKey = _dicFieldCH.ContainsKey(tmp);
                    if (haveFieldKey)
                    {
                        F欄位代碼 = tmp;
                        F欄位中文 = _dicFieldCH[tmp];
                    }
                    else
                    {
                        F欄位中文 = $"查無此欄位Key:[{F欄位代碼}]";
                    }
                }
            }

            bool haveCodeKey = _dicErrCodeCH.ContainsKey(F錯誤訊息代碼);
            if (haveCodeKey == false)
            {
                F錯誤訊息中文 = $"查無此ErrCode Key:[{F錯誤訊息代碼}]";
            }
            else
            {
                F錯誤訊息中文 = _dicErrCodeCH[F錯誤訊息代碼];
            }
        }

        public string GetSQL(string M15, bool IsCreatSQL)
        {
            if (IsCreatSQL == false)
            {
                return "";
            }

            string SQL = string.Empty;
            string result = string.Empty;

            // MHData
            SQL = $"\r\n Select top 1 * From DB_OPD..IC20HMData ";
            SQL += $"\r\n where ";
            SQL += $"\r\n M15 = '{M15}'; ";
            result += SQL;

            DataTable dt = _db.executesqldt(SQL, _conn);

            if (dt.pAny())
            {
                //DDate
                string HMUUID = dt.pRowCol("UUID");
                SQL = $"\r\n\r\n Select top 100 * From DB_OPD..IC20DData ";
                SQL += $"\r\n where ";
                SQL += $"\r\n HMUUID = '{HMUUID}'; ";
                result += SQL;
            }
            else
            {
                result += "\r\n -- DDate 沒有資料";
            }

            //OpdBasicICTbl
            SQL = $"\r\n\r\n select top 100 * from DB_OPD..OpdBasicICTbl ";
            SQL += $"\r\n where ";
            SQL += $"\r\n M15 = '{M15}'; ";
            result += SQL;
            dt = _db.executesqldt(SQL, _conn);

            if (dt.pAny())
            {
                //OpdBasicICMB2Tbl
                string Date = dt.pRowCol("chOp1Date");
                string Time = dt.pRowCol("chOp1Time");
                string Room = dt.pRowCol("chOp1Room");
                string No = dt.pRowCol("intOp1No");

                SQL = $"\r\n\r\n select top 100 * from DB_OPD..OpdBasicICMB2Tbl ";
                SQL += $"\r\n where ";
                SQL += $"\r\n chOp1Date = '{Date}' And chOp1Time = '{Time}' And chOp1Room = '{Room}' And intOp1No = '{No}'; ";


                SQL += $"\r\n\r\n select top 100 * from DB_OPD..OpdBasicTbl ";
                SQL += $"\r\n where ";
                SQL += $"\r\n chOp1Date = '{Date}' And chOp1Time = '{Time}' And chOp1Room = '{Room}' And intOp1No = '{No}'; ";

                SQL += $"\r\n\r\n select top 100 * from DB_OPD..OpdOrdTbl ";
                SQL += $"\r\n where ";
                SQL += $"\r\n chOp1Date = '{Date}' And chOp1Time = '{Time}' And chOp1Room = '{Room}' And intOp1No = '{No}' and chOp4Stat<> 'DC'; ";

                SQL += $"\r\n\r\n select top 100 * from DB_OPD..OpdDrgTbl ";
                SQL += $"\r\n where ";
                SQL += $"\r\n chOp1Date = '{Date}' And chOp1Time = '{Time}' And chOp1Room = '{Room}' And intOp1No = '{No}' and chOp3Stat<> 'DC'; ";

                result += SQL;
            }
            else
            {
                result += "\r\n -- OpdBasicICMB2Tbl 沒有資料";
            }

            return result;
        }
    }
}
