using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [Header("Interact Properties")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float radius;

    [Header("Pickup Properties")]
    [SerializeField] private int inventorySize;
    [SerializeField] private Transform[] pickUpPoints = new Transform[3];
    [SerializeField] private Wood[] woodStacks = new Wood[3];

    private Wood.WoodType woodTypeStacked = Wood.WoodType.None;

    private void Awake()
    {
        inventorySize = pickUpPoints.Length;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Interact();

        if (Input.GetKeyDown(KeyCode.E)) Pickup();

        if (Input.GetKeyDown(KeyCode.R)) Drop();
    }

    public void Interact()
    {
        Collider[] objects = Physics.OverlapSphere(interactPoint.position, radius);

        // Return if no objects are found
        if (objects.Length == 0) return;

        // Check to see if objects can be damaged
        I_HealthComponent healthComp = objects[0].GetComponentInParent<I_HealthComponent>();

        if (healthComp != null) healthComp.OnTakeDamage(10);
    }

    private void Pickup()
    {
        Collider[] objects = Physics.OverlapSphere(interactPoint.position, radius);

        if (objects.Length == 0) return;

        Wood wood = objects[0].GetComponentInParent<Wood>();

        if (wood == null) return;

        if (woodTypeStacked != Wood.WoodType.None && woodTypeStacked != wood.type) return;

        for (int i = 0; i < inventorySize; i++)
        {
            if (woodStacks[i] != null) continue;

            wood.PickedUp();
            woodStacks[i] = wood;
            wood.transform.parent = pickUpPoints[i].transform;
            wood.transform.localPosition = Vector3.zero;
            wood.transform.rotation = pickUpPoints[i].transform.rotation;
            woodTypeStacked = wood.type;

            break;
        }
    }

    private void Drop()
    {
        for (int i = 0; i < inventorySize; i++) 
        {
            if (woodStacks[i] == null) break;

            woodStacks[i].Dropped();
            woodStacks[i].transform.parent = null;
            woodStacks[i] = null;
        }

        woodTypeStacked = Wood.WoodType.None;
    }
}
