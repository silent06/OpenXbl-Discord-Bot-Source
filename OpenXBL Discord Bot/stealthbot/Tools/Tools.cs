using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json;
using DiscordBot;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using Nini.Config;
using System.Net;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace stealthbot
{

    internal class IniParsing
    {
        private string path;
        string SettingsName = "config";
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;
      
        //[DllImport("kernel32")]
        //private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public IniParsing(string INIPath)
        {
            //path = INIPath;
            path = new FileInfo(INIPath ?? SettingsName + ".ini").FullName.ToString();

        }
        
        public void IniWriteValue(string Section, string Key, string Value)
        {
            //WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /*public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }*/

    }

    class Tools
    {
        public static IniParsing LoadedIni;
        public static IConfigSource source = new IniConfigSource("config.ini");
        /*Nini Supporting Documentation https://nini.sourceforge.net/Manual/NiniManual-2.htm#ASimpleExample */

        public static bool Getdebugmode()
        {
            //string debug = LoadedIni.IniReadValue("Config", "DebugMode");
            string debug = source.Configs["Config"].Get("DebugMode");
            bool status = false;
            if (debug == "true")
            {
                status = true;
            }
            else
            {

                status = false;
            }

            return status;
        }

        public static bool GetProxymode()
        {
            //string debug = LoadedIni.IniReadValue("Config", "DebugMode");
            string proxy = source.Configs["Config"].Get("Proxymode");
            bool status = false;
            if (proxy == "true")
            {
                status = true;
            }
            else
            {

                status = false;
            }

            return status;
        }

        public static string GetURL()
        {

            //return LoadedIni.IniReadValue("OpenXbl", "VPSTRING");
            return source.Configs["Config"].Get("URL");
        }

        public static string GetOpenXblVPS()
        {

            //return LoadedIni.IniReadValue("OpenXbl", "VPSTRING");
            return source.Configs["OpenXbl"].Get("VPSTRING");
        }

        public static string GetOpenXblApiKey()
        {
            //return LoadedIni.IniReadValue("OpenXbl", "APIKEY");
            return source.Configs["OpenXbl"].Get("APIKEY");
        }

        public static string GetOpenDiscordAPIToken()
        {
            //return LoadedIni.IniReadValue("Config", "DiscordAPIToken");
            return source.Configs["Config"].Get("DiscordAPIToken");
        }

        public static string GetOpenDiscordTrigger()
        {
            //return LoadedIni.IniReadValue("Config", "DiscordAPIToken");
            return source.Configs["Config"].Get("DiscordTrigger");
        }

        public static string GetSqlHostName()
        {
            //return LoadedIni.IniReadValue("mysql", "host");
            return source.Configs["mysql"].Get("host");
        }

        public static string GetSqlUserName()
        {
            //return LoadedIni.IniReadValue("mysql", "username");
            return source.Configs["mysql"].Get("username");
        }

        public static string GetSqlPassword()
        {
            //return LoadedIni.IniReadValue("mysql", "password");
            return source.Configs["mysql"].Get("password");
        }

        public static string GetSqlDatabase()
        {
            //return LoadedIni.IniReadValue("mysql", "database");
            return source.Configs["mysql"].Get("database");
        }

        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        public static string CreateMD5Hash(string input)
        {
            
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input.Normalize(NormalizationForm.FormKC));
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string StringToHex(string hexstring)
        {
            string outputHex = BigInteger.Parse(hexstring).ToString("X");

            return outputHex;
        }

        public static string BytesToHexString(byte[] buffer)
        {
            string str = "";
            for (int i = 0; i < buffer.Length; i++) str += buffer[i].ToString("X2");
            return str;
        }
        public static long GetTimeStamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        public static void SecondsToTime(int sec, ref int days, ref int hours, ref int minutes, ref int secnds)
        {
            string val = "";

            TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(sec.ToString()));
            if (t.Days > 0)
                val = t.ToString(@"d\d\,\ hh\:mm\:ss");
            else val = t.ToString(@"hh\:mm\:ss");

            if (t.Days > 0)
            {
                days = int.Parse(val.Substring(0, val.IndexOf(',') - 1));
                hours = int.Parse(val.Substring(val.IndexOf(',') + 2, 2));
                minutes = int.Parse(val.Substring(val.IndexOf(':') + 1, 2));
                secnds = int.Parse(val.Substring(val.LastIndexOf(':') + 1));
            }
            else
            {
                hours = int.Parse(val.Substring(0, val.IndexOf(':')));
                minutes = int.Parse(val.Substring(val.IndexOf(':') + 1, 2));
                secnds = int.Parse(val.Substring(val.LastIndexOf(':') + 1));
            }
        }

        public static void AddStringToArray(ref char[] array, string err)
        {
            Array.Copy(err.ToCharArray(), 0, array, 0, err.Length);
        }

        public static byte[] GenerateRandomData(int count)
        {
            byte[] RandData = new byte[count];
            new Random().NextBytes(RandData);

            return RandData;
        }

        public static char[] GenerateRandomDataChars(int count)
        {
            byte[] RandData = new byte[count];
            new Random().NextBytes(RandData);

            return System.Text.Encoding.UTF8.GetString(RandData).ToCharArray();
        }

        public static string BytesToString(byte[] Buffer)
        {
            string str = "";
            for (int i = 0; i < Buffer.Length; i++) str = str + Buffer[i].ToString("X2");
            return str;
        }

        public static string BytesToStringSpaced(byte[] Buffer)
        {
            string str = "";
            for (int i = 0; i < Buffer.Length; i++) str = str + Buffer[i].ToString("X2") + " ";
            return str;
        }

        public static byte[] StringToBytes(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        public static bool PingHostA(string WebAddress) {


            try
            {
                Console.Write("WebAddress: {0}\n", WebAddress);
                // Create a new request to the mentioned URL.				
                WebRequest myWebRequest = WebRequest.Create(WebAddress);

                WebProxy myProxy = new WebProxy();
                // Obtain the Proxy Prperty of the  Default browser.  
                myProxy = (WebProxy)myWebRequest.Proxy;

                // Print myProxy address to the console.
                Console.Write("\nThe actual default Proxy settings are {0}", myProxy.Address);

                Console.Write("\nPlease enter the new Proxy Address to be set ");
                Console.Write("The format of the address should be http://proxyUriAddress:portaddress");
                Console.Write("Example:http://proxyadress.com:8080");
                //string proxyAddress;
                //proxyAddress = Console.ReadLine();

                if (WebAddress.Length == 0)
                {
                    myWebRequest.Proxy = myProxy;
                }
                else
                {

                }
                Console.Write("\nThe Address of the  new Proxy settings are {0}", myProxy.Address);
                WebResponse myWebResponse = myWebRequest.GetResponse();

                // Print the  HTML contents of the page to the console.
                Stream streamResponse = myWebResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse);
                Char[] readBuff = new Char[256];
                int count = streamRead.Read(readBuff, 0, 256);
                Console.Write("\nThe contents of the Html pages are :");
                while (count > 0)
                {
                    String outputData = new String(readBuff, 0, count);
                    Console.Write(outputData);
                    count = streamRead.Read(readBuff, 0, 256);
                }

                // Close the Stream object.
                streamResponse.Close();
                streamRead.Close();

                // Release the HttpWebResponse Resource.
                myWebResponse.Close();
                return true;
                //Console.WriteLine("\nPress any key to continue.........");
                //Console.Read();
            }
            catch (UriFormatException e)
            {
                Console.Write("\nUriFormatException is thrown.Message is {0}", e.Message);
                Console.Write("\nThe format of the myProxy address you entered is invalid");
                return false;
               // Console.WriteLine("\nPress any key to continue.........");
                //Console.Read();
            }

        }

        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber)) {

                    Console.Write("Proxy Server Online!\n");
                    return true;
                }
                    
            }
            catch (SocketException ex)
            {

                Console.Write("Error pinging host: {0}\n", config.Global.debug ? ex.Message : "Ping Failed. Proxy Server offline");
                return false;
            }
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException ex)
            {
                // Discard PingExceptions and return false;
                //Console.Write("Ping Failed. Proxy Server offline\n");
                Console.Write("Ping ERROR: {0}\n", config.Global.debug ? ex.Message : "Ping Failed. Proxy Server offline");
                return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public static string ProxyRandomDownload()
        {

            try {
                //Console.Write("Running Proxy..\n");
                string GetProxy = new WebClient().DownloadString("http://pubproxy.com/api/proxy?https=true");
                var doc = JsonDocument.Parse(GetProxy);
                var Proxy = doc.RootElement.GetProperty("data")[0].GetProperty("ipPort").ToString();
                var RandomProxy = doc.RootElement.GetProperty("data")[0].GetProperty("ip").ToString();
                var Port = doc.RootElement.GetProperty("data")[0].GetProperty("port").ToString();

                Console.Write("Checking Proxy: http://{0}\n", Proxy);
                //PingHostA("http://" + Proxy);
                //PingHost("http://" + Proxy);
                PingHost(RandomProxy, Convert.ToInt32(Port));
                config.Global.RandomProxyV = "http://" + Proxy;
                config.Global.proxyPort = Convert.ToInt32(Port);
                Console.Write("Random Proxy Being Used: {0}\n", RandomProxy);
                Console.Write("Port: {0}\n", Port);

            }
            catch (Exception ex)
            {
                Console.Write("ERROR: {0}\n", config.Global.debug ? ex.Message : "Unable to Fetch Random Proxy");
            }

            return "";
        }

        public static string Proxydownloadstring(string str) {

            WebClient client = new WebClient();
            WebProxy wp = new WebProxy("proxy server url here");
            client.Proxy = wp;
            string strURL = client.DownloadString("http://www.google.com");

            using (WebClient wc = new WebClient()) {

                //string strURL = "http://xxxxxxxxxxxxxxxxxxxxxxxx";

                //Download only when the webclient is not busy.
                if (!wc.IsBusy)
                {
                    string rtn_msg = string.Empty;
                    try
                    {
                        rtn_msg = wc.DownloadString(new Uri(strURL));
                        return rtn_msg;
                    }
                    catch (WebException ex)
                    {
                        Console.Write(ex.Message);
                        return "Error";
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                        return "Error";
                    }
                }
                else
                {

                    return "Error";
                }

            }

            //return str;
        }

    }
}



