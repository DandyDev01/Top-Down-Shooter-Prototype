using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] float speed;               // speed the player moves at
    [SerializeField] Camera cam = null;         // main camera
    private Vector2 movement;                   // user input for movement
    private Vector2 mousePos;                   // curr mouse position
    private Rigidbody2D rb;                     
	#endregion

	#region Unity Methods
	// Start is called before the first frame update
	void Start()
    {
        // sets the rigidbody and collider to the ones that we added earlier
        // Note: if we did not add them then they would be set to null
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // set x & y to 1 or -1 and allows us to use the keyboard for input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // go from pixels to world units
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    #endregion

    #region Private Methods
    private void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        // make the player look in the direction of the mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }
	#endregion

    public void SetSpeed(float newSpeed) { this.speed = newSpeed; }
    public float GetSpeed() { return speed; }
}
