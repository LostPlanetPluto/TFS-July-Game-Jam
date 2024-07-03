using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Orders", menuName = "New Order")]
public class SO_Order : ScriptableObject
{
    public int birchCount;
    public int mapleCount;
    public int spruceCount;

    public Sprite icon;

    public bool CheckOrder(int birch, int maple, int spruce)
    {
        if (birch == birchCount && maple == mapleCount && spruce == spruceCount) return true;
        else return false;
    }
}
