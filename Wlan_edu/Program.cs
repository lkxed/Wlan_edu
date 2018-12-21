using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fuck
{
    class Program
    {
        static void Help()
        {
            Console.WriteLine("\n使用方法\n");
            Console.WriteLine("　　初始化：fuck init 手机号 固定密码");
            Console.WriteLine("　　连　接：fuck on");
            Console.WriteLine("　　下　线：fuck off");
            Console.WriteLine("　　找自己：fuck me");
        }

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string action = args[0];
                FileStream fstream = null;
                StreamReader sreader = null;
                try
                {
                    fstream = new FileStream("data", FileMode.Open);
                    sreader = new StreamReader(fstream, Encoding.UTF8);
                    byte[] encrypted = Convert.FromBase64String(sreader.ReadToEnd());
                    string[] account = Encoding.UTF8.GetString(encrypted).Split(':');

                    if (action == "on")
                    {
                        string username = account[0];
                        string password = account[1];
                        if (Action.Login(username, password))
                        {
                            Console.WriteLine("\n登录成功，开始网上冲浪吧！");
                        }
                        else
                        {
                            Console.WriteLine("\n登录失败，请检查：1.是否已连接 WiFi 至 Wlan-edu  2.手机号密码是否正确");
                        }
                    }
                    else if (action == "off")
                    {
                        string username = account[0];
                        if (Action.Logout(username))
                        {
                            Console.WriteLine("\n下线成功");
                        }
                        else
                        {
                            Console.WriteLine("\n下线失败，你真的还在线上吗？");
                        }
                    }
                    else if (action == "me")
                    {
                        string username = account[0];
                        string password = account[1];
                        Console.WriteLine("\n手机号：{0}\n密　码：{1}", username, password);
                    }
                    else Help();
                }
                catch (FileNotFoundException)
                {
                    Help();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.ToString());
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("程序运行中出现错误，如需帮助请附截图联系作者");
                }
                finally
                {
                    if (sreader != null)
                    {
                        sreader.Close();
                    }
                    if (fstream != null)
                    {
                        fstream.Close();
                    }
                }
            }
            else if (args.Length == 3)
            {
                string action = args[0];
                if (action == "init")
                {
                    string username = args[1];
                    string password = args[2];
                    string account = string.Format("{0}:{1}", username, password);

                    // 登录前验证
                    if (!Util.IsPhoneValid(username))
                    {
                        Console.WriteLine("\n手机号格式不正确");
                    }
                    else if (!Util.IsPasswordValid(password))
                    {
                        Console.WriteLine("\n密码中含有非法字符，也许是空格或（~'!$%^*()+<>=|\\;:,?/#@&`\"[]{}.）");
                        Console.WriteLine("PS：固定密码一般为8位");
                    }
                    else
                    {
                        byte[] temp = Encoding.UTF8.GetBytes(account);
                        string encrypted = Convert.ToBase64String(temp);

                        FileStream fstream = new FileStream("data", FileMode.OpenOrCreate);
                        StreamWriter swriter = new StreamWriter(fstream);

                        swriter.Write(encrypted); // 存储账号密码信息

                        Console.WriteLine("\n初始化成功");

                        // 关闭IO流
                        swriter.Close();
                        fstream.Close();
                    }
                }
                else Help();
            }
            else Help();
        }
    }
}
