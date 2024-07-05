using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAssistant : MonoBehaviour
{
    [SerializeField] private AudioClip levelTheme;
    void Start()
    {
        if (AudioManager.instance != null) AudioManager.instance.PlayMusic(levelTheme);
    }
}
