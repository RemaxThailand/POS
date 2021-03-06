﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System.Globalization;

namespace PowerPOS
{
    public partial class FmSaleDetial : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt, _TABLE_SALE;
        int i, a;
        DataRow row;
        public FmSaleDetial()
        {
            InitializeComponent();
        }

        private void FmSaleDetial_Load(object sender, EventArgs e)
        {
            //_TABLE_SALE = Util.DBQuery(string.Format(@"SELECT p.Product, p.Name, p.Price{2} Price, b.ProductCount, sd.SellPrice, sh.SellNo, sh.SellDate, sh.TotalPrice, c.Firstname||' '||c.Lastname name
            //        FROM (SELECT Product, COUNT(*) ProductCount,SellNo FROM Barcode WHERE SellBy = '{0}' AND SellNo = '{3}' GROUP BY Product,SellNo) b 
            //            LEFT JOIN Product p 
            //            ON b.Product = p.Product
            //            LEFT JOIN SellDetail sd
            //            ON b.Product = sd.Product
            //            LEFT JOIN SellHeader sh
            //            ON sd.SellNo = sh.SellNo
            //            LEFT JOIN Customer c
            //            ON sh.Customer = c.Customer
            //        WHERE p.Shop = '{1}' AND sd.SellNo = '{3}'
            //        GROUP BY b.Product,b.SellNo", Param.UserId, Param.ShopId, Param.SelectCustomerSellPrice == 0 ? "" : "" + Param.SelectCustomerSellPrice, UcReport.sellNo));

            _TABLE_SALE = Util.DBQuery(string.Format(@"
                    SELECT p.Product , p.Name, sd.Quantity, sd.SellPrice, sh.SellNo, sh.SellDate, sh.TotalPrice, c.Firstname||' '||c.Lastname customer, p.Sku
                     FROM  SellDetail sd
                        LEFT JOIN Product p 
                        ON sd.Product = p.Product 
                         LEFT JOIN SellHeader sh
                        ON sd.SellNo = sh.SellNo
                        LEFT JOIN Customer c
                        ON sh.Customer = c.Customer 
                    WHERE sd.SellNo = '{0}'
                        AND sd.Quantity <> 0", Param.SellNo));

            if(_TABLE_SALE.Rows.Count > 0)
            { 
            //var sumPrice = 0;
            lblCustomer.Text = _TABLE_SALE.Rows[0]["customer"].ToString();
            lblSellDate.Text = Convert.ToDateTime(_TABLE_SALE.Rows[0]["SellDate"].ToString()).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
            lblSellNo.Text = _TABLE_SALE.Rows[0]["SellNo"].ToString();
            lblTotal.Text = double.Parse(_TABLE_SALE.Rows[0]["TotalPrice"].ToString()).ToString("#,##0.00");

            saleGridView.OptionsBehavior.AutoPopulateColumns = false;
            saleGridControl.MainView = saleGridView;

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)saleGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(saleGridView.Columns[i].FieldName);
            }

                if (_TABLE_SALE.Rows.Count > 0)
                {
                    for (a = 0; a < _TABLE_SALE.Rows.Count; a++)
                    {
                        var total = int.Parse(_TABLE_SALE.Rows[a]["SellPrice"].ToString()) / int.Parse(_TABLE_SALE.Rows[a]["Quantity"].ToString());
                        row = dt.NewRow();
                        row[0] = (a + 1) * 1;
                        row[1] = _TABLE_SALE.Rows[a]["Product"].ToString();
                        row[2] = _TABLE_SALE.Rows[a]["Name"].ToString();
                        row[3] = total.ToString("#,##0");
                        row[4] = Convert.ToInt32(_TABLE_SALE.Rows[a]["Quantity"].ToString()).ToString("#,##0");
                        row[5] = Convert.ToInt32(_TABLE_SALE.Rows[a]["SellPrice"].ToString()).ToString("#,##0");
                        row[6] = _TABLE_SALE.Rows[a]["Sku"].ToString();
                        dt.Rows.Add(row);
                    }

                    saleGridControl.DataSource = dt;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}