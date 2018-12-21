using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuck
{
    public class Action
    {
        public static bool Login(string username, string password)
        {
            // 请求URL
            string url_template = "https://211.138.125.52:7090/zmcc/portalLogin.wlan?{0}";
            string url = string.Format(url_template, Util.GetTimeStamp());

            // 请求头
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"Upgrade-Insecure-Requests", "1"},
                {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36"},
                {"Host", "211.138.125.52:7090"},
                {"Cookie", "nActions=\"http://111.1.47.58/school/wlan.do?areaCode=null\"; ALVERIFY=0; theme_name=THEME_PC"},
                {"Connection", "keep-alive"},
                {"Cache-Control", "max-age=0"},
                {"Accept-Encoding", "gzip, deflate, br"},
                {"Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7,ja;q=0.6"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"}
            };

            // 请求表单
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"wlanAcName", "0437.0571.571.00"},
                {"wlanAcIp", ""},
                {"wlanUserIp", Util.GetIpAddress()},
                {"ssid", ""},
                {"userName", username},
                {"_userPwd", "输入固定密码/临时密码"},
                {"userPwd", password},
                {"verifyCode", "输入验证码"},
                {"verifyHidden", ""},
                {"button", "  "},
                {"loginBtn", "  "},
                {"issaveinfo", ""},
                {"passType", "0"}
            };

            // 发送请求
            string responseBody = Util.Post(url, data, headers);
            return responseBody.Contains("正在登录中");
        }

        public static bool Logout(string username)
        {
            // 请求URL
            string url_template = "https://211.138.125.52:7090/zmcc/portalLogout.wlan?isCloseWindow=N&{0}";
            string url = string.Format(url_template, Util.GetTimeStamp());

            // 请求头
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"Upgrade-Insecure-Requests", "1"},
                {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36"},
                {"Host", "211.138.125.52:7090"},
                {"Cookie", "nActions=\"http://111.1.47.58/school/wlan.do?areaCode=null\"; ALVERIFY=0; theme_name=THEME_PC"},
                {"Connection", "keep-alive"},
                {"Cache-Control", "max-age=0"},
                {"Accept-Encoding", "gzip, deflate, br"},
                {"Referer", "https://211.138.125.52:7090/zmcc/portalLoginRedirect.wlan"},
                {"Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7,ja;q=0.6"},
                {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"}
            };

            // 请求表单
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                {"wlanAcName", "0437.0571.571.00"},
                {"wlanAcIp", ""},
                {"wlanUserIp", Util.GetIpAddress()},
                {"ssid", ""},
                {"userName", username},
                {"encryUser", ""},
                {"passType", "1"},
            };

            // 发送请求
            string responseBody = Util.Post(url, data, headers);
            return responseBody.Contains("下线成功");
        }
    }
}
