using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Break : MonoBehaviour
{

    [SerializeField] Sprite[] damagedSprites;   // Sprites to cycle through based on the health
    private SpriteRenderer sp;          
    private Health health;

    // MAKE WORK FOR ALL NUMBERS

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        sp = GetComponent<SpriteRenderer>();

        health.OnTakeDamageEvent += UpdateSprite;
    }

    // change the sprite of the object to coinside with the index related to the 
    // health
    private void UpdateSprite()
    {
        int newHealth = (int)health.GetCurrHealth() % damagedSprites.Length;
        sp.sprite = damagedSprites[Mathf.Clamp(newHealth, 0, 3)];
    }

}
