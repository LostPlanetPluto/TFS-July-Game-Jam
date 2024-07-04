using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Pause Properties
    public event Action onPause;
    public event Action onResume;
    private bool isPaused = false;

    // Point Properties
    public event Action onAddPoint;
    private int points = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        onPause += PauseBehaviours;
        onResume += ResumeBehaviours;
    }

    private void OnDestroy()
    {
        onPause -= PauseBehaviours;
        onResume -= ResumeBehaviours;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    private void PauseBehaviours()
    {
        isPaused = true;
    }

    private void ResumeBehaviours()
    {
        isPaused = false;
    }

    public void AddPoint()
    {
        points++;
        onAddPoint?.Invoke();
    }

    public int GetPoints()
    {
        return points;
    }
}
