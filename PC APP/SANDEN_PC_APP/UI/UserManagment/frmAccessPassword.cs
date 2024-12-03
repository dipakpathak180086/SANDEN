using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SANDEN_COMMON;
using SANDEN_PC_APP;

namespace SANDEN_PC_APP
{
    public partial class frmAccessPassword : Form
    {
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsCancel { get; set; }


        public frmAccessPassword()
        {
            InitializeComponent();

        }
        private void Show_ToolTip(Control ctr, string msg)
        {
            toolTip1.SetToolTip(ctr, msg);
            toolTip1.Show(msg, ctr, 2000);

        }
        private void bltOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim().ToUpper() == "ADMIN" && GlobalVariable.mTraceOffPass == txtpwd.Text.Trim())
                {
                    IsCancel = true;
                }
                else
                {
                    GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, "Invalid User Name or Password!!!", 2);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                GlobalVariable.mStoCustomFunction.setMessageBox(GlobalVariable.mSatoApps, ex.Message, 3);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            IsCancel = false;
        }


    }
}
