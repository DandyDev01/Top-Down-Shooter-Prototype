using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : EnemyMovement
{
    [SerializeField] ParticleSystem dust;
    [SerializeField] float timeUnderGround = 3;
    [SerializeField] float underSpeedMultiplier = 2;
    private new Collider2D collider2D;
    private SpriteRenderer sp;

    private new void Start()
    {
        base.Start();
        collider2D = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(ComeUp());
    }

    // make the enemy go under the ground and not be hittable
    private IEnumerator GoUnder()
    {
        // turn off collider
        collider2D.enabled = false;
        // change the opacity to zero
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
        speed *= underSpeedMultiplier;
        yield return new WaitForSeconds(timeUnderGround);
        StartCoroutine(ComeUp());
    }

    // make the enemy come up from under ground
    private IEnumerator ComeUp()
    {
        // enable collider
        collider2D.enabled = true;
        // change the opacity to 255
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
        speed /= underSpeedMultiplier;
        yield return new WaitForSeconds(timeUnderGround);
        StartCoroutine(GoUnder());
    }
}
