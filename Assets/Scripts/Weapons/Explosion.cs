using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Damage
{
    [SerializeField] GameObject explosionEffect = null;       // visual effect
    [SerializeField] GameObject flash;
    [SerializeField] GameObject[] explosionBoomText;
    [SerializeField] float timeToDestroyExplosionEffect = 1;  // wait to destroy vfx
    [SerializeField] float damageRadius;                      // how close an obj should be for damage
    [SerializeField] Vector2 cameraShake;
    AudioSource audioSource;


    protected override void TakeHealth(Collision2D collision)
    {
        Flash f = flash.GetComponent<Flash>();
        Instantiate(flash, transform.position, Quaternion.identity);
        // CameraShake.instance.ShakeCamera(10, 0.05f);
        CameraShake.instance.StartShake(cameraShake.x, cameraShake.y);
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        GameObject explosionEffect = explosionBoomText[Random.Range(0,
            explosionBoomText.Length)];
        GameObject vfx = Instantiate(this.explosionEffect, transform.position, Quaternion.identity);
        GameObject vfx1 = Instantiate(explosionEffect, transform.position, Quaternion.identity);

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
        Destroy(vfx1, timeToDestroyExplosionEffect);
        Destroy(gameObject, .1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
