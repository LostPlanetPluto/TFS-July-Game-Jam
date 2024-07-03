using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Pauseable
{
    public static DeliveryManager instance;

    [Header("Order Properties")]
    public List<SO_Order> orderTypes = new List<SO_Order>();
    public List<int> orders = new List<int>();

    [Header("Order Spawning Properties")]
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private int maxOrder;
    private float spawnTimer = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isPaused) return;

        SpawnBehaviour();
    }

    private void SpawnBehaviour()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime && orders.Count <= maxOrder)
        {
            orders.Add(Random.Range(0, orderTypes.Count));


                // GAME OVER LOGIC HERE
            if (orders.Count > maxOrder) Debug.Log("Game over dawg");
                
                
                
                
            if (OrderUIManager.instance != null) OrderUIManager.instance.SpawnOrderUI();

            spawnTimer = 0;
        }
    }

    public bool CheckOrders(int birch, int maple, int spruce)
    {
        if (orders.Count == 0)
        {
            Debug.Log("There are no orders");
            return false;
        }

        int orderToRemove = -1;

        foreach (int order in orders)
        {
            if (orderTypes[order].CheckOrder(birch, maple, spruce))
            {
                orderToRemove = order;

                if (OrderUIManager.instance != null) OrderUIManager.instance.RemoveOrder(order);

                break;
            }
        }

        if (orderToRemove != -1)
        {
            orders.Remove(orderToRemove);
            return true;
        }

        else return false;
    }
}