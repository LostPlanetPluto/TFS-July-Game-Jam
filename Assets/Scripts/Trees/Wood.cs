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

    public enum WoodType { Birch, Maple, Spruce, None}
    [Header("Wood Type Properties")]
    public WoodType type;


    private Rigidbody rb;
    private Collider col;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<Renderer>();
        blinkRatio = lifeTime / 4 * blinkRatio;
        invisRatio = blinkRatio * invisRatio;
    
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (isPickedUp) return;

        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime / 2) ManageBlink();

        if (lifeTimer > lifeTime) Destroy(gameObject);
    }

    public void PickedUp()
    {
        isPickedUp = true;
        meshRenderer.enabled = true;

        // Disable Rigidbody and collider;
        rb.isKinematic = true;
        col.enabled = false;
    }

    public void Dropped()
    {
        isPickedUp = false;
        lifeTimer = 0;
        blinkTimer = 0;

        // Re enable Rigidbody and collider
        rb.isKinematic = false;
        col.enabled = true;
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
