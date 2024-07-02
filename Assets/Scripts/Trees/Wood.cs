using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [Header("Spawn Wood Properties")]
    [SerializeField] private bool isPickedUp = false;
    private Renderer meshRenderer;
    [SerializeField] private float lifeTime;
    private float lifeTimer = 0;

    [Header("Blink Properties")]
    [SerializeField] [Range(0.1f,1)] private float blinkRatio; // Ratio of lifeTime that object will be blinking
    [SerializeField] [Range(0.1f,1)] private float invisRatio; // Ratio of blinking time that renderer is invisible
    private float blinkTimer = 0;

    private enum WoodType { Birch, Maple, Spruce}
    [Header("Wood Type Properties")]
    [SerializeField] private WoodType type;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<Renderer>();
        blinkRatio = lifeTime / 4 * blinkRatio;
        invisRatio = blinkRatio * invisRatio;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) meshRenderer.enabled = false;

        if (isPickedUp) return;

        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime / 2) ManageBlink();
    }

    public void PickedUp()
    {
        isPickedUp = true;
        meshRenderer.enabled = true;
    }

    public void Dropped()
    {
        isPickedUp = false;
        lifeTimer = 0;
        blinkTimer = 0;
    }

    private void ManageBlink()
    {
        blinkTimer += Time.deltaTime;
        if (blinkTimer > (blinkRatio - invisRatio)) meshRenderer.enabled = false;

        if (blinkTimer > blinkRatio)
        {
            meshRenderer.enabled = true;
            blinkTimer = 0;
        }

    }
}
