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
    public partial class FmSelectCustomer : DevExpress.XtraEditors.XtraForm
    {
        public FmSelectCustomer()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //DataTable dt = Util.DBQuery(string.Format(@"SELECT ID, Firstname, Lastname, IFNULL(Nickname, '') Nickname, IFNULL(CardNo, '') CardNo, IFNULL(CitizenID, '') CitizenID, 
            //        IFNULL(Mobile, '') Mobile, IFNULL(Sex, '') Sex, IFNULL(Birthday, '') Birthday, SellPrice
            //        FROM Customer
            //        WHERE Firstname LIKE '%{0}%'
            //        OR Lastname LIKE '%{0}%'
            //        OR Nickname LIKE '%{0}%'
            //        OR CardNo LIKE '%{0}%'
            //        OR CitizenID LIKE '%{0}%'
            //        OR Mobile LIKE '%{0}%'
            //        LIMIT 10", txtSearch.Text.Trim()));

            //table1.BeginUpdate();
            //tableModel1.Rows.Clear();
            //tableModel1.RowHeight = 22;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    var mobile = (dt.Rows[i]["Mobile"].ToString().Length == 10) ? dt.Rows[i]["Mobile"].ToString().Substring(0, 3) + "-" +
            //        dt.Rows[i]["Mobile"].ToString().Substring(3, 4) + "-" +
            //        dt.Rows[i]["Mobile"].ToString().Substring(7, 3)
            //        : dt.Rows[i]["Mobile"].ToString();
            //    var citizen = (dt.Rows[i]["CitizenID"].ToString().Length == 13) ? dt.Rows[i]["CitizenID"].ToString().Substring(0, 1) + "-" +
            //        dt.Rows[i]["CitizenID"].ToString().Substring(1, 4) + "-" +
            //        dt.Rows[i]["CitizenID"].ToString().Substring(5, 5) + "-" +
            //        dt.Rows[i]["CitizenID"].ToString().Substring(10, 2) + "-" +
            //        dt.Rows[i]["CitizenID"].ToString().Substring(12, 1)
            //        : dt.Rows[i]["CitizenID"].ToString();
            //    tableModel1.Rows.Add(new Row(
            //        new Cell[] {
            //            new Cell(dt.Rows[i]["ID"].ToString()),
            //            new Cell("" + (tableModel1.Rows.Count + 1)),
            //            new Cell(dt.Rows[i]["Firstname"].ToString()),
            //            new Cell(dt.Rows[i]["Lastname"].ToString()),
            //            new Cell(dt.Rows[i]["Nickname"].ToString()),
            //            new Cell(dt.Rows[i]["CardNo"].ToString()),
            //            new Cell(mobile),
            //            new Cell(citizen) ,
            //            new Cell(dt.Rows[i]["Birthday"].ToString()),
            //            new Cell(dt.Rows[i]["Sex"].ToString()),
            //            new Cell(dt.Rows[i]["SellPrice"].ToString())
            //        }));
            //}
            //table1.EndUpdate();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //if (table1.TableModel.Rows.Count > 0)
            //{
            //    try
            //    {
            //        var row = e.Row;
            //        var name = (table1.TableModel.Rows[row].Cells[0].Text == "000000") ? "ลูกค้าทั่วไป" :
            //            table1.TableModel.Rows[row].Cells[2].Text +
            //            ((table1.TableModel.Rows[row].Cells[4].Text != "") ? " (" + table1.TableModel.Rows[row].Cells[4].Text + ")" : "");
            //        Param.SelectCustomerId = table1.TableModel.Rows[row].Cells[0].Text;
            //        Param.SelectCustomerName = name;

            //        Param.SelectCustomerAge = table1.TableModel.Rows[row].Cells[8].Text == "" ? 0 : (int)DateTime.Today.Subtract(Convert.ToDateTime(table1.TableModel.Rows[row].Cells[8].Text).Date).TotalDays / 365;
            //        //Param.SelectCustomerAge = Param.SelectCustomerAge == DateTime.Today.Year ? 0 : Param.SelectCustomerAge;
            //        Param.SelectCustomerSex = table1.TableModel.Rows[row].Cells[9].Text;
            //        Param.SelectCustomerSellPrice = int.Parse(table1.TableModel.Rows[row].Cells[10].Text);

            //        Firstname = table1.TableModel.Rows[row].Cells[2].Text;
            //        Lastname = table1.TableModel.Rows[row].Cells[3].Text;
            //        Nickname = table1.TableModel.Rows[row].Cells[4].Text;
            //        tel = table1.TableModel.Rows[row].Cells[6].Text;

            //        this.DialogResult = DialogResult.OK;
            //        //this.Dispose();
            //    }
            //    catch
            //    {

            //    }
            //}
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && txtSearch.Text.Trim() != "")
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}