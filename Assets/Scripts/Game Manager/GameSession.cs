using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    #region Singelton
    public static GameSession instance;
    private void Awake()
    {
        if(instance != null)
            return;

        instance = this;
        
    }
    #endregion

    public void StartGame()
    {
        Wave.instance.StartWaves();
        UIController.instance.CloseAll();
    }
	
}
