using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class UI_Order : MonoBehaviour
{
    private Animator anim;

    [Header("Moving Animation Properties")]
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float duration = 3;
    private bool movingOver = false;
    private float startingXPosition;
    private float moveTimer = 0;

    [Header("Icon Properties")]
    [SerializeField] private SpriteRenderer iconSR;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (!movingOver) return;

        moveTimer += Time.deltaTime;

        float curveTest = curve.Evaluate( moveTimer / duration );

        transform.localPosition = Vector3.Lerp(new Vector3(startingXPosition, 0, 0), Vector3.zero, curveTest);

        if (moveTimer > duration) movingOver = false;
    }

    public void OrderFilled()
    {
        anim.Play("Filled");
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void MoveOver()
    {
        StartCoroutine(MoveOverHelper());
    }

    IEnumerator MoveOverHelper()
    {
        yield return 0;

        movingOver = true;
        startingXPosition = transform.localPosition.x;
    }

    public void SetIcon(Sprite icon)
    {
        iconSR.sprite = icon;
    }
}