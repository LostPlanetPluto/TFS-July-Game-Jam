using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauseable : MonoBehaviour
{
    protected bool isPaused = false;

    protected virtual void Start()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onPause += PauseBehaviours;
            GameManager.instance.onResume += ResumeBehaviours;
            isPaused = GameManager.instance.GetIsPaused();
        }
    }

    protected virtual void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.onPause -= PauseBehaviours;
            GameManager.instance.onResume -= ResumeBehaviours;
        }
    }
    protected void PauseBehaviours()
    {
        isPaused = true;
    }

    protected void ResumeBehaviours()
    {
        isPaused = false;
    }
}
