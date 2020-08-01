using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] float grabTime = 3;
    [SerializeField] Sprite triggeredSprite = null;
    [SerializeField] bool getPlayer;
    [SerializeField] bool getEnemeis;
    private SpriteRenderer sp;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(GrabObject(collision));
    }

    //
    protected virtual IEnumerator GrabObject( Collision2D collision)
    {
        audioSource.Play();
        Sprite temp = sp.sprite;
        sp.sprite = triggeredSprite;
        float speed = 0;
        EnemyMovement em = collision.gameObject.GetComponent<EnemyMovement>();
        if (em != null && getEnemeis)
        {
            speed = em.GetSpeed();
            em.SetSpeed(0);
        }

        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null && getPlayer)
        {
            speed = pc.GetSpeed();
            pc.SetSpeed(0);
        }

        boxCollider2D.enabled = false;

        yield return new WaitForSeconds(grabTime);

        if (em != null)
            em.SetSpeed(speed);

        if (pc != null)
            pc.SetSpeed(speed);

        sp.sprite = temp;

        yield return new WaitForSeconds(2f);

        boxCollider2D.enabled = true;
        
    }
}
