using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [Header("Settings Menu")]
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;

    // Start is called before the first frame update
    void Start()
    {
        masterVolume.onValueChanged.AddListener(delegate { MasterVolumeChanged(); });
        musicVolume.onValueChanged.AddListener(delegate { MusicVolumeChanged(); });
        sfxVolume.onValueChanged.AddListener(delegate { SFXVolumeChanged(); });

    }

    public void MasterVolumeChanged()
    {
        if (AudioManager.instance != null) AudioManager.instance.SetMasterVolume(masterVolume.value);
    }

    public void MusicVolumeChanged()
    {
        if (AudioManager.instance != null) AudioManager.instance.SetMasterVolume(musicVolume.value);
    }

    public void SFXVolumeChanged()
    {
        if (AudioManager.instance != null) AudioManager.instance.SetMasterVolume(sfxVolume.value);
    }
}
