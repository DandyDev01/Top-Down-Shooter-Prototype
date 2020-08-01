using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Interactable
{
    protected override void Interact()
    {
        Player.ammo += amount;
        base.Interact();
        Destroy(gameObject, .1f);
    }
}
