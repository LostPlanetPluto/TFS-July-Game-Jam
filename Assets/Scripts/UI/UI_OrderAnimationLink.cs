using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OrderAnimationLink : MonoBehaviour
{
    public void Death()
    {
        GetComponentInParent<UI_Order>().Death();
    }
}
