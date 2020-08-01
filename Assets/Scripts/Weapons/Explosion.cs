using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Damage
{
    [SerializeField] GameObject explosionEffect = null;       // visual effect
    [SerializeField] float timeToDestroyExplosionEffect = 1;  // wait to destroy vfx
    [SerializeField] float damageRadius;                      // how close an obj should be for damage
    AudioSource audioSource;


    protected override void TakeHealth(Collision2D collision)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        GameObject vfx = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // get all objects with a collider within damage range
        RaycastHit2D[] objects = Physics2D.CircleCastAll(transform.position, damageRadius,
            Vector2.zero);

        // for all objects within range apply damage if there is health
        foreach (RaycastHit2D obj in objects)
        {
            Health h = obj.transform.GetComponent<Health>();
            if (h != null)
                h.TakeDamage(damage);

        }

        Destroy(vfx, timeToDestroyExplosionEffect);
        Destroy(gameObject, .1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
