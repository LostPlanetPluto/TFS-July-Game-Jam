using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [Header("Fader Properties")]
    [SerializeField] private UI_Fader fader;

    [Header("Points Properties")]
    [SerializeField] private TextMeshProUGUI pointsText;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip playSound;
    [SerializeField] private AudioClip selectSound;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            pointsText.text = GameManager.instance.GetPoints().ToString();
        }
    }

    public void GoToPlayAgain()
    {
        if (GameManager.instance != null) Destroy(GameManager.instance);
        if (AudioManager.instance != null) AudioManager.instance.PlaySFX(playSound);

        fader.FadeToNextScene("Game Scene V2");
    }

    public void GoToMainMenu()
    {
        if (GameManager.instance != null) Destroy(GameManager.instance);
        if (AudioManager.instance != null) AudioManager.instance.PlaySFX(selectSound);

        fader.FadeToNextScene("Main Menu V2" +
            "");
    }
}
