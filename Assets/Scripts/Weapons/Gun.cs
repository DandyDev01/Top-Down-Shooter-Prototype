using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    #region Variables
    [SerializeField] protected GameObject flash;          // flash that shows when gun is shot
    [SerializeField] protected GameObject bulletPrefab;   // the bullet the hit enemies
    [SerializeField] protected Transform shootPoint;      // where the bullet is shot from
    [SerializeField] protected float bulletForce;         // amount of force that is applied to the bullet
    [SerializeField] protected float fireRate;            // how quickly the gun can shoot
    protected AudioSource audioSource;
    protected float lastShot;
    #endregion

    #region Delegates & Events
    public delegate void OnShootDelegate();
    public static event OnShootDelegate OnShootEvent;
    #endregion

	#region Unity Methods
	// Start is called before the first frame update
	protected virtual void Start()
    {
        flash.SetActive(false); // make sure the flash is not showing at the start
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;
            StartCoroutine(Shoot());
        }
    }
	#endregion

	#region public methods
    public void SetBulletPrefab(GameObject bullet)
    {
        bulletPrefab = bullet;
    }

    public GameObject GetBulletPrefab() { return bulletPrefab; }

    public void InvokeEvent()
    {
        OnShootEvent.Invoke();
    }
	#endregion

	#region Private Methods
	// shoots the players gun
	protected virtual IEnumerator Shoot()
    {
        if(Player.ammo > 0)
        {
            Player.ammo--;
            audioSource.Play();
            OnShootEvent.Invoke();
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
            flash.SetActive(true);
            yield return new WaitForSeconds(.1f);
            flash.SetActive(false); // make sure the flash is only shown for a little
        }
    }
	#endregion
}
