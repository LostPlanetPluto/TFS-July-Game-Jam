using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_HealthComponent
{
    public float health {  get; set; }
    public void OnTakeDamage(float damage);
}
