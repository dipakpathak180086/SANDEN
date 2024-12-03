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
    public partial class frmPartMaster : Form
    {


        #region Variables

        private BL_PART_MASTER _blObj = null;
        private PL_PART_MASTER _plObj = null;
        private bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmPartMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_PART_MASTER();
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
                txtModelNo.Focus();
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
                    _plObj = new PL_PART_MASTER();
                    _plObj.Part_No = txtPartNo.Text.Trim();
                    _plObj.Description = txtDisName.Text.Trim();
                    //if (cbPartType.SelectedItem.Equals("CHILD") || chkSubChild.Checked)
                    //{
                        _plObj.PackSize = Convert.ToInt32(txtPackSize.Text.Trim());
                    //}
                    //_plObj.PartType = cbPartType.Text.Trim();
                    _plObj.Model_No = txtModelNo.Text.Trim();
                    _plObj.Model_Char= txtModelChar.Text.Trim();
                    _plObj.BOP_No = txtBOPNo.Text.Trim();
                    _plObj.Cust_Part_No = txtCustPartNo.Text.Trim();
                    _plObj.Is_Active = chkActive.Checked;
                    //_plObj.Is_Sub_Child = chkSubChild.Checked;
                    //_plObj.Is_Sub_Parent = chkSubParent.Checked;
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
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
                }
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtPartSearch.Text = "";
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
                if (string.IsNullOrEmpty(txtPartNo.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Part No. can't be blank!!", 3);
                    return;
                }
                if (GlobalVariable.mStoCustomFunction.ConfirmationMsg(GlobalVariable.mSatoApps, "Äre you sure to delete the record !!"))
                {
                    _plObj = new PL_PART_MASTER();
                    _blObj = new BL_PART_MASTER();
                    _plObj.Part_No = txtPartNo.Text.Trim();
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
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                GlobalVariable.ExportInCSV(dgv);
            }
            catch (Exception ex)
            {

                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        #endregion

        #region Methods

        private void Clear()
        {
            try
            {
                txtPartNo.Text = "";
                txtDisName.Text = "";
                txtPackSize.Text = "";
                //cbPartType.SelectedIndex = 1;
                txtModelNo.Text = "";
                txtModelChar.Text = "";
                txtBOPNo.Text = "";
                txtModelChar.Text = "";
                txtModelNo.Text = "";
                txtCustPartNo.Text = "";
                chkActive.Checked = true;
                txtPartNo.Enabled = true;
                btnDelete.Enabled = false;
                //chkSubParent.Checked = chkSubChild.Checked = false;
                txtPartNo.Focus();
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
                _plObj = new PL_PART_MASTER();
                _blObj = new BL_PART_MASTER();
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
                //if (cbPartType.SelectedIndex <= 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Part Type can't be blank!!", 3);
                //    cbPartType.Focus();
                //    return false;
                //}
                if (txtModelNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Model No. can't be blank!!", 3);
                    txtModelNo.Focus();
                    txtModelNo.SelectAll();
                    return false;
                }
                if (txtPartNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Part No. can't be blank!!", 3);
                    txtPartNo.Focus();
                    return false;
                }
                if (txtDisName.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Part Description can't be blank!!", 3);
                    txtPartNo.Focus();
                    txtPartNo.SelectAll();
                    return false;
                }
                //if (cbPartType.SelectedItem.Equals("CHILD") || chkSubChild.Checked)
                //{
                    if (txtPackSize.Text.Trim().Length == 0)
                    {
                        GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Pack Size can't be blank!!", 3);
                        txtPackSize.Focus();
                        return false;
                    }
                //}
                
                //if (txtModelChar.Text.Trim().Length == 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Model Char can't be blank!!", 3);
                //    txtModelChar.Focus();
                //    txtModelChar.SelectAll();
                //    return false;
                //}
                if (txtBOPNo.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "BOP No. can't be blank!!", 3);
                    txtBOPNo.Focus();
                    txtBOPNo.SelectAll();
                    return false;
                }
                //if (txtCustPartNo.Text.Trim().Length == 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Customer Part No. can't be blank!!", 3);
                //    txtCustPartNo.Focus();
                //    txtCustPartNo.SelectAll();
                //    return false;
                //}
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
                txtPartNo.Text = dgv.Rows[e.RowIndex].Cells["PART_NO"].Value.ToString();
                txtDisName.Text = dgv.Rows[e.RowIndex].Cells["DESCRIPTION"].Value.ToString();
                txtPackSize.Text = dgv.Rows[e.RowIndex].Cells["PACK_SIZE"].Value.ToString();
                //cbPartType.Text = dgv.Rows[e.RowIndex].Cells["PART_TYPE"].Value.ToString();
                txtModelNo.Text = dgv.Rows[e.RowIndex].Cells["MODEL_NO"].Value.ToString();
                txtModelChar.Text = dgv.Rows[e.RowIndex].Cells["MODEL_CHAR"].Value.ToString();
                txtBOPNo.Text = dgv.Rows[e.RowIndex].Cells["BOP_NO"].Value.ToString();
                txtCustPartNo.Text = dgv.Rows[e.RowIndex].Cells["CUSTOMER_PART_NO"].Value.ToString();
                chkActive.Checked =Convert.ToBoolean( dgv.Rows[e.RowIndex].Cells["IS_ACTIVE"].Value);
                //chkSubParent.Checked = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["IS_SUB_PARENT"].Value);
                //chkSubChild.Checked = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["IS_SUB_CHILD"].Value);
                btnDelete.Enabled = true;
                txtPartNo.Enabled = false;
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

        private void txtPartSearch_TextChanged(object sender, EventArgs e)
        {
            (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("PART_NO LIKE '%{0}%'", txtPartSearch.Text);
        }
        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            GlobalVariable.allowOnlyNumeric(sender, e);
        }


        //private void cbPartType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cbPartType.SelectedItem.ToString() == "PARENT")
        //    {
        //        txtPackSize.Enabled = false;
        //        chkSubParent.Enabled = false;
        //        chkSubChild.Enabled = true;
        //        txtPartNo.Focus();

        //    }
        //    else
        //    {
        //        chkSubParent.Enabled = true;
        //        chkSubChild.Enabled = false;
        //        txtPackSize.Enabled = true;
        //        txtPartNo.Focus();
        //    }
        //}

        //private void chkSubParent_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkSubParent.Checked)
        //    {
        //        chkSubChild.Checked = false;
        //    }
        //}

        //private void chkSubChild_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkSubChild.Checked)
        //    {
        //        chkSubParent.Checked = false;
        //        txtPackSize.Enabled = true;
        //    }
        //}



        #endregion


    }
}
