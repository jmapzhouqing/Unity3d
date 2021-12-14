using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;

public class CorrespondWebSocket
{

    private ClientWebSocket socket;
    private CancellationTokenSource source;
    private CancellationToken token;

    private Task receive_data;

    private ArraySegment<byte> buffer;

    private byte[] buffer_data;

    FileStream stream = null;

    private string fileName;

    private int number = 0;

    public CorrespondWebSocket(string fileName)
    {
        this.fileName = fileName;
        socket = new ClientWebSocket();
        source = new CancellationTokenSource();
        token = source.Token;
        buffer_data = new byte[10240];
        buffer = new ArraySegment<byte>(buffer_data);

        receive_data = new Task(async () =>
        {
            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                Task<WebSocketReceiveResult> task = socket.ReceiveAsync(buffer, token);
                await task;

                WebSocketReceiveResult result = task.Result;
                /*
                byte[] data = new byte[result.Count];

                Buffer.BlockCopy(buffer_data, 0, data, 0, data.Length);*/

                if (stream != null) {
                    stream.Write(buffer_data, 0, result.Count);
                    stream.Flush(true);
                }
               
            }
        });
    }

    public long GetNumber() {
        return this.stream.Length;
    }

    public async void Connect(string ip,Action<string> action)
    {
        await socket.ConnectAsync(new Uri(ip), token);

        stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 102400, true);

        if (action != null) {
            action(fileName);
        }

        receive_data.Start();
    }

    public void Destory() {
        try
        {
            this.source.Cancel();
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }
            File.Delete(this.fileName);
            socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", token);
        }
        catch (Exception e) {
            Debug.Log(e.Message);
        }
    }

}
