﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using System.IO;    
using Newtonsoft.Json;
using System.Text.Json;
using stealthbot;

namespace DiscordBot
{
    public static class OpenXblHttp
    {


        public static object DeserializeFromStream(string stream)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize(jsonTextReader);
            }
        }

        public enum httpVerb
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum authenticationType
        {
            Basic,
            NTLM
        }

        public enum autheticationTechnique
        {
            RollYourOwn,
            NetworkCredential
        }
        
        public static class RestClient
        {
            public static string endPoint { get; set; }
            public static httpVerb httpMethod { get; set; }
            public static authenticationType authType { get; set; }
            public static autheticationTechnique authTech { get; set; }
            public static string strResponseValue = string.Empty;
#pragma warning disable CS1998 
            public static async System.Threading.Tasks.Task<string> makeRequestAsync(string ApiString, string PostRequest, string APIKey, bool httpType)
#pragma warning restore CS1998 
            {
                endPoint = "https://xbl.io/api/v2/";
                //endPoint = Tools.Proxydownloadstring("https://xbl.io/api/v2/");
                //config.Global.ProxySwitch = true;
                WebProxy myproxy = new WebProxy(config.Global.RandomProxyV, true);
                if (config.Global.ProxySwitch) {

                    Tools.ProxyRandomDownload();
                    myproxy = new WebProxy(config.Global.RandomProxyV, true);

                    // Step 1: Extract properties
                    string proxyAddress = myproxy.Address.ToString();
                    string bypassList = string.Join(", ", myproxy.BypassList);
                    bool useDefaultCredentials = myproxy.UseDefaultCredentials;

                    // Step 2: Format the output
                    string output = $"WebProxy Details:\n" +
                                    $"Proxy Address: {proxyAddress}\n" +
                                    $"Bypass List: {bypassList}\n" +
                                    $"Use Default Credentials: {useDefaultCredentials}\n";

                    // Step 3: Write to Console
                    Console.Write("{0}", output);

                }

                //Console.Write("Completed Proxy: {0}\n", config.Global.RandomProxyV);
                myproxy = new WebProxy(config.Global.RandomProxyV , true);
                //WebProxy myproxy = new WebProxy(proxy, port);

                byte[] byteArray = Encoding.UTF8.GetBytes(PostRequest);
                httpMethod = httpType? httpVerb.GET: httpVerb.POST;
                if (httpType)/*Get Method*/
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint + ApiString);
                    request.Proxy = config.Global.ProxySwitch ? myproxy : null;
                    request.Method = httpMethod.ToString();
                    request.Accept = "application/json";
                    request.Accept = "en-US";
                    request.Accept = "X-Contract: 100";
                    request.Accept = "image/*";
                    request.ContentType = "application/json";
                    request.Headers["X-Authorization"] = APIKey;/*APIKey Goes Here*/

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();

                        if (stealthbot.config.Global.debug) Console.WriteLine("Get reponse status: " + response.StatusCode);
                        //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (StreamReader reader = new StreamReader(responseStream))
                                {
                                    strResponseValue = reader.ReadToEnd();

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                    }
                    finally
                    {
                        if (response != null)
                        {
                            ((IDisposable)response).Dispose();
                        }
                    }
                }
                else if (!httpType)/*Post Method*/ {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint + ApiString);
                    request.Proxy = config.Global.ProxySwitch ? myproxy : null;
                    request.Method = httpMethod.ToString();
                    request.Accept = "application/json";
                    request.Accept = "en-US";
                    request.Accept = "X-Contract: 100";
                    request.Accept = "image/*";                                 
                    request.ContentType = "application/json";
                    request.ContentLength = PostRequest.Length;
                    var reqStream = request.GetRequestStream();
                    reqStream.Write(byteArray, 0, byteArray.Length);
                    request.Headers["X-Authorization"] = APIKey;/*APIKey Goes Here*/

                    HttpWebResponse response = null;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();

                        if (stealthbot.config.Global.debug) Console.WriteLine("Get reponse status: " + response.StatusCode);
                        //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                        using (Stream responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                            {
                                using (StreamReader reader = new StreamReader(responseStream))
                                {
                                    strResponseValue = reader.ReadToEnd();

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
                    }
                    finally
                    {
                        if (response != null)
                        {
                            ((IDisposable)response).Dispose();
                        }
                    }


                }
                /*Left here for future Reference*/
                if (config.Global.debug) Console.WriteLine(strResponseValue);
                /*Please note Linux file permissions made be denied if not set correctly*/

                /*try
                {
                    using (FileStream fileStream = File.Open("/root/OpenXbl", FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                    {
                        StreamWriter writer = new StreamWriter(fileStream);
                        Console.WriteLine("Writing json file to folder");
                        await writer.WriteAsync(strResponseValue);
                        writer.Flush();
                        writer.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }*/


                return strResponseValue;
            }
        }
    }
}


