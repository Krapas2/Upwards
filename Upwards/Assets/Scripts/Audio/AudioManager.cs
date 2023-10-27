using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

	public Sound[] sounds;

	public static AudioManager instance;
    private AudioManager audioManager;
	public bool hasIntro;

	// Use this for initialization
	void Awake ()
	{

		if (instance == null)
			instance = this;
		else {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource> ();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

		if (SceneManager.GetActiveScene().name != "TitleScreen")
		{
			if (hasIntro)
			{
				audioManager.Play("Intro");
				Invoke("StartTheme", audioManager.GetSource("Intro").clip.length);
			}
			else audioManager.Play("Tema");
		}
    }
	
	public void Play (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Play ();
	}

	public void Stop (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return;
		s.source.Stop ();
	}

	public AudioSource GetSource (string name)
	{
		Sound s = Array.Find (sounds, sound => sound.name == name);
		if (s == null)
			return null;
		return s.source;
	}

    private void StartTheme()
    {
        audioManager.Play("Tema");
    }
}
