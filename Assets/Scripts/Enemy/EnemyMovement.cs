using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	#region Variables
	[SerializeField] protected float speed;   // how fast the enemy will move
    float angle;                              // angle to look at 
    Vector3 lookDir;                          // direction the enemy will look to see the Player
    protected Transform player;               // the players transfrom   
    Rigidbody2D rb;                 
	#endregion

	#region Unity Methods
	// Start is called before the first frame update
	protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(Player.instance != null)
            player = Player.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
    }
	#endregion

	#region Private Methods
    // make the enemy face the player at all times
	private void Rotation()
    {
        if(player != null)
        {
            lookDir = player.position - transform.position;
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
            rb.rotation = angle;
        }
    }

    // move enemy towards the player
    protected virtual void Movement()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position,
                   speed * Time.deltaTime);
        }
    }
    #endregion

    public void SetSpeed(float newSpeed) { this.speed = newSpeed; }
    public float GetSpeed() { return speed; }
}
