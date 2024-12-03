using SANDEN_BL;
using SANDEN_PC_APP;
using SANDEN_PL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SANDEN_COMMON;

namespace SANDEN_PC_APP
{
    public partial class frmUserMaster : Form
    {
        #region Variables

        private BL_USER_MASTER _blObj = null;
        private PL_USER_MASTER _plObj = null;
        private bool _IsUpdate = false;

        #endregion

        #region Form Methods

        public frmUserMaster()
        {
            try
            {
                InitializeComponent();
                _blObj = new BL_USER_MASTER();
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
                //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                btnDelete.Enabled = false;
                if (GlobalVariable.UserGroup.ToUpper() != "ADMIN")
                {
                    Common common = new Common();
                    common.SetModuleChildSectionRights(this.Text, _IsUpdate, btnSave, btnDelete);
                }
                GetGroup();
                GetUser();
                txtName.Focus();
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
                    _plObj = new PL_USER_MASTER();
                    _plObj.UserId = txtUserId.Text.Trim();
                    _plObj.Name = txtName.Text.Trim();
                    _plObj.Password = txtPassword.Text.Trim();
                    //_plObj.EmailId = txtEmailId.Text.Trim();
                    //_plObj.EmpCode = txtEmpCode.Text.Trim();
                    //_plObj.Designation = txtDesignation.Text.Trim();
                    _plObj.Active = chkActive.Checked;
                    _plObj.CreatedBy = GlobalVariable.mSatoAppsLoginUser;
                    _plObj.Group = cmbGroup.SelectedItem.ToString();
                    //If saving data
                    if (_IsUpdate == false)
                    {
                        _plObj.DbType = "INSERT";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
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
                    else // if updating data
                    {
                        _plObj.DbType = "UPDATE";
                        DataTable dataTable = _blObj.BL_ExecuteTask(_plObj);
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
                txtUserIdSearch.Text = "";
                Clear();
                GetUser();

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
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select user", 3);
                    return;
                }
                if (GlobalVariable.mStoCustomFunction.ConfirmationMsg(GlobalVariable.mSatoApps, "Äre you sure to delete the record !!"))
                {
                    _plObj = new PL_USER_MASTER();
                    _blObj = new BL_USER_MASTER();
                    _plObj.UserId = txtUserId.Text.Trim();
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
                txtUserId.Text = "";
                txtName.Text = "";
                txtPassword.Text = "";
                //txtEmailId.Text = "";
                //txtEmpCode.Text = "";
                //txtDesignation.Text = "";
                // //cmbGroup.SelectedIndex =  0;
                //lblPasswordPolicy.Visible = false;
                chkActive.Checked = true;
                txtUserId.Enabled = true;
                btnDelete.Enabled = false;
                _IsUpdate = false;
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }

        private void GetGroup()
        {
            try
            {
                Common common = new Common();
                cmbGroup.Items.Clear();
                cmbGroup.Items.Add("--Select--");
                DataTable dt = common.GetGroup();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbGroup.Items.Add(row["GroupName"].ToString());
                    }

                    cmbGroup.SelectedIndex = 0;
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "User group data not found", 3);
                }
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }


        private void GetUser()
        {
            try
            {
                _plObj = new PL_USER_MASTER();
                _blObj = new BL_USER_MASTER();
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
                if (txtName.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "User name can't be blank!!", 3);
                    txtName.Focus();
                    return false;
                }
                if (txtUserId.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "User Id can't be blank!!", 3);
                    txtUserId.Focus();
                    txtUserId.SelectAll();
                    return false;
                }

                if (txtPassword.Text.Trim().Length == 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Password can't be blank!!", 3);
                    txtPassword.Focus();
                    return false;
                }
                //if (txtPassword.Text.Trim().Length <= 4)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Password length should be greater than at least 4 characters!!", 3);
                //    txtPassword.Focus();
                //    return false;
                //}
                //if (txtEmailId.Text.Trim().Length == 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Email Id can't be blank!!", 3);
                //    txtEmailId.Focus();
                //    return false;
                //}
                //else
                //{
                //    //if (ClsGlobal.IsEmailId(txtEmailId.Text.Trim()) == false)
                //    //{
                //    //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps,"Invalid Email id!!", 3);
                //    //    txtEmailId.Focus();
                //    //    return false;
                //    //}
                //}
                //if (txtEmpCode.Text.Trim().Length == 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Employee Code can't be blank!!", 3);
                //    txtEmpCode.Focus();
                //    return false;
                //}
                //if (txtDesignation.Text.Trim().Length == 0)
                //{
                //    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Designation can't be blank!!", 3);
                //    txtDesignation.Focus();
                //    return false;
                //}
                if (cmbGroup.SelectedIndex <= 0)
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Please select Group!!", 3);
                    cmbGroup.Focus();
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
        private void dgvUserMaster_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1)
                {
                    return;
                }
                Clear();
                txtUserId.Text = dgv.Rows[e.RowIndex].Cells["UserId"].Value.ToString();
                txtName.Text = dgv.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
                txtPassword.Text = dgv.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                //txtEmailId.Text = dgv.Rows[e.RowIndex].Cells["EmailId"].Value.ToString();
                //txtEmpCode.Text = dgv.Rows[e.RowIndex].Cells["EmployeeCode"].Value.ToString();
                //txtDesignation.Text = dgv.Rows[e.RowIndex].Cells["Designation"].Value.ToString();
                cmbGroup.SelectedItem = dgv.Rows[e.RowIndex].Cells["GroupName"].Value.ToString();
                chkActive.Checked = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["IS_ACTIVE"].Value);
                btnDelete.Enabled = true;
                txtUserId.Enabled = false;
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

        private void txtUserIdSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dgv.DataSource as DataTable).DefaultView.RowFilter = string.Format("UserId LIKE '%{0}%' or UserName LIKE '%{0}%'", txtUserIdSearch.Text);
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3); ;
            }
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //lblPasswordPolicy.Visible = true;
            //if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.Blank)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.Red;
            //    lblPasswordPolicy.ForeColor = Color.Black;
            //    lblPasswordPolicy.Text = "Password is Blank!!";
            //    lblPasswordPolicy.Visible = false;
            //}
            //else if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.VeryWeak)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.Gold;
            //    lblPasswordPolicy.ForeColor = Color.Black;
            //    lblPasswordPolicy.Text = "Password is VeryWeak!!";

            //}
            //else if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.Weak)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.Yellow;
            //    lblPasswordPolicy.ForeColor = Color.Black;
            //    lblPasswordPolicy.Text = "Password is Weak!!";
            //}
            //else if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.Medium)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.YellowGreen;
            //    lblPasswordPolicy.ForeColor = Color.Black;
            //    lblPasswordPolicy.Text = "Password is Medium!!";
            //}
            //else if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.Strong)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.GreenYellow;
            //    lblPasswordPolicy.ForeColor = Color.Black;
            //    lblPasswordPolicy.Text = "Password is Strong!!";
            //}
            //else if (PasswordPolicy.CheckPasswordStrength(txtPassword.Text.Trim()) == PasswordScore.VeryStrong)
            //{
            //    lblPasswordPolicy.Text = "";
            //    lblPasswordPolicy.BackColor = Color.Green;
            //    lblPasswordPolicy.ForeColor = Color.WhiteSmoke;
            //    lblPasswordPolicy.Text = "Password is VeryStrong!!";
            //}
        }



        #endregion




    }
}
