using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    #region Singleton
    public static UIController instance;
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
	#endregion

	#region Variables
	[SerializeField] TextMeshProUGUI ammoText = null;
    [SerializeField] TextMeshProUGUI coinText = null;
    [SerializeField] TextMeshProUGUI waveText = null;
    [SerializeField] TextMeshProUGUI waveOverUI = null;
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] GameObject weaponSelectUI = null;
    private bool pause;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        waveOverUI.color = new Color(0, 0, 0, 0);
        gameOverUI.SetActive(false);
        weaponSelectUI.SetActive(true);

        ammoText.text = Player.ammo.ToString();
        coinText.text = Player.coins.ToString() + " $$$";

        Interactable.OnInteractEvent += UpdateAmmoTxt;
        Interactable.OnInteractEvent += UpdateCoinTxt;
        Wave.instance.WaveOverEvent += WaveOverUI;
        Wave.instance.WaveOverEvent += UpdateCoinTxt;
        Wave.instance.WaveOverEvent += UpdateWaveTextUI;
        Player.instance.OnDeathEvent += GameOverUI;
        Gun.OnShootEvent += UpdateAmmoTxt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            Time.timeScale = 0;
            pause = true;
            WeaponSelectUI();
        } 
        else if(Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            CloseAll();
        }
    }

    public void UpdateWaveTextUI()
    {
        waveText.text = Wave.WaveNum.ToString();
    }

    public void UpdateAmmoTxt()
    {
        ammoText.text = Player.ammo.ToString();
    }

    public void UpdateCoinTxt()
    {
        coinText.text = Player.coins.ToString() + " $$$";
    }

    public void WaveOverUI()
    {
        StartCoroutine(WaveOverCouroutine());
    }

    IEnumerator WaveOverCouroutine()
    {
        waveOverUI.text = "Wave " + Wave.WaveNum + " Start";
        waveOverUI.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(5);
        waveOverUI.color = new Color(0, 0, 0, 0);
    }

    public void GameOverUI()
    {
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        gameOverUI.SetActive(true);
    }

    public void ShopUI()
    {
        gameOverUI.SetActive(false);
        weaponSelectUI.SetActive(false);
    }

    public void WeaponSelectUI()
    {
        gameOverUI.SetActive(false);
        weaponSelectUI.SetActive(true);
    }

    public void Back()
    {
        gameOverUI.SetActive(true);
        weaponSelectUI.SetActive(false);
    }

    public void CloseAll()
    {
        gameOverUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1;
        pause = false;
    }
}
