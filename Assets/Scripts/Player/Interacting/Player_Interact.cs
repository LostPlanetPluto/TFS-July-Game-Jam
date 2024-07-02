using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    [Header("Interact Properties")]
    [SerializeField] private Transform interactPoint;
    [SerializeField] private float radius;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Interact();
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
}
