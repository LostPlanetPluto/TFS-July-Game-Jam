using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderUIManager : MonoBehaviour
{
    public static OrderUIManager instance;

    [SerializeField] private UI_Order orderUIPF;

    [SerializeField] private List<UI_Order> ordersUIObject = new List<UI_Order>();

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
        if (ordersUIObject.Count > uiEndPositions.Count) return;

        for (int i = 0; i < uiEndPositions.Count; i++)
        {
            if (uiEndPositions[i].childCount > 0) continue;

            UI_Order order = Instantiate(orderUIPF, uiEndPositions[i]).GetComponent<UI_Order>();
            order.SetIcon(icon);

            ordersUIObject.Add(order);

            break;
        }
    }

    public void RemoveOrder(int index)
    {
        ordersUIObject[index].OrderFilled();
        Debug.Log($"The object that is being filled is at: {index}");
        ordersUIObject.RemoveAt(index);

        for (int i = index; i < ordersUIObject.Count; i++)
        {
            ordersUIObject[i].transform.parent = uiEndPositions[i].transform;
            ordersUIObject[i].MoveOver();
        }
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
        isShake = true;
    }

    public void StopShake()
    {
        isShake = false;

        for (int i = 0; i < uiEndPositions.Count; i++)
        {
            uiEndPositions[i].localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}