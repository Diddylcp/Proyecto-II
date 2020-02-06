using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptOnDrag : MonoBehaviour
{

    GameObject player;
    PlayerController playerController;
    public GameObject SoldierImage;
    public int soldierCost;

        // Start is called before the first frame update
    public void AddImageOnHUD()
    {
        if (tag == "AllyTroop") player = GameObject.Find("PlayerEconomy");
        else player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
        if (playerController.GetMoney() > soldierCost)
            Instantiate(SoldierImage, new Vector3(0, 1, -19), Quaternion.identity);
    }


}
