using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : Interactable
{
     protected override void Interact()
    {
        Player.coins += amount;
        base.Interact();
        Destroy(gameObject, .1f);
    }
}
