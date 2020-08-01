using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : Gun
{

    private Animator animator;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override IEnumerator Shoot()
    {
        if(Player.ammo > 0)
        {
            InvokeEvent();
            animator.SetBool("reloading", true);
            Player.ammo--;
            audioSource.Play();
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(.7f);
            animator.SetBool("reloading", false);
        }
    }
}
