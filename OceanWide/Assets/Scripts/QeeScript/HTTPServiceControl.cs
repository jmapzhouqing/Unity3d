using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace httpTool
{
    public class HTTPServiceControl
    {
        
        
        public static string GetPostHttpResponse(string url, string content,string cookies)
        {
            var result = "";
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.ServicePoint.Expect100Continue = false;
                req.Method = "POST";
                req.ContentType = "application/json; charset=UTF-8";
                req.UserAgent = "Mozilla/5.0";

                if (cookies != null)
                {
                    CookieContainer cc = new CookieContainer();
                    cc.Add(new Uri(url), new Cookie("IORISESSION", cookies));
                    req.CookieContainer = cc;

                }

                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream()){
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }catch (Exception ex){
                PrimaryContorl.dialog.SetActive(true);
                PrimaryContorl.dialog.GetComponent<DialogControl>().setContent(ex.Message);
                Debug.Log("GetPostHttpResponse err：" + ex.Message);
            }
            return result;
        }

        public static string GetHttpResponse(string url,string cookies)
        {
            var retString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json;charset=UTF-8";
                request.UserAgent = "Mozilla/5.0";
                if (cookies != null)
                {
                    CookieContainer cc = new CookieContainer();
                    cc.Add(new Uri(url), new Cookie("IORISESSION", cookies));
                    request.CookieContainer = cc;
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
            }
            catch (Exception ex)
            {
                PrimaryContorl.dialog.SetActive(true);
                PrimaryContorl.dialog.GetComponent<DialogControl>().setContent(ex.Message);
                Debug.Log("GetPostHttpResponse err：" + ex.Message);
            }
            return retString;
        }


        public static async Task<string> PostDataAsync(string url, string param, string cookies)
        {
            string responseString = ""; 
            Uri uri = new Uri(url);
            try
            {
                HttpClientHandler handler = new HttpClientHandler() { UseCookies = false };
                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = new TimeSpan(0, 1, 2);
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                    var content = new StringContent(param);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.Add("charset", "utf-8");

                    HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, url);
                    message.Headers.Add("Cookie", "IORISESSION=" + cookies);

                    message.Content = content;

                    var response = await client.SendAsync(message);

                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        responseString = response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                responseString = e.Message;
            }
            return responseString;
        }

        public static async Task<string> GetDataAsync(string url, string cookies)
        {
            string responseString = "";
            Uri uri = new Uri(url);
            try
            {
                HttpClientHandler handler = new HttpClientHandler() { UseCookies = true };
                handler.CookieContainer.SetCookies(uri, "IORISESSION=" + cookies);

                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = new TimeSpan(0, 0, 2);
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                    var response = await client.GetAsync(uri);

                    if (response.IsSuccessStatusCode)
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        responseString = response.ReasonPhrase;
                    }
                }
            }
            catch (Exception e)
            {
                responseString = e.Message;
            }
            return responseString;
        }

    }
}
