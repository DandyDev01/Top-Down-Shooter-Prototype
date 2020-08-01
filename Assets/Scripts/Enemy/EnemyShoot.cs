using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Gun
{

    [SerializeField] float range = 5;
    Transform player;

    protected override void Start()
    {
        base.Start();
        player = Player.instance.transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;
            StartCoroutine(Shoot());
        }
    }

    protected override IEnumerator Shoot()
    {
        if(Vector2.Distance(player.position, transform.position) < range)
        {
            audioSource.Play();
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
            flash.SetActive(true);
            yield return new WaitForSeconds(.1f);
            flash.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
