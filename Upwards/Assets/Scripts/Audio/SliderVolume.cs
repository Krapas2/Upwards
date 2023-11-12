using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetMasterLvl(float masterLvl){
        audioMixer.SetFloat("MasterLevel",masterLvl);
    }
    public void SetSfxLvl(float sfxLvl){
        audioMixer.SetFloat("SFXLevel",sfxLvl);
    }
    public void SetMusicLvl(float musicLvl){
        audioMixer.SetFloat("SongLevel",musicLvl);
    }
}
