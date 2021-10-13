using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;

public class PlayVideo : MonoBehaviour
{
    private byte[] data;
    private ArraySegment<byte> buffer;

    private CancellationTokenSource cancle_token_source;
    private CancellationToken cancle_token;
    private Task task;

    private VideoPlayer video;

    FileStream fs;
    BinaryWriter bw;
    private void Awake()
    {

        fs = new FileStream(@"D:\video.mp4", FileMode.OpenOrCreate);
        bw = new BinaryWriter(fs);
        video = this.GetComponent<VideoPlayer>();

        data = new byte[102400];
        buffer = new ArraySegment<byte>(data);

        cancle_token_source = new CancellationTokenSource();
        cancle_token = cancle_token_source.Token;
    }

    private void Start()
    {
       task = StartAsync();
        //Debug.Log(task.IsCompleted);
    }
    // Start is called before the first frame update
    async Task StartAsync()
    {
        using (var ws = new ClientWebSocket()){
            await ws.ConnectAsync(new Uri("ws://222.128.39.16:8866/live?url=rtsp://admin:admin@192.168.1.174:554/cam/realmonitor?channel=16&subtype=0"), cancle_token);


            while (true) {
                if (cancle_token.IsCancellationRequested) {
                    break;
                }

                WebSocketReceiveResult result = await ws.ReceiveAsync(buffer,cancle_token);

                string value = System.Text.Encoding.Default.GetString(buffer.Array, 0, result.Count);

                Debug.Log(result.Count+"#"+value);

                //bw.Write(buffer.Array, 0, result.Count);

                //Debug.Log(result.Count);
                //this.WriteData(@"D:\hello.jpg", buffer.Array, result.Count);
            }
        }
    }

    public void WriteData(string path,byte[] data,int length) {
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        //开始写入
        bw.Write(data, 0, length);
        //关闭流
        bw.Close();
        fs.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (cancle_token_source != null) {
            cancle_token_source.Cancel();
        }
        bw.Close();
        fs.Close();
    }
}
