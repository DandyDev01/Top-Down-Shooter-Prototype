using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Object that applies damage to the Health script that is on other objects
 */
public class Damage : MonoBehaviour
{
	#region Variables
	[SerializeField] protected float damage;    // how much damage will be applied
    [SerializeField] LayerMask damageLayer;     // layer that damage can be applied to
    [SerializeField] bool destroyOnCollision;   // weather or not the objec should be destroyed on a collision
    protected new Collider2D collider;          // collider on the object
	#endregion

	#region Unity Methods
	private void Start()
    {
        collider = GetComponent<Collider2D>();
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collider.IsTouchingLayers(damageLayer))
            TakeHealth(collision);
    }
	#endregion

	#region Private Methods
    // apply damage to some object that is collided with
	protected virtual void TakeHealth(Collision2D collision)
    {
        /* Update to damage script damageLayer*/
        Health h = collision.gameObject.GetComponent<Health>();
        if (h != null)
        {
            h.TakeDamage(damage);
        }

        if (destroyOnCollision)
            Destroy(gameObject, .1f);
    }
	#endregion
}
