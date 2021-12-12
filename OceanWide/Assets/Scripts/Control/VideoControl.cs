using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using System;

public class VideoControl : MonoBehaviour
{
    //private VideoPlayer player;

    private MediaPlayerCtrl player;

    CorrespondWebSocket socket = null;

    private int position = 0;

    private bool is_ready = false;

    private string fileName;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<MediaPlayerCtrl>();

        player.OnEnd += OnEnd;
        player.OnVideoFirstFrameReady += OnVideoReady;

        player.m_bLoop = false;

        //this.PlayVideo("ws://222.128.39.16:8866/live?url=rtsp://admin:admin@192.168.1.173:554/cam/realmonitor?channel=10&subtype=0");

        //this.PlayVideo("ws://222.128.39.25:8866/live?url=rtsp://admin:qwer1234@10.10.11.110:554/h264/ch1/main/av_stream");
    }

    private void OnEnd() {
        StartCoroutine(WaitPlayer());
    }

    private void OnVideoReady() {
        is_ready = true;
        //Debug.Log("EnterReady");
    }

    public void PlayVideo(string url) {
        socket = new CorrespondWebSocket(Application.persistentDataPath + "/" + "video.flv");
        //Debug.Log(Application.persistentDataPath + "/" + "video.flv");
        //socket = new CorrespondWebSocket(Application.streamingAssetsPath + "/" + "video.flv");

        socket.Connect(url, delegate (string fileName) {
            //player.m_strFileName = "video.flv";
            this.fileName = "file://" + fileName;
            StartCoroutine(Play(this.fileName));
        });
    }

    public void EndReached(){
        //position = player.Position;
        StartCoroutine(WaitPlayer());
    }

    private IEnumerator WaitPlayer(){
        player.UnLoad();
        player.Load(this.fileName);

        while (!player.GetCurrentState().Equals(MediaPlayerCtrl.MEDIAPLAYER_STATE.READY))
        {
            yield return new WaitForEndOfFrame();
        }
        //player.SeekTo(this.position);
        player.Play();

        //player.SeekTo(this.position);
        //player.Position = this.position;
    }


    IEnumerator Play(string fileName) {
        yield return new WaitForSeconds(4.0f);

        if (socket.GetNumber() > 1024 * 5)
        {
            player.Load(fileName);

            while (!player.GetCurrentState().Equals(MediaPlayerCtrl.MEDIAPLAYER_STATE.READY))
            {
                yield return new WaitForEndOfFrame();
            }

            player.Play();
        }
        else {
            Debug.Log(socket.GetNumber()/1024);
            Debug.Log("小于5K");
        }
    }

    private void OnDestroy()
    {
        player.UnLoad();
        socket.Destory();

    }

}
