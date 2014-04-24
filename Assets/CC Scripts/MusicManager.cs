using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	public AudioClip newMusic;

	void Awake(){
				var go = GameObject.Find ("Game Music");
				go.audio.clip = newMusic;
				go.audio.Play ();
		}


}
