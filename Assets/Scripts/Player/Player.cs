using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
    public static int ammo = 500;
    public static int coins = 100;
    //public static int revives = 0;
    [SerializeField]
    public Transform holdPoint;
    [SerializeField] Light2D light;
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
        if (ammo < 10)
            ammo = 50;
        UIController.instance.UpdateCoinTxt();
        Gun currGun = gun.GetComponent<Gun>();
        currGun.SetBulletPrefab(newBullet);
    }

    public void GunChange(GameObject newGun)
    {
        if (ammo < 10)
            ammo = 50;
        UIController.instance.UpdateCoinTxt();
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

        light.transform.parent = null;

        gameObject.SetActive(false);

    }

    public IEnumerator RevivePlayer()
    {
        yield return new WaitForSeconds(5f);
        //Heal(maxHealth);
        //revives--;
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
        light.transform.parent = transform;
        for(int i = 3; i > 0; i--)
        {
            StartCoroutine(MaterialSwap());
            yield return new WaitForSeconds(1f);
            i--;
        }
        collider.enabled = true;
        
    }

    public void SetSpeed(float speed) { speed = speed; }

    public void SetAmmo(int i) 
    {
        if(coins >= 5)
        {
            ammo += i;
            coins -= 5;
            UIController.instance.UpdateCoinTxt();
            UIController.instance.UpdateAmmoTxt();
        }
    }


}
