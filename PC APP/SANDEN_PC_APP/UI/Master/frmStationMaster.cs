using SANDEN_BL;
using SANDEN_PC_APP;
using SANDEN_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SANDEN_COMMON;

namespace SANDEN_PC_APP
{
    public partial class frmStationMaster : Form
    {

        #region Variables

        BL_STATION_MASTER _blObj = null;
        PL_STATION_MASTER _plObj = null;
        bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmStationMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_STATION_MASTER();
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

                btnDelete.Enabled = false;
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
                BindGrid();
                txtStationNo.Focus();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    _plObj = new PL_STATION_MASTER();
                    _plObj.StationNo = txtStationNo.Text.Trim();
                    _plObj.SationName = txtStationName.Text.Trim();
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        _plObj.DbType = "INSERT";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                            {
                                btnReset_Click(sender, e);
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Saved successfully!!", 1);
                                frmModelMaster_Load(null, null);
                            }
                            else
                            {
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                            }
                        }
                    }
                    else // if updating data
                    {
                        _plObj.DbType = "UPDATE";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                        if (dataTable.Rows.Count > 0)
                        {
                            if (dataTable.Rows[0]["RESULT"].ToString() == "Y")
                            {
                                btnReset_Click(sender, e);
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Updated successfully!!", 1);
                            }
                            else
                            {
                                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of PRIMARY KEY"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "UserId already exist!!", 3);
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
            
                Clear();
                BindGrid();

                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStationNo.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Revision Code can't be blank!!", 3);
                    return;
                }
                if (GlobalVariable.mStoCustomFunction.ConfirmationMsg(GlobalVariable.mSatoApps, "Äre you sure to delete the record !!"))
                {
                    _plObj = new PL_STATION_MASTER();
                    _blObj = new BL_STATION_MASTER();
                    _plObj.StationNo = txtStationNo.Text.Trim();
                    _plObj.DbType = "DELETE";
                    DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
                    if (dataTable.Rows.Count > 0)
                    {
                        if (dataTable.Rows[0][0].ToString().StartsWith("Y"))
                        {
                            btnReset_Click(sender, e);
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Deleted successfully!!", 1);
                            frmModelMaster_Load(null, null);
                        }
                        else
                        {
                            GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, dataTable.Rows[0][0].ToString(), 3);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("conflicted with the REFERENCE constraint"))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "This is already in use!!!", 3);
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
                }
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
                txtStationNo.Text = "";
                txtStationName.Text = "";
                txtStationNo.Enabled = true;
                btnDelete.Enabled = false;
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }




        private void BindGrid()
        {
            try
            {
                _plObj = new PL_STATION_MASTER();
                _blObj = new BL_STATION_MASTER();
                _plObj.DbType = "SELECT";
                DataTable dt = _blObj.BL_ExecuteTask(_plObj);
                dgv.DataSource = dt;
                lblCount.Text = "Rows Count : " + dgv.Rows.Count;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        private bool ValidateInput()
        {
            try
            {
                if (txtStationNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Station No. can't be blank!!", 3);
                    txtStationNo.Focus();
                    txtStationNo.SelectAll();
                    return false;
                }
                if (txtStationName.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Staion Name can't be blank!!", 3);
                    txtStationName.Focus();
                    txtStationName.SelectAll();
                    return false;
                }
                if (dgv.Rows.Count==12)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Staion Can't be more than 12!!", 3);
                    txtStationName.Focus();
                    txtStationName.SelectAll();
                    return false;
                }

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

        #region Label Event

        #endregion

        #region DataGridView Events
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1)
                {
                    return;
                }
                Clear();
                txtStationNo.Text = dgv.Rows[e.RowIndex].Cells["Station_No"].Value.ToString();
                txtStationName.Text = dgv.Rows[e.RowIndex].Cells["Station_Name"].Value.ToString();
                btnDelete.Enabled = true;
                txtStationNo.Enabled = false;
                _IsUpdate = true;
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }

        #endregion

        #region TextBox Event





        #endregion

    }
}
