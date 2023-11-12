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
    void Awake()
    {


            foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.audioMixerGroup;
        }
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager)
        {
            if (hasIntro && audioManager.GetSource("Intro") != null)
            {
                audioManager.Play("Intro");
                Invoke("StartTheme", audioManager.GetSource("Intro").clip.length);
            }
            else audioManager.Play(SceneManager.GetActiveScene().name);
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.source.Stop();
    }

    public AudioSource GetSource(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return null;
        return s.source;
    }

    public void SetVolume(string trackName, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == trackName);
        if (s == null)
        {
            Debug.LogWarning("Track: " + trackName + " not found!");
            return;
        }

        volume = Mathf.Clamp01(volume);

        s.source.volume = volume;
    }

    private void StartTheme()
    {
        if(audioManager)
            audioManager.Play(SceneManager.GetActiveScene().name);
    }
}
