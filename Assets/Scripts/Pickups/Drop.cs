using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Drop : MonoBehaviour
{

    [SerializeField] GameObject[] drops = null;
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDeathEvent += SpawnDrop;
    }

    void SpawnDrop()
    {
        GameObject drop = null;
        int randomIndex = Random.Range(0, drops.Length + 5);
        
        if (randomIndex >= drops.Length)
            return;
        
        drop = drops[randomIndex];
        Instantiate(drop, transform.position, Quaternion.identity);
    }


}
