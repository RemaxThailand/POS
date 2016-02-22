using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using System.Data.SqlClient;
using System.Reflection;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;

namespace PowerPOS
{
    public class Util
    {
        public static string ApiProcess(string method, string parameter)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Encoding = System.Text.Encoding.UTF8;
                return wc.UploadString(new Uri(Param.ApiUrl + method), parameter + "&apiKey=" + Param.ApiKey);
            }
        }

        public static void GetApiConfig()
        {
            Param.ApiUrl = Properties.Settings.Default.ApiUrl;
            Param.ApiKey = Properties.Settings.Default.ApiKey;
            Param.LicenseKey = Properties.Settings.Default.LicenseKey;
            Param.ApiChecked = Properties.Settings.Default.ApiChecked;
            Param.ImagePath = Properties.Settings.Default.ImagePath;
            Param.DeviceID = GetDiviceId();
            Param.ComputerName = System.Environment.MachineName;
            //Param.DatabaseName = Properties.Settings.Default.DatabaseName;
            //Param.DatabasePassword = Properties.Settings.Default.DatabasePassword;
            Param.ShopId = Properties.Settings.Default.ShopId;
            Param.ShopName = Properties.Settings.Default.ShopName;
            Param.ShopParent = Properties.Settings.Default.ShopParent;
            Param.ShopCustomer = Properties.Settings.Default.ShopCustomer;
            Param.ShopType = Properties.Settings.Default.ShopType;
            Param.ApiShopId = Properties.Settings.Default.ApiShopId;
            //Param.LogoPath = Properties.Settings.Default.LogoPath;
            //if (Param.LogoPath.ToString() == "")
            //{
            //    if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            //    if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
            //    using (var client = new WebClient())
            //    {
            //        client.DownloadFileAsync(new Uri(Param.LogoUrl), Param.LogoPath);
            //        Param.LogoPath = "Resource/Images/logo.jpg";
            //    }
            //}
        }

        public static string GetDiviceId()
        {
            /*string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }*/

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            string motherBoard = "";
            foreach (ManagementObject mo in moc)
            {
                motherBoard = (string)mo["SerialNumber"];
            }

            //Then use this code to get the HD ID:
            string drive = "C";
            ManagementObject dsk = new ManagementObject(
                @"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();

            string uniqueId = motherBoard + "-" + volumeSerial;
            Param.DeviceID = uniqueId;
            return uniqueId;
        }

        public static bool CanConnectInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(Param.ApiUrl))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static dynamic LoadAppConfig()
        {
            using (WebClient wc = new WebClient())
            {
              try
                {
                    dynamic application = Util.GetApiData("/shop-application/infoPos",
                    string.Format("licenseKey={0}&deviceId={1}",  Param.LicenseKey, Param.DeviceID));

                    dynamic jsonApplication = JsonConvert.DeserializeObject(application);
                    Console.WriteLine(jsonApplication.success);

                    //dynamic jsonApplication = jsonApplication.success;
                    if (jsonApplication.success.Value)
                    {
                        dynamic app = Util.GetApiData("/shop-application/updatePos",
                        string.Format("shop{0}&licenseKey={1}&column={2}&value={3}", Param.ApiShopId, Param.LicenseKey, "deviceName", Param.ComputerName));

                        dynamic jsonApp = JsonConvert.DeserializeObject(app);
                        Console.WriteLine(jsonApp.success);

                        Param.ShopId = jsonApplication.result[0].shop;
                        Param.DevicePrefix = jsonApplication.result[0].devicePrefix;
                        Param.DevicePrinter = jsonApplication.result[0].devicePrinter;
                        Param.MemberType = jsonApplication.result[0].memberType;
                        Param.ApiShopId = jsonApplication.result[0].id;
                        Param.ShopName = jsonApplication.result[0].name;
                        Param.ShopParent = jsonApplication.result[0].shopParent;
                        Param.ShopCustomer = jsonApplication.result[0].shopCustomer;
                        Param.ShopType = jsonApplication.result[0].shopType;
                        Param.PrintCount = jsonApplication.result[0].printcount;
                        Param.PrintType = jsonApplication.result[0].printType;
                        Param.PrintLogo = jsonApplication.result[0].printLogo;
                        Param.HeaderName = jsonApplication.result[0].headerName;
                        Param.FooterText = jsonApplication.result[0].footerText;
                        Param.ApiChecked = true;
                        Console.WriteLine(Param.LicenseKey);
                        Properties.Settings.Default.ApiShopId = Param.ApiShopId;
                        Properties.Settings.Default.ShopId = Param.ShopId;
                        Properties.Settings.Default.ShopType = Param.ShopType;
                        Properties.Settings.Default.DevicePrefix = Param.DevicePrefix;
                        Properties.Settings.Default.DevicePrinter = Param.DevicePrinter;
                        Properties.Settings.Default.MemberType = Param.MemberType;
                        Properties.Settings.Default.ApiChecked = true;
                        Properties.Settings.Default.Save();
                        Properties.Settings.Default.Upgrade();

                        Param.jsonObject = true;

                    }
                    else
                    {
                        Param.jsonObject = false;
                        //var structureFail = new { success = false, error = "1234", errorMessage = "" };
                        //jsonObject = new JsonSerializer().Deserialize(new StringReader(json), structureFail.GetType());
                    }
                    return jsonApplication;


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return null;
                }
            }
        }

        public static string GetApiData(string method, string parameter)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Encoding = System.Text.Encoding.UTF8;
                return wc.UploadString(new Uri(Param.ApiUrl + method), parameter + "&apiKey=" + Param.ApiKey);
            }
        }

        public static DataTable DBQuery(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, Param.SQLiteConnection);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
            return dt;
        }

        public static void DBExecute(string sql)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, Param.SQLiteConnection);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
        }

        public static void WriteErrorLog(string message)
        {
            string filename = "error-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + message);
            sw.Close();
        }

        // public static DataTable DBQuery(string sql)
        // {
        //     Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //     DataTable dt = new DataTable();
        //     try
        //     {
        //         SqlDataAdapter adapter = new SqlDataAdapter(sql, Param.SqlLocalDB);
        //         adapter.Fill(dt);
        //     }
        //     catch (Exception ex)
        //     {
        //         //WriteErrorLog(ex.Message);
        //         //WriteErrorLog(ex.StackTrace);
        //     }
        //     return dt;
        // }
        // public static void DBExecute(string sql)
        // {
        //     Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        //     try
        //     {
        //         SqlCommand command = new SqlCommand(sql, Param.SqlLocalDB);
        //         command.ExecuteNonQuery();

        //     }
        //     catch (Exception ex)
        //     {
        //         //WriteErrorLog(ex.Message);
        //         //WriteErrorLog(ex.StackTrace);
        //     }
        //}

        public static void ConnectSQLiteDatabase()
        {
            if (!File.Exists(Param.SQLiteFileName))
            {
                SQLiteConnection.CreateFile(Param.SQLiteFileName);
            }
            Param.SQLiteConnection = new SQLiteConnection("Data Source=" + Param.SQLiteFileName + ";Version=3;New=True;Compress=True;");
            Param.SQLiteConnection.Open();
        }

        //public static void ConnectSqlLocalDB()
        //{
        //    if (!File.Exists(Param.SQLFileName))
        //    {
        //        GetLocalDB(Param.DBName, false);
        //    }
        //    string sql = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFileName ={1};Initial Catalog ={0};Integrated Security = True;characterEncoding=UTF-8 ", Param.DBName, Param.dbFileName);
        //    Param.SqlLocalDB = new SqlConnection(sql);
        //    Param.SqlLocalDB.Open();
        //}

        //public static SqlConnection GetLocalDB(string dbName, bool deleteIfExists = false)
        //{
        //    try
        //    {
        //        string outputFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        //        string mdfFilename = dbName + ".mdf";
        //        Param.dbFileName = Path.Combine(outputFolder, mdfFilename);
        //        string logFileName = Path.Combine(outputFolder, String.Format("{0}_log.ldf", dbName));
        //        // Create Data Directory If It Doesn't Already Exist.
        //        if (!Directory.Exists(outputFolder))
        //        {
        //            Directory.CreateDirectory(outputFolder);
        //        }

        //        // If the file exists, and we want to delete old data, remove it here and create a new database.
        //        if (File.Exists(Param.dbFileName) && deleteIfExists)
        //        {
        //            if (File.Exists(logFileName)) File.Delete(logFileName);
        //            File.Delete(Param.dbFileName);
        //            CreateDatabase(dbName, Param.dbFileName);
        //        }
        //        // If the database does not already exist, create it.
        //        else if (!File.Exists(Param.dbFileName))
        //        {
        //            CreateDatabase(dbName, Param.dbFileName);
        //        }

        //        // Open newly created, or old database.
        //        string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDBFileName={1};Initial Catalog={0};Integrated Security=True;", dbName, Param.dbFileName);
        //        SqlConnection connection = new SqlConnection(connectionString);
        //        connection.Open();
        //        return connection;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public static bool CreateDatabase(string dbName, string dbFileName)
        //{
        //    try
        //    {
        //        string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True");
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            SqlCommand cmd = connection.CreateCommand();


        //            DetachDatabase(dbName);

        //            cmd.CommandText = String.Format("CREATE DATABASE {0} ON (NAME = N'{0}', FILENAME = '{1}')", dbName, dbFileName);
        //            cmd.ExecuteNonQuery();
        //        }

        //        if (File.Exists(dbFileName)) return true;
        //        else return false;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public static bool DetachDatabase(string dbName)
        //{
        //    try
        //    {
        //        string connectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB.0;Initial Catalog=master;Integrated Security=True");
        //        using (var connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            SqlCommand cmd = connection.CreateCommand();
        //            cmd.CommandText = String.Format("exec sp_detach_db '{0}'", dbName);
        //            cmd.ExecuteNonQuery();

        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static void LoadShopInfo()
        {
            //if (Util.CanConnectInternet() && (Param.ShopName == "" || Param.ShopParent == "" || Param.ShopCustomer == ""))
            //{
            //    var i = 0;
            //    var d = 0;

            //    string config = Util.GetApiData("/shop-application/infoPos",
            //       string.Format("shop={0}&licenseKey={1}&deviceId={2}", Param.ApiShopId));

            //    dynamic jsonConfig = JsonConvert.DeserializeObject(config);
            //    Console.WriteLine(jsonConfig.success);

            //    if (jsonConfig.success.Value)
            //    {
            //        Util.DBExecute(@"CREATE TABLE IF NOT EXISTS ShopConfig (
            //        shop NVARCHAR(8) NOT NULL ,
            //        configKey NVARCHAR(10) NOT NULL ,
            //        configValue NVARCHAR(20) NOT NULL ,
            //        PRIMARY KEY (shop,configKey))");

            //        const string command = @"INSERT OR REPLACE INTO ShopConfig (shop, configKey, configValue) ";
            //        var sb = new StringBuilder(command);

            //        for (i = 0; i < jsonConfig.result.Count; i++)
            //        {
            //            if (d != 0) sb.Append(" UNION ALL ");
            //            sb.Append(string.Format(@" SELECT '{0}', '{1}', '{2}'",
            //               jsonConfig.result[i].shop, jsonConfig.result[i].configKey, jsonConfig.result[i].configValue));
            //            d++;
            //        }
            //        Util.DBExecute(sb.ToString());
            //    }

            //    var azureTable = Param.AzureTableClient.GetTableReference("Shop");
            //    TableQuery<ShopEntity> query = new TableQuery<ShopEntity>()
            //        .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, Param.ShopId));

            //    foreach (ShopEntity entity in azureTable.ExecuteQuery(query))
            //    {
            //        Param.ShopName = entity.Name;
            //        Param.ShopParent = entity.ShopParent;
            //        Param.ShopCustomer = entity.ShopCustomer;

            //        Properties.Settings.Default.ShopName = Param.ShopName;
            //        Properties.Settings.Default.ShopParent = Param.ShopParent;
            //        Properties.Settings.Default.ShopCustomer = Param.ShopCustomer;
            //        Properties.Settings.Default.Save();
            //        Properties.Settings.Default.Upgrade();

            //        break;
            //    }
            //}

        }

        public static void LoadConfig()
        {
            if (Util.CanConnectInternet())
            {
                //Param.AzureTable = Param.AzureTableClient.GetTableReference("Shop");
                //TableOperation retrieveOperation = TableOperation.Retrieve<ShopConfigEntity>(Param.ShopId, "Config");
                //TableResult retrievedResult = Param.AzureTable.Execute(retrieveOperation);
                //ShopConfigEntity data = (ShopConfigEntity)retrievedResult.Result;

                //string product = Util.GetApiData("/shop-config/pos",
                //string.Format("shop={0}", Param.ApiShopId));

                //dynamic jsonProduct = JsonConvert.DeserializeObject(product);
                //Console.WriteLine(jsonProduct.success);

                //var json = new StringBuilder();
                //if (jsonProduct != null)
                //{
                //    json.Append(jsonProduct.Value);
                //}
                //Param.SystemConfig = JsonConvert.DeserializeObject(json.ToString());
            }
        }

        public static void SyncData()
        {
            DataTable dt;
            int i = 0;
            //## Barcode ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Barcode WHERE Sync = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string sellDate = dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()).ToString();
                    string inStock = dt.Rows[i]["inStock"].ToString() == "False" ? "0" : "1";
                    string sellPrice = dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString();
                    string customer = dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString();
                    string sellNo = dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString();
                    string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();
                    string value = sellDate + "," + inStock + "," + sellPrice + "," + customer + "," + sellNo + "," + sellBy;
                    //dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()) + "," + dt.Rows[i]["inStock"].ToString() == "false" ? "0" : "1" + "," + 
                    //dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString() + "," + 
                    //dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString() + "," + 
                    //dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString() + "," + dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();

                    string values = dt.Rows[i]["receivedDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["receivedDate"].ToString()) + "," + dt.Rows[i]["inStock"].ToString() + "," + dt.Rows[i]["operationCost"].ToString() + "," + dt.Rows[i]["cost"].ToString() + "," + dt.Rows[i]["receivedBy"].ToString();

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "sellDate,inStock,sellPrice,customer,sellNo,sellBy", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "receivedDate,inStock,operationCost,cost,receivedBy", values)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }


                    Util.DBExecute(string.Format("UPDATE Barcode SET Sync = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
                }

            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## Product ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Product WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string val = dt.Rows[i]["price"].ToString() + "," + dt.Rows[i]["price1"].ToString() + "," + dt.Rows[i]["price2"].ToString() + "," + dt.Rows[i]["price3"].ToString() + "," + dt.Rows[i]["price4"].ToString() + "," + dt.Rows[i]["price5"].ToString() + "," + dt.Rows[i]["quantity"].ToString() + "," + dt.Rows[i]["cost"].ToString();

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updatePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["product"].ToString(), "price,price1,price2,price3,price4,price5,quantity,cost", val)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE Product SET Sync = 0 WHERE product = '{0}' AND Shop = '{1}'", dt.Rows[i]["product"].ToString(), Param.ShopId));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }


            //## PurchaseOrder (Noserial) ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM PurchaseOrder WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string val = dt.Rows[i]["ReceivedQuantity"].ToString() + "," + dt.Rows[i]["ReceivedBy"].ToString() + "," + dt.Rows[i]["ReceivedDate"].ToString();

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateNsPos",
                    string.Format("shop={0}&orderno={4}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["product"].ToString(), "receivedQuantity,receivedBy,receivedDate", val, dt.Rows[i]["orderNo"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE Product SET Sync = 0 WHERE product = '{0}' AND Shop = '{1}'", dt.Rows[i]["product"].ToString(), Param.ShopId));

                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## CategoryProfit ##//
            //try
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //    dt = Util.DBQuery("SELECT * FROM CategoryProfit WHERE Sync = 1");

            //    var azureTable = Param.AzureTableClient.GetTableReference("CategoryProfit");
            //    TableBatchOperation batchOperation = new TableBatchOperation();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow row = dt.Rows[i];
            //        dynamic d = new DynamicEntity(Param.ShopId, row["ID"].ToString());
            //        if (row["Price"].ToString() == "" || row["Price"].ToString() == null) { d.Price = 0; } else { d.Price = double.Parse(row["Price"].ToString()); }
            //        if (row["Price1"].ToString() == "" || row["Price1"].ToString() == null) { d.Price1 = 0; } else { d.Price1 = double.Parse(row["Price1"].ToString()); }
            //        if (row["Price2"].ToString() == "" || row["Price2"].ToString() == null) { d.Price2 = 0; } else { d.Price2 = double.Parse(row["Price2"].ToString()); }
            //        if (row["Price3"].ToString() == "" || row["Price3"].ToString() == null) { d.Price3 = 0; } else { d.Price3 = double.Parse(row["Price3"].ToString()); }
            //        if (row["Price4"].ToString() == "" || row["Price4"].ToString() == null) { d.Price4 = 0; } else { d.Price4 = double.Parse(row["Price4"].ToString()); }
            //        if (row["Price5"].ToString() == "" || row["Price5"].ToString() == null) { d.Price5 = 0; } else { d.Price5 = double.Parse(row["Price5"].ToString()); }
            //        batchOperation.InsertOrMerge(d);

            //        Util.DBExecute(string.Format("UPDATE CategoryProfit SET Sync = 0 WHERE ID = {0}", row["ID"].ToString()));

            //        if (batchOperation.Count == 100)
            //        {
            //            azureTable.ExecuteBatch(batchOperation);
            //            batchOperation = new TableBatchOperation();
            //        }
            //    }
            //    if (batchOperation.Count > 0)
            //        azureTable.ExecuteBatch(batchOperation);
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorLog(ex.Message);
            //    WriteErrorLog(ex.StackTrace);
            //}

            ////## Customer ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Customer WHERE Sync = 1");
                i = 0;
                //    var azureTable = Param.AzureTableClient.GetTableReference("Customer");
                //    TableBatchOperation batchOperation = new TableBatchOperation();

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/customer/Add",
                    string.Format("shop={0}&mobile={1}&firstname={2}&lastname={3}&nickname={4}&sex={5}&birthday={6}&citizen={7}&cardno={8}&email={9}&address={10}&address2={11}&subdistrict={12}&district={13}&province={14}&zipcode={15}&shopname={16}&shopsameaddress={17}&shopaddress={18}&shopaddress2={19}&shopsubdistrict={20}&shopdistrict={21}&shopprovince={22}&shopzipcode={23}&credit={24}&sellprice={25}&customer={26}",
                                Param.ApiShopId, dt.Rows[i]["mobile"].ToString(), dt.Rows[i]["firstname"].ToString(), dt.Rows[i]["lastname"].ToString(), dt.Rows[i]["nickname"].ToString(),
                                dt.Rows[i]["sex"].ToString(), dt.Rows[i]["birthday"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["birthday"].ToString()), dt.Rows[i]["citizenId"].ToString(), dt.Rows[i]["cardno"].ToString(), dt.Rows[i]["email"].ToString(),
                                dt.Rows[i]["address"].ToString(), dt.Rows[i]["address2"].ToString(), dt.Rows[i]["subdistrict"].ToString(), dt.Rows[i]["district"].ToString(), dt.Rows[i]["province"].ToString(),
                                dt.Rows[i]["zipcode"].ToString(), dt.Rows[i]["shopname"].ToString(), dt.Rows[i]["shopsameaddress"].ToString() == "False" ? 0 : 1, dt.Rows[i]["shopaddress"].ToString(), dt.Rows[i]["shopaddress2"].ToString(),
                                dt.Rows[i]["shopsubdistrict"].ToString(), dt.Rows[i]["shopdistrict"].ToString(), dt.Rows[i]["shopprovince"].ToString(), dt.Rows[i]["shopzipcode"].ToString(), dt.Rows[i]["credit"].ToString(), dt.Rows[i]["sellprice"].ToString(),
                                dt.Rows[i]["customer"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE Customer SET Sync = 0 WHERE customer = '{0}'", dt.Rows[i]["customer"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## SellHeader ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM SellHeader WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/sale/saleAdd",
                    string.Format("shop={0}&saleno={1}&profit={2}&totalPrice={3}&payType={4}&cash={5}&credit={6}&customer={7}&sex={8}&age={9}&comment={10}&saledate={11}&saleby={12}",
                                Param.ApiShopId, dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["Profit"].ToString(), dt.Rows[i]["totalPrice"].ToString(), dt.Rows[i]["payType"].ToString(),
                                dt.Rows[i]["cash"].ToString(), dt.Rows[i]["credit"].ToString(), dt.Rows[i]["customer"].ToString(), dt.Rows[i]["customerSex"].ToString(), dt.Rows[i]["customerAge"].ToString(),
                                dt.Rows[i]["comment"].ToString(), dt.Rows[i]["sellDate"].ToString(), dt.Rows[i]["sellBy"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE SellHeader SET Sync = 0 WHERE sellNo = '{0}' ", dt.Rows[i]["sellNo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## SellDetail ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM SellDetail WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/sale/saleDetailAdd",
                    string.Format("shop={0}&saleno={1}&product={2}&price={3}&cost={4}&quantity={5}&comment={6}",
                            Param.ApiShopId, dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["product"].ToString(), dt.Rows[i]["sellPrice"].ToString(),
                           dt.Rows[i]["cost"].ToString(), dt.Rows[i]["quantity"].ToString(), dt.Rows[i]["comment"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE SellDetail SET Sync = 0 WHERE sellNo = '{0}'", dt.Rows[i]["sellNo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## Return ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM ReturnProduct WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/return/returnAdd",
                    string.Format("shop={0}&returnNo={1}&quantity={2}&sellNo={3}&product={4}&returnDate={5}&returnBy={6}&salePrice={7}&barcode={8}",
                                Param.ApiShopId, dt.Rows[i]["ReturnNo"].ToString(), dt.Rows[i]["quantity"].ToString(), dt.Rows[i]["SellNo"].ToString(), dt.Rows[i]["product"].ToString(),
                                dt.Rows[i]["returnDate"].ToString(), dt.Rows[i]["returnBy"].ToString(), double.Parse(dt.Rows[i]["SellPrice"].ToString()), dt.Rows[i]["barcode"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE ReturnProduct SET Sync = 0 WHERE SellNo = '{0}' AND Barcode = '{1}'", dt.Rows[i]["SellNo"].ToString(), dt.Rows[i]["barcode"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## ChangePrice ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM ChangePrice WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/sale/ChangePriceAdd",
                    string.Format("shop={0}&saleno={1}&product={2}&price={3}&change={4}&by={5}&date={6}",
                            Param.ApiShopId, dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["product"].ToString(), dt.Rows[i]["price"].ToString(),
                           dt.Rows[i]["priceChange"].ToString(), dt.Rows[i]["changeBy"].ToString(), dt.Rows[i]["changeDate"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE ChangePrice SET Sync = 0 WHERE sellNo = '{0}'", dt.Rows[i]["sellNo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## CreditCustomer ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM CreditCustomer WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/customer/creditAdd",
                    string.Format("shop={0}&creditno={1}&saleno={2}&paidprice={3}&paidby={4}&paiddate={5}",
                            Param.ApiShopId, dt.Rows[i]["creditNo"].ToString(), dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["paidPrice"].ToString(),
                           dt.Rows[i]["paidBy"].ToString(), dt.Rows[i]["paidDate"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE CreditCustomer SET Sync = 0 WHERE creditNo = '{0}'", dt.Rows[i]["creditNo"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## InventoryCount ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM InventoryCount WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/addCount",
                    string.Format("shop={0}&product={1}&quantity={2}",Param.ApiShopId, dt.Rows[i]["product"].ToString(), dt.Rows[i]["quantity"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }

                    Util.DBExecute(string.Format("UPDATE InventoryCount SET Sync = 0 WHERE product = '{0}'", dt.Rows[i]["product"].ToString()));
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## Claim ##//
            //try
            //{
            //    dt = Util.DBQuery("SELECT * FROM Claim WHERE Sync = 1");

            //    var azureTable = Param.AzureTableClient.GetTableReference("Claim");
            //    azureTable.CreateIfNotExists();

            //    TableBatchOperation batchOperation = new TableBatchOperation();
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow row = dt.Rows[i];
            //        dynamic d = new DynamicEntity(Param.ShopId, row["ClaimNo"].ToString());
            //        d.Barcode = row["Barcode"].ToString();
            //        d.ClaimType = row["ClaimType"].ToString();
            //        d.BarcodeClaim = row["BarcodeClaim"].ToString();
            //        d.Description = row["Description"].ToString();
            //        d.Firstname = row["Firstname"].ToString();
            //        d.Lastname = row["Lastname"].ToString();
            //        d.Nickname = row["Nickname"].ToString();
            //        d.Tel = row["Tel"].ToString();
            //        d.Email = row["Email"].ToString();
            //        d.Price = double.Parse(row["Price"].ToString());
            //        if (row["PriceClaim"].ToString() == "" || row["PriceClaim"].ToString() == null) { d.PriceClaim = 0; } else { d.PriceClaim = double.Parse(row["PriceClaim"].ToString()); }
            //        d.ClaimDate = Convert.ToDateTime(row["ClaimDate"].ToString());
            //        d.Product = row["Product"].ToString();
            //        d.ClaimBy = row["ClaimBy"].ToString();
            //        batchOperation.InsertOrMerge(d);

            //        Util.DBExecute(string.Format("UPDATE Claim SET Sync = 0 WHERE ClaimNo = '{0}' AND Barcode = '{1}'", row["ClaimNo"].ToString(), row["Barcode"].ToString()));

            //        if (batchOperation.Count == 100)
            //        {
            //            azureTable.ExecuteBatch(batchOperation);
            //            batchOperation = new TableBatchOperation();
            //        }
            //    }
            //    if (batchOperation.Count > 0)
            //        azureTable.ExecuteBatch(batchOperation);
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorLog(ex.Message);
            //    WriteErrorLog(ex.StackTrace);
            // }
        }

        public static void SetKeyboardLayout(InputLanguage layout)
        {
            InputLanguage.CurrentInputLanguage = layout;
        }

        public static InputLanguage GetInputLanguageByName(string inputName)
        {
            foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
            {
                if (lang.Culture.EnglishName.ToLower().StartsWith(inputName.ToLower()))
                    return lang;
            }
            return null;
        }

        public static void PrintReceipt(string sellNo)
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM SellDetail WHERE SellNo = '{0}'", sellNo));

            var hight = 195 + int.Parse(dt.Rows[0]["cnt"].ToString()) * 13;
            //PaperSize paperSize = new PaperSize("Custom Size", 280, hight);
            //PaperSize paperSize = new PaperSize("Custom Size", 380, hight);
            PaperSize paperSize = new PaperSize("Custom Size", 400, hight);
            paperSize.RawKind = (int)PaperKind.Custom;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;
            //pd.PrinterSettings.PrinterName = "GP-80250 Series";
            //pd.PrinterSettings.PrinterName = "POS80";

            pd.PrintPage += (_, g) =>
            {
                PrintReceipt(g, sellNo);
            };
            pd.Print();

        }


        private static void PrintReceipt(PrintPageEventArgs g, string sellNo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {

                DataTable dtHeader = Util.DBQuery(string.Format(@"SELECT h.TotalPrice Price, IFNULL(h.Cash,0) Cash, c.Firstname, c.Lastname, c.Mobile, datetime(h.SellDate, 'localtime') SellDate, h.SellBy
                    FROM SellHeader h
                        LEFT JOIN Customer c
                        ON h.Customer = c.Customer
                    WHERE h.SellNo = '{0}'"
                    , sellNo));

                var width = 280;
                var gab = 5;

                //if (Param.SystemConfig.Bill.PrintLogo == "Y")
                //{
                //    if (!File.Exists(Param.LogoPath))
                //    {
                //        if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                //        if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                //        using (var client = new WebClient())
                //        {
                //            client.DownloadFile(new Uri(Param.LogoUrl), Param.LogoPath);
                //            Param.SystemConfig.Bill.Logo = Param.LogoUrl;
                //        }
                //    }
                //    Image image = Image.FromFile(Param.LogoPath);
                //    Rectangle destRect = new Rectangle(0, 0, width, 64);
                //    //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                //    g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                //}


                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("Calibri", 6);
                //if (Param.SystemConfig.Bill.Logo == Param.LogoUrl && Param.SystemConfig.Bill.PrintLogo == "Y")
                //{
                //g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                //g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                //stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                //g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
                //}

                var pX = 0;
                var pY = 65;
                stringFont = new Font("Calibri", 7);
                g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

                stringFont = new Font("DilleniaUPC", 13);
                g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 188, pY));

                stringFont = new Font("Calibri", 10, FontStyle.Bold);
                string measureString = sellNo;
                SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width + gab, pY + 3));
                pY += 20;

                stringFont = new Font("DilleniaUPC", 17, FontStyle.Bold);
                measureString = Param.HeaderName; // "ใบเสร็จรับเงิน";
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
                pY += 30;

                stringFont = new Font("Cordia New", 10);
                DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name Name, sd.Quantity ProductCount, sd.SellPrice SellPrice
                            FROM  SellDetail sd
                                LEFT JOIN Product p 
                                ON sd.Product = p.Product 
                       WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0", sellNo, Param.ShopId));

                var sumQty = 0;
                var sumPrice = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 16, pY));

                    g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 230, pY + 3, 150, 10);
                    g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                        stringFont, brush, new PointF(pX + 232, pY));
                    measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                    pY += 13;
                }

                pY += 4;
                stringFont = new Font("Cordia New", 12, FontStyle.Bold);
                g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, sumQty), stringFont, brush, new PointF(pX, pY));
                measureString = "" + sumPrice.ToString("#,##0");
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                pY += 17;
                stringFont = new Font("Cordia New", 11);
                g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                pY += 23;

                g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                pY += 5;

                stringFont = new Font("DilleniaUPC", 10);
                g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
                    ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                    " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
                    : "")
                    , stringFont, brush, new PointF(pX, pY));

                /*stringFont = new Font("DilleniaUPC", 11);
                measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
                pY += 17;

                stringFont = new Font("Calibri", 8, FontStyle.Bold);
                measureString = Param.FooterText;
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public static void PrintOrder(string orderNo)
        {
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM Barcode WHERE OrderNo = '{0}'", orderNo));

            var hight = 195 + int.Parse(dt.Rows[0]["cnt"].ToString()) * 13;
            //PaperSize paperSize = new PaperSize("Custom Size", 280, hight);
            //PaperSize paperSize = new PaperSize("Custom Size", 380, hight);
            PaperSize paperSize = new PaperSize("Custom Size", 400, hight);
            paperSize.RawKind = (int)PaperKind.Custom;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;
            //pd.PrinterSettings.PrinterName = "GP-80250 Series";
            //pd.PrinterSettings.PrinterName = "POS80";

            pd.PrintPage += (_, g) =>
            {
                PrintOrder(g, orderNo);
            };
            pd.Print();

        }


        private static void PrintOrder(PrintPageEventArgs g, string orderNo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


            DataTable dtHeader = Util.DBQuery(string.Format(@"
                    SELECT DISTINCT p.sku, b.Product, p.Name, COUNT(*) ProductCount, IFNULL(r.ReceivedCount, 0) ReceivedCount, c.name Category
                    FROM Barcode b
                        LEFT JOIN Product p
                            ON b.Product = p.Product
                            AND p.Shop = '{0}'
                        LEFT JOIN(
                                SELECT DISTINCT Product, COUNT(*) ReceivedCount
                                FROM Barcode
                                WHERE ReceivedDate IS NOT NULL
                                AND ReceivedBy = '{2}'
                                AND OrderNo = '{1}'
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                    WHERE(b.ReceivedDate IS NULL OR b.ReceivedBy = '{2}')
                        AND b.OrderNo = '{1}'
                    GROUP BY b.Product
                    --ORDER BY c.name, p.Name
                    UNION ALL
                    SELECT p.sku, po.product, p.Name, po.Quantity, po.ReceivedQuantity, c.name category
                    FROM PurchaseOrder po
                        LEFT JOIN Product p
                        ON po.Product = p.product
                    LEFT JOIN Category c
                    ON p.Category = c.category
                    WHERE po.OrderNo = '{1}'
                ", Param.ShopId, orderNo, Param.UserId));
            var width = 280;
            var gab = 5;

            //if (Param.SystemConfig.Bill.PrintLogo == "Y")
            //{
            //    if (!File.Exists(Param.LogoPath))
            //    {
            //        if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
            //        if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
            //        using (var client = new WebClient())
            //        {
            //            client.DownloadFile(new Uri(Param.LogoUrl), Param.LogoPath);
            //            Param.SystemConfig.Bill.Logo = Param.LogoUrl;
            //        }
            //    }
            //    Image image = Image.FromFile(Param.LogoPath);
            //    Rectangle destRect = new Rectangle(0, 0, width, 64);
            //    //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
            //    g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            //}


            SolidBrush brush = new SolidBrush(Color.Black);
            Font stringFont = new Font("Calibri", 6);
            //if (Param.SystemConfig.Bill.Logo == Param.LogoUrl && Param.SystemConfig.Bill.PrintLogo == "Y")
            //{
            //g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
            //g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
            //stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
            //g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
            //}

            var pX = 0;
            var pY = 65;
            //stringFont = new Font("Calibri", 7);
            //g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

            //stringFont = new Font("DilleniaUPC", 13);
            //g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 188, pY));

            //stringFont = new Font("Calibri", 10, FontStyle.Bold);
            //string measureString = sellNo;
            //SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width + gab, pY + 3));
            //pY += 20;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            string measureString = "รายงานรับเข้าสินค้า"; 
            SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
            pY += 30;

            stringFont = new Font("DilleniaUPC", 13);
            g.Graphics.DrawString("วันที่ ", stringFont, brush, new PointF(pX, pY));

            stringFont = new Font("Calibri", 10);
            measureString = DateTime.Now.ToString("dd/MM/yyyy  HH:MM:ss") ;
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 30, pY + 3));
            pY += 20;

            stringFont = new Font("DilleniaUPC", 13);
            g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX, pY));

            stringFont = new Font("Calibri", 10);
            measureString = orderNo;
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(orderNo, stringFont, brush, new PointF(pX + 30, pY + 3));
            pY += 20;

            //stringFont = new Font("Cordia New", 10);
            //DataTable dt = Util.DBQuery(string.Format(@"SELECT Name, ProductCount, SellPrice
            //        FROM (SELECT product, SUM(SellPrice) SellPrice, COUNT(*) ProductCount FROM Barcode WHERE SellNo = '{0}' GROUP BY product) b 
            //            LEFT JOIN Product p 
            //            ON b.Product = p.Product
            //            AND p.Shop = '{1}'
            //    ", sellNo, Param.ShopId));
            //var sumQty = 0;
            //var sumPrice = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
            //    g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 16, pY));

            //    g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 230, pY + 3, 150, 10);
            //    g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
            //        stringFont, brush, new PointF(pX + 232, pY));
            //    measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
            //    stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
            //    sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
            //    sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
            //    pY += 13;
            //}

            //pY += 4;
            //stringFont = new Font("Cordia New", 12, FontStyle.Bold);
            //g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, sumQty), stringFont, brush, new PointF(pX, pY));
            //measureString = "" + sumPrice.ToString("#,##0");
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
            //pY += 17;
            //stringFont = new Font("Cordia New", 11);
            //g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
            //measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
            //pY += 23;

            //g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
            //pY += 5;

            //stringFont = new Font("DilleniaUPC", 10);
            //g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
            //    ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
            //    " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
            //    : "")
            //    , stringFont, brush, new PointF(pX, pY));

            ///*stringFont = new Font("DilleniaUPC", 11);
            //measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
            //pY += 17;

            //stringFont = new Font("Calibri", 8, FontStyle.Bold);
            //measureString = Param.FooterText;
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));

        }
    }
}
