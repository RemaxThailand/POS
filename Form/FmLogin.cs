using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace PowerPOS
{
    public partial class FmLogin : DevExpress.XtraEditors.XtraForm
    {
        public FmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var dt = Util.DBQuery(string.Format(@"
                SELECT employeeId, employeeType 
                FROM Employee
                WHERE LOWER(username) = '{0}'
                    AND password = '{1}'
            ", txtUsername.Text.ToLower(), Util.MD5String(txtPassword.Text)));

            if(dt.Rows.Count > 0)
            {
                Param.EmployeeId = dt.Rows[0]["employeeId"].ToString();
                Param.EmployeeType = dt.Rows[0]["employeeType"].ToString();
                Param.UserId = Param.EmployeeId;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ไม่สามารถเข้าสู่ระบบได้\nเนื่องจากชื่อผู้ใช้ หรือรหัสผ่านไม่ถูกต้อง", "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                if (txtUsername.Text != "")
                {
                    txtPassword.Focus();
                }
                else
                {
                    MessageBox.Show("กรุณากรอกข้อมูลชื่อผู้ใช้", "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                if (txtPassword.Text != "")
                {
                    btnLogin_Click(sender, (e));
                }
                else
                {
                    MessageBox.Show("กรุณากรอกข้อมูลรหัสผ่าน", "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 65 && (int)e.KeyChar <= 122) || (int)e.KeyChar == 8 || (int)e.KeyChar == 13 || (int)e.KeyChar == 46)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}