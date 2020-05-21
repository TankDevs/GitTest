using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardDispenserTest
{
    public partial class Form1 : Form
    {
        #region  C++动态库 DLL Import
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="deType">设备类型</param>
        /// <param name="com">串口号</param>
        /// <param name="aud">串口参数</param>
        /// <param name="reCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SetDeviceParam")]
        private static extern int CardDispenser_SetDeviceParam(String deType, String com, String aud, StringBuilder reCode);
        /// <summary>
        /// 设置日志级别
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SetDeviceTraceLevel")]
        private static extern int CardDispenser_SetDeviceTraceLevel(int level);
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="reCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_OpenDevice")]
        private static extern int CardDispenser_OpenDevice(StringBuilder reCode);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="reCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_Reset")]
        public static extern int CardDispenser_Reset(int dwTimeOut, StringBuilder pszRcCode);
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="reCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_CloseDevice")]
        private static extern int CardDispenser_CloseDevice(StringBuilder pszRcCode);
        /// <summary>
        /// 取卡机状态
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="status">返回4位状态码</param>
        /// <param name="pszRcCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_GetCardDispenserStatus")]
        private static extern int CardDispenser_GetCardDispenserStatus(int dwTimeOut, StringBuilder status, StringBuilder ReCode);
        /// <summary>
        /// 取发卡门状态
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="status">状态码</param>
        /// <param name="pszRcCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_GetDoorStatus")]
        private static extern int CardDispenser_GetDoorStatus(int dwTimeOut, ref int status, StringBuilder ReCode);
       
        /// <summary>
        /// 设置发卡盒
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SetCardBoxNum")]
        private static extern int CardDispenser_SetCardBoxNum(int boxNum, StringBuilder ReCode);
        /// <summary>
        /// 发卡
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_Dispense")]
        private static extern int CardDispenser_Dispense(int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// 吞卡
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_Capture")]
        private static extern int CardDispenser_Capture(int dwTimeOut, StringBuilder pszRcCode);
        /// <summary>
        /// 送卡至
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="position">指定送卡的位置</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_DispenseTo")]
        private static extern int CardDispenser_DispenseTo(int dwTimeOut, string position, StringBuilder ReCode);
        /// <summary>
        /// 退卡至
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="position">指定送卡的位置</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_MoveBackTo")]
        private static extern int CardDispenser_MoveBackTo(int dwTimeOut, string position, StringBuilder ReCode);
        /// <summary>
        /// 设置发卡门状态
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="mode">发卡器门状态</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SetDoor")]
        private static extern int CardDispenser_SetDoor(int dwTimeOut, int mode, StringBuilder ReCode);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sector">扇区号</param>
        /// <param name="oldKeybuf">旧密钥</param>
        /// <param name="newKeybuf">新密钥</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_ModifyKey")]
        private static extern int CardDispenser_ModifyKey(int sector, string oldKeybuf, string newKeybuf, int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// 写M1卡
        /// </summary>
        /// <param name="sector">扇区号</param>
        /// <param name="blockId">块号</param>
        /// <param name="keyBuf">A密钥的密钥值</param>
        /// <param name="inBuf">写入数据</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_WriteRecord")]
        private static extern int CardDispenser_WriteRecord(int sector, int blockId, string keyBuf, string inBuf, int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// 读M1卡
        /// </summary>
        /// <param name="sector">扇区号</param>
        /// <param name="blockId">块号</param>
        /// <param name="keyBuf">A密钥的密钥值</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outBuf">读出数据</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_ReadRecord")]
        private static extern int CardDispenser_ReadRecord(int sector, int blockId, string keyBuf, int dwTimeOut, byte[] outBuf, StringBuilder ReCode);
        /// <summary>
        /// 读M1卡HEX
        /// </summary>
        /// <param name="sector">扇区号</param>
        /// <param name="blockId">块号</param>
        /// <param name="pwdMode">密钥类型</param>
        /// <param name="keyBuf">密钥值</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outBuf">读出数据</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_ReadRecordEX")]
        private static extern int CardDispenser_ReadRecordEX(int sector, int blockId, int pwdMode, string keyBuf, int dwTimeOut, byte[] outBuf, StringBuilder ReCode);
        /// <summary>
        /// 写M1卡HEX
        /// </summary>
        /// <param name="sector">扇区号</param>
        /// <param name="blockId">块号</param>
        /// <param name="pwdMode">密钥类型</param>
        /// <param name="keyBuf">密钥值</param>
        /// <param name="inBuf">写入数据</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_WriteRecordEX")]
        private static extern int CardDispenser_WriteRecordEX(int sector, int blockId, int pwdMode, string keyBuf, string inBuf, int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// 读磁道信息
        /// </summary>
        /// <param name="trackId">磁道号</param>
        /// <param name="trackBuf">读取的磁道数据</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_ReadTrack")]
        private static extern int CardDispenser_ReadTrack(int trackId, int dwTimeOut, byte[] trackBuf, StringBuilder ReCode);
        /// <summary>
        /// 扫描条码
        /// </summary>
        /// <param name="ScanCardData">扫描读取的数据</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_Scan")]
        private static extern int CardDispenser_Scan(int dwTimeOut, byte[] ScanCardData, StringBuilder ReCode);
        /// <summary>
        /// IC卡上电
        /// </summary>
        /// <param name="protocol">备用参数</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outAtrBuf">上电返回的ATR</param>
        /// <param name="outDataLen">上电返回的ATR的长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_IcPowerOn")]
        private static extern int CardDispenser_IcPowerOn(int protocol, int dwTimeOut, byte[] outAtrBuf, ref int outDataLen, StringBuilder ReCode);
        /// <summary>
        /// IC卡下电
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_IcPowerOff")]
        private static extern int CardDispenser_IcPowerOff(int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// IC卡APDU通讯
        /// </summary>
        /// <param name="protocol">备用参数</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="inDataBuf">APDU指令</param>
        /// <param name="indataLen">APDU指令的长度</param>
        /// <param name="outDataBuf">APDU指令返回的数据</param>
        /// <param name="outDataLen">APDU指令返回的数据长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_IcExchange")]
        private static extern int CardDispenser_IcExchange(int protocol, byte[] inDataBuf, int indataLen, int dwTimeOut, byte[] outDataBuf, ref int outDataLen, StringBuilder ReCode);
        /// <summary>
        /// SAM卡上电
        /// </summary>
        /// <param name="samNum">SAM卡座号</param>
        /// <param name="protocol">备用参数</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outAtrBuf">上电返回的ATR</param>
        /// <param name="outDataLen">上电返回的ATR的长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SamPowerOn")]
        private static extern int CardDispenser_SamPowerOn(int samNum, int protocol, int dwTimeOut, byte[] outAtrBuf, ref int outDataLen, StringBuilder ReCode);
        /// <summary>
        /// SAM卡下电
        /// </summary>
        /// <param name="samNum">SAM卡座号</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SamPowerOff")]
        private static extern int CardDispenser_SamPowerOff(int samNum, int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// SAM卡APDU通讯
        /// </summary>
        /// <param name="samNum">SAM卡座号</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="inDataBuf">APDU指令</param>
        /// <param name="indataLen">APDU指令的长度</param>
        /// <param name="outDataBuf">APDU指令返回的数据</param>
        /// <param name="outDataLen">APDU指令返回的数据长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_SamExchange")]
        private static extern int CardDispenser_SamExchange(int samNum, byte[] inDataBuf, int indataLen, int dwTimeOut, byte[] outDataBuf, ref int outDataLen, StringBuilder pszRcCode);
        /// <summary>
        /// RFIC卡APDU上电
        /// </summary>
        /// <param name="protocol">备用参数</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outAtrBuf">上电返回的ATR</param>
        /// <param name="outDataLen">上电返回的ATR的长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_RFIcPowerOn")]
        private static extern int CardDispenser_RFIcPowerOn(int protocol, int dwTimeOut, byte[] outAtrBuf, ref int outDataLen, StringBuilder ReCode);
        /// <summary>
        /// RFIC卡下电
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_RFIcPowerOff")]
        private static extern int CardDispenser_RFIcPowerOff(int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// RFIC卡APDU通讯
        /// </summary>
        /// <param name="protocol">备用参数</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="inDataBuf">APDU指令</param>
        /// <param name="indataLen">APDU指令的长度</param>
        /// <param name="outDataBuf">APDU指令返回的数据</param>
        /// <param name="outDataLen">APDU指令返回的数据长度</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_RFIcExchange")]
        private static extern int CardDispenser_RFIcExchange(int protocol, byte[] inDataBuf, int indataLen, int dwTimeOut, byte[] outDataBuf, ref int outDataLen, StringBuilder ReCode);
        /// <summary>
        /// SLE4442卡/SLE4428卡验密
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="pwdData">密钥</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_MemVerifyPWD")]
        private static extern int CardDispenser_MemVerifyPWD(int dwTimeOut, string pwdData, StringBuilder ReCode);
        /// <summary>
        /// SLE4442卡/SLE4428卡修改密钥
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="pwdData">密钥</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_MemChangePWD")]
        private static extern int CardDispenser_MemChangePWD(string oldKey, string newKey, int dwTimeOut, StringBuilder ReCode);
        /// <summary>
        /// 读SLE4442卡/SLE4428卡主存贮区数据
        /// </summary>
        /// <param name="address">开始地址</param>
        /// <param name="dataLen">要读取数据的长度</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outBuf">返回的主存贮区数据</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_MemReadBlock")]
        private static extern int CardDispenser_MemReadBlock(int address, int dataLen, int dwTimeOut, byte[] outBuf, StringBuilder ReCode);
        /// <summary>
        /// 写SLE4442卡/SLE4428卡主存贮区数据
        /// </summary>
        /// <param name="address">开始地址</param>
        /// <param name="dataLen">要读取数据的长度</param>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="outBuf">返回的主存贮区数据</param>
        /// <param name="ReCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_MemWriteBlock")]
        private static extern int CardDispenser_MemWriteBlock(int address, int dataLen, int dwTimeOut, string pwdData, string inBuf, StringBuilder ReCode);
        /// <summary>
        /// 取卡机版本信息
        /// </summary>
        /// <param name="dwTimeOut">超时时间</param>
        /// <param name="status">返回版本信息</param>
        /// <param name="pszRcCode">返回码</param>
        /// <returns>System.Int32</returns>
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_GetVersion")]
        private static extern int CardDispenser_GetVersion(int dwTimeOut, StringBuilder Version, StringBuilder ReCode);

        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_RFActiveCard")]
        private static extern int CardDispenser_RFActiveCard(int dwTimeOut, StringBuilder cardNo, StringBuilder ReCode);

        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_ReadRecordHex")]
        private static extern int CardDispenser_ReadRecordHex(int sector, int blockId, int pwdMode, string keyBuf, int dwTimeOut, StringBuilder outBuf, StringBuilder ReCode);
 
        [DllImport("CardDispenser_Driver.dll", EntryPoint = "CardDispenser_WriteRecordHex")]
        private static extern int CardDispenser_WriteRecordHex(int sector, int blockId, int pwdMode, string keyBuf, string inBuf, int dwTimeOut, StringBuilder ReCode);
        
        #endregion

        #region  私有变量
        /// <summary>
        /// 超时时间
        /// </summary>
        private static int m_dwTimeOut = 5000;
        /// <summary>
        /// 返回码
        /// </summary>
        private  StringBuilder m_pszRcCode = new StringBuilder(260);
  
        #endregion
        public Form1()
        {
            InitializeComponent();
            FormLoad();
        }
        private void FormLoad()
        {
            
        }
        /// <summary>
        /// 操作记录
        /// </summary>
        /// <Param name="ret">接口返回值</Param>
        /// <returns>void</returns>
        private void Log(int ret)
        {
            txtLog.AppendText("ret:" + ret.ToString());
            txtLog.AppendText("\r\n");
            txtLog.AppendText("pszRcCode：" + m_pszRcCode.ToString());
            txtLog.AppendText("\r\n");
        }
        /// <summary>
        /// BCDs the decompress.
        /// </summary>
        /// <Param name="bytes">The bytes.</Param>
        /// <returns>System.String.</returns>
        public static string BCDDecompress(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("X02"));
            }

            return sb.ToString();
        }
        /// <summary>
        /// BCDs the decompress.
        /// </summary>
        /// <Param name="bytes">The bytes.</Param>
        /// <Param name="len">The len.</Param>
        /// <returns>System.String.</returns>
        public static string BCDDecompress(byte[] bytes, int len)
        {
            if (len > bytes.Length)
            {
                len = bytes.Length;
            }

            byte[] buf = new byte[len];
            Array.Copy(bytes, buf, len);

            return BCDDecompress(buf);
        }
        /// <summary>
        /// BCDs the compress.
        /// </summary>
        /// <Param name="str">The STR.</Param>
        /// <returns>System.Byte[][].</returns>
        /// <exception cref="System.Exception">N类型的域只允许包含数字0-9</exception>
        public static byte[] BCDCompress(string str)
        {
            foreach (var c in str)
            {
                string digit = "0123456789ABCDEF";
                if (digit.IndexOf(c) == -1)
                    throw new Exception("N类型的域只允许包含数字0-9");
            }
            if (str.Length % 2 == 1)
                str = "0" + str;

            byte[] enc = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i = i + 2)
            {
                enc[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            }

            return enc;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }

        private void btnSetDeviceParam_Click(object sender, EventArgs e)
        {
            int ret = CardDispenser_SetDeviceParam(comboxType.Text, comboxPort.Text, txtPortParam.Text, m_pszRcCode);
            Log(ret);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int ret = CardDispenser_OpenDevice(m_pszRcCode);
            Log(ret);
        }

        private void btnSetTraceLevel_Click(object sender, EventArgs e)
        {
            int traceLevel = 0;
            try
            {
                traceLevel = int.Parse(txtTraceLevel.Text, System.Globalization.NumberStyles.HexNumber);
                int ret = CardDispenser_SetDeviceTraceLevel(traceLevel);
                txtLog.AppendText("ret:" + ret.ToString());
                txtLog.AppendText("\r\n");
              
            }
            catch (Exception ex)
            {
                txtLog.AppendText(ex.Message); 
                txtLog.AppendText("\r\n");
            }
            
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_Reset(m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int ret = CardDispenser_CloseDevice(m_pszRcCode);
            Log(ret);
        }

        private void btnGetCardDispenserStatus_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            StringBuilder status = new StringBuilder(4);
            int ret = CardDispenser_GetCardDispenserStatus(m_dwTimeOut, status, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("发卡机状态为：" + status.ToString());
                txtLog.AppendText("\r\n");
            }
        }

        private void btnGetDoorStatus_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int status = 1;
            int ret = CardDispenser_GetDoorStatus(m_dwTimeOut, ref status, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("发卡机门状态为：" + status.ToString());
                txtLog.AppendText("\r\n");
            }

        }

        private void btnDispense_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_Dispense(m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnDispenseTo_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);

            string sendStatus = comboxSend.Text;
            int pos = sendStatus.IndexOf("(");
            if (pos != -1)
            {
                sendStatus = sendStatus.Substring(0, pos);
            }

            int ret = CardDispenser_DispenseTo(m_dwTimeOut, sendStatus, m_pszRcCode);
            Log(ret);
        }

        private void btnMoveBackTo_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);

            string moveBackStatus = comboxMoveBack.Text;
            int pos = moveBackStatus.IndexOf("(");
            if (pos != -1)
            {
                moveBackStatus = moveBackStatus.Substring(0, pos);
            }

            int ret = CardDispenser_MoveBackTo(m_dwTimeOut, moveBackStatus, m_pszRcCode);
            Log(ret);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_Capture(m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnSetDoor_Click(object sender, EventArgs e)
        {
            int doorStatus = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            string setDoorStatus = comboxSetDoor.Text;
            int pos = setDoorStatus.IndexOf("(");
            if (pos != -1)
            {
                setDoorStatus = setDoorStatus.Substring(0, pos);
            }
            int.TryParse(setDoorStatus, out doorStatus);
            int ret = CardDispenser_SetDoor(m_dwTimeOut, doorStatus, m_pszRcCode);
            Log(ret);
        }

        private void btnReadRecord_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            var outBuf = new byte[260];
            int ret = CardDispenser_ReadRecord(sector, blockid, txtKeybuf.Text, m_dwTimeOut, outBuf, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("块数据：" + System.Text.Encoding.Default.GetString(outBuf));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnWriteRecord_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            int ret = CardDispenser_WriteRecord(sector, blockid, txtKeybuf.Text, txtInbuf.Text, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnReadRecordEX_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int pwdmode = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            int.TryParse(txtPwdmode.Text, out pwdmode);
            var outBuf = new byte[260];
            int ret = CardDispenser_ReadRecordEX(sector, blockid, pwdmode, txtKeybuf.Text, m_dwTimeOut, outBuf, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("块数据：" + System.Text.Encoding.Default.GetString(outBuf));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnWriteRecordEX_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int pwdmode = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            int.TryParse(txtPwdmode.Text, out pwdmode);
            int ret = CardDispenser_WriteRecordEX(sector, blockid, pwdmode, txtKeybuf.Text, txtInbuf.Text, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnModifyKey_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int ret = CardDispenser_ModifyKey(sector, txtKeybuf.Text, txtNewKeybuf.Text, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnReadTrack_Click(object sender, EventArgs e)
        {
            int trackid = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtTrackid.Text, out trackid);
            var trackBuf = new byte[260];
            int ret = CardDispenser_ReadTrack(trackid, m_dwTimeOut, trackBuf, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("磁道数据：" + System.Text.Encoding.Default.GetString(trackBuf));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var scanBuf = new byte[260];
            int ret = CardDispenser_Scan(m_dwTimeOut, scanBuf, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("条码数据：" + System.Text.Encoding.Default.GetString(scanBuf));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnIcPowerOn_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var outAtrBuf = new byte[260];
            int outDataLen = -1;
            int ret = CardDispenser_IcPowerOn(0, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("IC上电返回ATR：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnIcExchange_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var outAtrBuf = new byte[260];
            int outDataLen = 0;
            var inBuf = BCDCompress(txtApdu.Text);
            int ret = CardDispenser_IcExchange(0, inBuf, inBuf.Length, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("IC卡通讯返回APDU：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnIcPowerOff_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_IcPowerOff(m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnSamPowerOn_Click(object sender, EventArgs e)
        {
            var outAtrBuf = new byte[260];
            int samNum = -1;
            int outDataLen = 0;
            int.TryParse(txtSamNum.Text, out samNum);
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_SamPowerOn(samNum, 0, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("SAM上电返回ATR：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnSamExchange_Click(object sender, EventArgs e)
        {
            var outAtrBuf = new byte[260];
            int outDataLen = 0;
            int samNum = -1;
            int.TryParse(txtSamNum.Text, out samNum);
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var inBuf = BCDCompress(txtApdu.Text);
            int ret = CardDispenser_SamExchange(samNum, inBuf, inBuf.Length, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("SAM卡通讯返回APDU：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnSamPowerOff_Click(object sender, EventArgs e)
        {
            int samNum = -1;
            int.TryParse(txtSamNum.Text, out samNum);
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_SamPowerOff(samNum, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnRFIcPowerOn_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var outAtrBuf = new byte[260];
            int outDataLen = -1;
            int ret = CardDispenser_RFIcPowerOn(0, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("RFIC上电返回ATR：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnRFIcExchange_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            var outAtrBuf = new byte[260];
            int outDataLen = 0;
            var inBuf = BCDCompress(txtApdu.Text);
            int ret = CardDispenser_RFIcExchange(0, inBuf, inBuf.Length, m_dwTimeOut, outAtrBuf, ref outDataLen, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("RFIC卡通讯返回APDU：" + BCDDecompress(outAtrBuf, outDataLen));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnRFIcPowerOff_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_RFIcPowerOff(m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnMemVerifyPWD_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            //var inBuf = BCDCompress(txtPwdM.Text);
            int ret = CardDispenser_MemVerifyPWD(m_dwTimeOut, txtPwdM.Text, m_pszRcCode);
            Log(ret);
        }

        private void btnMemReadBlock_Click(object sender, EventArgs e)
        {
            int startAddress = -1;
            int dataLen = -1;
            var outBuf = new byte[260];
            int.TryParse(txtAddress.Text, out startAddress);
            int.TryParse(txtBufLenth.Text, out dataLen);
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_MemReadBlock(startAddress, dataLen, m_dwTimeOut, outBuf, m_pszRcCode);
            Log(ret);
            if(ret == 0)
            {
                txtLog.AppendText("主存贮区数据：" + System.Text.Encoding.Default.GetString(outBuf));
                txtLog.AppendText("\r\n");
            }
        }

        private void btnMemWriteBlock_Click(object sender, EventArgs e)
        {
            int startAddress = -1;
            int dataLen = -1;
            var outBuf = new byte[260];
            int.TryParse(txtAddress.Text, out startAddress);
            int.TryParse(txtBufLenth.Text, out dataLen);
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_MemWriteBlock(startAddress, dataLen, m_dwTimeOut, txtPwdM.Text, txtInbufM.Text, m_pszRcCode);
            Log(ret);
        }

        private void btnMemChangePWD_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int ret = CardDispenser_MemChangePWD(txtPwdM.Text, txtNewPwdM.Text, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void btnGetVersion_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            StringBuilder version = new StringBuilder(260);
            int ret = CardDispenser_GetVersion(m_dwTimeOut, version, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("版本信息为：" + version.ToString());
                txtLog.AppendText("\r\n");
            }
           
        }

        private void RFActiveCard_Click(object sender, EventArgs e)
        {
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            StringBuilder cardNo = new StringBuilder(260);
            int ret = CardDispenser_RFActiveCard(m_dwTimeOut, cardNo, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("序列号：" + cardNo.ToString());
                txtLog.AppendText("\r\n");
            }
        }

        private void btnReadRecordHex_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int pwdmode = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            int.TryParse(txtPwdmode.Text, out pwdmode);
            StringBuilder outHex = new StringBuilder(260);
            int ret = CardDispenser_ReadRecordHex(sector, blockid, pwdmode, txtKeybuf.Text, m_dwTimeOut, outHex, m_pszRcCode);
            Log(ret);
            if (ret == 0)
            {
                txtLog.AppendText("块数据(HEX)：" + outHex);
                txtLog.AppendText("\r\n");
            }
        }

        private void btnWriteRecordHex_Click(object sender, EventArgs e)
        {
            int sector = -1;
            int blockid = -1;
            int pwdmode = -1;
            int.TryParse(txtTimeOut.Text, out m_dwTimeOut);
            int.TryParse(txtSector.Text, out sector);
            int.TryParse(txtBlockid.Text, out blockid);
            int.TryParse(txtPwdmode.Text, out pwdmode);
            int ret = CardDispenser_WriteRecordHex(sector, blockid, pwdmode, txtKeybuf.Text, txtInbuf.Text, m_dwTimeOut, m_pszRcCode);
            Log(ret);
        }

        private void buttonSetCardBoxNum_Click(object sender, EventArgs e)
        {
            int cardBoxNum = -1;
            int.TryParse(comboBoxCardBoxNum.Text, out cardBoxNum);
            int ret = CardDispenser_SetCardBoxNum(cardBoxNum, m_pszRcCode);
            Log(ret);
        }   
        private void testFunction(object sender, EventArgs e)
        {
            int cardBoxNum = -1;
            int.TryParse(comboBoxCardBoxNum.Text, out cardBoxNum);
            int ret = CardDispenser_SetCardBoxNum(cardBoxNum, m_pszRcCode);
            Log(ret);
        } 		
    }
}
