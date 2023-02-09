﻿using System;
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

    }
}



