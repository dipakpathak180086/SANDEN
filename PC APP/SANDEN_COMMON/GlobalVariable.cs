using SatoLib;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SANDEN_COMMON
{
    public class GlobalVariable
    {
        public static SatoCustomFunction mStoCustomFunction = new SatoCustomFunction();
        public static string mSatoApps = "SatoApps";
        public static string mMainSqlConString = "";
        public static string mSatoDbServer = "";
        public static string mSatoDb = "";
        public static string mSatoDbUser = "";
        public static string mSatoDbPassword = "";
        public static string mSatoAppsLoginUser = "";
        public static string mUserType = "";
        public static SatoLogger AppLog;
        public static string UserName = "";
        public static string UserGroup = "";
        public static string mAccessUser = "";
        public static string mPrinterName = "";
        public static string mPrnFileName = "FG_BARCODE.PRN";
        public static string mRevNo = string.Empty;
        public static string mRevName = string.Empty;
        public static string mRevDate = string.Empty;
        public static string mInputFileName = "";
        public static string mOutputFileName = "";
        public static string mMacAddress = "";
        public static string mTraceOffPass = "";
        public static void allowOnlyNumeric(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        /// <summary>
        /// its used for encript the given string 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt_data(string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            str = new string(arr);
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(str));
        }
        /// <summary>
        /// its used for decrypt the given string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt_data(string str)
        {
            char[] arr = Encoding.Unicode.GetString(Convert.FromBase64String(str)).ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //log.Debug(
                //    "Found MAC Address: " + nic.GetPhysicalAddress() +
                //    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    // log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }
        public static void BindCombo(ComboBox comboBox, DataTable dataTable)
        {
            try
            {
                DataRow Drw;
                Drw = dataTable.NewRow();
                Drw.ItemArray = new object[] { 0, "--Select--" };
                dataTable.Rows.InsertAt(Drw, 0);
                comboBox.DataSource = dataTable.DefaultView;
                comboBox.ValueMember = dataTable.Columns[0].ColumnName;
                comboBox.DisplayMember = dataTable.Columns[1].ColumnName;

            }
            catch (Exception ex)
            {
                if (ex.Message == "ComboBox that has a DataSource set cannot be sorted") { return; }
                throw;
            }
        }
        public static void allowOnlyForLandline(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '-'))
            {
                e.Handled = false;
            }

        }
        public static void allowOnlyNumericAndDecimal(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[0-9]{10}$").Success;
        }
        public static bool IsEmailId(string email)
        {
            return Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success;
        }
        public static string DataTableToCsv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            var columnNames = dt.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            return sb.ToString().Replace("" + '"' + "", "");
        }
        public static void ExportDataInCSV(DataTable _dt)
        {
            string _FileName = string.Empty;
            SaveFileDialog sdb = new SaveFileDialog();
            sdb.InitialDirectory = @"C:\";
            sdb.Title = "Save text Files";
            if (sdb.ShowDialog() == DialogResult.OK)
            {
                _FileName = sdb.FileName;
            }
            else
            {
                return;
            }

            StreamWriter _sWriter = new StreamWriter(_FileName + ".csv");
            string _sData = "";
            try
            {

                for (int i = 0; i < _dt.Columns.Count; i++)
                {
                    if (_sData == "")
                    {
                        _sData = _dt.Columns[i].ColumnName.ToString().ToUpper();
                    }
                    else
                    {
                        _sData = _sData + "," + _dt.Columns[i].ColumnName.ToString().ToUpper();
                    }
                }
                _sWriter.WriteLine(_sData);

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    _sData = "";
                    for (int j = 0; j < _dt.Columns.Count; j++)
                    {
                        if (_sData == "")
                        {
                            _sData = _dt.Rows[i][j].ToString().ToUpper().Replace(",", "").Replace("\t", "").Replace("\n", "").Trim();
                        }
                        else
                        {
                            _sData = _sData + "," + _dt.Rows[i][j].ToString().ToUpper().Replace(",", "").Replace("\t", "").Replace("\n", "").Trim();
                        }
                    }
                    _sWriter.WriteLine(_sData);
                }
                MessageBox.Show("Data exported successfully at " + _FileName + ".csv", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sWriter.Close();
                _sWriter.Dispose();
            }
        }
        public static void ExportInCSV(DataGridView _dg)
        {
            string _FileName = string.Empty;
            SaveFileDialog sdb = new SaveFileDialog();
            sdb.InitialDirectory = @"C:\";
            sdb.Title = "Save text Files";
            if (sdb.ShowDialog() == DialogResult.OK)
            {
                _FileName = sdb.FileName;
            }
            else
            {
                return;
            }

            StreamWriter _sWriter = new StreamWriter(_FileName + ".csv");
            string _sData = "";
            try
            {

                for (int i = 0; i < _dg.ColumnCount; i++)
                {
                    if (_sData == "")
                    {
                        _sData = _dg.Columns[i].HeaderText.ToString().ToUpper().Replace(",", "").Replace("\t", "").Replace("\n", "").Trim();
                    }
                    else
                    {
                        _sData = _sData + "," + _dg.Columns[i].HeaderText.ToString().ToUpper().Replace(",", "").Replace("\t", "").Replace("\n", "").Trim();
                    }
                }
                _sWriter.WriteLine(_sData);

                for (int i = 0; i < _dg.Rows.Count; i++)
                {
                    _sData = "";

                    for (int j = 0; j < _dg.ColumnCount; j++)
                    {
                        if (_dg.Rows[i].Cells[j].Value == null)
                        {
                            _dg.Rows[i].Cells[j].Value = "";
                        }

                        if (_sData == "")
                        {
                            _sData = _dg.Rows[i].Cells[j].Value.ToString().ToUpper().Replace(",", "~").Replace("\t", "").Replace("\n", "").Trim();
                        }
                        else
                        {
                            _sData = _sData + "," + _dg.Rows[i].Cells[j].Value.ToString().ToUpper().Replace(",", "~").Replace("\t", "").Replace("\n", "").Trim();
                        }
                    }
                    _sWriter.WriteLine(_sData);
                }
                MessageBox.Show("Data exported successfully at " + _FileName + ".csv", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sWriter.Close();
                _sWriter.Dispose();
            }
        }
    }
}
