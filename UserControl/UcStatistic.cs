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
using System.Threading;
using System.Globalization;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid.Views.Grid;

namespace PowerPOS
{
    public partial class UcStatistic : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_REPORT;
        public UcStatistic()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            DataTable dt;
            DataRow row;
            int i, a;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (cbbType.SelectedItem.ToString() == "สินค้าขายดี")
            {
                _TABLE_REPORT = Util.DBQuery(string.Format(@"
                SELECT sd.Product, p.Name,  c.Name category, b.Name brand,COUNT(sd.product) cnt FROM SellDetail sd
                LEFT JOIN Product p
                ON sd.product = p.product
                AND p.shop = '{0}' 
                LEFT JOIN SellHeader sh
                ON sh.SellNo = sd.SellNo
                LEFT JOIN Brand b
                ON b.Brand = p.Brand
                LEFT JOIN Category c
                ON c.Category = p.Category
                WHERE SUBSTR(sh.SellDate,1,10) BETWEEN '{2}' AND '{3}'
                GROUP BY sd.Product
                ORDER BY cnt DESC
                LIMIT {1}
            ", Param.ShopId, spCount.Value.ToString(), dtpStartDate.Value.ToString("yyyy-MM-dd"), dtpEndDate.Value.ToString("yyyy-MM-dd")));
            }
            else if (cbbType.SelectedItem.ToString() == "สินค้าขายไม่ดี")
            {
                _TABLE_REPORT = Util.DBQuery(string.Format(@"
                SELECT sd.Product, p.Name,  c.Name category, b.Name brand,COUNT(sd.product) cnt FROM SellDetail sd
                LEFT JOIN Product p
                ON sd.product = p.product
                AND p.shop = '{0}' 
                LEFT JOIN SellHeader sh
                ON sh.SellNo = sd.SellNo
                LEFT JOIN Brand b
                ON b.Brand = p.Brand
                LEFT JOIN Category c
                ON c.Category = p.Category
                WHERE SUBSTR(sh.SellDate,1,10) BETWEEN '{2}' AND '{3}'
                GROUP BY sd.Product
                ORDER BY cnt ASC
                LIMIT {1}
            ", Param.ShopId, spCount.Value.ToString(), dtpStartDate.Value.ToString("yyyy-MM-dd"), dtpEndDate.Value.ToString("yyyy-MM-dd")));
            }
            else if (cbbType.SelectedItem.ToString() == "สินค้าที่ทำกำไรสูงสุด")
            {
                _TABLE_REPORT = Util.DBQuery(string.Format(@"
                SELECT Product, Total, SellDate, Name, category, brand FROM 
                ( SELECT product, sellPrice - cost Total, SellDate, Name, category, brand FROM 
                ( SELECT bc.product, p.Name, SUM(bc.sellPrice) sellPrice, SUM(bc.cost) cost, bc.SellDate, c.Name category, b.Name brand
                    FROM Barcode bc 
                    LEFT JOIN Product p
                    ON bc.product = p.product                
                    LEFT JOIN Brand b
                    ON b.Brand = p.Brand
                    LEFT JOIN Category c
                    ON c.Category = p.Category
                    WHERE bc.SellDate IS NOT NULL 
                    GROUP BY bc.product))
                WHERE SUBSTR(SellDate,1,10) BETWEEN '{2}' AND '{3}'
                ORDER BY Total DESC
                LIMIT {1}
            ", Param.ShopId, spCount.Value.ToString(), dtpStartDate.Value.ToString("yyyy-MM-dd"), dtpEndDate.Value.ToString("yyyy-MM-dd")));
            }
            else if (cbbType.SelectedItem.ToString() == "สินค้าที่ทำกำไรต่ำสุด")
            {
                _TABLE_REPORT = Util.DBQuery(string.Format(@"
                SELECT Product, Total, SellDate FROM 
                ( SELECT product, sellPrice - cost Total, SellDate FROM 
                ( SELECT product, SUM(sellPrice) sellPrice, SUM(cost) cost, SellDate FROM Barcode WHERE SellDate IS NOT NULL 
                    GROUP BY product))
                WHERE SUBSTR(SellDate,1,10) BETWEEN '{2}' AND '{3}'
                ORDER BY Total ASC
                LIMIT {1}
            ", Param.ShopId, spCount.Value.ToString(), dtpStartDate.Value.ToString("yyyy-MM-dd"), dtpEndDate.Value.ToString("yyyy-MM-dd")));
            }
            statisticGridView.OptionsBehavior.AutoPopulateColumns = false;
            statisticGridControl.MainView = statisticGridView;

            dt = new DataTable();
            for (i = 0; i < ((ColumnView)statisticGridControl.MainView).Columns.Count; i++)
            {
                dt.Columns.Add(statisticGridView.Columns[i].FieldName);
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            chartReport.Series.Clear();
            chartReport.Titles.Clear();
            Series series = new Series("Side-by-Side Bar Series 1", ViewType.Bar);
            if (cbbType.SelectedItem.ToString() == "สินค้าขายดี" || cbbType.SelectedItem.ToString() == "สินค้าขายไม่ดี")
            {
                ((GridView)statisticGridControl.MainView).Columns[5].Caption = "จำนวนที่ขาย";
                for (a = 0; a < _TABLE_REPORT.Rows.Count; a++)
                {
                        row = dt.NewRow();
                        row[0] = (a + 1) * 1;
                        row[1] = _TABLE_REPORT.Rows[a]["Product"].ToString();
                        row[2] = _TABLE_REPORT.Rows[a]["Name"].ToString();
                        row[3] = _TABLE_REPORT.Rows[a]["category"].ToString();
                        row[4] = _TABLE_REPORT.Rows[a]["brand"].ToString();
                        row[5] = int.Parse(_TABLE_REPORT.Rows[a]["cnt"].ToString());
                        dt.Rows.Add(row);
                    // Create the first side-by-side bar series and add points to it.
                    series.Points.Add(new SeriesPoint(_TABLE_REPORT.Rows[a]["Name"].ToString(), new double[] { int.Parse(_TABLE_REPORT.Rows[a]["cnt"].ToString()) }));

                }
            }
            else
            {
                ((GridView)statisticGridControl.MainView).Columns[5].Caption = "ยอดขาย";
                for (a = 0; a < _TABLE_REPORT.Rows.Count; a++)
                {
                    row = dt.NewRow();
                    row[0] = (a + 1) * 1;
                    row[1] = _TABLE_REPORT.Rows[a]["Product"].ToString();
                    row[2] = _TABLE_REPORT.Rows[a]["Name"].ToString();
                    row[3] = _TABLE_REPORT.Rows[a]["category"].ToString();
                    row[4] = _TABLE_REPORT.Rows[a]["brand"].ToString();
                    row[5] = int.Parse(_TABLE_REPORT.Rows[a]["Total"].ToString());
                    dt.Rows.Add(row);
                    // Create the first side-by-side bar series and add points to it.
                    series.Points.Add(new SeriesPoint(_TABLE_REPORT.Rows[a]["Name"].ToString(), new double[] { int.Parse(_TABLE_REPORT.Rows[a]["Total"].ToString()) }));

                }
            }

            statisticGridControl.DataSource = dt;

            // Add the series to the chart.
            chartReport.Series.Add(series);

            // Hide the legend (if necessary).
            chartReport.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            ((SideBySideBarSeriesView)series.View).ColorEach = true;
            // Rotate the diagram (if necessary).
            ((XYDiagram)chartReport.Diagram).Rotated = true;

            // Add a title to the chart (if necessary).
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Text = cbbType.SelectedItem.ToString();
            chartReport.Titles.Add(chartTitle1);

        }

        private void UcStatistic_Load(object sender, EventArgs e)
        {
            cbbType.SelectedIndex = 0;
        }
    }
}
