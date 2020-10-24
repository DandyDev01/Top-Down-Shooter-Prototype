using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    #region Singlton
    public static Wave instance;

    public void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }

    #endregion

    #region Variables
    [SerializeField] SpawnEnemies[] spawners = null;
    [SerializeField] float timeBetweenWaves = 5;
    [SerializeField] GameObject ammoPrefab;
    public static int WaveNum { get; private set; }
    private int waveCompletionIndex;                // used to determine when the wave ends
    private EnemyMovement[] finalEnemies;
    private int finalEnemyCount;
    private bool hasStarted = false;
    #endregion

    #region Delegates & Events

    public delegate void WaveOverDelegate();
    public WaveOverDelegate WaveOverEvent;

    #endregion

    #region Unity Methods
    private void Start()
    {
    }
	#endregion

	#region Public Methods

    // call to start the first wave
    public void StartWaves()
    {
        if (hasStarted)
            return;

        WaveNum = 1;
        SetUpSpawnerEvents();
        hasStarted = true;
        StartCoroutine(StartWave());
    }

    // call when the player dies to restart the game on the current lvl
    public void Clear()
    {
    }

	#endregion

	#region Private Methods

	// the wave has started
	private IEnumerator StartWave()
    {
        // show the ui that tells the player what wave number they are on
        if (WaveOverEvent != null)
            WaveOverEvent.Invoke();

        Player.coins += 5;


        if (WaveNum % 2 == 0)
            Instantiate(ammoPrefab, transform);

        yield return new WaitForSeconds(timeBetweenWaves);
        StartSpawning();
    }

    // start calling the spawn method on the spawners
    private void StartSpawning()
    {
        foreach(SpawnEnemies spawner in spawners)
        {
            StartCoroutine(spawner.Spawn());
        } 
    }

    // setup the FinishSpawningEvent for the spawners
    private void SetUpSpawnerEvents()
    {
        foreach (SpawnEnemies spawner in spawners)
        {
            spawner.FinishSpawningEvent += CheckForWaveFinish;
        }
    }

    // when all spawners have finished setup everything to detemin when the last enemy is killed
    private void CheckForWaveFinish()
    {
        waveCompletionIndex++;
        if (waveCompletionIndex == spawners.Length)
        {
            GetAllEnemies();
        } 
    }

    private void GetAllEnemies()
    {
        finalEnemies = FindObjectsOfType<EnemyMovement>();
        finalEnemyCount = finalEnemies.Length;

        foreach (EnemyMovement enemy in finalEnemies)
        {
            enemy.GetComponent<Health>().OnDeathEvent += FinalEnemyKilled;
        }
    }

    // determin if all enemies have been killed
    private void FinalEnemyKilled()
    {
        finalEnemyCount--;
        if(finalEnemyCount == 0)
        {
            NewWave();
        }
    }

    // wave has finished start a new one
    private void NewWave()
    {
        WaveNum++;
        finalEnemyCount = 0;
        waveCompletionIndex = 0;
        StartCoroutine(StartWave());
    }

    #endregion

    #region Getters & Setters

    #endregion
}
