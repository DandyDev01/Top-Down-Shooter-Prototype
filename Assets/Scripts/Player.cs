using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Health
{
    #region Singleton
    public static Player instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    #endregion

    #region Variables
    public static int ammo = 50;
    public static int coins = 0;
    [SerializeField]
    public Transform holdPoint;
    public GameObject gun;
    public GameObject bullet;
    #endregion

    #region Delegates & Events
    // the object dies
    public new delegate void OnDeathDelegate();
    public new event OnDeathDelegate OnDeathEvent;
    #endregion

    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        gun = transform.GetChild(0).GetChild(0).gameObject;
        bullet = gun.GetComponent<Gun>().GetBulletPrefab();
    }
    #endregion

    public void BulletChange(GameObject newBullet)
    {
        Gun currGun = gun.GetComponent<Gun>();
        currGun.SetBulletPrefab(newBullet);
    }

    public void GunChange(GameObject newGun)
    {
        Transform oldGun = holdPoint.GetChild(0);
        oldGun.parent = null;
        Destroy(oldGun.gameObject);

        gun = Instantiate(newGun, holdPoint);

    }

    protected override void Die()
    {
        audioSource.clip = deathClip;
        audioSource.Play();

        // make death effect happen
        if (deathEffect != null)
            deathEffect = Instantiate(this.deathEffect, transform.position,
                Quaternion.identity);

        if (OnDeathEvent != null)
            OnDeathEvent.Invoke();

        gameObject.SetActive(false);


    }

    public void RevivePlayer()
    {
        gameObject.SetActive(true);
    }

    public void SetSpeed(float speed) { speed = speed; }
}
