using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAllSounds : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
	{
		AudioSource[] AS = FindObjectsOfType<AudioSource> ();
		
		for (int i = 0; i < AS.Length; i++) {
			AS [i].Stop ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
