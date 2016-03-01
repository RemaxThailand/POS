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
using System.Collections;

namespace PowerPOS
{
    public partial class UcStatistic : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable _TABLE_REPORT;
        Series[] _SERIES;
        dynamic _JSON;


        public UcStatistic()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //splashScreenManager.ShowWaitForm();
            //LoadData(int.Parse(cbbBackward.SelectedItem.ToString()), chkShowCurrentMonth.Checked, cbbCustomerType.SelectedIndex == 0 ? "all" : "chain");

        }

        public void LoadData(int month, bool showCurrentMonth, string type)
        {
            //JSON = JsonConvert.DeserializeObject(await Util.ApiProcessAsync("/category/report/monthly", "shop=" + Param.ShopId
            //    + "&month=" + month
            //    + "&type=" + type));
            //RenderData(month, showCurrentMonth, true);
        }

        private void RenderData(int month, bool showCurrentMonth, bool firstLoad)
        {
            chartControl1.Titles[0].Text = "กราฟแสดง" + cbbDataType.SelectedItem.ToString()
                + cbbTime.SelectedItem.ToString()
                + ((cbbBackward.SelectedIndex > 0) ?
                " ย้อนหลัง " + cbbBackward.SelectedItem.ToString() + " " + lblTime.Text :
                ((cbbTime.SelectedIndex == 2) ? " เดือน" + Param.thaiMonth[DateTime.Now.Month - 1] + " " + (DateTime.Now.Year + 543) : ""));

            DataSet DataSet = new DataSet();
            DataTable data = new DataTable();

            if ((bool)_JSON.success)
            {
                if (cbbCustomerType.SelectedIndex == 0)
                {
                    data = new DataTable();
                    data.Columns.Add("Name", typeof(string));
                    data.PrimaryKey = new DataColumn[] { data.Columns["Name"] };
                    data.TableName = "Data-category";
                    int cnt = (chkShowCurrentMonth.Checked) ? month + 1 : month;
                    _SERIES = new Series[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        _SERIES[i] = new Series();
                        _SERIES[i].Name = Param.thaiMonth[DateTime.Today.AddMonths(i - month).Month - 1];
                        _SERIES[i].CrosshairLabelPattern = "{V:#,#}";
                        _SERIES[i].Points.Clear();
                    }

                    //cbbCategory.Properties.Items.Clear();

                    //var count = _JSON.result.Count;
                    //for (int i = 0; i < count; i++)
                    //{
                    //    if (!data.Rows.Contains(_JSON.result[i].category.ToString()))
                    //    {
                    //        var val = _JSON.result[i].category.ToString();
                    //        data.Rows.Add(val);
                    //        cbbCategory.Properties.Items.Add(val, true);
                    //    }
                    //    SeriesPoint seriesPoint = new SeriesPoint(_JSON.result[i].category,
                    //        (cbbDataType.SelectedIndex == 0) ? (int)_JSON.result[i].price :
                    //        (cbbDataType.SelectedIndex == 1) ? (int)_JSON.result[i].qty :
                    //        (cbbDataType.SelectedIndex == 2) ? (int)(_JSON.result[i].price - _JSON.result[i].cost) :
                    //        (cbbDataType.SelectedIndex == 3) ? (int)(_JSON.result[i].cost / _JSON.result[i].qty) :
                    //        (cbbDataType.SelectedIndex == 4) ? (int)(_JSON.result[i].price / _JSON.result[i].qty) :
                    //        (int)((_JSON.result[i].price - _JSON.result[i].cost) / _JSON.result[i].qty)
                    //    );
                    //    try
                    //    {
                    //        if ((showCurrentMonth && (int)_JSON.result[i].monthNo <= 0) || (!showCurrentMonth && (int)_JSON.result[i].monthNo < 0))
                    //        {
                    //            _SERIES[month + (int)_JSON.result[i].monthNo].Points.Add(seriesPoint);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex.Message);
                    //    }
                    //}
                }
            else
            {
                data = new DataTable();
                data.Columns.Add("Name", typeof(string));
                data.PrimaryKey = new DataColumn[] { data.Columns["Name"] };
                data.TableName = "Data-customerName";

                int count = _JSON.result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!data.Rows.Contains(_JSON.result[i].customerName.ToString()))
                    {
                        var val = _JSON.result[i].customerName.ToString();
                        data.Rows.Add(val);
                    }
                }

                Hashtable customer = new Hashtable();
                _SERIES = new Series[data.Rows.Count];
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    _SERIES[i] = new Series(data.Rows[i]["Name"].ToString(), ViewType.StackedBar);
                    _SERIES[i].Points.Clear();
                    customer.Add(data.Rows[i]["Name"].ToString(), i);
                }

                //cbbCategory.Properties.Items.Clear();

                count = _JSON.result.Count;
                for (int i = 0; i < count; i++)
                {
                    /*if (!data.Rows.Contains(_JSON.result[i].category.ToString()))
                    {
                        var val = _JSON.result[i].category.ToString();
                        data.Rows.Add(val);
                        cbbCategory.Properties.Items.Add(val, true);
                    }*/
                    SeriesPoint seriesPoint = new SeriesPoint(_JSON.result[i].category,
                        (cbbDataType.SelectedIndex == 0) ? (int)_JSON.result[i].price :
                        (cbbDataType.SelectedIndex == 1) ? (int)_JSON.result[i].qty :
                        (cbbDataType.SelectedIndex == 2) ? (int)(_JSON.result[i].price - _JSON.result[i].cost) :
                        (cbbDataType.SelectedIndex == 3) ? (int)(_JSON.result[i].cost / _JSON.result[i].qty) :
                        (cbbDataType.SelectedIndex == 4) ? (int)(_JSON.result[i].price / _JSON.result[i].qty) :
                        (int)((_JSON.result[i].price - _JSON.result[i].cost) / _JSON.result[i].qty)
                    );
                    try
                    {
                        //if ((showCurrentMonth && (int)_JSON.result[i].monthNo <= 0) || (!showCurrentMonth && (int)_JSON.result[i].monthNo < 0))
                        //{
                        _SERIES[customer[_JSON.result[i].customerName.ToString()]].Points.Add(seriesPoint);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            //Param.DataSet.Tables.Add(data);
            chartControl1.SeriesSerializable = _SERIES;
            chartControl1.Visible = true;

        }
            else
            {
                chartControl1.Visible = false;
            }

            if (splashScreenManager.IsSplashFormVisible)
                splashScreenManager.CloseWaitForm();
        }

        private void UcStatistic_Load(object sender, EventArgs e)
        {
            
        }

        private void chkShowCurrentMonth_CheckedChanged(object sender, EventArgs e)
        {
            RenderData(int.Parse(cbbBackward.SelectedItem.ToString()), chkShowCurrentMonth.Checked, false);
        }
    }
}
