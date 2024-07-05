using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Pauseable
{
    public static DeliveryManager instance;

    [Header("Order Properties")]
    public List<SO_Order> orderTypes = new List<SO_Order>();
    public int[] orders;
    private int orderCount = 0;

    [Header("Order Spawning Properties")]
    [SerializeField] private float spawnTime = 10f;
    [SerializeField] private int maxOrder;
    private float spawnTimer = 0;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip orderFilledClip;
    [SerializeField] private AudioClip orderUnfilledClip;
    private void Awake()
    {
        instance = this;
        orders = new int[maxOrder];
        for (int i = 0; i < maxOrder; i++) orders[i] = -1;
    }

    private void Update()
    {
        if (OrderUIManager.instance != null)
        {
            if (orderCount == maxOrder) OrderUIManager.instance.StartShake();
            else OrderUIManager.instance.StopShake();
        }

        if (isPaused) return;

        SpawnBehaviour();
    }

    private void SpawnBehaviour()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime)
        {
            orderCount = 0;
            
            int randomIndex = Random.Range(0, orderTypes.Count);

            for (int i = 0; i < orders.Length; i++)
            {
                orderCount++;

                if (orders[i] == -1)
                {
                    orders[i] = randomIndex;
                    break;
                }
            }

            orderCount++;

            // GAME OVER LOGIC HERE
            if (orderCount > maxOrder) FindAnyObjectByType<UI_Fader>().FadeToNextScene("End Scene");
                
                

            if (OrderUIManager.instance != null)
            {
                // Always spawn UI
                OrderUIManager.instance.SpawnOrderUI(orderTypes[randomIndex].icon);

            }

            spawnTimer = 0;
        }
    }

    public bool CheckOrders(int birch, int maple, int spruce)
    {
        int orderToRemove = -1;

        for (int i = 0; i < orders.Length; i++)
        {
            if (orders[i] == -1) continue;

            if (orderTypes[orders[i]].CheckOrder(birch, maple, spruce))
            {
                orderToRemove = i;

                if (OrderUIManager.instance != null)
                {
                    OrderUIManager.instance.RemoveOrder(i);
                }

                if (GameManager.instance != null) GameManager.instance.AddPoint();
                if (AudioManager.instance != null) AudioManager.instance.PlaySFX(orderFilledClip);
                break;
            }
        }

        if (orderToRemove != -1)
        {
            orders[orderToRemove] = -1;
            orderCount--;
            return true;
        }

        else
        {
            if (AudioManager.instance != null) AudioManager.instance.PlaySFX(orderUnfilledClip);
            return false;
        }
    }
}