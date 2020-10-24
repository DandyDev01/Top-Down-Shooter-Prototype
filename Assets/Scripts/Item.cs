using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] GameObject item = null;
    [SerializeField] int cost;
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void Equip()
    {
        if (cost <= Player.coins)
        {
            Player.coins -= cost;
            switch (name)
            {
                case "shotgun":
                    player.GunChange(item);
                    break;
                case "assult rifle":
                    player.GunChange(item);
                    break;
                case "pistol":
                    player.GunChange(item);
                    break;
                case "cross bow":
                    player.GunChange(item);
                    break;
                case "sniper":
                    player.GunChange(item);
                    break;
                case "pircing":
                    player.BulletChange(item);
                    break;
                case "arrow":
                    player.BulletChange(item);
                    break;
                case "scatter":
                    player.BulletChange(item);
                    break;
                case "explosive":
                    player.BulletChange(item);
                    break;
                case "bullet":
                    player.BulletChange(item);
                    break;
                default:
                    Debug.Log("no");
                    break;
            }
        }
        else
            Debug.Log("Not enough money");
    }   

}
