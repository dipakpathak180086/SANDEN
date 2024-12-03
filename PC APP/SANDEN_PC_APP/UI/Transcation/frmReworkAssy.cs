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

namespace SANDEN_PC_APP
{
    public partial class frmReworkAssy : Form
    {

        #region Variables

        private BL_REWORK_ASSY _blObj = null;
        private PL_REWORK_ASSY _plObj = null;
       
        private string _stationNo = "";
       
        #endregion

        #region Form Methods

        public frmReworkAssy()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_REWORK_ASSY();
               
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
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                cbStation.SelectedIndex = 0;
                // GetMachineStatus();
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

        private void GetMachineStatus()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("StatusCode");
                dt.Columns.Add("StatusName");
                DataRow dr = dt.NewRow();
                dr["StatusCode"] = "1";
                dr["StatusName"] = "OK";
                
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["StatusCode"] = "2";
                dr["StatusName"] = "NG";
                dt.Rows.Add(dr);
                if (dt.Rows.Count > 0)
                {
                    GlobalVariable.BindCombo(cbStation, dt);
                    cbStation.SelectedIndex = 0;
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Sation No. data not found", 3);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private bool ValidateSationChildPart()
        {
            bool bReturn = false;
            try
            {
                Common common = new Common();
                DataTable dt = common.ValidateStationWithChildPart(cbStation.Text.Trim(),txtScanBarcode.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString().Equals("Y"))
                    {
                        bReturn = true;
                    }
                    else
                    {
                        lblMsg.BackColor = Color.Red;
                        lblMsg.ForeColor = Color.Yellow;
                        lblMsg.Text = dt.Rows[0][0].ToString();
                        bReturn = false;
                    }
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "No. data not found", 3);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
            return bReturn;
        }
        private void Clear()
        {
            try
            {
                lblMsg.Text = "";
                lblMsg.BackColor = System.Drawing.Color.Transparent;
                lblMsg.ForeColor = Color.Yellow;
                txtScanBarcode.Text = "";
                cbStation.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }






        #endregion

        #region Label Event

        #endregion

        #region DataGridView Events


        #endregion

        #region TextBox Event

        private void txtScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {


                if (e.KeyCode == Keys.Enter)
                {
                    if (cbStation.SelectedIndex <= 0)
                    {
                        lblMsg.BackColor = Color.Red;
                        lblMsg.ForeColor = Color.Yellow;
                        lblMsg.Text = "Select Station!!!";
                        return;

                    }
                    if (string.IsNullOrEmpty(txtScanBarcode.Text.Trim()))
                    {
                        lblMsg.BackColor = Color.Red;
                        lblMsg.ForeColor = Color.Yellow;
                        lblMsg.Text = "Scan Barcode Code!!!";
                        return;
                    }
                    //if (!txtScanBarcode.Text.Trim().Contains("$"))
                    //{
                    //    lblMsg.BackColor = Color.Red;
                    //    lblMsg.ForeColor = Color.Yellow;
                    //    lblMsg.Text = "Invalid Barcode";
                    //    return;
                    //}
                    //if (!ValidateSationChildPart())
                    //{
                    //    return;
                    //}

                    _plObj = new PL_REWORK_ASSY();
                    _blObj = new BL_REWORK_ASSY();
                    _plObj.Station_No = cbStation.Text.ToString().Trim();
                    _plObj.Scan_Barcode = txtScanBarcode.Text.Trim();
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                    DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows[0][0].ToString().Split('~')[0] == "Y")
                        {
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, $"{txtScanBarcode.Text.Trim()} Rework  Successfully!!", 1);
                            lblMsg.BackColor = Color.Green;
                            lblMsg.ForeColor = Color.Yellow;
                            lblMsg.Text = $"{txtScanBarcode.Text.Trim()} Rework  Successfully!!";
                            txtScanBarcode.Text = "";
                            //cbStation.SelectedIndex = 0;
                        }
                        else
                        {
                            lblMsg.BackColor = Color.Red;
                            lblMsg.ForeColor = Color.Yellow;
                            lblMsg.Text = dataTable.Rows[0][0].ToString();
                            txtScanBarcode.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }

        }





        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cbFinalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStation.SelectedIndex > 0)
            {
                txtScanBarcode.Focus();
            }
        }
    }
}
