using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Singlton
    public static CameraFollow instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion


    [SerializeField] Transform playerPos = null;
    [SerializeField] float dampDistance;
    [SerializeField] float maxDistance = 3;
    [SerializeField] Vector2 minPos;
    [SerializeField] Vector2 maxPos;
    Camera cam;
    Vector3 mousePos;

    private void Start()
    {
        cam = GetComponent<Camera>();
       // playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (playerPos != null)
        {
            
            Vector3 targetPos = mousePos;
            mousePos.z = 10;

            targetPos.x = Mathf.Clamp(targetPos.x, playerPos.position.x - maxDistance, 
                playerPos.position.x + maxDistance);
            targetPos.y = Mathf.Clamp(targetPos.y, playerPos.position.y - maxDistance,
                playerPos.position.y + maxDistance);

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

            // follow tha player around on the game scene
            transform.position = Vector3.Lerp(transform.position, targetPos, 
                dampDistance * Time.deltaTime);
           
        }
    }
}
