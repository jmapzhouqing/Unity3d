using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System;
using System.Threading.Tasks;

public class WebSocketControl
{
	private ClientWebSocket ws=new ClientWebSocket();
	private CancellationToken ct=new CancellationToken();
	private string urlPrefixStr = "ws://qy_uat.civicsquare.com.cn/saas/spray?guid=gbase%7Cgmaintenance%7Cginspection%7Cgorder%7Cisso%7Ciot%7Cnanhai%7Cinotify%7Citag%7Ciorganization&u=";
	private Uri url;
	private string subscribeMsg;
	public event EventHandler<string> receivePross;
	

	public void SetUrl(string token) {
		string urlStr = this.urlPrefixStr + token;
		this.url = new Uri(urlStr);
	}
	public void SetSubscribe(string msg)
	{
		this.subscribeMsg = msg;
	}

	public async void Connect() {
		try
		{
			await ws.ConnectAsync(this.url, ct);
			Debug.Log("websocket connect");
			//await this.ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(this.subscribeMsg)), WebSocketMessageType.Text, true, ct);
			while (true)
			{
				var result = new byte[1024];
				await this.ws.ReceiveAsync(new ArraySegment<byte>(result), ct);
				receivePross(this.ws, Encoding.UTF8.GetString(result).TrimEnd('\0'));
			}
		}
		catch (Exception e) {
			Debug.Log("Connect websocket err:" + e.Message);
		}

	}

	public void Close() {
		this.ws.Dispose();
	}

	public async void SendMsg(string content)
	{
		try
		{
			await this.ws.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(content)), WebSocketMessageType.Text, true, ct);			
		}
		catch (Exception e)
		{
			Debug.Log("websocket SendMsg err:" + e.Message);
		}

	}

}
