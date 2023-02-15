using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSounds : MonoBehaviour
{

	public string[] sounds;

	// Use this for initialization
	void Awake ()
	{
		AudioSource[] AS = FindObjectsOfType<AudioSource> ();
		
		for (int i = 0; i < AS.Length; i++) {
			for (int s = 0; i< sounds.Length; i++) {
				if (AS [i].name == sounds [s])
					AS [i].Stop ();
			}
		}
	}
}
