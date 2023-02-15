using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnStart : MonoBehaviour
{

	public string[] audios = {"startLevel","Music"};

	private AudioManager am;

	// Use this for initialization
	void Awake ()
	{
		am = FindObjectOfType<AudioManager> ();

		foreach (string name in audios) {
			if (!am.GetSource (name).isPlaying) {
				am.Play (name);
			}
		}
	}
}
