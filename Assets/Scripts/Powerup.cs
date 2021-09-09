using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Powerup : Interactable
{
    public int healthAmount;
    public bool overheal = true;

    public override void OnInteract()
    {
        Player.instance.Heal(healthAmount, overheal: overheal);
        Destroy(gameObject);
    }

}
