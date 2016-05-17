using DevExpress.XtraEditors;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PowerPOS
{
    public class Param
    {
        public enum StatusIcon { None, Loading, Success, Info };

        public static SQLiteConnection SQLiteConnection;
        //public static SqlConnection SqlLocalDB;
        public static UcSale UcSale;
        public static Main Main;
        //public static string ShopId = "POWERDDH-8888-8888-B620-48D3B6489999";
        //public static string ApiUrl = "http://api.powerdd.com";
        //public static string ApiKey = "27AD365F-FBFF-4994-BB9C-97ABAF80EFBB";

        public enum Screen { Sale, ReceiveProduct, Product, Customer, User, Brand, Category, Color, Report, ShopInfo, Config, Claim, Return, Stock, Statistic, Credit, ReportProduct };
        public static string[] thaiMonth = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };

        public static string ApiShopId; //= "5BB7C6B3-F6D0-4926-B14F-C580DD148612";
        public static string ShopId;// = "00000002";
        public static string ApiUrl;//= "http://api-test.powerdd.com/";
        public static string ApiKey;// = "TEST-0001";
        public static string Token;
        public static Hashtable SqlCeConfig;
        public const string SqlCeFile = "System.sdf";
        public const string ScpSoftwarePath = "/var/www/resources/app/POS";
        public const string ScpUploadPath = "/var/www/upload";
        public static string UserId;
        public static string UserCode;
        public static bool ApiChecked;
        public static string LicenseKey;
        public static string DeviceID;
        public static string ComputerName;
        public static string DatabaseName;
        public static string DatabasePassword;
        public static string ShopName;
        public static string ShopParent;
        public static string ShopCustomer;
        public static string ShopType;
        public static string DevicePrefix;
        public static string DevicePrinter;
        public static string MemberType;
        public static string ProductId;
        public static string CategoryName;
        public static string ImagePath;
        public static string PrintType;
        public static string PrintCount;
        public static string PrintLogo;
        public static string HeaderName;
        public static string FooterText;
        public static string Logo;
        public static string ShopCost;
        public static string PaperSize;


        public static bool jsonObject;
        public static string error;
        public static string errorMessage;

        public static string BarcodeNo;
        public static string SellNo;
        public static string status;
        public static string SelectProduct;

        public static string SelectCustomerId;
        public static string SelectCustomerName;
        public static string SelectCustomerSex;
        public static int SelectCustomerAge;
        public static int SelectCustomerSellPrice;

        public static string amount;
        public static string product;
        

        public static string LogoPath;
        //public const string LogoUrl = "https://lh3.googleusercontent.com/of2iTh9rSFHDQreN0Pu1CIV1_-K9BwqTyfFqNMkDtRA=w2655-no";
        public const string LogoUrl = "https://src.remaxthailand.co.th/img/web/logo/logoPos.jpg";
        public const string LoadingImageLocal = @"Resource/Images/Loading.gif";
        public const string LoadingImageUrl = "http://a.lnwpic.com/et1xpc.gif";
        public const string SQLiteFileName = "System.dll";
        //public const string SQLFileName = "System2.mdf";
        //public const string DBName = "System2";
        //public static string dbFileName;

        public static int SelectedScreen = -1;

        public static Label lblStatus;

        public static PanelControl MainPanel;

        public static bool InitialFinished = false;

        //public static dynamic SystemConfig;


    }
}
