using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpread : Gun
{
    [SerializeField] int numOfProjectile = 3;
    [SerializeField] float spreadAngleDeg = 45;

    #region Unity Methods
    // Start is called before the first frame update
    new void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Scatter(transform);
        Destroy(gameObject, .1f);
    }

    // shoot the projectiles with the appropriate angles
    protected void Scatter(Transform scatterPoint)
    {
        shootPoint = transform;

        // placement of each projectile for incrementation
        float angle = spreadAngleDeg / numOfProjectile;       
        float newAngle = scatterPoint.rotation.eulerAngles.z; // angle to shoot the projectile
        // correct the shoot angle so that one projectile is at the center
        float offSet = (angle * (numOfProjectile / 2));       

        for (int i = 0; i < numOfProjectile; i++)
        {
            GameObject obj = Instantiate(bulletPrefab, scatterPoint.position, 
                Quaternion.Euler(0, 0, newAngle - offSet));
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.AddForce(obj.transform.up * bulletForce, ForceMode2D.Impulse);
            newAngle += angle;
        }
    }
}
