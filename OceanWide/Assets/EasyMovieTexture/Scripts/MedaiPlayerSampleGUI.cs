using UnityEngine;
using System.Collections;

public class MedaiPlayerSampleGUI : MonoBehaviour {


	public MediaPlayerCtrl scrMedia;
	
	public bool m_bFinish = false;
	// Use this for initialization
	void Start () {
		scrMedia.OnEnd += OnEnd;

	}

	
	// Update is called once per frame
	void Update () {


	
	}
	#if !UNITY_WEBGL
	void OnGUI() {
		
	
		if( GUI.Button(new Rect(50,50,100,100),"Load"))
		{
			scrMedia.Load("video.flv");
			m_bFinish = false;
		}
		
		if( GUI.Button(new Rect(50,200,100,100),"Play"))
		{
			scrMedia.Play();
			m_bFinish = false;
		}
	 	
		if( GUI.Button(new Rect(50,350,100,100),"stop"))
		{
			scrMedia.Stop();
		}
		
		if( GUI.Button(new Rect(50,500,100,100),"pause"))
		{
			scrMedia.Pause();
		}
		
		if( GUI.Button(new Rect(50,650,100,100),"Unload"))
		{
			scrMedia.UnLoad();
		}
		
		if( GUI.Button(new Rect(50,800,100,100), " " + m_bFinish))
		{
		
		}
		
		if( GUI.Button(new Rect(200,50,100,100),"SeekTo"))
		{
			scrMedia.SeekTo(10000);
		}
	}
	#endif


	
	void OnEnd()
	{
		m_bFinish = true;
	}
}
