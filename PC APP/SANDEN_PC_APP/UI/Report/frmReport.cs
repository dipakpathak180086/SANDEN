using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SANDEN_BL;
using SANDEN_COMMON;
using SANDEN_PC_APP;
using SANDEN_PL;
using Excel = Microsoft.Office.Interop.Excel;

namespace SANDEN_PC_APP
{
    public partial class frmReport : Form
    {

        #region Variables

        private BL_REPORT _blObj = null;
        private PL_REPORT _plObj = null;
        private string _RPT_Type = "";
        private DataTable dtMapping = null;
        #endregion

        #region Form Methods

        public frmReport()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_REPORT();
                dtMapping = new DataTable();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void frmModelMaster_Load(object sender, EventArgs e)
        {
            try
            {
                // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, false, null, null);
                }
                lblSearchPartNo.Visible = true;
                txtPartSearch.Visible = true;
                dgv.DataSource = null;
                _RPT_Type = "";
                _RPT_Type = "FG_REPORT";


            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        #endregion

        #region Button Event
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {

                Clear();

                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, false, null, null);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtPartSearch.Text = "";
                dpFromDate.Text = dpToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                dgv.DataSource = null;

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void AllBorders(Excel.Borders _borders)
        {
            _borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            _borders.Color = Color.Black;
        }
        private void FGReportExportToExcel(DataGridView dg)
        {
            string sBarcodeFinal = "";
            try
            {

                DataTable dtBarcode = (DataTable)dg.DataSource;
                DataView dv = new DataView(dtBarcode);
                dv.RowFilter = (dgv.DataSource as DataTable).DefaultView.RowFilter;
                if (dv.Count > 0)
                {
                    dtBarcode = dv.ToTable();
                }
                DataTable dtFinal = dtBarcode.AsDataView().ToTable(true, "Parent_Barcode");

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                Excel.Range chartRange;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Rows.Font.Name = "Arial";
                xlWorkSheet.Rows.Font.Bold = true;
                xlWorkSheet.Rows.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //xlApp.StandardFontSize = 10;

                xlWorkSheet.Cells[2, 4] = "Customer:-";
                xlWorkSheet.Cells[2, 5] = "Mahindra";
                xlWorkSheet.Cells[5, 2] = "Housing QR Code";

                //Station No -01
                xlWorkSheet.Cells[4, 6] = "Ø12 Cup Gluing & Pressing";
                xlWorkSheet.get_Range("F4", "O4").Merge(true);
                xlWorkSheet.Cells[5, 6] = "Date and Time";
                xlWorkSheet.Cells[5, 7] = "Ø12 Cup pkt BOP.";
                xlWorkSheet.Cells[5, 8] = "Air Pressure";
                xlWorkSheet.get_Range("H5", "J5").Merge(true);
                xlWorkSheet.Cells[6, 8] = "Min";
                xlWorkSheet.Cells[6, 9] = "Max";
                xlWorkSheet.Cells[6, 10] = "Actual";
                xlWorkSheet.Cells[5, 11] = "Interference Load";
                xlWorkSheet.get_Range("K5", "M5").Merge(true);
                xlWorkSheet.Cells[6, 11] = "Min";
                xlWorkSheet.Cells[6, 12] = "Max";
                xlWorkSheet.Cells[6, 13] = "Actual";
                xlWorkSheet.Cells[5, 14] = "Gluing Confirmation";
                xlWorkSheet.Cells[5, 15] = "Result";


                //Station No -02
                xlWorkSheet.Cells[4, 16] = "Ø26 Cup Gluing & Pressing";
                xlWorkSheet.get_Range("P4", "Y4").Merge(true);
                xlWorkSheet.Cells[5, 16] = "Date and Time";
                xlWorkSheet.Cells[5, 17] = "BOP. of  pkt Ø26 Cup";
                xlWorkSheet.Cells[5, 18] = "Air Pressure";
                xlWorkSheet.get_Range("R5", "T5").Merge(true);
                xlWorkSheet.Cells[6, 18] = "Min";
                xlWorkSheet.Cells[6, 19] = "Max";
                xlWorkSheet.Cells[6, 20] = "Actual";
                xlWorkSheet.Cells[5, 21] = "Interference Load";
                xlWorkSheet.get_Range("U5", "W5").Merge(true);
                xlWorkSheet.Cells[6, 21] = "Min";
                xlWorkSheet.Cells[6, 22] = "Max";
                xlWorkSheet.Cells[6, 23] = "Actual";
                xlWorkSheet.Cells[5, 24] = "Gluing Confirmation";
                xlWorkSheet.Cells[5, 25] = "Result";


                //Station No -03
                xlWorkSheet.Cells[4, 26] = "Dowell Pressing";
                xlWorkSheet.get_Range("Z4", "AH4").Merge(true);
                xlWorkSheet.Cells[5, 26] = "Date and Time";
                xlWorkSheet.Cells[5, 27] = "Dowel*2 pkt BOP.";
                xlWorkSheet.Cells[5, 28] = "Air Pressure";
                xlWorkSheet.get_Range("AB5", "AD5").Merge(true);
                xlWorkSheet.Cells[6, 28] = "Min";
                xlWorkSheet.Cells[6, 29] = "Max";
                xlWorkSheet.Cells[6, 30] = "Actual";
                xlWorkSheet.Cells[5, 31] = "Interference Load";
                xlWorkSheet.get_Range("AE5", "AG5").Merge(true);
                xlWorkSheet.Cells[6, 31] = "Min";
                xlWorkSheet.Cells[6, 32] = "Max";
                xlWorkSheet.Cells[6, 33] = "Actual";
                xlWorkSheet.Cells[5, 34] = "Result";

                //Station No -04
                xlWorkSheet.Cells[4, 35] = "Anti-Drain Pressing";
                xlWorkSheet.get_Range("AI4", "AP4").Merge(true);
                xlWorkSheet.Cells[5, 35] = "Date and Time";
                xlWorkSheet.Cells[5, 36] = "Air Pressure";
                xlWorkSheet.get_Range("AJ5", "AL5").Merge(true);
                xlWorkSheet.Cells[6, 36] = "Min";
                xlWorkSheet.Cells[6, 37] = "Max";
                xlWorkSheet.Cells[6, 38] = "Actual";
                xlWorkSheet.Cells[5, 39] = "Interference Load";
                xlWorkSheet.get_Range("AM5", "AO5").Merge(true);
                xlWorkSheet.Cells[6, 39] = "Min";
                xlWorkSheet.Cells[6, 40] = "Max";
                xlWorkSheet.Cells[6, 41] = "Actual";
                xlWorkSheet.Cells[5, 42] = "Result";


                ////Station No -05
                xlWorkSheet.Cells[4, 43] = "O ring/Element Oiling & Checking And Big Cap Tightening";
                xlWorkSheet.get_Range("AQ4", "BD4").Merge(true);
                xlWorkSheet.Cells[5, 43] = "Date and Time";
                xlWorkSheet.Cells[5, 44] = "BOP. of Big Cap";
                xlWorkSheet.Cells[5, 45] = "Air Pressure";
                xlWorkSheet.get_Range("AS5", "AU5").Merge(true);
                xlWorkSheet.Cells[6, 45] = "Min";
                xlWorkSheet.Cells[6, 46] = "Max";
                xlWorkSheet.Cells[6, 47] = "Actual";

                xlWorkSheet.Cells[5, 48] = "Presence of Cap Oring";
                xlWorkSheet.Cells[5, 49] = "Presence of Element Oring-1";
                xlWorkSheet.Cells[5, 50] = "Presence of Element Oring-2";
                xlWorkSheet.Cells[5, 51] = "Presence of Bypass Valve";
                xlWorkSheet.Cells[5, 52] = "Element  & Cap Oiling Confirmation";
                xlWorkSheet.Cells[5, 53] = "Tightening Torque";
                xlWorkSheet.get_Range("BA5", "BC5").Merge(true);
                xlWorkSheet.Cells[6, 53] = "Min";
                xlWorkSheet.Cells[6, 54] = "Max";
                xlWorkSheet.Cells[6, 55] = "Actual";

                xlWorkSheet.Cells[5, 56] = "Result";



                ////Station No -06
                xlWorkSheet.Cells[4, 57] = " Gasket Installation & Screw Feeding";
                xlWorkSheet.get_Range("BE4", "BP4").Merge(true);
                xlWorkSheet.Cells[5, 57] = "Date and Time";

                //******Commented by dipak 10_10_20 for bop issue
                //xlWorkSheet.Cells[6, 58] = "BOP. pkt  M6*1.25 Screw"; //Interchange 02_Aug_20
                //xlWorkSheet.Cells[6, 59] = "BOP. pkt Gasket-1";
                //xlWorkSheet.Cells[6, 60] = "BOP. pkt Gasket-2";
                //xlWorkSheet.Cells[6, 61] = "BOP. pkt Gasket-3";
                //xlWorkSheet.Cells[6, 62] = "BOP. pkt Gasket-4";
                //xlWorkSheet.Cells[6, 63] = "BOP. pkt Cooler";//Interchange 02_Aug_20


                xlWorkSheet.Cells[6, 58] = "BOP. pkt Gasket-1"; //Interchange 02_Aug_20
                xlWorkSheet.Cells[6, 59] = "BOP. pkt Gasket-2";
                xlWorkSheet.Cells[6, 60] = "BOP. pkt Gasket-3";
                xlWorkSheet.Cells[6, 61] = "BOP. pkt Gasket-4";
                xlWorkSheet.Cells[6, 62] = "BOP. pkt Cooler";
                xlWorkSheet.Cells[6, 63] = "BOP. pkt  M6*1.25 Screw";//Interchange 02_Aug_20

                xlWorkSheet.Cells[5, 64] = "Air Pressure";
                xlWorkSheet.get_Range("BL5", "BN5").Merge(true);
                xlWorkSheet.Cells[6, 64] = "Min";
                xlWorkSheet.Cells[6, 65] = "Max";
                xlWorkSheet.Cells[6, 66] = "Actual";

                xlWorkSheet.Cells[5, 67] = "All gasket present status by CAMERA";

                xlWorkSheet.Cells[5, 68] = "Result";

                ////Station No -07
                xlWorkSheet.Cells[4, 69] = "Cooler Pre Tightening";
                xlWorkSheet.get_Range("BQ4", "BV4").Merge(true);
                xlWorkSheet.Cells[5, 69] = "Date and Time";

                xlWorkSheet.Cells[5, 70] = "Air Pressure";
                xlWorkSheet.get_Range("BR5", "BT5").Merge(true);
                xlWorkSheet.Cells[6, 70] = "Min";
                xlWorkSheet.Cells[6, 71] = "Max";
                xlWorkSheet.Cells[6, 72] = "Actual";

                xlWorkSheet.Cells[5, 73] = "Screw Count(Nos)";

                xlWorkSheet.Cells[5, 74] = "Result";

                ////Station No -08
                xlWorkSheet.Cells[4, 75] = "Heat Exchanger Final Tightening";
                xlWorkSheet.get_Range("BW4", "CS4").Merge(true);
                xlWorkSheet.Cells[5, 75] = "Date and Time";

                xlWorkSheet.Cells[5, 76] = "Air Pressure";
                xlWorkSheet.get_Range("BX5", "BZ5").Merge(true);
                xlWorkSheet.Cells[6, 76] = "Min";
                xlWorkSheet.Cells[6, 77] = "Max";
                xlWorkSheet.Cells[6, 78] = "Actual";

                xlWorkSheet.Cells[5, 79] = "Tightening Torque Screw 1";
                xlWorkSheet.get_Range("CA5", "CC5").Merge(true);
                xlWorkSheet.Cells[6, 79] = "Min";
                xlWorkSheet.Cells[6, 80] = "Max";
                xlWorkSheet.Cells[6, 81] = "Actual";

                xlWorkSheet.Cells[5, 82] = "Tightening Torque Screw 2";
                xlWorkSheet.get_Range("CD5", "CF5").Merge(true);
                xlWorkSheet.Cells[6, 82] = "Min";
                xlWorkSheet.Cells[6, 83] = "Max";
                xlWorkSheet.Cells[6, 84] = "Actual";

                xlWorkSheet.Cells[5, 85] = "Tightening Torque Screw 3";
                xlWorkSheet.get_Range("CG5", "CI5").Merge(true);
                xlWorkSheet.Cells[6, 85] = "Min";
                xlWorkSheet.Cells[6, 86] = "Max";
                xlWorkSheet.Cells[6, 87] = "Actual";

                xlWorkSheet.Cells[5, 88] = "Tightening Torque Screw 4";
                xlWorkSheet.get_Range("CJ5", "CL5").Merge(true);
                xlWorkSheet.Cells[6, 88] = "Min";
                xlWorkSheet.Cells[6, 89] = "Max";
                xlWorkSheet.Cells[6, 90] = "Actual";

                xlWorkSheet.Cells[5, 91] = "Tightening Torque Screw 5";
                xlWorkSheet.get_Range("CM5", "CO5").Merge(true);
                xlWorkSheet.Cells[6, 91] = "Min";
                xlWorkSheet.Cells[6, 92] = "Max";
                xlWorkSheet.Cells[6, 93] = "Actual";

                xlWorkSheet.Cells[5, 94] = "Tightening Torque Screw 6";
                xlWorkSheet.get_Range("CP5", "CR5").Merge(true);
                xlWorkSheet.Cells[6, 94] = "Min";
                xlWorkSheet.Cells[6, 95] = "Max";
                xlWorkSheet.Cells[6, 96] = "Actual";

                xlWorkSheet.Cells[5, 97] = "Result";

                ////Station No -09
                xlWorkSheet.Cells[4, 98] = "Anti drain leak testing";
                xlWorkSheet.get_Range("CT4", "DD4").Merge(true);
                xlWorkSheet.Cells[5, 98] = "Date and Time";

                xlWorkSheet.Cells[6, 99] = "BOP. Gasket Clean/Dirty Oil";
                xlWorkSheet.Cells[6, 100] = "BOP. Gasket Oil Drain Out";
                xlWorkSheet.Cells[6, 101] = "BOP. Gasket Coolent Outlet";

                xlWorkSheet.Cells[5, 102] = "Test Pressure";
                xlWorkSheet.get_Range("CX5", "CZ5").Merge(true);
                xlWorkSheet.Cells[6, 102] = "Min";
                xlWorkSheet.Cells[6, 103] = "Max";
                xlWorkSheet.Cells[6, 104] = "Actual";

                xlWorkSheet.Cells[5, 105] = "Leak Flow Rate";
                xlWorkSheet.get_Range("DA5", "DC5").Merge(true);
                xlWorkSheet.Cells[6, 105] = "Min";
                xlWorkSheet.Cells[6, 106] = "Max";
                xlWorkSheet.Cells[6, 107] = "Actual";

                //xlWorkSheet.Cells[5, 108] = "Main Line Pressure";
                //xlWorkSheet.get_Range("DD5", "DF5").Merge(true);
                //xlWorkSheet.Cells[6, 108] = "Min";
                //xlWorkSheet.Cells[6, 109] = "Max";
                //xlWorkSheet.Cells[6, 110] = "Actual";

                xlWorkSheet.Cells[5, 108] = "Result";

                ////Station No -10
                xlWorkSheet.Cells[4, 109] = "Dry leak testing";
                xlWorkSheet.get_Range("DE4", "DO4").Merge(true);
                xlWorkSheet.Cells[5, 109] = "Date and Time";

                xlWorkSheet.Cells[5, 110] = "Water Pressure (kpa)";
                xlWorkSheet.Cells[5, 111] = "Water Leakage (cm/mm)";
                xlWorkSheet.Cells[5, 112] = "Service Pressure (kap)";
                xlWorkSheet.Cells[5, 113] = "Service Leakage (cm/mm)";
                xlWorkSheet.Cells[5, 114] = "Oil Pressure (kpa)";
                xlWorkSheet.Cells[5, 115] = "Module/Chamber Pressure   (kpa)";
                xlWorkSheet.Cells[5, 116] = "Oil Leakage   (cm/mm)";
                xlWorkSheet.Cells[5, 117] = "Service Flow  (mm/cm)";
                xlWorkSheet.Cells[5, 118] = "Lasser Code";

                xlWorkSheet.Cells[5, 119] = "Result";


                ////Station No -11
                xlWorkSheet.Cells[4, 120] = "Final Inspection";
                xlWorkSheet.get_Range("DP4", "DR4").Merge(true);
                xlWorkSheet.Cells[5, 120] = "Date and Time";

                xlWorkSheet.Cells[5, 121] = "Final 2D Data matrix Code";

                xlWorkSheet.Cells[5, 122] = "Result";


                xlWorkSheet.Cells[6, 1] = "Sr No.";
                xlWorkSheet.Cells[6, 3] = "Model.";
                xlWorkSheet.Cells[6, 4] = "Part No";
                xlWorkSheet.Cells[6, 5] = "Cusotmer Part No";

                xlWorkSheet.get_Range("F2", "M2").Merge(true);
                xlWorkSheet.Cells[2, 6] = "Traceability Report";
                chartRange = xlWorkSheet.get_Range("F2", "M2");
                chartRange.Font.Underline = true;
                chartRange.Style.HorizontalAlignment = true;
                chartRange.Font.Size = 16;
                //AllBorders(xlWorkSheet.Rows.Borders);
                //xlWorkSheet.get_Range("F4", "BD4").Cells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                //xlWorkSheet.get_Range("F4", "O4").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                int counter = 0;
               
                string[] aParamArr = null;
                string sBarcode = "";
                bool b6Child = false;
                bool b9Child = false;
                progressBar1.Visible = true;
                for (int iBarcode = 0; iBarcode < dtFinal.Rows.Count; iBarcode++)
                {
                    sBarcodeFinal = iBarcode.ToString();
                    if (iBarcode == 21)
                    {

                    }
                    b9Child = false;
                    b6Child = false;
                    counter = counter + 1;
                    sBarcode = dtFinal.Rows[iBarcode][0].ToString();
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        //Model_No,       "0"
                        //FG_PART_NO,     "1"
                        //CUST_PART_NO,   "2"
                        //Parent_Barcode, "3"
                        //Station_No,     "4" 
                        //Station_Name,   "5"
                        //Machine_Param,  "6"
                        //Machine_Status, "7"
                        //Child_Barcode,  "8"
                        //Created_On      "9"
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                        {
                            continue;
                        }
                        if (sBarcode != dgv.Rows[i].Cells[3].Value.ToString())
                        {
                            continue;
                        }
                        if (sBarcode != dgv.Rows[i].Cells[3].Value.ToString())
                        {
                            goto OuterLoop;
                        }


                        if (dgv.Rows[i].Cells[4].Value.ToString() == "01")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "02")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "03")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[8];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "04")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[8];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "05")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[11];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "06")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[4];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "07")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[4];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "08")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[21];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "09")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "10")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        xlWorkSheet.Cells[iBarcode + 7, 1] = counter;//Sr_No
                        xlWorkSheet.Cells[iBarcode + 7, 2] = dgv.Rows[i].Cells[3].Value.ToString().Trim();//Parent QR Code
                        xlWorkSheet.Cells[iBarcode + 7, 3] = dgv.Rows[i].Cells[0].Value.ToString(); //Model No
                        xlWorkSheet.Cells[iBarcode + 7, 4] = dgv.Rows[i].Cells[1].Value.ToString(); //FG_Part No
                        xlWorkSheet.Cells[iBarcode + 7, 5] = dgv.Rows[i].Cells[2].Value.ToString();  //Cust Part No
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "01")
                        {

                            #region  Staion-01
                            xlWorkSheet.Cells[iBarcode + 7, 6] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 7] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].Trim().ToString(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 8] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 9] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 10] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 11] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 12] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 13] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 14] = aParamArr[j].Trim();
                                }
                            }
                            xlWorkSheet.Cells[iBarcode + 7, 15] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "02")
                        {
                            #region  Staion-02
                            xlWorkSheet.Cells[iBarcode + 7, 16] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 17] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 18] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 19] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 20] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 21] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 22] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 23] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 24] = aParamArr[j].Trim();
                                }
                            }
                            xlWorkSheet.Cells[iBarcode + 7, 25] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "03")
                        {
                            #region  Staion-03
                            xlWorkSheet.Cells[iBarcode + 7, 26] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 27] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 28] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 29] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 30] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 31] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 32] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 33] = aParamArr[j].Trim();
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 34] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "04")
                        {
                            #region  Staion-04
                            xlWorkSheet.Cells[iBarcode + 7, 35] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 36] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 37] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 38] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 39] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 40] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 41] = aParamArr[j].Trim();
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 42] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "05")
                        {
                            #region  Staion-05
                            xlWorkSheet.Cells[iBarcode + 7, 43] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 44] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 45] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 46] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 47] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 48] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 49] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 50] = aParamArr[j].Trim();
                                }
                                if (j == 6)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 51] = aParamArr[j].Trim();
                                }
                                if (j == 7)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 52] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 53] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 9)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 54] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 10)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 55] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 56] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "06")
                        {
                            #region  Staion-06
                            xlWorkSheet.Cells[iBarcode + 7, 57] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            if (b6Child == false)
                            {
                                int i6ChildCounter = 0;
                                for (int iChildBarcode = i; iChildBarcode < dg.Rows.Count; iChildBarcode++)
                                {
                                    b6Child = true;
                                    i6ChildCounter = i6ChildCounter + 1;
                                    if (i6ChildCounter == 1)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 58] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode1
                                    }
                                    if (i6ChildCounter == 2)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 59] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode2
                                    }
                                    if (i6ChildCounter == 3)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 60] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode3
                                    }
                                    if (i6ChildCounter == 4)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 61] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode4
                                    }
                                    if (i6ChildCounter == 5)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 62] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode5

                                    }
                                    if (i6ChildCounter == 6)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 63] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode6
                                        break;
                                    }
                                }
                            }
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 64] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 65] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 66] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 67] = Convert.ToString(aParamArr[j].Trim());
                                    }
                                }


                            }
                            xlWorkSheet.Cells[iBarcode + 7, 68] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "07")
                        {
                            #region  Staion-07
                            xlWorkSheet.Cells[iBarcode + 7, 69] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 70] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 71] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 72] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 73] = Convert.ToString(aParamArr[j].Trim());
                                    }
                                }


                            }
                            xlWorkSheet.Cells[iBarcode + 7, 74] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "08")
                        {
                            #region  Staion-08
                            xlWorkSheet.Cells[iBarcode + 7, 75] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 76] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 77] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 78] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 79] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 4)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 80] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 5)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 81] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 6)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 82] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 7)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 83] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 8)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 84] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 9)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 85] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 10)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 86] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 11)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 87] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 12)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 88] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 13)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 89] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 14)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 90] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 15)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 91] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 16)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 92] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 17)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 93] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 18)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 94] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 19)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 95] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 20)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 96] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }



                            }
                            xlWorkSheet.Cells[iBarcode + 7, 97] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "09")
                        {
                            #region  Staion-09
                            xlWorkSheet.Cells[iBarcode + 7, 98] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            if (b9Child == false)
                            {
                                int i9ChildCounter = 0;
                                for (int iChildBarcode = i; iChildBarcode < dg.Rows.Count; iChildBarcode++)
                                {
                                    b9Child = true;
                                    i9ChildCounter = i9ChildCounter + 1;
                                    if (i9ChildCounter == 1)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 99] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode1
                                    }
                                    if (i9ChildCounter == 2)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 100] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode2
                                    }
                                    if (i9ChildCounter == 3)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 101] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode3
                                    }

                                }
                            }

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 102] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 103] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 104] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 105] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 4)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 106] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 5)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 107] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                //if (j == 6)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 108] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}
                                //if (j == 7)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 109] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}
                                //if (j == 8)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 110] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 108] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "10")
                        {
                            #region  Staion-10
                            xlWorkSheet.Cells[iBarcode + 7, 109] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 110] = aParamArr[j].Trim();

                                }
                                if (j == 1)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 111] = aParamArr[j].Trim();
                                }
                                if (j == 2)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 112] = aParamArr[j].Trim();
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 113] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 114] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 115] = aParamArr[j].Trim();
                                }
                                if (j == 6)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 116] = aParamArr[j].Trim();
                                }
                                if (j == 7)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 117] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    if (dgv.Rows[i].Cells[7].Value.ToString() == "PASS")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 118] = aParamArr[j].Trim();
                                    }
                                    else
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 118] = "";
                                    }
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 119] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "11")
                        {
                            #region  Staion-11
                            xlWorkSheet.Cells[iBarcode + 7, 120] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            xlWorkSheet.Cells[iBarcode + 7, 121] = dgv.Rows[i].Cells[8].Value.ToString();  //FG Barcode

                            xlWorkSheet.Cells[iBarcode + 7, 122] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        progressBar1.Value = i * progressBar1.Maximum / dtBarcode.Rows.Count;
                        Application.DoEvents();
                    }
                    progressBar1.Value = iBarcode * progressBar1.Maximum / dtFinal.Rows.Count;
                    Application.DoEvents();
                OuterLoop:
                    continue;
                   
                }
                progressBar1.Value = 100;
                saveFileDialog1.Filter = "CSV Files|*.csv|Excel Files|*.xlsx|1997-2003 Excel Files|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //obj.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName);

                    //obj.ActiveWorkbook.Saved = true;

                    //obj.Quit();

                    xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();



                    MessageBox.Show("File created !");
                }
                progressBar1.Visible = false;
                releaseObject(xlApp);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkSheet);
                //Now remove the file to backup location
                //string FileNewName = SelectedFile.Split('.')[0];
                //FileNewName += "_" + DateTime.Now.ToString("ddMMyyHHmmss");

                //if (!Directory.Exists(BCommon.PcFilePathBackup))
                //    Directory.CreateDirectory(BCommon.PcFilePathBackup);

                //File.Move(BCommon.PcFilePath + "\\" + SelectedFile, BCommon.PcFilePathBackup + "\\" + FileNewName + ".txt");

                //MessageBox.Show("File created successfully");
                //ReadAllAvailableFiles();
                //Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(sBarcodeFinal);
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }

        private void SubAssyReportExportToExcel(DataGridView dg)
        {
            try
            {
                DataTable dtBarcode = (DataTable)dg.DataSource;
                DataTable dtFinal = dtBarcode.AsDataView().ToTable(true, "Parent_Barcode");

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                Excel.Range chartRange;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Rows.Font.Name = "Arial";
                xlWorkSheet.Rows.Font.Bold = true;
                xlWorkSheet.Rows.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //xlApp.StandardFontSize = 10;

                xlWorkSheet.Cells[2, 4] = "Customer:-";
                xlWorkSheet.Cells[2, 5] = "Mahindra";
                xlWorkSheet.Cells[5, 2] = "Housing QR Code";

                //Station No -00
                xlWorkSheet.Cells[4, 6] = "Big Cup Assy.";
                xlWorkSheet.get_Range("F4", "O4").Merge(true);
                xlWorkSheet.Cells[5, 6] = "Date and Time";
                xlWorkSheet.Cells[5, 7] = "Child pkt BOP.";
                xlWorkSheet.Cells[5, 8] = "Air Pressure";
                xlWorkSheet.get_Range("H5", "J5").Merge(true);
                xlWorkSheet.Cells[6, 8] = "Min";
                xlWorkSheet.Cells[6, 9] = "Max";
                xlWorkSheet.Cells[6, 10] = "Actual";
                xlWorkSheet.Cells[5, 11] = "Result";





                xlWorkSheet.Cells[6, 1] = "Sr No.";
                xlWorkSheet.Cells[6, 3] = "Model.";
                xlWorkSheet.Cells[6, 4] = "Part No";
                xlWorkSheet.Cells[6, 5] = "Cusotmer Part No";

                xlWorkSheet.get_Range("F2", "M2").Merge(true);
                xlWorkSheet.Cells[2, 6] = "Traceability Report";
                chartRange = xlWorkSheet.get_Range("F2", "M2");
                chartRange.Font.Underline = true;
                chartRange.Style.HorizontalAlignment = true;
                chartRange.Font.Size = 16;
                //AllBorders(xlWorkSheet.Rows.Borders);
                //xlWorkSheet.get_Range("F4", "BD4").Cells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                //xlWorkSheet.get_Range("F4", "O4").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                int counter = 0;
                string[] aParamArr = null;
                string sBarcode = "";
                bool b6Child = false;
                //for (int iBarcode = 0; iBarcode < dtFinal.Rows.Count; iBarcode++)
                //{
                //    b6Child = false;
                //    counter = counter + 1;
                //    sBarcode = dtFinal.Rows[iBarcode][0].ToString();
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    counter = counter + 1;
                    //Model_No,       "0"
                    //FG_PART_NO,     "1"
                    //CUST_PART_NO,   "2"
                    //Parent_Barcode, "3"
                    //Station_No,     "4" 
                    //Station_Name,   "5"
                    //Machine_Param,  "6"
                    //Machine_Status, "7"
                    //Child_Barcode,  "8"
                    //Created_On      "9"

                    //if (sBarcode != dgv.Rows[i].Cells[3].Value.ToString())
                    //{
                    //    continue;
                    //}
                    //if (sBarcode != dgv.Rows[i].Cells[3].Value.ToString())
                    //{
                    //    goto OuterLoop;
                    //}


                    if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                    {
                        int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                        aParamArr = new string[3];
                        aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                    }

                    xlWorkSheet.Cells[i + 7, 1] = counter;//Sr_No
                    xlWorkSheet.Cells[i + 7, 2] = dgv.Rows[i].Cells[3].Value.ToString().Trim();//Parent QR Code
                    xlWorkSheet.Cells[i + 7, 3] = dgv.Rows[i].Cells[0].Value.ToString(); //Model No
                    xlWorkSheet.Cells[i + 7, 4] = dgv.Rows[i].Cells[1].Value.ToString(); //FG_Part No
                    xlWorkSheet.Cells[i + 7, 5] = dgv.Rows[i].Cells[2].Value.ToString();  //Cust Part No
                    if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                    {

                        #region  Staion-00
                        xlWorkSheet.Cells[i + 7, 6] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                        xlWorkSheet.Cells[i + 7, 7] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                        for (int j = 0; j < aParamArr.Length; j++)
                        {

                            if (j == 0)
                            {
                                if (char.IsNumber(aParamArr[j].Trim().ToString(), 0) && aParamArr[j].Trim() != "")
                                {
                                    xlWorkSheet.Cells[i + 7, 8] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                }

                            }
                            if (j == 1)
                            {
                                if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                {
                                    xlWorkSheet.Cells[i + 7, 9] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                }
                            }
                            if (j == 2)
                            {
                                if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                {
                                    xlWorkSheet.Cells[i + 7, 10] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                }
                            }

                        }
                        xlWorkSheet.Cells[i + 7, 11] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                        #endregion
                    }


                }
                //OuterLoop:
                //    continue;
                //}
                saveFileDialog1.Filter = "CSV Files|*.csv|Excel Files|*.xlsx|1997-2003 Excel Files|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();



                    MessageBox.Show("File created !");
                }
                releaseObject(xlApp);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkSheet);

            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }

        private void TraceOffReportExportToExcel(DataGridView dg)
        {
            try
            {
                DataTable dtBarcode = (DataTable)dg.DataSource;
                DataTable dtFinal = dtBarcode.AsDataView().ToTable(true, "row_num");

                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                Excel.Range chartRange;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Rows.Font.Name = "Arial";
                xlWorkSheet.Rows.Font.Bold = true;
                xlWorkSheet.Rows.Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //xlApp.StandardFontSize = 10;

                xlWorkSheet.Cells[2, 4] = "Customer:-";
                xlWorkSheet.Cells[2, 5] = "Mahindra";
                xlWorkSheet.Cells[5, 2] = "Housing QR Code";


                //Station No -00
                xlWorkSheet.Cells[4, 6] = "Big Cup Assy.";
                xlWorkSheet.get_Range("F4", "K4").Merge(true);
                xlWorkSheet.Cells[5, 6] = "Date and Time";
                xlWorkSheet.Cells[5, 7] = "Child pkt Barcode";
                xlWorkSheet.Cells[5, 8] = "Air Pressure";
                xlWorkSheet.get_Range("H5", "J5").Merge(true);
                xlWorkSheet.Cells[6, 8] = "Min";
                xlWorkSheet.Cells[6, 9] = "Max";
                xlWorkSheet.Cells[6, 10] = "Actual";
                xlWorkSheet.Cells[5, 11] = "Result";


                //Station No -01
                xlWorkSheet.Cells[4, 12] = "Ø12 Cup Gluing & Pressing";
                xlWorkSheet.get_Range("L4", "U4").Merge(true);
                xlWorkSheet.Cells[5, 12] = "Date and Time";
                xlWorkSheet.Cells[5, 13] = "Ø12 Cup pkt Barcode";
                xlWorkSheet.Cells[5, 14] = "Air Pressure";
                xlWorkSheet.get_Range("N5", "P5").Merge(true);
                xlWorkSheet.Cells[6, 14] = "Min";
                xlWorkSheet.Cells[6, 15] = "Max";
                xlWorkSheet.Cells[6, 16] = "Actual";
                xlWorkSheet.Cells[5, 17] = "Interference Load";
                xlWorkSheet.get_Range("Q5", "S5").Merge(true);
                xlWorkSheet.Cells[6, 17] = "Min";
                xlWorkSheet.Cells[6, 18] = "Max";
                xlWorkSheet.Cells[6, 19] = "Actual";
                xlWorkSheet.Cells[5, 20] = "Gluing Confirmation";
                xlWorkSheet.Cells[5, 21] = "Result";


                //Station No -02
                xlWorkSheet.Cells[4, 22] = "Ø26 Cup Gluing & Pressing";
                xlWorkSheet.get_Range("V4", "AE4").Merge(true);
                xlWorkSheet.Cells[5, 22] = "Date and Time";
                xlWorkSheet.Cells[5, 23] = "Barcode of  pkt Ø26 Cup";
                xlWorkSheet.Cells[5, 24] = "Air Pressure";
                xlWorkSheet.get_Range("X5", "Z5").Merge(true);
                xlWorkSheet.Cells[6, 24] = "Min";
                xlWorkSheet.Cells[6, 25] = "Max";
                xlWorkSheet.Cells[6, 26] = "Actual";
                xlWorkSheet.Cells[5, 27] = "Interference Load";
                xlWorkSheet.get_Range("AA5", "AC5").Merge(true);
                xlWorkSheet.Cells[6, 27] = "Min";
                xlWorkSheet.Cells[6, 28] = "Max";
                xlWorkSheet.Cells[6, 29] = "Actual";
                xlWorkSheet.Cells[5, 30] = "Gluing Confirmation";
                xlWorkSheet.Cells[5, 31] = "Result";


                //Station No -03
                xlWorkSheet.Cells[4, 32] = "Dowell Pressing";
                xlWorkSheet.get_Range("AF4", "AN4").Merge(true);
                xlWorkSheet.Cells[5, 32] = "Date and Time";
                xlWorkSheet.Cells[5, 33] = "Dowel*2 pkt  nos Barcode";
                xlWorkSheet.Cells[5, 34] = "Air Pressure";
                xlWorkSheet.get_Range("AH5", "AJ5").Merge(true);
                xlWorkSheet.Cells[6, 34] = "Min";
                xlWorkSheet.Cells[6, 35] = "Max";
                xlWorkSheet.Cells[6, 36] = "Actual";
                xlWorkSheet.Cells[5, 37] = "Interference Load";
                xlWorkSheet.get_Range("AK5", "AM5").Merge(true);
                xlWorkSheet.Cells[6, 37] = "Min";
                xlWorkSheet.Cells[6, 38] = "Max";
                xlWorkSheet.Cells[6, 39] = "Actual";
                xlWorkSheet.Cells[5, 40] = "Result";

                //Station No -04
                xlWorkSheet.Cells[4, 41] = "Anti-Drain Pressing";
                xlWorkSheet.get_Range("AO4", "AV4").Merge(true);
                xlWorkSheet.Cells[5, 41] = "Date and Time";
                xlWorkSheet.Cells[5, 42] = "Air Pressure";
                xlWorkSheet.get_Range("AP5", "AR5").Merge(true);
                xlWorkSheet.Cells[6, 42] = "Min";
                xlWorkSheet.Cells[6, 43] = "Max";
                xlWorkSheet.Cells[6, 44] = "Actual";
                xlWorkSheet.Cells[5, 45] = "Interference Load";
                xlWorkSheet.get_Range("AS5", "AU5").Merge(true);
                xlWorkSheet.Cells[6, 45] = "Min";
                xlWorkSheet.Cells[6, 46] = "Max";
                xlWorkSheet.Cells[6, 47] = "Actual";
                xlWorkSheet.Cells[5, 48] = "Result";


                ////Station No -05
                xlWorkSheet.Cells[4, 49] = "O ring/Element Oiling & Checking And Big Cap Tightening";
                xlWorkSheet.get_Range("AW4", "BJ4").Merge(true);
                xlWorkSheet.Cells[5, 49] = "Date and Time";
                xlWorkSheet.Cells[5, 50] = "Barcode of Big Cap";
                xlWorkSheet.Cells[5, 51] = "Air Pressure";
                xlWorkSheet.get_Range("AY5", "BA5").Merge(true);
                xlWorkSheet.Cells[6, 51] = "Min";
                xlWorkSheet.Cells[6, 52] = "Max";
                xlWorkSheet.Cells[6, 53] = "Actual";

                xlWorkSheet.Cells[5, 54] = "Presence of Cap Oring";
                xlWorkSheet.Cells[5, 55] = "Presence of Element Oring-1";
                xlWorkSheet.Cells[5, 56] = "Presence of Element Oring-2";
                xlWorkSheet.Cells[5, 57] = "Presence of Bypass Valve";
                xlWorkSheet.Cells[5, 58] = "Element  & Cap Oiling Confirmation";
                xlWorkSheet.Cells[5, 59] = "Tightening Torque";
                xlWorkSheet.get_Range("BG5", "BI5").Merge(true);
                xlWorkSheet.Cells[6, 59] = "Min";
                xlWorkSheet.Cells[6, 60] = "Max";
                xlWorkSheet.Cells[6, 61] = "Actual";

                xlWorkSheet.Cells[5, 62] = "Result";



                ////Station No -06
                xlWorkSheet.Cells[4, 63] = " Gasket Installation & Screw Feeding";
                xlWorkSheet.get_Range("BK4", "BV4").Merge(true);
                xlWorkSheet.Cells[5, 63] = "Date and Time";

                xlWorkSheet.Cells[6, 64] = "Barcode pkt Cooler";
                xlWorkSheet.Cells[6, 65] = "Barcode pkt Gasket-1";
                xlWorkSheet.Cells[6, 66] = "Barcode pkt Gasket-2";
                xlWorkSheet.Cells[6, 67] = "Barcode pkt Gasket-3";
                xlWorkSheet.Cells[6, 68] = "Barcode pkt Gasket-4";
                xlWorkSheet.Cells[6, 69] = "Barcode  pkt  M6*1.25 Screw";

                xlWorkSheet.Cells[5, 70] = "Air Pressure";
                xlWorkSheet.get_Range("BR5", "BT5").Merge(true);
                xlWorkSheet.Cells[6, 70] = "Min";
                xlWorkSheet.Cells[6, 71] = "Max";
                xlWorkSheet.Cells[6, 72] = "Actual";

                xlWorkSheet.Cells[5, 73] = "All gasket present status by CAMERA";

                xlWorkSheet.Cells[5, 74] = "Result";

                ////Station No -07
                xlWorkSheet.Cells[4, 75] = "Cooler Pre Tightening";
                xlWorkSheet.get_Range("BW4", "CB4").Merge(true);
                xlWorkSheet.Cells[5, 75] = "Date and Time";

                xlWorkSheet.Cells[5, 76] = "Air Pressure";
                xlWorkSheet.get_Range("BX5", "BZ5").Merge(true);
                xlWorkSheet.Cells[6, 76] = "Min";
                xlWorkSheet.Cells[6, 77] = "Max";
                xlWorkSheet.Cells[6, 78] = "Actual";

                xlWorkSheet.Cells[5, 79] = "Screw Count(Nos)";

                xlWorkSheet.Cells[5, 80] = "Result";

                ////Station No -08
                xlWorkSheet.Cells[4, 81] = "Heat Exchanger Final Tightening";
                xlWorkSheet.get_Range("CC4", "CY4").Merge(true);
                xlWorkSheet.Cells[5, 81] = "Date and Time";

                xlWorkSheet.Cells[5, 82] = "Air Pressure";
                xlWorkSheet.get_Range("CD5", "CF5").Merge(true);
                xlWorkSheet.Cells[6, 82] = "Min";
                xlWorkSheet.Cells[6, 83] = "Max";
                xlWorkSheet.Cells[6, 84] = "Actual";

                xlWorkSheet.Cells[5, 85] = "Tightening Torque Screw 1";
                xlWorkSheet.get_Range("CG5", "CI5").Merge(true);
                xlWorkSheet.Cells[6, 85] = "Min";
                xlWorkSheet.Cells[6, 86] = "Max";
                xlWorkSheet.Cells[6, 87] = "Actual";

                xlWorkSheet.Cells[5, 88] = "Tightening Torque Screw 2";
                xlWorkSheet.get_Range("CJ5", "CL5").Merge(true);
                xlWorkSheet.Cells[6, 88] = "Min";
                xlWorkSheet.Cells[6, 89] = "Max";
                xlWorkSheet.Cells[6, 90] = "Actual";

                xlWorkSheet.Cells[5, 91] = "Tightening Torque Screw 3";
                xlWorkSheet.get_Range("CM5", "CO5").Merge(true);
                xlWorkSheet.Cells[6, 91] = "Min";
                xlWorkSheet.Cells[6, 92] = "Max";
                xlWorkSheet.Cells[6, 93] = "Actual";

                xlWorkSheet.Cells[5, 94] = "Tightening Torque Screw 4";
                xlWorkSheet.get_Range("CP5", "CR5").Merge(true);
                xlWorkSheet.Cells[6, 94] = "Min";
                xlWorkSheet.Cells[6, 95] = "Max";
                xlWorkSheet.Cells[6, 96] = "Actual";

                xlWorkSheet.Cells[5, 97] = "Tightening Torque Screw 5";
                xlWorkSheet.get_Range("CS5", "CU5").Merge(true);
                xlWorkSheet.Cells[6, 97] = "Min";
                xlWorkSheet.Cells[6, 98] = "Max";
                xlWorkSheet.Cells[6, 99] = "Actual";

                xlWorkSheet.Cells[5, 100] = "Tightening Torque Screw 6";
                xlWorkSheet.get_Range("CV5", "CX5").Merge(true);
                xlWorkSheet.Cells[6, 100] = "Min";
                xlWorkSheet.Cells[6, 101] = "Max";
                xlWorkSheet.Cells[6, 102] = "Actual";

                xlWorkSheet.Cells[5, 103] = "Result";

                ////Station No -09
                xlWorkSheet.Cells[4, 104] = "Anti drain leak testing";
                xlWorkSheet.get_Range("CZ4", "DG4").Merge(true);
                xlWorkSheet.Cells[5, 104] = "Date and Time";

                xlWorkSheet.Cells[5, 105] = "Test Pressure";
                xlWorkSheet.get_Range("DA5", "DC5").Merge(true);
                xlWorkSheet.Cells[6, 105] = "Min";
                xlWorkSheet.Cells[6, 106] = "Max";
                xlWorkSheet.Cells[6, 107] = "Actual";

                xlWorkSheet.Cells[5, 108] = "Leak Flow Rate";
                xlWorkSheet.get_Range("DD5", "DF5").Merge(true);
                xlWorkSheet.Cells[6, 108] = "Min";
                xlWorkSheet.Cells[6, 109] = "Max";
                xlWorkSheet.Cells[6, 110] = "Actual";

                //xlWorkSheet.Cells[5, 111] = "Main Line Pressure";
                //xlWorkSheet.get_Range("DG5", "DI5").Merge(true);
                //xlWorkSheet.Cells[6, 111] = "Min";
                //xlWorkSheet.Cells[6, 112] = "Max";
                //xlWorkSheet.Cells[6, 113] = "Actual";

                xlWorkSheet.Cells[5, 111] = "Result";



                ////Station No -10
                xlWorkSheet.Cells[4, 112] = "Dry leak testing";
                xlWorkSheet.get_Range("DH4", "DR4").Merge(true);
                xlWorkSheet.Cells[5, 112] = "Date and Time";

                xlWorkSheet.Cells[5, 113] = "Water Pressure (kpa)";
                xlWorkSheet.Cells[5, 114] = "Water Leakage (cm/mm)";
                xlWorkSheet.Cells[5, 115] = "Service Pressure (kap)";
                xlWorkSheet.Cells[5, 116] = "Service Leakage (cm/mm)";
                xlWorkSheet.Cells[5, 117] = "Oil Pressure (kpa)";
                xlWorkSheet.Cells[5, 118] = "Module/Chamber Pressure   (kpa)";
                xlWorkSheet.Cells[5, 119] = "Oil Leakage   (cm/mm)";
                xlWorkSheet.Cells[5, 120] = "Service Flow  (mm/cm)";
                xlWorkSheet.Cells[5, 121] = "Lasser Code";

                xlWorkSheet.Cells[5, 122] = "Result";


                ////Station No -11
                xlWorkSheet.Cells[4, 123] = "Final Inspection";
                xlWorkSheet.get_Range("DS4", "DU4").Merge(true);
                xlWorkSheet.Cells[5, 123] = "Date and Time";

                xlWorkSheet.Cells[5, 124] = "Final 2D Data matrix Code";

                xlWorkSheet.Cells[5, 125] = "Result";



                xlWorkSheet.Cells[6, 1] = "Sr No.";
                xlWorkSheet.Cells[6, 3] = "Model.";
                xlWorkSheet.Cells[6, 4] = "Part No";
                xlWorkSheet.Cells[6, 5] = "Cusotmer Part No";

                xlWorkSheet.get_Range("F2", "M2").Merge(true);
                xlWorkSheet.Cells[2, 6] = "Traceability Report";
                chartRange = xlWorkSheet.get_Range("F2", "M2");
                chartRange.Font.Underline = true;
                chartRange.Style.HorizontalAlignment = true;
                chartRange.Font.Size = 16;
                //AllBorders(xlWorkSheet.Rows.Borders);
                //xlWorkSheet.get_Range("F4", "BD4").Cells.Borders.Weight = Excel.XlBorderWeight.xlThin;
                //xlWorkSheet.get_Range("F4", "O4").Cells.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                int counter = 0;
                string[] aParamArr = null;
                string sBarcode = "";
                bool b6Child = false;
                for (int iBarcode = 0; iBarcode < dtFinal.Rows.Count; iBarcode++)
                {
                    b6Child = false;
                    counter = counter + 1;
                    sBarcode = dtFinal.Rows[iBarcode]["row_num"].ToString();
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        //Model_No,       "0"
                        //FG_PART_NO,     "1"
                        //CUST_PART_NO,   "2"
                        //Parent_Barcode, "3"
                        //Station_No,     "4" 
                        //Station_Name,   "5"
                        //Machine_Param,  "6"
                        //Machine_Status, "7"
                        //Child_Barcode,  "8"
                        //Created_On      "9"
                        //if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                        //{
                        //    continue;
                        //}
                        if (sBarcode != dgv.Rows[i].Cells["row_num"].Value.ToString())
                        {
                            continue;
                        }
                        if (sBarcode != dgv.Rows[i].Cells["row_num"].Value.ToString())
                        {
                            goto OuterLoop;
                        }

                        if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[3];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "01")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "02")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "03")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[8];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "04")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[8];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "05")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[11];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "06")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[4];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "07")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[4];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "08")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[21];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "09")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "10")
                        {
                            int len = dgv.Rows[i].Cells[6].Value.ToString().Split(';').Length;
                            aParamArr = new string[9];
                            aParamArr = dgv.Rows[i].Cells[6].Value.ToString().Split(';'); //Parameter
                        }
                        xlWorkSheet.Cells[iBarcode + 7, 1] = counter;//Sr_No
                        xlWorkSheet.Cells[iBarcode + 7, 2] = dgv.Rows[i].Cells[3].Value.ToString().Trim();//Parent QR Code
                        xlWorkSheet.Cells[iBarcode + 7, 3] = dgv.Rows[i].Cells[0].Value.ToString(); //Model No
                        xlWorkSheet.Cells[iBarcode + 7, 4] = dgv.Rows[i].Cells[1].Value.ToString(); //FG_Part No
                        xlWorkSheet.Cells[iBarcode + 7, 5] = dgv.Rows[i].Cells[2].Value.ToString();  //Cust Part No
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "00")
                        {
                            #region  Staion-00
                            xlWorkSheet.Cells[iBarcode + 7, 6] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 7] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].Trim().ToString(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 8] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 9] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 10] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 11] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "01")
                        {

                            #region  Staion-01
                            xlWorkSheet.Cells[iBarcode + 7, 12] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 13] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].Trim().ToString(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 14] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 15] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 16] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 17] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 18] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 19] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 20] = aParamArr[j].Trim();
                                }
                            }
                            xlWorkSheet.Cells[iBarcode + 7, 21] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "02")
                        {
                            #region  Staion-02
                            xlWorkSheet.Cells[iBarcode + 7, 22] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 23] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 24] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 25] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 26] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 27] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 28] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 29] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 30] = aParamArr[j].Trim();
                                }
                            }
                            xlWorkSheet.Cells[iBarcode + 7, 31] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "03")
                        {
                            #region  Staion-03
                            xlWorkSheet.Cells[iBarcode + 7, 32] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 33] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 34] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 35] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 36] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 37] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 38] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 39] = aParamArr[j].Trim();
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 40] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "04")
                        {
                            #region  Staion-04
                            xlWorkSheet.Cells[iBarcode + 7, 41] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 42] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 43] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 44] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 45] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 46] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 47] = aParamArr[j].Trim();
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 48] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "05")
                        {
                            #region  Staion-05
                            xlWorkSheet.Cells[iBarcode + 7, 49] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            xlWorkSheet.Cells[iBarcode + 7, 50] = dgv.Rows[i].Cells[8].Value.ToString();  //Child Barcode
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 51] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 52] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 53] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {

                                    xlWorkSheet.Cells[iBarcode + 7, 54] = aParamArr[j].Trim();

                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 55] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 56] = aParamArr[j].Trim();
                                }
                                if (j == 6)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 57] = aParamArr[j].Trim();
                                }
                                if (j == 7)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 58] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 59] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 9)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 60] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 10)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 61] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 62] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "06")
                        {
                            #region  Staion-06
                            xlWorkSheet.Cells[iBarcode + 7, 63] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time
                            if (b6Child == false)
                            {
                                int i6ChildCounter = 0;
                                for (int iChildBarcode = i; iChildBarcode < dg.Rows.Count; iChildBarcode++)
                                {
                                    b6Child = true;
                                    i6ChildCounter = i6ChildCounter + 1;
                                    if (i6ChildCounter == 1)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 64] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode1
                                    }
                                    if (i6ChildCounter == 2)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 65] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode2
                                    }
                                    if (i6ChildCounter == 3)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 66] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode3
                                    }
                                    if (i6ChildCounter == 4)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 67] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode4
                                    }
                                    if (i6ChildCounter == 5)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 68] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode5

                                    }
                                    if (i6ChildCounter == 6)
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 69] = dgv.Rows[iChildBarcode].Cells[8].Value.ToString();  //Child Barcode6
                                        break;
                                    }
                                }
                            }
                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 70] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 71] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 72] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 73] = Convert.ToString(aParamArr[j].Trim());
                                    }
                                }


                            }
                            xlWorkSheet.Cells[iBarcode + 7, 74] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "07")
                        {
                            #region  Staion-07
                            xlWorkSheet.Cells[iBarcode + 7, 75] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 76] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 77] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 78] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 79] = Convert.ToString(aParamArr[j].Trim());
                                    }
                                }


                            }
                            xlWorkSheet.Cells[iBarcode + 7, 80] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "08")
                        {
                            #region  Staion-08
                            xlWorkSheet.Cells[iBarcode + 7, 81] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 82] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 83] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 84] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 85] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 4)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 86] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 5)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 87] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 6)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 88] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 7)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 89] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 8)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 90] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 9)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 91] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 10)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 92] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 11)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 93] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 12)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 94] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 13)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 95] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 14)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 96] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 15)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 97] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }

                                }
                                if (j == 16)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 98] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 17)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 99] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 18)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 100] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 19)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 101] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }
                                if (j == 20)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 102] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 10);
                                    }
                                }



                            }
                            xlWorkSheet.Cells[iBarcode + 7, 103] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "09")
                        {
                            #region  Staion-09
                            xlWorkSheet.Cells[iBarcode + 7, 104] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 105] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }

                                }
                                if (j == 1)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 106] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 2)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 107] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 3)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 108] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 4)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 109] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                if (j == 5)
                                {
                                    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                    {
                                        xlWorkSheet.Cells[iBarcode + 7, 110] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                    }
                                }
                                //if (j == 6)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 111] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}
                                //if (j == 7)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 112] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}
                                //if (j == 8)
                                //{
                                //    if (char.IsNumber(aParamArr[j].ToString().Trim(), 0) && aParamArr[j].Trim() != "")
                                //    {
                                //        xlWorkSheet.Cells[iBarcode + 7, 113] = Convert.ToString((Convert.ToDecimal(aParamArr[j].Trim()) * 1) / 100);
                                //    }
                                //}

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 111] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "10")
                        {
                            #region  Staion-10
                            xlWorkSheet.Cells[iBarcode + 7, 112] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            for (int j = 0; j < aParamArr.Length; j++)
                            {

                                if (j == 0)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 113] = aParamArr[j].Trim();

                                }
                                if (j == 1)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 114] = aParamArr[j].Trim();
                                }
                                if (j == 2)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 115] = aParamArr[j].Trim();
                                }
                                if (j == 3)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 116] = aParamArr[j].Trim();
                                }
                                if (j == 4)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 117] = aParamArr[j].Trim();
                                }
                                if (j == 5)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 118] = aParamArr[j].Trim();
                                }
                                if (j == 6)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 119] = aParamArr[j].Trim();
                                }
                                if (j == 7)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 120] = aParamArr[j].Trim();
                                }
                                if (j == 8)
                                {
                                    xlWorkSheet.Cells[iBarcode + 7, 121] = aParamArr[j].Trim();
                                }

                            }
                            xlWorkSheet.Cells[iBarcode + 7, 122] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }
                        if (dgv.Rows[i].Cells[4].Value.ToString() == "11")
                        {
                            #region  Staion-11
                            xlWorkSheet.Cells[iBarcode + 7, 123] = dgv.Rows[i].Cells[9].Value.ToString();  //Date Time

                            xlWorkSheet.Cells[iBarcode + 7, 124] = dgv.Rows[i].Cells[3].Value.ToString();  //FG Barcode

                            xlWorkSheet.Cells[iBarcode + 7, 125] = dgv.Rows[i].Cells[7].Value.ToString();  //Result
                            #endregion
                        }

                    }
                OuterLoop:
                    continue;
                }
                saveFileDialog1.Filter = "CSV Files|*.csv|Excel Files|*.xlsx|1997-2003 Excel Files|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //obj.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName);

                    //obj.ActiveWorkbook.Saved = true;

                    //obj.Quit();

                    xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();



                    MessageBox.Show("File created !");
                }
                releaseObject(xlApp);
                releaseObject(xlWorkBook);
                releaseObject(xlWorkSheet);
                //Now remove the file to backup location
                //string FileNewName = SelectedFile.Split('.')[0];
                //FileNewName += "_" + DateTime.Now.ToString("ddMMyyHHmmss");

                //if (!Directory.Exists(BCommon.PcFilePathBackup))
                //    Directory.CreateDirectory(BCommon.PcFilePathBackup);

                //File.Move(BCommon.PcFilePath + "\\" + SelectedFile, BCommon.PcFilePathBackup + "\\" + FileNewName + ".txt");

                //MessageBox.Show("File created successfully");
                //ReadAllAvailableFiles();
                //Clear();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }



        private bool ValidateInput()
        {
            try
            {

                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        private void BindGrid()
        {

        }

        #endregion

        #region Label Event

        #endregion

        #region DataGridView Events


        #endregion

        #region TextBox Event






        #endregion



        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtPartSearch.Text = ""; //SATO
                _plObj = new PL_REPORT();
                _blObj = new BL_REPORT();
                _plObj.DbType = _RPT_Type;
                _plObj.FromDate = dpFromDate.Value.ToString("yyyy-MM-dd");
                _plObj.ToDate = dpToDate.Value.ToString("yyyy-MM-dd");
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                if (dt.Rows.Count > 0)
                {
                    dgv.DataSource = dt;
                   
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "No Data Found!!", 2);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)//SATO
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "No Data Found!!", 2);
                    return;
                }
                FGReportExportToExcel(dgv);
            }
            catch (Exception ex)
            {

                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void txtPartSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgv.Columns.Count > 0)
            {
                (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("Parent_Barcode LIKE '%{0}%'", txtPartSearch.Text);
            }
        }
    }
}
