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

        fader.FadeToNextScene("Game Scene");
    }

    public void GoToMainMenu()
    {
        if (GameManager.instance != null) Destroy(GameManager.instance);

        fader.FadeToNextScene("Main Menu");
    }
}
