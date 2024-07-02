using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Spawn Properties")]


    private float spawnTimer = 0;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onPause += PauseBehaviours;
            GameManager.instance.onResume += ResumeBehaviours;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.instance)
        {
            GameManager.instance.onPause -= PauseBehaviours;
            GameManager.instance.onResume -= ResumeBehaviours;
        }
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (isPaused) return;
        spa
    }

    private void PauseBehaviours()
    {
        isPaused = true;
    }

    private void ResumeBehaviours()
    {
        isPaused = false;
    }
}
