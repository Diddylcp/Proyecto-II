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
        player = GameObject.FindGameObjectWithTag("GameController");
        playerController = player.GetComponent<PlayerController>();
        Debug.Log(playerController.GetMoney());
        if (playerController.GetMoney() > soldierCost)
            Instantiate(SoldierImage, new Vector3(0, 1, -19), Quaternion.identity);
    }


}
