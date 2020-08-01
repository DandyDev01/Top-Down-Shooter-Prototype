using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected int amount;
    [SerializeField] private float despawnTime;
    private AudioSource audioSource;

    public delegate void OnInteractDelegate();
    public static event OnInteractDelegate OnInteractEvent;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            Interact();
    }

    protected virtual void Interact()
    {
        OnInteractEvent.Invoke();
        audioSource.Play();
    }

    private IEnumerator Despawn()
    {

        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
