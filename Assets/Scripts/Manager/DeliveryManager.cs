using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager instance;

    public List<SO_Order> orders = new List<SO_Order>();

    private void Awake()
    {
        instance = this;
    }

    public bool CheckOrders(int birch, int maple, int spruce)
    {
        if (orders.Count == 0)
        {
            Debug.Log("There are no orders");
            return false;
        }

        SO_Order orderToRemove = null;

        foreach (SO_Order order in orders)
        {
            if (order.CheckOrder(birch, maple, spruce))
            {
                orderToRemove = order;
            }
        }

        if (orderToRemove != null)
        {
            orders.Remove(orderToRemove);
            return true;
        }

        else return false;
    }
}