using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : Interactable
{

    Health health;
    protected override void Interact()
    {
        health = FindObjectOfType<Player>().GetComponent<Health>();
        health.Heal(health.GetMaxHealth() / 2);
        base.Interact();
        Destroy(gameObject, .1f);
    }
}
