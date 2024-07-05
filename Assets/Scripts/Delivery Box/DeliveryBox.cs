using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryBox : MonoBehaviour
{
    [SerializeField] private Transform[] itemPoints = new Transform[3];
    [SerializeField] private Wood[] itemsHeld = new Wood[3];

    [Header("Drop Properties")]
    [SerializeField] private float dropHeight;
    [SerializeField] private float dropForce;
    [SerializeField] private float dropRadius;

    public bool CheckAvailableSpace()
    {
        if (itemsHeld[itemsHeld.Length - 1] != null) return false;
        else return true;
    }

    public void TakeWood(ref Wood wood)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] != null) continue;

            itemsHeld[i] = wood;
            wood.transform.parent = itemPoints[i].transform;
            wood.transform.localPosition = Vector3.zero;
            wood.transform.rotation = itemPoints[i].transform.rotation;
            break;
        }

        StartCoroutine(CheckDelivery());
    }

    IEnumerator CheckDelivery()
    {
        yield return 0;

        if (itemsHeld[itemsHeld.Length - 1] != null)
        {
            if (CheckOrders())
            {
                for (int i = 0; i < itemsHeld.Length; i++)
                {
                    Destroy(itemsHeld[i].gameObject);
                    itemsHeld[i] = null;
                }
            }

            else DropObjects();
        }
    }

    private bool CheckOrders()
    {
        int birchCount = 0;
        int mapleCount = 0;
        int spruceCount = 0;

        for (int i = 0;  i < itemsHeld.Length;i++)
        {
            switch (itemsHeld[i].type) 
            {
                case Wood.WoodType.Birch:
                    birchCount += 1;
                    break;
                case Wood.WoodType.Maple:
                    mapleCount += 1;
                    break;
                case Wood.WoodType.Spruce:
                    spruceCount += 1;
                    break;
            }
        }

        return DeliveryManager.instance.CheckOrders(birchCount, mapleCount, spruceCount);
    }

    private void DropObjects()
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i].transform.position += Vector3.up * dropHeight;
            itemsHeld[i].transform.parent = null;
            itemsHeld[i].Dropped();

            itemsHeld[i] = null;
        }

        Collider[] wood = Physics.OverlapSphere(transform.position + Vector3.up * dropHeight, dropRadius);

        if (wood.Length == 0) return;

        foreach (Collider collider in wood)
        {
            Rigidbody rb = collider.GetComponentInParent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(dropForce, transform.position + Vector3.up * dropHeight, dropRadius);        
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + Vector3.up * dropHeight, dropRadius);
    }
}
