using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Threading;
using System.Globalization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerPOS
{
    public partial class UcClaimShop : DevExpress.XtraEditors.XtraUserControl
    {
        public static DataTable  _TABLE_CLAIM, _TABLE_DATA;
        public static string data, dat;

        public UcClaimShop()
        {
            InitializeComponent();
        }

        private void textBarcodeSwap_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnBarcodeWarr_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void UcClaimShop_Load(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //dt.Columns.Add("FirstName");
            //dt.Columns.Add("Age");

            //dt.Rows.Add("rambo", 60);
            //dt.Rows.Add("Arnie", 35);
            //bindingSource1.DataSource = dt;
            //gridControl1.DataSource = bindingSource1;
            //gridView1.RefreshData();

            //gridView1.Columns.Add(
            //    new DevExpress.XtraGrid.Columns.GridColumn()
            //    {
            //        Caption = "Selected",
            //        ColumnEdit = new RepositoryItemCheckEdit() { },
            //        VisibleIndex = 0,
            //        UnboundType = DevExpress.Data.UnboundColumnType.Boolean
            //    }
            //    );
            LoadData();
            cbbReport.SelectedIndex = 0;
            cbbRStatus.SelectedIndex = 0;
            cbbStatus.SelectedIndex = 0;

        }

        public void LoadData()
        {
            DataTable dt, dtQty;
            DataRow row;
            int i, a;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                _TABLE_CLAIM = Util.DBQuery(string.Format(@"
                SELECT s.shop, s.shopName, bc.orderNo, s.name FROM BarcodeClaim bc
                LEFT JOIN Shop s
                ON s.shop= bc.shop
                WHERE bc.receivedDate IS NOT NULL
                AND bc.status = 0
                GROUP BY bc.shop, bc.orderNo
                ORDER BY bc.shop"));

                claimGridControl.DataSource = null;
                claimGridView.OptionsBehavior.AutoPopulateColumns = false;
                claimGridControl.MainView = claimGridView;
                //var sumPrice = 0.0;
                //var sumProfit = 0.0;
                dt = new DataTable();
                for (i = 0; i < ((ColumnView)claimGridControl.MainView).Columns.Count; i++)
                {
                    dt.Columns.Add(claimGridView.Columns[i].FieldName);
                }
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

                for (a = 0; a < _TABLE_CLAIM.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = _TABLE_CLAIM.Rows[a]["shop"].ToString();
                    row[1] = _TABLE_CLAIM.Rows[a]["shopName"].ToString();
                    row[2] = _TABLE_CLAIM.Rows[a]["orderNo"].ToString();
                    row[3] = _TABLE_CLAIM.Rows[a]["name"].ToString();
                    dt.Rows.Add(row);
                }

                claimGridControl.DataSource = dt;

                lblListCount.Text = claimGridView.RowCount.ToString() + " รายการ";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


                DataTable dtab = Util.DBQuery(@"SELECT DISTINCT s.shop, s.name, s.shopName FROM Shop s
                        LEFT JOIN BarcodeClaim bc
                        ON s.shop = bc.shop
                        ORDER BY s.name");

                cbbRShop.Properties.Items.Clear();
                cbbRShop.Properties.Items.Add("--เลือกสาขา--");
                if (dtab.Rows.Count == 0)
                {
                    cbbRShop.Enabled = false;
                }
                else
                {
                    cbbRShop.Enabled = true;
                    for (int v = 0; v < dtab.Rows.Count; v++)
                    {
                        cbbRShop.Properties.Items.Add(dtab.Rows[v]["shopName"].ToString());
                    }
                }
                cbbRShop.SelectedIndex = 0;

               

                //dtQty = Util.DBQuery(string.Format(@"SELECT IFNULL(SUM(d.Quantity),0) QTY FROM SellHeader h
                //            LEFT JOIN SellDetail d
                //            ON h.SellNo = d.SellNo 
                //            WHERE h.SellDate LIKE '{0}%'
                //              AND h.Customer = '000002'
                //              AND (h.Comment <> 'คืนสินค้า' OR h.Comment IS Null)
                //              AND h.sellNo NOT LIKE '%CL%'
                //            ORDER BY SellDate DESC
                //         ", dtpDate.Value.ToString("yyyy-MM-dd"), Param.UserId));

                //lblProductCount.Text = dtQty.Rows[0]["QTY"].ToString() + " ชิ้น";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void claimGridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            //DataTable dtl, dtQty;
            //DataRow rowl;
            //int i, a;
            //string _orderNo, _shop;

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //try
            //{
            //    _orderNo = claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["ClaimNo"]).ToString();
            //    _shop = claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["ID"]).ToString();


            //    _TABLE_DATA = Util.DBQuery(string.Format(@"
            //    SELECT p.name,  bc.barcode FROM BarcodeClaim bc
            //    LEFT JOIN Product p
            //    ON p.product = bc.product
            //    WHERE bc.receivedDate IS NOT NULL
            //    AND bc.orderNo = '{0}'
            //    AND bc.shop = '{1}'
            //    GROUP BY bc.shop, bc.orderNo
            //    ORDER BY bc.shop", _orderNo, _shop));

            //    listGridView.OptionsBehavior.AutoPopulateColumns = false;
            //    listGridControl.MainView = listGridView;

            //    dtl = new DataTable();
            //    for (i = 0; i < ((ColumnView)listGridControl.MainView).Columns.Count; i++)
            //    {
            //        dtl.Columns.Add(listGridView.Columns[i].FieldName);
            //    }
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            //    for (a = 0; a < _TABLE_DATA.Rows.Count; a++)
            //    {
            //        rowl = dtl.NewRow();
            //        //rowl[0] = "1";
            //        rowl[1] = _TABLE_DATA.Rows[a]["name"].ToString();
            //        rowl[2] = _TABLE_DATA.Rows[a]["barcode"].ToString();
            //        dtl.Rows.Add(rowl);
            //    }

            //    listGridControl.DataSource = dtl;

            //    LoadData();

            //    //lblListCount.Text = listGridView.RowCount.ToString() + " รายการ";
            //    //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbbStatus.SelectedIndex == 0)
            {
                MessageBox.Show("กรุณาเลือกสถานะการเคลมสินค้า", "แจ้งเตือน");
            }
            else
            { 
                if (cbbStatus.SelectedIndex == 1)
                {
                    if (txtComment.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง", "แจ้งเตือน");
                    }
                    else
                    {
                        if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะบันทึกการเคลมนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Util.DBExecute(string.Format(@"UPDATE BarcodeClaim SET Status = '{0}' , comment = '{1}', claimDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), claimBy= {2}, priceClaim = '0', barcodeClaim ='', posClaim = '', syncClaim = 1 WHERE barcode = '{3}'", cbbStatus.SelectedIndex.ToString(), txtComment.Text, Param.UserId, listGridView.GetRowCellDisplayText(listGridView.FocusedRowHandle, listGridView.Columns["Barcode"]), txtPrice.Text, txtBarcodeClaim.Text, cbbPosNo.SelectedItem.ToString()));

                            LoadData();
                            txtPrice.Text = "";
                            txtBarcodeClaim.Text = "";
                            txtComment.Text = "";
                            cbbStatus.SelectedIndex = 0;

                        }
                    }
                }
                else if (cbbStatus.SelectedIndex == 2)
                {
                    if (txtBarcodeClaim.Text == "" || cbbPosNo.SelectedIndex == 0)
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง", "แจ้งเตือน");
                    }
                    else
                    {
                        if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะบันทึกการเคลมนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Util.DBExecute(string.Format(@"UPDATE BarcodeClaim SET Status = '{0}' , comment = '{1}', claimDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), claimBy= {2}, priceClaim = '0', barcodeClaim ='{5}', posClaim = '{6}', syncClaim = 1 WHERE barcode = '{3}'", cbbStatus.SelectedIndex.ToString(), txtComment.Text, Param.UserId, listGridView.GetRowCellDisplayText(listGridView.FocusedRowHandle, listGridView.Columns["Barcode"]), txtPrice.Text, txtBarcodeClaim.Text, cbbPosNo.SelectedItem.ToString()));

                            LoadData();
                            txtPrice.Text = "";
                            txtBarcodeClaim.Text = "";
                            txtComment.Text = "";
                            cbbStatus.SelectedIndex = 0;

                        }
                    }
                }
                else if (cbbStatus.SelectedIndex == 3)
                {
                    if (txtPrice.Text == "")
                    {
                        MessageBox.Show("กรุณากรอกข้อมูลให้ถูกต้อง", "แจ้งเตือน");
                    }
                    else
                    {
                        if (MessageBox.Show("คุณแน่ใจหรือไม่ ที่จะบันทึกการเคลมนี้ ?", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Util.DBExecute(string.Format(@"UPDATE BarcodeClaim SET Status = '{0}' , comment = '{1}', claimDate = STRFTIME('%Y-%m-%d %H:%M:%S', 'NOW'), claimBy= {2}, priceClaim = '{4}', barcodeClaim ='', posClaim = '', syncClaim = 1 WHERE barcode = '{3}'", cbbStatus.SelectedIndex.ToString(), txtComment.Text, Param.UserId, listGridView.GetRowCellDisplayText(listGridView.FocusedRowHandle, listGridView.Columns["Barcode"]), txtPrice.Text, txtBarcodeClaim.Text, cbbPosNo.SelectedItem.ToString()));

                            LoadData();
                            txtPrice.Text = "";
                            txtBarcodeClaim.Text = "";
                            txtComment.Text = "";
                            cbbStatus.SelectedIndex = 0;

                        }
                    }
                }
            }
        }

        private void listGridControl_Click(object sender, EventArgs e)
        {
            //lbl.Text = listGridView.GetRowCellDisplayText(listGridView.FocusedRowHandle, listGridView.Columns["Barcode"]);

            DataTable dtl, dtQty;
            DataRow rowl;
            int i, a;
            string _orderNo, _shop;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                DataTable BDT;
                BDT = Util.DBQuery(string.Format(@"
                SELECT * FROM BarcodeClaim bc WHERE bc.barcode = '{0}'", listGridView.GetRowCellDisplayText(listGridView.FocusedRowHandle, listGridView.Columns["Barcode"])));

                cbbStatus.SelectedIndex = int.Parse(BDT.Rows[0]["status"].ToString());
                txtBarcodeClaim.Text = BDT.Rows[0]["barcodeClaim"].ToString();
                txtPrice.Text = BDT.Rows[0]["priceClaim"].ToString();
                txtComment.Text = BDT.Rows[0]["comment"].ToString();
                lblCost.Text = BDT.Rows[0]["cost"].ToString();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void cbbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbStatus.SelectedIndex == 1)
            {
                txtBarcodeClaim.Enabled = false;
                txtPrice.Enabled = false;
                txtComment.Enabled = true;
                cbbPosNo.Enabled = false;
                dtpClaim.Enabled = false;
            }
            else if (cbbStatus.SelectedIndex == 2)
            {
                txtBarcodeClaim.Enabled = true;
                txtPrice.Enabled = false;
                txtComment.Enabled = true;
                cbbPosNo.Enabled = true;
                dtpClaim.Enabled = true;
            }
            else if (cbbStatus.SelectedIndex == 3)
            {
                txtBarcodeClaim.Enabled = false;
                txtPrice.Enabled = true;
                txtComment.Enabled = true;
                cbbPosNo.Enabled = false;
                dtpClaim.Enabled = false;
            }
            else
            {
                txtBarcodeClaim.Enabled = false;
                txtPrice.Enabled = false;
                txtComment.Enabled = false;
                cbbPosNo.Enabled = false;
                dtpClaim.Enabled = false;
            }
        }

        private void listGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView gView = sender as GridView;
            if (e.Column.FieldName == "Status")
            {
                string status = gView.GetRowCellDisplayText(e.RowHandle, gView.Columns["Status"]);
                switch (status)
                {
                    case "คืนของผิดเงื่อนไข":
                        e.Appearance.BackColor = Color.PaleVioletRed;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "เปลี่ยนสินค้า":
                        e.Appearance.BackColor = Color.CornflowerBlue;
                        e.Appearance.ForeColor = Color.White;
                        break;
                    case "ลดหนี้/คืนเงิน":
                        e.Appearance.BackColor = Color.MediumSeaGreen;
                        e.Appearance.ForeColor = Color.White;
                        break;
                }
            }
        }

        private void listGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    string status = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
            //    if (status == "คืนของผิดเงื่อนไข")
            //    {
            //        e.Appearance.BackColor = Color.PaleVioletRed;
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //    else if (status == "เปลี่ยนสินค้า")
            //    {
            //        e.Appearance.BackColor = Color.CornflowerBlue;
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //    else if (status == "คืนเงิน")
            //    {
            //        e.Appearance.BackColor = Color.MediumSeaGreen;
            //        e.Appearance.ForeColor = Color.White;
            //    }
            //}
        }

        private void cbbPosNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpClaim_ValueChanged(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            DataTable dtP = Util.DBQuery(string.Format(@"SELECT sh.sellNo 
                    FROM SellHeader sh
                    LEFT JOIN Customer c
                    ON c.customer = sh.customer
                    WHERE c.shopName LIKE '%{0}%'
                    AND sh.SellDate LIKE '{1}%'
                    ORDER BY  sh.sellNo DESC", claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["code"]).ToString(), dtpClaim.Value.ToString("yyyy-MM-dd")));

            cbbPosNo.Properties.Items.Clear();
            cbbPosNo.Properties.Items.Add("เลขที่บิลเคลม");
            if (dtP.Rows.Count == 0)
            {
                cbbPosNo.Enabled = false;
            }
            else
            {
                cbbPosNo.Enabled = true;
                for (int b = 0; b < dtP.Rows.Count; b++)
                {
                    cbbPosNo.Properties.Items.Add(dtP.Rows[b]["sellNo"].ToString());
                }
            }
            cbbPosNo.SelectedIndex = 0;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (cbbReport.SelectedIndex != 0)
            {
                miPrintReceipt_Click(sender, (e));
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรูปแบบรายงานที่ต้องการพิมพ์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void miPrintReceipt_Click(object sender, EventArgs e)
        {
            if (cbbReport.SelectedIndex != 0)
            {
                dat = dtpDate.Value.ToString("yyyy-MM-dd");

                if (cbbRStatus.SelectedIndex != 0)
                {
                    Util.PrintReportStatusClaim(cbbRStatus.SelectedIndex.ToString(), dat);
                }
                else if (cbbRclaimNo.SelectedIndex != 0)
                {
                    Util.PrintReportClaim(cbbRclaimNo.SelectedItem.ToString(), cbbRShop.SelectedItem.ToString());
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการพิมพ์ใบเสร็จรับเงิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cbbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReport.SelectedIndex == 1)
            {
                cbbRStatus.Enabled = false;
                cbbRShop.Enabled = true;
                dtpDate.Enabled = true;
                cbbRclaimNo.Enabled = true;
                cbbRStatus.SelectedIndex = 0;
            }
            else if (cbbReport.SelectedIndex == 2)
            {
                cbbRStatus.Enabled = true;
                cbbRShop.Enabled = false;
                cbbRclaimNo.Enabled = false;
                dtpDate.Enabled = true;
                cbbRShop.SelectedIndex = 0;
                cbbRclaimNo.SelectedIndex = 0;

            }
            else
            {
                cbbRStatus.Enabled = false;
                cbbRShop.Enabled = false;
                cbbRclaimNo.Enabled = false;
                dtpDate.Enabled = false;
                cbbRShop.SelectedIndex = 0;
                cbbRclaimNo.SelectedIndex = 0;
                cbbRStatus.SelectedIndex = 0;
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            cbbRShop_SelectedIndexChanged(sender, (e));
        }

        private void cbbRShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtC = Util.DBQuery(string.Format(@"SELECT DISTINCT bc.orderNo FROM BarcodeClaim bc
                        LEFT JOIN Shop s
                        ON s.shop = bc.shop
                        WHERE s.shopName = '{0}'
                        AND bc.receivedDate IS NOT NULL
                        AND bc.ClaimDate LIKE '{1}%'
                        ORDER BY bc.orderNo ", cbbRShop.SelectedItem.ToString(), dtpDate.Value.ToString("yyyy-MM-dd")));

            cbbRclaimNo.Properties.Items.Clear();
            cbbRclaimNo.Properties.Items.Add("--เลือกเลขที่บิลเตลม--");
            if (dtC.Rows.Count == 0)
            {
                cbbRclaimNo.Enabled = false;
            }
            else
            {
                cbbRclaimNo.Enabled = true;
                for (int c = 0; c < dtC.Rows.Count; c++)
                {
                    cbbRclaimNo.Properties.Items.Add(dtC.Rows[c]["orderNo"].ToString());
                }
            }
            cbbRclaimNo.SelectedIndex = 0;
        }

        private void claimGridControl_Click(object sender, EventArgs e)
        {
            DataTable dtl, dtQty;
            DataRow rowl;
            int i, a;
            string _orderNo, _shop;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                _orderNo = claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["ClaimNo"]).ToString();
                _shop = claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["ID"]).ToString();


                _TABLE_DATA = Util.DBQuery(string.Format(@"
                SELECT p.name,  bc.barcode, bc.status FROM BarcodeClaim bc
                LEFT JOIN Product p
                ON p.product = bc.product
                WHERE bc.receivedDate IS NOT NULL
                AND bc.orderNo = '{0}'
                AND bc.shop = '{1}'
                ORDER BY p.name", _orderNo, _shop));

                listGridView.OptionsBehavior.AutoPopulateColumns = false;
                listGridControl.MainView = listGridView;

                dtl = new DataTable();
                for (i = 0; i < ((ColumnView)listGridControl.MainView).Columns.Count; i++)
                {
                    dtl.Columns.Add(listGridView.Columns[i].FieldName);
                }
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

                for (a = 0; a < _TABLE_DATA.Rows.Count; a++)
                {
                    rowl = dtl.NewRow();
                    //rowl[0] = "1";
                    rowl[1] = _TABLE_DATA.Rows[a]["name"].ToString();
                    rowl[2] = _TABLE_DATA.Rows[a]["barcode"].ToString();
                    if (_TABLE_DATA.Rows[a]["status"].ToString() == "1")
                    {
                        rowl[3] = "คืนของผิดเงื่อนไข";
                    }
                    else if (_TABLE_DATA.Rows[a]["status"].ToString() == "2")
                    {
                        rowl[3] = "เปลี่ยนสินค้า";
                    }
                    else if (_TABLE_DATA.Rows[a]["status"].ToString() == "3")
                    {
                        rowl[3] = "ลดหนี้/คืนเงิน";
                    }
                    else
                    {
                        rowl[3] = "";
                    }
                    dtl.Rows.Add(rowl);
                }

                listGridControl.DataSource = dtl;

                DataTable dtP = Util.DBQuery(string.Format(@"SELECT sh.sellNo 
                    FROM SellHeader sh
                    LEFT JOIN Customer c
                    ON c.customer = sh.customer
                    WHERE c.shopName LIKE '%{0}%'
                    AND sh.SellDate LIKE '{1}%'
                    ORDER BY  sh.sellNo DESC", claimGridView.GetRowCellDisplayText(claimGridView.FocusedRowHandle, claimGridView.Columns["code"]).ToString(), dtpClaim.Value.ToString("yyyy-MM-dd")));

                cbbPosNo.Properties.Items.Clear();
                cbbPosNo.Properties.Items.Add("เลขที่บิลเคลม");
                if (dtP.Rows.Count == 0)
                {
                    cbbPosNo.Enabled = false;
                }
                else
                {
                    cbbPosNo.Enabled = true;
                    for (int b = 0; b < dtP.Rows.Count; b++)
                    {
                        cbbPosNo.Properties.Items.Add(dtP.Rows[b]["sellNo"].ToString());
                    }
                }
                cbbPosNo.SelectedIndex = 0;

                //LoadData();

                //lblListCount.Text = listGridView.RowCount.ToString() + " รายการ";
                //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
