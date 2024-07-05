using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image iconImage;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (!movingOver) return;

        Debug.Log(transform.localPosition.x);

        moveTimer += Time.deltaTime;

        float curveTest = curve.Evaluate( moveTimer / duration );

        float xPosition = Mathf.Lerp(startingXPosition, 0, curveTest);

        Debug.Log(xPosition);

        Vector3 newPos = new Vector3(xPosition, 0, -11);

        transform.localPosition = newPos;

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
        startingXPosition = transform.position.x;

        yield return 0;

        movingOver = true;
    }

    public void SetIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }
}