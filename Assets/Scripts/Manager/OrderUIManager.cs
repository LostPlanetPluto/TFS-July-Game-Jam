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

    private void Awake()
    {
        instance = this;
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
}
