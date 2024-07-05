using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Fader Properties")]
    [SerializeField] private UI_Fader fader;

    [Header("Main Menu Object")]
    [SerializeField] private GameObject mainMenu;

    [Header("Animator Properties")] 
    [SerializeField] private Animator anim;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip playSound;
    [SerializeField] private AudioClip selectSound;

    public void GoToGameScene()
    {
        MenuSounds(playSound);
        fader.FadeToNextScene("Game Scene V2");
    }

    public void DisplaySettings()
    {
        MenuSounds(selectSound);
        anim.Play("Go To Settings");
    }

    public void DisplayCredits()
    {
        MenuSounds(selectSound);
        anim.Play("Go To Credits");
    }

    public void SettingsToMain()
    {
        MenuSounds(selectSound);
        anim.Play("Settings To Main");
    }

    public void CreditsToMain()
    {
        MenuSounds(selectSound);
        anim.Play("Credits To Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void MenuSounds(AudioClip clip)
    {
        if (AudioManager.instance != null) AudioManager.instance.PlaySFX(clip);
    }
}
