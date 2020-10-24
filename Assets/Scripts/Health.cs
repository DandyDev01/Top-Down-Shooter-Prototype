using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    #region Variables
    [SerializeField] Slider healthBar;                 // the halth bar that displays the health to the user
    [SerializeField] protected float maxHealth = 5;    // max health the object can have
    [SerializeField] 
    protected GameObject deathEffect;                  // effect that is called to happen when Die() is called
    [SerializeField]
    protected ParticleSystem damageEffect;
    [SerializeField] bool destroyOnDeath = true;       // weather or not the object should be destroyed on death
    [SerializeField] bool destroyDeathEffect = false;  // weather or not the effect should be destroyed
    [SerializeField] 
    protected float timeToDestroyDeathEffect;          // time to wait before removing the effect from the scene
    [SerializeField] 
    protected AudioClip deathClip;                     // Sound to play when the Health Die() is called
    [SerializeField] Material white;
    [SerializeField] float swapTime = .1f;
    private Material defaultMaterial;
    private Color defaultColor;
    protected float currHealth;                          // keeps track of the object health
    protected AudioSource audioSource;                 // plays the sounds
    private SpriteRenderer sp;
    protected Collider2D collider;
    private static bool isTimePause;
    #endregion

    #region Delegates & Events
    // the object should take damage
    public delegate void OnTakeDamageDelegate();
    public event OnTakeDamageDelegate OnTakeDamageEvent;

    // the object dies
    public delegate void OnDeathDelegate();
    public event OnDeathDelegate OnDeathEvent;
    #endregion

	#region Unity Methods
	// Start is called before the first frame update
	protected virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        defaultColor = sp.color;
        defaultMaterial = sp.material;
        audioSource = GetComponent<AudioSource>();
        currHealth = maxHealth;
        healthBar.value = healthBar.maxValue;
        collider = GetComponent<Collider2D>();
    }
    #endregion

    #region Public Methods
    // make the currHealth go down
    public void TakeDamage(float amount)
    {
        // pause the game for a second when enemy hit
        if (gameObject.tag == "Enemy")
                StartCoroutine(PauseTime());

        if (gameObject.tag == "Player")
            CameraShake.instance.StartShake(0.2f, 0.5f);
       
        currHealth -= amount;

        audioSource.Play();

        // blood splatter
        if(damageEffect != null)
            damageEffect.Play();

        StartCoroutine(MaterialSwap());

        if(OnTakeDamageEvent != null)
            OnTakeDamageEvent.Invoke();
        
        UpdateHealthUI();

        if (currHealth <= 0)
            Die();
    }

    // health will be added to the object
    public void Heal(float ammount)
    {
        currHealth += ammount;

        if (currHealth > maxHealth)
            currHealth = maxHealth;

        UpdateHealthUI();
    }
    #endregion

    #region Private Methods
    // update the healthBar to show the current health of the object
    private void UpdateHealthUI()
    {
        float percent = currHealth / maxHealth; 
        healthBar.value = percent * healthBar.maxValue;
    }

    // pause the time for a fraction of a second 
    private static IEnumerator PauseTime()
    {
        if (!isTimePause)
        {
            isTimePause = true;
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(0.05f);
            Time.timeScale = 1;
            yield return new WaitForSeconds(3f);
        }
        isTimePause = false;
    }

    protected IEnumerator MaterialSwap()
    {
        collider.enabled = false;
        for(int i = 0; i < 2; i++)
        {
            sp.color = Color.white;
            sp.material = white;
            yield return new WaitForSeconds(swapTime);
            sp.color = defaultColor;
            sp.material = defaultMaterial;
            yield return new WaitForSeconds(swapTime);
        }
        collider.enabled = true;
        audioSource.Stop();
    }

    // the object has no health left kill them
    protected virtual void Die()
    {
        audioSource.clip = deathClip;
        audioSource.Play();
        GameObject deathEffect = null;

        // make death effect happen
        if (this.deathEffect != null)
            deathEffect = Instantiate(this.deathEffect, transform.position, Quaternion.identity);

        // destroy death effect after some time
        if (destroyDeathEffect)
            Destroy(deathEffect, timeToDestroyDeathEffect);
        

        // destroy the object
        if (destroyOnDeath)
            Destroy(gameObject, .1f);

        if (OnDeathEvent != null)
            OnDeathEvent.Invoke();

    }
	#endregion

	#region Getters & Setters
    public float GetCurrHealth() { return currHealth; }
    public float GetMaxHealth() { return maxHealth; }
	#endregion
}
