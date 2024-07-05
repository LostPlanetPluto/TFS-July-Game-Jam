using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    [SerializeField] private AudioClip splashClip;

    private bool canSplash = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canSplash) return;

        if (AudioManager.instance != null) AudioManager.instance.PlaySFX(splashClip);
        canSplash = false;

        StartCoroutine(SplashDelay());
    }

    IEnumerator SplashDelay()
    {
        yield return new WaitForSeconds(2f);

        canSplash = true;
    }
}
