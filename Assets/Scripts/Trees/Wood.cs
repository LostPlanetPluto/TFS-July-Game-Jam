using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    [Header("Spawn Wood Properties")]
    [SerializeField] private bool isPickedUp = false;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private float lifeTime;
    [SerializeField] private float blinkTime;
    [SerializeField] private float invisTime;
    private float lifeTimer = 0;

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (isPickedUp) return;

        lifeTimer += Time.deltaTime;

        if (lifeTimer > lifeTime) Destroy(gameObject);
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
    }
}
