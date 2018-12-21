using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.IO;
using System.Diagnostics;

namespace Fuck
{
    public class Util
    {
        // 判断手机号是否合法
        public static bool IsPhoneValid(string phone)
        {
            string pattern = @"^1[34578]\d{9}$";
            return Regex.IsMatch(phone, pattern);
        }

        // 判断密码中是否有特殊字符
        public static bool IsPasswordValid(string password)
        {
            string pattern = @"^[0-9a-zA-Z]{8}$";
            return Regex.IsMatch(password, pattern);
        }

        // 获取当前时间戳(13位)
        public static string GetTimeStamp()
        {
            DateTime currentTime = DateTime.Now;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timeStamp = (currentTime.Ticks - startTime.Ticks) / 10000; // 除10000调整为13位      
            return timeStamp.ToString();
        }

        // 获取当前IPv4地址
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); // 获取本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostName); // 获取IPv4的地址
            foreach (IPAddress localaddr in localhost.AddressList)
            {
                if (localaddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return localaddr.ToString();
                }
            }
            return "";
        }

        // 获取当前程序的名称
        public static string GetProgramName()
        {
            string fullPath = Process.GetCurrentProcess().MainModule.FileName;
            return Path.GetFileNameWithoutExtension(fullPath).ToLower();
        }

        // HTTP GET 实现
        public static string Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 发起异步请求
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        // HTTP GET 实现 - 可定制请求头
        public static string Get(string url, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 定制请求头
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }

                    // 发起异步请求
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        // HTTP POST 实现
        public static string Post(string url, Dictionary<string, string> data)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 定制请求数据
                    FormUrlEncodedContent content = new FormUrlEncodedContent(data);

                    // 发起异步请求
                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        // HTTP POST 实现 - 可定制请求头
        public static string Post(string url, Dictionary<string, string> data, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 定制请求头
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }

                    // 定制请求数据
                    FormUrlEncodedContent content = new FormUrlEncodedContent(data);

                    // 发起异步请求
                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    response.EnsureSuccessStatusCode();
                    return response.Content.ReadAsStringAsync().Result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
