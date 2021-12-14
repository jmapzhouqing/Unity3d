using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using System;
using static MediaPlayerCtrl;

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
        player.OnVideoError += OnVideoError;
        player.OnVideoFirstFrameReady += OnVideoReady;

        player.m_bLoop = false;

        //this.PlayVideo("ws://222.128.39.16:8866/live?url=rtsp://admin:admin@192.168.1.173:554/cam/realmonitor?channel=10&subtype=0");

        //this.PlayVideo("ws://222.128.39.25:8866/live?url=rtsp://admin:qwer1234@10.10.11.110:554/h264/ch1/main/av_stream");
    }

    private void OnEnd() {
        this.position = player.GetDuration();
        StartCoroutine(WaitPlayer());
    }

    private void OnVideoReady() {
        is_ready = true;
        //Debug.Log("EnterReady");
    }

    private void OnVideoError(MEDIAPLAYER_ERROR errorCode, MEDIAPLAYER_ERROR errorCodeExtra) {
        Debug.Log("Enter Error");
    }

    public void PlayVideo(string url) {
        socket = new CorrespondWebSocket(Application.persistentDataPath + "/" + "video.flv");

        Debug.Log(Application.persistentDataPath);

        //socket = new CorrespondWebSocket(Application.streamingAssetsPath + "/" + "video.flv");

        socket.Connect(url, delegate (string fileName){
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
        this.position = player.GetSeekPosition();
        player.UnLoad();
        yield return new WaitForSeconds(1.0f);
        player.Load(this.fileName);

        while (!player.GetCurrentState().Equals(MediaPlayerCtrl.MEDIAPLAYER_STATE.READY)){
            yield return new WaitForEndOfFrame();
        }
        
        player.Play();

        yield return new WaitForEndOfFrame();

        player.SeekTo(this.position);

        //player.SeekTo(this.position);
        //player.Position = this.position;
    }


    IEnumerator Play(string fileName) {
        yield return new WaitForSeconds(2.0f);

        if (socket.GetNumber() > 0){
            player.Load(fileName);

            yield return new WaitForSeconds(0.5f);

            if (!player.GetCurrentState().Equals(MediaPlayerCtrl.MEDIAPLAYER_STATE.READY))
            {
                player.UnLoad();
                StartCoroutine(Play(fileName));
                yield break;
            }
            else
            {
                player.SeekTo(position);
                player.Play();
                //player.SetSpeed(0.5f);
            }
        }else {
            StartCoroutine(Play(fileName));
            yield break;
        }
        
        /*
        while(socket.GetNumber() < 1024 * 200){
            yield return new WaitForSeconds(1.0f);
        }

        player.Load(fileName);

        while (!player.GetCurrentState().Equals(MediaPlayerCtrl.MEDIAPLAYER_STATE.READY)){
            yield return new WaitForEndOfFrame();
        }

        player.Play();*/
    }

    private void OnDestroy()
    {
        player.UnLoad();
        socket.Destory();

    }

}
