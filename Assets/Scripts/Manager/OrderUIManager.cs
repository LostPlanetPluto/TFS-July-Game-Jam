using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    public static OrderUIManager instance;

    [SerializeField] private UI_Order orderUIPF;

    [SerializeField] private UI_Order[] ordersUIObject;

    [Header("UI End Positions")]
    [SerializeField] private List<Transform> uiEndPositions = new List<Transform>();

    [Header("Shake Properties")]
    [SerializeField] private float intensity;
    [SerializeField] private float speed;
    private bool isShake = false;
    

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (!isShake) return;

        Shake();
    }

    public void SpawnOrderUI(Sprite icon)
    {
        for (int i = 0; i < ordersUIObject.Length; i++)
        {
            if (ordersUIObject[i] != null) continue;

            UI_Order order = Instantiate(orderUIPF, uiEndPositions[i]).GetComponent<UI_Order>();
            order.SetIcon(icon);

            ordersUIObject[i] = order;

            break;
        }
    }

    public void RemoveOrder(int index)
    {
        ordersUIObject[index].OrderFilled();
        ordersUIObject[index] = null;
    }

    private void Shake()
    {
        for (int i = 0; i < uiEndPositions.Count; i++)
        {
            Quaternion shake = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(Time.time * speed) * intensity));
            uiEndPositions[i].localRotation = shake;
        }
    }

    public void StartShake()
    {
        if (isShake) return;

        isShake = true;
    }

    public void StopShake()
    {
        if (!isShake) return;

        isShake = false;

        for (int i = 0; i < uiEndPositions.Count; i++)
        {
            uiEndPositions[i].localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void FadeUI()
    {
        for (int i = 0; i < ordersUIObject.Length; i++)
        {
            if (ordersUIObject[i] != null) ordersUIObject[i].GetComponent<Animator>().Play("Fade Out");
        }
    }
}