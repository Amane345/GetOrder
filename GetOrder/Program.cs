using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Newegg
{
    class Program
    {
        static void Main(string[] args)
        {
           try
            {
                GetInfo getInfo = new GetInfo();
              
                string url = "https://api.newegg.com/marketplace/ordermgmt/order/orderinfo?sellerid={0}";
                url = String.Format(url, "A006");
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "PUT";

                httpRequest.Headers["Authorization"] = "720ddc067f4d115bd544aff46bc75634";
                httpRequest.Headers["Secretkey"] = "21EC2020-3AEA-1069-A2DD-08002B30309D";
                httpRequest.ContentType = "application/json";

                var data = @"{

  ""OperationType"": ""GetOrderInfoRequest"",
""RequestBody"": {
""RequestCriteria"": {
""OrderNumberList"": {
                ""OrderNumber"": [
                    159243598,
                    41473642
                                 ]
                       }
                         }
     }
}";

                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                Console.WriteLine(httpResponse.StatusCode);

                int orderNumber = getInfo.OrderNumber;
                string shipToAddress1 = getInfo.ShipToAddress1;
                int shipToZipCode = getInfo.ShipToZipCode;
                string shipToCountryCode = getInfo.ShipToCountryCode;
                string shipToFirstName = getInfo.ShipToFirstName;
                string message = String.Format("OrderNumber:{0}\n ShipToAddress1:{1}\n ShipToZipCode:{2} \n ShipToCountryCode:{3}\n ShipToFirstName:{4} ",
                    getInfo.OrderNumber,
                    getInfo.ShipToAddress1,
                    getInfo.ShipToZipCode,
                    getInfo.ShipToCountryCode,
                   getInfo.ShipToFirstName);
                Console.WriteLine(message);

                Console.WriteLine(string.Format("Newegg Marketplace API - Get Order Information request at:{0}", DateTime.Now.ToString()));
                Console.WriteLine("");
                Console.WriteLine("*********************************************************************");
                Console.WriteLine("");

            }
            catch (WebException we)
            {
                if (((WebException)we).Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse errResp = ((WebException)we).Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        Console.WriteLine(String.Format("{0}", reader.ReadToEnd()));
                    }
                }
            }
        }
    }
}
