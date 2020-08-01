using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    [SerializeField] int numOfEnemies = 0;            // number of enemies that the spawner should spawn
    [SerializeField] float timeBetweenSpawns = 3;     // time between the enemy sapwns
    [SerializeField] GameObject[] enemyTypes = null;  // different enemys that will be spawned

    #region Events & Delegates
    public delegate void FinishSpawningDelegate();
    public event FinishSpawningDelegate FinishSpawningEvent;
    #endregion

    private void Start()
    {
        Player.instance.OnDeathEvent += StopSpawning;
    }

    // spawns a specified number of enemies with a specified amount of time between them
    public IEnumerator Spawn()
    {
        for (int i = 0; i < numOfEnemies * Wave.WaveNum; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Instantiate(SpawnRandomEnemy(), transform.position, Quaternion.identity);
        }

        if(FinishSpawningEvent != null)
            FinishSpawningEvent.Invoke();
    }

    // randomly chooses what type of enemie to spawn
    private GameObject SpawnRandomEnemy()
    {
        int max = Random.Range(0, enemyTypes.Length + 1);
        max = Mathf.Clamp(max, 0, Wave.WaveNum);
        int num = Random.Range(0, max);
        return enemyTypes[num];
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void SetNumEnemies(int amount) { numOfEnemies = amount; }
    public int GetNumEnemies() { return numOfEnemies; }
}
