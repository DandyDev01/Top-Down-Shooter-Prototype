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
    [SerializeField] TextMeshProUGUI waveOverUI = null;
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] GameObject shopUI = null;
    [SerializeField] GameObject weaponSelectUI = null;
    [SerializeField] Button gunButtons;
    [SerializeField] Button bulletButtons;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        waveOverUI.color = new Color(0, 0, 0, 0);
        gameOverUI.SetActive(false);
        shopUI.SetActive(false);
        weaponSelectUI.SetActive(true);

        ammoText.text = Player.ammo.ToString();
        coinText.text = Player.coins.ToString() + " $$$";

        Interactable.OnInteractEvent += UpdateAmmoTxt;
        Interactable.OnInteractEvent += UpdateCoinTxt;
        Wave.instance.WaveOverEvent += WaveOverUI;
        Player.instance.OnDeathEvent += GameOverUI;
        Gun.OnShootEvent += UpdateAmmoTxt;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            WeaponSelectUI();
        }
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
        shopUI.SetActive(true);
        weaponSelectUI.SetActive(false);
    }

    public void WeaponSelectUI()
    {
        gameOverUI.SetActive(false);
        shopUI.SetActive(false);
        weaponSelectUI.SetActive(true);
    }

    public void Back()
    {
        gameOverUI.SetActive(true);
        shopUI.SetActive(false);
        weaponSelectUI.SetActive(false);
    }

    public void CloseAll()
    {
        gameOverUI.SetActive(false);
        shopUI.SetActive(false);
        weaponSelectUI.SetActive(false);
        Time.timeScale = 1;
    }
}
