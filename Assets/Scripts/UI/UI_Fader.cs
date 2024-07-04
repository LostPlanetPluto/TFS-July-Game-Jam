using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Fader : MonoBehaviour
{
    private string sceneName;
    public void ChangeScene()
    {
        if (sceneName == null) Debug.Log("No name was given");
        SceneManager.LoadScene(sceneName);
    }

    public void FadeToNextScene(string scene)
    {
        if (scene == null) return;
        sceneName = scene;
        GetComponent<Animator>().Play("Fade In");
    }
}
