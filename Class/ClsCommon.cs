using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Net.Sockets;
namespace RiMonitoring
{
    public class ClsCommon:Form
    {
        public static string _ConnectionString = ConfigurationManager.AppSettings["conString"];
        public static string MyConnectionString = ConfigurationManager.AppSettings["myConnectionString"];
        private string _Server = ConfigurationManager.AppSettings["Server"];
        private int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

        public ClsCommon()
        { 
        }

        public class AlarmValue
        {
            private float mMinVoltMono1;
            private float mMaxVoltMono1;
            private float mMinCurrentMono1;
            private float mMaxCurrentMono1;
            private float mMinVoltMono2;
            private float mMaxVoltMono2;
            private float mMinCurrentMono2;
            private float mMaxCurrentMono2;
            private float mMinVoltPoly1;
            private float mMaxVoltPoly1;
            private float mMinCurrentPoly1;
            private float mMaxCurrentPoly1;
            private float mMinVoltPoly2;
            private float mMaxVoltPoly2;
            private float mMinCurrentPoly2;
            private float mMaxCurrentPoly2;
            private float mMinVoltThin1;
            private float mMaxVoltThin1;
            private float mMinCurrentThin1;
            private float mMaxCurrentThin1;
            private float mMinVoltThin2;
            private float mMaxVoltThin2;
            private float mMinCurrentThin2;
            private float mMaxCurrentThin2;
            private float mMinVoltLVD;
            private float mMaxVoltLVD;
            private float mMinCurrentLVD;
            private float mMaxCurrentLVD;
            private float mMinVoltGrid;
            private float mMaxVoltGrid;
            private float mMinCurrentGrid;
            private float mMaxCurrentGrid;
            private float mMinPowerAC;
            private float mMaxPowerAC;
            private float mMinVoltAC;
            private float mMaxVoltAC;
            private float mMinCurrentAC;
            private float mMaxCurrentAC;
            private float mMinTemp1;
            private float mMaxTemp1;
            private float mMinTemp2;
            private float mMaxTemp2;
            private float mMinLux;
            private float mMaxLux;

            private float mMinPF;
            private float mMaxPF;
            private float mMinHumit;
            private float mMaxHumit;
            private float mMinIrr;
            private float mMaxIrr;
            //public string PageHtml
            //{
            //    get { return echoPageControl; }
            //    //set { m_PageHtml = value; }
            //}

            public float MinVoltMono1
            {
                get { return mMinVoltMono1; }
            }
            public float MaxVoltMono1
            {
                get { return mMaxVoltMono1; }
            }
            public float MinCurrentMono1
            {
                get { return mMinCurrentMono1; }
            }
            public float MaxCurrentMono1
            {
                get { return mMaxCurrentMono1; }
            }
            public float MinVoltMono2
            {
                get { return mMinVoltMono2; }
            }
            public float MaxVoltMono2
            {
                get { return mMaxVoltMono2; }
            }
            public float MinCurrentMono2
            {
                get { return mMinCurrentMono2; }
            }
            public float MaxCurrentMono2
            {
                get { return mMaxCurrentMono2; }
            }

            public float MinVoltPoly1
            {
                get { return mMinVoltPoly1; }
            }
            public float MaxVoltPoly1
            {
                get { return mMaxVoltPoly1; }
            }
            public float MinCurrentPoly1
            {
                get { return mMinCurrentPoly1; }
            }
            public float MaxCurrentPoly1
            {
                get { return mMaxCurrentPoly1; }
            }
            public float MinVoltPoly2
            {
                get { return mMinVoltPoly2; }
            }
            public float MaxVoltPoly2
            {
                get { return mMaxVoltPoly2; }
            }
            public float MinCurrentPoly2
            {
                get { return mMinCurrentPoly2; }
            }
            public float MaxCurrentPoly2
            {
                get { return mMaxCurrentPoly2; }
            }

            public float MinVoltThin1
            {
                get { return mMinVoltThin1; }
            }
            public float MaxVoltThin1
            {
                get { return mMaxVoltThin1; }
            }
            public float MinCurrentThin1
            {
                get { return mMinCurrentThin1; }
            }
            public float MaxCurrentThin1
            {
                get { return mMaxCurrentThin1; }
            }
            public float MinVoltThin2
            {
                get { return mMinVoltThin2; }
            }
            public float MaxVoltThin2
            {
                get { return mMaxVoltThin2; }
            }
            public float MinCurrentThin2
            {
                get { return mMinCurrentThin2; }
            }
            public float MaxCurrentThin2
            {
                get { return mMaxCurrentThin2; }
            }

            public float MinVoltLVD
            {
                get { return mMinVoltLVD; }
            }
            public float MaxVoltLVD
            {
                get { return mMaxVoltLVD; }
            }
            public float MinCurrentLVD
            {
                get { return mMinCurrentLVD; }
            }
            public float MaxCurrentLVD
            {
                get { return mMaxCurrentLVD; }
            }

            public float MinVoltGrid
            {
                get { return mMinVoltGrid; }
            }
            public float MaxVoltGrid
            {
                get { return mMaxVoltGrid; }
            }
            public float MinCurrentGrid
            {
                get { return mMinCurrentGrid; }
            }
            public float MaxCurrentGrid
            {
                get { return mMaxCurrentGrid; }
            }

            public float MinPowerAC
            {
                get { return mMinPowerAC; }
            }
            public float MaxPowerAC
            {
                get { return mMaxPowerAC; }
            }
            public float MinVoltAC
            {
                get { return mMinVoltAC; }
            }
            public float MaxVoltAC
            {
                get { return mMaxVoltAC; }
            }
            public float MinCurrentAC
            {
                get { return mMinCurrentAC; }
            }
            public float MaxCurrentAC
            {
                get { return mMaxCurrentAC; }
            }
            public float MaxTemp1
            {
                get { return mMaxTemp1; }
            }
            public float MinTemp1
            {
                get { return mMinTemp1; }
            }
            public float MaxTemp2
            {
                get { return mMaxTemp2; }
            }
            public float MinTemp2
            {
                get { return mMinTemp2; }
            }
            public float MaxLux
            {
                get { return mMaxLux; }
            }
            public float MinLux
            {
                get { return mMinLux; }
            }


            //Create By art 19/02/57
            public float MaxPF
            {
                get { return mMaxPF; }
            }
            public float MinPF
            {
                get { return mMinPF; }
            }
            public float MaxHumit
            {
                get { return mMaxHumit; }
            }
            public float MinHumit
            {
                get { return mMinHumit; }
            }
            public float MaxIrr
            {
                get { return mMaxIrr; }
            }
            public float MinIrr
            {
                get { return mMinIrr; }
            }


            public AlarmValue()
            {
                DataManager dataManager = new DataManager(_ConnectionString); //Edit By art 3/02/57
                //dataManager = new DataManager(_ConnectionString); // Edit by art 3/02/57
                System.Data.DataTable tb1 = dataManager.CodeReadAlarm();
                foreach (System.Data.DataRow row in tb1.Rows)
                {
                    if (ObjString(row["code"]) == "R14")
                    {
                        mMinPF = ObjFloat(row["alarm_min"]);
                        mMaxPF = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R91")
                    {
                        mMinPowerAC = ObjFloat(row["alarm_min"]);
                        mMaxPowerAC = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R92")
                    {
                        mMinVoltAC = ObjFloat(row["alarm_min"]);
                        mMaxVoltAC = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R93")
                    {
                        mMinCurrentAC = ObjFloat(row["alarm_min"]);
                        mMaxCurrentAC = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R121")
                    {
                        mMinTemp1 = ObjFloat(row["alarm_min"]);
                        mMaxTemp1 = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R122")
                    {
                        mMinHumit = ObjFloat(row["alarm_min"]);
                        mMaxHumit = ObjFloat(row["alarm_max"]);
                    }
                    if (ObjString(row["code"]) == "R128")
                    {
                        mMinIrr = ObjFloat(row["alarm_min"]);
                        mMaxIrr = ObjFloat(row["alarm_max"]);
                    }

                    // Edit By art 19/02/57

                    //if (ObjString(row["code"]) == "R12")
                    //{
                    //    mMinVoltMono1 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltMono1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R13")
                    //{
                    //    mMinCurrentMono1 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentMono1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R42")
                    //{
                    //    mMinVoltMono2 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltMono2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R43")
                    //{
                    //    mMinCurrentMono2 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentMono2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R22")
                    //{
                    //    mMinVoltPoly1 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltPoly1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R23")
                    //{
                    //    mMinCurrentPoly1 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentPoly1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R52")
                    //{
                    //    mMinVoltPoly2 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltPoly2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R53")
                    //{
                    //    mMinCurrentPoly2 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentPoly2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R32")
                    //{
                    //    mMinVoltThin1 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltThin1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R33")
                    //{
                    //    mMinCurrentThin1 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentThin1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R62")
                    //{
                    //    mMinVoltThin2 = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltThin2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R63")
                    //{
                    //    mMinCurrentThin2 = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentThin2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R72")
                    //{
                    //    mMinVoltLVD = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltLVD = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R73")
                    //{
                    //    mMinCurrentLVD = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentLVD = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R82")
                    //{
                    //    mMinVoltGrid = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltGrid = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R83")
                    //{
                    //    mMinCurrentGrid = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentGrid = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R91")
                    //{
                    //    mMinPowerAC = ObjFloat(row["alarm_min"]);
                    //    mMaxPowerAC = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R92")
                    //{
                    //    mMinVoltAC = ObjFloat(row["alarm_min"]);
                    //    mMaxVoltAC = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R93")
                    //{
                    //    mMinCurrentAC = ObjFloat(row["alarm_min"]);
                    //    mMaxCurrentAC = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R121")
                    //{
                    //    mMinTemp1 = ObjFloat(row["alarm_min"]);
                    //    mMaxTemp1 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R122")
                    //{
                    //    mMinTemp2 = ObjFloat(row["alarm_min"]);
                    //    mMaxTemp2 = ObjFloat(row["alarm_max"]);
                    //}
                    //if (ObjString(row["code"]) == "R128")
                    //{
                    //    mMinLux = ObjFloat(row["alarm_min"]);
                    //    mMaxLux = ObjFloat(row["alarm_max"]);
                    //}
                }
            }
        }

        public class ColorGraph
        {
            private int mColorMono1;
            private int mColorMono2;
            private int mColorPoly1;
            private int mColorPoly2;
            private int mColorThin1;
            private int mColorThin2;
            private int mColorLVD;
            private int mColorGrid;
            private int mColorAC;
            private int mColorTemp1;
            private int mColorTemp2;
            private int mColorLux;
            private int mColorhumid;

            public int ColorMono1
            {
                get { return mColorMono1; }
            }
            public int ColorMono2
            {
                get { return mColorMono2; }
            }
            public int ColorPoly1
            {
                get { return mColorPoly1; }
            }
            public int ColorPoly2
            {
                get { return mColorPoly2; }
            }
            public int ColorThin1
            {
                get { return mColorThin1; }
            }
            public int ColorThin2
            {
                get { return mColorThin2; }
            }
            public int ColorLVD
            {
                get { return mColorLVD; }
            }
            public int ColorGrid
            {
                get { return mColorGrid; }
            }

            public int ColorAC
            {
                get { return mColorAC; }
            }
            public int ColorTemp1
            {
                get { return mColorTemp1; }
            }
            public int ColorTemp2
            {
                get { return mColorTemp2; }
            }
            public int ColorLux
            {
                get { return mColorLux; }
            }

            public int Colorhumid
            {
                get { return mColorhumid; }
            }
           
            public ColorGraph()
            {
                DataManager dataManager = new DataManager(_ConnectionString); // Edit By art 3/02/57
                //dataManager = new DataManager(_ConnectionString); // Edit By art 3/02/57
                //System.Data.DataTable tb1 = dataManager.ReadConfigColor();// CodeReadAlarm();
                mColorMono1 = dataManager.GetLineColor("power", "10");
                mColorMono2 = dataManager.GetLineColor("power", "11");
                mColorPoly1 = dataManager.GetLineColor("power", "12");
                mColorPoly2 = dataManager.GetLineColor("power", "20");
                mColorThin1 = dataManager.GetLineColor("power", "21");
                mColorThin2 = dataManager.GetLineColor("power", "22");
                mColorLVD = dataManager.GetLineColor("power", "30");
                mColorGrid = dataManager.GetLineColor("power", "31");
                mColorAC = dataManager.GetLineColor("power", "32");
                mColorTemp1 = dataManager.GetLineColor("temperature1", "90");
                //mColorTemp2 = dataManager.GetLineColor("power", "32"); Edit By art 4/02/57
                mColorTemp2 = dataManager.GetLineColor("power", "32");
                mColorLux = dataManager.GetLineColor("irr", "90");
                mColorhumid = dataManager.GetLineColor("humid", "90");
                

            }

        }

        public class LabelAddress
        {
            private string m10;
            private string m11;
            private string m12;
            private string m20;
            private string m21;
            private string m22;
            private string m30;
            private string m31;
            private string m32;

            public string n10
            {
                get { return m10; }
            }
            public string n11
            {
                get { return m11; }
            }
            public string n12
            {
                get { return m12; }
            }
            public string n20
            {
                get { return m20; }
            }
            public string n21
            {
                get { return m21; }
            }

            public string n22
            {
                get { return m22; }
            }
            public string n30
            {
                get { return m30; }
            }
            public string n31
            {
                get { return m31; }
            }
            public string n32
            {
                get { return m32; }
            }


            public LabelAddress()
            {
                DataManager dataManager = new DataManager(_ConnectionString); // Edit By art 3/02/57
                //dataManager = new DataManager(_ConnectionString); // Edit By art 3/02/57
                //System.Data.DataTable tb1 = dataManager.ReadConfigColor();// CodeReadAlarm();
                m10 = dataManager.GetDescription("10");
                m11 = dataManager.GetDescription("11");
                m12 = dataManager.GetDescription("12");
                m20 = dataManager.GetDescription("20");
                m21 = dataManager.GetDescription("21");
                m22 = dataManager.GetDescription("22");
                m30 = dataManager.GetDescription("30");
                m31 = dataManager.GetDescription("31");
                m32 = dataManager.GetDescription("32");
            }

        }

        public static string RemoveTail(string obj)
        {
           int lastStr=obj.LastIndexOf(",");
           return (lastStr<0)?"":obj.Substring(0,lastStr);
        }
        public static string ObjString(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return "";
            else
                return obj.ToString();
        }
        public static float ObjFloat(object obj)
        {
            if (obj == DBNull.Value || obj == null || string.IsNullOrEmpty(obj.ToString()))
                return 0;
            else
                return Convert.ToSingle(obj);//.ToString();
        }
        public static Int32 ObjInt32(object obj)
        {
            if (obj == DBNull.Value || obj == null || obj == "")
                return 0;
            else
                return Convert.ToInt32(obj);
        }
        public static Int64 ObjInt64(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return 0;
            else
                return Convert.ToInt64(obj);
        }

        public static string MyDate(DateTime obj)
        {
            //if (obj == DBNull.Value || obj == null)
            //    return "";
            //else
                return obj.Year.ToString() + obj.ToString("-MM-dd");
        }
        public static string MyTime(DateTime obj)
        {
            //if (obj == DBNull.Value || obj == null)
            //    return "";
            //else
            return obj.ToString("HH:mm:ss");
        }
        public static DateTime ObjDateTime(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return DateTime.Now;
            else
                return Convert.ToDateTime(obj);
        }
  
        public void PlotGraph(DataTable _dt, int _MaxX, int _MaxY, Color _LineColor,string _x, decimal _yRatio, PaintEventArgs e)
        {
            // _x ค่าสำหรับเลือก แกน x x,x1 x สำหรับกราฟแรงดัน x1 สำหรับ ambient

            Bitmap pt = new Bitmap(1, 1);
            pt.SetPixel(0, 0, _LineColor);
            int xDummy = -1; int ylast = 0;
            int x = (_dt.Rows.Count > 0) ? ObjInt32(_dt.Rows[0][_x]) : 0;
            int Beginx = x;
            foreach (DataRow row in _dt.Rows)
            {
                int y = (int)(_MaxY - (decimal)((ObjFloat(row["val"]) < 0) ? 0 : ObjFloat(row["val"])) * _yRatio);
                x = ObjInt32(row[_x]);
                if (xDummy == x) continue;
                xDummy = x;
                if (ylast < y && x > Beginx)
                {
                    for (int i = ylast; i < y; i++)
                    {
                        e.Graphics.DrawImageUnscaled(pt, x, i);
                    }
                }
                else if (ylast > y && x > Beginx)
                {
                    for (int i = y; i < ylast; i++)
                    {
                        e.Graphics.DrawImageUnscaled(pt, x, i);
                    }
                }
                else
                    e.Graphics.DrawImageUnscaled(pt, x, y);
                //x++;
                ylast = y;
            }
        }

        public void PlotGraphTest(DataTable _dt, int _MaxX, int _MaxY, Color _LineColor, string _x, decimal _yRatio, decimal _xRatio, PaintEventArgs e)
        {
            // _x ค่าสำหรับเลือก แกน x x,x1 x สำหรับกราฟแรงดัน x1 สำหรับ ambient

            Bitmap pt = new Bitmap(1, 1);
            pt.SetPixel(0, 0, _LineColor);
            int xDummy = -1; int ylast = 0;
            int x = (int)(_MaxX - (decimal)((ObjFloat(_dt.Rows[0][_x]) < 0) ? 0 : ObjFloat(_dt.Rows[0][_x])) * _xRatio);
            //int x = (_dt.Rows.Count > 0) ? ObjInt32(_dt.Rows[0][_x]) : 0;
            int Beginx = x;
            foreach (DataRow row in _dt.Rows)
            {
                int y = (int)(_MaxY - (decimal)((ObjFloat(row["val"]) < 0) ? 0 : ObjFloat(row["val"])) * _yRatio);
                x = (int)(_MaxX - (decimal)((ObjFloat(row[_x]) < 0) ? 0 : ObjFloat(row[_x])) / _xRatio);
                //x = ObjInt32(row[_x]);
                if (xDummy == x) continue;
                xDummy = x;
                if (ylast < y && x > Beginx)
                {
                    for (int i = ylast; i < y; i++)
                    {
                        e.Graphics.DrawImageUnscaled(pt, x, i);
                    }
                }
                else if (ylast > y && x > Beginx)
                {
                    for (int i = y; i < ylast; i++)
                    {
                        e.Graphics.DrawImageUnscaled(pt, x, i);
                    }
                }
                else
                    e.Graphics.DrawImageUnscaled(pt, x, y);
                //x++;
                ylast = y;
            }
        }

        public string GetDataFromIMON(string code)
        {
            try
            {
                TcpClient client = new TcpClient(_Server, _Port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(code);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);


                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                // Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
                return responseData;// "Received: " + responseData;
            }
            catch (Exception ex)
            {
               return "-99";
            }
        }

        public string genJson(DataTable dt)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
               // sb.Append("[");

                foreach (DataRow ob in dt.Rows)
                {
                    sb.Append("{");
                    foreach (DataColumn c in ob.Table.Columns)  //loop through the columns. 
                    {
                     if(c.DataType ==typeof(string))
                        sb.AppendFormat("\"{0}\":\"{1}\",", c.ColumnName, ObjString(ob[c.Ordinal]));
                     else
                        sb.AppendFormat("\"{0}\":{1},", c.ColumnName, ObjString(ob[c.Ordinal]));
                        // Console.WriteLine(sb);
                    }
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append("}");
                }

                //sb.Remove(sb.Length - 1, 1);
                //sb.Append("]");
                return sb.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
