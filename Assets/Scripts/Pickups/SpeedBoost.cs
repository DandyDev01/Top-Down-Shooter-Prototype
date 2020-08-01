using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Interactable
{
    [SerializeField] float duration;
    [SerializeField] float speedModifier;

    protected override void Interact()
    {
        base.Interact();

        StartCoroutine(ChangeSpeed());
    }

    private IEnumerator ChangeSpeed()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        // player already has a speed boost
        if (player.GetSpeed() > 6)
            yield break;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        gameObject.GetComponent<Collider2D>().enabled = false;
        player.SetSpeed(player.GetSpeed() * speedModifier);
        yield return new WaitForSeconds(duration);
        player.SetSpeed(player.GetSpeed() / speedModifier);
        Destroy(gameObject);

    }
}
