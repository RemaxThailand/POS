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
using System.Data.SqlServerCe;
using System.Collections;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data.SqlServerCe;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization.Data;
using WinSCP;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Zen.Barcode;
using System.Threading;
using System.Globalization;
using System.Drawing.Printing;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace PowerPOS
{
    public class Util
    {
        public static dynamic jsonAddressInfo;
        public static dynamic jsonWarrantyInfo;

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
            GetConfigFromSqlCe(Param.DatabaseName, Param.DatabasePassword);
            Param.ApiUrl = Properties.Settings.Default.ApiUrl;
            Param.ApiKey = Properties.Settings.Default.ApiKey;
            //Param.LicenseKey = Properties.Settings.Default.LicenseKey;
            Param.ApiChecked = Properties.Settings.Default.ApiChecked;
            Param.ImagePath = Properties.Settings.Default.ImagePath;
            Param.DeviceID = GetDiviceId();
            Param.ComputerName = System.Environment.MachineName;
            //Param.DatabaseName = Properties.Settings.Default.DatabaseName;
            //Param.DatabasePassword = Properties.Settings.Default.DatabasePassword;
            //Param.ShopId = Properties.Settings.Default.ShopId;
            Param.ShopName = Properties.Settings.Default.ShopName;
            Param.ShopParent = Properties.Settings.Default.ShopParent;
            Param.ShopCustomer = Properties.Settings.Default.ShopCustomer;
            Param.ShopType = Properties.Settings.Default.ShopType;
            //Param.ApiShopId = Properties.Settings.Default.ApiShopId;

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

            string uniqueId = (motherBoard.Trim() + "-" + volumeSerial.Trim()).Trim();
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
                    string.Format("licenseKey={0}&deviceId={1}", Param.LicenseKey, Param.DeviceID));

                    dynamic jsonApplication = JsonConvert.DeserializeObject(application);
                    Console.WriteLine(jsonApplication.success);

                    //dynamic jsonApplication = jsonApplication.success;
                    if (jsonApplication.success.Value)
                    {
                        dynamic app = Util.GetApiData("/shop-application/updatePos",
                        string.Format("shop={0}&licenseKey={1}&column={2}&value={3}", Param.ApiShopId, Param.LicenseKey,
                            "applicationPath", Param.ApplicationDataPath));

                        dynamic jsonApp = JsonConvert.DeserializeObject(app);

                        Param.ShopId = jsonApplication.result[0].shop;
                        Param.DevicePrefix = jsonApplication.result[0].devicePrefix;
                        Param.DevicePrinter = jsonApplication.result[0].devicePrinter;
                        Param.MemberType = jsonApplication.result[0].memberType;
                        Param.ApiShopId = jsonApplication.result[0].id;
                        Param.ShopName = jsonApplication.result[0].name;
                        Param.ShopParent = jsonApplication.result[0].shopParent;
                        Param.ShopCustomer = jsonApplication.result[0].shopCustomer;
                        Param.ShopType = jsonApplication.result[0].shopType;
                        Param.ShopType = jsonApplication.result[0].shopType;
                        Param.ShopCost = jsonApplication.result[0].shopCost;
                        Param.PrintCount = jsonApplication.result[0].printCount;
                        Param.PrintType = jsonApplication.result[0].printType;
                        Param.PrintLogo = jsonApplication.result[0].printLogo;
                        Param.HeaderName = jsonApplication.result[0].headerName;
                        Param.FooterText = jsonApplication.result[0].footerText;
                        Param.PaperSize = jsonApplication.result[0].paperSize;
                        Param.shopClaim = jsonApplication.result[0].shopClaimType;
                        Param.shopReceived = jsonApplication.result[0].shopReceived;
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

        public class TimedWebClient : WebClient
        {
            // Timeout in milliseconds, default = 600,000 msec
            public int Timeout { get; set; }

            public TimedWebClient()
            {
                this.Timeout = 600000;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var objWebRequest = base.GetWebRequest(address);
                objWebRequest.Timeout = this.Timeout;
                return objWebRequest;
            }
        }

        public static string GetApiData(string method, string parameter)
        {

            using (WebClient wc = new TimedWebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                wc.Encoding = System.Text.Encoding.UTF8;
                return wc.UploadString(new Uri(Param.ApiUrl + method), parameter + "&apiKey=" + Param.ApiKey);
            }
        }

        public static string GetApiBigData(string method)
        {

            //using (var client = new HttpClient())
            //{
            //    var values = new Dictionary<string, string>
            //{
            //   { "apiKey", Param.ApiKey },
            //   { "shop", Param.ApiShopId }
            //};


            //    var content = new FormUrlEncodedContent(values);

            //    var response = await client.PostAsync(new Uri(Param.ApiUrl + method), content);

            //    var responseString = await response.Content.ReadAsStringAsync();

            //    return responseString;
            //}

            var request = (HttpWebRequest)WebRequest.Create(new Uri(Param.ApiUrl + method));

            var postData = "apiKey=" + Param.ApiKey;
            postData += "&shop=" + Param.ApiShopId;
            var data = Encoding.UTF8.GetBytes(postData);
            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            
            return responseString;

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

        public static DataTable SqlCeQuery(string sql)
        {
            string connStr = "Data Source=" + Param.SqlCeFile;
            Param.SqlCeConnection = new SqlCeConnection(connStr);
            if (Param.SqlCeConnection.State == ConnectionState.Closed)
            {
                Param.SqlCeConnection = new SqlCeConnection(connStr);
                Param.SqlCeConnection.Open();
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            DataTable dt = new DataTable();
            try
            {
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, Param.SqlCeConnection);
                adapter.Fill(dt);
                adapter.Dispose();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
            finally
            {
                Param.SqlCeConnection.Close();
            }
            return dt;
        }

        public static void SqlCeExecute(string sql)
        {
            string connStr = "Data Source=" + Param.SqlCeFile;
            if (Param.SqlCeConnection.State == ConnectionState.Closed)
            {
                Param.SqlCeConnection = new SqlCeConnection(connStr);
                Param.SqlCeConnection.Open();
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {
                SqlCeCommand command = new SqlCeCommand(sql, Param.SqlCeConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }
            finally
            {
                Param.SqlCeConnection.Close();
            }
        }

        public static void WriteErrorLog(string message)
        {
            if (!Directory.Exists(Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName)))
                Directory.CreateDirectory(Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string filename = Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName) + @"\error-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            StreamWriter sw = new StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + message);
            sw.Close();
        }

        public static void WriteLog(string message)
        {
            if (!Directory.Exists(Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName)))
                Directory.CreateDirectory(Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string filename = Path.Combine(Param.ApplicationDataPath, "log", Param.ShopId, System.Environment.MachineName) + @"\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
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

        public static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            WriteErrorLog(string.Format("SCP Conflict.Type Error : {0}", e.Conflict.Type));
            WriteErrorLog(string.Format("SCP Error : {0}", e.Error));
        }

        public static void CreateDatabaseProvision(SqlConnection serverConn, SqlCeConnection clientConn, string scopeName)
        {
            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(scopeName);
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(scopeName.Replace("Scope", ""), serverConn);
            scopeDesc.Tables.Add(tableDesc);

            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
            if (!serverProvision.ScopeExists(scopeName))
            {
                serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);
                serverProvision.Apply();
            }

            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);
            if (!clientProvision.ScopeExists(scopeName))
            {
                clientProvision.Apply();
            }
        }

        public static void CreateDatabaseProvisionFilter(SqlConnection serverConn, SqlCeConnection clientConn, string scopeName, string filterName, string filterValue)
        {
            /*SqlSyncStoreMetadataCleanup metadataCleanup = new SqlSyncStoreMetadataCleanup(serverConn);
            bool cleanupSuccessful;
            metadataCleanup.RetentionInDays = 0;
            cleanupSuccessful = metadataCleanup.PerformCleanup();*/

            string filterTemplate = scopeName + "_filter_template";

            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(filterTemplate);
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(scopeName.Replace("Scope", ""), serverConn);
            scopeDesc.Tables.Add(tableDesc);

            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc, SqlSyncScopeProvisioningType.Template);
            serverProvision.Tables[scopeName.Replace("Scope", "")].AddFilterColumn(filterName);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterClause = "[side].[" + filterName + "] = @" + filterName;
            SqlParameter parameter = new SqlParameter("@" + filterName, SqlDbType.NVarChar, 8);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterParameters.Add(parameter);
            if (!serverProvision.TemplateExists(filterTemplate))
            {
                try
                {
                    serverProvision.Apply();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
            serverProvision.PopulateFromTemplate(scopeName + "-" + filterName + filterValue, filterTemplate);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterParameters["@" + filterName].Value = filterValue;
            if (!serverProvision.ScopeExists(scopeName + "-" + filterName + filterValue))
            {
                try
                {
                    serverProvision.Apply();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(scopeName + "-" + filterName + filterValue, serverConn);
            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);
            if (!clientProvision.ScopeExists(scopeName + "-" + filterName + filterValue))
            {
                try
                {
                    clientProvision.Apply();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error {0}", ex.Message);
                }
            }
        }

        public static void CreateDatabaseProvisionFilter(SqlConnection serverConn, SqlCeConnection clientConn, string scopeName, string filterName, string filterValue, Collection<string> columnsToInclude)
        {
            string filterTemplate = scopeName + "_filter_template";

            DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(filterTemplate);
            DbSyncTableDescription tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(scopeName.Replace("Scope", ""), columnsToInclude, serverConn);
            scopeDesc.Tables.Add(tableDesc);

            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc, SqlSyncScopeProvisioningType.Template);
            serverProvision.Tables[scopeName.Replace("Scope", "")].AddFilterColumn(filterName);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterClause = "[side].[" + filterName + "] = @" + filterName;
            SqlParameter parameter = new SqlParameter("@" + filterName, SqlDbType.NVarChar, 8);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterParameters.Add(parameter);
            if (!serverProvision.TemplateExists(filterTemplate))
            {
                serverProvision.Apply();
            }

            serverProvision = new SqlSyncScopeProvisioning(serverConn, scopeDesc);
            serverProvision.PopulateFromTemplate(scopeName + "-" + filterName + filterValue, filterTemplate);
            serverProvision.Tables[scopeName.Replace("Scope", "")].FilterParameters["@" + filterName].Value = filterValue;
            if (!serverProvision.ScopeExists(scopeName + "-" + filterName + filterValue))
            {
                serverProvision.Apply();
            }

            scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(scopeName + "-" + filterName + filterValue, serverConn);
            SqlCeSyncScopeProvisioning clientProvision = new SqlCeSyncScopeProvisioning(clientConn, scopeDesc);
            if (!clientProvision.ScopeExists(scopeName + "-" + filterName + filterValue))
            {
                try
                {
                    clientProvision.Apply();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error {0}", ex.Message);
                }
            }
        }

        public static void SyncDatabase(SqlConnection serverConn, SqlCeConnection clientConn, string scopeName)
        {
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider(scopeName, clientConn);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider(scopeName, serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;

            ((SqlCeSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            // print statistics
            WriteLog(string.Format("Sync Scope {0}\t\tUploaded : {1} / Download : {2}\tTime : {3} Seconds", scopeName.Replace("Scope", ""),
                syncStats.UploadChangesTotal, syncStats.DownloadChangesTotal,
                ((syncStats.SyncEndTime - syncStats.SyncStartTime).TotalMilliseconds / 1000).ToString("#.00")));
        }

        public static void SyncDatabaseFilter(SqlConnection serverConn, SqlCeConnection clientConn, string scopeName, string filterName, string filterValue)
        {
            SyncOrchestrator syncOrchestrator = new SyncOrchestrator();
            syncOrchestrator.LocalProvider = new SqlCeSyncProvider(scopeName + "-" + filterName + filterValue, clientConn);
            syncOrchestrator.RemoteProvider = new SqlSyncProvider(scopeName + "-" + filterName + filterValue, serverConn);
            syncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;

            ((SqlCeSyncProvider)syncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(Program_ApplyChangeFailed);
            SyncOperationStatistics syncStats = syncOrchestrator.Synchronize();

            // print statistics
            WriteLog(string.Format("Sync Scope {0}\t\tUploaded : {1} / Download : {2}\tTime : {3} Seconds", scopeName.Replace("Scope", "") + "-" + filterName + '-' + filterValue,
                syncStats.UploadChangesTotal, syncStats.DownloadChangesTotal,
                ((syncStats.SyncEndTime - syncStats.SyncStartTime).TotalMilliseconds / 1000).ToString("#.00")));
        }

        public static void SyncFile()
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = Param.SqlCeConfig["sshServer"].ToString(),
                UserName = Param.SqlCeConfig["sshUsername"].ToString(),
                SshPrivateKeyPath = Param.SqlCeConfig["sshKeyPath"].ToString(),
                PortNumber = int.Parse(Param.SqlCeConfig["sshPort"].ToString()),
                SshHostKeyFingerprint = Param.SqlCeConfig["sshHostKey"].ToString()
            };

            if (CanConnectInternet())
            {
                using (Session session = new Session())
                {
                    session.Open(sessionOptions);

                    RemoteFileInfo fileInfo = session.GetFileInfo(Param.ScpSoftwarePath + "/Updater.exe");
                    FileInfo fi = new FileInfo("Updater.exe");
                    if (DateTimeToUnixTimestamp(fi.LastWriteTime) != DateTimeToUnixTimestamp(fileInfo.LastWriteTime))
                    {
                        try
                        {
                            TransferOptions transferOptions = new TransferOptions();
                            transferOptions.TransferMode = TransferMode.Automatic;
                            TransferOperationResult transferResult;
                            transferResult = session.GetFiles(Param.ScpSoftwarePath + "/Updater.exe", "Updater.exe", false, transferOptions);
                            transferResult.Check();
                            foreach (TransferEventArgs transfer in transferResult.Transfers)
                            {
                                WriteLog(string.Format("Download of {0} : {1} succeeded", transfer.FileName, transfer.Destination));
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteErrorLog(string.Format("SCP Download Error : {0}", ex.Message));
                        }
                    }

                    //session.FileTransferred += FileTransferred;
                    if (Directory.Exists(Param.ApplicationDataPath + @"\log"))
                    {
                        try
                        {
                            TransferOptions transferOptions = new TransferOptions();
                            transferOptions.TransferMode = TransferMode.Automatic;

                            SynchronizationResult synchronizationResult;
                            synchronizationResult = session.SynchronizeDirectories(SynchronizationMode.Remote,
                                Param.ApplicationDataPath + @"\log", Param.ScpLogPath, false, false, SynchronizationCriteria.Time, transferOptions);
                            synchronizationResult.Check();
                        }
                        catch (Exception ex)
                        {
                            WriteErrorLog(string.Format("SCP SynchronizeDirectories Error : {0}", ex.Message));
                        }
                    }

                    session.Close();
                }
            }

        }

        private static void FileTransferred(object sender, TransferEventArgs e)
        {
            if (e.Error == null)
            {
                Console.WriteLine("Upload of {0} succeeded", e.FileName);
            }
            else
            {
                Console.WriteLine("Upload of {0} failed: {1}", e.FileName, e.Error);
            }

            if (e.Chmod != null)
            {
                if (e.Chmod.Error == null)
                {
                    Console.WriteLine("Permisions of {0} set to {1}", e.Chmod.FileName, e.Chmod.FilePermissions);
                }
                else
                {
                    Console.WriteLine("Setting permissions of {0} failed: {1}", e.Chmod.FileName, e.Chmod.Error);
                }
            }
            else
            {
                Console.WriteLine("Permissions of {0} kept with their defaults", e.Destination);
            }

            if (e.Touch != null)
            {
                if (e.Touch.Error == null)
                {
                    Console.WriteLine("Timestamp of {0} set to {1}", e.Touch.FileName, e.Touch.LastWriteTime);
                }
                else
                {
                    Console.WriteLine("Setting timestamp of {0} failed: {1}", e.Touch.FileName, e.Touch.Error);
                }
            }
            else
            {
                // This should never happen during "local to remote" synchronization
                Console.WriteLine("Timestamp of {0} kept with its default (current time)", e.Destination);
            }
        }

        public static void SyncData()
        {
            //SyncFile();

            //string connStr = "Data Source=" + Param.SqlCeFile; // + "; password=" + Param.DatabasePassword;
            //Param.SqlCeConnection = new SqlCeConnection(connStr);

            //SqlConnection serverConn = new SqlConnection(@"Data Source=" + Param.SqlCeConfig["msSqlServer"].ToString() +
            //    ";Initial Catalog=" + Param.SqlCeConfig["msSqlDatabase"].ToString() +
            //    ";User ID=" + Param.SqlCeConfig["msSqlUsername"].ToString() +
            //    ";Password=" + Param.SqlCeConfig["msSqlPassword"].ToString());

            //if (serverConn.State == ConnectionState.Closed)
            //{
            //    serverConn.Open();
            //}

            //if (!File.Exists(Param.SqlCeFile))
            //{
            //    SqlCeEngine engine = new SqlCeEngine(connStr);
            //    engine.CreateDatabase();
            //    engine.Dispose();
            //}
            ///*else
            //{
            //    try
            //    {
            //        //SqlCeCommand selectCmd = Param.SqlCeConnection.CreateCommand();
            //        //selectCmd.CommandText = "SELECT COUNT(*) cnt FROM Barcode WHERE shop <> '" + Param.ShopId + "'";
            //        //SqlCeDataAdapter adp = new SqlCeDataAdapter(selectCmd);
            //        //var dataTable = new DataTable();
            //        //adp.Fill(dataTable);
            //        //adp.Dispose();

            //        var dataTable = SqlCeQuery("SELECT COUNT(*) cnt FROM Barcode WHERE shop <> '" + Param.ShopId + "'");

            //        if (dataTable.Rows[0]["cnt"].ToString() != "0")
            //        {
            //            Param.SqlCeConnection.Close();
            //            File.Delete(Param.SqlCeFile);
            //            SqlCeEngine engine = new SqlCeEngine(connStr);
            //            engine.CreateDatabase();
            //            engine.Dispose();
            //            Param.SqlCeConnection = new SqlCeConnection(connStr);
            //        }
            //    }
            //    catch { }
            //}*/

            ////try
            ////{
            //    var scopeName = "ProvinceScope";
            //    CreateDatabaseProvision(serverConn, Param.SqlCeConnection, scopeName);
            //    SyncDatabase(serverConn, Param.SqlCeConnection, scopeName);

            //    scopeName = "DistrictScope";
            //    CreateDatabaseProvision(serverConn, Param.SqlCeConnection, scopeName);
            //    SyncDatabase(serverConn, Param.SqlCeConnection, scopeName);

            //    scopeName = "CustomerScope";
            //    CreateDatabaseProvision(serverConn, Param.SqlCeConnection, scopeName);
            //    SyncDatabase(serverConn, Param.SqlCeConnection, scopeName);

            //    scopeName = "SystemScreenPermissionScope";
            //    CreateDatabaseProvision(serverConn, Param.SqlCeConnection, scopeName);
            //    SyncDatabase(serverConn, Param.SqlCeConnection, scopeName);

            //    scopeName = "ProductScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "EmployeeScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "EmployeeTypeScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "BarcodeScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "SellHeaderScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "SellDetailScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "EmployeeScreenMappingScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "shop", Param.ShopId);

            //    scopeName = "SystemScreenScope";
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "system", "POS");
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "system", "POS");
            //    /*Collection<string> columnsToInclude = new Collection<string>();
            //    columnsToInclude.Add("system");
            //    columnsToInclude.Add("id");
            //    columnsToInclude.Add("name");
            //    columnsToInclude.Add("parent");
            //    columnsToInclude.Add("orderLevel");
            //    CreateDatabaseProvisionFilter(serverConn, Param.SqlCeConnection, scopeName, "system", "POS", columnsToInclude);
            //    SyncDatabaseFilter(serverConn, Param.SqlCeConnection, scopeName, "system", "POS");*/

            //    serverConn.Close();
            //    Param.SqlCeConnection.Close();
            ////}
            ////catch (Exception ex)
            ////{
            ////    WriteErrorLog(ex.Message);
            ////    WriteErrorLog(ex.StackTrace);
            ////}

            DataTable dt;
            int i = 0;
            //## Barcode Received ##//
            //if (Param.ShopId != "00000008" && Param.ShopId != "66666666")
            //{
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    dt = Util.DBQuery("SELECT * FROM Barcode WHERE syncReceived = 1");

                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        string sellPrice = dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString();
                        string sellNo = dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString();
                        string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();
                        string values = dt.Rows[i]["receivedDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["receivedDate"].ToString()) + "," + dt.Rows[i]["operationCost"].ToString() + "," + dt.Rows[i]["cost"].ToString() + "," + dt.Rows[i]["receivedBy"].ToString();

                        string value = sellPrice + "," + sellNo + "," + sellBy + "," + values;

                        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                        string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "sellPrice,sellNo,sellBy,receivedDate,operationCost,cost,receivedBy", value)
                        ));
                        if (!json.success.Value)
                        {
                            Console.WriteLine(json.errorMessage.Value + json.error.Value);
                        }
                        else
                        {
                            Util.DBExecute(string.Format("UPDATE Barcode SET syncReceived = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
                        }

                    }
                }
                catch (Exception ex)
                {
                    WriteErrorLog(ex.Message);
                    WriteErrorLog(ex.StackTrace);
                }
            //}

            //## Barcode ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Barcode WHERE Sync = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string sellDate = dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()).ToString();
                    string sellPrice = dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString();
                    string customer = dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString();
                    string sellNo = dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString();
                    string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();

                    string value = sellDate + "," + sellPrice + "," + customer + "," + sellNo + "," + sellBy;

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "sellDate,sellPrice,customer,sellNo,sellBy", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Barcode SET Sync = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
                    }
               }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //## Barcode Return ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Barcode WHERE syncReturn = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string sellDate = dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()).ToString();
                    string sellPrice = dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString();
                    string customer = dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString();
                    string sellNo = dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString();
                    string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();

                    string value = sellDate + "," + sellPrice + "," + customer + "," + sellNo + "," + sellBy;

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "sellDate,sellPrice,customer,sellNo,sellBy", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Barcode SET syncReturn = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
                    }

                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }


            //## Barcode Check ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Barcode WHERE syncCheck = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string inStock = dt.Rows[i]["inStock"].ToString() == "False" ? "0" : "1";
                    
                    string value = inStock;

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "inStock", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Barcode SET syncCheck = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
                    }

                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }



            //## BarcodeClaim ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM BarcodeClaim WHERE Sync = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string status = dt.Rows[i]["status"].ToString() == "" ? "" : dt.Rows[i]["status"].ToString();
                    string comment = dt.Rows[i]["comment"].ToString() == "" ? "" : dt.Rows[i]["comment"].ToString();
                    //string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();
                    string values = dt.Rows[i]["receivedDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["receivedDate"].ToString()) + "," + dt.Rows[i]["receivedBy"].ToString() ;
                    //string values2= dt.Rows[i]["claimDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["claimDate"].ToString()) + "," + dt.Rows[i]["claimBy"].ToString() + "," + dt.Rows[i]["priceClaim"].ToString() + "," + dt.Rows[i]["barcodeClaim"].ToString() + "," + dt.Rows[i]["posClaim"].ToString() + "," + dt.Rows[i]["claimNo"].ToString();

                    string value = status + "," + comment + "," + values ;

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodeClaimPos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", dt.Rows[i]["shop"].ToString(), dt.Rows[i]["barcode"].ToString(), "status,comment,receivedDate,receivedBy", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE BarcodeClaim SET sync = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), dt.Rows[i]["shop"].ToString()));
                    }
                }

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM BarcodeClaim WHERE SyncClaim = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string status = dt.Rows[i]["status"].ToString() == "" ? "" : dt.Rows[i]["status"].ToString();
                    string comment = dt.Rows[i]["comment"].ToString() == "" ? "" : dt.Rows[i]["comment"].ToString();
                    string values2 = dt.Rows[i]["claimDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["claimDate"].ToString()) + "," + dt.Rows[i]["claimBy"].ToString() + "," + dt.Rows[i]["priceClaim"].ToString() + "," + dt.Rows[i]["barcodeClaim"].ToString() + "," + dt.Rows[i]["posClaim"].ToString() + "," + dt.Rows[i]["claimNo"].ToString();

                    string value = status + "," + comment + "," + values2;


                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodeClaimPos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", dt.Rows[i]["shop"].ToString(), dt.Rows[i]["barcode"].ToString(), "status,comment,claimDate,claimBy,priceClaim,barcodeClaim,posclaim,claimNo", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE BarcodeClaim SET SyncClaim = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), dt.Rows[i]["shop"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }



            ////## Barcode ##//
            //try
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //    dt = Util.DBQuery("SELECT * FROM Barcode WHERE Sync = 1");

            //    for (i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string sellDate = dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()).ToString();
            //        string inStock = dt.Rows[i]["inStock"].ToString() == "False" ? "0" : "1";
            //        string sellPrice = dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString();
            //        string customer = dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString();
            //        string sellNo = dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString();
            //        string sellBy = dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();
            //        string values = dt.Rows[i]["receivedDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[i]["receivedDate"].ToString()) + "," + dt.Rows[i]["inStock"].ToString() + "," + dt.Rows[i]["operationCost"].ToString() + "," + dt.Rows[i]["cost"].ToString() + "," + dt.Rows[i]["receivedBy"].ToString();

            //        string value = sellDate + "," + inStock + "," + sellPrice + "," + customer + "," + sellNo + "," + sellBy + "," + values;
            //        //dt.Rows[i]["sellDate"].ToString() == "" ? "2000-01-01 00:00:00.000" : Convert.ToDateTime(dt.Rows[i]["sellDate"].ToString()) + "," + dt.Rows[i]["inStock"].ToString() == "false" ? "0" : "1" + "," + 
            //        //dt.Rows[i]["sellPrice"].ToString() == "" ? "" : dt.Rows[i]["sellPrice"].ToString() + "," + 
            //        //dt.Rows[i]["customer"].ToString() == "" ? "" : dt.Rows[i]["customer"].ToString() + "," + 
            //        //dt.Rows[i]["sellNo"].ToString() == "" ? "" : dt.Rows[i]["sellNo"].ToString() + "," + dt.Rows[i]["sellBy"].ToString() == "" ? "" : dt.Rows[i]["sellBy"].ToString();



            //        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
            //        string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "sellDate,inStock,sellPrice,customer,sellNo,sellBy,receivedDate,inStock,operationCost,cost,receivedBy", value)
            //        ));
            //        if (!json.success.Value)
            //        {
            //            Console.WriteLine(json.errorMessage.Value + json.error.Value);
            //        }
            //        else
            //        {
            //            Util.DBExecute(string.Format("UPDATE Barcode SET Sync = 0 WHERE barcode = '{0}' AND Shop = '{1}'", dt.Rows[i]["barcode"].ToString(), Param.ShopId));
            //        }


            //        //json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
            //        //string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "receivedDate,inStock,operationCost,cost,receivedBy", values)
            //        //));
            //        //if (!json.success.Value)
            //        //{
            //        //    Console.WriteLine(json.errorMessage.Value + json.error.Value);
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorLog(ex.Message);
            //    WriteErrorLog(ex.StackTrace);
            //}




            //## SellHeader ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM SellHeader WHERE Sync = 1");
                i = 0;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/sale/sale",
                    string.Format("shop={0}&saleno={1}&profit={2}&totalPrice={3}&payType={4}&cash={5}&credit={6}&customer={7}&sex={8}&age={9}&comment={10}&saledate={11}&saleby={12}&discountcash={13}&discountpercent={14}",
                                Param.ApiShopId, dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["Profit"].ToString(), dt.Rows[i]["totalPrice"].ToString(), dt.Rows[i]["payType"].ToString(),
                                dt.Rows[i]["cash"].ToString(), dt.Rows[i]["credit"].ToString(), dt.Rows[i]["customer"].ToString(), dt.Rows[i]["customerSex"].ToString(), dt.Rows[i]["customerAge"].ToString(),
                                dt.Rows[i]["comment"].ToString(), dt.Rows[i]["sellDate"].ToString(), dt.Rows[i]["sellBy"].ToString(), dt.Rows[i]["discountCash"].ToString(), dt.Rows[i]["discountPercent"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE SellHeader SET Sync = 0 WHERE sellNo = '{0}' ", dt.Rows[i]["sellNo"].ToString()));
                    }
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
                        dt.Rows[i]["cost"].ToString(), dt.Rows[i]["quantity"].ToString(), dt.Rows[i]["comment"].ToString()
                        )));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE SellDetail SET Sync = 0 WHERE sellNo = '{0}'", dt.Rows[i]["sellNo"].ToString()));
                    }
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
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE ReturnProduct SET Sync = 0 WHERE SellNo = '{0}' AND Barcode = '{1}'", dt.Rows[i]["SellNo"].ToString(), dt.Rows[i]["barcode"].ToString()));
                    }
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
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE ChangePrice SET Sync = 0 WHERE sellNo = '{0}'", dt.Rows[i]["sellNo"].ToString()));
                    }
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
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/customer/credit",
                    string.Format("shop={0}&creditno={1}&saleno={2}&paidprice={3}&paidby={4}&paiddate={5}&duedate={6}",
                            Param.ApiShopId, dt.Rows[i]["creditNo"].ToString(), dt.Rows[i]["sellNo"].ToString(), dt.Rows[i]["paidPrice"].ToString(), dt.Rows[i]["paidBy"].ToString(), dt.Rows[i]["paidDate"].ToString(), dt.Rows[i]["dueDate"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE CreditCustomer SET Sync = 0 WHERE creditNo = '{0}'", dt.Rows[i]["creditNo"].ToString()));
                    }
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
                    string val = dt.Rows[i]["price"].ToString() + "," + dt.Rows[i]["price1"].ToString() + "," + dt.Rows[i]["price2"].ToString() + "," + dt.Rows[i]["price3"].ToString() + "," + dt.Rows[i]["price4"].ToString() + "," + dt.Rows[i]["price5"].ToString() + "," + dt.Rows[i]["price7"].ToString() + "," + dt.Rows[i]["quantity"].ToString() + "," + dt.Rows[i]["cost"].ToString();

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updatePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["product"].ToString(), "price,price1,price2,price3,price4,price5,price7,quantity,cost", val)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Product SET Sync = 0 WHERE product = '{0}' AND Shop = '{1}'", dt.Rows[i]["product"].ToString(), Param.ShopId));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }


            ////## PurchaseOrder (Noserial) ##//
            //try
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //    dt = Util.DBQuery("SELECT * FROM PurchaseOrder WHERE Sync = 1");
            //    i = 0;
            //    for (i = 0; i < dt.Rows.Count; i++)
            //    {
            //        string val = dt.Rows[i]["ReceivedQuantity"].ToString() + "," + dt.Rows[i]["ReceivedBy"].ToString() + "," + dt.Rows[i]["ReceivedDate"].ToString();

            //        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateNsPos",
            //        string.Format("shop={0}&orderno={4}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["product"].ToString(), "receivedQuantity,receivedBy,receivedDate", val, dt.Rows[i]["orderNo"].ToString())
            //        ));
            //        if (!json.success.Value)
            //        {
            //            Console.WriteLine(json.errorMessage.Value + json.error.Value);
            //        }
            //        else
            //        {
            //            Util.DBExecute(string.Format("UPDATE Product SET Sync = 0 WHERE product = '{0}' AND Shop = '{1}'", dt.Rows[i]["product"].ToString(), Param.ShopId));
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorLog(ex.Message);
            //    WriteErrorLog(ex.StackTrace);
            //}

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
                    string.Format("shop={0}&mobile={1}&firstname={2}&lastname={3}&nickname={4}&sex={5}&birthday={6}&citizen={7}&cardno={8}&email={9}&address={10}&address2={11}&subdistrict={12}&district={13}&province={14}&zipcode={15}&shopname={16}&shopsameaddress={17}&shopaddress={18}&shopaddress2={19}&shopsubdistrict={20}&shopdistrict={21}&shopprovince={22}&shopzipcode={23}&credit={24}&sellprice={25}&addBy={26}&updateBy={27}&customer={28}",
                                Param.ApiShopId, dt.Rows[i]["mobile"].ToString(), dt.Rows[i]["firstname"].ToString(), dt.Rows[i]["lastname"].ToString(), dt.Rows[i]["nickname"].ToString(),
                                dt.Rows[i]["sex"].ToString(), dt.Rows[i]["birthday"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["birthday"].ToString()), dt.Rows[i]["citizenId"].ToString(), dt.Rows[i]["cardno"].ToString(), dt.Rows[i]["email"].ToString(),
                                dt.Rows[i]["address"].ToString(), dt.Rows[i]["address2"].ToString(), dt.Rows[i]["subdistrict"].ToString(), dt.Rows[i]["district"].ToString(), dt.Rows[i]["province"].ToString(),
                                dt.Rows[i]["zipcode"].ToString(), dt.Rows[i]["shopname"].ToString(), dt.Rows[i]["shopsameaddress"].ToString() == "False" ? 0 : 1, dt.Rows[i]["shopaddress"].ToString(), dt.Rows[i]["shopaddress2"].ToString(),
                                dt.Rows[i]["shopsubdistrict"].ToString(), dt.Rows[i]["shopdistrict"].ToString(), dt.Rows[i]["shopprovince"].ToString(), dt.Rows[i]["shopzipcode"].ToString(), dt.Rows[i]["credit"].ToString(), dt.Rows[i]["sellprice"].ToString(), dt.Rows[i]["addBy"].ToString(), dt.Rows[i]["updateBy"].ToString(), dt.Rows[i]["customer"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Customer SET Sync = 0 WHERE customer = '{0}'", dt.Rows[i]["customer"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }


            ////## Customer Update ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Customer WHERE SyncUpdate = 1");

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    string inStock = dt.Rows[i]["inStock"].ToString() == "False" ? "0" : "1";

                    string value = inStock;

                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/updateBarcodePos",
                    string.Format("shop={0}&id={1}&entity={2}&value={3}", Param.ApiShopId, dt.Rows[i]["barcode"].ToString(), "inStock", value)
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Customer SET Sync = 0 WHERE customer = '{0}'", dt.Rows[i]["customer"].ToString()));
                    }

                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## Employee ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM Employee WHERE Sync = 1");
                i = 0;
                //    var azureTable = Param.AzureTableClient.GetTableReference("Customer");
                //    TableBatchOperation batchOperation = new TableBatchOperation();

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/Add",
                    string.Format("shop={0}&employeeId={1}&employeeType={2}&firstname={3}&lastname={4}&nickname={5}&mobile={6}&code={7}&username={8}&password={9}&status={10}&logindate={11}&logincount={12}&addby={13}&updateby={14}",
                                Param.ApiShopId, dt.Rows[i]["employeeid"].ToString(), dt.Rows[i]["employeeType"].ToString(), dt.Rows[i]["firstname"].ToString(), dt.Rows[i]["lastname"].ToString(), dt.Rows[i]["nickname"].ToString(),
                                dt.Rows[i]["mobile"].ToString(), dt.Rows[i]["code"].ToString(), dt.Rows[i]["username"].ToString(),
                                dt.Rows[i]["password"].ToString(), dt.Rows[i]["status"].ToString() == "False" ? 0 : 1,
                                dt.Rows[i]["logindate"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["logindate"].ToString()), dt.Rows[i]["logincount"].ToString(), dt.Rows[i]["addby"].ToString(), dt.Rows[i]["updateby"].ToString())
                    ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE Employee SET Sync = 0 WHERE employeeid = '{0}'", dt.Rows[i]["employeeid"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            ////## EmployeeType ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM EmployeeType WHERE Sync = 1");
                i = 0;
                //    var azureTable = Param.AzureTableClient.GetTableReference("Customer");
                //    TableBatchOperation batchOperation = new TableBatchOperation();

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/AddType",
                    string.Format("shop={0}&id={1}&name={2}&level={3}&active={4}&adddate={5}&addby={6}&updatedate={7}&updateby={8}",
                            Param.ApiShopId, dt.Rows[i]["id"].ToString(), dt.Rows[i]["name"].ToString(), dt.Rows[i]["orderLevel"].ToString(), dt.Rows[i]["active"].ToString() == "False" ? 0 : 1, dt.Rows[i]["addDate"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["addDate"].ToString()), dt.Rows[i]["addBy"].ToString(), dt.Rows[i]["updateDate"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["updateDate"].ToString()), dt.Rows[i]["updateBy"].ToString())));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE EmployeeType SET Sync = 0 WHERE id = '{0}'", dt.Rows[i]["id"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }


            ////## EmployeeScreenMapping ##//
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                dt = Util.DBQuery("SELECT * FROM EmployeeScreenMapping WHERE Sync = 1");
                i = 0;
                //    var azureTable = Param.AzureTableClient.GetTableReference("Customer");
                //    TableBatchOperation batchOperation = new TableBatchOperation();

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/employee/AddScreen",
                    string.Format("shop={0}&system={1}&screen={2}&permission={3}&employeetype={4}&adddate={5}&addby={6}",
                            Param.ApiShopId, dt.Rows[i]["system"].ToString(), dt.Rows[i]["screen"].ToString(), dt.Rows[i]["permission"].ToString(), dt.Rows[i]["employeeType"].ToString(), dt.Rows[i]["addDate"].ToString() == "" ? DateTime.Now : DateTime.Parse(dt.Rows[i]["addDate"].ToString()),dt.Rows[i]["addBy"].ToString()) ));
                    if (!json.success.Value)
                    {
                        Console.WriteLine(json.errorMessage.Value + json.error.Value);
                    }
                    else
                    {
                        Util.DBExecute(string.Format("UPDATE EmployeeScreenMapping SET Sync = 0 WHERE system = '{0}', screen = '{1}', permission = '{2}', employeeType = '{3}' ", dt.Rows[i]["system"].ToString(), dt.Rows[i]["screen"].ToString(), dt.Rows[i]["permission"].ToString(), dt.Rows[i]["employeeType"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message);
                WriteErrorLog(ex.StackTrace);
            }

            //////## InventoryCount ##//
            //try
            //{
            //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //    dt = Util.DBQuery("SELECT * FROM InventoryCount WHERE Sync = 1");
            //    i = 0;
            //    for (i = 0; i < dt.Rows.Count; i++)
            //    {
            //        dynamic json = JsonConvert.DeserializeObject(Util.ApiProcess("/product/addCount",
            //        string.Format("shop={0}&product={1}&quantity={2}", Param.ApiShopId, dt.Rows[i]["product"].ToString(), dt.Rows[i]["quantity"].ToString())
            //        ));
            //        if (!json.success.Value)
            //        {
            //            Console.WriteLine(json.errorMessage.Value + json.error.Value);
            //        }
            //        else
            //        {
            //            Util.DBExecute(string.Format("UPDATE InventoryCount SET Sync = 0 WHERE product = '{0}'", dt.Rows[i]["product"].ToString()));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    WriteErrorLog(ex.Message);
            //    WriteErrorLog(ex.StackTrace);
            //}

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

        public static void PrintProductClaim(string sellNo)
        {
            Param.Page = 0;
            Param.d = 0;
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM SellDetail WHERE SellNo = '{0}'", sellNo));

            PaperSize paperSize = new PaperSize();
            paperSize.RawKind = (int)PaperKind.A4;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;

            int count = int.Parse(dt.Rows[0]["cnt"].ToString());

            for (int i = 1; i <= Math.Ceiling((float)count / Param.NumClaim); i++)
            {
                if (i > 1)
                {
                    Param.Page = Param.Page + 1;
                }

                pd.PrintPage += (_, g) =>
                {
                    PrintProductClaim(g, sellNo);
                };

                pd.Print();
                Param.Page += Param.NumClaim;
            }
        }

        private static void PrintProductClaim(PrintPageEventArgs g, string sellNo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {

                DataTable dtHeader = Util.DBQuery(string.Format(@"SELECT h.TotalPrice Price, IFNULL(h.Cash,0) Cash, c.Firstname, c.Lastname, c.Mobile, datetime(h.SellDate, 'localtime') SellDate, h.SellBy, h.discountCash, h.discountPercent, c.address
                    FROM SellHeader h
                        LEFT JOIN Customer c
                        ON h.Customer = c.Customer
                    WHERE h.SellNo = '{0}'"
                    , sellNo));

           
                var width = 800;
                var gab = 20;

                //if (Param.PrintLogo == "Y")
                //{
                //    if (!File.Exists(Param.LogoPath))
                //    {
                //        if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                //        if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                //        using (var client = new WebClient())
                //        {
                //            Param.LogoPath = "1234";
                //            client.DownloadFile(Param.LogoUrl, Param.LogoPath);
                //            Param.Logo = Param.LogoUrl;
                //        }

                //    }
                //    Image image = Image.FromFile(Param.LogoPath);
                //    Rectangle destRect = new Rectangle(0, 0, width, 280);
                //    //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                //    g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                //}


                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("Calibri", 6);
                //if (Param.Logo == Param.LogoUrl && Param.PrintLogo == "Y")
                //{
                //    g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                //    g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                //    stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                //    g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
                //}
                var pX = 0;
                var pY = 0;
                if (Param.PrintLogo == "Y")
                {
                    pX = 0;
                    pY = 280;
                }
                else
                {
                    pX = 0;
                    pY = 10;
                }

                //if (Param.MemberType == "Shop")
                //{
                //    stringFont = new Font("DilleniaUPC", 25, FontStyle.Bold);
                //    g.Graphics.DrawString(Param.ShopName, stringFont, brush, new PointF(pX, pY + 6));
                //    pY += 50;
                //}

                stringFont = new Font("DilleniaUPC", 20);

                g.Graphics.DrawString("วันที่", stringFont, brush, new PointF(pX, pY));

                g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm"), stringFont, brush, new PointF(pX + 70, pY));

                g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 620, pY));

                string measureString = sellNo;
                SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                pY += 35;


                stringFont = new Font("DilleniaUPC", 20);
                g.Graphics.DrawString("ชื่อร้านค้า : " + dtHeader.Rows[0]["address"].ToString() + " " +
                    ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                    " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
                    : "")
                    , stringFont, brush, new PointF(pX, pY));
                pY += 30;

                //stringFont = new Font("DilleniaUPC", 20);
               
                

                stringFont = new Font("DilleniaUPC", 25, FontStyle.Bold);
                measureString = "ใบรับสินค้าเคลม";
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
                pY += 40;

                stringFont = new Font("DilleniaUPC", 20);
                DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name Name, sd.Quantity ProductCount, sd.SellPrice SellPrice
                        FROM  SellDetail sd
                            LEFT JOIN Product p 
                            ON sd.Product = p.Product 
                    WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0
                    ORDER BY p.Name", sellNo, Param.ShopId));

                DataTable dtCnt = Util.DBQuery(string.Format(@"SELECT SUM(sd.Quantity) cnt, SUM(sd.SellPrice) SellP
                        FROM  SellDetail sd
                            LEFT JOIN Product p 
                            ON sd.Product = p.Product 
                    WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0", sellNo, Param.ShopId));

                var totalQty = 0;
                totalQty = int.Parse(dtCnt.Rows[0]["cnt"].ToString());
                var totalPrice = 0;
                totalPrice = int.Parse(dtCnt.Rows[0]["SellP"].ToString());


                var sumQty = 0;
                var sumPrice = 0;
                int i = 0;
                //if (Param.Page >= Param.Num)
                //{
                if (Param.Page == 31)
                {
                    Param.d = int.Parse(Param.Page.ToString()) - 1;
                }
                else
                {
                    Param.d = int.Parse(Param.Page.ToString());
                }
                //d = d - 49;
                for (i = Param.d; i < Param.NumClaim + Param.Page/*dt.Rows.Count*/; i++)
                {
                    if (i == dt.Rows.Count)
                    {
                        break;
                    }
                    int val = i + 1;
                    g.Graphics.DrawString(val.ToString(), stringFont, brush, new PointF(pX, pY));
                    g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 40, pY));
                    g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX + 630, pY));

                    //g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 620, pY + 3, 620, 10);
                    //g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                    //    stringFont, brush, new PointF(pX + 620, pY));
                    //measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                    //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                    sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                    pY += 30;
                    Param.Qty += 1;
                }


                //}
                //else
                //{
                //    d = 1;

                //    for (i = d; i < Param.Num; i++) //dt.Rows.Count; i++)
                //    {
                //        g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                //        g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));

                //        g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 620, pY + 3, 620, 10);
                //        g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                //            stringFont, brush, new PointF(pX + 620, pY));
                //        measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                //        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                //        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                //        sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                //        sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                //        pY += 20;
                //    }
                //}

                //for (i = d; i < Param.Page; i++) //dt.Rows.Count; i++)
                //{
                //    g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                //    g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));

                //    g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 500, pY + 3, 500, 10);
                //    g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                //        stringFont, brush, new PointF(pX + 650, pY));
                //    measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                //    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                //    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                //    sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                //    sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                //    pY += 20;
                //}
                if (i == dt.Rows.Count)
                {
                    pY += 4;
                    stringFont = new Font("DilleniaUPC", 20, FontStyle.Bold);
                    g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, totalQty/*sumQty*/), stringFont, brush, new PointF(pX, pY));
                    //measureString = "" + totalPrice.ToString("#,##0");/*sumPrice.ToString("#,##0");*/
                    //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                    pY += 220;

                    stringFont = new Font("DilleniaUPC", 20, FontStyle.Bold);
                    g.Graphics.DrawString(string.Format("ลงชื่อ...........................................(ผู้รับสินค้า)"), stringFont, brush, new PointF(pX, pY));
                    pY += 35;
                    g.Graphics.DrawString(string.Format("วันที่............/............/..............."), stringFont, brush, new PointF(pX, pY));

                    //stringFont = new Font("DilleniaUPC", 15);
                    //g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    //measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - totalPrice/*sumPrice*/).ToString("#,##0");
                    //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                    //pY += 30;

                    //g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                    //pY += 5;

                    //stringFont = new Font("DilleniaUPC", 15);
                    //g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
                    //    ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                    //    " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
                    //    : "")
                    //    , stringFont, brush, new PointF(pX, pY));

                    ///*stringFont = new Font("DilleniaUPC", 11);
                    //measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
                    //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
                    //pY += 20;

                    //stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                    //measureString = Param.FooterText;
                    //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));
                    Param.Qty = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void PrintReceipt(string sellNo)
        {
            if (Param.PaperSize == "A4")
            {
                Param.Page = 0;
                Param.d = 0;
                DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM SellDetail WHERE SellNo = '{0}'", sellNo));

                PaperSize paperSize = new PaperSize();
                paperSize.RawKind = (int)PaperKind.A4;

                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.PaperSize = paperSize;
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                pd.PrinterSettings.PrinterName = Param.DevicePrinter;

                int count = int.Parse(dt.Rows[0]["cnt"].ToString());
               
                for (int i = 1; i <= Math.Ceiling((float)count / Param.Num); i++)
                {
                    //if (i > 1)
                    //{
                    //    Param.Page = Param.Page + 1;
                    //}

                    pd.PrintPage += (_, g) =>
                    {
                        PrintReceipt(g, sellNo);
                    };

                    pd.Print();
                    Param.Page += Param.Num;
                }
            }
            else
            {
                DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM SellDetail WHERE SellNo = '{0}'", sellNo));

                var hight = 290 + int.Parse(dt.Rows[0]["cnt"].ToString()) * 13;
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

        }

        private static void PrintReceipt(PrintPageEventArgs g, string sellNo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            try
            {

                DataTable dtHeader = Util.DBQuery(string.Format(@"SELECT h.TotalPrice Price, IFNULL(h.Cash,0) Cash, c.Firstname, c.Lastname, c.Mobile, datetime(h.SellDate, 'localtime') SellDate, h.SellBy, h.discountCash, h.discountPercent
                    FROM SellHeader h
                        LEFT JOIN Customer c
                        ON h.Customer = c.Customer
                    WHERE h.SellNo = '{0}'"
                    , sellNo));

                if (Param.PaperSize == "80")
                {
                    var width = 280;
                    var gab = 5;

                    if (Param.PrintLogo == "Y")
                    {
                        if (!File.Exists(Param.LogoPath))
                        {
                            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                            if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                            using (var client = new WebClient())
                            {
                                Param.LogoPath = "1234";
                                client.DownloadFile(Param.LogoUrl, Param.LogoPath);
                                Param.Logo = Param.LogoUrl;
                            }

                        }
                        Image image = Image.FromFile(Param.LogoPath);
                        Rectangle destRect = new Rectangle(0, 0, width, 100);
                        //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                        g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    }


                    SolidBrush brush = new SolidBrush(Color.Black);
                    Font stringFont = new Font("Calibri", 6);
                    //if (Param.Logo == Param.LogoUrl && Param.PrintLogo == "Y")
                    //{
                    //    g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                    //    g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                    //    stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                    //    g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
                    //}

                    var pX = 0;
                    var pY = 0;
                    if (Param.PrintLogo == "Y")
                    {
                        pX = 0;
                        pY = 90;
                    }
                    else
                    {
                        pX = 0;
                        pY = 5;
                    }

                    if (Param.MemberType == "Shop")
                    {
                        stringFont = new Font("DilleniaUPC", 12, FontStyle.Bold);
                        g.Graphics.DrawString(Param.ShopName, stringFont, brush, new PointF(pX, pY + 6));
                        pY += 20;
                    }

                    stringFont = new Font("Calibri", 8);
                    g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

                    stringFont = new Font("DilleniaUPC", 13);
                    g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 188, pY));

                    stringFont = new Font("Calibri", 10, FontStyle.Bold);
                    string measureString = sellNo;
                    SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width + gab, pY + 3));
                    pY += 12;

                    stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
                    measureString = Param.HeaderName; // "ใบเสร็จรับเงิน";
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
                    pY += 30;

                    stringFont = new Font("Cordia New", 10);
                    DataTable dt = Util.DBQuery(string.Format
                        (@"SELECT p.Name Name, sd.Quantity ProductCount, sd.SellPrice SellPrice
                            FROM  SellDetail sd
                                LEFT JOIN Product p 
                                ON sd.Product = p.Product 
                            WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0
                            ORDER BY p.Name ", sellNo, Param.ShopId));

                    var sumQty = 0;
                    var sumPrice = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 15, pY));

                        g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 220, pY + 5, 150, 10);
                        g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                            stringFont, brush, new PointF(pX + 222, pY));
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

                    stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                    measureString = "" + sumPrice.ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 15;

                    //g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                    /*
                    stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                    //g.Graphics.DrawString(string.Format("ส่วนลด {0}", int.Parse(dtHeader.Rows[0]["discountCash"].ToString())), stringFont, brush, new PointF(pX, pY));
                    g.Graphics.DrawString("ส่วนลด ", stringFont, brush, new PointF(pX + 188, pY));


                    stringFont = new Font("Cordia New", 11);
                    measureString = int.Parse(dtHeader.Rows[0]["discountCash"].ToString()).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 17;

                    stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                    g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 188, pY));

                    stringFont = new Font("Cordia New", 12, FontStyle.Bold);
                    measureString = (sumPrice - int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));

                    pY += 17;
                    stringFont = new Font("Cordia New", 11);
                    g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    measureString = "เงินทอน  " + ((int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice) + int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 23; */

                    //********************************
                    stringFont = new Font("DilleniaUPC", 11, FontStyle.Bold);
                    //g.Graphics.DrawString(string.Format("ส่วนลด {0}", int.Parse(dtHeader.Rows[0]["discountCash"].ToString())), stringFont, brush, new PointF(pX, pY));

                    if (dtHeader.Rows[0]["discountPercent"].ToString() == "0")
                    {
                        g.Graphics.DrawString("ส่วนลด ", stringFont, brush, new PointF(pX + 188, pY));

                        stringFont = new Font("Cordia New", 11);
                        measureString = int.Parse(dtHeader.Rows[0]["discountCash"].ToString()).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;

                        stringFont = new Font("DilleniaUPC", 11, FontStyle.Bold);
                        g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 188, pY));

                        stringFont = new Font("Cordia New", 13, FontStyle.Bold);
                        measureString = (sumPrice - int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;

                        stringFont = new Font("Cordia New", 11);
                        g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;
                    }
                    else if (dtHeader.Rows[0]["discountPercent"].ToString() != "0")
                    {
                        g.Graphics.DrawString(string.Format("ส่วนลด ({0}%)", dtHeader.Rows[0]["discountPercent"].ToString()), stringFont, brush, new PointF(pX + 188, pY));

                        double perBath = (int)Math.Round(sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString()) / 100);
                        double perTotal = (int)Math.Round(sumPrice - (sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString()) / 100));

                        stringFont = new Font("Cordia New", 11);
                        measureString = perBath.ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;

                        stringFont = new Font("DilleniaUPC", 11, FontStyle.Bold);
                        g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 188, pY));

                        stringFont = new Font("Cordia New", 13, FontStyle.Bold);
                        measureString = perTotal.ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;

                        stringFont = new Font("Cordia New", 11);
                        g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - perTotal/*sumPrice*/).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 17;
                    }

                    //*************************

                    g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                    pY += 5;

                    stringFont = new Font("Cordia New", 10);
                    g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
                        ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                        " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(6) + ")"
                        : "")
                        , stringFont, brush, new PointF(pX, pY));


                    /*stringFont = new Font("DilleniaUPC", 11);
                    measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
                    pY += 17;

                    stringFont = new Font("DilleniaUPC", 12, FontStyle.Bold);
                    measureString = Param.FooterText;
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));
                }
                else if (Param.PaperSize == "76")
                {
                    var width = 240;
                    var gab = 5;

                    if (Param.PrintLogo == "Y")
                    {
                        if (!File.Exists(Param.LogoPath))
                        {
                            if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                            if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                            using (var client = new WebClient())
                            {
                                Param.LogoPath = "1234";
                                client.DownloadFile(Param.LogoUrl, Param.LogoPath);
                                Param.Logo = Param.LogoUrl;
                            }

                        }
                        Image image = Image.FromFile(Param.LogoPath);
                        Rectangle destRect = new Rectangle(0, 0, width, 90);
                        //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                        g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    }


                    SolidBrush brush = new SolidBrush(Color.Black);
                    Font stringFont = new Font("Calibri", 6);
                    //if (Param.Logo == Param.LogoUrl && Param.PrintLogo == "Y")
                    //{
                    //    g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                    //    g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                    //    stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                    //    g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
                    //}
                    var pX = 0;
                    var pY = 0;
                    if (Param.PrintLogo == "Y")
                    {
                        pX = 0;
                        pY = 90;
                    }
                    else
                    {
                        pX = 0;
                        pY = 5;
                    }

                    if (Param.MemberType == "Shop")
                    {
                        stringFont = new Font("DilleniaUPC", 10, FontStyle.Bold);
                        g.Graphics.DrawString(Param.ShopName, stringFont, brush, new PointF(pX, pY + 6));
                        pY += 20;
                    }

                    stringFont = new Font("Calibri", 7);
                    g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

                    stringFont = new Font("DilleniaUPC", 11);
                    g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 165, pY));

                    stringFont = new Font("Calibri", 8, FontStyle.Bold);
                    string measureString = sellNo;
                    SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width + gab, pY + 3));
                    pY += 15;

                    stringFont = new Font("DilleniaUPC", 12, FontStyle.Bold);
                    measureString = Param.HeaderName; // "ใบเสร็จรับเงิน";
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
                    pY += 25;

                    stringFont = new Font("Cordia New", 9);
                    DataTable dt = Util.DBQuery(string.Format
                           (@"SELECT p.Name Name, sd.Quantity ProductCount, sd.SellPrice SellPrice
                            FROM  SellDetail sd
                                LEFT JOIN Product p 
                                ON sd.Product = p.Product 
                            WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0
                            ORDER BY p.Name", sellNo, Param.ShopId));

                    var sumQty = 0;
                    var sumPrice = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 13, pY));

                        g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 190, pY + 3, 150, 10);
                        g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                            stringFont, brush, new PointF(pX + 190, pY));
                        measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                        sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                        pY += 13;
                    }

                    //pY += 4;
                    stringFont = new Font("DilleniaUPC", 10, FontStyle.Bold);
                    g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, sumQty), stringFont, brush, new PointF(pX, pY));
                    stringFont = new Font("Cordia New", 10, FontStyle.Bold);
                    measureString = "" + sumPrice.ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 15;
                    /*
                    stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                    //g.Graphics.DrawString(string.Format("ส่วนลด {0}", int.Parse(dtHeader.Rows[0]["discountCash"].ToString())), stringFont, brush, new PointF(pX, pY));
                    g.Graphics.DrawString("ส่วนลด ", stringFont, brush, new PointF(pX + 188, pY));


                    stringFont = new Font("Cordia New", 11);
                    measureString = int.Parse(dtHeader.Rows[0]["discountCash"].ToString()).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 17;

                    stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                    g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 188, pY));

                    stringFont = new Font("Cordia New", 12, FontStyle.Bold);
                    measureString = (sumPrice - int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));

                    pY += 17;
                    stringFont = new Font("Cordia New", 11);
                    g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    measureString = "เงินทอน  " + ((int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice) + int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 23;

                    g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                    pY += 5;

                    measureString = "" + sumPrice.ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 17;
                    stringFont = new Font("Cordia New", 9);
                    g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    pY += 23;
                    */
                    //********************************
                    stringFont = new Font("Cordia New", 9, FontStyle.Bold);
                    //g.Graphics.DrawString(string.Format("ส่วนลด {0}", int.Parse(dtHeader.Rows[0]["discountCash"].ToString())), stringFont, brush, new PointF(pX, pY));

                    if (dtHeader.Rows[0]["discountPercent"].ToString() == "0")
                    {
                        g.Graphics.DrawString("ส่วนลด ", stringFont, brush, new PointF(pX + 150, pY));

                        stringFont = new Font("Cordia New", 9);
                        measureString = int.Parse(dtHeader.Rows[0]["discountCash"].ToString()).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;

                        stringFont = new Font("Cordia New", 9, FontStyle.Bold);
                        g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 150, pY));

                        stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                        measureString = (sumPrice - int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;

                        stringFont = new Font("Cordia New", 9);
                        g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - sumPrice).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;
                    }
                    else if (dtHeader.Rows[0]["discountPercent"].ToString() != "0")
                    {
                        g.Graphics.DrawString(string.Format("ส่วนลด ({0}%)", dtHeader.Rows[0]["discountPercent"].ToString()), stringFont, brush, new PointF(pX + 150, pY));

                        double perBath = (int)Math.Round(sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString()) / 100);
                        double perTotal = (int)Math.Round(sumPrice - (sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString()) / 100));

                        stringFont = new Font("Cordia New", 9);
                        measureString = perBath.ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;

                        stringFont = new Font("Cordia New", 9, FontStyle.Bold);
                        g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 150, pY));

                        stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                        measureString = perTotal.ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;

                        stringFont = new Font("Cordia New", 9);
                        g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                        measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - perTotal/*sumPrice*/).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        pY += 15;
                    }

                    //*************************
                    g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                    pY += 5;

                    stringFont = new Font("Cordia New", 9);
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

                    stringFont = new Font("DilleniaUPC", 10, FontStyle.Bold);
                    measureString = Param.FooterText;
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));
                }
                else if (Param.PaperSize == "A4")
                {
                    var width = 800;
                    var gab = 20;

                    //if (Param.PrintLogo == "Y")
                    //{
                    //    if (!File.Exists(Param.LogoPath))
                    //    {
                    //        if (!Directory.Exists("Resource/Images")) Directory.CreateDirectory("Resource/Images");
                    //        if (File.Exists(Param.LogoPath)) File.Delete(Param.LogoPath);
                    //        using (var client = new WebClient())
                    //        {
                    //            Param.LogoPath = "1234";
                    //            client.DownloadFile(Param.LogoUrl, Param.LogoPath);
                    //            Param.Logo = Param.LogoUrl;
                    //        }

                    //    }
                    //    Image image = Image.FromFile(Param.LogoPath);
                    //    Rectangle destRect = new Rectangle(0, 0, width, 280);
                    //    //Rectangle destRect = new Rectangle(0, 0, width, image.Height * width / image.Width);
                    //    g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    //}


                    SolidBrush brush = new SolidBrush(Color.Black);
                    Font stringFont = new Font("Calibri", 6);
                    //if (Param.Logo == Param.LogoUrl && Param.PrintLogo == "Y")
                    //{
                    //    g.Graphics.DrawString("http:// www.", stringFont, brush, new PointF(62, 49));
                    //    g.Graphics.DrawString(".co.th", stringFont, brush, new PointF(193, 49));
                    //    stringFont = new Font("Calibri", 6.5f, FontStyle.Bold);
                    //    g.Graphics.DrawString("R e m a x T h a i l a n d", stringFont, brush, new PointF(109, 48.3f));
                    //}
                    var pX = 0;
                    var pY = 0;
                    if (Param.PrintLogo == "Y")
                    {
                        pX = 0;
                        pY = 280;
                    }
                    else
                    {
                        pX = 0;
                        pY = 5;
                    }

                    if (Param.MemberType == "Shop")
                    {
                        stringFont = new Font("DilleniaUPC", 20, FontStyle.Bold);
                        g.Graphics.DrawString(Param.ShopName, stringFont, brush, new PointF(pX, pY + 6));
                        pY += 20;
                    }

                    stringFont = new Font("DilleniaUPC", 17);
                    g.Graphics.DrawString(DateTime.Parse(dtHeader.Rows[0]["SellDate"].ToString()).ToString("dd/MM/yyyy HH:mm") + " : " + dtHeader.Rows[0]["SellBy"].ToString(), stringFont, brush, new PointF(pX, pY + 6));

                    stringFont = new Font("DilleniaUPC", 20);
                    g.Graphics.DrawString("เลขที่ ", stringFont, brush, new PointF(pX + 620, pY));

                    stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                    string measureString = sellNo;
                    SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(sellNo, stringFont, brush, new PointF(width - stringSize.Width - gab, pY + 3));
                    pY += 35;

                    stringFont = new Font("DilleniaUPC", 20, FontStyle.Bold);
                    measureString = Param.HeaderName; // "ใบเสร็จรับเงิน";
                    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
                    pY += 30;

                    stringFont = new Font("DilleniaUPC", 15);
                    DataTable dt = Util.DBQuery(string.Format(@"SELECT p.Name Name, sd.Quantity ProductCount, sd.SellPrice SellPrice
                            FROM  SellDetail sd
                                LEFT JOIN Product p 
                                ON sd.Product = p.Product 
                       WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0
                       ORDER BY p.Name", sellNo, Param.ShopId));

                    DataTable dtCnt = Util.DBQuery(string.Format(@"SELECT SUM(sd.Quantity) cnt, SUM(sd.SellPrice) SellP
                            FROM  SellDetail sd
                                LEFT JOIN Product p 
                                ON sd.Product = p.Product 
                       WHERE p.Shop = '{1}' AND sd.SellNo = '{0}' AND sd.Quantity <> 0", sellNo, Param.ShopId));

                    var totalQty = 0;
                    totalQty = int.Parse(dtCnt.Rows[0]["cnt"].ToString());
                    var totalPrice = 0;
                    totalPrice = int.Parse(dtCnt.Rows[0]["SellP"].ToString());


                    var sumQty = 0;
                    var sumPrice = 0;
                    int i = 0;
                    //if (Param.Page >= Param.Num)
                    //{
                        Param.d = int.Parse(Param.Page.ToString());
                        //d = d - 49;
                        for (i = Param.d; i < Param.Num + Param.Page/*dt.Rows.Count*/; i++)
                        {
                            if (i == dt.Rows.Count)
                            {
                                break;
                            }
                            g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                            g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));

                            g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 620, pY + 3, 620, 10);
                            g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                                stringFont, brush, new PointF(pX + 620, pY));
                            measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                            sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                            pY += 20;
                            Param.Qty += 1;
                        }
                        

                    //}
                    //else
                    //{
                    //    d = 1;

                    //    for (i = d; i < Param.Num; i++) //dt.Rows.Count; i++)
                    //    {
                    //        g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    //        g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));

                    //        g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 620, pY + 3, 620, 10);
                    //        g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                    //            stringFont, brush, new PointF(pX + 620, pY));
                    //        measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                    //        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                    //        sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    //        sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                    //        pY += 20;
                    //    }
                    //}

                    //for (i = d; i < Param.Page; i++) //dt.Rows.Count; i++)
                    //{
                    //    g.Graphics.DrawString(int.Parse(dt.Rows[i]["ProductCount"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                    //    g.Graphics.DrawString(dt.Rows[i]["Name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));

                    //    g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 500, pY + 3, 500, 10);
                    //    g.Graphics.DrawString("@" + (int.Parse(dt.Rows[i]["SellPrice"].ToString()) / int.Parse(dt.Rows[i]["ProductCount"].ToString())).ToString("#,##0"),
                    //        stringFont, brush, new PointF(pX + 650, pY));
                    //    measureString = int.Parse(dt.Rows[i]["SellPrice"].ToString()).ToString("#,##0");
                    //    stringSize = g.Graphics.MeasureString(measureString, stringFont);
                    //    g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                    //    sumQty += int.Parse(dt.Rows[i]["ProductCount"].ToString());
                    //    sumPrice += int.Parse(dt.Rows[i]["SellPrice"].ToString());
                    //    pY += 20;
                    //}
                    if (i == dt.Rows.Count)
                    {
                        pY += 4;
                        stringFont = new Font("DilleniaUPC", 17, FontStyle.Bold);
                        g.Graphics.DrawString(string.Format("รวม {0} รายการ ({1} ชิ้น)", dt.Rows.Count, totalQty/*sumQty*/), stringFont, brush, new PointF(pX, pY));
                        measureString = "" + totalPrice.ToString("#,##0");/*sumPrice.ToString("#,##0");*/
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                        pY += 25;
                        //stringFont = new Font("Cordia New", 11, FontStyle.Bold);
                        //measureString = "" + sumPrice.ToString("#,##0");
                        //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY));
                        //pY += 15;
                        //********************************

                        stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
                        //g.Graphics.DrawString(string.Format("ส่วนลด {0}", int.Parse(dtHeader.Rows[0]["discountCash"].ToString())), stringFont, brush, new PointF(pX, pY));

                        if (dtHeader.Rows[0]["discountPercent"].ToString() == "0")
                        {
                            g.Graphics.DrawString("ส่วนลด ", stringFont, brush, new PointF(pX + 600, pY));

                            stringFont = new Font("DilleniaUPC", 15);
                            measureString = int.Parse(dtHeader.Rows[0]["discountCash"].ToString()).ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 25;

                            stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
                            g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 600, pY));

                            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                            measureString = (sumPrice - int.Parse(dtHeader.Rows[0]["discountCash"].ToString())).ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 30;

                            stringFont = new Font("DilleniaUPC", 15);
                            g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                            measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - totalPrice + int.Parse(dtHeader.Rows[0]["discountCash"].ToString())/*sumPrice*/).ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 30;
                        }
                        else if (dtHeader.Rows[0]["discountPercent"].ToString() != "0")
                        {
                            g.Graphics.DrawString(string.Format("ส่วนลด ({0}%)", dtHeader.Rows[0]["discountPercent"].ToString()), stringFont, brush, new PointF(pX + 600, pY));

                            double perBath = (int)Math.Round(sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString())/100);
                            double perTotal = (int)Math.Round(sumPrice - (sumPrice * double.Parse(dtHeader.Rows[0]["discountPercent"].ToString()) / 100));

                            stringFont = new Font("DilleniaUPC", 15);
                            measureString = perBath.ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 25;

                            stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
                            g.Graphics.DrawString("รวมสุทธิ ", stringFont, brush, new PointF(pX + 600, pY));

                            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                            measureString = perTotal.ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 30;

                            stringFont = new Font("DilleniaUPC", 15);
                            g.Graphics.DrawString("เงินสด  " + int.Parse(dtHeader.Rows[0]["Cash"].ToString()).ToString("#,##0"), stringFont, brush, new PointF(pX, pY));
                            measureString = "เงินทอน  " + (int.Parse(dtHeader.Rows[0]["Cash"].ToString()) - perTotal/*sumPrice*/).ToString("#,##0");
                            stringSize = g.Graphics.MeasureString(measureString, stringFont);
                            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width - gab, pY));
                            pY += 30;
                        }

                        //*************************
                       

                        g.Graphics.DrawLine(new Pen(Color.Black, 0.25f), pX, pY, pX + width, pY);
                        pY += 5;

                        stringFont = new Font("DilleniaUPC", 15);
                        g.Graphics.DrawString("ชื่อลูกค้า " + dtHeader.Rows[0]["Firstname"].ToString() + " " + dtHeader.Rows[0]["Lastname"].ToString() +
                            ((dtHeader.Rows[0]["Mobile"].ToString() != "") ?
                            " (" + dtHeader.Rows[0]["Mobile"].ToString().Substring(0, 3) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(3, 4) + "-" + dtHeader.Rows[0]["Mobile"].ToString().Substring(7) + ")"
                            : "")
                            , stringFont, brush, new PointF(pX, pY));

                        /*stringFont = new Font("DilleniaUPC", 11);
                        measureString = "แต้มสะสม  " + (34534).ToString("#,##0");
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF(width - stringSize.Width + gab, pY - 2));*/
                        pY += 20;

                        stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                        measureString = Param.FooterText;
                        stringSize = g.Graphics.MeasureString(measureString, stringFont);
                        g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY));
                        Param.Qty = 0;
                    }
                }
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
                                AND OrderNo = '{1}'
                                GROUP BY Product
                        ) r
                            ON b.Product = r.Product
                        LEFT JOIN Category c
                            ON p.category = c.category
                    WHERE b.OrderNo = '{1}'
                    GROUP BY b.Product
                    ORDER BY c.name, p.Name
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
            measureString = DateTime.Now.ToString("dd/MM/yyyy  HH:MM:ss");
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

        public static bool GetConfigFromSqlCe(string filename, string password)
        {
            try
            {
                string strDataSource = @"Data Source=" + Path.Combine(Directory.GetCurrentDirectory(), filename) + ";Encrypt Database=True;Password=" + password + ";" +
                    @"File Mode=shared read;Persist Security Info = False;";
                SqlCeConnection conn = new SqlCeConnection();
                conn.ConnectionString = strDataSource;

                SqlCeCommand selectCmd = conn.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Config";

                SqlCeDataAdapter adp = new SqlCeDataAdapter(selectCmd);

                DataTable dt = new DataTable();
                adp.Fill(dt);
                adp.Dispose();
                conn.Close();

                Param.SqlCeConfig = new Hashtable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Param.SqlCeConfig.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("รหัสผ่านไม่ถูกต้อง กรุณาลองใหม่อีกครั้ง\n" + ex.Message, "มีข้อผิดพลาดเกิดขึ้น", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public static string DateTimeToUnixTimestamp(DateTime dateTime)
        {
            string time = ((TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds).ToString();
            string[] sp = time.Split('.');
            return sp[0];
        }

        public static string MD5String(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static void LoadScreenPermissionDetail(string screen)
        {
            StringBuilder sb = new StringBuilder("|");
            var dt = Util.DBQuery(string.Format(@"
                SELECT permission 
                FROM EmployeeScreenMapping 
                WHERE system = 'POS' 
                    AND screen = '{0}' 
                    AND employeeType = '{1}'
            ", screen, Param.EmployeeType));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append(dt.Rows[i]["permission"].ToString() + "|");
            }

            Param.ScreenPermissionDetail = sb.ToString();
        }

        public static bool CanAccessScreenDetail(string permission)
        {
            return Param.ScreenPermissionDetail.IndexOf("|" + permission + "|") != -1;
        }

        public static void PrintReturn(PrintPageEventArgs g, string claimNo)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                var pX = 60;
                var pY = 60;

                Image image = Image.FromFile("logo.png");
                Rectangle destRect = new Rectangle(pX, pY, 134, 32);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("DilleniaUPC", 14);
                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont, brush, new PointF(pX - 2, pY + 30));
                stringFont = new Font("DilleniaUPC", 12);
                /*g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont, brush, new PointF(pX - 2, pY + 50));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont, brush, new PointF(pX - 2, pY + 65));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont, brush, new PointF(pX - 2, pY + 80));*/
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont, brush, new PointF(pX - 2, pY + 50));

                stringFont = new Font("DilleniaUPC", 8);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].shopCode.ToString() + " : " + Param.ShopName, stringFont, brush, new PointF(pX, pY + 65));


                stringFont = new Font("DilleniaUPC", 26, FontStyle.Bold);
                g.Graphics.DrawString("ใบส่งคืนสินค้า", stringFont, brush, new PointF(pX + 550, pY));

                stringFont = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("(สำหรับลูกค้า)", stringFont, brush, new PointF(pX + 600, pY + 30));

                Code128BarcodeDraw bdw = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img, new Point(pX + 568, pY + 50));

                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("เลขที่:", stringFont, brush, new PointF(pX + 560, pY + 75));
                g.Graphics.DrawString("วันที่:", stringFont, brush, new PointF(pX + 560, pY + 95));

                stringFont = new Font("Calibri", 10, FontStyle.Bold);
                g.Graphics.DrawString(claimNo, stringFont, brush, new PointF(pX + 600, pY + 78));

                DateTime now = DateTime.Now;
                g.Graphics.DrawString(now.ToString("dd/MM/yyyy (HH:mm)"), stringFont, brush, new PointF(pX + 600, pY + 98));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 120, 710, 90);

                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อลูกค้า", stringFont, brush, new PointF(pX + 10, pY + 130));
                g.Graphics.DrawString("ที่อยู่", stringFont, brush, new PointF(pX + 10, pY + 150));
                g.Graphics.DrawString("เบอร์โทร", stringFont, brush, new PointF(pX + 10, pY + 170));

                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].firstname.ToString() + ' ' + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont, brush, new PointF(pX + 60, pY + 130));
                bool isBkk = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString() + ' ' + jsonAddressInfo.result[0][0].address2.ToString() + ' ' + (isBkk ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + ' ' + jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont, brush, new PointF(pX + 60, pY + 150));

                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont, brush, new PointF(pX + 60, pY + 170));
                }
                else
                {
                    g.Graphics.DrawString("-", stringFont, brush, new PointF(pX + 60, pY + 170));
                }

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 220, 710, 200);

                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("วันที่เคลม", stringFont, brush, new PointF(pX + 60, pY + 230));
                g.Graphics.DrawString("สินค้าที่เคลม", stringFont, brush, new PointF(pX + 60, pY + 250));
                g.Graphics.DrawString("Serial Number ที่เคลม", stringFont, brush, new PointF(pX + 60, pY + 270));
                g.Graphics.DrawString("ประกัน", stringFont, brush, new PointF(pX + 60, pY + 290));
                g.Graphics.DrawString("วันที่ส่งคืน", stringFont, brush, new PointF(pX + 60, pY + 310));
                g.Graphics.DrawString("สินค้าที่ส่งคืน", stringFont, brush, new PointF(pX + 60, pY + 330));
                g.Graphics.DrawString("Serial Number ที่ส่งคืน", stringFont, brush, new PointF(pX + 60, pY + 350));

                stringFont = new Font("DilleniaUPC", 14);
                DateTime claimDate = DateTime.Now;
                claimDate = jsonAddressInfo.result[0][0].claimDate;
                g.Graphics.DrawString(claimDate.ToString("dd/MM/yyyy"), stringFont, brush, new PointF(pX + 120, pY + 230));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].productName.ToString(), stringFont, brush, new PointF(pX + 140, pY + 250));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcode.ToString(), stringFont, brush, new PointF(pX + 200, pY + 270));
                DateTime expireDate = DateTime.Now;
                expireDate = jsonAddressInfo.result[0][0].expireDate;
                g.Graphics.DrawString("สิ้นสุด ณ " + expireDate.ToString("dd/MM/yyyy"), stringFont, brush, new PointF(pX + 120, pY + 290));
                DateTime returnDate = DateTime.Now;
                g.Graphics.DrawString(returnDate.ToString("dd/MM/yyyy"), stringFont, brush, new PointF(pX + 120, pY + 310));
                g.Graphics.DrawString(jsonWarrantyInfo.result.productName.ToString(), stringFont, brush, new PointF(pX + 140, pY + 330));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcodeClaim.ToString(), stringFont, brush, new PointF(pX + 200, pY + 350));

                Point point1 = new Point(pX + 100, pY + 460);
                Point point2 = new Point(pX + 200, pY + 460);
                Point point3 = new Point(pX + 300, pY + 460);
                Point point4 = new Point(pX + 400, pY + 460);
                Point point5 = new Point(pX + 500, pY + 460);
                Point point6 = new Point(pX + 600, pY + 460);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point1, point2);
                //g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point3, point4);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point5, point6);
                stringFont = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("ผู้รับคืน", stringFont, brush, new PointF(pX + 130, pY + 465));
                //g.Graphics.DrawString("ผู้รับเคลม", stringFont, brush, new PointF(pX + 330, pY + 465));
                g.Graphics.DrawString("ผู้ส่งคืน", stringFont, brush, new PointF(pX + 530, pY + 465));

                /////////////////////////////////////////////////////////////////////////////////////////
                float[] dashValues = { 5, 2, 15, 4 };
                Point pointX = new Point(pX, pY + 500);
                Point pointY = new Point(pX + 710, pY + 500);
                Pen linePen = new Pen(Color.Gray);
                linePen.DashPattern = dashValues;
                g.Graphics.DrawLine(linePen, pointX, pointY);

                Image image2 = Image.FromFile("logo.png");
                Rectangle destRect2 = new Rectangle(pX, pY + 540, 134, 32);
                g.Graphics.DrawImage(image2, destRect2, 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);

                SolidBrush brush2 = new SolidBrush(Color.Black);
                Font stringFont2 = new Font("DilleniaUPC", 14);
                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont2, brush2, new PointF(pX - 2, pY + 30 + 540));
                stringFont2 = new Font("DilleniaUPC", 12);
                /*g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont2, brush2, new PointF(pX - 2, pY + 50 + 540));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont2, brush2, new PointF(pX - 2, pY + 65 + 540));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont2, brush2, new PointF(pX - 2, pY + 80 + 540));*/
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont2, brush2, new PointF(pX - 2, pY + 50 + 540));

                stringFont = new Font("DilleniaUPC", 8);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].shopCode.ToString() + " : " + Param.ShopName, stringFont, brush, new PointF(pX, pY + 65 + 540));

                stringFont2 = new Font("DilleniaUPC", 26, FontStyle.Bold);
                g.Graphics.DrawString("ใบส่งคืนสินค้า", stringFont2, brush2, new PointF(pX + 550, pY + 540));

                stringFont2 = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("(สำหรับเจ้าหน้าที่)", stringFont2, brush2, new PointF(pX + 595, pY + 32 + 540));

                Code128BarcodeDraw bdw2 = BarcodeDrawFactory.Code128WithChecksum;
                Image img2 = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img2, new Point(pX + 568, pY + 50 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("เลขที่:", stringFont2, brush2, new PointF(pX + 560, pY + 75 + 540));
                g.Graphics.DrawString("วันที่:", stringFont2, brush2, new PointF(pX + 560, pY + 95 + 540));

                stringFont2 = new Font("Calibri", 10, FontStyle.Bold);
                g.Graphics.DrawString(claimNo, stringFont2, brush2, new PointF(pX + 600, pY + 78 + 540));

                DateTime now2 = DateTime.Now;
                g.Graphics.DrawString(now2.ToString("dd/MM/yyyy (HH:mm)"), stringFont2, brush2, new PointF(pX + 600, pY + 98 + 540));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 120 + 540, 710, 90);

                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อลูกค้า", stringFont2, brush2, new PointF(pX + 10, pY + 130 + 540));
                g.Graphics.DrawString("ที่อยู่", stringFont2, brush2, new PointF(pX + 10, pY + 150 + 540));
                g.Graphics.DrawString("เบอร์โทร", stringFont2, brush2, new PointF(pX + 10, pY + 170 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].firstname.ToString() + ' ' + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont2, brush2, new PointF(pX + 60, pY + 130 + 540));
                bool isBkk2 = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString() + ' ' + jsonAddressInfo.result[0][0].address2.ToString() + ' ' + (isBkk ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + ' ' + jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont2, brush2, new PointF(pX + 60, pY + 150 + 540));
                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont2, brush2, new PointF(pX + 60, pY + 170 + 540));
                }
                else
                {
                    g.Graphics.DrawString("-", stringFont2, brush2, new PointF(pX + 60, pY + 170 + 540));
                }

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 220 + 540, 710, 200);

                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("วันที่เคลม", stringFont2, brush2, new PointF(pX + 60, pY + 230 + 540));
                g.Graphics.DrawString("สินค้าที่เคลม", stringFont2, brush2, new PointF(pX + 60, pY + 250 + 540));
                g.Graphics.DrawString("Serial Number ที่เคลม", stringFont2, brush2, new PointF(pX + 60, pY + 270 + 540));
                g.Graphics.DrawString("ประกัน", stringFont2, brush2, new PointF(pX + 60, pY + 290 + 540));
                g.Graphics.DrawString("วันที่ส่งคืน", stringFont2, brush2, new PointF(pX + 60, pY + 310 + 540));
                g.Graphics.DrawString("สินค้าที่ส่งคืน", stringFont2, brush2, new PointF(pX + 60, pY + 330 + 540));
                g.Graphics.DrawString("Serial Number ที่ส่งคืน", stringFont2, brush2, new PointF(pX + 60, pY + 350 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                DateTime claimDate2 = DateTime.Now;
                claimDate2 = jsonAddressInfo.result[0][0].claimDate;
                g.Graphics.DrawString(claimDate.ToString("dd/MM/yyyy"), stringFont2, brush2, new PointF(pX + 120, pY + 230 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].productName.ToString(), stringFont2, brush2, new PointF(pX + 140, pY + 250 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcode.ToString(), stringFont2, brush2, new PointF(pX + 200, pY + 270 + 540));
                DateTime expireDate2 = DateTime.Now;
                expireDate = jsonAddressInfo.result[0][0].expireDate;
                g.Graphics.DrawString("สิ้นสุด ณ " + expireDate.ToString("dd/MM/yyyy"), stringFont2, brush2, new PointF(pX + 120, pY + 290 + 540));
                DateTime returnDate2 = DateTime.Now;
                g.Graphics.DrawString(returnDate2.ToString("dd/MM/yyyy"), stringFont2, brush2, new PointF(pX + 120, pY + 310 + 540));
                g.Graphics.DrawString(jsonWarrantyInfo.result.productName.ToString(), stringFont2, brush2, new PointF(pX + 140, pY + 330 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcodeClaim.ToString(), stringFont2, brush2, new PointF(pX + 200, pY + 350 + 540));

                Point point12 = new Point(pX + 100, pY + 460 + 540);
                Point point22 = new Point(pX + 200, pY + 460 + 540);
                Point point32 = new Point(pX + 300, pY + 460 + 540);
                Point point42 = new Point(pX + 400, pY + 460 + 540);
                Point point52 = new Point(pX + 500, pY + 460 + 540);
                Point point62 = new Point(pX + 600, pY + 460 + 540);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point12, point22);
                //g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point32, point42);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point52, point62);
                stringFont2 = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("ผู้รับคืน", stringFont2, brush2, new PointF(pX + 130, pY + 465 + 540));
                //g.Graphics.DrawString("ผู้รับเคลม", stringFont2, brush2, new PointF(pX + 330, pY + 465 + 540));
                g.Graphics.DrawString("ผู้ส่งคืน", stringFont2, brush2, new PointF(pX + 530, pY + 465 + 540));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        public static bool GetAddress(string claimNo, bool returnClaim)
        {
            bool success = false;
            jsonAddressInfo = JsonConvert.DeserializeObject(Util.ApiProcess("/claim/info", "shop=" + "" + "&id=" + claimNo + "&barcode=" + "" + "&claimdate_from=" + "" + "&claimdate_to=" + "" + "&status=" + "" + "&firstname=" + "" + "&lineid=" + "" + "&tel=" + ""));
            if (jsonAddressInfo.success.Value)
            {
                if (returnClaim)
                {
                    jsonWarrantyInfo = JsonConvert.DeserializeObject(Util.ApiProcess("/warranty/info", "barcode=" + jsonAddressInfo.result[0][0].barcodeClaim.ToString()));
                    if (jsonWarrantyInfo.success.Value)
                    {
                        success = true;
                    }
                }
                else
                {
                    success = true;
                }

            }

            return success;
        }

        public static void PrintClaim(PrintPageEventArgs g, string claimNo)
        {
            try
            {

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                var pX = 60;
                var pY = 60;

                Image image = Image.FromFile("logo.png");
                Rectangle destRect = new Rectangle(pX, pY, 134, 32);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("DilleniaUPC", 14);
                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont, brush, new PointF(pX - 2, pY + 30));
                stringFont = new Font("DilleniaUPC", 12);
                /*g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont, brush, new PointF(pX - 2, pY + 50));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont, brush, new PointF(pX - 2, pY + 65));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont, brush, new PointF(pX - 2, pY + 80));*/
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont, brush, new PointF(pX - 2, pY + 50));

                stringFont = new Font("DilleniaUPC", 8);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].shopCode.ToString() + " : " + Param.ShopName, stringFont, brush, new PointF(pX, pY + 65));

                stringFont = new Font("DilleniaUPC", 26, FontStyle.Bold);
                g.Graphics.DrawString("ใบรับเคลมสินค้า", stringFont, brush, new PointF(pX + 550, pY));

                stringFont = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("(สำหรับลูกค้า)", stringFont, brush, new PointF(pX + 600, pY + 30));

                Code128BarcodeDraw bdw = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img, new Point(pX + 568, pY + 50));

                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("เลขที่:", stringFont, brush, new PointF(pX + 560, pY + 78));
                g.Graphics.DrawString("วันที่:", stringFont, brush, new PointF(pX + 560, pY + 95));

                stringFont = new Font("Calibri", 10, FontStyle.Bold);
                g.Graphics.DrawString(claimNo, stringFont, brush, new PointF(pX + 600, pY + 80));

                DateTime now = DateTime.Now;
                g.Graphics.DrawString(now.ToString("dd/MM/yyyy (HH:mm)"), stringFont, brush, new PointF(pX + 600, pY + 98));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 120, 710, 90);

                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อลูกค้า", stringFont, brush, new PointF(pX + 10, pY + 130));
                g.Graphics.DrawString("ที่อยู่", stringFont, brush, new PointF(pX + 10, pY + 150));
                g.Graphics.DrawString("เบอร์โทร", stringFont, brush, new PointF(pX + 10, pY + 170));

                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].firstname.ToString() + ' ' + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont, brush, new PointF(pX + 60, pY + 130));
                bool isBkk = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString() + ' ' + jsonAddressInfo.result[0][0].address2.ToString() + ' ' + (isBkk ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + ' ' + jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont, brush, new PointF(pX + 60, pY + 150));

                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont, brush, new PointF(pX + 60, pY + 170));
                }
                else
                {
                    g.Graphics.DrawString("-", stringFont, brush, new PointF(pX + 60, pY + 170));
                }

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 220, 710, 200);

                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อสินค้า", stringFont, brush, new PointF(pX + 60, pY + 230));
                g.Graphics.DrawString("ประเภท", stringFont, brush, new PointF(pX + 60, pY + 250));
                g.Graphics.DrawString("ยี่ห้อ", stringFont, brush, new PointF(pX + 60, pY + 270));
                g.Graphics.DrawString("Serial Number", stringFont, brush, new PointF(pX + 60, pY + 290));
                g.Graphics.DrawString("ประกัน", stringFont, brush, new PointF(pX + 60, pY + 310));
                g.Graphics.DrawString("วันที่เข้าระบบ", stringFont, brush, new PointF(pX + 60, pY + 330));
                g.Graphics.DrawString("อาการเสีย", stringFont, brush, new PointF(pX + 60, pY + 350));

                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].productName.ToString(), stringFont, brush, new PointF(pX + 120, pY + 230));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].categoryName.ToString(), stringFont, brush, new PointF(pX + 120, pY + 250));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].brandName.ToString(), stringFont, brush, new PointF(pX + 120, pY + 270));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcode.ToString(), stringFont, brush, new PointF(pX + 160, pY + 290));
                DateTime expireDate = DateTime.Now;
                expireDate = jsonAddressInfo.result[0][0].expireDate;
                DateTime claimDate = DateTime.Now;
                claimDate = jsonAddressInfo.result[0][0].claimDate;
                g.Graphics.DrawString("สิ้นสุด ณ " + expireDate.ToString("dd/MM/yyyy"), stringFont, brush, new PointF(pX + 120, pY + 310));
                g.Graphics.DrawString(claimDate.ToString("dd/MM/yyyy"), stringFont, brush, new PointF(pX + 160, pY + 330));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].description.ToString(), stringFont, brush, new PointF(pX + 120, pY + 350));

                Point point1 = new Point(pX + 100, pY + 460);
                Point point2 = new Point(pX + 200, pY + 460);
                Point point3 = new Point(pX + 300, pY + 460);
                Point point4 = new Point(pX + 400, pY + 460);
                Point point5 = new Point(pX + 500, pY + 460);
                Point point6 = new Point(pX + 600, pY + 460);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point1, point2);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point3, point4);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point5, point6);
                stringFont = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("ผู้ส่งเคลม", stringFont, brush, new PointF(pX + 130, pY + 465));
                g.Graphics.DrawString("ผู้รับเคลม", stringFont, brush, new PointF(pX + 330, pY + 465));
                g.Graphics.DrawString("ผู้รับคืน", stringFont, brush, new PointF(pX + 530, pY + 465));

                /////////////////////////////////////////////////////////////////////////////////////////
                float[] dashValues = { 5, 2, 15, 4 };
                Point pointX = new Point(pX, pY + 500);
                Point pointY = new Point(pX + 710, pY + 500);
                Pen linePen = new Pen(Color.Gray);
                linePen.DashPattern = dashValues;
                g.Graphics.DrawLine(linePen, pointX, pointY);

                Image image2 = Image.FromFile("logo.png");
                Rectangle destRect2 = new Rectangle(pX, pY + 540, 134, 32);
                g.Graphics.DrawImage(image2, destRect2, 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel);

                SolidBrush brush2 = new SolidBrush(Color.Black);
                Font stringFont2 = new Font("DilleniaUPC", 14);
                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont2, brush2, new PointF(pX - 2, pY + 30 + 540));
                stringFont2 = new Font("DilleniaUPC", 12);
                /*g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont2, brush2, new PointF(pX - 2, pY + 50 + 540));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont2, brush2, new PointF(pX - 2, pY + 65 + 540));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont2, brush2, new PointF(pX - 2, pY + 80 + 540));*/
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont2, brush2, new PointF(pX - 2, pY + 50 + 540));

                stringFont = new Font("DilleniaUPC", 8);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].shopCode.ToString() + " : " + Param.ShopName, stringFont, brush, new PointF(pX, pY + 65 + 540));

                stringFont2 = new Font("DilleniaUPC", 26, FontStyle.Bold);
                g.Graphics.DrawString("ใบรับเคลมสินค้า", stringFont2, brush2, new PointF(pX + 550, pY + 540));

                stringFont2 = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("(สำหรับเจ้าหน้าที่)", stringFont2, brush2, new PointF(pX + 595, pY + 32 + 540));

                Code128BarcodeDraw bdw2 = BarcodeDrawFactory.Code128WithChecksum;
                Image img2 = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img2, new Point(pX + 568, pY + 50 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("เลขที่:", stringFont2, brush2, new PointF(pX + 560, pY + 75 + 540));
                g.Graphics.DrawString("วันที่:", stringFont2, brush2, new PointF(pX + 560, pY + 95 + 540));

                stringFont2 = new Font("Calibri", 10, FontStyle.Bold);
                g.Graphics.DrawString(claimNo, stringFont2, brush2, new PointF(pX + 600, pY + 78 + 540));

                DateTime now2 = DateTime.Now;
                g.Graphics.DrawString(now2.ToString("dd/MM/yyyy (HH:mm)"), stringFont2, brush2, new PointF(pX + 600, pY + 98 + 540));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 120 + 540, 710, 90);

                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อลูกค้า", stringFont2, brush2, new PointF(pX + 10, pY + 130 + 540));
                g.Graphics.DrawString("ที่อยู่", stringFont2, brush2, new PointF(pX + 10, pY + 150 + 540));
                g.Graphics.DrawString("เบอร์โทร", stringFont2, brush2, new PointF(pX + 10, pY + 170 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].firstname.ToString() + ' ' + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont2, brush2, new PointF(pX + 60, pY + 130 + 540));
                bool isBkk2 = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString() + ' ' + jsonAddressInfo.result[0][0].address2.ToString() + ' ' + (isBkk ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + ' ' + jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont2, brush2, new PointF(pX + 60, pY + 150 + 540));

                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont2, brush2, new PointF(pX + 60, pY + 170 + 540));
                }
                else
                {
                    g.Graphics.DrawString("-", stringFont2, brush2, new PointF(pX + 60, pY + 170 + 540));
                }

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX, pY + 220 + 540, 710, 200);

                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ชื่อสินค้า", stringFont2, brush2, new PointF(pX + 60, pY + 230 + 540));
                g.Graphics.DrawString("ประเภท", stringFont2, brush2, new PointF(pX + 60, pY + 250 + 540));
                g.Graphics.DrawString("ยี่ห้อ", stringFont2, brush2, new PointF(pX + 60, pY + 270 + 540));
                g.Graphics.DrawString("Serial Number", stringFont2, brush2, new PointF(pX + 60, pY + 290 + 540));
                g.Graphics.DrawString("ประกัน", stringFont2, brush2, new PointF(pX + 60, pY + 310 + 540));
                g.Graphics.DrawString("วันที่เข้าระบบ", stringFont2, brush2, new PointF(pX + 60, pY + 330 + 540));
                g.Graphics.DrawString("อาการเสีย", stringFont2, brush2, new PointF(pX + 60, pY + 350 + 540));

                stringFont2 = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].productName.ToString(), stringFont2, brush2, new PointF(pX + 120, pY + 230 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].categoryName.ToString(), stringFont2, brush2, new PointF(pX + 120, pY + 250 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].brandName.ToString(), stringFont2, brush2, new PointF(pX + 120, pY + 270 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].barcode.ToString(), stringFont2, brush2, new PointF(pX + 160, pY + 290 + 540));
                DateTime expireDate2 = DateTime.Now;
                expireDate2 = jsonAddressInfo.result[0][0].expireDate;
                DateTime claimDate2 = DateTime.Now;
                claimDate2 = jsonAddressInfo.result[0][0].claimDate;
                g.Graphics.DrawString("สิ้นสุด ณ " + expireDate.ToString("dd/MM/yyyy"), stringFont2, brush2, new PointF(pX + 120, pY + 310 + 540));
                g.Graphics.DrawString(claimDate2.ToString("dd/MM/yyyy"), stringFont2, brush2, new PointF(pX + 160, pY + 330 + 540));
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].description.ToString(), stringFont2, brush2, new PointF(pX + 120, pY + 350 + 540));

                Point point12 = new Point(pX + 100, pY + 460 + 540);
                Point point22 = new Point(pX + 200, pY + 460 + 540);
                Point point32 = new Point(pX + 300, pY + 460 + 540);
                Point point42 = new Point(pX + 400, pY + 460 + 540);
                Point point52 = new Point(pX + 500, pY + 460 + 540);
                Point point62 = new Point(pX + 600, pY + 460 + 540);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point12, point22);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point32, point42);
                g.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray)), point52, point62);
                stringFont2 = new Font("DilleniaUPC", 12);
                g.Graphics.DrawString("ผู้ส่งเคลม", stringFont2, brush2, new PointF(pX + 130, pY + 465 + 540));
                g.Graphics.DrawString("ผู้รับเคลม", stringFont2, brush2, new PointF(pX + 330, pY + 465 + 540));
                g.Graphics.DrawString("ผู้รับคืน", stringFont2, brush2, new PointF(pX + 530, pY + 465 + 540));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }
        }

        public static void PrintReportStatusClaim(string data, string dat)
        {
            Param.Page = 0;
            Param.d = 0;
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM BarcodeClaim WHERE  status = '{0}' AND claimDate LIKE '%{1}%'", data, dat));

            PaperSize paperSize = new PaperSize();
            paperSize.RawKind = (int)PaperKind.A4;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;

            int count = int.Parse(dt.Rows[0]["cnt"].ToString());

            for (int i = 1; i <= Math.Ceiling((float)count / Param.Num); i++)
            {
                //if (i > 1)
                //{
                //    Param.Page = Param.Page + 1;
                //}

                pd.PrintPage += (_, g) =>
                {
                    PrintReportStatusClaim(g, data, dat);
                };

                pd.Print();
                Param.Page += Param.Num;
            }
        }

        private static void PrintReportStatusClaim(PrintPageEventArgs g, string data, string dat)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


            DataTable dtHeader = Util.DBQuery(string.Format(@"
                SELECT DISTINCT s.shop, s.shopName  FROM BarcodeClaim b
                LEFT JOIN Product p
                ON p.product = b.product
                LEFT JOIN Shop s
                ON b.shop = s.shop
                LEFT JOIN Employee e
                ON b.claimBy = e.employeeId
                WHERE b.status = '{0}' AND claimDate LIKE '%{1}%'
                ", data, dat, Param.UserId));

            var width = 800;
            var gab = 20;

            SolidBrush brush = new SolidBrush(Color.Black);
            Font stringFont = new Font("DilleniaUPC", 6);

            var pX = 0;
            var pY = 5;

            stringFont = new Font("DilleniaUPC", 22, FontStyle.Bold);
            string measureString = "รายงานการเคลมสินค้า ตามสถานะการเคลม";
            SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
            pY += 35;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("วันที่ ", stringFont, brush, new PointF(pX, pY));
            stringFont = new Font("DilleniaUPC", 18);
            measureString = DateTime.Now.ToString("dd/MM/yyyy");
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 40, pY));

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("สถานะการเคลม : ", stringFont, brush, new PointF(pX + 250, pY));
            stringFont = new Font("DilleniaUPC", 18);

            var stat = "";
            if (data == "1")
            {
                stat = "คืนของผิดเงื่อนไข";
            }
            else if (data == "2")
            {
                stat = "เปลี่ยนสินค้า";
            }
            else if (data == "3")
            {
                stat = "ลดหนี้/คืนเงิน";
            }
            measureString = stat;
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 380, pY));
            pY += 25;


            for (int w = 0; w < dtHeader.Rows.Count; w++)
            {
                stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                g.Graphics.DrawString("สาขา : ", stringFont, brush, new PointF(pX, pY));
                stringFont = new Font("DilleniaUPC", 18);
                measureString = dtHeader.Rows[w]["shopName"].ToString();
                stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 50, pY));

                //stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                //g.Graphics.DrawString("สาขา : ", stringFont, brush, new PointF(pX + 250, pY));
                //stringFont = new Font("DilleniaUPC", 18);
                //measureString = dtHeader.Rows[0]["shopName"].ToString();
                //stringSize = g.Graphics.MeasureString(measureString, stringFont);
                //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 320, pY));

                pY += 40;

                stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
                DataTable dt = Util.DBQuery(string.Format(@"
                      SELECT  s.shopName, b.orderNo, p.name, b.barcode, receivedDate, receivedBy, b.status, claimDate, e.firstname claimBy, priceClaim, barcodeClaim, posClaim, comment, CASE WHEN  b.status= 1 THEN 'คืนสินค้า'  WHEN  b.status= 2 THEN 'เปลี่ยนสินค้า' WHEN  b.status= 3 THEN 'ลดหนี้/คืนเงิน' WHEN  b.status= '' THEN 'ยังไม่รับเข้า' ELSE 'ค้างส่ง' END st, CASE WHEN  b.status = 1 THEN comment WHEN  b.status= 2 THEN posClaim||'/'||barcodeClaim||' '||comment   WHEN  b.status= 3 THEN priceClaim||'   '||comment  WHEN  b.status= '' THEN '' ELSE '' END comm FROM BarcodeClaim b
                        LEFT JOIN Product p
                        ON p.product = b.product
                        LEFT JOIN Shop s
                        ON b.shop = s.shop
                        LEFT JOIN Employee e
                        ON b.claimBy = e.employeeId
                        WHERE b.status = '{0}' 
                        AND b.claimDate LIKE '%{1}%'
                        AND s.shop = '{3}'
                        ORDER BY p.name, b.barcode
                        ", data, dat, Param.ShopId, dtHeader.Rows[w]["shop"].ToString()));
                g.Graphics.DrawString("ที่", stringFont, brush, new PointF(pX, pY));
                g.Graphics.DrawString("ชื่อสินค้า", stringFont, brush, new PointF(pX + 150, pY));
                g.Graphics.DrawString("บาร์โค้ด", stringFont, brush, new PointF(pX + 395, pY));
                //g.Graphics.DrawString("สถานะ", stringFont, brush, new PointF(pX + 500, pY));
                g.Graphics.DrawString("หมายเหตุ", stringFont, brush, new PointF(pX + 550, pY));
                pY += 23;
                stringFont = new Font("DilleniaUPC", 15);

                int sumPrice = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int row = i + 1;
                    g.Graphics.DrawString(row.ToString("#,##0") + ".", stringFont, brush, new PointF(pX, pY));


                    g.Graphics.DrawString(dt.Rows[i]["name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));
                    g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 368, pY + 5, 150, 20);
                    g.Graphics.DrawString(dt.Rows[i]["barcode"].ToString(), stringFont, brush, new PointF(pX + 370, pY));
                    //g.Graphics.DrawString(dt.Rows[i]["st"].ToString(), stringFont, brush, new PointF(pX + 485, pY));
                    g.Graphics.DrawString(dt.Rows[i]["comm"].ToString(), stringFont, brush, new PointF(pX + 500, pY));
                    sumPrice += int.Parse(dt.Rows[i]["priceClaim"].ToString());
                    //g.Graphics.DrawString(dt.Rows[i]["claimDate"].ToString(), stringFont, brush, new PointF(pX + 750, pY));
                    //g.Graphics.DrawString(dt.Rows[i]["claimBy"].ToString(), stringFont, brush, new PointF(pX + 800, pY));
                    pY += 23;
                    //g.Graphics.DrawString("วันที่รับสินค้าเคลม", stringFont, brush, new PointF(pX + 20, pY));
                    //g.Graphics.DrawString(dt.Rows[i]["receivedDate"].ToString(), stringFont, brush, new PointF(pX + 135, pY));
                    //g.Graphics.DrawString("วันที่ทำการเคลม", stringFont, brush, new PointF(pX + 280, pY));
                    //g.Graphics.DrawString(dt.Rows[i]["claimDate"].ToString(), stringFont, brush, new PointF(pX + 380, pY));
                    //g.Graphics.DrawString("ผู้ทำการเคลมสินค้า", stringFont, brush, new PointF(pX + 520, pY));
                    //g.Graphics.DrawString(dt.Rows[i]["claimBy"].ToString(), stringFont, brush, new PointF(pX + 640, pY));
                    //pY += 23;

                }

                pY += 5;
                stringFont = new Font("DilleniaUPC", 16, FontStyle.Bold);
                g.Graphics.DrawString("รวม ลดหนี้/คืนเงิน = " + sumPrice.ToString("#,##0") + " บาท", stringFont, brush, new PointF(pX, pY));


                pY += 20;
            }
            pY += 30;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("ผู้ทำการเคลมสินค้า ", stringFont, brush, new PointF(pX, pY));
            //stringFont = new Font("DilleniaUPC", 18);
            //measureString = dtHeader.Rows[0]["claimBy"].ToString();
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 80, pY));

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("ลงชื่อ...................................................... ", stringFont, brush, new PointF(pX + 150, pY));
            pY += 25;


        }

        public static void PrintReportClaim(string data, string shop)
        {
            Param.Page = 0;
            Param.d = 0;
            DataTable dt = Util.DBQuery(string.Format(@"SELECT COUNT(*) cnt FROM BarcodeClaim WHERE orderNo  = '{0}'", data));

            PaperSize paperSize = new PaperSize();
            paperSize.RawKind = (int)PaperKind.A4;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = Param.DevicePrinter;

            int count = int.Parse(dt.Rows[0]["cnt"].ToString());

            for (int i = 1; i <= Math.Ceiling((float)count / Param.Num); i++)
            {
                //if (i > 1)
                //{
                //    Param.Page = Param.Page + 1;
                //}

                pd.PrintPage += (_, g) =>
                {
                    PrintReportClaim(g, data, shop);
                };

                pd.Print();
                Param.Page += Param.Num;
            }
        }

        private static void PrintReportClaim(PrintPageEventArgs g, string orderNo, string shopName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


            DataTable dtHeader = Util.DBQuery(string.Format(@"
                SELECT s.shopName, b.orderNo FROM BarcodeClaim b
                LEFT JOIN Product p
                ON p.product = b.product
                LEFT JOIN Shop s
                ON b.shop = s.shop
                LEFT JOIN Employee e
                ON b.claimBy = e.employeeId
                WHERE b.orderNo = '{0}'
                AND s.shopName = '{2}'
                ", orderNo, Param.UserId, shopName));

            var width = 800;
            var gab = 20;

            SolidBrush brush = new SolidBrush(Color.Black);
            Font stringFont = new Font("DilleniaUPC", 6);

            var pX = 0;
            var pY = 5;

            stringFont = new Font("DilleniaUPC", 22, FontStyle.Bold);
            string measureString = "รายงานการเคลมสินค้า";
            SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF((width - stringSize.Width + gab) / 2, pY + 5));
            pY += 35;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("วันที่ ", stringFont, brush, new PointF(pX, pY));
            stringFont = new Font("DilleniaUPC", 18);
            measureString = DateTime.Now.ToString("dd/MM/yyyy");
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 40, pY));
            pY += 25;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("เลขที่เคลม : ", stringFont, brush, new PointF(pX, pY));
            stringFont = new Font("DilleniaUPC", 18);
            measureString = dtHeader.Rows[0]["orderNo"].ToString();
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 80, pY));

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("จากสาขา : ", stringFont, brush, new PointF(pX + 250, pY));
            stringFont = new Font("DilleniaUPC", 18);
            measureString = dtHeader.Rows[0]["shopName"].ToString();
            stringSize = g.Graphics.MeasureString(measureString, stringFont);
            g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 320, pY));
            pY += 40;

            stringFont = new Font("DilleniaUPC", 15, FontStyle.Bold);
            DataTable dt = Util.DBQuery(string.Format(@"
                      SELECT  s.shopName, b.orderNo, p.name, b.barcode, receivedDate, receivedBy, b.status, claimDate, e.firstname claimBy, priceClaim, barcodeClaim, posClaim, comment, CASE WHEN  b.status= 1 THEN 'คืนสินค้า'  WHEN  b.status= 2 THEN 'เปลี่ยนสินค้า' WHEN  b.status= 3 THEN 'ลดหนี้/คืนเงิน' WHEN  b.status= '' THEN 'ยังไม่รับเข้า' ELSE 'ค้างส่ง' END st, CASE WHEN  b.status = 1 THEN comment WHEN  b.status= 2 THEN posClaim||'/'||barcodeClaim||' '||comment   WHEN  b.status= 3 THEN priceClaim||' '||comment  WHEN  b.status= '' THEN '' ELSE '' END comm FROM BarcodeClaim b
                        LEFT JOIN Product p
                        ON p.product = b.product
                        LEFT JOIN Shop s
                        ON b.shop = s.shop
                        LEFT JOIN Employee e
                        ON b.claimBy = e.employeeId
                        WHERE b.orderNo = '{0}' 
                        AND s.shopName = '{2}'
                        ORDER BY p.name, b.barcode
                        ", orderNo, Param.ShopId, shopName));
            g.Graphics.DrawString("ที่", stringFont, brush, new PointF(pX, pY));

            g.Graphics.DrawString("ชื่อสินค้า", stringFont, brush, new PointF(pX + 150, pY));
            g.Graphics.DrawString("บาร์โค้ด", stringFont, brush, new PointF(pX + 395, pY));
            g.Graphics.DrawString("สถานะ", stringFont, brush, new PointF(pX + 500, pY));
            g.Graphics.DrawString("หมายเหตุ", stringFont, brush, new PointF(pX + 610, pY));
            pY += 23;
            stringFont = new Font("DilleniaUPC", 15);
            int sumPrice = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int row = i + 1;
                g.Graphics.DrawString(row.ToString("#,##0")+".", stringFont, brush, new PointF(pX, pY));

                
                g.Graphics.DrawString(dt.Rows[i]["name"].ToString(), stringFont, brush, new PointF(pX + 20, pY));
                g.Graphics.FillRectangle(new SolidBrush(Color.White), pX + 368, pY + 5, 150, 20);
                g.Graphics.DrawString(dt.Rows[i]["barcode"].ToString(),stringFont, brush, new PointF(pX + 370, pY));
                g.Graphics.DrawString(dt.Rows[i]["st"].ToString(), stringFont, brush, new PointF(pX + 485, pY));
                g.Graphics.DrawString(dt.Rows[i]["comm"].ToString(), stringFont, brush, new PointF(pX + 570, pY));
                sumPrice += int.Parse(dt.Rows[i]["priceClaim"].ToString());

                //g.Graphics.DrawString(dt.Rows[i]["claimDate"].ToString(), stringFont, brush, new PointF(pX + 750, pY));
                //g.Graphics.DrawString(dt.Rows[i]["claimBy"].ToString(), stringFont, brush, new PointF(pX + 800, pY));
                pY += 23;
                //g.Graphics.DrawString("วันที่รับสินค้าเคลม", stringFont, brush, new PointF(pX + 20, pY));
                //g.Graphics.DrawString(dt.Rows[i]["receivedDate"].ToString(), stringFont, brush, new PointF(pX + 135, pY));
                //g.Graphics.DrawString("วันที่ทำการเคลม", stringFont, brush, new PointF(pX + 280, pY));
                //g.Graphics.DrawString(dt.Rows[i]["claimDate"].ToString(), stringFont, brush, new PointF(pX + 380, pY));
                //g.Graphics.DrawString("ผู้ทำการเคลมสินค้า", stringFont, brush, new PointF(pX + 520, pY));
                //g.Graphics.DrawString(dt.Rows[i]["claimBy"].ToString(), stringFont, brush, new PointF(pX + 640, pY));
                //pY += 23;

            }
            pY += 5;
            stringFont = new Font("DilleniaUPC", 16, FontStyle.Bold);
            g.Graphics.DrawString("รวม ลดหนี้/คืนเงิน = "+ sumPrice.ToString("#,##0") + " บาท", stringFont, brush, new PointF(pX, pY));

            pY += 30;

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("ผู้ทำการเคลมสินค้า ", stringFont, brush, new PointF(pX, pY));
            //stringFont = new Font("DilleniaUPC", 18);
            //measureString = dtHeader.Rows[0]["claimBy"].ToString();
            //stringSize = g.Graphics.MeasureString(measureString, stringFont);
            //g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 80, pY));

            stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
            g.Graphics.DrawString("ลงชื่อ...................................................... ", stringFont, brush, new PointF(pX + 150, pY));
            pY += 25;
        }

        public static void PrintAddress(PrintPageEventArgs g, string claimNo)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

                var pX = 60;
                var pY = 60;

                Image image = Image.FromFile("logo.png");
                Rectangle destRect = new Rectangle(pX, pY, 134, 32);
                g.Graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                SolidBrush brush = new SolidBrush(Color.Black);
                Font stringFont = new Font("DilleniaUPC", 14);
                stringFont = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont, brush, new PointF(pX - 2, pY + 30));
                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont, brush, new PointF(pX - 2, pY + 50));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont, brush, new PointF(pX - 2, pY + 70));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont, brush, new PointF(pX - 2, pY + 90));
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont, brush, new PointF(pX - 2, pY + 110));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX + 430, pY, 140, 50);

                Code128BarcodeDraw bdw = BarcodeDrawFactory.Code128WithChecksum;
                Image img = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img, new Point(pX + 435, pY + 5));

                stringFont = new Font("Calibri", 8);
                g.Graphics.DrawString(claimNo, stringFont, brush, new PointF(pX + 432, pY + 31));

                DateTime now = DateTime.Now;
                g.Graphics.DrawString(now.ToString("ddMMHHmm"), stringFont, brush, new PointF(pX + 517, pY + 31));

                /*stringFont = new Font("Calibri", 30, FontStyle.Bold);
                string measureString = x + "/" + boxCount;
                SizeF stringSize = g.Graphics.MeasureString(measureString, stringFont);
                g.Graphics.DrawString(measureString, stringFont, brush, new PointF(pX + 577 + ((124 - stringSize.Width) / 2), pY - 2));*/


                /*stringFont = new Font("DilleniaUPC", 18);
                if (isCash) g.Graphics.DrawString("เก็บเงินสดปลายทาง", stringFont, brush, new PointF(pX + 425, pY + 47));
                stringFont = new Font("DilleniaUPC", 18, FontStyle.Bold);
                stringSize = g.Graphics.MeasureString(shipping, stringFont);
                g.Graphics.DrawString(shipping, stringFont, brush, new PointF(pX + 705 - stringSize.Width, pY + 47));*/


                pX += 200;
                pY += 170;
                stringFont = new Font("DilleniaUPC", 18);
                g.Graphics.DrawString("กรุณาส่ง", stringFont, brush, new PointF(pX - 50, pY));

                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString("โทรศัพท์ " + jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont, brush, new PointF(pX + 47, pY));
                }
                else
                {
                    g.Graphics.DrawString("โทรศัพท์ -", stringFont, brush, new PointF(pX + 47, pY));
                }

                pY += 50;
                stringFont = new Font("DilleniaUPC", 24, FontStyle.Bold);
                g.Graphics.DrawString("คุณ " + jsonAddressInfo.result[0][0].firstname.ToString() + " " + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont, brush, new PointF(pX, pY));

                pY += 42;
                pX += 47;
                /*if (jsonAddressInfo.result[0][0].nickname.ToString().Trim() != "")
                {
                    stringFont = new Font("DilleniaUPC", 24, FontStyle.Bold);
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].nickname.ToString(), stringFont, brush, new PointF(pX, pY));
                    pY += 40;
                }*/

                stringFont = new Font("DilleniaUPC", 18);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString(), stringFont, brush, new PointF(pX, pY));
                pY += 35;
                if (jsonAddressInfo.result[0][0].address2.ToString().Trim() != "")
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].address2.ToString(), stringFont, brush, new PointF(pX, pY));
                    pY += 35;
                }

                bool isBkk = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString((isBkk ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + "",
                    stringFont, brush, new PointF(pX, pY));
                pY += 35;
                g.Graphics.DrawString("รหัสไปรษณีย์", stringFont, brush, new PointF(pX + 30, pY));
                stringFont = new Font("Calibri", 18, FontStyle.Bold);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont, brush, new PointF(pX + 140, pY));

                pX = 60;
                pY = 60;
                if (jsonAddressInfo.result[0][0].trackNo.ToString().Length > 1)
                {
                    Code128BarcodeDraw bdwTrack = BarcodeDrawFactory.Code128WithChecksum;
                    Image imgTrack = bdwTrack.Draw(jsonAddressInfo.result[0][0].trackNo.ToString().ToUpper(), 24);
                    g.Graphics.DrawImage(imgTrack, new Point(pX + 435, pY + 400));
                    stringFont = new Font("Calibri", 8);
                    g.Graphics.DrawString("Track No.", stringFont, brush, new PointF(pX + 432, pY + 426));
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].trackNo.ToString().ToUpper(), stringFont, brush, new PointF(pX + 518, pY + 426));
                }
                /////////////////////////////////////////////////////////////////////////////////////////

                pX = 60;
                pY = 60;
                float[] dashValues = { 5, 2, 15, 4 };
                Point pointX = new Point(pX, pY + 500);
                Point pointY = new Point(pX + 710, pY + 500);
                Pen linePen = new Pen(Color.Gray);
                linePen.DashPattern = dashValues;
                g.Graphics.DrawLine(linePen, pointX, pointY);

                Image image2 = Image.FromFile("logo.png");
                Rectangle destRect2 = new Rectangle(pX, pY + 540, 134, 32);
                g.Graphics.DrawImage(image2, destRect2, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

                SolidBrush brush2 = new SolidBrush(Color.Black);
                Font stringFont2 = new Font("DilleniaUPC", 14);
                stringFont2 = new Font("DilleniaUPC", 14, FontStyle.Bold);
                g.Graphics.DrawString("ศูนย์บริการ Remax Thailand", stringFont2, brush2, new PointF(pX - 2, pY + 30 + 540));
                stringFont = new Font("DilleniaUPC", 14);
                g.Graphics.DrawString("99 หมู่ที่ 8 อาคารเชียร์รังสิต ชั้น G ห้อง GB066", stringFont2, brush2, new PointF(pX - 2, pY + 50 + 540));
                g.Graphics.DrawString("ถนนพหลโยธิน ตำบลคูคต อำเภอลำลูกกา", stringFont2, brush2, new PointF(pX - 2, pY + 70 + 540));
                g.Graphics.DrawString("จังหวัด ปทุมธานี 12130", stringFont2, brush2, new PointF(pX - 2, pY + 90 + 540));
                g.Graphics.DrawString("โทรศัพท์ 081-8288-833", stringFont2, brush2, new PointF(pX - 2, pY + 110 + 540));

                g.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Gray)), pX + 430, pY + 540, 140, 50);

                Code128BarcodeDraw bdw2 = BarcodeDrawFactory.Code128WithChecksum;
                Image img2 = bdw.Draw(claimNo.ToUpper(), 24);
                g.Graphics.DrawImage(img2, new Point(pX + 435, pY + 5 + 540));

                stringFont2 = new Font("Calibri", 8);
                g.Graphics.DrawString(claimNo, stringFont2, brush2, new PointF(pX + 432, pY + 31 + 540));

                DateTime now2 = DateTime.Now;
                g.Graphics.DrawString(now2.ToString("ddMMHHmm"), stringFont2, brush2, new PointF(pX + 517, pY + 31 + 540));

                pX += 200;
                pY += 170;
                stringFont2 = new Font("DilleniaUPC", 18);
                g.Graphics.DrawString("กรุณาส่ง", stringFont2, brush2, new PointF(pX - 50, pY + 540));
                if (jsonAddressInfo.result[0][0].tel.ToString().Length > 1)
                {
                    g.Graphics.DrawString("โทรศัพท์ " + jsonAddressInfo.result[0][0].tel.ToString().Substring(0, 3) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(3, 4) + "-" +
                    jsonAddressInfo.result[0][0].tel.ToString().Substring(7),
                    stringFont2, brush2, new PointF(pX + 47, pY + 540));
                }
                else
                {
                    g.Graphics.DrawString("โทรศัพท์ -", stringFont2, brush2, new PointF(pX + 47, pY + 540));
                }
                pY += 50;
                stringFont2 = new Font("DilleniaUPC", 24, FontStyle.Bold);
                g.Graphics.DrawString("คุณ " + jsonAddressInfo.result[0][0].firstname.ToString() + " " + jsonAddressInfo.result[0][0].lastname.ToString(), stringFont2, brush2, new PointF(pX, pY + 540));

                pY += 42;
                pX += 47;

                stringFont2 = new Font("DilleniaUPC", 18);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].address.ToString(), stringFont2, brush2, new PointF(pX, pY + 540));
                pY += 35;
                if (jsonAddressInfo.result[0][0].address2.ToString().Trim() != "")
                {
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].address2.ToString(), stringFont2, brush2, new PointF(pX, pY + 540));
                    pY += 35;
                }

                bool isBkk2 = jsonAddressInfo.result[0][0].province.ToString() == "กรุงเทพมหานคร";
                g.Graphics.DrawString((isBkk2 ? "แขวง" : "ต.") + jsonAddressInfo.result[0][0].subDistrict.ToString() +
                    (isBkk ? " เขต" : " อ.") + jsonAddressInfo.result[0][0].district.ToString() +
                    (isBkk ? " " : " จ.") + jsonAddressInfo.result[0][0].province.ToString() + "",
                    stringFont2, brush2, new PointF(pX, pY + 540));
                pY += 35;
                g.Graphics.DrawString("รหัสไปรษณีย์", stringFont2, brush2, new PointF(pX + 30, pY + 540));
                stringFont2 = new Font("Calibri", 18, FontStyle.Bold);
                g.Graphics.DrawString(jsonAddressInfo.result[0][0].zipcode.ToString(), stringFont2, brush2, new PointF(pX + 140, pY + 540));

                pX = 60;
                pY = 60;
                if (jsonAddressInfo.result[0][0].trackNo.ToString().Length > 1)
                {
                    Code128BarcodeDraw bdwTrack = BarcodeDrawFactory.Code128WithChecksum;
                    Image imgTrack = bdwTrack.Draw(jsonAddressInfo.result[0][0].trackNo.ToString().ToUpper(), 24);
                    g.Graphics.DrawImage(imgTrack, new Point(pX + 435, pY + 400 + 540));
                    stringFont = new Font("Calibri", 8);
                    g.Graphics.DrawString("Track No.", stringFont, brush, new PointF(pX + 432, pY + 426 + 540));
                    g.Graphics.DrawString(jsonAddressInfo.result[0][0].trackNo.ToString().ToUpper(), stringFont, brush, new PointF(pX + 518, pY + 426 + 540));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is : " + ex);
            }

        }


    }
}
