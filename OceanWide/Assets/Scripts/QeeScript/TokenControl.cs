using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;
using LitJson;

namespace httpTool
{
    public class tokenMsg {
        public int tenantId;
        public string productKey;
    }
    public class TokenControl
    {
        private string userName;
        private string password;
        private string passwordBase64;
        private string url;
        private string tokenUrl1;
        private string tokenUrl2;
        private string tokenUrl3;
        private string key = @"<RSAKeyValue><Modulus>mgtBIna59QKL1D1x9vSqZcpBBK389GfHvfc6jazEZjDr/FfBaryyo2ge0uMFPppc19qDaJp8on9K/GbpXpa86zCA8x2SHpCMatv3QWOOd87dgSxMXqihfufIP1AIObi304VB1WuT77FNVjogqvFwDQgOZ2m/Rjp4gewW9oDIT/k=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


        public void SetUserName(string value) {
            this.userName = value;
        }

        public void SetPassword(string value)
        {
            this.password = value;
            this.passwordBase64 = SetPasswordBase64();
        }

        public void setUrl(string value) 
        {
            this.url = value;
        }

        public void setTokenUrl1(string value)
        {
            this.tokenUrl1 = value;
        }
        public void setTokenUrl2(string value)
        {
            this.tokenUrl2 = value;
        }
        public void setTokenUrl3(string value)
        {
            this.tokenUrl3 = value;
        }

        private string SetPasswordBase64() 
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(this.key);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(this.password), false);
            return Convert.ToBase64String(cipherbytes);
        }

        public string getToken() {
            string parameters = JsonMapper.ToJson(new Dictionary<string, string> {
            {"userAccount",this.userName},
            {"password",this.passwordBase64} });

            string tokenStr = "";

            string httpResult = HTTPServiceControl.GetPostHttpResponse(this.url, parameters,null);

            if (!string.IsNullOrEmpty(httpResult)) {
                Dictionary<string, string> resultDic = JsonMapper.ToObject<Dictionary<string, string>>(httpResult);
                tokenStr = resultDic["authToken"];
                string tokenStrEncrypt = Convert.ToBase64String(Encoding.UTF8.GetBytes(tokenStr));

                string tokrnResult1 = HTTPServiceControl.GetHttpResponse(this.tokenUrl1 + tokenStrEncrypt, tokenStr);
                
                string tokrnResult2 = HTTPServiceControl.GetHttpResponse(this.tokenUrl2, tokenStr);

                if (!string.IsNullOrEmpty(tokrnResult2)) {
                    List<tokenMsg> tokenMsg = JsonMapper.ToObject<List<tokenMsg>>(tokrnResult2);
                    string tokenParam = JsonMapper.ToJson(new Dictionary<string, string> {
                    {"tenantId",tokenMsg[0].tenantId.ToString()},
                    {"productKey",tokenMsg[0].productKey} });

                    string tokrnResult3 = HTTPServiceControl.GetPostHttpResponse(this.tokenUrl3, tokenParam, tokenStr);
                }
            }
            
            //Debug.Log(tokrnResult3);
            return tokenStr;
        }
    }
}
