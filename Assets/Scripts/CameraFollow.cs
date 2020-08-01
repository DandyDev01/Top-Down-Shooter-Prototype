using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform playerPos = null;

    // Update is called once per frame
    void Update()
    {
        if(playerPos != null)
        {
            // follow tha player around on the game scene
            transform.position = new Vector3(playerPos.position.x, playerPos.position.y,
                transform.position.z);
        }

    }
}
