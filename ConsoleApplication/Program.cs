using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:40332/api/ServiceAdmin";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Method = "GET";
            //var dataStream = request.GetRequestStream();
            //string postData = "This is the data";
            //var byteArray = Encoding.UTF8.GetBytes(postData);

            //dataStream.Write(byteArray, 0, byteArray.Length);
            //dataStream.Close();

            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            var result = new byte[response.ContentLength];
            var content = responseStream.Read(result, 0, (int)response.ContentLength );
            Console.WriteLine(response.StatusDescription);
            Console.WriteLine(Encoding.UTF8.GetString(result));
            Console.ReadKey();
        }
    }
}
