using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : ProjectileSpread
{

    #region Unity Methods
    // Start is called before the first frame update
    protected override void Start()
    {
        flash.SetActive(false); // make sure the flash is not showing at the start
        audioSource = GetComponent<AudioSource>();
    } 

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;
            StartCoroutine(Shoot());
        }
    }
    #endregion

    protected override IEnumerator Shoot()
    {
        if (Player.ammo > 0)
        {
            InvokeEvent();
            Player.ammo--;
            Scatter(shootPoint);
            audioSource.Play();
            flash.SetActive(true);
            yield return new WaitForSeconds(.1f);
            flash.SetActive(false);
        }
    }
}
