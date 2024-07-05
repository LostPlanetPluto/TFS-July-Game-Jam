using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TutorialScene : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ToControlScheme()
    {
        anim.Play("ToControlScheme");
    }

    public void ToGameScheme()
    {
        FindAnyObjectByType<UI_Fader>().FadeToNextScene("Game Scene V2");
    }
}
