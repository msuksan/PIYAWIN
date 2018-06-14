using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.IO;

//using MySql.Data.MySqlClient;

namespace RiMonitoring
{
    public partial class frmMonitor : ClsCommon
    {
        frmGraph1 frmCurve;
        frmGraph2 frmBar;
        frmDataText frmView;

        // for blink value
        private int i=0;
        Color BackColorNormal;
        Color ForeColorNormal;
        Color BackColorAlarm;
        Color ForeColorAlarm;

        private bool AlertVoltMono1 = false;
        private bool AlertCurrentMono1 = false;
        private bool AlertVoltMono2 = false;
        private bool AlertCurrentMono2 = false;
        private bool AlertVoltPoly1 = false;
        private bool AlertCurrentPoly1 = false;
        private bool AlertVoltPoly2 = false;
        private bool AlertCurrentPoly2 = false;
        private bool AlertVoltThin1 = false;
        private bool AlertCurrentThin1 = false;
        private bool AlertVoltThin2 = false;
        private bool AlertCurrentThin2 = false;
        private bool AlertVoltLVD = false;
        private bool AlertCurrentLVD = false;
        private bool AlertVoltGrid = false;
        private bool AlertCurrentGrid = false;
        private bool AlertVoltAC = false;
        private bool AlertCurrentAC = false;
        private bool AlertPowerAC = false;
        private bool AlertLux = false;




        ArrayList arrObj = new ArrayList();

        DataManager dataManager;
        AlarmValue al;
        public frmMonitor()
        {
            InitializeComponent();
            // timer config
            DataTimer.Interval = int.Parse(ConfigurationManager.AppSettings["RefreshTimer"]) * 1000;

            dataManager = new DataManager(_ConnectionString);
            //dataManager.RepairDataLogger();

            arrObj.Clear();
            BlinkTimer.Stop();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
            if (arrObj.Count>0)
            {
                BlinkTimer.Start();
            }

            foreach(Form a in Application.OpenForms)
            {
                if (a.Name=="frmGraph1" && frmCurve.cbAuto.Checked) frmCurve.btRefresh_Click(sender, e);
                if (a.Name == "frmGraph2" && frmBar.cbAuto.Checked) frmBar.btRefresh_Click(sender, e);
                if (a.Name == "frmDataText" && frmView.cbAuto.Checked) frmView.ReloadData();
            }
     
        }

        private void LoadData()
        {
            try
            {
                DataRow row = dataManager.RetriveNowData("01");
                if (row != null)
                {
                    //AlertPowerAC = (ObjFloat(row["power"]) < al.MinPowerAC || ObjFloat(row["power"]) > al.MaxPowerAC) ? true : false;
                    //lbPowerAC.BackColor = (AlertPowerAC) ? BackColorAlarm : BackColorNormal;
                    //lbPowerAC.ForeColor = (AlertPowerAC) ? ForeColorAlarm : ForeColorNormal;
                    //lbPowerAC.Text = ObjString(row["power"]);

                    //AlertCurrentAC = (ObjFloat(row["current"]) < al.MinCurrentAC || ObjFloat(row["current"]) > al.MaxCurrentAC) ? true : false;
                    //lbCurrentAC.BackColor = (AlertCurrentAC) ? BackColorAlarm : BackColorNormal;
                    //lbCurrentAC.ForeColor = (AlertCurrentAC) ? ForeColorAlarm : ForeColorNormal;
                    //lbCurrentAC.Text = ObjString(row["current"]);

                    AlertVoltAC = (ObjFloat(row["VG"]) < al.MinVoltAC || ObjFloat(row["VG"]) > al.MaxVoltAC) ? true : false;
                    lbVG.BackColor = (AlertVoltAC) ? BackColorAlarm : BackColorNormal;
                    lbVG.ForeColor = (AlertVoltAC) ? ForeColorAlarm : ForeColorNormal;
                    lbVG.Text = ObjString(row["VG"]);


                    lbIG.Text = ObjString(row["IG"]);
                    lbPG.Text = ObjString(row["PG"]);
                    lbFG.Text = ObjString(row["FG"]);
                    lbDWH.Text = ObjString(row["DWH"]);
                    lbTWH.Text = ObjString(row["TWH"]);

                    lbVIN1.Text = ObjString(row["VIN1"]);
                    lbIIN1.Text = ObjString(row["IIN1"]);
                    lbPIN1.Text = ObjString(row["PIN1"]);

                    lbVIN2.Text = ObjString(row["VIN2"]);
                    lbIIN2.Text = ObjString(row["IIN2"]);
                    lbPIN2.Text = ObjString(row["PIN2"]);

                    lbDate.Text = ObjString(row["Rectime"]);
                }
/*
                row = dataManager.RetriveNowData("90");
                if (row != null)
                {
                    lbTemp1.BackColor = (AlertVoltPoly1) ? BackColorAlarm : BackColorNormal;
                    lbTemp1.ForeColor = (AlertVoltPoly1) ? ForeColorAlarm : ForeColorNormal;
                    lbTemp1.Text = ObjString(row["temperature1"]);

                    AlertVoltPoly1 = (ObjFloat(row["temperature2"]) < al.MinVoltPoly1 || ObjFloat(row["temperature2"]) > al.MaxVoltPoly1) ? true : false;
                    lbTemp2.BackColor = (AlertVoltPoly1) ? BackColorAlarm : BackColorNormal;
                    lbTemp2.ForeColor = (AlertVoltPoly1) ? ForeColorAlarm : ForeColorNormal;
                    lbTemp2.Text = ObjString(row["temperature2"]);

                    AlertCurrentPoly1 = (ObjFloat(row["humid1"]) < al.MinCurrentPoly1 || ObjFloat(row["humid1"]) > al.MaxCurrentPoly1) ? true : false;
                    lbHumid1.BackColor = (AlertCurrentPoly1) ? BackColorAlarm : BackColorNormal;
                    lbHumid1.ForeColor = (AlertCurrentPoly1) ? ForeColorAlarm : ForeColorNormal;
                    lbHumid1.Text = ObjString(row["humid1"]);

                    lbIrr1.Text = ObjString(row["irr1"]);
                    if (ObjString(row["control_code"]) != "0") errorPvd.SetError(lbTemp2, "Error: " + ObjString(row["remark"]));
                    else errorPvd.SetError(lbTemp2, "");
                }
               
                row = dataManager.RetriveNowData("10");
                if (row != null)
                {

                    AlertPowerAC = (ObjFloat(row["power"]) < al.MinPowerAC || ObjFloat(row["power"]) > al.MaxPowerAC) ? true : false;
                    lbPowerAC.BackColor = (AlertPowerAC) ? BackColorAlarm : BackColorNormal;
                    lbPowerAC.ForeColor = (AlertPowerAC) ? ForeColorAlarm : ForeColorNormal;
                    lbPowerAC.Text = ObjString(row["power"]);

                    AlertVoltAC = (ObjFloat(row["voltage"]) < al.MinVoltAC || ObjFloat(row["voltage"]) > al.MaxVoltAC) ? true : false;
                    lbVoltAC.BackColor = (AlertVoltAC) ? BackColorAlarm : BackColorNormal;
                    lbVoltAC.ForeColor = (AlertVoltAC) ? ForeColorAlarm : ForeColorNormal;
                    lbVoltAC.Text = ObjString(row["voltage"]);

                    AlertCurrentAC = (ObjFloat(row["current"]) < al.MinCurrentAC || ObjFloat(row["current"]) > al.MaxCurrentAC) ? true : false;
                    lbCurrentAC.BackColor = (AlertCurrentAC) ? BackColorAlarm : BackColorNormal;
                    lbCurrentAC.ForeColor = (AlertCurrentAC) ? ForeColorAlarm : ForeColorNormal;
                    lbCurrentAC.Text = ObjString(row["current"]);

                    lbPF.Text = ObjString(row["pf"]);
                    lbFrequency.Text = ObjString(row["frequency"]);


                    if (ObjString(row["control_code"]) != "0") errorPvd.SetError(lbVoltAC, "Error: " + ObjString(row["remark"]));
                    else errorPvd.SetError(lbVoltAC, "");

                    lbDate.Text = "data update: " + ObjDateTime(row["create_date"]).ToString("d/MM/yyyy H:mm");

                }
                row = dataManager.RetriveNowData("11");
                if (row != null)
                {

                    AlertPowerAC = (ObjFloat(row["power"]) < al.MinPowerAC || ObjFloat(row["power"]) > al.MaxPowerAC) ? true : false;
                    lbPower2.BackColor = (AlertPowerAC) ? BackColorAlarm : BackColorNormal;
                    lbPower2.ForeColor = (AlertPowerAC) ? ForeColorAlarm : ForeColorNormal;
                    lbPower2.Text = ObjString(row["power"]);

                    AlertVoltAC = (ObjFloat(row["voltage"]) < al.MinVoltAC || ObjFloat(row["voltage"]) > al.MaxVoltAC) ? true : false;
                    lbVolt2.BackColor = (AlertVoltAC) ? BackColorAlarm : BackColorNormal;
                    lbVolt2.ForeColor = (AlertVoltAC) ? ForeColorAlarm : ForeColorNormal;
                    lbVolt2.Text = ObjString(row["voltage"]);

                    AlertCurrentAC = (ObjFloat(row["current"]) < al.MinCurrentAC || ObjFloat(row["current"]) > al.MaxCurrentAC) ? true : false;
                    lbAmp2.BackColor = (AlertCurrentAC) ? BackColorAlarm : BackColorNormal;
                    lbAmp2.ForeColor = (AlertCurrentAC) ? ForeColorAlarm : ForeColorNormal;
                    lbAmp2.Text = ObjString(row["current"]);

                    lbPF2.Text = ObjString(row["pf"]);
                    lbFreq2.Text = ObjString(row["frequency"]);

                    if (ObjString(row["control_code"]) != "0") errorPvd.SetError(lbVolt2, "Error: " + ObjString(row["remark"]));
                    else errorPvd.SetError(lbVoltAC, "");

                }
                

                row = dataManager.RetriveNowData("12");
                if (row != null)
                {

                    AlertPowerAC = (ObjFloat(row["power"]) < al.MinPowerAC || ObjFloat(row["power"]) > al.MaxPowerAC) ? true : false;
                    lbPower3.BackColor = (AlertPowerAC) ? BackColorAlarm : BackColorNormal;
                    lbPower3.ForeColor = (AlertPowerAC) ? ForeColorAlarm : ForeColorNormal;
                    lbPower3.Text = ObjString(row["power"]);

                    AlertVoltAC = (ObjFloat(row["voltage"]) < al.MinVoltAC || ObjFloat(row["voltage"]) > al.MaxVoltAC) ? true : false;
                    lbVolt3.BackColor = (AlertVoltAC) ? BackColorAlarm : BackColorNormal;
                    lbVolt3.ForeColor = (AlertVoltAC) ? ForeColorAlarm : ForeColorNormal;
                    lbVolt3.Text = ObjString(row["voltage"]);

                    AlertCurrentAC = (ObjFloat(row["current"]) < al.MinCurrentAC || ObjFloat(row["current"]) > al.MaxCurrentAC) ? true : false;
                    lbAmp3.BackColor = (AlertCurrentAC) ? BackColorAlarm : BackColorNormal;
                    lbAmp3.ForeColor = (AlertCurrentAC) ? ForeColorAlarm : ForeColorNormal;
                    lbAmp3.Text = ObjString(row["current"]);

                    lbPF3.Text = ObjString(row["pf"]);
                    lbFreq3.Text = ObjString(row["frequency"]);

                    if (ObjString(row["control_code"]) != "0") errorPvd.SetError(lbVolt3, "Error: " + ObjString(row["remark"]));
                    else errorPvd.SetError(lbVoltAC, "");

                }
               */

            }
            catch (Exception ex)
            {
                DataTimer.Stop();
                MessageBox.Show("Error Loading!!" + ex.Message, "มีข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataTimer.Start();
                //throw (ex);
            }
        }
        private void SetTextColor()
        {
            DataRow drow = dataManager.ReadConfigColor();
            if (drow != null)
            {
                BackColorNormal = Color.FromArgb(ObjInt32(drow["backcolor_normal"]));
                ForeColorNormal = Color.FromArgb(ObjInt32(drow["forecolor_normal"]));
                BackColorAlarm = Color.FromArgb(ObjInt32(drow["backcolor_alarm"]));
                ForeColorAlarm = Color.FromArgb(ObjInt32(drow["forecolor_alarm"]));
            }
        
        }
              
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            lbDay.Text = DateTime.Now.ToString("d-MM-yyyy");
            lbTime.Text = DateTime.Now.ToString("HH:mm:ss");

            if (((DateTime.Now.Day % 4) == 0) && (lbTime.Text.Trim() == "22:05:00"))
            {
                try
                {
                    string path = ConfigurationManager.AppSettings["DataPathFile"], path2 = ConfigurationManager.AppSettings["BackupPathFile"];
                    path2 = path2 + "\\dbgi" + DateTime.Now.ToString("yyMMdd-HHmmss");

                    Directory.CreateDirectory(path2);
                    //File.Copy(path, path2);
                    string[] files = Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        File.Copy(file, path2 + "\\" + file.Substring(file.LastIndexOf('\\')));
                    }

                    dataManager.DeleteRawDetail();
                    //MessageBox.Show("การดำเนินการเสร็จ !! ไฟล์แบคอัพคือ" + path2, "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("มีข้อผิดพลาด !! ไม่สามารถ Backup ได้: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            if (i++ >= 16)// || obj=="")
            {
                //if (idx < arrObj.Count)
                //{
                //    obj = arrObj[idx++].ToString();
                //}
                //else
                //{
                    BlinkTimer.Stop();
                    //obj = "";
                    //idx = 0;
                    arrObj.Clear();
                //}
                i = 0;
            }
            //obj = arrObj[idx++].ToString();
            foreach(string obj in  arrObj)
                this.select_blink(obj);
        }

        private void select_blink(string obj)
        {

            switch (obj)
            {
                
                case "volt_poly1":
                    lbTemp2.ForeColor = (lbTemp2.ForeColor == ForeColorNormal) ? BackColorNormal : ForeColorNormal;
                    break;
                case "current_poly1":
                    lbHumid1.ForeColor = (lbHumid1.ForeColor == ForeColorNormal) ? BackColorNormal : ForeColorNormal;
                    break;
                case "volt_ac":
                    lbVG.ForeColor = (lbVG.ForeColor == ForeColorNormal) ? BackColorNormal : ForeColorNormal;
                    break;
                case "current_ac":
                    lbIG.ForeColor = (lbIG.ForeColor == ForeColorNormal) ? BackColorNormal : ForeColorNormal;
                    break;
                case "power_ac":
                    lbPG.ForeColor = (lbPG.ForeColor == ForeColorNormal) ? BackColorNormal : ForeColorNormal;
                    break;
               
            }

        }

        private void lbVoteMono1_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltMono1) arrObj.Add("volt_mono1");
            else arrObj.Remove("volt_mono1");
        }

        private void lbCurrentMono1_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentMono1) arrObj.Add("current_mono1");
            else arrObj.Remove("current_mono1");
        }
        private void lbVoltMono2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltMono2) arrObj.Add("volt_mono2");
            else arrObj.Remove("volt_mono2");
        }

        private void lbCurrentMono2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentMono2) arrObj.Add("current_mono2");
            else arrObj.Remove("current_mono2");
        }
        private void lbTemp2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltPoly1) arrObj.Add("volt_poly1");
            else arrObj.Remove("volt_poly1");
        }
        private void lbHumid1_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentPoly1) arrObj.Add("current_poly1");
            else arrObj.Remove("current_poly1");
        }
        private void lbVoltPoly2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltPoly2) arrObj.Add("volt_poly2");
            else arrObj.Remove("volt_poly2");
        }
        private void lbCurrentPoly2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentPoly2) arrObj.Add("current_poly2");
            else arrObj.Remove("current_poly2");
        }

        private void lbVoltThin1_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltThin1) arrObj.Add("volt_thin1");
            else arrObj.Remove("volt_thin1");
        }
        private void lbCurrentThin1_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentThin1) arrObj.Add("current_thin1");
            else arrObj.Remove("current_thin1");
        }
        private void lbVoltThin2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltThin2) arrObj.Add("volt_thin2");
            else arrObj.Remove("volt_thin2");
        }
        private void lbCurrentThin2_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentThin2) arrObj.Add("current_thin2");
            else arrObj.Remove("current_thin2");

        }
        private void lbVoltLVD_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltLVD) arrObj.Add("volt_lvd");
            else arrObj.Remove("volt_lvd");
        }

        private void lbCurrentLVD_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentLVD) arrObj.Add("current_lvd");
            else arrObj.Remove("current_lvd");
        }
        private void lbVoltGrid_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltGrid) arrObj.Add("volt_grid");
            else arrObj.Remove("volt_grid");
        }

        private void lbCurrentGrid_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentGrid) arrObj.Add("current_grid");
            else arrObj.Remove("current_grid");
        }
        private void lbVoltAC_TextChanged(object sender, EventArgs e)
        {
            if (!AlertVoltAC) arrObj.Add("volt_ac");
            else arrObj.Remove("volt_ac");
        }

        private void lbCurrentAC_TextChanged(object sender, EventArgs e)
        {
            if (!AlertCurrentAC) arrObj.Add("current_ac");
            else arrObj.Remove("current_ac");
        }

        private void lbPowerAC_TextChanged(object sender, EventArgs e)
        {
            if (!AlertPowerAC) arrObj.Add("power_ac");
            else arrObj.Remove("power_ac");
        }

        private void lbTemp_TextChanged(object sender, EventArgs e)
        {

        }
     
        private void lbLux_TextChanged(object sender, EventArgs e)
        {
           if(!AlertLux) arrObj.Add("lux");
           else arrObj.Remove("lux");
            //arrObj.Remove("lux");
        }

        private void btGraph1_Click(object sender, EventArgs e)
        {
            frmCurve = new frmGraph1();
            frmCurve.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBar = new frmGraph2();
            frmBar.ShowDialog();
        }
        private void btText_Click(object sender, EventArgs e)
        {
            frmView = new frmDataText();
            frmView.ShowDialog();

        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmData_Click(object sender, EventArgs e)     
        {
            frmCheckPrint f = new frmCheckPrint();
            f.ShowDialog();
        }

        private void CloseAllChildForm()
        {
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }

        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            menuStrip1.Visible = !menuStrip1.Visible;
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            SetTextColor();
            al = new AlarmValue();
            LoadData();
        }

      
        private void minMaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdjustAlarm f = new frmAdjustAlarm();
            f.ShowDialog();
            if (f.IsSave == "Y")
            {
                al = new AlarmValue(); ;
                LoadData();
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigAlarm f = new frmConfigAlarm();
            f.ShowDialog();
            if (f.IsSave == "Y")
            {
                SetTextColor();
                LoadData();
            }
        }

        
        private void frmMonitor_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if (y < 20) menuStrip1.Visible=true;

        }

        private void frmMonitor_MouseClick(object sender, MouseEventArgs e)
        {
            menuStrip1.Visible = false;
            //tslGraph2.Visible = true;
            if (e.Y > (this.Size.Height - 60) && e.X > (this.Size.Width - 50) && e.Button == MouseButtons.Right)
            {
                frmMonitorInstant f = new frmMonitorInstant();
                f.ShowDialog();
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmClearData f = new frmClearData();
            f.ShowDialog();
        }

        private void tsMonitor_MouseClick(object sender, MouseEventArgs e)
        {
            menuStrip1.Visible = true;
            //tslGraph2.Visible = false;
        }

        private void instantModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMonitorInstant f = new frmMonitorInstant();
            f.ShowDialog();
        }

       
        private void tsMenuRepair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการซ่อมแซมข้อมูลในฐานข้อมูล ใช่หรือไม่?", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

          

            dataManager.RepairDataLogger();//.DeleteAllRecords();
            dataManager.DeleteRawDetail();
            MessageBox.Show("การดำเนินการเสร็จ !! ", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void ConstantMenu_Click(object sender, EventArgs e)
        {
            frmConstant f = new frmConstant();
            f.ShowDialog();
            if (f.IsSave == "Y")
            {
               // SetTextColor();
                LoadData();
            }
        }

        private void tsmSummarize2_Click(object sender, EventArgs e)
        {

            frmDataEffMonth f = new frmDataEffMonth();
            f.ShowDialog();

        }


       
     }
}
