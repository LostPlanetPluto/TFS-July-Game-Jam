using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHolder : MonoBehaviour
{
    [Header("Log Holder Type")]
    [SerializeField] private Wood.WoodType woodType;

    [Header("Wood Points and holder")]
    [SerializeField] private Transform[] itemPoints = new Transform[4];
    [SerializeField] private Wood[] itemsHeld = new Wood[4];


    public bool CheckAvailableSpace()
    {
        if (itemsHeld[itemsHeld.Length - 1] != null) return false;
        else return true;
    }

    public void TakeWood(ref Wood wood)
    {
        if (wood.type != woodType) return;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] != null) continue;

            itemsHeld[i] = wood;
            wood.transform.parent = itemPoints[i].transform;
            wood.transform.localPosition = Vector3.zero;
            wood.transform.rotation = itemPoints[i].transform.rotation;
            break;
        }
    }

    public Wood PickUpWood()
    {
        Wood item = null;

        for (int i = itemsHeld.Length - 1; i >= 0; i--)
        {
            if (itemsHeld[i] == null) continue;

            item = itemsHeld[i];
            itemsHeld[i] = null;
            break;
        }

        return item;
    }

    public Wood.WoodType GetWoodType()
    {
        return woodType;
    }
}
