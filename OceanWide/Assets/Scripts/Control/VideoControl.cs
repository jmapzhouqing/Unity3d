using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;

public class VideoControl : MonoBehaviour
{
    //private VideoPlayer player;

    private UniversalMediaPlayer player;

    CorrespondWebSocket socket = null;

    private float position = -1;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<UniversalMediaPlayer>();

        //this.PlayVideo("ws://222.128.39.16:8866/live?url=rtsp://admin:admin@192.168.1.173:554/cam/realmonitor?channel=10&subtype=0");
    }

    public void PlayVideo(string url) {
        socket = new CorrespondWebSocket(Application.persistentDataPath + "/" + "video.flv");
        socket.Connect(url, delegate (string fileName) {
            player.Path = fileName;
            StartCoroutine(Play());
        });
    }

    private void Update()
    {

    }

    public void EndReached(){
        position = player.Position;
        StartCoroutine(WaitPlayer());
    }

    private IEnumerator WaitPlayer() {
        yield return new WaitForSeconds(2.0f);
        player.Play();
        player.Position = this.position;
    }


    IEnumerator Play() {
        player.Stop();
        while (socket.GetNumber() < 1024*400) {
            yield return new WaitForEndOfFrame();
        }
        player.Play(); 
    }

    private void OnDestroy()
    {
        socket.Destory();
    }
}
