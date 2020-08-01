using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    #region Variables
    [SerializeField] Slider healthBar;                 // the halth bar that displays the health to the user
    [SerializeField] float maxHealth = 5;              // max health the object can have
    [SerializeField] 
    protected GameObject deathEffect;                  // effect that is called to happen when Die() is called
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
    private float currHealth;                          // keeps track of the object health
    protected AudioSource audioSource;                 // plays the sounds
    private SpriteRenderer sp;
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
    }
    #endregion

    #region Public Methods
    // make the currHealth go down
    public void TakeDamage(float amount)
    {
        currHealth -= amount;

        audioSource.Play();

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

    private IEnumerator MaterialSwap()
    {
        for(int i = 0; i < 2; i++)
        {
            sp.color = Color.white;
            sp.material = white;
            yield return new WaitForSeconds(swapTime);
            sp.color = defaultColor;
            sp.material = defaultMaterial;
            yield return new WaitForSeconds(swapTime);
        }
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
