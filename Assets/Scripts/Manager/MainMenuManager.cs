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

    public void GoToGameScene()
    {
        fader.FadeToNextScene("Game Scene");
    }

    public void DisplaySettings()
    {
        anim.Play("Go To Settings");
    }

    public void SettingsToMain()
    {
        anim.Play("Settings To Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
