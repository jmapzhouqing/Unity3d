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
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<UniversalMediaPlayer>();
        //this.PlayVideo("");
    }

    public void PlayVideo(string url) {
        socket = new CorrespondWebSocket(Application.persistentDataPath + "/" + "video.flv");
        socket.Connect("ws://222.128.39.16:8866/live?url=rtsp://admin:admin@192.168.1.173:554/cam/realmonitor?channel=10&subtype=0", delegate (string fileName) {
            player.Path = fileName;
            StartCoroutine(Play());
        });
    }


    IEnumerator Play() {
        player.Stop();
        yield return new WaitForSeconds(10.0f);
        player.Play(); 
    }

    private void OnDestroy()
    {
        socket.Destory();
    }
}
