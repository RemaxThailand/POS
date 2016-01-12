using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PowerPOS
{
    class DownloadImage
    {
        public void Download(string url, string savePath, string fileName)
        {
            fileName = savePath + "/" + fileName;
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, fileName);
                }
            }
            catch { }
        }
    }
}
